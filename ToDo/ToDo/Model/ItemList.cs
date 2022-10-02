using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo.Model
{
    public class ItemList
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }

        [OneToMany]
        public List<Item> Items { get; set; }

    }
}
