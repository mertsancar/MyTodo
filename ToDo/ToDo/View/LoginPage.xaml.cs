using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDo.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginViewModel ViewModel { get; set; }
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = ViewModel = new LoginViewModel(this);
        }
    }
}