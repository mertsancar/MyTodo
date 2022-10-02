using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo.Model
{
    public class Item
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public bool IsDone { get; set; }

        [ForeignKey(typeof(Item))]
        public int ItemListId { get; set; }

        public string ItemListName { get; set; }
        public ItemGenre ItemGenre { get; set; }
        public string AssignTo { get; set; }
        public string Notes { get; set; }

        //public GameItem SetGameItem(GamePlatform gamePlatform, GameGenre gameGenre)
        //{
        //    return new GameItem(this, gamePlatform, gameGenre);
        //}

    }
}
