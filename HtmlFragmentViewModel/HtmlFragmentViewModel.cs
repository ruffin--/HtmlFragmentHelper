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
        public bool doStripColorStyleFromInlineHtml = true;
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

        public string FragmentSourceRaw = "";

        public string HtmlSource
        {
            get
            {
                return _parseHtmlClipboardFragment(this.FragmentSourceRaw);
            }
        }
        public string PageTitle
        {
            get
            {
                return this.FragmentSourceRaw.ExtractBetween("<title*>", "</title>");
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

        private string _parseHtmlClipboardFragment(string rawFragmentSource)
        {
            string ret = rawFragmentSource;
            string delimiterStartAfter = "<!--StartFragment-->";
            string delimiterEndBefore = "<!--EndFragment-->";

            ret = rawFragmentSource.ExtractBetween(delimiterStartAfter, delimiterEndBefore) ?? string.Empty;

            if (this.doStripColorStyleFromInlineHtml)
            {
                ret = _parseColorStyleFromHtml(ret);
            }

            return ret;
        }

        private string _parseColorStyleFromHtml(string src)
        {
            string ret = string.Empty;
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

            return ret;
        }

        public HtmlFragmentViewModel() { }

        public HtmlFragmentViewModel(string rawClipboard, bool stripColorFromHtmlSource = true)
        {
            try
            {
                this.doStripColorStyleFromInlineHtml = stripColorFromHtmlSource;

                //                                               Should always be lower-case, but just in case... Note that we force lower later.
                string[] headAndTail = rawClipboard.Split(new[] { "<html", "<HTML" }, 2, StringSplitOptions.RemoveEmptyEntries);

                if (headAndTail.Length < 2)
                {
                    this.FragmentSourceRaw = rawClipboard;
                }
                else
                {
                    this.FragmentSourceRaw = "<html" + headAndTail[1];  // TODO: Optionally normalize inline styles into classes to make this source more easily readable.

                    string[] aLines = headAndTail[0].Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string line in aLines)
                    {
                        if (line.Length > 5 && line.Contains(":"))
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
                                    // Should consider throwing an error here to for ill-formatted clipboard.
                                    break;

                            }
                        }
                        // else we should consider throwing an error for an ill-formatted HTML clipboard
                    }
                }
            }
            catch (Exception e)
            {
                this.FragmentSourceRaw = rawClipboard;
                this.Error = e.Message;
            }
        }

        // Very basic unit test. You get the idea. Test away.
        public static bool SanityCheck()
        {
            bool pass = true;
            string testSource = @"Version:0.9
StartHTML:0000000196
EndHTML:0000000845
StartFragment:0000000232
EndFragment:0000000809
SourceURL:https://msdn.microsoft.com/en-us/library/windows/desktop/ms649015(v=vs.85).aspx
<html>
<body>
<!--StartFragment--><span style=""color: rgb(69, 69, 69); font-family: &quot;Segoe UI&quot;, &quot;Lucida Grande&quot;, Verdana, Arial, Helvetica, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: normal; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none;"">The official name of the clipboard (the string used by RegisterClipboardFormat) is HTML Format.</span><!--EndFragment-->
</body>
</html>";

            HtmlFragmentViewModel vm = new HtmlFragmentViewModel(testSource);
            pass = pass & vm.Version.Equals("0.9");
            pass = pass && vm.StartHtml.Equals(196);
            pass = pass && vm.EndHtml.Equals(845);
            pass = pass && vm.StartFragment.Equals(232);
            pass = pass && vm.EndFragment.Equals(809);
            pass = pass && vm.SourceUrl.Equals("https://msdn.microsoft.com/en-us/library/windows/desktop/ms649015(v=vs.85).aspx");
            pass = pass & vm.HtmlSource.Equals(@"<span style=""color: rgb(69, 69, 69); font-family: &quot;Segoe UI&quot;, &quot;Lucida Grande&quot;, Verdana, Arial, Helvetica, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: normal; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none;"">The official name of the clipboard (the string used by RegisterClipboardFormat) is HTML Format.</span>");
            pass = pass & vm.SourceUrlDomainSecondAndTopLevelsOnly.Equals("microsoft.com");

            return pass;
        }
    }

    public static class HelperExtensions
    {
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
