using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo.Model
{
    public class GameItem : Item
    {
        public GamePlatform GamePlatform { get; set; }
        public GameGenre GameGenre { get; set; }

        public GameItem(Item item, GamePlatform platform, GameGenre genre)
        {
            Id = item.Id;
            Name = item.Name;
            Owner = item.Owner;
            ItemListName = item.ItemListName;
            ItemGenre = item.ItemGenre;
            AssignTo = item.AssignTo;
            Notes = item.Notes;
            IsDone = item.IsDone;

            GamePlatform = platform;
            GameGenre = genre;
        }

    }
}
