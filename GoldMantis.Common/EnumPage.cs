using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using GoldMantis.Common.CustomAttribute;

namespace GoldMantis.Common
{

    public enum EnumPageMode
    {
        /// <summary>
        /// 新增
        /// </summary>
        [Description("新增")]
        Add,

        /// <summary>
        /// 编辑
        /// </summary>
        [Description("编辑")]
        Edit,

        /// <summary>
        /// 查看
        /// </summary>
        [Description("查看")]
        View,

        /// <summary>
        /// 审批
        /// </summary>
        [Description("审批")]
        Check,

        /// <summary>
        /// 跟踪
        /// </summary>
        [Description("跟踪")]
        Trace,

        /// <summary>
        /// 没有设置
        /// </summary>
        [Description("没有设置")]
        None
    }


    [DataContract]
    public enum EnumExportType
    {
        [EnumMember]
        导出本页 = 0,
        [EnumMember]
        导出全部 = 1
    }

    public enum EnumCheckStatus
    {
        /// <summary>
        /// 未审核
        /// </summary>
        [EnumCode("未审核")]
        Init = 0,

        /// <summary>
        /// 审批中
        /// </summary>
        [EnumCode("审批中")]
        Doing = 1,

        /// <summary>
        /// 审批通过
        /// </summary>
        [EnumCode("审批通过")]
        Ok = 2,

        /// <summary>
        /// 终止
        /// </summary>
        [EnumCode("终止")]
        No = 3,

        /// <summary>
        /// 过账
        /// </summary>
        [EnumCode("已审核")]
        Post = 4,

        /// <summary>
        /// 作废
        /// </summary>
        [EnumCode("已作废")]
        Cancel = 5
    }

    public enum EnumERPSystem
    {
        OA = 0,
        CRM = 1,
        DP = 2,
        PM = 3,
        MRP = 4,
        HR = 5,
        QP = 6,
        DM = 7
    }

    public class EnumHelper
    {
        public static string GetDescription(Enum value)
        {
            Type enumType = value.GetType();
            // 获取枚举常数名称。
            string name = Enum.GetName(enumType, value);
            if (name != null)
            {
                // 获取枚举字段。
                FieldInfo fieldInfo = enumType.GetField(name);
                if (fieldInfo != null)
                {
                    // 获取描述的属性。
                    DescriptionAttribute attr = Attribute.GetCustomAttribute(fieldInfo,
                        typeof(DescriptionAttribute), false) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return null;
        }

        public static List<SelectListItem> GetSelectList(Type enumType)
        {
            return (from int i in Enum.GetValues(enumType) select new SelectListItem() {Text = Enum.GetName(enumType, i), Selected = false, Value = i.ToString()}).ToList();
        }

        public static List<SelectListItem> GetSelectListWithFirstOption(Type enumType, string firstOptionText, string firstOptionValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Text = firstOptionText, Selected = true, Value = firstOptionValue });

            foreach (int i in Enum.GetValues(enumType))
            {
                SelectListItem listitem = new SelectListItem() { Text = Enum.GetName(enumType, i), Selected = false, Value = i.ToString() };
                list.Add(listitem);
            }
            

            return list;
        }

    }

}

