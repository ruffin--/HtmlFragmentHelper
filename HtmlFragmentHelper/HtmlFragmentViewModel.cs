// =========================== LICENSE ===============================
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.
// ======================== EO LICENSE ===============================

using System;

namespace HtmlFragmentHelper
{
    public class HtmlFragmentViewModel
    {
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

        public string Error = "";
        public bool HasErrors
        {
            get
            {
                return !string.IsNullOrEmpty(this.Error);
            }
        }

        public string FragmentSourceParsed
        {
            get
            {
                return _parseHtmlClipboardFragment(this.FragmentSourceRaw);
            }
        }

        private string _parseHtmlClipboardFragment(string rawFragmentSource)
        {
            string ret = rawFragmentSource;
            string delimiterStartAfter = "<!--StartFragment-->";
            string delimiterEndBefore = "<!--EndFragment-->";

            if (-1 < ret.IndexOf(delimiterStartAfter))
            {
                ret = ret.Substring(ret.IndexOf(delimiterStartAfter) + delimiterStartAfter.Length);
                if (-1 < ret.IndexOf(delimiterEndBefore))
                {
                    ret = ret.Substring(0, ret.IndexOf(delimiterEndBefore));
                }
                else
                {
                    ret = string.Empty;  // No luck, Ending not after Start; go back to nothing.
                }
            }

            return ret;
        }

        public HtmlFragmentViewModel() { }

        public HtmlFragmentViewModel(string rawClipboard)
        {
            try
            {
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
            pass = pass & vm.FragmentSourceParsed.Equals(@"<span style=""color: rgb(69, 69, 69); font-family: &quot;Segoe UI&quot;, &quot;Lucida Grande&quot;, Verdana, Arial, Helvetica, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: normal; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none;"">The official name of the clipboard (the string used by RegisterClipboardFormat) is HTML Format.</span>");
            pass = pass & vm.SourceUrlDomainSecondAndTopLevelsOnly.Equals("microsoft.com");

            return pass;
        }
    }
}
