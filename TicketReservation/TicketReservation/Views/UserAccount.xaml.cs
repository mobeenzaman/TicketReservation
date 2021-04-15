using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketReservation.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TicketReservation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserAccount : ContentPage
    {
        private readonly ITickerRepository ticketRepository;
        public UserAccount()
        {
            InitializeComponent();
        }

        
        public UserAccount(ITickerRepository tRepo)
        {
            InitializeComponent();
            this.ticketRepository = tRepo;
        }
        

        async void OnButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(entryEmail.Text) && !string.IsNullOrWhiteSpace(entryPassword.Text))
            {
                bool isAdmin = false;
                if (!(await IsUserRecordExistAsync()))
                {
                    isAdmin = true;
                }

                _ = ticketRepository.AddOrUpdateUserData(new Models.UserInfo
                {
                    PersonName = entryName.Text,
                    PersonEmail = entryEmail.Text,
                    PersonPassword = entryPassword.Text,
                    PersonContact = entryContact.Text,
                    IsAdmin = isAdmin

                });

                /*
                await DependencyService.Get<ITickerRepository>().AddOrUpdateUserData(new Models.UserInfo
                {
                    PersonName = entryName.Text,
                    PersonEmail = entryEmail.Text,
                    PersonPassword = entryPassword.Text,
                    PersonContact = entryContact.Text,
                    IsAdmin = isAdmin

                }) ;
                */
                
                entryName.Text = entryPassword.Text = entryContact.Text = string.Empty;
                //listView.ItemsSource = await App.Database.GetPeopleAsync();

                //save message
                await DisplayAlert("Info", "Account Created successfully.", "OK");

                //open login Page
                // await Navigation.PushAsync(new UserLogin());
                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }

        private async Task<bool> IsUserRecordExistAsync()
        {
            try
            {
                //int userDataCont = await DependencyService.Get<ITickerRepository>().isUserExist();

                Task<int> userDataCont = ticketRepository.isUserExist();

                if (userDataCont.Equals(0))
                {
                    return true;
                }
                else {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }

    }
}