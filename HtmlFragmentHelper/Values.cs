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


        public static string SlashdotMoreFail = @"Version:0.9
StartHTML:0000000138
EndHTML:0000040684
StartFragment:0000000174
EndFragment:0000040648
SourceURL:https://slashdot.org/
<html>
<body>
<!--StartFragment--><article id=""firehose-99902413"" data-fhid=""99902413"" data-fhtype=""story"" class=""fhitem fhitem-story article usermode thumbs grid_24 currfh"" style=""box-sizing: border-box; margin: 0px 0.53125px 10px; padding: 0px; width: 540.906px; float: left; display: block; position: relative; color: rgb(54, 54, 54); font-family: Arial, sans-serif; font-size: 13px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial;""><header style=""box-sizing: border-box; margin: 0px; padding: 0px; display: block;""><h2 class=""story"" style=""box-sizing: border-box; margin: 0px; padding: 4px 155px 4px 10px; font-weight: bold; font-size: 1rem; color: white; line-height: 1.5rem; background-color: rgb(1, 103,
 101); font-family: arial, serif;""><span id=""title-99902413"" class=""story-title"" style=""box-sizing: border-box; margin: 0px; padding: 0px; left: 15px; position: relative; color: white;""><a onclick=""return toggle_fh_body_wrap_return(this);"" href=""https://entertainment.slashdot.org/story/18/04/26/2311200/cord-cutting-caused-by-74-percent-tv-price-hikes-since-2000-says-report"" style=""box-sizing: border-box; margin: 0px; padding: 0px; color: white; text-decoration: none; cursor: pointer;"">ord Cutting Caused By 74 Percent TV Price Hikes Since 2000, Says Report</a><span> </span><span class="" no extlnk"" style=""box-sizing: border-box; margin: 0px; padding: 0px; display: inline-block; height: 14px; position: relative; width: 14px;""><a class=""story-sourcelnk"" href=""http://www.dslreports.com/shownews/Report-Cord-Cutting-Caused-by-74-TV-Price-Hikes-Since-2000-141703"" title=""External link - http://www.dslreports.com/shownews/Report-Cord-Cutting-Caused-by-74-TV-Price-Hikes-Since-2000-141703"" target=""_blank"" style=""box-sizin
g: border-box; margin: 0px 15px; padding: 0px; color: rgb(147, 204, 204); text-decoration: none; cursor: pointer; font-size: 0.75rem; white-space: nowrap; font-weight: normal;"">(dslreports.com)</a></span></span><span class=""comment-bubble"" style=""box-sizing: border-box; margin: 0px; padding: 0px 5px; position: absolute; top: 12px; background-color: rgb(30, 35, 41); line-height: 2rem; width: 45px; text-align: center; right: 12px; border-radius: 3px;""><a href=""https://entertainment.slashdot.org/story/18/04/26/2311200/cord-cutting-caused-by-74-percent-tv-price-hikes-since-2000-says-report#comments"" title="""" style=""box-sizing: border-box; margin: 0px; padding: 0px; color: rgb(255, 255, 255); text-decoration: none; cursor: pointer;"">13</a></span></h2><div class=""details"" id=""details-99902413"" style=""box-sizing: border-box; margin: 0px; padding: 5px 20px; color: rgb(77, 77, 77); font-size: 0.85em; line-height: 1.4rem; background: rgb(242, 242, 242);""><span class=""story-details"" style=""box-sizing: border-box; margin:
 0px 3px 0px 0px; padding: 9px 0px; border-right: 1px solid rgb(222, 222, 222);""><span class=""story-views"" style=""box-sizing: border-box; margin: 0px; padding: 0px 10px 0px 0px;""><span class=""sodify"" onclick=""firehose_set_options('color', 'red')"" title=""Filter Firehose to entries rated red or better"" style=""box-sizing: border-box; margin: 0px; padding: 0px;""></span><span class=""icon-beaker pop1 "" alt=""Popularity"" title=""Filter Firehose to entries rated red or better"" onclick=""firehose_set_options('color', 'red')"" style=""box-sizing: border-box; margin: 0px; padding: 0px; font-size: 0.7rem; cursor: pointer; color: rgb(204, 0, 0);""><span style=""box-sizing: border-box; margin: 0px; padding: 0px;""></span></span></span></span><span class=""story-byline"" style=""box-sizing: border-box; margin: 0px 20px 0px 0px; padding: 0px 0px 0px 10px; display: inline; position: absolute; line-height: 1.4rem; height: 20px; overflow: hidden !important;"">Posted by<span> </span><a href=""https://twitter.com/BeauHD"" rel=""nofollow"" style=""
