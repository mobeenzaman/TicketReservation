using System;
using TicketReservation.Data;
using TicketReservation.Models;
using TicketReservation.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TicketReservation
{
    public partial class App : Application
    {
        public static string _UserLogedIn { get; set; }
        public static int _LogedInUserID { get; set; }

        public static UserInfo _LogedUser { get; set; }
        public App()
        {
            InitializeComponent();

            Locator.Initialize();

            InitializeTicketRepository();

            InitializeAppMainPage();

            //MainPage = new NavigationPage(new LoginPage());
        }

        private void InitializeAppMainPage()
        {
            MainPage = new NavigationPage(Locator.Resolve<AppMainPage>());
        }

        private static void InitializeTicketRepository()
        {
            ITickerRepository repository = Locator.Resolve<ITickerRepository>();
            repository.Initialize();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
