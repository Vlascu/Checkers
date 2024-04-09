using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Checkers.Viewmodels.Entities
{
    public class Cell : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private BitmapImage image;
        private bool isEnabled = true;
        private byte rowIndex;
        private byte columnIndex;
        private SolidColorBrush background;

        public BitmapImage Image
        {
            get { return image; }
            set
            {
                image = value;
                OnPropertyChanged(nameof(Image));
            }
        }

        public SolidColorBrush Background
        {
            get { return background; }
            set { background = value; OnPropertyChanged(nameof(Background)); }
        }

        public byte RowIndex
        {
            get { return rowIndex; }
            set
            {
                rowIndex = value;
                OnPropertyChanged(nameof(RowIndex));
            }
        }
        public byte ColumnIndex
        {
            get { return columnIndex; }
            set
            {
                columnIndex = value;
                OnPropertyChanged(nameof(ColumnIndex));
            }
        }

        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                isEnabled = value;
                OnPropertyChanged(nameof(IsEnabled));
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
