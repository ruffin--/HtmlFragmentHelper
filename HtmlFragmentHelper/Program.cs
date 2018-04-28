// =========================== LICENSE ===============================
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.
// ======================== EO LICENSE ===============================

using System;
using System.IO;

namespace HtmlFragmentHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(100, 40);

            string response = "";

            while (!response.Equals("."))
            {
                try
                {


                    //HtmlFragmentViewModel vmChromeStripColor1 = new HtmlFragmentViewModel(Program.ChromeFragment);
                    //File.WriteAllText(@"C:\temp\withStrip.html", vmChromeStripColor1.ClippedSource.Replace("<", "\n<"));

                    //HtmlFragmentViewModel vmChromeNoStrip = new HtmlFragmentViewModel(Program.ChromeFragment, false);
                    //File.WriteAllText(@"C:\temp\withoutStrip.html", vmChromeNoStrip.ClippedSource.Replace("<", "\n<"));

                    //Program.TestSanity();
                    //Program.TestTitle();
                    //Program.TestClassNormalization();

                    string tag = @"<a href=""http://www.google.com"" class="""" style="""">";
                    //Console.WriteLine(tag.RetrieveAttribute("class", true));
                    //Console.WriteLine(tag.RetrieveAttribute("style", true));
                    //Console.WriteLine(tag.RetrieveAttribute("class", false));
                    //Console.WriteLine(tag.RetrieveAttribute("style", false));

                    //tag = @"<span class=""topic"" style=""box-sizing: border-box; margin: 0px; padding: 0px; overflow: hidden; height: 40px; width: auto; position: absolute; top: 12px; right: 70px; max-width: 100px;"">";
                    //Console.WriteLine(tag.RetrieveAttribute("class", true));
                    //Console.WriteLine(tag.RetrieveAttribute("style", true));

                    //tag = @"<span id=""title-99902227"" class=""story-title"" style=""box-sizing: border-box; margin: 0px; padding: 0px; left: 15px; position: relative; color: white;"">";
                    //Console.WriteLine(tag.RetrieveAttribute("class", true));
                    //Console.WriteLine(tag.RetrieveAttribute("style", true));


                    Program.TestBadSlashdotKnockout();

                    //======================================================
                    #region A more real world use case
                    //======================================================
                    //HtmlFragmentViewModel vmEdge = new HtmlFragmentViewModel(EdgeFragment);
                    //string strIntroLink = string.Empty;

                    //if (vmEdge.SourceUrl.Length > 0 && vmEdge.SourceUrlDomainSecondAndTopLevelsOnly.Length > 0)
                    //{
                    //    strIntroLink = string.Format("From <a href=\"{0}\">{1}</a>:" + Environment.NewLine + Environment.NewLine,
                    //        vmEdge.SourceUrl, vmEdge.SourceUrlDomainSecondAndTopLevelsOnly);
                    //}

                    //Console.WriteLine(strIntroLink + vm.HtmlSource);
                    //======================================================
                    #endregion A more real world use case
                    //======================================================

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.ToString());
                }

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("======================================================");
                Console.WriteLine("Test finished. Period on line by itself to end.");
                Console.WriteLine("======================================================");
                response = Console.ReadLine();
            }
        }

        public static void TestBadSlashdotKnockout()
        {
            string stringToUse = Values.SlashdotMoreFail;
            HtmlFragmentViewModel vm = new HtmlFragmentViewModel(stringToUse, true, INLINE_STYLE_OPERATIONS.CONSOLIDATE_STYLES_AND_REMOVE_COLORS);

            string stamp = DateTime.Now.ToString("yyyy-MM-dd_HHmmss.fff");
            File.WriteAllText($@"C:\temp\html\htmlFrag_{stamp}.html", vm.StyleBlock + Environment.NewLine + vm.ClippedSource);
            Console.WriteLine(vm.HtmlSource);
        }

        public static void TestClassNormalization()
        {
            string stringToUse = Values.QuoteFail;
            HtmlFragmentViewModel vm = new HtmlFragmentViewModel(stringToUse, true);

            Console.WriteLine(vm.HtmlSource);
        }

        // Very basic unit test. You get the idea. Test away.
        public static void TestSanity()
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

            HtmlFragmentViewModel vm = new HtmlFragmentViewModel(testSource, false);
            pass = pass & vm.Version.Equals("0.9");
            pass = pass && vm.StartHtml.Equals(196);
            pass = pass && vm.EndHtml.Equals(845);
            pass = pass && vm.StartFragment.Equals(232);
            pass = pass && vm.EndFragment.Equals(809);
            pass = pass && vm.SourceUrl.Equals("https://msdn.microsoft.com/en-us/library/windows/desktop/ms649015(v=vs.85).aspx");

            // Note that if we had blank lines in here, they'd be removed on construction as currently written (20161029).
            pass = pass && vm.ClippedSource.Equals(@"<span style=""color: rgb(69, 69, 69); font-family: 'Segoe UI', 'Lucida Grande', Verdana, Arial, Helvetica, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: normal; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none;"">The official name of the clipboard (the string used by RegisterClipboardFormat) is HTML Format.</span>");

            string expectedHtml = @"<html>
