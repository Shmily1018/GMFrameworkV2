using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Description;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;
using GoldMantis.Common;
using GoldMantis.Service;
using GoldMantis.Service.Biz;
using GoldMantis.ServiceNew.ServiceInterface;

namespace GoldMantis.ServiceNew
{
	public static class Shared
	{
		public static readonly string VirtualWcfDirectoryName = "~/wcf"; // Must start with "~/", must not end with "/".
		public static readonly string ThisAssemblyFullName = typeof(Shared).Assembly.FullName;
		public static readonly string ThisNamespaceName = typeof(Shared).Namespace;
	}

	public class ServicePathProvider : VirtualPathProvider
	{
		public override bool FileExists(string virtualPath)
		{
			string appRelativeVirtualPath = ToAppRelativeVirtualPath(virtualPath);
			if (IsVirtualFile(appRelativeVirtualPath))
			{
				return true;
			}
			else
			{
				return Previous.FileExists(virtualPath);
			}
		}

		public override System.Web.Hosting.VirtualFile GetFile(string virtualPath)
		{
			string appRelativeVirtualPath = ToAppRelativeVirtualPath(virtualPath);

			if (IsVirtualFile(appRelativeVirtualPath))
			{
				return new ServiceFile(virtualPath, typeof(DynamicHostFactory).FullName);
			}
			else
			{
				return Previous.GetFile(virtualPath);
			}
		}

		public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
		{
			string appRelativeVirtualPath = ToAppRelativeVirtualPath(virtualPath);

			if (IsVirtualFile(appRelativeVirtualPath) || IsVirtualDirectory(appRelativeVirtualPath))
			{
				return null;
			}
			else
			{
				return Previous.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
			}
		}

		private bool IsVirtualFile(string appRelativeVirtualPath)
		{
			if (appRelativeVirtualPath.StartsWith(Shared.VirtualWcfDirectoryName + "/", StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}
			return false;
		}

		private bool IsVirtualDirectory(string appRelativeVirtualPath)
		{
			return appRelativeVirtualPath.Equals(Shared.VirtualWcfDirectoryName, StringComparison.OrdinalIgnoreCase);
		}

		private string ToAppRelativeVirtualPath(string virtualPath)
		{
			string appRelativeVirtualPath = VirtualPathUtility.ToAppRelative(virtualPath);

			if (!appRelativeVirtualPath.StartsWith("~/"))
			{
				throw new HttpException("Unexpectedly does not start with ~.");
			}
			return appRelativeVirtualPath;
		}
	}

	public class ServiceFile : VirtualFile
	{
		private string _Factory;

		public ServiceFile(string vp, string factory)
			: base(vp)
		{
			_Factory = factory;
		}

		public override Stream Open()
		{
			MemoryStream ms = new MemoryStream();
			StreamWriter tw = new StreamWriter(ms);
			tw.Write(string.Format(CultureInfo.InvariantCulture,
			  "<%@ServiceHost language=c# Debug=\"true\" Factory=\"{0}\"%>", HttpUtility.HtmlEncode(_Factory)));
			tw.Flush();
			ms.Position = 0;
			return ms;
		}
	}

	public static class AssemblyCache
	{
		public static IList<Type> InterfaceTypeCollection
		{
			get;
			set;
		}

		public static IList<Type> NormalTypeCollection
		{
			get;
			set;
		}

		public static void Initialize()
		{
			Initialize(null, string.Empty, string.Empty);
		}

