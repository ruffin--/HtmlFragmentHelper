using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlFragmentHelper
{
    public static class Extensions
    {
        private static char[] _acCrLF = { '\n', '\r' };

        private static char? _addWhat(ref string src, int i, char last, bool checkSpace = false)
        {
            char? ret = null;

            switch (src[i])
            {
                case '\n':
                case '\r':
                    if (i == 0 || !last.Equals(' '))
                    {
                        ret = ' ';
                    }
                    break;

                case ' ':
                    if (!(checkSpace && last.Equals(' ')))
                        ret = ' ';
                    break;

                default:
                    ret = src[i];
                    break;
            }

            return ret;
        }

        public static string UnwrapHtmlTags(this string str)
        {
            StringBuilder sbUnwrapped = new StringBuilder();

            try
            {
                int i = 0;
                while (i < str.Length)
                {
                    if (!str[i].Equals('<'))
                    {
                        sbUnwrapped.Append(str[i]);
                        i++;
                    }
                    else
                    {
                        while (i < str.Length && !str[i].Equals('>'))
                        {
                            // I don't think we can start mid-html tag with an escaped ' or ",
                            // so I'm not checking for an escape.
                            if (str[i].Equals('\'') || str[i].Equals('"'))
                            {
                                char chrOpen = str[i];
                                sbUnwrapped.Append(str[i]);
                                i++;

                                while (i < str.Length
                                    && !(str[i].Equals(chrOpen) || (i > 0 && str[i - 1].Equals('\\') && !(i > 1 && str[i - 2].Equals('\\'))))
                                )
                                {
                                    char? c = _addWhat(ref str, i, sbUnwrapped[sbUnwrapped.Length - 1]);
                                    if (c.HasValue)
                                        sbUnwrapped.Append(c.Value);
                                    i++;
                                }

                                // Throw in final quote if we're not at the end of the line and it's escaped
                                // (because then we threw it in, above).
                                if (i < str.Length
                                    && str[i].Equals(chrOpen)
                                )
                                {
                                    sbUnwrapped.Append(str[i]);
                                }
                                i++;
                            }
                            else
                            {
                                char? c = _addWhat(ref str, i, 0 == i ? 'a' : sbUnwrapped[sbUnwrapped.Length - 1], true);
                                if (c.HasValue)
                                    sbUnwrapped.Append(c.Value);
                                i++;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debugger.Break();
                sbUnwrapped = new StringBuilder(str + "<!-- error: " + e.Message + " -->");
            }

            return sbUnwrapped.ToString();
        }
    }
}
