using System;
using System.Collections.ObjectModel;
using System.Linq;
using SetWallpapers.Infrastructure;
using System.Text;
using System.Threading.Tasks;
using SetWallpapers.Infrastructure;
using SetWallpapers.Model;
using System.Windows.Input;
using System.Drawing;
using System.Windows.Forms;
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

        private ICommand _saveChangesCommand;
        private ICommand _getResolutionCommand;

        public ICommand SaveChangesCommand
        {
            get
            {
                if (_saveChangesCommand == null)
                {
                    _saveChangesCommand = new RelayCommand(ExecuteSaveChangesCommand);
                }

                return _saveChangesCommand;
            }
        }
        public ICommand GetResolutionCommand
        {
            get
            {
                if (_getResolutionCommand == null)
                {
                    _getResolutionCommand = new RelayCommand(ExecuteGetResolutionCommand);
                }

                return _getResolutionCommand;
            }
        }

        private void ExecuteGetResolutionCommand(object obj)
        {
            SelectedResolution = new Resolution(){Value = String.Format("{0}x{1}", Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height) };
        }
        private void ExecuteSaveChangesCommand(object obj)
        {
            _parser.SaveChanges("wallpaperscraftInfo.xml",Categories.ToList(),SelectedResolution,SelectedInterval);
        }



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
                    _intervals = new ObservableCollection<string>() { "5 min", "10 min", "1 day" };
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


        private void ExecuteGetResolutionCommand(object obj)
        {
            SelectedResolution = new Resolution() { Value = String.Format("{0}x{1}", Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height) };
        }
        private void ExecuteSaveChangesCommand(object obj)
        {
            _parser.SaveChanges("wallpaperscraftInfo.xml", Categories.ToList(), SelectedResolution, SelectedInterval);
        }


    }
}
