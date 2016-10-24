##HtmlFragmentHelper

HtmlFragmentHelper makes handling [Microsoft HTML clipboard format](https://msdn.microsoft.com/en-us/library/windows/desktop/ms649015(v=vs.85).aspx) easy in your .NET applications. Written in C#, it parses HTML clipboard metadata in the clipboard and separates it from the HTML source, and further parses the precise source that belongs to the selected html fragment in the clipboard. Each property is then easily accessible from a strongly typed view model, rather than losing them in a blob of text.

`HtmlFragmentViewModel` also includes a number of convenience methods and properties to make dealing with this data easy. For now, the details are in the source.

The engine of HtmlFragmentHelper lives inside a single file, `HtmlFragmentViewModel.cs`. To create the view model, you send `HtmlFragmentViewModel`'s constructor the contents of a Microsoft formatted HTML Fragment as a raw string.

###Example usage

    public string CreateQuote(string htmlFragmentSource) 
    {
        string strIntroLink = string.Empty;
        HtmlFragmentViewModel vm = new HtmlFragmentViewModel(htmlFragmentSource);
    
        if (vm.SourceUrl.Length > 0 && vm.SourceUrlDomainSecondAndTopLevelsOnly.Length > 0)
        {
            strIntroLink = string.Format("From <a href=\"{0}\">{1}</a>:" + Environment.NewLine + Environment.NewLine,
                vm.SourceUrl, vm.SourceUrlDomainSecondAndTopLevelsOnly);
        }
    
        return strIntroLink + vm.FragmentSourceParsed;
    }

To produce the source for `htmlFragmentSource`, you'd likely use async code similar to the following:

    string src = string.Empty;
    DataPackageView dataPackageView = Clipboard.GetContent();
    
    if (dataPackageView.Contains(StandardDataFormats.Html))
    {
        src = await Clipboard.GetContent().GetHtmlFormatAsync();
    }
    else if (dataPackageView.Contains(StandardDataFormats.Text))
    {
        src = await Clipboard.GetContent().GetTextAsync();
    }
    // Else it's a clipboard format we're not handling.
    
    string blockquotedHtml = CreateQuote(src);
    
The resulting `blockquotedHtml` might look like this:

    From <a href="http://daringfireball.net/2016/10/mossberg_siri">daringfireball.net</a>:
    
    <P>Mossberg:</P><BLOCKQUOTE><P>For instance, when I asked Siri on my Mac how long it would take me to get to work, it said it didn't have my work address - even though the "me" contact card contains a work address and the same synced contact card on my iPhone allowed Siri to give me an answer.</P><P>Similarly, on my iPad, when I asked what my next appointment was, it said "Sorry, Walt, something's wrong" - repeatedly, with slightly different wording, in multiple places on multiple days. But, using the same Apple calendar and data, Siri answered correctly on the iPhone.</P></BLOCKQUOTE><P>These sort of glaring inconsistencies are almost as bad as universal failures. The big problem Apple faces with Siri is that when people encounter these problems, <EM>they stop trying</EM>.</P>
    
###[LICENSE](http://mozilla.org/MPL/2.0/)

    // =========================== LICENSE ===============================
    // This Source Code Form is subject to the terms of the Mozilla Public
    // License, v. 2.0. If a copy of the MPL was not distributed with this
    // file, You can obtain one at http://mozilla.org/MPL/2.0/.
    // ======================== EO LICENSE ===============================