		public static void Initialize(string[] strAssemblyInfo, string fullOutDirectory, string assemblyNamePrefix)
		{
			if (InterfaceTypeCollection != null || NormalTypeCollection != null)
				return;

			InterfaceTypeCollection = new List<Type>();
			NormalTypeCollection = new List<Type>();

			IList<Assembly> assemblyInterfaceCollection = new List<Assembly>();
			IList<Assembly> assemblyNormalCollection = new List<Assembly>();

			if (strAssemblyInfo == null)
			{
				/* 处理从server执行生成程序集
				 -----------------------------------------------------------------*/
				AssemblyElementCollection configurationCollection = ServerConfigurationSection.GetServerConfiguration().AssemblyCollection;
				foreach (var item in configurationCollection)
				{
					if (((AssemblyElement)item).Type.Equals("interface"))
						assemblyInterfaceCollection.Add(Assembly.Load(((AssemblyElement)item).Name));
					else
						assemblyNormalCollection.Add(Assembly.Load(((AssemblyElement)item).Name));
				}
			}
			else
			{
				/* 处理从exe执行生成程序集
				 -----------------------------------------------------------------*/
				foreach (var item in strAssemblyInfo)
				{
					string[] tempArray = item.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
					if (tempArray[1].Equals("interface", StringComparison.OrdinalIgnoreCase))
						assemblyInterfaceCollection.Add(Assembly.Load(tempArray[0]));
					else
						assemblyNormalCollection.Add(Assembly.Load(tempArray[0]));
				}
			}
			foreach (var item in assemblyInterfaceCollection)
				((List<Type>)InterfaceTypeCollection).AddRange(item.GetTypes());

			foreach (var item in assemblyNormalCollection)
				((List<Type>)NormalTypeCollection).AddRange(item.GetTypes());

			for (int i = 0; i < NormalTypeCollection.Count(); i++)
			{
				if (NormalTypeCollection[i].BaseType == null)
					NormalTypeCollection.Remove(NormalTypeCollection[i]);
			}


			/* add dynamic interface type
			 -----------------------------------------------------------------*/
			if (string.IsNullOrEmpty(fullOutDirectory))
			{
				((List<Type>)InterfaceTypeCollection).AddRange(DynamicInterfaceAssembly.ServiceInterfaceCollection.ToArray());
				((List<Type>)NormalTypeCollection).AddRange(DynamicServiceAssembly.ServiceTypeCollection.ToArray());
			}
			else
			{
				DynamicInterfaceAssembly.CreateDynamicAssembly(fullOutDirectory, assemblyNamePrefix);
				((List<Type>)InterfaceTypeCollection).AddRange(DynamicInterfaceAssembly.ServiceInterfaceCollection.ToArray());

				DynamicServiceAssembly.CreateDynamicAssembly(fullOutDirectory, assemblyNamePrefix);
				((List<Type>)NormalTypeCollection).AddRange(DynamicServiceAssembly.ServiceTypeCollection.ToArray());
			}

		}
	}

