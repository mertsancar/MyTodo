using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo.Model
{
    public class CurrentToDoList
    {
        public static int Id { get; set; }
        public static string Name { get; set; }
        public static List<Item> Items { get; set; }
    }
}
