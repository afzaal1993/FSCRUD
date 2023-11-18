using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class Helper
    {
        public static string GetDateAndTime()
        {
            return DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
        }
    }
}
