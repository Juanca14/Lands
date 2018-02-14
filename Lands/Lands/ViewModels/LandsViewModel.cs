namespace Lands.ViewModels
{
    using Lands.Models;
    using Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Xamarin.Forms;

    public class LandsViewModel : BaseViewModel
    {

        #region Services

        private ApiServices apiservices;

        #endregion

        #region Attributes

        private ObservableCollection<Land> lands;

        #endregion

        #region Properties

        public ObservableCollection<Land> Lands
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
            var response = await this.apiservices.GetList<Land>
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

            var list = (List<Land>)response.Result;

            this.lands = new ObservableCollection<Land>(list);
        }

        #endregion
    }
}
