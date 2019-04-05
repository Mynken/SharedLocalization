using SharedLocalization.Common;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SharedLocalization.Helpers.Localization
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return "";

            return new DictionaryManager().Translate(Text);
        }
    }
}
