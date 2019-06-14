using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lso.Core
{
    class DbUtils
    {
        internal  static string ConvertToSqlLike(string term)
        {
            if (String.IsNullOrEmpty(term))
                return "";

            // turn all spaces, commas, etc into %
            string retval = term.Replace(" ", "%");
            retval = retval.Replace(",", "%");
            retval = retval.Replace("-", "%");
            retval = retval.Replace(".", "%");
            retval = retval.Replace("LLC", "%");
            return "%" + retval + "%";
        }
    }
}
