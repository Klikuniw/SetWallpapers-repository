using System;
using System.Collections.ObjectModel;
using System.Linq;
using SetWallpapers.Infrastructure;
using SetWallpapers.Model;
using System.Windows.Input;
using System.Windows.Forms;
using System.Windows.Threading;

namespace SetWallpapers.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly WallpaperCraftParser _parser = new WallpaperCraftParser();
        private readonly XmlFileService _xmlFileService = new XmlFileService();

        private ObservableCollection<Category> _categories;
        private ObservableCollection<Resolution> _resolutions;
        private ObservableCollection<string> _intervals;
        private Resolution _selectedResolution;
        private string _selectedInterval;
        private TimeSpan _time;

        private readonly DispatcherTimer _dispatcherTimerShowTime = new DispatcherTimer();

        private ICommand _saveChangesCommand;
        private ICommand _getResolutionCommand;
        private ICommand _closingWindowCommand;
        private ICommand _startedWindowCommand;

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
        public ICommand ClosingWindowCommand
        {
            get
            {
                if (_closingWindowCommand == null)
                {
                    _closingWindowCommand = new RelayCommand(ExecuteClosingWindowCommand);
                }

                return _closingWindowCommand;
            }
        }
        public ICommand StartedWindowCommand
        {
            get
            {
                if (_startedWindowCommand == null)
                {
                    _startedWindowCommand = new RelayCommand(ExecuteStartedWindowCommand);
                }

                return _startedWindowCommand;
            }
        }

        
        public ObservableCollection<Category> Categories
        {
            get
            {
                if (_categories == null)
                {
                    _categories = _xmlFileService.ReadCategories("wallpaperscraftInfo.xml");
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
                    _resolutions = _xmlFileService.ReadResolutions("wallpaperscraftInfo.xml");
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
                    _intervals = new ObservableCollection<string>() { "5 sec","5 min", "10 min", "1 day" };
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
                    _selectedResolution = _xmlFileService.ReadSelectedResolution("settings.xml");
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
                    _selectedInterval = _xmlFileService.ReadInterval("settings.xml");
                }

                return _selectedInterval;
            }
            set
            {
                _selectedInterval = value;
                OnPropertyChanged("SelectedInterval");
            }
        }
        public TimeSpan Time
        {
            get
            {
                return _time;
            }
            set
            {
                _time = value;
                OnPropertyChanged("Time");
            }
        }

        private void ExecuteGetResolutionCommand(object obj)
        {
            SelectedResolution = new Resolution() { 
                Value = $"{Screen.PrimaryScreen.Bounds.Width}x{Screen.PrimaryScreen.Bounds.Height}"};
        }
        private void ExecuteSaveChangesCommand(object obj)
        {
            _xmlFileService.SaveUserInfoChanges("wallpaperscraftInfo.xml", Categories.ToList());
            _xmlFileService.SaveSettingChanges("settings.xml", SelectedInterval,DateTime.Now, SelectedResolution);

            _dispatcherTimerShowTime.Tick += dispatcherTimer_Tick;
            _dispatcherTimerShowTime.Interval = new TimeSpan(0,0,0,1);
            
            Time = ConverterTime.ToTimeSpan(SelectedInterval);
            _dispatcherTimerShowTime.Start();
        }
        private void ExecuteClosingWindowCommand(object obj)
        {
            _xmlFileService.WriteRemainsIntervalTime("settings.xml",Time);   
        }
        private void ExecuteStartedWindowCommand(object obj)
        {
            _dispatcherTimerShowTime.Tick += dispatcherTimer_Tick;
            _dispatcherTimerShowTime.Interval = new TimeSpan(0, 0, 0, 1);

            Time = _xmlFileService.ReadRemainsIntervalTime("settings.xml");
            _dispatcherTimerShowTime.Start();
        }


        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (Time.Equals(new TimeSpan(0,0,0)))
            {
                Wallpaper.Set(new Uri(_parser.ParseImage(Categories[0], SelectedResolution)), Wallpaper.Style.Centered);
                Time = ConverterTime.ToTimeSpan(SelectedInterval);
            }
            else
            {
                Time = new TimeSpan(Time.Hours,Time.Minutes,Time.Seconds-1);
            }
            
        }

    }
}
