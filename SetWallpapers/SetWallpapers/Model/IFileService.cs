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
        DateTime ReadClosingTime(string path);
        TimeSpan ReadRemainsIntervalTime(string path);
        bool ReadTimerStarted(string path);

        void SaveSettingChanges(string path, string interval,DateTime closingTime,Resolution resolution);
        void SaveUserInfoChanges(string path, List<Category> categories);

        void WriteClosingTime(string path, DateTime time);
        void WriteRemainsIntervalTime(string path, TimeSpan time);
        void WriteTimerStarted(string path,bool value);
    }

}