box-sizing: border-box; margin: 0px; padding: 0px; color: rgb(51, 51, 51); text-decoration: none; cursor: pointer; font-weight: bold;"">BeauHD</a><span> </span><time id=""fhtime-99902413"" datetime=""on Thursday April 26, 2018 @09:00PM"" style=""box-sizing: border-box; margin: 0px; padding: 0px;"">on Thursday April 26, 2018 @09:00PM</time><span> </span>from the<span> </span><span class=""dept-text"" style=""box-sizing: border-box; margin: 0px; padding: 0px;"">cause-and-effect</span><span> </span>dept.</span></div></header><div class=""body"" id=""fhbody-99902413"" style=""box-sizing: border-box; margin: 0px; padding: 0px;""><div id=""text-99902413"" class=""p"" style=""box-sizing: border-box; margin: 20px; padding: 0px;"">A<span> </span><a href=""https://www.fiercecable.com/cable/pay-tv-bills-up-74-since-2000-leading-cause-cord-cutting-kagan-says"" style=""box-sizing: border-box; margin: 0px; padding: 0px; color: rgb(0, 47, 47); text-decoration: underline; cursor: pointer;"">new study</a><span> </span>by Kagan, S&amp;P Global Market Int
elligence finds that cord cutting is being<span> </span><a href=""http://www.dslreports.com/shownews/Report-Cord-Cutting-Caused-by-74-TV-Price-Hikes-Since-2000-141703"" style=""box-sizing: border-box; margin: 0px; padding: 0px; color: rgb(0, 47, 47); text-decoration: underline; cursor: pointer;"">caused primarily by a 74% increase in customer cable bills since 2000</a>. From a report:<i style=""box-sizing: border-box; margin: 0.5em; padding: 0px 0px 0px 1em; font-style: normal; border-left: 3px solid rgb(221, 221, 221); display: block;"">That increase is even adjusted for inflation, and it should be noted that individual earnings have seen a modest decline during that same period, making soaring cable rates untenable for many. This affordability gap is ""squeezing penetration rates, particularly among the more economically vulnerable households,"" the research company added. As<span> </span><a href=""http://i.dslr.net/syms/7f561edfd62f277d2c44623089056d45.png"" style=""box-sizing: border-box; margin: 0px; padding: 0px; c
olor: rgb(0, 47, 47); text-decoration: underline; cursor: pointer;"">their chart</a><span> </span>illustrates, prices for multichannel packages have steadily risen from just below $60 a month in 2000 to close to $100 in 2016. All while incomes remained largely stagnant. As customers grow increasingly angry at cable TV rate hikes and defect to streaming alternatives, most cable operators are simply raising the price of broadband (often via usage caps and overage fees) to try and make up for lost revenue. And because most parts of America still don't really see healthy broadband competition, they can consistently get away with it.</i><br style=""box-sizing: border-box; margin: 0px; padding: 0px;""></div></div><footer class=""clearfix meta article-foot"" style=""box-sizing: border-box; margin: 0px 0px 10px; padding: 0px; display: block;""><div class=""story-controls"" style=""box-sizing: border-box; margin: 0px; padding: 0px; display: inline-block; float: left; clear: both;""><div class=""janrainSocialPlaceholder"" data-janra
in-url=""https://entertainment.slashdot.org/story/18/04/26/2311200/cord-cutting-caused-by-74-percent-tv-price-hikes-since-2000-says-report"" data-janrain-title=""Cord Cutting Caused By 74 Percent TV Price Hikes Since 2000, Says Report"" data-janrain-message=""Cord Cutting Caused By 74 Percent TV Price Hikes Since 2000, Says Report @slashdot"" style=""box-sizing: border-box; margin: 0px; padding: 0px;""><div class=""janrainSocialRoot janrainOrientationHorizontal janrainFormFactorBar janrainProviders_5 janrainGravityEast janrainModeBroadcast janrainShareCountHidden"" data-bind=""css: cssClass, attr: { dir: i18n.direction }"" dir=""ltr"" style=""box-sizing: border-box; margin: 0px; padding: 0px; font-family: &quot;Helvetica Neue&quot;, Helvetica, FreeSans, Arial, sans-serif; position: relative; display: inline-block; height: 32px;""><div class=""janrainSocialBar"" style=""box-sizing: border-box; margin: 0px; padding: 0px; font-size: 1rem; display: inline-block; position: relative; vertical-align: top; overflow: visible;""><div class
=""janrainProviderList"" data-bind=""foreach: providerList.providers"" style=""box-sizing: border-box; margin: 0px; padding: 0px; font-size: 1rem; height: 32px; display: inline-block;""><button type=""button"" class=""janrainProvider janrain_facebookButton"" data-bind=""
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
                    "" title=""Facebook"" style=""box-sizing: border-box; margin: 0px; padding: 0px; height: 24px; display: inline-block; font-size: 1rem; line-height: 25px; text-decoration: none; text-align: center; position: relative; border: 1px solid rgb(231, 232, 234) !important; border-radius: 0px !important; background: padding-box rgb(242, 242, 242); font-weight: normal !important; color: rgb(0, 134, 134); font-family: &quot;Helvetica Neue&quot;, Helvetica, FreeSans, Arial, sans-serif; cursor: pointer; width: 24px; top: 0px;""></button><button type=""button"" class=""janrainProvider janrain_twitterButton"" data-bind=""
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
                    "" title=""Twitter"" style=""box-sizing: border-box; margin: 0px 0px 0px 9px; padding: 0px; height: 24px; display: inline-block; font-size: 1rem; line-height: 25px; text-decoration: none; text-align: center; position: relative; border: 1px solid rgb(231, 232, 234) !important; border-radius: 0px !important; background: padding-box rgb(242, 242, 242); font-weight: normal !important; color: rgb(0, 134, 134); font-family: &quot;Helvetica Neue&quot;, Helvetica, FreeSans, Arial, sans-serif; cursor: pointer; width: 24px; top: 0px;""></button><button type=""button"" class=""janrainProvider janrain_linkedinButton"" data-bind=""
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
                    "" title=""LinkedIn"" style=""box-sizing: border-box; margin: 0px 0px 0px 9px; padding: 0px; height: 24px; display: inline-block; font-size: 1rem; line-height: 25px; text-decoration: none; text-align: center; position: relative; border: 1px solid rgb(231, 232, 234) !important; border-radius: 0px !important; background: padding-box rgb(242, 242, 242); font-weight: normal !important; color: rgb(0, 134, 134); font-family: &quot;Helvetica Neue&quot;, Helvetica, FreeSans, Arial, sans-serif; cursor: pointer; width: 24px; top: 0px;""></button><button type=""button"" class=""janrainProvider janrain_native-googleplusButton"" data-bind=""
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
                    "" title=""Google+"" style=""box-sizing: border-box; margin: 0px 0px 0px 9px; padding: 0px; height: 24px; display: inline-block; font-size: 1rem; line-height: 25px; text-decoration: none; text-align: center; position: relative; border: 1px solid rgb(231, 232, 234) !important; border-radius: 0px !important; background: padding-box rgb(242, 242, 242); font-weight: normal !important; color: rgb(0, 134, 134); font-family: &quot;Helvetica Neue&quot;, Helvetica, FreeSans, Arial, sans-serif; cursor: pointer; width: 24px; top: 0px;""></button><button type=""button"" class=""janrainProvider janrain_native-redditButton"" data-bind=""
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
                    "" title=""Reddit"" style=""box-sizing: border-box; margin: 0px 0px 0px 9px; padding: 0px; height: 24px; display: inline-block; font-size: 1rem; line-height: 25px; text-decoration: none; text-align: center; position: relative; border: 1px solid rgb(231, 232, 234) !important; border-radius: 0px !important; background: padding-box rgb(242, 242, 242); font-weight: normal !important; color: rgb(0, 134, 134); font-family: &quot;Helvetica Neue&quot;, Helvetica, FreeSans, Arial, sans-serif; cursor: pointer; width: 24px; top: 0px;""></button></div></div></div></div></div><div class=""story-tags"" style=""box-sizing: border-box; margin: 0px; padding: 0px; display: inline; float: right;""><span class=""tright tags"" style=""box-sizing: border-box; margin: 0px; padding: 0px; text-align: right !important; color: rgb(119, 119, 119); position: relative; width: auto; float: right;""><menu type=""toolbar"" class=""edit-bar"" style=""box-sizing: border-box; margin: 0px; padding: 0px; text-decoration: none; position: relative