	public class DynamicHostFactory : ServiceHostFactory
	{
		public DynamicHostFactory() { }
		public override ServiceHostBase CreateServiceHost(string constructorString, Uri[] baseAddresses)
        {

			string serviceName = baseAddresses[0].LocalPath.Substring(baseAddresses[0].LocalPath.LastIndexOf('/')).Replace("/", string.Empty).Replace(".svc", string.Empty);
			string iServiceName = "I" + serviceName;
			AssemblyCache.Initialize();

			Type interfaceType = AssemblyCache.InterfaceTypeCollection.FirstOrDefault(x => x.Name.Equals(iServiceName, StringComparison.OrdinalIgnoreCase));
			if (interfaceType == null)
				return null;

			Type implementedType = AssemblyCache.NormalTypeCollection.FirstOrDefault(x => x.Name.Equals(serviceName, StringComparison.OrdinalIgnoreCase));
			if (implementedType == null)
				return null;

			ServiceHost host = new ServiceHost(implementedType, baseAddresses);

			/* basicHttpBinding 绑定节点,java客户端可选择此绑定。
			----------------------------------------------------------*/
			BasicHttpBinding basicHttpBinding = new BasicHttpBinding("basicHttpBinding-binding");
			ServiceEndpoint basicHttpEndpoint = host.AddServiceEndpoint(interfaceType, basicHttpBinding, "basicHttp");

			
            /* wsHttpBinding 绑定节点
            ----------------------------------------------------------*/
            WSHttpBinding wsHttpBinding = new WSHttpBinding("wsHttpBinding-binding");
            ServiceEndpoint wsHttpEndpoint = host.AddServiceEndpoint(interfaceType, wsHttpBinding, "wsHttp");


            /* webHttpBinding 绑定节点
            ----------------------------------------------------------*/
            //WebHttpBinding webHttpBinding = new WebHttpBinding("webHttpBinding-binding");
            //ServiceEndpoint webHttpEndpoint = host.AddServiceEndpoint(interfaceType, webHttpBinding, "webHttp");

            //webHttpEndpoint.Behaviors.Add(new WebHttpBehavior()
            //{
            //    AutomaticFormatSelectionEnabled = true,
            //    HelpEnabled = true
            //});

            /* 是否启用应用程序上下文
            ----------------------------------------------------------*/
            if (System.Configuration.ConfigurationManager.AppSettings["EnableApplicationContext"] != null && System.Configuration.ConfigurationManager.AppSettings["EnableApplicationContext"].Equals(true.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                basicHttpEndpoint.Behaviors.Add(new ContextPropagationBehavior());
                wsHttpEndpoint.Behaviors.Add(new ContextPropagationBehavior());
                //webHttpEndpoint.Behaviors.Add(new ContextPropagationBehavior());
            }

            ServiceMetadataBehavior metaDataBehavior = new ServiceMetadataBehavior { HttpGetEnabled = true };
            host.Description.Behaviors.Add(metaDataBehavior);

            //host.Description.Behaviors.Add(new NHibernateBehavior());
            host.Description.Behaviors.Add(new UseRequestHeadersForMetadataAddressBehavior());
            //host.Description.Behaviors.Add(new ServiceDebugBehavior());

            ServiceDebugBehavior debug = host.Description.Behaviors.Find<ServiceDebugBehavior>();

            if (debug == null)
            {
                host.Description.Behaviors.Add(
                     new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });
            }
            else
            {
                if (!debug.IncludeExceptionDetailInFaults)
                    debug.IncludeExceptionDetailInFaults = true;
            }

			//mex 数据交换终结点，暂不启用
			/*
			host.AddServiceEndpoint(typeof(IMetadataExchange), webHttpBinding, "webhttp-mex");
			host.AddServiceEndpoint(typeof(IMetadataExchange), wsHttpBinding, "wshttp-mex");
			host.AddServiceEndpoint(typeof(IMetadataExchange), basicHttpBinding, "basichttp-mex");
			*/
			return host;
		}
	}

	public class DynamicServiceAssembly
	{
		private static readonly Type voidType = Type.GetType("System.Void");

        private static readonly Type baseBiz = typeof(BaseBiz);

        private static readonly Type baseBiz0Parameter = typeof(BaseTableBiz<>);
        private static readonly Type baseBiz2Parameter = typeof(BaseViewBiz<>);
        private static readonly Type baseBizProcedureParameter = typeof(BaseSPBiz<,>);

		private static readonly MethodAttributes implicitImplementation = MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig;
		private static IList<Type> serviceTypeCollection = null;
		public static IList<Type> ServiceTypeCollection
		{
			get
			{
				if (serviceTypeCollection == null)
				{
					CreateDynamicAssembly();
				}
				return serviceTypeCollection;
			}
		}

