namespace Lands
{
    using Xamarin.Forms;
    using Views;
    using Helpers;
    using ViewModels;
    using Services;
    using Models;
    
    public partial class App : Application
	{
        #region Services

        DataServices dataServices;

        #endregion

        #region Properties

        public static NavigationPage Navigator
        { 
            get;
            internal set; 
        }

        public static MasterPage Master
        {
            get;
            internal set;
        }

        #endregion

        #region Constructors
        public App ()
		{
			InitializeComponent();

            if (string.IsNullOrEmpty(Settings.Token))
            {
                this.MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                var mainViewModel = MainViewModel.GetInstance();
                mainViewModel.Token = Settings.Token;
                mainViewModel.TokenType = Settings.TokenType;

                this.dataServices = new DataServices();
                var user = this.dataServices.First<UserLocal>(false);
                mainViewModel.User = user;
                mainViewModel.Lands = new LandsViewModel();
                Application.Current.MainPage = new MasterPage();
            }

            
		}
        #endregion

        #region Methods

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

        #endregion
    }
}