<body>
<!--StartFragment--><span style=""color: rgb(69, 69, 69); font-family: 'Segoe UI', 'Lucida Grande', Verdana, Arial, Helvetica, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: normal; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none;"">The official name of the clipboard (the string used by RegisterClipboardFormat) is HTML Format.</span><!--EndFragment-->
</body>
</html>";
            expectedHtml.NLWriteLine();
            vm.HtmlSource.NLWriteLine();
            pass = pass && vm.HtmlSource.Equals(expectedHtml);

            pass = pass & vm.SourceUrlDomainSecondAndTopLevelsOnly.Equals("microsoft.com");

            if (!pass) throw new Exception("TestSanity check test did not pass successfully.");
        }

        public static void TestTitle()
        {
            bool pass = true;
            HtmlFragmentViewModel vmEdge = new HtmlFragmentViewModel(EdgeFragment);
            HtmlFragmentViewModel vmChrome = new HtmlFragmentViewModel(ChromeFragment);

            pass = pass && vmEdge.PageTitle.Equals("Daring Fireball: Walt Mossberg: ‘Why Does Siri Seem So Dumb?’");
            pass = pass && vmChrome.PageTitle.Equals("daringfireball.net");

            if (!pass) throw new Exception("TestTitle check did not pass successfully.");
        }


        public static string EdgeFragment = @"Version:1.0
StartHTML:000000210
EndHTML:000003550
StartFragment:000002696
EndFragment:000003500
StartSelection:000002696
EndSelection:000003496
SourceURL:http://daringfireball.net/2016/10/mossberg_siri
<!DOCTYPE HTML>
<HTML lang=""en""><HEAD>       <!-- Open Graph [jive] --> <!--
    <meta property=""og:site_name""   content=""Daring Fireball"" />
    <meta property=""og:title""       content=""Walt Mossberg: ‘Why Does Siri Seem So Dumb?’"" />
    <meta property=""og:url""         content=""http://daringfireball.net/2016/10/mossberg_siri"" />
    <meta property=""og:description"" content=""In addition to the engineering hurdles to actually make Siri much better, Apple also has to overcome a “boy who cried wolf” credibility problem."" />
    <meta property=""og:image""       content=""https://daringfireball.net/graphics/df-square-192"" />
    <meta property=""og:type""        content=""article"" />
 -->         <!-- Twitter Card [jive] -->                    <TITLE>Daring Fireball: Walt Mossberg: ‘Why Does Siri Seem So Dumb?’</TITLE>        <LINK href=""/graphics/apple-touch-icon.png"" rel=""apple-touch-icon-precomposed"">     <LINK href=""/graphics/favicon.ico?v=005"" rel=""shortcut icon"">   <LINK href=""/graphics/dfstar.svg"" rel=""mask-icon"" color=""#4a525a"">  <LINK href=""/css/fireball_screen.css?v1.7"" rel=""stylesheet"" type=""text/css"" media=""screen"">     <LINK href=""/css/ie_sucks.php"" rel=""stylesheet"" type=""text/css"" media=""screen"">     <LINK href=""/css/fireball_print.css?v01"" rel=""stylesheet"" type=""text/css"" media=""print"">    <LINK href=""/feeds/main"" rel=""alternate"" type=""application/atom+xml"">
<SCRIPT src=""/mint/?js"" type=""text/javascript"" async=""""></SCRIPT>

<SCRIPT src=""http://www.google-analytics.com/ga.js"" type=""text/javascript"" async=""""></SCRIPT>

<SCRIPT src=""/js/js-global/FancyZoom.js"" type=""text/javascript""></SCRIPT>

