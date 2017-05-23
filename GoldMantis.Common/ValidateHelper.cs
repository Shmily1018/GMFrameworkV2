/*********************************************************
** Copyright (c)     2015 Gold Mantis Co., Ltd. 
** FileName:         ValidateHelper.cs
** Creator:          Joe
** CreateDate:       2015-03-27
** Changer:
** LastChangeDate:
** Description:      辅助数据验证
** VersionInfo:
*********************************************************/

using System;
using System.Text.RegularExpressions;

namespace GoldMantis.Common
{
    public static class ValidateHelper
    {
        //邮件正则表达式
        public static readonly string EmailRegexString = @"^[a-z]([a-z0-9]*[-_]?[a-z0-9]+)*@([a-z0-9]*[-_]?[a-z0-9]+)+[\.][a-z]{2,3}([\.][a-z]{2})?$";
        public static readonly Regex EmailRegex = new Regex(EmailRegexString, RegexOptions.IgnoreCase);

        //手机号正则表达式
        public static readonly string MobileRegexString = "^(13|15|18)[0-9]{9}$";
        public static readonly Regex MobileRegex = new Regex(MobileRegexString);

        //固话号正则表达式
        public static readonly string PhoneRegexString = @"^(\d{3,4}-?)?\d{7,8}$";
        public static readonly Regex PhoneRegex = new Regex(PhoneRegexString);

        //IP正则表达式
        public static readonly string IpRegexString = @"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$";
        public static readonly Regex IpRegex = new Regex(IpRegexString);

        //日期正则表达式
        public static readonly string DateRegexString = @"(\d{4})-(\d{1,2})-(\d{1,2})";
        public static readonly Regex DateRegex = new Regex(DateRegexString);

        //数值(包括整数和小数)正则表达式
        public static readonly string NumericRegexString = @"^[-]?[0-9]+(\.[0-9]+)?$";
        public static readonly Regex NumericRegex = new Regex(NumericRegexString);

        //整数正则表达式
        public static readonly string IntegerRegexString = @"^[0-9]+$";
        public static readonly Regex IntegerRegex = new Regex(IntegerRegexString);

        //邮政编码正则表达式
        public static readonly string ZipcodeRegexString = @"^\d{6}$";
        public static readonly Regex ZipcodeRegex = new Regex(ZipcodeRegexString);

        //图片扩展名列表
        public static readonly string[] ImgExtensionList = new string[] { ".png", ".bmp", ".jpg", ".jpeg", ".gif" };


        #region 1.0 邮箱验证

        /// <summary>
        /// 是否为邮箱名
        /// </summary>
        /// <param name="email">email字符串</param>
        /// <returns>是否为邮箱名</returns>
        public static bool IsEmail(string email)
        {
            return EmailRegex.IsMatch(email);
        }

        #endregion

        #region 2.0 手机号验证

        /// <summary>
        /// 是否为手机号
        /// </summary>
        /// <param name="mobile">手机号字符串</param>
        /// <returns>是否为手机号</returns>
        public static bool IsMobile(string mobile)
        {
            return MobileRegex.IsMatch(mobile);
        }

        #endregion

        #region 3.0 固话号码验证

        /// <summary>
        /// 是否为固话号
        /// </summary>
        /// <param name="phone">固话号字符串</param>
        /// <returns>是否为固话号</returns>
        public static bool IsPhone(string phone)
        {
            return PhoneRegex.IsMatch(phone);
        }

        #endregion

        #region 4.0 身份证号码验证

        /// <summary>
        /// 是否是身份证号
        /// </summary>
        /// <param name="idCard">身份证号</param>
        /// <returns>是否是身份证号</returns>
        public static bool IsIdCard(string idCard)
        {
            bool result = false;

            if (idCard.Length == 18)
            {
                result = CheckIDCard18(idCard);
            }
            else if (idCard.Length == 15)
            {
                result = CheckIDCard15(idCard);
            }


            return result;
        }

        /// <summary>
        /// 是否为18位身份证号
        /// </summary>
        /// <param name="Id">身份证号</param>
        /// <returns>是否为18位身份证号</returns>
        private static bool CheckIDCard18(string Id)
        {
            //数字验证
            long n = 0;

            if (long.TryParse(Id.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;
            }


            //省份验证
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";

            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;
            }


            //生日验证
            string birth = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();

            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;
            }


