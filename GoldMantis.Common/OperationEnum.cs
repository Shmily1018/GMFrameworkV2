/*********************************************************
** Copyright (c)     2015 Gold Mantis Co., Ltd. 
** FileName:         OperationEnum.cs
** Creator:          Joe
** CreateDate:       2015-11-3
** Changer:
** LastChangeDate:
** Description:      菜单操作枚举
** VersionInfo:
*********************************************************/

namespace GoldMantis.Common
{
    public enum OperationEnum
    {
        /// <summary>
        /// 
        /// </summary>
        None = 0,

        /// <summary>
        /// 新增
        /// </summary>
        Add = 1,

        /// <summary>
        /// 编辑
        /// </summary>
        Edit = 2,

        /// <summary>
        /// 删除
        /// </summary>
        Delete = 3,

        /// <summary>
        /// 查看
        /// </summary>
        View = 4,

        /// <summary>
        /// 审核
        /// </summary>
        Check = 5,

        /// <summary>
        /// 导出  
        /// </summary>
        Export = 6,

        /// <summary>
        /// 打印
        /// </summary>
        Print = 7,

        /// <summary>
        /// 导出
        /// </summary>
        Import = 8
    }
}