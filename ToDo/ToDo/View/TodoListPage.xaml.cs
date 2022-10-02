using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Model;
using ToDo.Services;
using ToDo.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDo.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TodoListPage : ContentPage
    {
        public TodoListViewModel ViewModel { get; set; }

        public static StackLayout itemListStackLayout;
        public static AbsoluteLayout loadingIcon;
        public TodoListPage(ItemList itemList)
        {
            InitializeComponent();
            //itemListStackLayout = ItemListStackLayout;
            loadingIcon = LoadingIcon;
            itemListStackLayout = ItemListStackLayout;
            Title = itemList.Name;
            CurrentToDoList.Id = itemList.Id;
            CurrentToDoList.Name = itemList.Name;
            CurrentToDoList.Items = itemList.Items;
            BindingContext = ViewModel = new TodoListViewModel(this);
        }

        public TodoListPage()
        {
        }
    }
}