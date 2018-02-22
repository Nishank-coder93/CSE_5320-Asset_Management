using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSE_5320.Helper
{
    public class ResponseHelper
    {
        public string fixResult(string input)
        {
            var step_1 = input.Replace("\\", "");
            var n = 2;

            var result = string.Empty;

            if (step_1.Length > n * 2)
                result = step_1.Substring(n, step_1.Length - (n * 2));
            else
                result = string.Empty;

            var output = "{" + result + "}";
            return output;
        }

        public string fixListResult(string input)
        {
            var step_1 = input.Replace("\\", "");
            var n = 2;

            var result = string.Empty;

            if (step_1.Length > n * 2)
                result = step_1.Substring(n, step_1.Length - (n * 2));
            else
                result = string.Empty;

            var output = "[" + result + "]";
            return output;
        }
    }
}