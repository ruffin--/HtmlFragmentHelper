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
    public enum INLINE_STYLE_OPERATIONS
    {
        NONE = 0,
        NOT_YET_IMPLEMENTED__CONSOLIDATE_STYLES_AND_REMOVE_COLORS = 1,
        REMOVE_STYLES_AND_CLASSES = 2
    }

    #region Constructor
    public class HtmlFragmentViewModel
    {
        // sorry, finally gave in and move the constructor to the front. I don't like
        // having properties later, but this is where I always want to start.

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
        /// <param name="operationType"
        /// <param name="consolidateMultilineHtmlTags">Multiline html tags are often valid, but may make further manipulation of the
        /// clipboard text difficult or unwieldy (eg, putting into a Markdown blockquote format). So that inserts at the start
        /// and end of lines doesn't break html, this can be set to `true` (default is `false`) to unwrap multiline html.</param>
        public HtmlFragmentViewModel(string rawClipboard,
            bool consolidateMutlilineHtmlTags = true,
            INLINE_STYLE_OPERATIONS operationType = INLINE_STYLE_OPERATIONS.REMOVE_STYLES_AND_CLASSES)
        {
            try
            {
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

                this.HtmlSource = sbClippedSource.ToString().TrimEnd('\r', '\n');
                if (consolidateMutlilineHtmlTags)
                {
                    this.HtmlSource = this.HtmlSource.ConslidateMultilineHtmlTags();
                }

                var htmlAndClasses = this.HtmlSource.OperateOnInlineStyles(operationType);

                this.HtmlSource = htmlAndClasses.Item1;
                if (null != htmlAndClasses.Item2)
                {
                    this.HtmlSource = this.HtmlSource.Replace("<!--StartFragment-->",
                        "<!--StartFragment-->" + Environment.NewLine + htmlAndClasses.Item2);
                }
            }
            catch (Exception e)
            {
                this._fragmentSourceRaw = rawClipboard;
                this.Error += e.Message + "\n";
            }
        }
        #endregion Constructor



        private string _fragmentSourceRaw = "";

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
                string ret = this.HtmlSource;
                string delimiterStartAfter = "<!--StartFragment-->";
                string delimiterEndBefore = "<!--EndFragment-->";

                return null == this.HtmlSource
                    ? string.Empty
                    : this.HtmlSource.ExtractBetween(delimiterStartAfter, delimiterEndBefore) ?? string.Empty;
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

    }
}
