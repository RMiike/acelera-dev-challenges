using System;
using System.Collections.Generic;
using System.Text;

namespace Source
{
    class Team
    {
        public Team(long id, string name, DateTime createDate, string mainShirtColor, string secondaryShirtColor)
        {
            Id = id;
            Name = name;
            CreateDate = createDate;
            MainShirtColor = mainShirtColor;
            SecondaryShirtColor = secondaryShirtColor;
            Players = new List<Player>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public string MainShirtColor { get; set; }
        public string SecondaryShirtColor { get; set; }

        public List<Player> Players { get; set; }

        public void AddNewPlayer(Player player)
        {
            Players.Add(player);
        }
    }
}
