// =========================== LICENSE ===============================
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.
// ======================== EO LICENSE ===============================

using System;
using System.Text;
using System.Text.RegularExpressions;

namespace HtmlFragmentHelper
{
    public class HtmlFragmentViewModel
    {
        // These are ugly globals so that we can use the `ClippedSource` property more easily.
        // TODO: Though now that I'm thinking about it, I don't think we ever allow for a second
        // parsing pass, so we should do this once and forget about it.
        public bool doStripColorStyleFromInlineHtml = true;
        public bool doUnwrapMultilineHtmlTags = false;
        private string _fragmentSourceRaw = "";

        // TODO: This is going to zap a little too much, as we'll lose
        // image, repeat, attachment, and/or position if they're defined
        // in the `background` "shortcut".
        // https://www.w3.org/TR/CSS2/colors.html#background-properties
        // (That is, I'm now removing most anything in not just `background-color`,
        // but also `background` by itself)
        private string _patternStripColorStyle = @"(background|(background-|border-)*color):[a-zA-Z0-9(), ]+;";

        public string Version = "";
        public int StartHtml = int.MinValue;
        public int EndHtml = int.MinValue;
        public int StartFragment = int.MinValue;
        public int EndFragment = int.MinValue;
        public string SourceUrl = "";
        public string SourceUrlDomain
        {
            get
            {
                string ret = this.SourceUrl;
                if (ret.StartsWith("http") && ret.Contains("://"))
                {
                    int firstCutLoc = ret.IndexOf("://");
                    ret = ret.Substring(firstCutLoc + 3);
                    ret = ret.Substring(0, ret.IndexOf("/"));
                }
                else if (this.SourceUrl.Trim('/').Contains("/"))
                {
                    ret = ret.Trim('/');
                    ret = ret.Substring(0, ret.IndexOf("/"));
                }
                // else we kind of lose out. What kinda URL is this, anyway? Return it all.

                return ret;
            }
        }
        public string SourceUrlDomainSecondAndTopLevelsOnly
        {
            get
            {
                string ret = string.Empty;
                string[] aDomainParts = this.SourceUrlDomain.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

                if (aDomainParts.Length >= 2)
                {
                    ret = aDomainParts[aDomainParts.Length - 2] + "." + aDomainParts[aDomainParts.Length - 1];
                }
                else
                {
                    ret = this.SourceUrlDomain;
                }

                return ret;
            }
        }

        public string HtmlSource = "";

        public string ClippedSource
        {
            get
            {
                return _parseHtmlClipboardFragment(this.HtmlSource);
            }
        }
        public string PageTitle
        {
            get
            {
                return this.HtmlSource.ExtractBetween("<title*>", "</title>") ?? this.SourceUrlDomainSecondAndTopLevelsOnly;
            }
        }

        #region Construction Error Handling
        /// <summary>
        /// Will hold the Message from any Exceptions thrown during constructor execution.
        /// </summary>
        public string Error = "";
        /// <summary>
        /// Convenience boolean to see if `Error` IsNullOrEmpty or not.
        /// </summary>
        public bool HasErrors
        {
            get
            {
                return !string.IsNullOrEmpty(this.Error);
            }
        }
        #endregion Construction Error Handling

        private string _parseHtmlClipboardFragment(string clippedSource)
        {
            string ret = clippedSource;
            string delimiterStartAfter = "<!--StartFragment-->";
            string delimiterEndBefore = "<!--EndFragment-->";

            ret = clippedSource.ExtractBetween(delimiterStartAfter, delimiterEndBefore) ?? string.Empty;

            if (this.doStripColorStyleFromInlineHtml)
            {
                ret = _parseColorStyleFromHtml(ret);
            }

            return ret;
        }

