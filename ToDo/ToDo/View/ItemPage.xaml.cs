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
    public partial class ItemPage : ContentPage
    {
        public ItemViewModel ItemViewModel { get; set; }
        public ItemPage(Item item)
        {
            InitializeComponent();
            CurrentItem.SetCurrentItem(item);
            BindingContext = ItemViewModel = new ItemViewModel(this);
        }
    }
}