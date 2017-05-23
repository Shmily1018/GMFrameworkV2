using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoldMantis.Common.CustomAttribute;

namespace GoldMantis.Common
{
    public class PageHelper
    {
        public static string IsPostDecoration(int checkStatus)
        {
            string tem = @"<button type='button' class='btn {2} btn-xs'><i class='fa {1}'></i>{0}</button>";

            return String.Format(tem, EnumCode.GetFieldCode(Enum.Parse(typeof(EnumCheckStatus), checkStatus.ToString())), "fa-check", "blue");


        }
    }
}
