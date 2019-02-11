using Syncfusion.Windows.Tools;
using Syncfusion.Windows.Tools.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Search_Highlight_TreeView
{
    class ViewModel 
    {
     
        public ViewModel()
        {
           
            items.Add(new Model() { Header = "Quick Access" });
            items.Add(new Model() { Header = "One Drive" });
            items.Add(new Model() { Header = "This PC" });
            items.Add(new Model() { Header = "Network" });

            items[1].Children.Add(new Model() { Header = "Documents"});
            items[1].Children.Add(new Model() { Header = "Pictures" });
            items[1].Children[0].Children.Add(new Model() { Header = "Screenshots" });

            items[2].Children.Add(new Model() { Header = "3D Objects" });
            items[2].Children.Add(new Model() { Header = "Desktop" });
            items[2].Children.Add(new Model() { Header = "Documents" });
            items[2].Children.Add(new Model() { Header = "Downloads" });
            items[2].Children.Add(new Model() { Header = "Music" });
            items[2].Children.Add(new Model() { Header = "Pictures" });
            items[2].Children.Add(new Model() { Header = "Videos" });
            items[2].Children.Add(new Model() { Header = "Local Disk (C:)" });
            items[2].Children.Add(new Model() { Header = "Local Disk (D:)" });

            items[2].Children[4].Children.Add(new Model() { Header = "iTunes" });

            items[2].Children[5].Children.Add(new Model() { Header = "Camera Roll" });
            items[2].Children[5].Children.Add(new Model() { Header = "FeebBack" });
            items[2].Children[5].Children.Add(new Model() { Header = "Saved Pictures" });
            items[2].Children[5].Children.Add(new Model() { Header = "Screenshots" });

            items[2].Children[6].Children.Add(new Model() { Header = "Videos" });

            items[2].Children[7].Children.Add(new Model() { Header = "Intel" });
            items[2].Children[7].Children.Add(new Model() { Header = "Logs" });
            items[2].Children[7].Children.Add(new Model() { Header = "Program files" });
            items[2].Children[7].Children.Add(new Model() { Header = "ProgramData" });
            items[2].Children[7].Children.Add(new Model() { Header = "temp" });
            items[2].Children[7].Children.Add(new Model() { Header = "Users" });
            items[2].Children[7].Children.Add(new Model() { Header = "Windows" });
            items[2].Children[7].Children.Add(new Model() { Header = "Work" });
               
        }

        private ObservableCollection<Model> items = new ObservableCollection<Model>();

        public ObservableCollection<Model> TreeItems
        {
            get { return items; }
            set { items = value; }
        }    
    }
}