        private string _parseColorStyleFromHtml(string src)
        {
            string ret = string.Empty;
            bool hasLeadingLt = false;

            if (!string.IsNullOrEmpty(src))
            {
                hasLeadingLt = src[0].Equals('<');
                string[] astrTags = src.Split(new[] { '<' }, StringSplitOptions.RemoveEmptyEntries);
                StringBuilder stringBuilder = new StringBuilder();

                if (astrTags.Length > 0)
                {
                    foreach (string tag in astrTags)
                    {
                        if (tag.IndexOf('>') > -1)
                        {
                            string[] astrHalfs = tag.Split(new[] { '>' }, 2);
                            astrHalfs[0] = Regex.Replace(astrHalfs[0], _patternStripColorStyle, " ");
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
                    ret = src;
                }
            }

            return hasLeadingLt ? ret : ret.TrimStart('<');
        }

        //public HtmlFragmentViewModel() { }

        /// <summary>
        /// This HtmlFragmentViewModel constructor takes in a string and, if it
        /// is in Microsoft HTML clipboard format, will convert it to a
        /// HtmlFragmentViewModel.
        /// </summary>
        /// <param name="rawClipboard">The Microsoft HTML clipboard formatted string to parse into a view model.</param>
        /// <param name="stripColorFromHtmlSource">Keeping original inline color style in clipboards from Chrome
        /// (in particular) can sometimes cause unexpected and difficult to read results. Setting `stripColorFromHhtmlSource`
        /// to `true` (which is the default) will remove color, background-color, and border-color from inline style values.
        /// `false` will keep all inline style unedited.</param>
        /// <param name="unwrapHtmlTags">Multiline html tags are often valid, but may make further manipulation of the
        /// clipboard text difficult or unwieldy (eg, putting into a Markdown blockquote format). So that inserts at the start
        /// and end of lines doesn't break html, this can be set to `true` (default is `false`) to unwrap multiline html.</param>
        public HtmlFragmentViewModel(string rawClipboard,
            bool stripColorFromHtmlSource = true,
            bool unwrapHtmlTags = false)
        {
            try
            {
                this.doStripColorStyleFromInlineHtml = stripColorFromHtmlSource;
                this.doUnwrapMultilineHtmlTags = unwrapHtmlTags;

                this._fragmentSourceRaw = rawClipboard;
                string[] aLines = rawClipboard.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);   // I think it's okay to remove empties semantically, but aesthetically, maybe not best to remove all the ?

                int i = 0;
                bool headerOver = false;
                while (i < aLines.Length && !headerOver)
                {
                    string line = aLines[i];
                    int colLoc = line.IndexOf(":");

                    if (colLoc > 5 && line.Length > colLoc)
                    {
                        int intParseDummy = int.MinValue;
                        string value = line.Split(new[] { ':' }, 2)[1];

                        switch (line.Substring(0, 6).ToLower())
                        {
                            case "versio":
                                this.Version = value;
                                break;

                            case "starth":
                                if (int.TryParse(value, out intParseDummy))
                                    this.StartHtml = intParseDummy;
                                break;

                            case "endhtm":
                                if (int.TryParse(value, out intParseDummy))
                                    this.EndHtml = intParseDummy;
                                break;

                            case "startf":
                                if (int.TryParse(value, out intParseDummy))
                                    this.StartFragment = intParseDummy;
                                break;

                            case "endfra":
                                if (int.TryParse(value, out intParseDummy))
                                    this.EndFragment = intParseDummy;
                                break;

                            case "source":
                                this.SourceUrl = value;
                                break;

                            default:
                                // If this is a header value we don't know about (say Version is > 1.0),
                                // just skip it. Otherwise pretend we're in the HTML.
                                // TODO: Seems fragile, even with duck typing by looking for <!--StartFragment-->.
                                if (!Regex.IsMatch(line, @"^[A-Za-z]+:") || line.IndexOf("<!--StartFragment-->") > -1)
                                {
                                    headerOver = true;
                                    i--;    // We'll need to back up one to process this line as html source since we're incrementing, below.
                                }
                                break;
                        }
                        i++;
                    }
                    else
                    {
                        headerOver = true;
                    }
                }   // while i < aLines.Length & !headerOver

                StringBuilder sbClippedSource = new StringBuilder();    // MICRO OPTIMIZATION THEATER!!!
                while (i < aLines.Length)
                {
                    sbClippedSource.Append(aLines[i]).Append(System.Environment.NewLine);   // Side effect: Standardizes on environment newline.
                    i++;
                }

                this.HtmlSource = sbClippedSource.ToString().TrimEnd('\r','\n');
                if (unwrapHtmlTags)
                {
                    this.HtmlSource = this.HtmlSource.UnwrapHtmlTags();
                }
            }
            catch (Exception e)
            {
                this._fragmentSourceRaw = rawClipboard;
                this.Error += e.Message + "\n";
            }
        }
    }

    public static class HelperExtensions
    {
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
    }
}
