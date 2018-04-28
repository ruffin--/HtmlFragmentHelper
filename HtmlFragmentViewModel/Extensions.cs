using System;
using System.Collections.Generic;
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

        public static string NormalizeNewlineToCarriageReturn_(this string str)
        {
            str = str.Replace("\r\n", "\r");
            str = str.Replace("\n", "\r");
            return str;
        }

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
                str = str.Substring(findAfterStartLoc); // TODO: You could keep track of markers instead of making new strings every time, though I guess this is okay for smallish strings.
            }

            int findBeforeStartLoc = str.ToLower().IndexOf(findBeforeMarker.ToLower());

            if (-1 < findBeforeStartLoc)
            {
                ret = str.Substring(0, findBeforeStartLoc);
            }

            return ret;
        }

        private static Tuple<bool, string, string> _RemoveAttribute(string tag, string attributeToRemove)
        {
            bool replace = false;
            string attribute = null;
            string cleanedTag = tag;

            attribute = tag.RetrieveAttribute(attributeToRemove, true);
            while (attribute != null)
            {
                replace = true;
                cleanedTag = cleanedTag.Replace(attribute, "");
                attribute = cleanedTag.RetrieveAttribute(attributeToRemove, true);
            }

            return new Tuple<bool, string, string>(replace, attribute, cleanedTag);
        }

        // TODO: This is going to zap a little too much, as we'll lose
        // image, repeat, attachment, and/or position if they're defined
        // in the `background` "shortcut".
        // https://www.w3.org/TR/CSS2/colors.html#background-properties
        // (That is, I'm now removing most anything in not just `background-color`,
        // but also `background`)
        private static string _PatternStripColorStyle = @"(background|(background-|border-)*color):[a-zA-Z0-9(), ]+;";
        private static char[] _EitherQuote = { '"', '\'' };

        public static string ReplaceFirst(this string str, string find, string replace)
        {
            Regex regex = new Regex(Regex.Escape(find));
            var ret = regex.Replace(str, replace, 1);
            return ret;
        }

        private static Tuple<string, string> _HandleAttributes(string source, INLINE_STYLE_OPERATIONS operation)
        {
            long startTicks = DateTime.UtcNow.Ticks;
            int classNum = 1;
            Dictionary<string, string> cssAndClassNames = new Dictionary<string, string>();
            string styleBlock = string.Empty;

            int lessThan = source.IndexOf('<');
            int greaterThan = -1 == lessThan
                ? -1
                : source.IndexOfNextUnescapedGreaterThan(lessThan);

            while (lessThan != -1 && greaterThan != -1)
            {
                string tagOriginal = source.Substring(lessThan, greaterThan - lessThan + 1);
                string tag = tagOriginal;

                bool replace = false;

                string[] attributesToRemove = { "class", "data-bind", "style" };    // Style must be last, idiot, since you're inserting classes.
                foreach (string attributeName in attributesToRemove)
                {
                    string attribute = tag.RetrieveAttribute(attributeName, true);
                    while (attribute != null)
                    {
                        replace = true;
                        string replaceVal = string.Empty;

                        if (
                                operation.Equals(INLINE_STYLE_OPERATIONS.CONSOLIDATE_STYLES_AND_REMOVE_COLORS)
                                && attributeName.Equals("style")
                        ) {
                            string inAttributeDelimiter = attribute[attribute.IndexOfAny(_EitherQuote)].Equals('"')
                                ? "'"
                                : "\"";

                            // Note that we have to clean any instances of &quot; or, strangely,
                            // &amp;quot;, out of inline styles that are / double-quoted and replace with
                            // single quotes. (Clipboards tend to return stuff like:
                            //
                            // style="color: rgb(69, 69, 69); font-family: &quot;Segoe UI&quot;;"
                            // or
                            // font-family: &amp;quot;Source Sans Pro&amp;quot;,&amp;quot;Helvetica Neue&amp;quot;
                            // for some reason.)
                            var attrStripped = Regex.Replace(attribute, _PatternStripColorStyle, " ")
                                .Replace("&quot;", inAttributeDelimiter).Replace("&amp;quot;", inAttributeDelimiter)
                                .UnescapeAnyEscapedQuotesInStyle()
                                .Substring(attribute.IndexOfAny(_EitherQuote))
                                .Trim(_EitherQuote);

                            string className = null;

                            if (!cssAndClassNames.TryGetValue(attrStripped, out className))
                            {
                                className = $"q{classNum++}_{startTicks}";
                                cssAndClassNames.Add(attrStripped, className);
                            }

                            replaceVal = $@"class=""{className}""";
                        }

                        tag = tag.Replace(attribute, replaceVal);
                        attribute = tag.RetrieveAttribute(attributeName, true);
                    }
                }

                if (replace)
                {
                    source = source.ReplaceFirst(tagOriginal, tag);
                }

                lessThan = source.IndexOf('<', lessThan + 1);
                if (-1 != lessThan)
                {
                    greaterThan = source.IndexOfNextUnescapedGreaterThan(lessThan);
                }
            }

            if (operation.Equals(INLINE_STYLE_OPERATIONS.CONSOLIDATE_STYLES_AND_REMOVE_COLORS) && cssAndClassNames.Any())
            {
                styleBlock = "<!-- This style block is linked to the html paste below it -->\r<style>\r";
                foreach (var kvp in cssAndClassNames)
                {
                    styleBlock += $"    .{kvp.Value} {{{kvp.Key}}}\r";
                }
                styleBlock += "</style>\r";
            }

            return new Tuple<string, string>(source, styleBlock);
        }


        public static Tuple<string, string> OperateOnInlineStyles(
             this string str,
             INLINE_STYLE_OPERATIONS operation = INLINE_STYLE_OPERATIONS.REMOVE_STYLES_AND_CLASSES
        )
        {
            Tuple<string, string> ret = null;

            switch (operation)
            {
                case INLINE_STYLE_OPERATIONS.NONE:
                    // No changes.
                    ret = new Tuple<string, string>(string.Empty, string.Empty);
                    break;

                case INLINE_STYLE_OPERATIONS.CONSOLIDATE_STYLES_AND_REMOVE_COLORS:
                case INLINE_STYLE_OPERATIONS.REMOVE_STYLES_AND_CLASSES:
                    ret = _HandleAttributes(str, operation);
                    break;

                default:
                    throw new Exception("Inline style operation not supported: " + operation.ToString());
            }

            return ret;
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
