using TicketReservation.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TicketReservation.Data
{
    public interface ITickerRepository
    {
        Task Initialize();

        Task<List<UserInfo>> GetUsers();

        Task AddOrUpdateUserData(UserInfo user);

        Task<int> isUserExist();

        Task<UserInfo> GetUserAthentication(string userName, string usersPass);

        Task AddOrUpdateNewTicket(TicketInfo ticket);

        Task<List<TicketInfo>> GetAllTickets();

        Task<UserInfo> GetSingleUserTickets(int userId);

    }
}
