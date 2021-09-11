using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinTest.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        void OnButtonClicked(object sender, EventArgs e)
        {
            testLabel.Text = "Button Clicked!";
        }

        void OnEntryButtonClicked(object sender, EventArgs e)
        {
            entryLabel.Text = testEntry.Text;
        }

        void OnEditorButtonClicked(object sender, EventArgs e)
        {
            editorLabel.Text = testEditor.Text;
        }
    }
}