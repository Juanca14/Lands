namespace Lands.ViewModels
{
    using Domain;
    using Helpers;
    using Models;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class MainViewModel
    {

        #region Properties

        public string Token { get; set; }

        public string TokenType { get; set; }

        #endregion

        #region Views

        public List<Land> LandsList
        {
            get;
            set;
        }

        public LoginViewModel Login
        {
            get;
            set;
        }

        public RegisterViewModel Register
        {
            get;
            set;
        }

        public LandsViewModel Lands
        {
            get;
            set;
        }

        public LandViewModel Land
        {
            get;
            set;
        }

        public ObservableCollection<MenuItemViewModel> Menus
        {
            get;
            set;
        }

        public User User
        {
            get;
            set;
        }

        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
            this.LoadMenu();
        }
        #endregion

        #region Methods

        private void LoadMenu()
        {
            this.Menus = new ObservableCollection<MenuItemViewModel>
            {
                new MenuItemViewModel
                {
                    Icon = "ic_settings",
                    PageName = "MyProfilePage",
                    Title = Languages.MyProfile,
                },

                new MenuItemViewModel
                {
                    Icon = "ic_insert_chart",
                    PageName = "StaticsPage",
                    Title = Languages.Statics,
                },

                new MenuItemViewModel
                {
                    Icon = "ic_exit_to_app",
                    PageName = "LoginPage",
                    Title = Languages.LogOut,
                }
            };
        }

        #endregion

        #region Singleton

        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }

        #endregion
    }
}