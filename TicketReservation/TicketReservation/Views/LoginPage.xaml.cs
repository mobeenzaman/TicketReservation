using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TicketReservation.Data;
using TicketReservation.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TicketReservation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private readonly ITickerRepository ticketRepository;
        public ICommand AddNewAccountCommand { get; private set; }
        public LoginPage()
        {
            InitializeComponent();

            InitializeAddNewAccountCommand();
        }

        public LoginPage(ITickerRepository trepo)
        {
            InitializeComponent();

            InitializeAddNewAccountCommand();

            this.ticketRepository = trepo;
        }

        /*
         * 
          private void InitializeAddNoteCommand()
            => AddNoteCommand = new Command(async () => NavigateToNoteView(new Note()));

        private async void NavigateToNoteView(Note value)
        {
            var noteView = Locator.Resolve<NoteView>();
            var noteViewModel = noteView.BindingContext as NoteViewModel;
            noteViewModel.Note = value;

            await Navigation.PushAsync(noteView);
        }
         */

        private void InitializeAddNewAccountCommand()
            => AddNewAccountCommand = new Command(async () => RegisterNewAccountCommand());

        private async void RegisterNewAccountCommand()
        {
            var userAccount = Locator.Resolve<UserAccount>();
            await Navigation.PushAsync(userAccount);
        }

        private async void RegisterNewUserCommand1(object s, EventArgs e)
        {
            Console.WriteLine("RegisterNewUserCommand1 ::Here");

            var userAccount = Locator.Resolve<UserAccount>();
            await Navigation.PushAsync(userAccount);
            //await Navigation.PushAsync(new UserAccount());
        }
        

        async void onlogin(object s, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(loginName.Text) && !string.IsNullOrWhiteSpace(loginPassword.Text))
                {
                    //UserInfo user = await DependencyService.Get<ITickerRepository>().GetUserAthentication(loginName.Text, loginPassword.Text);

                    UserInfo user = await ticketRepository.GetUserAthentication(loginName.Text, loginPassword.Text);
                    //UserInfo user = await DependencyService. .updateUserValidation(loginName.Text, loginPassword.Text);
                    if (user != null && !string.IsNullOrWhiteSpace(user.PersonEmail))
                    {
                        App._LogedUser = user;
                        App._UserLogedIn = "User ::" + user.PersonName;
                        App._LogedInUserID = user.UserId;

                        await DisplayAlert("Info", user.PersonName + " logged in successfully.", "OK");

                        loginName.Text = "NAME";
                        loginPassword.Text = "PASSWORD";

                        OpenMainPage();
                    }
                    else
                    {
                        await DisplayAlert("Toast", "Email or password Icorrect", "OK");
                    }

                }
                else
                {
                    await DisplayAlert("Toast", "Enter Email and Password.", "OK");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private async void OpenMainPage()
        {
            var mainPage = Locator.Resolve<AppMainPage>();

            App.Current.MainPage = new NavigationPage(mainPage);
            //await new NavigationPage(mainPage);
        }

    }
}