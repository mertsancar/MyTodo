using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToDo.Model;
using ToDo.Services;
using ToDo.View;
using Xamarin.Forms;

namespace ToDo.ViewModel
{
    public class TodoListViewModel
    {
        public static Page Page { get; set; }
        //public static async Command GetItemsCommand { get; set; }
        public Command AddItemPopupCommand { get; set; }
        public Command UpdateItemsPopupCommand { get; set; }

        //public static List<Item> ItemList { get; set; }

        public TodoListViewModel(Page page)
        {
            Page = page;
            AddItemPopupCommand = new Command(AddItemPopup);
            //GetItemsCommand = new Command(GetItems);

            GetItems();
        }

        public static async Task GetItems()
        {
            TodoListPage.loadingIcon.IsVisible = true;
            TodoListPage.itemListStackLayout.IsVisible = false;

            TodoListPage.itemListStackLayout.Children.Clear();

            //AWS
            //ItemList = await DynamoDBTodoListService._instance.GetItems(CurrentToDoList.Name);

            //SQLite
            //List<Assignment> ItemList = await SQLiteItemsService.GetItems(CurrentToDoList.Name);

            List<Item> items = await SQLiteTodoListService.GetItems(CurrentToDoList.Id);

            for (int i = 0; i < items.Count; i++)
            {

                CreateItemButton(items[i]);
            }

            TodoListPage.loadingIcon.IsVisible = false;
            TodoListPage.itemListStackLayout.IsVisible = true;
        }

        public async void AddItemPopup()
        {
            string itemName = await Page.DisplayPromptAsync("Add Item", "Enter Item Name", "Ok", "Cancel", "Item Name...", 30);
            if (itemName == null || itemName == "")
            {
                return;
            }

            Item item = new Item
            {
                Id = 1,
                Name = itemName,
                ItemListName = CurrentToDoList.Name,
                IsDone = false,
                AssignTo = TestUser.Mert.ToString(),
                Owner = TestUser.Mert.ToString(),
                Notes = " ",
                ItemListId = CurrentToDoList.Id,
            };

            var itemList = await SQLiteTodoListService.AddItemtoList(CurrentToDoList.Id, item);

            CreateItemButton(item);
          
        }

        public static void CreateItemButton(Item item)
        {
            Button button = new Button
            {
                Text = item.Name,
                BindingContext = item, //Command = new Command(async () => await Page.Navigation.PushAsync(new ItemPage(item))),
                CornerRadius = 25,
                BackgroundColor = item.IsDone ? Color.Green : Color.Default
            };
            button.Command = new Command(() => ClickedItemButton(button));

            TodoListPage.itemListStackLayout.Children.Add(button);
        }

        private async static void ClickedItemButton(Button button)
        {
            await CurrentItem.SetCurrentItem((Item)button.BindingContext);

            if (button.BackgroundColor == Color.Green)
            {
                button.BackgroundColor = Color.Default;
                CurrentItem.IsDone = false;
            }
            else
            {
                button.BackgroundColor = Color.Green;
                CurrentItem.IsDone = true;
            }
            var x = await SQLiteTodoListService.UpdateItem(CurrentItem.Id, CurrentItem.IsDone);
        }



    }
}
