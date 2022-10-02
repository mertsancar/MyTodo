using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ToDo.Model;
using Xamarin.Essentials;

namespace ToDo.Services
{
    public class SQLiteService
    {
        public static SQLiteAsyncConnection db;
        public static string databasePath;
        public static async Task Init()
        {
            if (db != null)
            {
                return;
            }

            databasePath = Path.Combine(FileSystem.AppDataDirectory, "TodoApp.db");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<ItemList>();
            await db.CreateTableAsync<Item>();
        }
    }
}
