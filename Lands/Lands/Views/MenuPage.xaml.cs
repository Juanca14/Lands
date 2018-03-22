namespace Lands.Views
{
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using ViewModels;

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MenuPage : ContentPage
	{
		public MenuPage ()
		{
			InitializeComponent ();
        }

        protected override void OnAppearing()
		{
			base.OnAppearing();
            MainViewModel.GetInstance().RefreshUser();
		}
}
}