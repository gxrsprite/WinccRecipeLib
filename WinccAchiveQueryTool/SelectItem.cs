using Ruifei.AppMain;
using System;

namespace Ruifei.ViewModels
{
    public class SelectItem : ViewModelBase
    {
        public SelectItem()
        {

        }
        public SelectItem(string name)
        {
            Name = name;
        }

        public SelectItem(int Id, string name) : this(name)
        {
            ID = Id;
        }

        private int id;
        private string displayName;
        private bool isSelected;

        public int ID
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        public Guid Guid { get; set; }

        public string Name
        {
            get
            {
                return this.displayName;
            }
            set
            {
                this.displayName = value;
            }
        }

        public string Text { get; set; }

        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }

            set
            {
                if (this.isSelected != value)
                {
                    this.isSelected = value;
                    this.OnPropertyChanged("IsSelected");
                }
            }
        }
    }

    public class SelectItemViewModel : ViewModelBase
    {
        public SelectItemViewModel()
        {

        }
        public SelectItemViewModel(string name)
        {
            Name = name;
        }

        private int id;
        private string displayName;
        private bool isSelected;

        public int ID
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
                OnPropertyChanged("ID");
            }
        }

        public Guid Guid { get; set; }

        public string Name
        {
            get
            {
                return this.displayName;
            }
            set
            {
                this.displayName = value;
                OnPropertyChanged("Name");
            }
        }

        private string text;

        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                OnPropertyChanged("Text");
            }
        }


        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }

            set
            {
                if (this.isSelected != value)
                {
                    this.isSelected = value;
                    this.OnPropertyChanged("IsSelected");
                }
            }
        }
    }
}
