using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using GoldMantis.Entity;
using GoldMantis.Entity;
using GoldMantis.WorkFlow.WCFClient;

namespace GoldMantis.Web.ViewModel.User
{
    /// <summary>
    /// 列表页模型
    /// </summary>
    [DataContract]
    public class ModelDemoIndex : BaseModel
    {
        private SearchDemo _searchEntity = new SearchDemo();

        /// <summary>
        /// 列表数据源
        /// </summary>
        [DataMember]
        public IList<Demo> GridDataSources
        { get; set; }

        /// <summary>
        /// 查询实体
        /// </summary>
        [DataMember]
        public SearchDemo SearchEntity
        {
            get
            {
                return _searchEntity;
            }
            set
            {
                _searchEntity = value;
            }
        }

        ///// <summary>
        ///// 导出成Excel格式
        ///// </summary>
        ///// <param name="datasource">租赁户信息管理</param>
        //public void ExportExcel(IList<Demo> datasource)
        //{
        //    if (ExportObject.ExportType == null)
        //        return;
        //    var exportInstance = ExportHelper.GetInstance();
        //    exportInstance.ExportExcel<Demo>(datasource, ExportObject, "租赁户信息管理");
        //}
    }




    /// <summary>
    /// 查询面板模型
    /// </summary>
    [DataContract]
    public class SearchDemo : BaseSearch
    {
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "ID")]
        [DataMember(Order = 1)]
        public virtual string ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "Name")]
        [DataMember(Order = 2)]
        public virtual string Name { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "Age")]
        [DataMember(Order = 3)]
        public virtual int Age { get; set; }
    }

    //[DataContract]
    //public class ModelDemoCreate : BaseModel
    //{
    //    /// <summary>
    //    /// 用户实体
    //    /// </summary>
    //    [DataMember]
    //    public Demo Demo { get; set; }
    //    /// <summary>
    //    /// 记录文件路径
    //    /// </summary>
    //    [DataMember]
    //    public string FinalPath { get; set; }

    //    /// <summary>
    //    /// 记录文件在服务器端的相对路径（供客户端下载用）
    //    /// </summary>
    //    [DataMember]
    //    public string ServerFinalPath { get; set; }

    //}

    [DataContract]
    public class ModelDemoCreate : WFModel
    {
        /// <summary>
        /// 用户实体
        /// </summary>
        [DataMember]
        public Demo Demo { get; set; }
        /// <summary>
        /// 记录文件路径
        /// </summary>
        [DataMember]
        public string FinalPath { get; set; }

        /// <summary>
        /// 记录文件在服务器端的相对路径（供客户端下载用）
        /// </summary>
        [DataMember]
        public string ServerFinalPath { get; set; }

    }



}