            //校验码验证
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] ai = Id.Remove(17).ToCharArray();
            int sum = 0;

            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(wi[i]) * int.Parse(ai[i].ToString());
            }


            int y = -1;
            Math.DivRem(sum, 11, out y);

            if (arrVarifyCode[y] != Id.Substring(17, 1).ToLower())
            {
                return false;
            }


            return true;//符合GB11643-1999标准
        }

        /// <summary>
        /// 是否为15位身份证号
        /// </summary>
        /// <param name="Id">身份证号</param>
        /// <returns>是否为15位身份证号</returns>
        private static bool CheckIDCard15(string Id)
        {
            //数字验证
            long n = 0;

            if (long.TryParse(Id, out n) == false || n < Math.Pow(10, 14))
            {
                return false;
            }


            //省份验证
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";

            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;
            }


            //生日验证
            string birth = Id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time = new DateTime();

            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;
            }


            return true;//符合15位身份证标准
        }

        #endregion

        #region 5.0 日期验证

        /// <summary>
        /// 是否为日期
        /// </summary>
        /// <param name="date">日期字符串</param>
        /// <returns>是否为日期</returns>
        public static bool IsDate(string date)
        {
            return DateRegex.IsMatch(date);
        }

        #endregion

        #region 6.0 邮政编码验证

        /// <summary>
        /// 是否为邮政编码
        /// </summary>
        /// <param name="zipCode">邮政编码</param>
        /// <returns>是否为邮政编码</returns>
        public static bool IsZipCode(string zipCode)
        {
            return ZipcodeRegex.IsMatch(zipCode);
        }

        #endregion

        #region 7.0 图片文件名验证

        /// <summary>
        /// 是否是图片文件名
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>是否是图片文件名</returns>
        public static bool IsImgFileName(string fileName)
        {
            if (fileName.IndexOf('.') == -1)
            {
                return false;
            }


            string tempFileName = fileName.Trim().ToLower();
            string extension = tempFileName.Substring(tempFileName.LastIndexOf('.'));

            return ImgExtensionList.Contains(extension);
        }

        #endregion

        #region 8.0 IP验证

        /// <summary>
        /// 是否为IP
        /// </summary>
        /// <param name="ip">IP</param>
        /// <returns>是否为IP</returns>
        public static bool IsIP(string ip)
        {
            return IpRegex.IsMatch(ip);
        }

        /// <summary>
        /// 判断一个ip是否在另一个ip内
        /// </summary>
        /// <param name="sourceIP">检测ip</param>
        /// <param name="targetIP">匹配ip</param>
        /// <returns></returns>
        public static bool InIP(string sourceIP, string targetIP)
        {
            if (sourceIP.IsNullOrEmpty() || targetIP.IsNullOrEmpty())
            {
                return false;
            }


            string[] sourceIPBlockList = sourceIP.Split('.');
            string[] targetIPBlockList = targetIP.Split('.');
            int sourceIPBlockListLength = sourceIPBlockList.Length;

            for (int i = 0; i < sourceIPBlockListLength; i++)
            {
                if (targetIPBlockList[i] == "*")
                {
                    return true;
                }


                if (sourceIPBlockList[i] != targetIPBlockList[i])
                {
                    return false;
                }
                else
                {
                    if (i == 3)
                    {
                        return true;
                    }
                }
            }


            return false;
        }

        /// <summary>
        /// 判断一个ip是否在另一个ip内
        /// </summary>
        /// <param name="sourceIP">检测ip</param>
        /// <param name="targetIPList">匹配ip列表</param>
        /// <returns></returns>
        public static bool InIPList(string sourceIP, string[] targetIPList)
        {
            if (targetIPList != null && targetIPList.Length > 0)
            {
                foreach (string targetIP in targetIPList)
                {
                    if (InIP(sourceIP, targetIP))
                    {
                        return true;
                    }
                }
            }


            return false;
        }

        #endregion

        #region 9.0 数字验证

        /// <summary>
        /// 是否是整数
        /// </summary>
        /// <param name="numericStr">数值字符串</param>
        /// <returns>是否是整数</returns>
        public static bool IsInteger(string integerStr)
        {
            return IntegerRegex.IsMatch(integerStr);
        }

        /// <summary>
        /// 是否是数值(包括整数和小数)
        /// </summary>
        /// <param name="numericStr">数值字符串</param>
        /// <returns>是否是数值</returns>
        public static bool IsNumeric(string numericStr)
        {
            return NumericRegex.IsMatch(numericStr);
        }

        /// <summary>
        /// 是否是数值数组(包括整数和小数)
        /// </summary>
        /// <param name="numericStrList">数组</param>
        /// <returns>是否是数值数组</returns>
        public static bool IsNumericArray(string[] numericStrList)
        {
            if (numericStrList == null || numericStrList.Length == 0)
            {
                return false;
            }


            foreach (string numberStr in numericStrList)
            {
                if (IsNumeric(numberStr))
                {
                    continue;
                }


                return false;
            }


            return true;
        }

        /// <summary>
        /// 是否是数值连接的字符串(包括整数和小数)
        /// </summary>
        /// <param name="numericRuleStr">数值连接的字符串</param>
        /// <param name="splitChar">分隔符</param>
        /// <returns>是否是数值连接的字符串</returns>
        public static bool IsNumericRule(string numericRuleStr, char splitChar)
        {
            return IsNumericArray(numericRuleStr.Split(splitChar));
        }

        /// <summary>
        /// 是否是数值连接的字符串(包括整数和小数)
        /// 分隔符特定为 ','
        /// </summary>
        /// <param name="numericRuleStr">数值连接的字符串</param>
        /// <returns>是否是数值连接的字符串</returns>
        public static bool IsNumericRule(string numericRuleStr)
        {
            return IsNumericRule(numericRuleStr, ',');
        }

        #endregion

        #region 10.0 验证字符是否为汉字

        /// <summary>
        /// 用ASCII码判断字符是否是汉字
        /// </summary>
        /// <param name="c">字符</param>
        /// <returns>是否是汉字</returns>
        public static bool IsHanZiAscii(char c)
        {
            return (int) c > 127;
        }

        /// <summary>
        /// 用UNICODE编码判断字符是否是汉字
        /// </summary>
        /// <param name="c">字符</param>
        /// <returns>是否是汉字</returns>
        public static bool IsHanZiUnicode(char c)
        {
            return c >= 0x4e00 && c <= 0x9fbb;
        }

        #endregion
    }
}
