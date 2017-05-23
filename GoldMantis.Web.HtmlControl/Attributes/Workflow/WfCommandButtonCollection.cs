/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         WfCommandButtonCollection.cs
** Creator:          Joe
** CreateDate:       2015-11-10
** Changer:
** LastChangeDate:
** Description:      WorkFlow命令按钮集合
** VersionInfo:
*********************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GoldMantis.Web.HtmlControl
{
    public class WfCommandButtonCollection
    {
        private static WfCommandButtonCollection _instance;
        private List<WfCommandButton> _buttonList;

        static WfCommandButtonCollection()
        {
            _instance = new WfCommandButtonCollection();
        }

        private WfCommandButtonCollection()
        {
            _buttonList = new List<WfCommandButton>();
            Type buttonType = typeof(WfCommandButtonTypeEnum);
            FieldInfo[] fields = buttonType.GetFields();

            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.IsEnum)
                {
                    _buttonList.Add(new WfCommandButton((WfCommandButtonTypeEnum)Enum.Parse(buttonType, field.Name)));
                }
            }
        }

        public WfCommandButton FindButton(WfCommandButtonTypeEnum buttonType)
        {
            return _buttonList.Single(b => b.CommandButtonType == buttonType);
        }

        public static WfCommandButtonCollection Instance
        {
            get { return _instance; }
        }

        public static WfCommandButton Find(WfCommandButtonTypeEnum buttonType)
        {
            return Instance.FindButton(buttonType);
        }
    }
}