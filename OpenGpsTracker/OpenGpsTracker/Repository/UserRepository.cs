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
            database.CreateTable<Tracker>();
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
            string sql = "SELECT * FROM Users WHERE login = ?";
            User user = database.Query<User>(sql, login).FirstOrDefault<User>();
            return user;
        }

        public User GetCurrentUser()
        {
            string sqluser = "SELECT * FROM Users WHERE Current = ?";
            User user = database.Query<User>(sqluser, true).FirstOrDefault<User>();

            if (user != null)
            {
                string sqltracker = "SELECT * FROM Trackers WHERE UserID = ?";
                user.Trackers = database.Query<Tracker>(sqltracker, user.Id);
            }           

            return user;
        }

        public void SaveUser(User user)
        {            

            if (user.Id != 0)
            {
                database.Update(user);
            }
            else
            {
                //Set users to inactive
                database.Execute("UPDATE Users SET Current = ?", false);

                //add new user
                database.Insert(user);
                //get id of new user
                int lastID = database.ExecuteScalar<int>("SELECT MAX(ID) FROM Users");
                //fill user id in all avaliable user trackers
                user.Trackers.ForEach(tr => tr.UserID = lastID);
                //add trackers to database
                database.InsertAll(user.Trackers);
                
            }

        }


    }
}
