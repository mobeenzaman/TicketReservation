using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TicketReservation.Data;
using TicketReservation.Models;

namespace TicketReservation.Data
{
    public class SqliteTicketRepository : ITickerRepository
    {
        private SQLiteAsyncConnection _connection;

        public async Task Initialize()
        {
            if (_connection != null) return;

            _connection = new SQLiteAsyncConnection(
                Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, "airticketdb.db3"));

            await _connection.CreateTableAsync<UserInfo>();
            await _connection.CreateTableAsync<TicketInfo>();
        }

        //user authentication
        /*
        public async UserData GetUserAthentication(string userEmail, string usersPass)
        {
            return await _connection.Table<UserData>().FirstOrDefaultAsync(x => x.UserName == userEmail && x.UserPassword == usersPass);
            /*
            lock (locker)
            {
                return database.Table<UserInfo>().FirstOrDefault(x => x.UserName == userEmail && x.UserPassword == usersPass);
            }
            
        }
    */

        //Add New User 
        public async Task AddOrUpdateUserData(UserInfo user)
        {
            if (user.UserId != 0)
            {
                _ = await _connection.UpdateAsync(user);
            }
            else
            {
                _ = await _connection.InsertAsync(user);
            }
        }


        //user authentication
        public Task<UserInfo> GetUserAthentication(string userName, string usersPass)
            => _connection.Table<UserInfo>().FirstOrDefaultAsync(x => x.PersonName == userName && x.PersonPassword == usersPass);

        //Get user from database
        public Task<List<UserInfo>> GetUsers()
        => _connection.Table<UserInfo>().ToListAsync();


        public Task<int> isUserExist()
        {
            return _connection.Table<UserInfo>().CountAsync();
           // throw new NotImplementedException();
        }

        //Add or update new ticket
        public async Task AddOrUpdateNewTicket(TicketInfo ticket)
        {
            if (ticket.TicketID != 0)
            {
                _ = await _connection.UpdateAsync(ticket);
            }
            else
            {
                _ = await _connection.InsertAsync(ticket);
            }
        }

        //Get user from database
        public Task<List<TicketInfo>> GetAllTickets()
        => _connection.Table<TicketInfo>().ToListAsync();

        //Get tickets against single user
        public Task<UserInfo> GetSingleUserTickets(int userId)
            => _connection.Table<UserInfo>().FirstOrDefaultAsync(x => x.UserId == userId);
    }
}
