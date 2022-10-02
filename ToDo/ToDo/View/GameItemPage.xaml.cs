using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Model;
using ToDo.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDo.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameItemPage : ContentPage
    {
        public GameItemViewModel ViewModel { get; set; }
        public GameItemPage(GameItem gameItem)
        {
            InitializeComponent();
            //CurrentItem.Instance.SetCurrentGameItem(gameItem);
            BindingContext = ViewModel = new GameItemViewModel(this);
        }
    }
}