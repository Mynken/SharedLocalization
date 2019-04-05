using System;
using System.Globalization;
using Xamarin.Forms;

namespace SharedLocalization
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void LanguagePicker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedLanguage = languagePicker.SelectedItem.ToString();
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(selectedLanguage);

            InitializeComponent();
        }
    }
}
