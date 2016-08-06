using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QLike.Foto.Grid
{
    public class Common
    {
        /// <summary>
        /// Get the int value from a percentage string
        /// </summary>
        /// <param name="percentage"></param>
        /// <returns></returns>
        internal static int GetIntFromPercentage(string percentage)
        {
            if (!percentage.EndsWith("%"))
            {
                return 0;
            }
            else
            {
                int intValue;
                if (Int32.TryParse(percentage.Substring(0, percentage.Length -1), out intValue))
                {
                    return intValue;
                }
                else
                {
                    return 0;
                }
            }
        }
    }//end of class
}