		public static void CreateDynamicAssembly()
		{
			CreateDynamicAssembly(string.Empty, string.Empty);
		}
		public static void CreateDynamicAssembly(string fullOutDirectory, string assemblyNamePrefix)
		{
			/* initialize varies
			 ----------------------------------------------*/
			serviceTypeCollection = new List<Type>();
			assemblyNamePrefix = string.IsNullOrEmpty(assemblyNamePrefix) ? ServerConfigurationSection.GetServerConfiguration().DllOutput.AssemblyNamePrefix : assemblyNamePrefix;
			string universalName = assemblyNamePrefix + ".Service";

			/* Create Assembly
			 ----------------------------------------------*/
			if (string.IsNullOrEmpty(fullOutDirectory))
			{
				string outDirectory = ServerConfigurationSection.GetServerConfiguration().DllOutput.Path;
				fullOutDirectory = HttpRuntime.AppDomainAppPath + outDirectory;
			}
			if (!Directory.Exists(fullOutDirectory))
				Directory.CreateDirectory(fullOutDirectory);
			AssemblyName aname = new AssemblyName(universalName);
			AppDomain currentDomain = AppDomain.CurrentDomain;
			AssemblyBuilder asmBuilder = currentDomain.DefineDynamicAssembly(aname, AssemblyBuilderAccess.RunAndSave, fullOutDirectory);

			/* Create Module
			 ----------------------------------------------*/
			ModuleBuilder modBuilder = asmBuilder.DefineDynamicModule(universalName, universalName + ".dll");

			/* Create Class
			 ----------------------------------------------*/
			IList<Type> targetBizClassCollection = AssemblyCache.NormalTypeCollection.Where(x => x.BaseType.GUID.Equals(baseBiz0Parameter.GUID) 
                || x.BaseType.GUID.Equals(baseBiz2Parameter.GUID)
                || x.BaseType.GUID.Equals(baseBiz.GUID) 
                || x.BaseType.GUID.Equals(baseBizProcedureParameter.GUID)).ToList();

			//Console.WriteLine(DateTime.Now.ToString() + "- 创建Service代理类******************************************************************");
			foreach (var targetBizClassItem in targetBizClassCollection)
			{
			    if (targetBizClassItem.Name == "BaseTableBiz`1")
			    {
                    continue;
			    }

			    Type parentType = null;
				Type interfaceType = null;
				Type entityType = null;

				try
				{
					if (targetBizClassItem.BaseType.GUID.Equals(baseBiz0Parameter.GUID)
                        || targetBizClassItem.BaseType.GUID.Equals(baseBiz.GUID)
                        || targetBizClassItem.BaseType.GUID.Equals(baseBiz2Parameter.GUID)
                        || targetBizClassItem.BaseType.GUID.Equals(baseBizProcedureParameter.GUID))
					{
                        parentType = typeof(BaseService);
						//parentType = parentType.MakeGenericType(targetBizClassItem);
					}
					else
					{
                        //parentType = typeof(BaseService<,>);
                        //entityType = targetBizClassItem.BaseType.GetGenericArguments()[0];
                        //parentType = parentType.MakeGenericType(entityType, targetBizClassItem);
					}

					interfaceType = AssemblyCache.InterfaceTypeCollection.FirstOrDefault(x => x.Name.Equals(String.Format("I{0}Service",targetBizClassItem.Name.Substring(0, targetBizClassItem.Name.Length - 3))));

					//TypeBuilder typeBuilder = modBuilder.DefineType(string.Format("{0}.Service{1}", targetBizClassItem.Namespace, targetBizClassItem.Name.Substring(3)), TypeAttributes.Public, parentType);
                    TypeBuilder typeBuilder = modBuilder.DefineType(string.Format("{0}.{1}Service", targetBizClassItem.Namespace, targetBizClassItem.Name.Substring(0, targetBizClassItem.Name.Length - 3)), TypeAttributes.Public, parentType);

                    typeBuilder.SetParent(parentType);
                    typeBuilder.AddInterfaceImplementation(interfaceType);

					/* Create Service Class Attribute
					----------------------------------------------*/
					Type[] attributeParams = new Type[] { };
					bool isHasServiceBehaviorAttribute = false;
                    bool isHasSessionPerCallServiceBehavior = false;

					var classAttributeDataCollection = targetBizClassItem.GetCustomAttributesData();
					foreach (var customAttributeDataItem in classAttributeDataCollection)
					{
						if (customAttributeDataItem.Constructor.DeclaringType == typeof(ServiceContractAttribute))
							continue;
						if (customAttributeDataItem.Constructor.DeclaringType == typeof(ServiceBehaviorAttribute))
							isHasServiceBehaviorAttribute = true;
                        if (customAttributeDataItem.Constructor.DeclaringType == typeof(SessionPerCallServiceBehavior))
                            isHasSessionPerCallServiceBehavior = true;
						typeBuilder.SetCustomAttribute(CustomAttributeHelper.ToAttributeBuilder(customAttributeDataItem));
					}

                    if (!isHasSessionPerCallServiceBehavior)
                    {
                        ConstructorInfo methodCtorInfo = typeof(SessionPerCallServiceBehavior).GetConstructor(attributeParams);
                        CustomAttributeBuilder attributeBuilder = new CustomAttributeBuilder(methodCtorInfo, new object[] { });
                        typeBuilder.SetCustomAttribute(attributeBuilder);
                    }

					if (!isHasServiceBehaviorAttribute)
					{
						ConstructorInfo classCtorInfo = typeof(ServiceBehaviorAttribute).GetConstructor(attributeParams);
						PropertyInfo instanceContextModeProperty = typeof(ServiceBehaviorAttribute).GetProperty("InstanceContextMode");
						CustomAttributeBuilder attributeBuilder = new CustomAttributeBuilder(classCtorInfo, new object[] { }, new PropertyInfo[] { instanceContextModeProperty }, new object[] { InstanceContextMode.PerCall });
						typeBuilder.SetCustomAttribute(attributeBuilder);
					}
				    


				    MethodInfo[] methods = interfaceType.GetMethods();
                    
                    //定义业务层变量，名称：service
                    FieldBuilder fieldBuilder = typeBuilder.DefineField("service", targetBizClassItem, FieldAttributes.Private);

					foreach (var method in methods)
					{

                        //判断是否含有返回值
						var hasResult = method.ReturnType != voidType;

                        //获取方法参数签名
						ParameterInfo[] parameterInfoCollection = method.GetParameters();
						IList<Type> parameterType = new List<Type>();
                        IList<Object> p=new List<object>();
						foreach (var parameterInfoItem in parameterInfoCollection)
						{
							parameterType.Add(parameterInfoItem.ParameterType);
                            p.Add(parameterInfoItem.ParameterType);
						}

                        //定义方法签名
						MethodBuilder mothodbuilder = typeBuilder.DefineMethod(method.Name, implicitImplementation, method.ReturnType, parameterType.ToArray());

						ILGenerator generator = mothodbuilder.GetILGenerator();

                        generator.Emit(OpCodes.Nop);
                        if (hasResult)
                            generator.DeclareLocal(method.ReturnType);

                        generator.Emit(OpCodes.Ldarg_0);
                        generator.Emit(OpCodes.Ldfld, fieldBuilder);

                        for (int i = 1, length = parameterInfoCollection.Count() + 1; i < length; i++)
                        {
                            generator.Emit(OpCodes.Ldarg_S, i);
                        }

                        generator.Emit(OpCodes.Callvirt, fieldBuilder.FieldType.GetMethod(method.Name, parameterType.ToArray()));

                        if (hasResult)
                        {
                            generator.Emit(OpCodes.Stloc_0);
                            generator.Emit(OpCodes.Ldloc_0);
                        }
                        generator.Emit(OpCodes.Ret);
					}
					Type resultType = typeBuilder.CreateType();
					serviceTypeCollection.Add(resultType);
					Console.WriteLine(resultType.FullName + "- 创建成功");
				}
				catch (Exception ex)
				{
					Console.WriteLine("xxxxxx" + targetBizClassItem.FullName + "- 创建失败");
				}
			}
			asmBuilder.Save(universalName + ".dll");
			Console.WriteLine(DateTime.Now.ToString() + string.Format("- {0}.dll 写入成功", universalName));
		}
	}

