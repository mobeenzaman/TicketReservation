using TicketReservation.Data;
using TicketReservation.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace TicketReservation
{
    public static class Locator
    {
        private static IServiceProvider serviceProvider;

        public static void Initialize()
        {
            var services = new ServiceCollection();
            services.AddSingleton<ITickerRepository, SqliteTicketRepository>();

            services.AddTransient<LoginPage>();
            services.AddTransient<UserAccount>();
            services.AddTransient<AppMainPage>();
            services.AddTransient<MakeBooking>();

            serviceProvider = services.BuildServiceProvider();
        }

        public static T Resolve<T>() => serviceProvider.GetService<T>();

    }
}