<SCRIPT src=""/js/js-global/FancyZoomHTML.js"" type=""text/javascript""></SCRIPT>
     <LINK title=""Home"" href=""/"" rel=""home"">     <LINK href=""http://df4.us/pfz"" rel=""shorturl"">  <LINK title=""Apple Responds to Dash Controversy"" href=""http://daringfireball.net/2016/10/apple_dash_controversy"" rel=""prev"">
<SCRIPT src=""http://daringfireball.net/mint/?record&amp;key=383950464d37374b39333637695970466458724e6779513431&amp;referer=&amp;resource=http%3A//daringfireball.net/2016/10/mossberg_siri&amp;resource_title=Daring%20Fireball%3A%20Walt%20Mossberg%3A%20%u2018Why%20Does%20Siri%20Seem%20So%20Dumb%3F%u2019&amp;resource_title_encoded=0&amp;window_width=1756&amp;window_height=921&amp;resolution=2438x1371&amp;flash_version=0&amp;1476397798179&amp;serve_js"" type=""text/javascript""></SCRIPT>
</HEAD><BODY onload=""setupZoom()""><DIV id=""Box""><DIV id=""Main""><DIV class=""article""><!--StartFragment--><P>Mossberg:</P><BLOCKQUOTE><P>For instance, when I asked Siri on my Mac how long it would take me to get to work, it said it didn’t have my work address — even though the “me” contact card contains a work address and the same synced contact card on my iPhone allowed Siri to give me an answer.</P><P>Similarly, on my iPad, when I asked what my next appointment was, it said “Sorry, Walt, something’s wrong” — repeatedly, with slightly different wording, in multiple places on multiple days. But, using the same Apple calendar and data, Siri answered correctly on the iPhone.</P></BLOCKQUOTE><P>These sort of glaring inconsistencies are almost as bad as universal failures. The big problem Apple faces with Siri is that when people encounter these problems, <EM>they stop trying</EM>.</P><!--EndFragment--></DIV></DIV></DIV></BODY></HTML>";

        public static string ChromeFragment = @"Version:0.9
StartHTML:0000000164
EndHTML:0000002719
StartFragment:0000000200
EndFragment:0000002683
SourceURL:http://daringfireball.net/2016/10/mossberg_siri
<html>
<body>
<!--StartFragment--><p style=""margin: 0px 0px 1.6em; padding: 0px; color: rgb(238, 238, 238); font-family: Verdana, &quot;Bitstream Vera Sans&quot;, sans-serif; font-size: 11px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: normal; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(74, 82, 90);"">Mossberg:</p><blockquote style=""font-size: 11px; margin: 2em 2em 2em 1em; padding: 0px 0.75em 0px 1.25em; border-left: 1px solid rgb(119, 119, 119); border-right: 0px solid rgb(119, 119, 119); outline: 0px; vertical-align: baseline; background: rgb(74, 82, 90); color: rgb(238, 238, 238); font-family: Verdana, &quot;Bitstream Vera Sans&quot;, sans-serif; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: normal; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-t
ransform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px;""><p style=""margin: 0px 0px 1.6em; padding: 0px;"">For instance, when I asked Siri on my Mac how long it would take me to get to work, it said it didn’t have my work address — even though the “me” contact card contains a work address and the same synced contact card on my iPhone allowed Siri to give me an answer.</p><p style=""margin: 0px 0px 1.6em; padding: 0px;"">Similarly, on my iPad, when I asked what my next appointment was, it said “Sorry, Walt, something’s wrong” — repeatedly, with slightly different wording, in multiple places on multiple days. But, using the same Apple calendar and data, Siri answered correctly on the iPhone.</p></blockquote><p style=""margin: 0px 0px 1.6em; padding: 0px; color: rgb(238, 238, 238); font-family: Verdana, &quot;Bitstream Vera Sans&quot;, sans-serif; font-size: 11px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: normal; letter-spacin
g: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(74, 82, 90);"">These sort of glaring inconsistencies are almost as bad as universal failures. The big problem Apple faces with Siri is that when people encounter these problems,<span class=""Apple-converted-space""> </span><em>they stop trying</em>.</p><!--EndFragment-->
</body>
</html>";

        public static string FirefoxFragment = @"Version:0.9
StartHTML:00000156
EndHTML:00001060
StartFragment:00000190
EndFragment:00001024
SourceURL:http://daringfireball.net/2016/10/mossberg_siri
<html><body>
<!--StartFragment--><p>Mossberg:</p>

<blockquote>
  <p>For instance, when I asked Siri on my Mac how long it would take