	public class DynamicInterfaceAssembly
	{
        private static readonly Type baseBiz = typeof(BaseBiz);
        private static readonly Type baseBiz0Parameter = typeof(BaseTableBiz<>);
        private static readonly Type baseBiz2Parameter = typeof(BaseViewBiz<>);
        private static readonly Type baseBizProcedureParameter = typeof(BaseSPBiz<,>);

		private static readonly MethodAttributes interfaceMethodAttributes = MethodAttributes.Abstract | MethodAttributes.Virtual | MethodAttributes.Public;

		private static IList<Type> serviceInterfaceCollection = null;
		public static IList<Type> ServiceInterfaceCollection
		{
			get
			{
				if (serviceInterfaceCollection == null)
					CreateDynamicAssembly();
				return serviceInterfaceCollection;
			}
		}

		public static void CreateDynamicAssembly()
		{
			CreateDynamicAssembly(string.Empty, string.Empty);
		}
		public static void CreateDynamicAssembly(string fullOutDirectory, string assemblyNamePrefix)
		{
			/* initialize varies
			 ----------------------------------------------*/
			serviceInterfaceCollection = new List<Type>();
			assemblyNamePrefix = string.IsNullOrEmpty(assemblyNamePrefix) ? ServerConfigurationSection.GetServerConfiguration().DllOutput.AssemblyNamePrefix : assemblyNamePrefix;
			string universalName = assemblyNamePrefix + ".Interface";
			/* Create Assembly
			 ----------------------------------------------*/
			if (string.IsNullOrEmpty(fullOutDirectory))
			{
				string outDirectory = ServerConfigurationSection.GetServerConfiguration().DllOutput.Path;
				fullOutDirectory = HttpRuntime.AppDomainAppPath + outDirectory;
			}
			if (!Directory.Exists(fullOutDirectory))
				Directory.CreateDirectory(fullOutDirectory);
			AssemblyName aname = new AssemblyName(universalName);
			AppDomain currentDomain = AppDomain.CurrentDomain;
			AssemblyBuilder asmBuilder = currentDomain.DefineDynamicAssembly(aname, AssemblyBuilderAccess.RunAndSave, fullOutDirectory);

			/* Create Module
			 ----------------------------------------------*/
			ModuleBuilder modBuilder = asmBuilder.DefineDynamicModule(universalName, universalName + ".dll");

			/* Create Class
			 ----------------------------------------------*/
			IList<Type> targetBizClassCollection = AssemblyCache.NormalTypeCollection.Where(x => x.BaseType.GUID.Equals(baseBiz0Parameter.GUID) 
                || x.BaseType.GUID.Equals(baseBiz2Parameter.GUID)
                || x.BaseType.GUID.Equals(baseBiz.GUID)
                || x.BaseType.GUID.Equals(baseBizProcedureParameter.GUID)).ToList();
			Console.WriteLine(DateTime.Now + "- 创建Interface代理类******************************************************************");
			foreach (var targetBizClassItem in targetBizClassCollection)
			{
				Type parentType = null;
				Type entityType = null;

				try
				{
                    if (targetBizClassItem.BaseType.GUID.Equals(baseBiz0Parameter.GUID) 
                        || targetBizClassItem.BaseType.GUID.Equals(baseBiz2Parameter.GUID)
                        || targetBizClassItem.BaseType.GUID.Equals(baseBiz.GUID) 
                        || targetBizClassItem.BaseType.GUID.Equals(baseBizProcedureParameter.GUID))
					{
						parentType = typeof(IService);
					}
					else
					{
                        //parentType = typeof(IBaseService<>);
                        //entityType = targetBizClassItem.BaseType.GetGenericArguments()[0];
                        //parentType = parentType.MakeGenericType(entityType);
					}

                   // TypeBuilder typeBuilder = modBuilder.DefineType(string.Format("{0}.I{1}Service", targetBizClassItem.Namespace, targetBizClassItem.Name.Substring(3)), TypeAttributes.Public | TypeAttributes.Interface | TypeAttributes.AnsiClass | TypeAttributes.Abstract | TypeAttributes.AutoClass);

                    TypeBuilder typeBuilder = modBuilder.DefineType(string.Format("{0}.I{1}Service", targetBizClassItem.Namespace, targetBizClassItem.Name.Substring(0, targetBizClassItem.Name.Length - 3)), TypeAttributes.Public | TypeAttributes.Interface | TypeAttributes.AnsiClass | TypeAttributes.Abstract | TypeAttributes.AutoClass);


                    //GoldMantis.Service.Contract.Contract
                    typeBuilder.AddInterfaceImplementation(parentType);

					/* Set Service Class Attribute
					----------------------------------------------*/
					Type[] attributeParams = new Type[] { };
					IList<CustomAttributeData> classAttributeDataCollection = targetBizClassItem.GetCustomAttributesData();
					bool isHasServiceContractAttribute = false;
					foreach (var customAttributeDataItem in classAttributeDataCollection)
					{
						if (customAttributeDataItem.Constructor.DeclaringType == typeof(ServiceContractAttribute))
							isHasServiceContractAttribute = true;
						typeBuilder.SetCustomAttribute(CustomAttributeHelper.ToAttributeBuilder(customAttributeDataItem));
					}
					if (!isHasServiceContractAttribute)
					{
						ConstructorInfo classCtorInfo = typeof(ServiceContractAttribute).GetConstructor(attributeParams);
						CustomAttributeBuilder attributeBuilder = new CustomAttributeBuilder(classCtorInfo, new object[] { });
						typeBuilder.SetCustomAttribute(attributeBuilder);
					}

					MethodInfo[] methods = targetBizClassItem.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

					foreach (var method in methods)
					{
						/* check OperationBehavior Attribute
						----------------------------------------------*/
						IList<CustomAttributeData> methodAttributeDataCollection = method.GetCustomAttributesData();
                        //var operationContractCount = methodAttributeDataCollection.Count(x => x.Constructor.DeclaringType == typeof(OperationContractAttribute));
                        //if (operationContractCount == 0)
                        //    continue;

                       
                       


						ParameterInfo[] parameterInfoCollection = method.GetParameters();

						IList<Type> parameterType = new List<Type>();
						foreach (var parameterInfoItem in parameterInfoCollection)
						{
							parameterType.Add(parameterInfoItem.ParameterType);
						}
						MethodBuilder methodbuilder = typeBuilder.DefineMethod(method.Name, interfaceMethodAttributes, method.ReturnType, parameterType.ToArray());

						for (int i = 0; i < parameterInfoCollection.Count(); i++)
						{
							methodbuilder.DefineParameter(i + 1, ParameterAttributes.None, parameterInfoCollection[i].Name);
						}

                        ConstructorInfo methodCtorInfo = typeof(OperationContractAttribute).GetConstructor(attributeParams);
                        CustomAttributeBuilder attributeBuilder = new CustomAttributeBuilder(methodCtorInfo, new object[] { });
                        methodbuilder.SetCustomAttribute(attributeBuilder);

						/* Set Customer Attribute
						----------------------------------------------*/


                        

						foreach (var customAttributeDataItem in methodAttributeDataCollection)
						{
							methodbuilder.SetCustomAttribute(CustomAttributeHelper.ToAttributeBuilder(customAttributeDataItem));
						}

						if (targetBizClassItem.BaseType.GUID.Equals(baseBizProcedureParameter.GUID))
						{

						}

					}
					Type resultType = typeBuilder.CreateType();
					serviceInterfaceCollection.Add(resultType);
					Console.WriteLine(resultType.FullName + "- 创建成功");
				}
				catch (Exception ex)
				{
					Console.WriteLine("xxxxxx" + targetBizClassItem.FullName + "- 创建失败");
				}
			}

			asmBuilder.Save(universalName + ".dll");

			Console.WriteLine(DateTime.Now.ToString() + string.Format("- {0}.dll 写入成功", universalName));
		    //Console.ReadKey();

		}
	}

