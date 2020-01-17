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
using System.Windows.Threading;

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
        private TimeSpan _time;

        private readonly DispatcherTimer _dispatcherTimerShowTime = new DispatcherTimer();
        private readonly DispatcherTimer _dispatcherTimerExecuteMethod = new DispatcherTimer();

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
            SelectedResolution = new Resolution() { Value = String.Format("{0}x{1}", Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height) };
        }
        private void ExecuteSaveChangesCommand(object obj)
        {
            _parser.SaveChanges("wallpaperscraftInfo.xml", Categories.ToList(), SelectedResolution, SelectedInterval);

            _dispatcherTimerShowTime.Tick += new EventHandler(dispatcherTimer_Tick);
            _dispatcherTimerShowTime.Interval = new TimeSpan(0,0,0,1);
            
            _dispatcherTimerExecuteMethod.Tick+= new EventHandler(Show);
            _dispatcherTimerExecuteMethod.Interval = ToTimeSpan(SelectedInterval);



            Time = _dispatcherTimerExecuteMethod.Interval;
            _dispatcherTimerShowTime.Start();
            _dispatcherTimerExecuteMethod.Start();

        }

        private void Show(object sender, EventArgs e)
        {
            MessageBox.Show("sdasd");
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (Time.Equals(new TimeSpan(0,0,0)))
            {
                Time = _dispatcherTimerExecuteMethod.Interval;
            }
            else
            {
                Time = new TimeSpan(Time.Hours,Time.Minutes,Time.Seconds-1);
            }
            
        }

        private TimeSpan ToTimeSpan(string time)
        {
            TimeSpan res = new TimeSpan(0, 0, 0);

            var value = SelectedInterval.Split(' ');
            switch (value[1])
            {
                case "min":
                {
                    res = new TimeSpan(0, Convert.ToInt32(value[0]),0);
                    break;
                }
                case "day":
                {
                    res = new TimeSpan(Convert.ToInt32(value[0]),0,0, 0);
                        break;
                }
                case "sec":
                {
                    res = new TimeSpan(0, 0, 0, Convert.ToInt32(value[0]));
                    break;
                }
            }

            return res;
        }
    }
}
