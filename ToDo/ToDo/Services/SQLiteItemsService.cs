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
    public class SQLiteItemsService : SQLiteService
    {

        public static async Task<Assignment> AddItem(string itemName, string itemListName, string username)
        {
            await Init();
            //var item = new Item
            //{
            //    Id = itemName + DateTime.Now.ToString("yyyyMMddHHmmssffff"),
            //    Name = itemName
            //    ItemListName = itemListName,
            //    Owner = username,
            //    Notes = " ",
            //    AssignTo = TestUser.Nisa.ToString(),
            //    IsDone = true,
            //    ItemGenre = ItemGenre.None
            //};
            var assignment = new Assignment
            {
                Id = itemName + DateTime.Now.ToString("yyyyMMddHHmmssffff"),
                Name = itemName
            };

            var id = await db.InsertAsync(assignment);

            return assignment;
        }

        public static async Task DeleteItem(int itemId)
        {
            await Init();
            var item = await db.Table<Item>().Where(x => x.Id == itemId).FirstOrDefaultAsync();
            var id = await db.DeleteAsync(item);
        }

        public static async Task UpdateItem(int itemId)
        {
            await Init();
            var item = await db.Table<Item>().Where(x => x.Id == itemId).FirstOrDefaultAsync();
            var id = await db.UpdateAsync(item);
        }

        public static async Task<List<Assignment>> GetItems(string itemList)
        {
            await Init();
            //var x = await AddItem("Item0", "My List", "Admin");
            //var items = await db.Table<Item>().Where(x => x.ItemListName == itemList).ToListAsync();
            var items = await db.Table<Assignment>().ToListAsync();
            return items;
        }

    }


}
