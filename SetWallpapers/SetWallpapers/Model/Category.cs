using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SetWallpapers.Model
{
    public class Category : INotifyPropertyChanged
    {
        private string _name;
        private string _tag;
        private bool _checked;

        public string Name
        {
            get => _name;
            set
            { 
                _name = value;
                OnPropertyChanged();
            }
        }
        public string Tag
        {
            get => _tag;
            set
            {
                _tag = value;
                OnPropertyChanged();
            } 
        }
        public bool Checked
        {
            get => _checked;
            set
            { 
                _checked = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
