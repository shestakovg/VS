using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace TradeServices.Classes
{
    public class TradeUtils
    {
        public static string EncodeCyrilicString(string s)
        {
            string[] hexValuesSplit = s.TrimEnd().Split(' ');
            List<Byte> lst = new List<byte>();
            foreach (String hex in hexValuesSplit)
            {
                // Convert the number expressed in base-16 to an integer.
                int value = Convert.ToInt32(hex, 16);
                // Get the character corresponding to the integral value.
                string stringValue = Char.ConvertFromUtf32(value);
                char charValue = (char)value;
                //string f=String.Format("hexadecimal value = {0}, int value = {1}, char value = {2} or {3}",
                //                    hex, value, stringValue, charValue);
                lst.Add(Convert.ToByte(value));
            }
            Encoding en = Encoding.GetEncoding(1251);
            return en.GetString(lst.ToArray());
        }

        public static string Convert1CDateToTextDate(string _1cDate)
        {
            if (String.IsNullOrEmpty(_1cDate)) return "";

            return _1cDate.Substring(6, 2) + "." + _1cDate.Substring(4, 2) + "." + _1cDate.Substring(0, 4);

        }

        public static string FilterString(string inputStr)
        {
            return new String(inputStr.Where(c => (char.IsLetterOrDigit(c) ||
                             char.IsWhiteSpace(c) || c == '-' || c == ':' || c == ';' || c == '!' || c == '+' || c == '_' || c == '"' || c == '\'' || c == ' ' || c == ',' || c == '.' || c == ']' || c == '}' || c == '=')).ToArray());
        }


    }
}