// =========================== LICENSE ===============================
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.
// ======================== EO LICENSE ===============================

using System;

namespace HtmlFragmentHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            // Quick parsing check with built-in SanityCheck method.
            Console.WriteLine(HtmlFragmentViewModel.SanityCheck()
                ? "Sanity Check Success"
                : "Sanity Check Failure");
            Console.WriteLine();
            Console.WriteLine();

            //======================================================
            #region A more real world use case
            //======================================================

            string edgeFormatFragment = @"Version:1.0
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


            string strIntroLink = string.Empty;
            HtmlFragmentViewModel vm = new HtmlFragmentViewModel(edgeFormatFragment);

            if (vm.SourceUrl.Length > 0 && vm.SourceUrlDomainSecondAndTopLevelsOnly.Length > 0)
            {
                strIntroLink = string.Format("From <a href=\"{0}\">{1}</a>:" + Environment.NewLine + Environment.NewLine,
                    vm.SourceUrl, vm.SourceUrlDomainSecondAndTopLevelsOnly);
            }

            Console.WriteLine(strIntroLink + vm.FragmentSourceParsed);
            //======================================================
            #endregion A more real world use case
            //======================================================

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("======================================================");
            Console.WriteLine("Test finished. Press return to end.");
            Console.ReadLine();
        }
    }
}