me to get to work, it said it didn’t have my work address —
even though the “me” contact card contains a work address and
the same synced contact card on my iPhone allowed Siri to give
me an answer.</p>

<p>Similarly, on my iPad, when I asked what my next appointment was,
it said “Sorry, Walt, something’s wrong” — repeatedly, with
slightly different wording, in multiple places on multiple days.
But, using the same Apple calendar and data, Siri answered
correctly on the iPhone.</p>
</blockquote>

<p>These sort of glaring inconsistencies are almost as bad as universal 
failures. The big problem Apple faces with Siri is that when people 
encounter these problems, <em>they stop trying</em>.</p><!--EndFragment-->
</body>
</html>";

        public static string LibreCalcFragment = @"Version:1.0
StartHTML:0000000170
EndHTML:0000002466
StartFragment:0000000787
EndFragment:0000002447
<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">

<html>
<head>

    <meta http-equiv=""content-type"" content=""text/html; charset=utf-8""/>
    <title></title>
    <meta name=""generator"" content=""LibreOffice 5.1.4.2 (Windows)""/>
    <style type=""text/css"">
        body,div,table,thead,tbody,tfoot,tr,th,td,p { font-family:""Liberation Sans""; font-size:x-small }
        a.comment-indicator:hover + comment { background:#ffd; position:absolute; display:block; border:1px solid black; padding:0.5em;  }
        a.comment-indicator { background:red; display:inline-block; border:1px solid black; width:0.5em; height:0.5em;  }
        comment { display:none;  }
    </style>

</head>

<body>
<table cellspacing=""0"" border=""0"">
    <colgroup width=""26""></colgroup>
    <colgroup width=""34""></colgroup>
    <colgroup span=""2"" width=""41""></colgroup>
    <colgroup width=""48""></colgroup>
    <tr>
        <td height=""17"" align=""right"" sdval=""1"" sdnum=""1033;"">1</td>
        <td align=""right"" sdval=""2"" sdnum=""1033;"">2</td>
        <td align=""right"" sdval=""3"" sdnum=""1033;"">3</td>
        <td align=""right"" sdval=""4"" sdnum=""1033;"">4</td>
        <td align=""right"" sdval=""5"" sdnum=""1033;"">5</td>
    </tr>
    <tr>
        <td height=""17"" align=""right"" sdval=""2"" sdnum=""1033;"">2</td>
        <td align=""right"" sdval=""4"" sdnum=""1033;"">4</td>
        <td align=""right"" sdval=""7"" sdnum=""1033;"">7</td>
        <td align=""right"" sdval=""11"" sdnum=""1033;"">11</td>
        <td align=""right"" sdval=""16"" sdnum=""1033;"">16</td>
    </tr>
    <tr>
        <td height=""17"" align=""right"" sdval=""3"" sdnum=""1033;"">3</td>
        <td align=""right"" sdval=""7"" sdnum=""1033;"">7</td>
        <td align=""right"" sdval=""14"" sdnum=""1033;"">14</td>
        <td align=""right"" sdval=""25"" sdnum=""1033;"">25</td>
        <td align=""right"" sdval=""41"" sdnum=""1033;"">41</td>
    </tr>
    <tr>
        <td height=""17"" align=""right"" sdval=""4"" sdnum=""1033;"">4</td>
        <td align=""right"" sdval=""11"" sdnum=""1033;"">11</td>
        <td align=""right"" sdval=""25"" sdnum=""1033;"">25</td>
        <td align=""right"" sdval=""50"" sdnum=""1033;"">50</td>
        <td align=""right"" sdval=""91"" sdnum=""1033;"">91</td>
    </tr>
    <tr>
        <td height=""17"" align=""right"" sdval=""5"" sdnum=""1033;"">5</td>
        <td align=""right"" sdval=""16"" sdnum=""1033;"">16</td>
        <td align=""right"" sdval=""41"" sdnum=""1033;"">41</td>
        <td align=""right"" sdval=""91"" sdnum=""1033;"">91</td>
        <td align=""right"" sdval=""182"" sdnum=""1033;"">182</td>
    </tr>
</table>
</body>

</html>

";
    }

    public static class TestingExtensions
    {
        public static void NLWriteLine(this string toWrite, bool bookend = true)
        {
            if (bookend)
            {
                toWrite = "#" + toWrite + "#";
            }

            Console.WriteLine(toWrite.Replace("\r", "\\r\r").Replace("\n", "\n\\n") + "\n\n");
        }
    }
}