;""><span id=""tagbar-99902413"" class=""tag-bar none"" style=""box-sizing: border-box; margin: 0px; padding: 0px; text-decoration: none;""><a class=""topic tag"" rel=""statictag"" href=""https://slashdot.org/tag/"" target=""_blank"" style=""box-sizing: border-box; margin: 0px; padding: 0.1em 0.2em; color: rgb(0, 47, 47); text-decoration: none; cursor: pointer; position: relative; border-radius: 2px;""></a><a class=""popular tag"" rel=""statictag"" href=""https://slashdot.org/tag/business"" target=""_blank"" style=""box-sizing: border-box; margin: 0px; padding: 0.1em 0.2em; color: rgb(0, 47, 47); text-decoration: none; cursor: pointer; position: relative; border-radius: 2px;"">business</a><span> </span><a class=""popular tag"" rel=""statictag"" href=""https://slashdot.org/tag/internet"" target=""_blank"" style=""box-sizing: border-box; margin: 0px; padding: 0.1em 0.2em; color: rgb(0, 47, 47); text-decoration: none; cursor: pointer; position: relative; border-radius: 2px;"">internet</a><span> </span><a class=""popular tag"" rel=""statictag"" href=""htt
ps://slashdot.org/tag/money"" target=""_blank"" style=""box-sizing: border-box; margin: 0px; padding: 0.1em 0.2em; color: rgb(0, 47, 47); text-decoration: none; cursor: pointer; position: relative; border-radius: 2px;"">money</a><span> </span></span><a class=""edit-toggle"" href=""https://slashdot.org/my/login/"" onclick=""show_login_box();return false;"" style=""box-sizing: border-box; margin: 0px 0.5em 0px 0px; padding: 0px; color: rgb(0, 134, 134); text-decoration: none; cursor: pointer; display: inline-block;""><span class=""icon-tag btn collapse"" style=""box-sizing: border-box; margin: 0px; padding: 1px; text-decoration: none; display: inline-block; font-size: 0.875rem; line-height: 1.42857; text-align: center; white-space: nowrap; vertical-align: middle; touch-action: manipulation; cursor: pointer; user-select: none; background-image: none; border: 1px solid rgb(231, 232, 234); border-radius: 4px; font-weight: 400 !important; color: rgb(0, 134, 134); background-color: rgb(242, 242, 242);""></span></a></menu></span></div
></footer></article><article class=""fhitem fhitem-story article usermode thumbs grid_24 article-nel-1665"" style=""box-sizing: border-box; margin: 0px 0.53125px 20px; padding: 0px; width: 540.906px; float: left; display: block !important; position: relative; color: rgb(54, 54, 54); font-family: Arial, sans-serif; font-size: 13px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial;""><header style=""box-sizing: border-box; margin: 0px; padding: 0px; display: block;""><span class=""topic"" style=""box-sizing: border-box; margin: 0px; padding: 0px; overflow: hidden; height: 40px; width: auto; position: absolute; top: 12px; right: 70px; max-width: 100px;""><img src=""data:image/png;base64,iVBORw0KGgoAAAAN
SUhEUgAAADAAAAAwCAYAAABXAvmHAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAABHFJREFUeNrUWl1IFFEUvrsoUWKYmZZirRAWVP4g2oOCqxT4UP685VL0I9iLEGT0mPZYISL5JJg+SfSyllBG5BboQ1Zs9gMWgYKsoS66KauJ0XS/ccZ2x9mde+/MuvnBZVx398757vnOuefcWRuxAJIkOekFo5wOhzL0MKmM13S8stlsr0i8QI0uoKOHjgVJHAvKHM6tNNxJh0eyHp6YEqGTp9DhlmIP3MNhtfG1JqUiIq1LVhnfLsUP7WaN75Hij57tbLwYCStksxj8JbV0v5Su338q+eYWYyYnm17A0ovbjPQGRsZJ28MRsrS8Kr9O3rWDNFYXE9fpPLMheZlufr0RCSBV0ssEHSkis0/7l0jrgyHy/qtP9/2iI1nkRn0pyc1OEyUQoKOQkpiMRAArXysyc9+Lj6TryduNVY+GqzXFskcEgRKkYhMBZRf0iEoGK88DeKOtqUqWlwAq1DrKHvLPFt5ZVtb+kHvPp4jDkS2vKg8gM9ftR+TblF+EQEuYB1CY0YuX1/jOIR/xBVZJalIiuXXmEDVqmjR3PmOSkQp4oOtmjUhcyF5QPXBN1HhgPrhGRieWqCwyycCd88RZmMM8F8g23n0s4omLoR5YYM08WuNVlOTsJq6S9H+R5p2gqXRYzkwsyExLpuQvcGUk6oE9diV4mdNm35uZTcarXggFvACDWq9UysYZGd98roy8/hbgIYDq2JmgdFJMwA0++YJcfj5belQekAi88m58OsxwEMUY/DJPBr1+2cNVx1JZp5cJ5LN8cj74mwx+nhfeQhGkGI3Vm9/7PruyMTeuJ7KSSFYKU3ott0fpX8OlMzojr04kMN5QN6a6h39oZDrL+nUHCBSwSAerFA2H03cKEUBMaRcGMcYYDw67FdLBPgC38wLxFCmmcE/c2wiGBBBc0aSznkKThVbf7fVHlZbbO2eegBGw+
uW5/MUrJKJNvSIwJFBXmEZ2JtqF349M4GfU9zFnXeE+8wSiTQTdi2gfQWq0+lXHU6l3E5gIfDD6EDSuzTIg5jqZIeR2o80Q92KU5aRdOas0hKskI0wqDWUHhKSjblzRPI57MWISPhpj6cLgTrgVmQNXrBIaGZTQoQXbkYN75WaFpyIVkY4aSjbeXgBl83LAb1hpos5BDVR/Km9T19Xp8el6AfEEz3L1BLzlNFDfyt5Jwfjmc6UymdD8r91pUYo0VWbxyHK9nFZe9PLQRgfF2suiYUG/jKZF9Zg2IWAv4TQe6A9Nox0831TbQKM6X68HRkkNqcBo1fiGsv0iCaFDeyrh4ekNzLSDOOCqqSySpSSw8mFHK6aPVVSJYGV5YPJsaONYRXuwhUPUS7E82NILak70U+Pr4na0yNIjGxwt5lACAV0CComYHO6aXPWN2pEa32/4KUqi1arjdQz8veVPa7b1A47/jESPKdFZIae4PeQLDew4PGatJVZCedC9FZJyK+k8Nti2PzXQIeJQnmZa8WOPAlE7bBaRKVC6unzlqDKSQR+UFnaMWPRzm78CDADXPDfw7bld4AAAAABJRU5ErkJggg=="" width=""64"" height=""64"" style=""box-sizing: border-box; margin: 0px; padding: 0px; border: none; width: 40px; height: 40px;""></span><div class=""ntv-sponsored-disclaimer"" style=""box-sizing: border-box; margin: 0px 0px 0px -4px; padding: 0px; position: absolute; top: 4px;""><a target=""_blank"" title=""How Fast and Reliable WiFi Can Improve Your Customerâ??s Experience"" rel=""nofollow"" class=""  ntv_link777074-148399"" style=""b
ox-sizing: border-box; margin: 0px; padding: 5px 7px; color: rgb(255, 255, 255); text-decoration: none; cursor: pointer; background-color: rgb(166, 105, 52); font-size: 11px; line-height: 11px; float: left;""><span class="""" style=""box-sizing: border-box; margin: 0px; padding: 0px;"">Sponsored Content</span><span> </span><span class=""ntv-sponsored-question"" style=""box-sizing: border-box; margin: 0px 0px 0px 3px; padding: 2px 4px 1px; border: 0.21em solid rgb(255, 255, 255); border-radius: 100%; text-align: center; font-size: 9px; line-height: 11px; font-weight: bold;"">?</span></a></div><h2 class=""story"" style=""box-sizing: border-box; margin: 0px; padding: 4px 110px 4px 130px; font-weight: bold; font-size: 1rem; color: white; line-height: 1.5rem; background-color: rgb(109, 70, 52); font-family: arial, serif; border-radius: 0px; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial; background-image: n
one !important;""><span style=""box-sizing: border-box; margin: 0px; padding: 0px;""><a target=""_blank"" rel=""nofollow"" href=""https://a.slashdotmedia.com/www/delivery/ck.php?oaparams=2__bannerid=22795__zoneid=18799__cb=46a9e41a80__oadest=https%3A%2F%2Fshop.sherweb.com%2FResellerProgram%2F%3Futm_source%3DSlashdot%26utm_medium%3DLeadgen%26utm_content%3DStandardNEL%26utm_campaign%3DO365-OB365Bundle"" title=""Take Advantage of O365 + Online Backup Bundles"" style=""box-sizing: border-box; margin: 0px; padding: 0px; color: rgb(255, 255, 255); text-decoration: none; cursor: pointer;"">Take Advantage of O365 + Online Backup Bundles</a></span></h2><div class=""details"" style=""box-sizing: border-box; margin: 0px; padding: 5px 20px; color: rgb(77, 77, 77); font-size: 0.85em; line-height: 1.4rem; background: rgb(242, 242, 242);""><span class=""story-details"" style=""box-sizing: border-box; margin: 0px 3px 0px 0px; padding: 9px 0px; border-right: 1px solid rgb(222, 222, 222);""> </span><span class=""story-byline"" style=""box-sizing: bord
er-box; margin: 0px 20px 0px 0px; padding: 0px 0px 0px 10px; display: inline; position: absolute; line-height: 1.4rem; height: 20px; overflow: hidden !important;"">Posted by Slashdot</span></div></header><div class=""body ntv-preview-img-wrapper"" style=""box-sizing: border-box; margin: 0px; padding: 0px;""><div class=""ntv-preview-img-wrapper"" style=""box-sizing: border-box; margin: 0px !important; padding: 0px !important; border: 0px !important; position: relative !important;""><img class=""nel-image"" width=""120"" src=""data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAHgAAAB4CAIAAAC2BqGFAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAyNpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuNi1jMTQwIDc5LjE2MDQ1MSwgMjAxNy8wNS8wNi0wMTowODoyMSAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbn
M6eG1wPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RSZWY9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZVJlZiMiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENDIChNYWNpbnRvc2gpIiB4bXBNTTpJbnN0YW5jZUlEPSJ4bXAuaWlkOjA4RjRFRDc5M0E4OTExRThBQTkzODEzRjU3QjE3MTBFIiB4bXBNTTpEb2N1bWVudElEPSJ4bXAuZGlkOjA4RjRFRDdBM0E4OTExRThBQTkzODEzRjU3QjE3MTBFIj4gPHhtcE1NOkRlcml2ZWRGcm9tIHN0UmVmOmluc3RhbmNlSUQ9InhtcC5paWQ6MDhGNEVENzczQTg5MTFFOEFBOTM4MTNGNTdCMTcxMEUiIHN0UmVmOmRvY3VtZW50SUQ9InhtcC5kaWQ6MDhGNEVENzgzQTg5MTFFOEFBOTM4MTNGNTdCMTcxMEUiLz4gPC9yZGY6RGVzY3JpcHRpb24+IDwvcmRmOlJERj4gPC94OnhtcG1ldGE+IDw/eHBhY2tldCBlbmQ9InIiPz6Lve0QAAAjQElEQVR42ux9B5gc1ZVu3XsrdpzuCT15RhM0CqOAIgKMQRJRpMWAF2yw116Cn/HybOOIv29xDgus03MAs7vgD2OMbRDBoAUWgQUSQhLKOY8md/d0rK5837lVPaNBwjCjnQFLmitpVFNdXV3117n/+c+5595GlFJuoo1/wxMQTAA9AfREmwB6AugJoCfaBNATQE+0CaAngJ4AeqJNAD0B9ESbAPrvtPEjPM7LpiKEvF8ty8zn8tRxZJ9PlmV4deilY971jvtH8uop1tBI8tHUbQCKruuqWsjlcgB0oVAIh0KE523bdhwHDhMEAUAnhMDBGGPRbcO
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"" style=""box-sizing: border-box; margin: 0px 10px 10px; padding: 0px; border: none; float: left;""></div><div class=""p"" style=""box-sizing: border-box; margin: 20px; padding: 0px; position: relative;""><strong style=""box-sizing: border-box; margin: 0px; padding: 0px;"">The Perfect Small Business Offer That Packages Productivity, Disaster Recovery &amp; Security Under One Price Tag. Join SherWeb to Start Reselling.</strong><span> </span><br style=""box-sizing: border-box; margin: 0px; padding: 0px;"">Add more value to your Office 365 offers with Online Backup. The pro
gram will back up servers, desktops, virtual machines and smart devices, plus popular applications and Office 365 data. Online Backup comes with the Active Protection feature embedded, making it possible to stop any ransomware attack and restore encrypted files in a blink. It also provides a web-based intuitive console that eases backup and disaster recovery management.<span> </span><br style=""box-sizing: border-box; margin: 0px; padding: 0px;""><a target=""_blank"" rel=""nofollow"" class=""nel-button"" href=""https://a.slashdotmedia.com/www/delivery/ck.php?oaparams=2__bannerid=22795__zoneid=18799__cb=46a9e41a80__oadest=https%3A%2F%2Fshop.sherweb.com%2FResellerProgram%2F%3Futm_source%3DSlashdot%26utm_medium%3DLeadgen%26utm_content%3DStandardNEL%26utm_campaign%3DO365-OB365Bundle"" title=""Take Advantage of O365 + Online Backup Bundles"" style=""box-sizing: border-box; margin: 10px 0px 0px; padding: 5px 12px; color: rgb(255, 255, 255); text-decoration: underline; cursor: pointer; background: rgb(52, 170, 85); display: table
; border: 0px;"">Sign up NOW, it’s FREE</a></div><div style=""box-sizing: border-box; margin: 0px; padding: 0px;""><aside class=""novote"" style=""box-sizing: border-box; margin: 0px; padding: 0px;""></aside></div></div></article><article id=""firehose-99902227"" data-fhid=""99902227"" data-fhtype=""story"" class=""fhitem fhitem-story article usermode thumbs grid_24"" style=""box-sizing: border-box; margin: 0px 0.53125px 10px; padding: 0px; width: 540.906px; float: left; display: block; position: relative; color: rgb(54, 54, 54); font-family: Arial, sans-serif; font-size: 13px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial;""><header style=""box-sizing: border-box; margin: 0px; padding: 0px; display: blo
ck;""><span class=""topic"" id=""topic-99902227"" style=""box-sizing: border-box; margin: 0px; padding: 0px; overflow: hidden; height: 40px; width: auto; position: absolute; top: 12px; right: 70px; max-width: 100px;""><a href=""https://slashdot.org/index2.pl?fhfilter=ai"" onclick=""return addfhfilter('ai');"" style=""box-sizing: border-box; margin: 0px; padding: 0px; color: rgb(0, 47, 47); text-decoration: none; cursor: pointer;""><img src=""https://a.fsdn.com/sd/topics/ai_64.png"" width=""64"" height=""64"" alt=""AI"" title=""AI"" style=""box-sizing: border-box; margin: 0px; padding: 0px; border: none; width: 40px; height: 40px;""></a></span><h2 class=""story"" style=""box-sizing: border-box; margin: 0px; padding: 4px 155px 4px 10px; font-weight: bold; font-size: 1rem; color: white; line-height: 1.5rem; background-color: rgb(1, 103, 101); font-family: arial, serif;""><span id=""title-99902227"" class=""story-title"" style=""box-sizing: border-box; margin: 0px; padding: 0px; left: 15px; position: relative; color: white;""><a onclick=""return tog
gle_fh_body_wrap_return(this);"" href=""https://tech.slashdot.org/story/18/04/26/2246239/tesla-autopilot-crisis-deepens-with-loss-of-third-autopilot-boss-in-18-months"" style=""box-sizing: border-box; margin: 0px; padding: 0px; color: white; text-decoration: none; cursor: pointer;"">Tesla Autopilot Crisis Deepens With</a></span></h2></header></article><!--EndFragment-->
</body>
</html>

";

    }
}
