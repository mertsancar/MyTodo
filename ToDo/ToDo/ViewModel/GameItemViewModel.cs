using System;
using System.Collections.Generic;
using System.Text;
using ToDo.Model;
using ToDo.Services;
using ToDo.View;
using Xamarin.Forms;

namespace ToDo.ViewModel
{
    public class GameItemViewModel : ContentPage
    {

        public Page Page { get; set; }

        public string TitleName
        {
            get { return CurrentItem.ItemListName; }
            set
            {
                CurrentItem.ItemListName = value;
                OnPropertyChanged(nameof(TitleName));
            }
        }
        public string ItemName
        {
            get { return CurrentItem.Name; }
            set
            {
                CurrentItem.Name = value;
                OnPropertyChanged(nameof(ItemName));
            }
        }
        public string AssignTo
        {
            get { return CurrentItem.AssignTo; }
            set
            {
                CurrentItem.AssignTo = value;
                OnPropertyChanged(nameof(AssignTo));
            }
        }
        public bool IsDone
        {
            get { return CurrentItem.IsDone; }
            set
            {
                CurrentItem.IsDone = value;
                OnPropertyChanged(nameof(IsDone));
            }
        }
        public string NotesBox
        {
            get { return CurrentItem.Notes; }
            set
            {
                CurrentItem.Notes = value;
                OnPropertyChanged(nameof(NotesBox));
            }
        }
        //public string GameGenre
        //{
        //    get { return CurrentItem.GameGenre.ToString(); }
        //    set
        //    {
        //        CurrentItem.GameGenre = (GameGenre)Enum.Parse(typeof(GameGenre), value, true);
        //        OnPropertyChanged(nameof(GameGenre));
        //    }
        //}
        //public string GamePlatform
        //{
        //    get { return CurrentItem.GamePlatform.ToString(); }
        //    set
        //    {
        //        CurrentItem.GamePlatform = (GamePlatform)Enum.Parse(typeof(GamePlatform), value, true);
        //        OnPropertyChanged(nameof(GamePlatform));
        //    }
        //}

        public Command GetItemInfoCommand { get; set; }
        public Command AssignToCommand { get; set; }
        public Command GamePlatformCommand { get; set; }
        public Command GameGenreCommand { get; set; }
        public Command ClickedDoneCommand { get; set; }
        public Command ClickedDeleteCommand { get; set; }
        public Command ClickedSaveChangesCommand { get; set; }

        public GameItemViewModel(Page page)
        {
            Page = page;
            AssignToCommand = new Command(AssignToPopup);
            //GamePlatformCommand = new Command(GamePlatformPopup);
            //GameGenreCommand = new Command(GameGenrePopup);
            ClickedDoneCommand = new Command(ClickedDone);
            ClickedDeleteCommand = new Command(ClickedDelete);
            ClickedSaveChangesCommand = new Command(ClickedSaveChanges);
            GetItemInfo();
        }

        public void GetItemInfo()
        {
            TitleName = CurrentItem.ItemListName;
            ItemName = CurrentItem.Name;
            AssignTo = CurrentItem.AssignTo;
            NotesBox = CurrentItem.Notes;
            //GameGenre = CurrentItem.GameGenre.ToString();
            //GamePlatform = CurrentItem.GamePlatform.ToString();
        }

        public async void AssignToPopup()
        {
            AssignTo = await Page.DisplayActionSheet("Assign to...", "None", "", Enum.GetNames(typeof(TestUser)));
        }

        //public async void GamePlatformPopup()
        //{
        //    GamePlatform = await Page.DisplayActionSheet("Game platform...", "Cancel", "Ok", Enum.GetNames(typeof(GamePlatform)));
        //}

        //public async void GameGenrePopup()
        //{
        //    GameGenre = await Page.DisplayActionSheet("Game genre...", "Cancel", "Ok", Enum.GetNames(typeof(GameGenre)));
        //}

        private void ClickedDone(object sender)
        {
            Button button = sender as Button;
            if (CurrentItem.IsDone)
            {
                CurrentItem.IsDone = false;
                button.Text = "Done";
                button.BackgroundColor = Color.DarkGreen; 
                return;
            }
            CurrentItem.IsDone = true;
            button.Text = "Undone";
            button.BackgroundColor = Color.LightGreen;
        }

        private async void ClickedDelete()
        {
            bool delete = await Page.DisplayAlert("This item will be delete.", "Are you sure?", "Yes", "No");

            if (delete)
            {
                await DynamoDBTodoListService._instance.RemoveItemFromList(CurrentItem.Id);
                await Page.Navigation.PopAsync();
                await TodoListViewModel.GetItems();
            }
        }

        private async void ClickedSaveChanges()
        {
            bool update = await Page.DisplayAlert("This item will be update.", "Are you sure?", "Yes", "No");

            if (update)
            {
                //await DynamoDBTodoListService._instance.UpdateGameItem(CurrentItem.GetGameItemObject());
                await Page.Navigation.PopAsync();
                await TodoListViewModel.GetItems();
            }

            //try
            //{
            //    await DynamoDBTodoListService._instance.UpdateGameItem(CurrentItem.Instance.GetGameItemObject());
            //    await Page.DisplayAlert("Saving Changes...", "Successfully saved", "Ok");
            //    await Page.Navigation.PopAsync();
            //}
            //catch
            //{
            //    await Page.DisplayAlert("Somethings wrong...", "Please try again", "Ok");
            //}
        }
    }
}