	public class CustomAttributeHelper
	{
		public static CustomAttributeBuilder ToAttributeBuilder(CustomAttributeData data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}

			var constructorArguments = new List<object>();
			foreach (var ctorArg in data.ConstructorArguments)
			{
				constructorArguments.Add(ctorArg.Value);
			}

			var propertyArguments = new List<PropertyInfo>();
			var propertyArgumentValues = new List<object>();
			var fieldArguments = new List<FieldInfo>();
			var fieldArgumentValues = new List<object>();
			foreach (var namedArg in data.NamedArguments)
			{
				var fi = namedArg.MemberInfo as FieldInfo;
				var pi = namedArg.MemberInfo as PropertyInfo;

				if (fi != null)
				{
					fieldArguments.Add(fi);
					fieldArgumentValues.Add(namedArg.TypedValue.Value);
				}
				else if (pi != null)
				{
					propertyArguments.Add(pi);
					propertyArgumentValues.Add(namedArg.TypedValue.Value);
				}
			}
			return new CustomAttributeBuilder(
			  data.Constructor,
			  constructorArguments.ToArray(),
			  propertyArguments.ToArray(),
			  propertyArgumentValues.ToArray(),
			  fieldArguments.ToArray(),
			  fieldArgumentValues.ToArray());
		}
	}

