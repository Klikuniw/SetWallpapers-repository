using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetWallpapers.Model
{
    public class WallpaperCraftParser : IWallpaperParser
    {
        public string WebsiteName => "";

        public List<string> Categories => ReadCategories("");

        public List<string> Resolutions => ReadResolutions("");

        public string ParseImage(string path)
        {
            throw new NotImplementedException();
        }

        public List<string> ParseImages(List<string> paths)
        {
            throw new NotImplementedException();
        }

        public List<string> ReadCategories(string paths)
        {
            throw new NotImplementedException();
        }

        public List<string> ReadResolutions(string paths)
        {
            throw new NotImplementedException();
        }

        private string LoadDocument(string path)
        {
            string htmlDoc = "";

            return htmlDoc;
        }


    }
}
