using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Settings
{
    public class FavouriteSettings
    {
        public Result FavouriteTeam { get; set; }
        public IList<Player> FavouritePlayers { get; set; }
    }
}