	public class DynamicGenerateAssembly
	{
		public const string EXE_FILE_NAME = "DynamicGenerateAssembly.exe";
		public const string ASSEMBLY_NAME = "DynamicGenerateAssembly";
		public static void CreateExe()
		{
			AssemblyElementCollection configurationCollection = ServerConfigurationSection.GetServerConfiguration().AssemblyCollection;
			IList<string> strAssemblyInfo = new List<string>();
			foreach (var item in configurationCollection)
			{
				strAssemblyInfo.Add(string.Format("{0}#{1}", ((AssemblyElement)item).Name, ((AssemblyElement)item).Type));
			}

			AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(new AssemblyName(ASSEMBLY_NAME), AssemblyBuilderAccess.Save, HttpRuntime.BinDirectory);
			ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(ASSEMBLY_NAME, EXE_FILE_NAME);
			TypeBuilder typeBuilder = moduleBuilder.DefineType("Program", TypeAttributes.Class | TypeAttributes.Public);
			
			//create contruct method
			typeBuilder.DefineDefaultConstructor(MethodAttributes.Public |
									MethodAttributes.SpecialName |
									MethodAttributes.RTSpecialName);

			//end
			
			MethodBuilder methodBuilder = typeBuilder.DefineMethod("Main", MethodAttributes.HideBySig | MethodAttributes.Public | MethodAttributes.Static, typeof(void), new Type[] { typeof(string[]) });
			ILGenerator generator = methodBuilder.GetILGenerator();
			generator.Emit(OpCodes.Nop);
			LocalBuilder strArrayBuilder = generator.DeclareLocal(typeof(string[]));
			generator.Emit(OpCodes.Ldc_I4_S, configurationCollection.Count);
			generator.Emit(OpCodes.Newarr, typeof(string));
			generator.Emit(OpCodes.Stloc, strArrayBuilder);
			for (int i = 0; i < strAssemblyInfo.Count; i++)
			{
				generator.Emit(OpCodes.Ldloc_0);
				generator.Emit(OpCodes.Ldc_I4_S, i);
				generator.Emit(OpCodes.Ldstr, strAssemblyInfo[i]);
				generator.Emit(OpCodes.Stelem_Ref);
			}
			generator.Emit(OpCodes.Ldloc_0);
			generator.Emit(OpCodes.Ldstr, HttpRuntime.AppDomainAppPath + ServerConfigurationSection.GetServerConfiguration().DllOutput.Path);
			generator.Emit(OpCodes.Ldstr, ServerConfigurationSection.GetServerConfiguration().DllOutput.AssemblyNamePrefix);
			generator.Emit(OpCodes.Call, typeof(AssemblyCache).GetMethod("Initialize", new Type[] { typeof(string[]), typeof(string), typeof(string) }));
			generator.Emit(OpCodes.Ret);
			typeBuilder.CreateType();
			assemblyBuilder.SetEntryPoint(methodBuilder, PEFileKinds.ConsoleApplication);
			if (!File.Exists(HttpRuntime.BinDirectory + EXE_FILE_NAME))
				assemblyBuilder.Save(EXE_FILE_NAME);
		}
	}
}


