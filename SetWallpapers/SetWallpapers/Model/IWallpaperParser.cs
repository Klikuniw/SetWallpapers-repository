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

        ObservableCollection<Category> Categories { get; }
        ObservableCollection<Resolution> Resolutions { get; }
        
        string ParseImage(string path);
        List<string> ParseImages(string path);

        ObservableCollection<Category> ReadCategories(string path);
        ObservableCollection<Resolution> ReadResolutions(string path);

    }
}
