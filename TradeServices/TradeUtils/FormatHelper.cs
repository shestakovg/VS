using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeUtils
{
    public  class FormatHelper
    {
        public static string FormatDate(DateTime? date)
        {
            return String.Format("{0:F}", date);
        }

        public static string FormatDateSimple(DateTime? date)
        {
            DateTime locDate = Convert.ToDateTime(date);
            return date==null ? String.Empty : locDate.ToString("dd.MM.yyyy hh:mm");
        }

        public static string FormatDateToTimeSimple(DateTime? date)
        {
            DateTime locDate = Convert.ToDateTime(date);
            return date == null ? String.Empty : locDate.ToString("HH:mm");
        }
    }
}
