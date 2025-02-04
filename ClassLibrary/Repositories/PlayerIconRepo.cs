﻿using ClassLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Repositories
{
    internal class PlayerIconRepo : IPlayerIconRepo
    {
        private const string ICON_PATH_FILE = @"..\..\..\..\ClassLibrary\Images\iconPaths.txt";

        public async Task<Dictionary<string, string>> GetAllIconPathsAsync()
        {
            var iconPaths = new Dictionary<string, string>();

            if (File.Exists(ICON_PATH_FILE))
            {
                var lines = await File.ReadAllLinesAsync(ICON_PATH_FILE);

                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 2)
                    {
                        iconPaths[parts[0]] = parts[1];
                    }
                }
            }

            return iconPaths;
        }

        public async Task SaveAllIconsAsync(IDictionary<string, string> iconPaths)
        {
            var lines = iconPaths.Select(kvp => $"{kvp.Key},{kvp.Value}").ToList();
            await File.WriteAllLinesAsync(ICON_PATH_FILE, lines);
        }

        public async void SavePlayerIconPath(string playerName, string imagePath)
        {
            var iconPaths = await GetAllIconPathsAsync();
            iconPaths[playerName] = imagePath;
            await SaveAllIconsAsync(iconPaths);
        }
    }
}
