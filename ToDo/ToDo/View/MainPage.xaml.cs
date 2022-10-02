using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Model;
using ToDo.Services;
using ToDo.View;
using ToDo.ViewModel;
using Xamarin.Forms;

namespace ToDo
{
    public partial class MainPage : ContentPage
    {
        public MainViewModel ViewModel { get; }

        public static StackLayout listsStackLayout;
        public static AbsoluteLayout loadingIcon;
        public MainPage()
        {
            InitializeComponent();
            loadingIcon = LoadingIcon;
            listsStackLayout = ListsStackLayout;
            BindingContext = ViewModel = new MainViewModel(this);
        }

    }
}
