﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Interfaces
{
    public interface IPlayerIconRepo
    {
        Task<Dictionary<string, string>> GetAllIconPathsAsync();
        Task SaveAllIconsAsync(IDictionary<string, string> iconPaths);
        void SavePlayerIconPath(string playerName, string imagePath);
    }
}
