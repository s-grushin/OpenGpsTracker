using OpenGpsTracker.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace OpenGpsTracker.Repository
{
    public class UserRepository
    {
        SQLiteConnection database;

        public UserRepository(string filename)
        {
            string databasePath = DependencyService.Get<ISQLite>().GetDatabasePath(filename);
            database = new SQLiteConnection(databasePath);
            database.CreateTable<User>();
        }

        public IEnumerable<User> GetUsers()
        {
            return (from i in database.Table<User>() select i).ToList();
        }

        public User GetUserByID(int id)
        {
            return database.Get<User>(id);
        }

        public User GetUserByLogin(string login)
        {
            return database.Get<User>(login);
        }


        public int SaveUser(User user)
        {
            if (user.Id != 0)
            {
                database.Update(user);
                return user.Id;
            }
            else
            {
                return database.Insert(user);
            }
        }


    }
}
