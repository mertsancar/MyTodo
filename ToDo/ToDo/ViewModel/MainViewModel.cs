using System;
using System.Collections.Generic;
using System.Text;
using ToDo.Model;
using ToDo.Services;
using ToDo.View;
using Xamarin.Forms;

namespace ToDo.ViewModel
{
    public class MainViewModel
    {
        public Page Page { get; set; }
        public Command GetListsCommand { get; set; }
        public Command AddListCommand { get; set; }
        public Command LogoutCommand { get; set; }
        public MainViewModel(Page page)
        {
            Page = page;
            GetLists();       
            AddListCommand = new Command(AddList);
            LogoutCommand = new Command(Logout);
        }
        public async void GetLists()
        {
            MainPage.loadingIcon.IsVisible = true;
            MainPage.listsStackLayout.IsVisible = false;

            MainPage.listsStackLayout.Children.Clear();

            //AWS
            //List<ItemList> itemLists = await DynamoDBTodoListService._instance.GetLists();

            //SQLite
            List<ItemList> itemLists = await SQLiteTodoListService.GetLists();

            for (int i = 0; i < itemLists.Count; i++)
            {
                //Buton için fonksiyon oluşturma
                ItemList itemList = itemLists[i];
                //async void ClickedList()
                //{
                //    await Page.Navigation.PushAsync(new TodoListPage(itemList));
                //}
                //Command ClickedListCommand = new Command(await Page.Navigation.PushAsync(new TodoListPage(itemList)));

                //Butonu oluşturma
                Button button = new Button
                {
                    Text = itemLists[i].Name,
                    Command = new Command(async () => await Page.Navigation.PushAsync(new TodoListPage(itemList))),
                    CornerRadius = 25
                };

                MainPage.listsStackLayout.Children.Add(button);
            }

            MainPage.listsStackLayout.IsVisible = true;
            MainPage.loadingIcon.IsVisible = false;
        }

        public async void AddList()
        {
            string listName = await Page.DisplayPromptAsync("Create List", "Enter List Name", "Ok", "Cancel", "Enter your list name...", 30, Keyboard.Default, "My List");

            if (listName == null || listName == "")
            {
                return;
            }

            bool isExist = await SQLiteTodoListService.CheckListName(listName);
            if (isExist)
            {
                await Page.DisplayAlert("This list name is already using", "Please try another name", "OK");
                return;
            }

            //SQLite
            ItemList itemList = await SQLiteTodoListService.AddList(listName);

            Button button = new Button
            {
                Text = listName,
                Command = new Command(async () => await Page.Navigation.PushAsync(new TodoListPage(itemList))),
                CornerRadius = 25
            };

            MainPage.listsStackLayout.Children.Add(button);

            //AWS
            //await DynamoDBTodoListService._instance.AddList(listName);
        }

        public async void Logout()
        {
            CurrentUser.Username = null;
            Application.Current.MainPage = new LoginPage();
        }
    }
}
