using System;
using System.Collections.Generic;
using System.Text;

namespace BrgyProfileCore.Core
{
    public static class Helpers
    {
        public static string ReplaceString(string originalText, string startString, string endString, string replacement)
        {
            var builder = new StringBuilder(originalText);
            var start = IndexOf(builder, startString, 0, false);
            var end = IndexOf(builder, endString, endString.Length - 1, false);
            builder.Remove(start, end - start);
            builder.Insert(start, replacement);
            return builder.ToString();
        }

        public static int IndexOf(this StringBuilder sb, string value, int startIndex, bool ignoreCase)
        {
            int index;
            int length = value.Length;
            int maxSearchLength = (sb.Length - length) + 1;

            if (ignoreCase)
            {
                for (int i = startIndex; i < maxSearchLength; ++i)
                {
                    if (Char.ToLower(sb[i]) == Char.ToLower(value[0]))
                    {
                        index = 1;
                        while ((index < length) && (Char.ToLower(sb[i + index]) == Char.ToLower(value[index])))
                            ++index;

                        if (index == length)
                            return i;
                    }
                }

                return -1;
            }

            for (int i = startIndex; i < maxSearchLength; ++i)
            {
                if (sb[i] == value[0])
                {
                    index = 1;
                    while ((index < length) && (sb[i + index] == value[index]))
                        ++index;

                    if (index == length)
                        return i;
                }
            }

            return -1;
        }
    }
}
