/*********************************************************
** Copyright (c)     2015 Gold Mantis Co., Ltd. 
** FileName:         InfoSafetyHelper.cs
** Creator:          Joe
** CreateDate:       2015-03-27
** Changer:
** LastChangeDate:
** Description:      辅助保密 敏感信息
** VersionInfo:
*********************************************************/

using System;
using System.Text;

namespace GoldMantis.Common
{
    public static class InfoSafetyHelper
    {
        /// <summary>
        /// 邮件转换为安全格式
        /// </summary>
        /// <param name="email">邮件</param>
        /// <returns></returns>
        public static string HiddenEmail(string email)
        {
            if (!ValidateHelper.IsEmail(email))
            {
                throw new ApplicationException(ExceptionMessage.NotEmail);
            }


            StringBuilder emailBuilder = new StringBuilder();
            int index = email.LastIndexOf('@');

            if (index == 1)
            {
                emailBuilder.Append("*");
            }
            else if (index == 2)
            {
                emailBuilder.AppendFormat("{0}*", email[0]);
            }
            else
            {
                emailBuilder.Append(email.Substring(0, 2));

                for (int count = index - 2; count > 0; count--)
                {
                    emailBuilder.Append("*");
                }
            }


            emailBuilder.Append(email.Substring(index));

            return emailBuilder.ToString();
        }

        /// <summary>
        /// 隐藏手机
        /// </summary>
        /// <param name="mobile">手机号码</param>
        /// <returns></returns>
        public static string HiddenMobile(string mobile)
        {
            if (!ValidateHelper.IsMobile(mobile))
            {
                throw new ApplicationException(ExceptionMessage.NotMobile);
            }


            return string.Format("{0}****{1}", mobile.Substring(0, 3), mobile.Substring(7));
        }

        /// <summary>
        /// 隐藏身份证
        /// </summary>
        /// <param name="idNumber">身份证号码</param>
        /// <returns></returns>
        public static string HiddenIdCard(string idNumber)
        {
            if (!ValidateHelper.IsIdCard(idNumber))
            {
                throw new ApplicationException(ExceptionMessage.NotIdCard);
            }


            StringBuilder idCardBuilder = new StringBuilder(idNumber.Substring(0, 6));

            for (int i = 6; i < idNumber.Length - 4; i++)
            {
                idCardBuilder.Append("*");
            }


            idCardBuilder.Append(idNumber.Substring(idNumber.Length - 4));

            return idCardBuilder.ToString();
        }
    }
}
