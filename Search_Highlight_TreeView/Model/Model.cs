using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search_Highlight_TreeView
{
    class Model : INotifyPropertyChanged
    {
        public Model()
        {
            SubItems = new ObservableCollection<Model>();
        }


        public string Header { get; set; }

        private bool isSelected= true;

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                RaisePropertyChanged("IsSelected");
            }
        }

        private bool isExpanded;

        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                isExpanded = value;
                RaisePropertyChanged("IsExpanded");
            }
        }

        private void RaisePropertyChanged(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

        private ObservableCollection<Model> children = new ObservableCollection<Model>();

        public ObservableCollection<Model> Children
        {
            get { return children; }
            set { children = value; }
        }
        public ObservableCollection<Model> SubItems { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
