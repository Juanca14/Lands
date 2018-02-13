namespace Lands.ViewModels
{
    using Services;
    using Lands.Models;
    using System.Collections.ObjectModel;
    using Xamarin.Forms;
    using System.Collections.Generic;

    public class LandsViewModel : BaseViewModel
    {
        #region Services

        private ApiServices apiservices;

        #endregion

        #region Attributes

        private ObservableCollection<Lands> lands;

        #endregion

        #region Properties

        public ObservableCollection<Lands> Lands
        {
            get { return this.lands; }
            set { SetValue(ref this.lands, value); }
        }

        #endregion

        #region Constructors

        public LandsViewModel()
        {
            this.apiservices = new ApiServices();
            this.LoadLands();
        }

        #endregion

        #region Methods

        private async void LoadLands()
        {
            var response = await this.apiservices.GetList<Lands>
                (
                "http://restcountries.eu",
                "/rest",
                "/v2/all"
                );

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            var list = (List<Lands>)response.Result;

            this.lands = new ObservableCollection<Lands>(list);
        }

        #endregion

    }
}
