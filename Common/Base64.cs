using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QLike.Foto.Common
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
                byte[] bytes = Encoding.GetEncoding(type).GetBytes(value);
                return Convert.ToBase64String(bytes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Encode to Base64 string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Encode(string value)
        {
            try
            {
                return Base64.Encode("UTF-8", value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Encode to Base64 string without "/"
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string EncodeForPath(string value)
        {
            try
            {
                string str = Base64.Encode("UTF-8", value);
                str = str.Replace("/", "_");
                str = str.Replace("+", "-");
                return str;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                byte[] bytes = Convert.FromBase64String(code);
                return Encoding.GetEncoding(code_type).GetString(bytes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Decode from Base64 string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Decode(string code)
        {
            try
            {
                return Base64.Decode("UTF-8", code);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Decode from Base64 string without "/"
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string DecodeForPath(string code)
        {
            try
            {
                code = code.Replace("_", "/");
                code = code.Replace("-", "+");
                return Base64.Decode("UTF-8", code);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }//end of class
}
