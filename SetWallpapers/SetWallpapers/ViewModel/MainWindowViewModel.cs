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
        private ObservableCollection<string> _resolutions;

        public ObservableCollection<Category> Categories
        {
            get
            {
                if (_categories == null)
                {
                    _categories = _parser.Categories;
                }

                return _categories;
            }
        }

        public ObservableCollection<string> Resolutions 
        {
            get
            {
                if (_resolutions == null)
                {
                    _resolutions = _parser.Resolutions;
                }

                return _resolutions;
            }
        }






    }
}
