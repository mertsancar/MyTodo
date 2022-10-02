using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToDo.Services;
using ToDo.View;
using ToDo.ViewModel;
using Xamarin.Forms;

namespace ToDo.Model
{
    public class CurrentItem : ContentPage
    {

        public static int Id { get; set; }
        public static string Name { get; set; }
        public static string ItemListId { get; set; }
        public static string ItemListName { get; set; }
        public static string Owner { get; set; }
        public static bool IsDone { get; set; }
        public static string AssignTo { get; set; }
        public static string Notes { get; set; }

        public static async Task SetCurrentItem(Item item)
        {
            Id = item.Id;
            Name = item.Name;
            Owner = TestUser.Mert.ToString();
            ItemListName = item.ItemListName;
            AssignTo = item.AssignTo;
            Notes = item.Notes;
            IsDone = item.IsDone;
        }

    }
}
