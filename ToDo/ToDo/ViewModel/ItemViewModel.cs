using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using ToDo.Model;
using ToDo.Services;
using ToDo.View;
using Xamarin.Forms;

namespace ToDo.ViewModel
{
    public class ItemViewModel : ContentPage
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

        public Command GetItemInfoCommand { get; set; }
        public Command AssignToCommand { get; set; }
        public Command GamePlatformCommand { get; set; }
        public Command GameGenreCommand { get; set; }
        public Command ClickedDoneCommand { get; set; }
        public Command ClickedDeleteCommand { get; set; }
        public Command ClickedSaveChangesCommand { get; set; }

        public ItemViewModel(Page page)
        {
            Page = page;
            AssignToCommand = new Command(AssignToPopup);
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
        }

        public async void AssignToPopup()
        {
            AssignTo = await Page.DisplayActionSheet("Assign to...", "None", "", Enum.GetNames(typeof(TestUser)));
        }

        private void ClickedDone(object sender)
        {
            Button button = sender as Button;
            if (CurrentItem.IsDone)
            {
                CurrentItem.IsDone = false;
                button.Text = "Done";
                button.BackgroundColor = Color.LightGreen; 
                return;
            }
            CurrentItem.IsDone = true;
            button.Text = "Undone";
            button.BackgroundColor = Color.DarkGreen;
        }

        private async void ClickedDelete()
        {
            bool delete = await Page.DisplayAlert("This item will be delete.", "Are you sure?", "Yes", "No");

            if (delete)
            {
                //AWS
                //await DynamoDBTodoListService._instance.RemoveItemFromList(CurrentItem.Instance.ItemId);
                await SQLiteItemsService.DeleteItem(CurrentItem.Id);
                await Page.Navigation.PopAsync();
                await TodoListViewModel.GetItems();
            }
        }

        private async void ClickedSaveChanges()
        {
            bool update = await Page.DisplayAlert("This item will be update.", "Are you sure?", "Yes", "No");

            if (update)
            {
                //await DynamoDBTodoListService._instance.UpdateItem(CurrentItem.Instance.GetItemObject());
                //await SQLiteItemsService.UpdateItem(CurrentItem.Instance.ItemId);
                //TodoListViewModel.ItemList.ForEach(item => {
                //    if (CurrentItem.Instance.ItemId == item.Id)
                //    {
                //        item.Notes = CurrentItem.Instance.Notes;
                //    }
                //});
                //await Page.DisplayAlert("Saving Changes...", "Successfully saved", "Ok");
                //await Page.Navigation.PopAsync();
            }
            //CurrentItem.Instance.UpdateCurrentItem();
            //try
            //{
            //    await DynamoDBTodoListService._instance.UpdateItem(CurrentItem.Instance.MyItem);
            //    await Page.DisplayAlert("Saving Changes...", "Successfully saved", "Ok");

            //    await Page.Navigation.PopAsync();
            //    await TodoListViewModel.GetItems();

            //}
            //catch
            //{
            //    await Page.DisplayAlert("Somethings wrong...", "Please try again", "Ok");
            //}
            ////CurrentItem.Instance.UpdateCurrentItem();
            ////await DynamoDBTodoListService._instance.UpdateItem(CurrentItem.Instance.MyItem);
            ////await Page.Navigation.PopAsync();
            ////await TodoListViewModel.GetItems();

        }


    }
}
