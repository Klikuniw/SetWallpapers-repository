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

        List<string> Categories { get; }
        List<string> Resolutions { get; }
        
        string ParseImage(string path);
        List<string> ParseImages(string path);

        List<string> ReadCategories(string path);
        List<string> ReadResolutions(string path);

    }
}
