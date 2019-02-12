using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ActivityButton
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Handle_Clicked(object sender, EventArgs e)
        {
            this.Button.IsBusy = true;
            await Task.Delay(2000);
            this.Button.IsBusy = false;
        }
    }
}
