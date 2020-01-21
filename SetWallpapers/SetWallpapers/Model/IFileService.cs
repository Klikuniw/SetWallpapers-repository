using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetWallpapers.Model
{
    public interface IFileService
    {
        ObservableCollection<Category> ReadCategories(string path);
        ObservableCollection<Resolution> ReadResolutions(string path);
        Resolution ReadSelectedResolution(string path);
        string ReadInterval(string path);
        TimeSpan ReadClosingTime(string path);
        void SaveChanges(string path, List<Category> categories, Resolution resolution, string interval);
    }

}
