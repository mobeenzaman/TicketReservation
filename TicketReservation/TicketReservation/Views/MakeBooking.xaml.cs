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
    public partial class MakeBooking : ContentPage
    {
        private readonly ITickerRepository ticketRepository;
        public MakeBooking()
        {
            InitializeComponent();

            ticketTime.Time = DateTime.Now.TimeOfDay;

            ticketDate.SetValue(DatePicker.MinimumDateProperty, DateTime.Now.AddDays(1));
            ticketDate.SetValue(DatePicker.MaximumDateProperty, DateTime.Now.AddDays(180));
        }

        public MakeBooking(ITickerRepository tRepo)
        {
            InitializeComponent();

            ticketTime.Time = DateTime.Now.TimeOfDay;

            ticketDate.SetValue(DatePicker.MinimumDateProperty, DateTime.Now.AddDays(1));
            ticketDate.SetValue(DatePicker.MaximumDateProperty, DateTime.Now.AddDays(180));
            this.ticketRepository = tRepo;
        }


        async void OnBookingBtnClicked(object sender, EventArgs args)
        {
            //fields validation
            if (await ValidateFields())
            {
                try
                {
                    _ = ticketRepository.AddOrUpdateNewTicket(new Models.TicketInfo
                    {
                        DepCity = depCity.SelectedItem.ToString(),
                        DestCity = destCity.SelectedItem.ToString(),
                        FlightDate = ticketDate.Date,
                        FlightTime = ticketTime.Time,
                        UserID = App._LogedInUserID
                    });

                    // reset fields
                    //de
                    ticketDate.SetValue(DatePicker.MinimumDateProperty, DateTime.Now);
                    ticketDate.SetValue(DatePicker.MaximumDateProperty, DateTime.Now.AddDays(180));
                    ticketTime.Time = DateTime.Now.TimeOfDay;

                    //save message
                    await DisplayAlert("Alert", "Ticket Bocked Successfully.", "OK");

                    //Main Page
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
                catch(Exception ee)
                {
                    Console.WriteLine(ee.ToString());
                }
                             
            }

        }


        void OnStartDateSelected(object sender, EventArgs args)
        {

        }

        //OnTimePickerPropertyChanged
        void OnTimePickerPropertyChanged(object sender, EventArgs args)
        {

        }

        private string groundName;
        void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            Picker picker = sender as Picker;
            var selectedItem = picker.SelectedItem;
            groundName = selectedItem.ToString();
        }



        private async Task<bool> ValidateFields()
        {
            bool status = true;
            if(depCity.SelectedIndex == 0)
            {
                status = false;
            }

            if(destCity.SelectedIndex == 0)
            {
                status = false;
            }

            if (!status)
            {
                await DisplayAlert("Error", "Please fill empty feilds.", "OK");
            }

            return status;
        }

    }
}