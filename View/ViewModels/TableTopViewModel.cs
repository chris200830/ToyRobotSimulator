using Caliburn.Micro;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Media;
using View.Commands;
using View.ViewModels.Components;

namespace View.ViewModels
{
    internal class TableTopViewModel : INotifyPropertyChanged
    {
        private string statusReport { get; set; }
        private int rows;
        private int columns;
        public BindableCollection<TableTopViewComponent> TableTopViewComponents { get; set; }

        public bool IsRobotPlaced { get; set; }

        public int Rows
        {
            get => rows;
            set
            {
                rows = value;
                OnPropertyChanged();
            }
        }

        public int Columns
        {
            get => columns;
            set
            {
                columns = value;
                OnPropertyChanged();
            }
        }

        public string StatusReport
        {
            get => statusReport;
            set
            {
                statusReport = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand PlaceCommand { get; set; }
        public RelayCommand MoveCommand { get; set; }
        public RelayCommand LeftCommand { get; set; }
        public RelayCommand RightCommand { get; set; }
        public RelayCommand ReportCommand { get; set; }

        public TableTopViewModel()
        {
            TableTopViewComponents = new BindableCollection<TableTopViewComponent>();
            IsRobotPlaced = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
