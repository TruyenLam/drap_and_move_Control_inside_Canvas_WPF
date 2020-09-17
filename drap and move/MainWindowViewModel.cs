using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace drap_and_move
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        bool captured = false;

        public ICommand _leftButtonDownCommand;
        public ICommand _leftButtonUpCommand;
        public ICommand _previewMouseMove;
        public ICommand _leftMouseButtonUp;

        public MainWindowViewModel()
        {
            PanelX = 100;
            PanelY = 100;
            RectX = PanelX - 50.0;
            RectY = PanelY - 50.0;
        }

        public ICommand PreviewMouseMove
        {
            get
            {
                return _previewMouseMove ?? (_previewMouseMove = new RelayCommand(
                   x =>
                   {
                       if (captured)
                       {
                           RectX = PanelX - 50.0;
                           RectY = PanelY - 50.0;
                       }
                   }));
            }
        }

        public ICommand LeftMouseButtonUp
        {
            get
            {
                return _leftMouseButtonUp ?? (_leftMouseButtonUp = new RelayCommand(
                   x =>
                   {
                       captured = false;
                   }));
            }
        }

        public ICommand LeftMouseButtonDown
        {
            get
            {
                return _leftButtonDownCommand ?? (_leftButtonDownCommand = new RelayCommand(
                   x =>
                   {
                       captured = true;
                   }));
            }
        }

        private double _panelX;
        private double _panelY;
        private double _rectX;
        private double _rectY;

        public double RectX
        {
            get { return _rectX; }
            set
            {
                if (value.Equals(_rectX)) return;
                _rectX = value;
                OnPropertyChanged("RectX");
            }
        }

        public double RectY
        {
            get { return _rectY; }
            set
            {
                if (value.Equals(_rectY)) return;
                _rectY = value;
                OnPropertyChanged("RectY");
            }
        }

        public double PanelX
        {
            get { return _panelX; }
            set
            {
                if (value.Equals(_panelX)) return;
                _panelX = value;
                OnPropertyChanged("PanelX");
            }
        }

        public double PanelY
        {
            get { return _panelY; }
            set
            {
                if (value.Equals(_panelY)) return;
                _panelY = value;
                OnPropertyChanged("PanelY");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            VerifyPropertyName(propertyName);
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [Conditional("DEBUG")]
        private void VerifyPropertyName(string propertyName)
        {
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
                throw new ArgumentNullException(GetType().Name + " does not contain property: " + propertyName);
        }
    }
}
