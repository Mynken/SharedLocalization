# SharedLocalization
Implementation of localization in shared xamarin forms.

# WHY?

    When you want to find info about how to "do" localization in Xamarin, there are enough results from .Net Standard projects, but not from __"SharedProject"__.
    And in __"SharedProject"__ there are some problems with this topic.
    ## Problem:
    *   You cannot create .resx files in __"SharedProject"__, but you can manualy copy it to the project and then include to the project. Furthemore this .resx files won`t be copied to the Android  or IOS projects. You will get erros when trying to translate.

# Manual for simple solution
First of all we need to add a new Class Library to our solution(for example: __SharedLocalization.Common__)

Then create a folder named "Resources" where will be located our .resx files for necessary languages.

# Naming
This is really important, you must be carefull about the file names.
Every filename MUST correspond to the CultureInfo Name property in lower case (en,pl,ru...).

Example: Your default(en) .resx file has name __"AppResource"__. For the next languages you should add a prefix to the name of file:
__"AppResource.de.resx"__ - for German language.

When resx files created and filled with values you should create class which will process data and return translated strings for each language.

# Working with ResourceManager
using System.Globalization;
using System.Reflection;
using System.Resources;

```
namespace SharedLocalization.Common
{
    public class DictionaryManager
    {
        public string Translate(string key)
        {
            ResourceManager rm = new ResourceManager("SharedLocalization.Common.Resources.Resource", typeof(DictionaryManager).GetTypeInfo().Assembly);

            var currentCulture = CultureInfo.DefaultThreadCurrentCulture;
            return rm.GetString(key, currentCulture);
        }
    }
}
```

First parametr of new ResourceManager() is path(localization) to your default .resx file.
As settings of culture is setted globally for the solution, when we will change CultureInfo in __"SharedProject"__ in our __"Common"__ project we will see chages too.

# Android, IOS Projects
Then add reference from Android and IOS to our "Common" project.

# Shared Project
After that in __"SharedProject"__ we should create class, which will help translate our keys. 
```
-Shared project
 |
 |-Helpers
     |- Localization
        |- TranslateExtension.cs
 |- Views
 |- etc.
```

*Code of TranslateExtension.cs*

```
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
```
*MainPage.xaml*

Then in our MainPage.xaml we can "call" this class, lets name it "i18n":

```
    <ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:i18n="clr-namespace:SharedLocalization.Helpers.Localization;"
             x:Class="SharedLocalization.MainPage">
```

Synatax for translate is quite simple:
```
    {i18n:Translate HelloWorld}
```

1) our attribute;
2) Property for transfer data;
3) key in our .resx file;

*MainPage.xaml.cs*

And last add event listner to change language:

```
 private void LanguagePicker_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        string selectedLanguage = languagePicker.SelectedItem.ToString();
        CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(selectedLanguage);

        InitializeComponent();
    }
```
###### NOTE: We call InitializeComponent() one more time to refresh the current view.
