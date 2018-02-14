
namespace Lands.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.Net.Mail;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Views;

    public class LoginViewModel : BaseViewModel
    {

        #region Attributes

        private bool isRunning;
        private bool isEnabled;
        private string password;
        private string email;

        #endregion

        #region Properties
        public string Email
        {
            get { return this.email; }
            set { SetValue(ref this.email, value); }
        }

        public string Password
        {
            get { return this.password; }
            set { SetValue(ref this.password, value); }
        }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }

        public bool IsRemembered
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public LoginViewModel()
        {
            this.IsRemembered = true;
            this.IsEnabled = true;
            this.Email = "juank-nac@hotmail.com";
            this.Password = "12345";
        }

        #endregion

        #region Commands
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }

    
        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter an email.",
                    "Accept");
                return;
            }
            else
            {
                try
                {
                    MailAddress m = new MailAddress(this.Email);

                }
                catch (FormatException)
                {
                    await Application.Current.MainPage.DisplayAlert(
                     "Error",
                     "It must be email type ",
                     "Accept");
                    return;
                }
            }


            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter a password.",
                    "Accept");

                await Application.Current.MainPage.Navigation.PopAsync();

                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            if (this.Email != "Juank-nac@hotmail.com" || this.Password != "12345")
            {
                this.IsRunning = false;
                this.IsEnabled = true;

                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Email or password incorrect",
                    "Accept");

                this.Password = string.Empty;

                await Application.Current.MainPage.Navigation.PopAsync();

                return;
            }
            else
            {
                this.IsRunning = true;
                this.IsEnabled = true;

                this.Email = String.Empty;
                this.Password = String.Empty;

                MainViewModel.GetInstance().Lands = new LandsViewModel();
                await Application.Current.MainPage.Navigation.PushAsync(new LandsPage());

            }
        }
        #endregion
    }
}