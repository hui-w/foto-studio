using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QLike.AutoUpdate
{
    /// <summary>
    /// Class for Base64 encoding and decoding
    /// </summary>
    public class Base64
    {
        /// <summary>
        /// Endode to Base64 string
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Encode(string type, string value)
        {
            try
            {
                string encode = "";
                byte[] bytes = System.Text.Encoding.GetEncoding(type).GetBytes(value);
                try
                {
                    encode = Convert.ToBase64String(bytes);
                }
                catch
                {
                    encode = value;
                }
                return encode;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Encode to Base64 string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Encode(string value)
        {
            return Base64.Encode("UTF-8", value);
        }

        /// <summary>
        /// Encode to Base64 string without "/"
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string EncodeForPath(string value)
        {
            //string str = Base64.Encode("UTF-8", value);
            //str = str.Replace("/", "_");
            //str = str.Replace("+", "-");
            //return str;
            return value;
        }

        /// <summary>
        /// Decode from Base64 string
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Decode(string code_type, string code)
        {
            try
            {
                string decode = "";
                byte[] bytes = Convert.FromBase64String(code);
                try
                {
                    decode = System.Text.Encoding.GetEncoding(code_type).GetString(bytes);
                }
                catch
                {
                    decode = code;
                }
                return decode;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Decode from Base64 string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Decode(string code)
        {
            return Base64.Decode("UTF-8", code);
        }

        /// <summary>
        /// Decode from Base64 string without "/"
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string DecodeForPath(string code)
        {
            //code = code.Replace("_", "/");
            //code = code.Replace("-", "+");
            //return Base64.Decode("UTF-8", code);
            return code;
        }
    }//end of class
}
