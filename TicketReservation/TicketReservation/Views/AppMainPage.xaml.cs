using TicketReservation.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TicketReservation.Data;
using System.Collections.ObjectModel;
using TicketReservation.Models;

namespace TicketReservation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppMainPage : ContentPage
    {
        private readonly ITickerRepository ticketRepository;

        public ObservableCollection<TicketInfo> TicketModelPairs { get; set; }
        public AppMainPage()
        {
            InitializeComponent();

            TicketModelPairs = new ObservableCollection<TicketInfo>();

            checkUserLogedInStatus();

            if (userLogedInStatus)
            {
                lblUserName.Text = App._UserLogedIn;
                logOutBtn.IsVisible = true;
            }
            else
            {
                logOutBtn.IsVisible = false;
            }
        }

        private bool userLogedInStatus = false;

        private void checkUserLogedInStatus()
        {
            if (App._LogedInUserID != null && !string.IsNullOrWhiteSpace(App._UserLogedIn))
            {
                userLogedInStatus = true;
            }
        }

        public AppMainPage(ITickerRepository trepo)
        {
            InitializeComponent();
            TicketModelPairs = new ObservableCollection<TicketInfo>();
            this.ticketRepository = trepo;
            checkUserLogedInStatus();
            if (userLogedInStatus)
            {
                lblUserName.Text = App._UserLogedIn;
                logOutBtn.IsVisible = true;
                lblUserName.IsVisible = true;
            }
            else
            {
                lblUserName.IsVisible = false;
                logOutBtn.IsVisible = false;
            }
            
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                if (userLogedInStatus)
                {
                    int userID = App._LogedInUserID;
                    ticketLists.ItemsSource = await ticketRepository.GetAllTickets();
                    //ticketLists.ItemsSource = await ticketRepository.GetSingleUserTickets(App._LogedUser.UserId);

                    //ticketLists.ItemsSource = (System.Collections.IEnumerable)await ticketRepository.GetSingleUserTickets(App._LogedUser.UserId);
                }
                else
                {
                    ticketLists.ItemsSource = await ticketRepository.GetAllTickets();
                }
            }
            catch(Exception er)
            {

            }
            
            
            //TicketModelPairs = await ticketRepository.GetAllTickets();
        }


        public async void OnNewTicketClicked(object s, EventArgs e)
        {
            if(userLogedInStatus)
            {
                var makeBooking = Locator.Resolve<MakeBooking>();
                await Navigation.PushAsync(makeBooking);
            }
            else
            {
                var loginPage = Locator.Resolve<LoginPage>();
                await Navigation.PushAsync(loginPage);
            }
            
        } 
        
        public async void OnLogoutClicked(object s, EventArgs e)
        {
            if(userLogedInStatus)
            {
                App._LogedInUserID = -1;
                App._UserLogedIn = null;
                App._LogedUser = null;

                var mainPage = Locator.Resolve<AppMainPage>();
                App.Current.MainPage = new NavigationPage(mainPage);
            }
            
        }


       
    }
}