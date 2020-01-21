using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetWallpapers.Model
{
    interface IWallpaperParser
    {
        string WebsiteName { get; }
        string ParseImage(Category path,Resolution resolution);
        string ParseImage(List<string> path, Resolution resolution);

        string ToLink(string website, Category category, Resolution resolution, string page="");
    }
}
