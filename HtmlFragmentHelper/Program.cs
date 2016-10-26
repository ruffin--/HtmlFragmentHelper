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
            HtmlFragmentViewModel vm1 = new HtmlFragmentViewModel(Program.ChromeFragment);
            Console.WriteLine(vm1.HtmlSource);

            File.WriteAllText(@"C:\temp\withStrip.html", vm1.HtmlSource.Replace("<", "\n<"));


            HtmlFragmentViewModel vm2 = new HtmlFragmentViewModel(Program.ChromeFragment, false);
            Console.WriteLine(vm2.HtmlSource);

            File.WriteAllText(@"C:\temp\withoutStrip.html", vm2.HtmlSource.Replace("<", "\n<"));



            // Quick parsing check with built-in SanityCheck method.
            Console.WriteLine(HtmlFragmentViewModel.SanityCheck()
                ? "Sanity Check Success"
                : "Sanity Check Failure");
            Console.WriteLine();
            Console.WriteLine();

            //======================================================
            #region A more real world use case
            //======================================================

            string edgeFormatFragment = @"Version:0.9
StartHTML:0000000143
EndHTML:0000002756
StartFragment:0000000179
EndFragment:0000002720
SourceURL:http://daringfireball.net/
<html>
<body>
<!--StartFragment--><dt style=""font-family: &quot;Gill Sans MT&quot;, &quot;Gill Sans&quot;, &quot;Gill Sans Std&quot;, Verdana, &quot;Bitstream Vera Sans&quot;, sans-serif; font-size: 1.05em; text-align: left; font-weight: normal; margin: 2em 0px 1em; letter-spacing: 0.15em; text-transform: uppercase; color: rgb(238, 238, 238); font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; orphans: 2; text-indent: 0px; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(74, 82, 90);""><a href=""http://www.newyorker.com/business/currency/apple-samsung-and-good-design-inside-and-out"" style=""text-decoration: none; color: rgb(204, 204, 204); border-color: rgb(114, 118, 122); border-width: 0px 0px 1px; border-style: dotted; padding: 2px 0px 0px; background-color: inherit;"">OM MALIK: ‘GOOD DESIGN — INSIDE AND OUT’</a> <a class=""permalink"" title=""Permanent link to ‘Om Malik: ‘Good Design — Inside and Out’’"" href=""http://daringfireball.net/linked/2016
/10/24/om-new-yorker-design"" style=""text-decoration: none; color: rgb(96, 104, 112); border: 0px dotted rgb(114, 118, 122); padding: 3px 3px 3px 5px; background-color: inherit; margin-left: 0.5em; font-family: &quot;Hiragino Kaku Gothic Pro&quot;, Osaka, &quot;Zapf Dingbats&quot;;"">★</a></dt><dd style=""margin: auto auto 5em 0em; color: rgb(238, 238, 238); font-family: Verdana, &quot;Bitstream Vera Sans&quot;, sans-serif; font-size: 11px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: normal; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(74, 82, 90);""><p style=""margin: 0px 0px 1.6em; padding: 0px;"">Om Malik, writing last week for The New Yorker:</p><blockquote style=""font-size: 1em; margin: 0px 1em 0px 0.25em; padding: 0px 0.75em 0px 1em; border-left: 1px solid rgb(119, 119, 119); border-right: 0px solid rgb(119, 119
, 119); outline: 0px; vertical-align: baseline; background: transparent; color: rgb(221, 221, 221);""><p style=""margin: 0px 0px 1.6em; padding: 0px;"">When I asked John Maeda, the former president of the Rhode Island School of Design, why, then, people have turned on the design of the iPhone 7, he pointed out that perhaps these critics “seem to believe that there’s some as yet unimaginable transcendence that can happen in a small, palm-shaped, rectangular device.</p></blockquote></dd><!--EndFragment-->
</body>
</html>";


            string strIntroLink = string.Empty;
            HtmlFragmentViewModel vm = new HtmlFragmentViewModel(edgeFormatFragment);

            if (vm.SourceUrl.Length > 0 && vm.SourceUrlDomainSecondAndTopLevelsOnly.Length > 0)
            {
                strIntroLink = string.Format("From <a href=\"{0}\">{1}</a>:" + Environment.NewLine + Environment.NewLine,
                    vm.SourceUrl, vm.SourceUrlDomainSecondAndTopLevelsOnly);
            }

            Console.WriteLine(strIntroLink + vm.HtmlSource);
            //======================================================
            #endregion A more real world use case
            //======================================================

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("======================================================");
            Console.WriteLine("Test finished. Press return to end.");
            Console.ReadLine();
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
}
