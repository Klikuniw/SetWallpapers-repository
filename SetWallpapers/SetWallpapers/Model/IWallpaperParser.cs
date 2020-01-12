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

        ObservableCollection<string> Categories { get; }
        ObservableCollection<string> Resolutions { get; }
        
        string ParseImage(string path);
        List<string> ParseImages(string path);

        ObservableCollection<string> ReadCategories(string path);
        ObservableCollection<string> ReadResolutions(string path);

    }
}
