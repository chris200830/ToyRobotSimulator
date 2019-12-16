using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace View.ViewModels.Components
{
    internal class TableTopViewComponent : INotifyPropertyChanged
    {
        public int Row { get; set; }

        public int Column { get; set; }

        private SolidColorBrush gridCellColor;

        public SolidColorBrush GridCellColor
        {
            get => gridCellColor;
            set
            {
                gridCellColor = value;
                OnPropertyChanged();
            }
        }

        public Thickness BorderThickness { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
