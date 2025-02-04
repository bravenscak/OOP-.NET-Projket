using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Settings
{
    public class AppSettings
    {
        public string Language { get; set; }
        public string GenderCategory { get; set; }
        public string LoadingDataBy { get; set; }
        public string Resolution { get; set; }

        public FavouriteSettings FavouriteSettings { get; set; }
    }
}
