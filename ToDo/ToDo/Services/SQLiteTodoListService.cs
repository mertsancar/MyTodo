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
    public class SQLiteTodoListService : SQLiteService
    {

        public static async Task<ItemList> AddList(string listName)
        {
            await Init();
            var itemList = new ItemList // listName + DateTime.Now.ToString("yyyyMMddHHmmssffff")
            {
                Name = listName,
                Items = new List<Item>()
            };

            itemList.Id = await db.InsertAsync(itemList);

            return itemList;
        }

        public static async Task<bool> CheckListName(string listName)
        {
            await Init();
            var isExist = await db.Table<ItemList>().Where(x => x.Name == listName).ToListAsync();

            if (isExist.Count > 0)
            {
                return true;
            }
            return false;
        }

        public static async Task<List<ItemList>> GetLists()
        {
            await Init();
            var itemList = await db.Table<ItemList>().ToListAsync();
            return itemList;
        }

        public static async Task<ItemList> GetList(int Id)
        {
            await Init();
            var itemList = await db.Table<ItemList>().Where(x => x.Id == Id).FirstAsync();

            if (itemList.Items == null)
            {
                itemList.Items = new List<Item>();
            }

            return itemList;
        }

        public static async Task<Item> GetItem(int itemId)
        {
            await Init();
            var item = await db.Table<Item>().Where(x => x.Id == itemId).ToListAsync();
            return item[0];
        }
        public static async Task<Item> UpdateItem(int itemId, object changed)
        {
            await Init();
            //var item = await db.Table<Item>().Where(x => x.Id == itemId).FirstAsync();
            var item = await GetItem(itemId);
            item.IsDone = (bool)changed;
            await db.UpdateAsync(item);
            return item;
        }

        public static async Task<List<Item>> GetItems(int listId)
        {
            await Init();
            var items = await db.Table<Item>().Where(x => x.ItemListId == listId).ToListAsync();
            return items;
        }

        public static async Task<ItemList> UpdateList(ItemList itemList)
        {
            await Init();
            await db.UpdateAsync(itemList);
            return itemList;
        }

        public static async Task<ItemList> AddItemtoList(int listId, Item item)
        {
            await Init();
            item.ItemListId = listId;
            item.Id =  await db.InsertAsync(item);
            var itemList = await GetList(listId);
            return itemList;
        }


    }
}
