using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HtmlFragmentHelper
{
    internal static class Extensions
    {
        #region char extensions
        public static bool ContainsCaseInsensitive(this char[] chars, char chrContains)
        {
            return chars.Any(c => c.EqualsCaseInsensitive(chrContains));
        }

        public static bool EqualsCaseInsensitive(this char chrOrig, char chrCompare)
        {
            return char.ToUpperInvariant(chrOrig).Equals(char.ToUpperInvariant(chrCompare));
        }
        #endregion char extensions

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

        /// <summary>
        /// This extension ensure that html tags found in a string are not multi-line. Though not
        /// invalid HTML, in some Markdown situations (like when bullheadedly converted a quote block
        /// by adding "> " to the start of each line), it's easier to deal with tags on a single line.
        ///
        /// So this turns...
        ///
        /// &lt;a href="spam.html"
        ///     style="color:orange">
        ///
        /// ... into...
        ///
        /// &lt;a href="spam.html" style="color:orange">
        ///
        /// etc etc.
        /// </summary>
        /// <param name="str">The string to parse that may contain multiline html tags.</param>
        /// <returns>A string with all multiline html tags as single-line tags. Parsing assumes all "&lt;"
        /// characters are the start of tags.</returns>
        public static string ConslidateMultilineHtmlTags(this string str)
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

        /// <summary>
        /// Extension method that will parse a string, returning the characters
        /// between `findAfterMarker` and `findBeforeMarker` as a string.
        /// </summary>
        /// <param name="str">The string upon which the extension method is being called.</param>
        /// <param name="findAfterMarker">The string to find BEFORE the extraction value. (exclusive)
        /// Wild-cards ARE supported. Use an asterisk to denote wild card areas. To escape
        /// asterisks, put two "**" in a row. NOTE: "#$$#" will be translated as an escaped asterisk.
        /// There is no escape value for "#$$#".</param>
        /// <param name="findBeforeMarker">The string to find AFTER the extraction value (exclusive).
        /// Wild-cards are NOT supported in the findBeforeMarker.</param>
        /// <returns>null if the markers are not found or no characters exist between them,
        /// and the string between the two markers if they are, if any.</returns>
        public static string ExtractBetween(this string str,
            string findAfterMarker, string findBeforeMarker)
        {
            string ret = null;
            int findAfterStartLoc = -1;

            string[] astrFindAfters = findAfterMarker.Replace("**", "#$$#").Split(new[] { '*' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < astrFindAfters.Length; i++)
            {
                string strCurrentFind = astrFindAfters[i].Replace("#$$#", "*");
                findAfterStartLoc = str.ToLower().IndexOf(strCurrentFind.ToLower());    // Case insensitive for now.
                if (findAfterStartLoc < 0)
                {
                    break;
                }
                findAfterStartLoc += strCurrentFind.Length;
                str = str.Substring(findAfterStartLoc);
            }

            int findBeforeStartLoc = str.ToLower().IndexOf(findBeforeMarker.ToLower());

            if (-1 < findBeforeStartLoc)
            {
                ret = str.Substring(0, findBeforeStartLoc);
            }

            return ret;
        }


        #region Escaped index and attribute jive.


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
                startPos, Extensions.ESCAPE_TYPES.BACKSLASH_BEFORE, false, '\'', '"');
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
                } while (!propEnd.Equals(-1) && str[propEnd - 1].Equals('\\'));

                if (!propEnd.Equals(-1))
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
        #endregion Escaped index and attribute jive.

        public static Tuple<string, string> OperateOnInlineStyles(
             this string str,
             INLINE_STYLE_OPERATIONS operation = INLINE_STYLE_OPERATIONS.REMOVE_STYLES_AND_CLASSES
        )
        {
            string source = str;
            string styles = string.Empty;

            switch (operation)
            {
                case INLINE_STYLE_OPERATIONS.NONE:
                    // No changes.
                    break;

                case INLINE_STYLE_OPERATIONS.NOT_YET_IMPLEMENTED__CONSOLIDATE_STYLES_AND_REMOVE_COLORS:
                    _ConsolidateStylesAndRemoveColors(source);
                    break;

                case INLINE_STYLE_OPERATIONS.REMOVE_STYLES_AND_CLASSES:
                    int lessThan = source.IndexOf('<');
                    int greaterThan = -1 == lessThan
                        ? -1
                        : source.IndexOfNextUnescapedGreaterThan(lessThan);

                    while (lessThan != -1 && greaterThan != -1)
                    {
                        string tagOriginal = source.Substring(lessThan, greaterThan - lessThan + 1);
                        string tag = tagOriginal;
                        bool replace = false;

                        string attribute = tag.RetrieveAttribute("style", true);
                        while (attribute != null)
                        {
                            replace = true;
                            tag = tag.Replace(attribute, "");
                            attribute = tag.RetrieveAttribute("style", true);
                        }

                        attribute = tag.RetrieveAttribute("class", true);
                        while (attribute != null)
                        {
                            replace = true;
                            tag = tag.Replace(attribute, "");
                            attribute = tag.RetrieveAttribute("class", true);
                        }

                        if (replace)
                        {
                            source = source.Replace(tagOriginal, tag);
                        }

                        lessThan = source.IndexOf('<', lessThan + 1);
                        if (-1 != lessThan) {
                            greaterThan = source.IndexOfNextUnescapedGreaterThan(lessThan);
                        }

                        System.Diagnostics.Debug.WriteLine(lessThan);
                    }
                    break;

            }

            return new Tuple<string, string>(source, styles);
        }

        // TODO: This is going to zap a little too much, as we'll lose
        // image, repeat, attachment, and/or position if they're defined
        // in the `background` "shortcut".
        // https://www.w3.org/TR/CSS2/colors.html#background-properties
        // (That is, I'm now removing most anything in not just `background-color`,
        // but also `background`)
        private static string _PatternStripColorStyle = @"(background|(background-|border-)*color):[a-zA-Z0-9(), ]+;";

        private static Tuple<string, string> _ConsolidateStylesAndRemoveColors(
             string str
        )
        {
            string ret = string.Empty;
            bool hasLeadingLt = false;

            if (!string.IsNullOrEmpty(str))
            {
                str = str.UnescapeAnyEscapedQuotesInStyle();

                hasLeadingLt = str[0].Equals('<');
                string[] astrTags = str.Split(new[] { '<' }, StringSplitOptions.RemoveEmptyEntries);
                StringBuilder stringBuilder = new StringBuilder();

                if (astrTags.Length > 0)
                {
                    foreach (string tag in astrTags)
                    {
                        if (tag.IndexOfNextUnescapedGreaterThan(0) > -1)
                        {
                            string[] astrHalfs = tag.Split(new[] { '>' }, 2);
                            astrHalfs[0] = Regex.Replace(astrHalfs[0], _PatternStripColorStyle, " ");
                            stringBuilder.Append('<').Append(astrHalfs[0]).Append('>').Append(astrHalfs[1]);
                        }
                        else
                        {
                            stringBuilder.Append('<').Append(tag);
                        }
                    }
                    ret = stringBuilder.ToString();
                }
                else
                {
                    ret = str;
                }
            }

            ret = hasLeadingLt ? ret : ret.TrimStart('<');


            // TODO: Clean up old code and try to create classes for inline styles.
            throw new NotImplementedException("Can't consolidate styles in html yet.");
        }

        /// <summary>
        /// Clean any instances of &quot; or, strangely, &amp;quot;, out of inline styles that are
        /// double-quoted and replace with single quotes. (Clipboards tend to return stuff like:
        /// style="color: rgb(69, 69, 69); font-family: &quot;Segoe UI&quot;;"
        /// or
        /// font-family: &amp;quot;Source Sans Pro&amp;quot;,&amp;quot;Helvetica Neue&amp;quot;
        /// for some reason.)
        /// </summary>
        /// <param name="str">The html string to clean.</param>
        /// <returns>Html string with single quotes in place of &quot; found within inline, double-quoted styles.</returns>
        public static string UnescapeAnyEscapedQuotesInStyle(this string str)
        {
            int lessThanLoc = str.IndexOf('<');
            int greaterThanLoc = str.IndexOf('>', Math.Max(lessThanLoc, 0));

            while (lessThanLoc > -1 && greaterThanLoc > lessThanLoc)
            {
                int styleLoc = str.IndexOf("style=\"", lessThanLoc);

                // I'm duping checks for > lessThanLoc here and below, even though
                // the IndexOf with starting position should already guarantee that.
                while (styleLoc > lessThanLoc && styleLoc < greaterThanLoc)
                {
                    int closingQuoteLoc = str.IndexOf("\"", styleLoc + 7);

                    // If this was an escaped quote, skip it and find the next.
                    // I guess in an inline style, though, that shouldn't happen.
                    while (closingQuoteLoc > 0 && str[closingQuoteLoc - 1].Equals('\\'))
                    {
                        closingQuoteLoc = str.IndexOf("\"", closingQuoteLoc);
                    }

                    if (
                        styleLoc > lessThanLoc && styleLoc < greaterThanLoc
                        && closingQuoteLoc > lessThanLoc && closingQuoteLoc < greaterThanLoc
                    )
                    {
                        // Note that we have to go one PAST the closingQuoteLoc to include the quote.
                        str = str.Substring(0, styleLoc) +
                              str.Substring(styleLoc, closingQuoteLoc - styleLoc + 1).Replace("&quot;", "'").Replace("&amp;quot;", "'") +
                              str.Substring(closingQuoteLoc + 1);

                        styleLoc = str.IndexOf("style=\"", closingQuoteLoc);
                    }
                    else
                    {
                        // If we didn't find another quote anywhere, well, there's no reason to continue.
                        break;
                    }

                }

                lessThanLoc = str.IndexOf('<', greaterThanLoc);
                greaterThanLoc = str.IndexOf('>', Math.Max(lessThanLoc, 0));    // Can still use 0; we're just avoiding throws.
            }

            return str;
        }
    }
}
