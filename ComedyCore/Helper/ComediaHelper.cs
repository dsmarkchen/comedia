using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComediaCore.Helper
{
    public class ComediaHelper
    {
        public static string TrimUnecessaryCharacters(string input)
        {
            return input.Trim(new char[] { ',', ' ', '.', '"', '-', ';' });
        }
        public static string CantoReformat(string input, int lineNumber)
        {
            StringBuilder sb = new StringBuilder();
            if (lineNumber % 3 != 1)
            {
                sb.Append("    ");
            }
            sb.AppendLine(input.First().ToString().ToUpper() + String.Join("", input.Skip(1)));
            if (lineNumber % 3 == 0)
            {
                sb.AppendLine("");
            }
            return sb.ToString();
        }
    }

}
