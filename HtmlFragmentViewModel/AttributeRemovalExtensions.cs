using System;
using System.Linq;

namespace HtmlFragmentHelper
{
    public static class AttributeRemovalExtensions
    {
        /// <summary>
        /// How are characters escaped in your string? If it's DOUBLED_CHAR,
        /// then "" is an escaped " and should be ignored for quotes.
        /// If BACKSLASH_BEFORE, then \" is an escaped ". Etc.
        /// </summary>
        public enum ESCAPE_TYPES
        {
            DOUBLED_CHAR = 1,
            BACKSLASH_BEFORE = 2,
            NONE = 3
        }

        public static int IndexOfEndDelimiter(
            this string str,
            int startPos,
            ESCAPE_TYPES escapeType,
            bool anyDelimiterCanClose,
            params char[] astrDelimitersCaseINsensitive
        )
        {
            int endSplit = 0;

            for (int i = startPos; i < str.Length; i++)
            {
                while (i < str.Length && !astrDelimitersCaseINsensitive.ContainsCaseInsensitive(str[i]))
                    i++;

                if (i < str.Length)
                {
                    // Should splitters also be case insensitive? I'm going to say yes.
                    if (astrDelimitersCaseINsensitive.ContainsCaseInsensitive(str[i]))
                    {
                        // Remember which of the astrSplittingTokens was found to find the right closing char.
                        char[] closingChars = anyDelimiterCanClose
                            ? astrDelimitersCaseINsensitive // any delimiter can close the pattern.
                            : new[] { str[i] };            // only the found delimiter can close.

                        i++;
                        while (i < str.Length)
                        {
                            if (closingChars.ContainsCaseInsensitive(str[i]))
                                if (escapeType.Equals(ESCAPE_TYPES.DOUBLED_CHAR) && i + 1 < str.Length && closingChars.ContainsCaseInsensitive(str[i + 1]))
                                    i = i + 2;
                                else if (escapeType.Equals(ESCAPE_TYPES.BACKSLASH_BEFORE) && i > 0 && str[i - 1].Equals('\\'))
                                    i++;
                                else
                                {
                                    // Found the [unescaped] end tag.
                                    endSplit = i;
                                    break;
                                }
                            else
                                i++;
                        }
                    }
                }
            }

            return endSplit;
        }

        private static bool _isEscaped(string str, ESCAPE_TYPES escapeType, int pos)
        {
            bool ret = false;

            switch (escapeType)
            {
                case ESCAPE_TYPES.BACKSLASH_BEFORE:
                    ret = pos > 0 && str[pos - 1].Equals('\\')
                        && !(pos > 1 && str[pos - 2].Equals('\\'));
                    break;

                case ESCAPE_TYPES.DOUBLED_CHAR:
                    ret = pos + 1 < str.Length && str[pos + 1].Equals(str[pos]);
                    break;
            }

            return ret;
        }

        public static int IndexOfNextUnescapedGreaterThan(this string str, int startPos)
        {
            return str.IndexOfOutsideOfDelimitersCaseSensitive(">",
                startPos, ESCAPE_TYPES.BACKSLASH_BEFORE, false, '\'', '"');
        }

        public static int IndexOfOutsideOfDelimitersCaseSensitive(
            this string str,
            string strToFind,
            int startPos,
            ESCAPE_TYPES escapeType,
            bool anyDelimiterCanClose,
            params char[] astrDelimiters
        )
        {
            int ret = -1;
            int i = startPos;

            while (i < str.Length)
            {
                if (
                    astrDelimiters.Contains(str[i])
                    && !_isEscaped(str, escapeType, i)
                )
                {
                    // Remember which of the astrSplittingTokens was found to find the right closing char.
                    char[] closingChars = anyDelimiterCanClose
                        ? astrDelimiters                // any delimiter can close the pattern.
                        : new[] { str[i] };            // only the found delimiter can close.

                    i++;

                    while (
                        i < str.Length
                        && (
                            !closingChars.Contains(str[i])
                            || _isEscaped(str, escapeType, i)
                        )
                    )
                    {
                        i++;
                    }

                    i++;    // get past the matching end delimiter.
                }

                if (
                    i < str.Length
                    && str[i].Equals(strToFind[0])
                    && (i - 1 + strToFind.Length) < str.Length
                    && str.Substring(i, strToFind.Length).Equals(strToFind)
                )
                {
                    ret = i;
                    break;
                }

                i++;
            }

            return ret;
        }

        public static string RetrieveAttribute(this string str, string attributeName, bool withAssignment = false)
        {
            string ret = null;

            int propStart = -1;
            string lookFor = null;

            var match = System.Text.RegularExpressions.Regex.Match(str,
                $@" {attributeName} *= *['""]",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            if (match.Success)
            {
                propStart = match.Index;
                lookFor = match.Value;
            }

            if (propStart > -1 && str.Length > propStart + lookFor.Length + 2)
            {
                char quote = lookFor[lookFor.Length - 1];
                int propEnd = propStart + lookFor.Length;

                do
                {
                    // HTML 4.01 and 5 say you can't escape a quote inside of a quoted string.
                    // So we should be able to safely use IndexOf here instead of IndexOfOutsideOfDelimiters[...]
                    propEnd = str.IndexOf(quote, propEnd + 1) + 1;
                } while (!propEnd.Equals(0) && str[propEnd - 1].Equals('\\'));

                if (!propEnd.Equals(0))
                {
                    int retStart = withAssignment
                        ? propStart
                        : propStart + lookFor.Length;

                    int retEnd = withAssignment
                        ? propEnd
                        : propEnd - 1;

                    ret = str.Substring(retStart, retEnd - retStart).Trim();
                }
            }

            return ret;
        }
    }
}
