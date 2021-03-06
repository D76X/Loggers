﻿//      *********    DO NOT MODIFY THIS FILE     *********
//      This file is regenerated by a design tool. Making
//      changes to this file can cause errors.
namespace Expression.Blend.SampleData.SampleDataSource
{
    using System; 
    using System.ComponentModel;

// To significantly reduce the sample data footprint in your production application, you can set
// the DISABLE_SAMPLE_DATA conditional compilation constant and disable sample data at runtime.
#if DISABLE_SAMPLE_DATA
    internal class SampleDataSource { }
#else

    public class SampleDataSource : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public SampleDataSource()
        {
            try
            {
                Uri resourceUri = new Uri("/TestApp3;component/SampleData/SampleDataSource/SampleDataSource.xaml", UriKind.RelativeOrAbsolute);
                System.Windows.Application.LoadComponent(this, resourceUri);
            }
            catch
            {
            }
        }

        private Games _Games = new Games();

        public Games Games
        {
            get
            {
                return this._Games;
            }
        }
    }

    public class GamesItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string _Name = string.Empty;

        public string Name
        {
            get
            {
                return this._Name;
            }

            set
            {
                if (this._Name != value)
                {
                    this._Name = value;
                    this.OnPropertyChanged("Name");
                }
            }
        }

        private double _Price = 0;

        public double Price
        {
            get
            {
                return this._Price;
            }

            set
            {
                if (this._Price != value)
                {
                    this._Price = value;
                    this.OnPropertyChanged("Price");
                }
            }
        }

        private string _ReleaseDate = string.Empty;

        public string ReleaseDate
        {
            get
            {
                return this._ReleaseDate;
            }

            set
            {
                if (this._ReleaseDate != value)
                {
                    this._ReleaseDate = value;
                    this.OnPropertyChanged("ReleaseDate");
                }
            }
        }

        private System.Windows.Media.ImageSource _CoverImage = null;

        public System.Windows.Media.ImageSource CoverImage
        {
            get
            {
                return this._CoverImage;
            }

            set
            {
                if (this._CoverImage != value)
                {
                    this._CoverImage = value;
                    this.OnPropertyChanged("CoverImage");
                }
            }
        }
    }

    public class Games : System.Collections.ObjectModel.ObservableCollection<GamesItem>
    { 
    }
#endif
}
