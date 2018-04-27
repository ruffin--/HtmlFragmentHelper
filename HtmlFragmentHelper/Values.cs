using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlFragmentHelper
{
    public class Values
    {
        public static string ClassRepeatFromSO = @"Version:0.9StartHTML:0000000220EndHTML:0000012647StartFragment:0000000256EndFragment:0000012611SourceURL:https://stackoverflow.com/questions/784929/what-is-the-not-not-operator-in-javascript/29951409#29951409<html><body><!--StartFragment--><p style=""margin: 0px 0px 1em; padding: 0px; border: 0px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-variant-numeric: inherit; font-variant-east-asian: inherit; font-weight: 400; font-stretch: inherit; font-size: 15px; line-height: inherit; font-family: Arial, &quot;Helvetica Neue&quot;, Helvetica, sans-serif; vertical-align: baseline; box-sizing: inherit; clear: both; color: rgb(36, 39, 41); letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial;"">So many answers doing half the work. Yes,<span> </span><code style=""margin: 0px; padding: 1px 5px; border: 0px; font-style: inherit; font-variant: inherit; font-weight: inherit; font-stretch: inherit; font-size: 13px; line-height: inherit; font-family: Consolas, Menlo, Monaco, &quot;Lucida Console&quot;, &quot;Liberation Mono&quot;, &quot;DejaVu Sans Mono&quot;, &quot;Bitstream Vera Sans Mono&quot;, &quot;Courier New&quot;, monospace, sans-serif; vertical-align: baseline; box-sizing: inherit; background-color: rgb(239, 240, 241); white-space: pre-wrap;"">!!X</code><span> </span>could be read as ""the truthiness of X [represented as a boolean]"". But<span> </span><code style=""margin: 0px; padding: 1px 5px; border: 0px; font-style: inherit; font-variant: inherit; font-weight: inherit; font-stretch: inherit; font-size: 13px; line-height: inherit; font-family: Consolas, Menlo, Monaco, &quot;Lucida Console&quot;, &quot;Liberation Mono&quot;, &quot;DejaVu Sans Mono&quot;, &quot;Bitstream Vera Sans Mono&quot;, &quot;Courier New&quot;, monospace, sans-serif; vertical-align: baseline; box-sizing: inherit; background-color: rgb(239, 240, 241); white-space: pre-wrap;"">!!</code><span> </span>isn't, practically speaking, so important for figuring out whether a single variable is (or even if many variables are) truthy or falsy.<span> </span><code style=""margin: 0px; padding: 1px 5px; border: 0px; font-style: inherit; font-variant: inherit; font-weight: inherit; font-stretch: inherit; font-size: 13px; line-height: inherit; font-family: Consolas, Menlo, Monaco, &quot;Lucida Console&quot;, &quot;Liberation Mono&quot;, &quot;DejaVu Sans Mono&quot;, &quot;Bitstream Vera Sans Mono&quot;, &quot;Courier New&quot;, monospace, sans-serif; vertical-align: baseline; box-sizing: inherit; background-color: rgb(239, 240, 241); white-space: pre-wrap;"">!!myVar === true</code><span> </span>is the same as just<span> </span><code style=""margin: 0px; padding: 1px 5px; border: 0px; font-style: inherit; font-variant: inherit; font-weight: inherit; font-stretch: inherit; font-size: 13px; line-height: inherit; font-family: Consolas, Menlo, Monaco, &quot;Lucida Console&quot;, &quot;Liberation Mono&quot;, &quot;DejaVu Sans Mono&quot;, &quot;Bitstream Vera Sans Mono&quot;, &quot;Courier New&quot;, monospace, sans-serif; vertical-align: baseline; box-sizing: inherit; background-color: rgb(239, 240, 241); white-space: pre-wrap;"">myVar</code>. Comparing<span> </span><code style=""margin: 0px; padding: 1px 5px; border: 0px; font-style: inherit; font-variant: inherit; font-weight: inherit; font-stretch: inherit; font-size: 13px; line-height: inherit; font-family: Consolas, Menlo, Monaco, &quot;Lucida Console&quot;, &quot;Liberation Mono&quot;, &quot;DejaVu Sans Mono&quot;, &quot;Bitstream Vera Sans Mono&quot;, &quot;Courier New&quot;, monospace, sans-serif; vertical-align: baseline; box-sizing: inherit; background-color: rgb(239, 240, 241); white-space: pre-wrap;"">!!X</code><span> </span>to a ""real"" boolean isn't really useful.</p><p style=""margin: 0px 0px 1em; padding: 0px; border: 0px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-variant-numeric: inherit; font-variant-east-asian: inherit; font-weight: 400; font-stretch: inherit; font-size: 15px; line-height: inherit; font-family: Arial, &quot;Helvetica Neue&quot;, Helvetica, sans-serif; vertical-align: baseline; box-sizing: inherit; clear: both; color: rgb(36, 39, 41); letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial;"">What you gain with<span> </span><code style=""margin: 0px; padding: 1px 5px; border: 0px; font-style: inherit; font-variant: inherit; font-weight: inherit; font-stretch: inherit; font-size: 13px; line-height: inherit; font-family: Consolas, Menlo, Monaco, &quot;Lucida Console&quot;, &quot;Liberation Mono&quot;, &quot;DejaVu Sans Mono&quot;, &quot;Bitstream Vera Sans Mono&quot;, &quot;Courier New&quot;, monospace, sans-serif; vertical-align: baseline; box-sizing: inherit; background-color: rgb(239, 240, 241); white-space: pre-wrap;"">!!</code><span> </span>is the ability to check the truthiness of multiple variables<span> </span><em style=""margin: 0px; padding: 0px; border: 0px; font-style: italic; font-variant: inherit; font-weight: inherit; font-stretch: inherit; font-size: inherit; line-height: inherit; font-family: inherit; vertical-align: baseline; box-sizing: inherit;"">against each other</em>in a repeatable, standardized (and JSLint friendly) fashion.</p><h3 style=""margin: 0px 0px 1em; padding: 0px; border: 0px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-variant-numeric: inherit; font-variant-east-asian: inherit; font-weight: 400; font-stretch: inherit; font-size: 17px; line-height: 1.3; font-family: Arial, &quot;Helvetica Neue&quot;, Helvetica, sans-serif; vertical-align: baseline; box-sizing: inherit; word-wrap: break-word; color: rgb(36, 39, 41); letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial;"">Simply casting :(</h3><p style=""margin: 0px 0px 1em; padding: 0px; border: 0px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-variant-numeric: inherit; font-variant-east-asian: inherit; font-weight: 400; font-stretch: inherit; font-size: 15px; line-height: inherit; font-family: Arial, &quot;Helvetica Neue&quot;, Helvetica, sans-serif; vertical-align: baseline; box-sizing: inherit; clear: both; color: rgb(36, 39, 41); letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial;"">That is...</p><ul style=""margin: 0px 0px 1em 30px; padding: 0px; border: 0px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-variant-numeric: inherit; font-variant-east-asian: inherit; font-weight: 400; font-stretch: inherit; font-size: 15px; line-height: inherit; font-family: Arial, &quot;Helvetica Neue&quot;, Helvetica, sans-serif; vertical-align: baseline; list-style: disc; box-sizing: inherit; color: rgb(36, 39, 41); letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial;""><li style=""margin: 0px 0px 0.5em; padding: 0px; border: 0px; font-style: inherit; font-variant: inherit; font-weight: inherit; font-stretch: inherit; font-size: inherit; line-height: inherit; font-family: inherit; vertical-align: baseline; box-sizing: inherit; word-wrap: break-word;""><code style=""margin: 0px; padding: 1px 5px; border: 0px; font-style: inherit; font-variant: inherit; font-weight: inherit; font-stretch: inherit; font-size: 13px; line-height: inherit; font-family: Consolas, Menlo, Monaco, &quot;Lucida Console&quot;, &quot;Liberation Mono&quot;, &quot;DejaVu Sans Mono&quot;, &quot;Bitstream Vera Sans Mono&quot;, &quot;Courier New&quot;, monospace, sans-serif; vertical-align: baseline; box-sizing: inherit; background-color: rgb(239, 240, 241); white-space: pre-wrap;"">0 === false</code><span> </span>is<span> </span><code style=""margin: 0px; padding: 1px 5px; border: 0px; font-style: inherit; font-variant: inherit; font-weight: inherit; font-stretch: inherit; font-size: 13px; line-height: inherit; font-family: Consolas, Menlo, Monaco, &quot;Lucida Console&quot;, &quot;Liberation Mono&quot;, &quot;DejaVu Sans Mono&quot;, &quot;Bitstream Vera Sans Mono&quot;, &quot;Courier New&quot;, monospace, sans-serif; vertical-align: baseline; box-sizing: inherit; background-color: rgb(239, 240, 241); white-space: pre-wrap;"">false</code>.</li><li style=""margin: 0px; padding: 0px; border: 0px; font-style: inherit; font-variant: inherit; font-weight: inherit; font-stretch: inherit; font-size: inherit; line-height: inherit; font-family: inherit; vertical-align: baseline; box-sizing: inherit; word-wrap: break-word;""><code style=""margin: 0px; padding: 1px 5px; border: 0px; font-style: inherit; font-variant: inherit; font-weight: inherit; font-stretch: inherit; font-size: 13px; line-height: inherit; font-family: Consolas, Menlo, Monaco, &quot;Lucida Console&quot;, &quot;Liberation Mono&quot;, &quot;DejaVu Sans Mono&quot;, &quot;Bitstream Vera Sans Mono&quot;, &quot;Courier New&quot;, monospace, sans-serif; vertical-align: baseline; box-sizing: inherit; background-color: rgb(239, 240, 241); white-space: pre-wrap;"">!!0 === false</code><span> </span>is<span> </span><code style=""margin: 0px; padding: 1px 5px; border: 0px; font-style: inherit; font-variant: inherit; font-weight: inherit; font-stretch: inherit; font-size: 13px; line-height: inherit; font-family: Consolas, Menlo, Monaco, &quot;Lucida Console&quot;, &quot;Liberation Mono&quot;, &quot;DejaVu Sans Mono&quot;, &quot;Bitstream Vera Sans Mono&quot;, &quot;Courier New&quot;, monospace, sans-serif; vertical-align: baseline; box-sizing: inherit; background-color: rgb(239, 240, 241); white-space: pre-wrap;"">true</code>.</li></ul><p style=""margin: 0px 0px 1em; padding: 0px; border: 0px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-variant-numeric: inherit; font-variant-east-asian: inherit; font-weight: 400; font-stretch: inherit; font-size: 15px; line-height: inherit; font-family: Arial, &quot;Helvetica Neue&quot;, Helvetica, sans-serif; vertical-align: baseline; box-sizing: inherit; clear: both; color: rgb(36, 39, 41); letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial;"">The above's not so useful.<span> </span><code style=""margin: 0px; padding: 1px 5px; border: 0px; font-style: inherit; font-variant: inherit; font-weight: inherit; font-stretch: inherit; font-size: 13px; line-height: inherit; font-family: Consolas, Menlo, Monaco, &quot;Lucida Console&quot;, &quot;Liberation Mono&quot;, &quot;DejaVu Sans Mono&quot;, &quot;Bitstream Vera Sans Mono&quot;, &quot;Courier New&quot;, monospace, sans-serif; vertical-align: baseline; box-sizing: inherit; background-color: rgb(239, 240, 241); white-space: pre-wrap;"">if (!0)</code><span> </span>gives you the same results as<span> </span><code style=""margin: 0px; padding: 1px 5px; border: 0px; font-style: inherit; font-variant: inherit; font-weight: inherit; font-stretch: inherit; font-size: 13px; line-height: inherit; font-family: Consolas, Menlo, Monaco, &quot;Lucida Console&quot;, &quot;Liberation Mono&quot;, &quot;DejaVu Sans Mono&quot;, &quot;Bitstream Vera Sans Mono&quot;, &quot;Courier New&quot;, monospace, sans-serif; vertical-align: baseline; box-sizing: inherit; background-color: rgb(239, 240, 241); white-space: pre-wrap;"">if (!!0 === false)</code>. I can't think of a good case for casting a variable to boolean and then comparing to a ""true"" boolean.</p><!--EndFragment--></body></html>";

        public static string QuoteFail = @"Version:0.9
StartHTML:0000000191
EndHTML:0000001977
StartFragment:0000000227
EndFragment:0000001941
SourceURL:https://medium.com/@housecor/in-defense-of-javascript-classes-e50bf2270a95
<html>
<body>
<!--StartFragment--><h4 name=""d2fd"" id=""d2fd"" class=""graf graf--h4 graf-after--p"" style=""font-family: medium-content-sans-serif-font, &quot;Lucida Grande&quot;, &quot;Lucida Sans Unicode&quot;, &quot;Lucida Sans&quot;, Geneva, Arial, sans-serif; letter-spacing: -0.012em; font-weight: 600; font-style: normal; margin: 30px 0px 0px -1.63px; color: rgba(0, 0, 0, 0.84); --baseline-multiplier:0.22; font-size: 26px; line-height: 1.22; font-variant-ligatures: normal; font-variant-caps: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial;"">Low Learning Curve</h4><p name=""90de"" id=""90de"" class=""graf graf--p graf-after--h4"" style=""margin: 6px 0px 0px; --baseline-multiplier:0.17; font-family: medium-content-serif-font, Georgia, Cambria, &quot;Times New Roman&quot;, Times, serif; font-weight: 400; font-style: normal; font-size: 21px; line-height: 1.58; letter-spacing: -0.003em; color: rgba(0, 0, 0, 0.84); font-variant-ligatures: normal; font-variant-caps: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial;"">If you’re working on a team that already understands classes, then ES6 classes are clearly easier to understand than the alternatives mentioned above. They look, feel, and operate similar to the C# and Java classes so many enterprise developers have already embraced (yes, for better or worse).</p><!--EndFragment-->
</body>
</html>
";


        public static string SlashdotFail = @"Version:0.9
StartHTML:0000000191
EndHTML:0000001977
StartFragment:0000000227
EndFragment:0000001941
SourceURL:https://medium.com/@housecor/in-defense-of-javascript-classes-e50bf2270a95
<html>
<body>
<!--StartFragment--><footer class=""clearfix meta article-foot"">
			<div class=""story-controls"">
				<div class=""janrainSocialPlaceholder"" data-janrain-url=""https://hardware.slashdot.org/story/18/04/26/183255/robot-launched-weather-balloons-in-alaska-hasten-demise-of-remote-stations"" data-janrain-title=""Robot-Launched Weather Balloons in Alaska Hasten Demise of Remote Stations"" data-janrain-message=""Robot-Launched Weather Balloons in Alaska Hasten Demise of Remote Stations @slashdot""><div class=""janrainSocialRoot janrainOrientationHorizontal janrainFormFactorBar janrainProviders_5 janrainGravityEast janrainModeBroadcast janrainShareCountHidden"" data-bind=""css: cssClass, attr: { dir: i18n.direction }"" dir=""ltr"">
    <div class=""janrainSocialBar"">
        <div class=""janrainShareCountContainer"" data-bind=""with: shareCounter""><span class=""janrainShareCount"" data-bind=""text: prettyTotalCount"">0</span></div><div class=""janrainDrawerButtonContainer"" data-bind=""visible: formFactor === 'drawer'"" style=""display: none;""><button type=""button"" class=""janrainDrawerButton"" data-bind=""
                    click: toggleDrawer,
                    text: drawerButtonText"">Share</button></div><div class=""janrainProviderList"" data-bind=""foreach: providerList.providers""><button type=""button"" class=""janrainProvider janrain_facebookButton"" data-bind=""
                    click: $root.shareUsing,
                    event: { focus: $root.updateSelected },
                    attr: {
                        'class': 'janrainProvider janrain_' + buttonClass,
                        title: $root.getProviderTitle($data)
                    },
                    css: {
                        'shareScreenVisible': $root.shareScreen.visible,
                        'currentProviderShowing': ( $data.name === $root.selectedProvider().name ) ||
                                                  ( $data.name === 'email' &amp;&amp;
                                                    $root.selectedProvider().isEmailProvider )
                    }
                    "" title=""Facebook"">
                <img data-bind=""attr: {
                    src: image,
                    alt: friendlyName,
                    title: $root.getProviderTitle($data)
                }"" src=""//cdn-social.janrain.com/social/img/64/facebook.png"" alt=""Facebook"" title=""Facebook"">
            </button><button type=""button"" class=""janrainProvider janrain_twitterButton"" data-bind=""
                    click: $root.shareUsing,
                    event: { focus: $root.updateSelected },
                    attr: {
                        'class': 'janrainProvider janrain_' + buttonClass,
                        title: $root.getProviderTitle($data)
                    },
                    css: {
                        'shareScreenVisible': $root.shareScreen.visible,
                        'currentProviderShowing': ( $data.name === $root.selectedProvider().name ) ||
                                                  ( $data.name === 'email' &amp;&amp;
                                                    $root.selectedProvider().isEmailProvider )
                    }
                    "" title=""Twitter"">
                <img data-bind=""attr: {
                    src: image,
                    alt: friendlyName,
                    title: $root.getProviderTitle($data)
                }"" src=""//cdn-social.janrain.com/social/img/64/twitter.png"" alt=""Twitter"" title=""Twitter"">
            </button><button type=""button"" class=""janrainProvider janrain_linkedinButton"" data-bind=""
                    click: $root.shareUsing,
                    event: { focus: $root.updateSelected },
                    attr: {
                        'class': 'janrainProvider janrain_' + buttonClass,
                        title: $root.getProviderTitle($data)
                    },
                    css: {
                        'shareScreenVisible': $root.shareScreen.visible,
                        'currentProviderShowing': ( $data.name === $root.selectedProvider().name ) ||
                                                  ( $data.name === 'email' &amp;&amp;
                                                    $root.selectedProvider().isEmailProvider )
                    }
                    "" title=""LinkedIn"">
                <img data-bind=""attr: {
                    src: image,
                    alt: friendlyName,
                    title: $root.getProviderTitle($data)
                }"" src=""//cdn-social.janrain.com/social/img/64/linkedin.png"" alt=""LinkedIn"" title=""LinkedIn"">
            </button><button type=""button"" class=""janrainProvider janrain_native-googleplusButton"" data-bind=""
                    click: $root.shareUsing,
                    event: { focus: $root.updateSelected },
                    attr: {
                        'class': 'janrainProvider janrain_' + buttonClass,
                        title: $root.getProviderTitle($data)
                    },
                    css: {
                        'shareScreenVisible': $root.shareScreen.visible,
                        'currentProviderShowing': ( $data.name === $root.selectedProvider().name ) ||
                                                  ( $data.name === 'email' &amp;&amp;
                                                    $root.selectedProvider().isEmailProvider )
                    }
                    "" title=""Google+"">
                <img data-bind=""attr: {
                    src: image,
                    alt: friendlyName,
                    title: $root.getProviderTitle($data)
                }"" src=""//cdn-social.janrain.com/social/img/64/googleplus.png"" alt=""Google+"" title=""Google+"">
            </button><button type=""button"" class=""janrainProvider janrain_native-redditButton"" data-bind=""
                    click: $root.shareUsing,
                    event: { focus: $root.updateSelected },
                    attr: {
                        'class': 'janrainProvider janrain_' + buttonClass,
                        title: $root.getProviderTitle($data)
                    },
                    css: {
                        'shareScreenVisible': $root.shareScreen.visible,
                        'currentProviderShowing': ( $data.name === $root.selectedProvider().name ) ||
                                                  ( $data.name === 'email' &amp;&amp;
                                                    $root.selectedProvider().isEmailProvider )
                    }
                    "" title=""Reddit"">
                <img data-bind=""attr: {
                    src: image,
                    alt: friendlyName,
                    title: $root.getProviderTitle($data)
                }"" src=""//cdn-social.janrain.com/social/img/64/reddit.png"" alt=""Reddit"" title=""Reddit"">
            </button></div>
    </div>
    <div class=""janrainShareForm janrainGravityEast janrainProvider_undefined"" data-bind=""
            visible: shareScreen.visible,
            with: shareScreen,
            css: $root.shareScreenClass,
            style: {
                left: shareScreen.offsetLeft,
                top: shareScreen.offsetTop
            }"" style=""display: none;"">
        <div class=""janrainShareUserInputArea"">
            <span class=""janrainShareCaption janrainShareTitle"" data-bind=""text: caption"">Share this with your friends!</span>
            <button type=""button"" class=""janrainShareCloseButton"" data-bind=""
                    click: $parent.closeShareForm,
                    attr: {
                        title: $root.i18n.getText('common.close')
                    }"" title=""Close""></button>
            <!-- ko if: showEmailProviders --><!-- /ko -->
            <!-- ko ifnot: showEmailProviders -->
            <div class=""janrainShareFromLine"" data-bind=""
                    visible: provider().isEmailProvider,
                    css: { janrainEmailSharing: provider().isEmailProvider }
                    "" style=""display: none;""><span class=""janrainShareCaption"" data-bind=""text: $root.i18n.getText('email.from')"">From</span><span class=""janrainReturnAddress"" data-bind=""text: contactList.currentUserInfo().name""></span><span class=""janrainReturnAddressEmail"" data-bind=""
                    visible: contactList.currentUserInfo().email,
                    text: contactList.currentUserInfo().email"" style=""display: none;""></span></div>
            <div class=""janrainContactList"" data-bind=""
                    with: contactList,
                    visible: mode() === 'contact' || mode() === 'email'"" style=""display: none;""><span class=""janrainShareCaption"" data-bind=""text: $root.i18n.getText('email.to')"">To</span><input class=""janrainShareContactSearch"" data-bind=""
                        value: search,
                        valueUpdate: 'keyup',
                        attr: {
                            placeholder: $root.i18n.getText('common.placeholderContactSearch'),
                            title: $root.i18n.getText('common.placeholderContactSearch')
                        },
                        hasFocus: contactSearchFocused,
                        event: { keypress: keypressHandler, keydown: keydownHandler }
                    "" placeholder=""Type name or username..."" title=""Type name or username..."">
                <ul class=""janrainShareContactSearchResults"" data-bind=""
                        visible: searchResults().length > 0,
                        foreach: searchResults"" style=""display: none;""></ul>
                <div class=""janrainShareSelectedContacts"" data-bind=""visible: selected().length > 0"" style=""display: none;""><a href=""#"" data-bind=""
                        text: selectedMessage,
                        click: toggleSelectedDetails
                    ""></a>
                    <div class=""janrainShareSelectedContactsDetails"" data-bind=""visible: showSelectedDetails"" style=""display: none;""><ul class=""janrainShareSelectedContactsList"" data-bind=""foreach: selected""></ul>
                    </div>
                </div>
            </div>
            <!-- ko with: shareContent -->
            <div class=""janrainComposeMessageHeader"">
                <span class=""janrainShareCaption"" data-bind=""text: $root.i18n.getText('email.composeMessage')"">Compose your message</span>
            </div>
            <textarea class=""janrainShareMessage"" data-bind=""
                value: message,
                valueUpdate: 'afterkeydown',
                attr: {
                    placeholder: $root.i18n.getText('common.placeholderComment'),
                    title: $root.i18n.getText('common.placeholderComment')
                },
                hasFocus: $parent.shareMessageFocused"" placeholder=""Add a comment..."" title=""Add a comment...""></textarea>
            <div class=""janrainShareInputCounter"">
                <span data-bind=""
                        visible: showCharCount,
                        text: remainingCharCount,
                        css: { janrainShareInputCounterExcess: remainingCharCount() < 1 }"" class=""janrainShareInputCounterExcess""></span>
            </div>
            <!-- /ko -->
            <div class=""janrainShareSubmitActions"">
                <button type=""button"" data-bind=""
                        attr: { 'class': 'janrainShare background-' + provider().name + ' border-' + provider().name },
                        click: $root.submitShare,
                        css: { janrainLoading: shareState() === 'working' }"" class=""janrainShare background-undefined border-undefined""><span data-bind=""
                            text: shareButtonText
                            "">Share</span></button>
                <!-- ko with: $root.shareResponseMessage --><span class=""janrainShareResponseMessage janrainError"" data-bind=""
                        text: text,
                        css: {
                            janrainVisible: visible(),
                            janrainError: !successful()
                        }""></span>
                <!-- /ko -->
                <span data-bind=""
                        attr: { 'class': 'janrainSharingProgressBar background-' + provider().name },
                        css: { janrainVisible: shareState() === 'working' }
                    "" class=""janrainSharingProgressBar background-undefined""><span class=""janrainProgressIndicator ellipsis-1""></span><span class=""janrainProgressIndicator ellipsis-2""></span><span class=""janrainProgressIndicator ellipsis-3""></span><img class=""janrainProviderDestination"" data-bind=""attr:{ src: provider().solidImage }"">
                </span>
            </div>
            <!-- /ko -->
        </div>
        <!-- ko ifnot: showEmailProviders -->
        <!-- ko with: shareContent -->
        <div class=""janrainShareContent"" data-bind=""css: { janrainImageSupported: currentProvider().supportsImages &amp;&amp; $root.options.image }"">
            <div class=""janrainShareImage"">
                <img data-bind=""attr: { src: image }"" alt=""share image"" src="""">
            </div><div class=""janrainShareTextContainer"">
                <span class=""janrainShareTitle"" data-bind=""text: title"">Robot-Launched Weather Balloons in Alaska Hasten Demise of Remote Stations</span>
                <span class=""janrainShareDescription"" data-bind=""text: prettyDescription""></span>
                <a class=""janrainShareUrl text-undefined"" data-bind=""
                    attr: {
                        'class': 'janrainShareUrl text-' + currentProvider().name,
                        href: url
                    },
                    text: url"" href=""https://hardware.slashdot.org/story/18/04/26/183255/robot-launched-weather-balloons-in-alaska-hasten-demise-of-remote-stations"">https://hardware.slashdot.org/story/18/04/26/183255/robot-launched-weather-balloons-in-alaska-hasten-demise-of-remote-stations</a>
            </div>
        </div>
        <!-- /ko -->
        <!-- /ko -->
    </div>
</div>
</div>




			</div>


				<div class=""story-tags"">
					<span class=""tright tags""><menu type=""toolbar"" class=""edit-bar"">
		<span id=""tagbar-99898767"" class=""tag-bar none"">
			<a class=""topic tag"" rel=""statictag"" href=""//slashdot.org/tag/"" target=""_blank""></a>
<a class=""popular tag"" rel=""statictag"" href=""//slashdot.org/tag/robot"" target=""_blank"">robot</a>
<a class=""popular tag"" rel=""statictag"" href=""//slashdot.org/tag/business"" target=""_blank"">business</a>
<a class=""popular tag"" rel=""statictag"" href=""//slashdot.org/tag/money"" target=""_blank"">money</a>

		</span>

			<a class=""edit-toggle"" href=""/my/login/"" onclick=""show_login_box();return false;"">
				<span class=""icon-tag btn collapse""></span>
			</a>


		<div class=""tag-menu"">
			<input class=""tag-entry default"" type=""text"" value=""apply tags"">
		</div>





	</menu></span>
				</div>


		</footer>
<!--EndFragment-->
</body>
</html>
";

    }
}
