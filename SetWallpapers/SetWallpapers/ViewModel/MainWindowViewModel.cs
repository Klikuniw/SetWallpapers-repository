using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SetWallpapers.Model;

namespace SetWallpapers.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly WallpaperCraftParser _parser = new WallpaperCraftParser();
        private ObservableCollection<Category> _categories;
        private ObservableCollection<Resolution> _resolutions;
        private ObservableCollection<string> _intervals;
        private Resolution _selectedResolution;
        private string _selectedInterval;

        public ObservableCollection<Category> Categories
        {
            get
            {
                if (_categories == null)
                {
                    _categories = _parser.ReadCategories("wallpaperscraftInfo.xml");
                }

                return _categories;
            }
            set
            {
                _categories = value;
                OnPropertyChanged("Categories");
            }
        }

        public ObservableCollection<Resolution> Resolutions 
        {
            get
            {
                if (_resolutions == null)
                {
                    _resolutions = _parser.ReadResolutions("wallpaperscraftInfo.xml");
                }

                return _resolutions;
            }
        }

        public ObservableCollection<string> Intervals
        {
            get
            {
                if (_intervals == null)
                {
                    _intervals = new ObservableCollection<string>(){"5 min","10 min","1 day"};
                }

                return _intervals;
            }
        }

        public Resolution SelectedResolution
        {
            get
            {
                if (_selectedResolution == null)
                {
                    _selectedResolution = _parser.ReadSelectedResolution("wallpaperscraftInfo.xml");
                }

                return _selectedResolution;
            }
            set
            {
                _selectedResolution = value;
                OnPropertyChanged("SelectedResolution");
            }
        }

        public string SelectedInterval
        {
            get
            {
                if (_selectedInterval == null)
                {
                    _selectedInterval = _parser.ReadInterval("wallpaperscraftInfo.xml");
                }

                return _selectedInterval;
            }
            set
            {
                _selectedInterval = value;
                OnPropertyChanged("SelectedInterval");
            }
        }



    }
}
