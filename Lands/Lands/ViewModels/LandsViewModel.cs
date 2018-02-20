namespace Lands.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Lands.Models;
    using Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class LandsViewModel : BaseViewModel
    {

        #region Services

        private ApiServices apiservices;

        #endregion

        #region Attributes

        private List<Land> landsList;
        private ObservableCollection<LandItemViewModel> lands;
        private bool isRefreshing;
        private string filter;

        #endregion

        #region Properties

        public ObservableCollection<LandItemViewModel> Lands
        {
            get { return this.lands; }
            set { SetValue(ref this.lands, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }

        public string  Filter
        {
            get { return this.filter; }

            set
               {
                    SetValue(ref this.filter, value);
                    Search();
               }
        }

        #endregion

        #region Constructors

        public LandsViewModel()
        {
            this.apiservices = new ApiServices();
            this.LoadLands();
        }

        #endregion

        #region ICommand

        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadLands);
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(Search);
            }
        }


        #endregion

        #region Methods

        private void Search()
        {

            this.IsRefreshing = true;

            if (string.IsNullOrEmpty(this.Filter))
            {
                this.Lands = new ObservableCollection<LandItemViewModel>
                    (this.ToLandItemViewModel());
            }
            else
            {
                this.Lands = new ObservableCollection<LandItemViewModel>(
                    this.ToLandItemViewModel().
                    Where(l => l.Name.ToLower().Contains(this.Filter.ToLower()) || 
                    l.Capital.ToLower().Contains(this.Filter.ToLower())));
            }

            this.IsRefreshing = false;

        }

        private async void LoadLands()
        {
            this.IsRefreshing = true;
            var connection = await this.apiservices.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    connection.Message,
                    "Accept");

                await Application.Current.MainPage.Navigation.PopAsync();

                return;
            }

            var response = await this.apiservices.GetList<Land>
                (
                "http://restcountries.eu",
                "/rest",
                "/v2/all"
                );

            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                await Application.Current.MainPage.Navigation.PopAsync();

                return;
            }

            landsList = (List<Land>)response.Result;
            this.Lands = new ObservableCollection<LandItemViewModel>
                (this.ToLandItemViewModel());
            this.IsRefreshing = false;

        }

        private List<LandItemViewModel> ToLandItemViewModel()
        {
            return this.landsList.Select(l => new LandItemViewModel
            {
                Alpha2Code = l.Alpha2Code,
                Alpha3Code = l.Alpha3Code,
                AltSpellings = l.AltSpellings,
                Area = l.Area,
                Borders = l.Borders,
                CallingCodes = l.CallingCodes,
                Capital = l.Capital,
                Cioc = l.Cioc,
                Currencies = l.Currencies,
                Demonym = l.Demonym,
                Flag = l.Flag,
                Gini = l.Gini,
                Languages = l.Languages,
                Latlng = l.Latlng,
                Name = l.Name,
                NativeName = l.NativeName,
                NumericCode = l.NumericCode,
                Population = l.Population,
                Region = l.Region,
                RegionalBlocs = l.RegionalBlocs,
                Subregion = l.Subregion,
                Timezones = l.Timezones,
                TopLevelDomain = l.TopLevelDomain,
                Translations = l.Translations,
            }).ToList();
        }

        #endregion
    }
}
