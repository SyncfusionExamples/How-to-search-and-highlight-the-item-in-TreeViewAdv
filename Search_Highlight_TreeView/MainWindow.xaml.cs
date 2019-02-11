using Syncfusion.SfSkinManager;
using Syncfusion.Windows.Shared;
using Syncfusion.Windows.Tools.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Search_Highlight_TreeView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new ViewModel();
        } 
    }

    public enum SearchType
    {
        /// <summary>
        /// Search the TreeViewItemAdv which contains the SearchText.
        /// </summary>
        Contains,
        /// <summary>
        /// Search the TreeViewItemAdv which starts with the SearchText.
        /// </summary>
        StartsWith,
        /// <summary>
        /// Search the TreeViewItemAdv which ends with the SearchText.
        /// </summary>
        EndsWith
    }
   
}
