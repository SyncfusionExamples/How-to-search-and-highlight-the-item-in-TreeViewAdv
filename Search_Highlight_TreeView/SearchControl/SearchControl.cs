#region Copyright Syncfusion Inc. 2001-2018.
// Copyright Syncfusion Inc. 2001-2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Windows.Shared;
using Syncfusion.Windows.Tools.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Search_Highlight_TreeView
{
    /// <summary>
    /// class which helps to Search the Text in the TreeView.
    /// </summary>
    [TemplatePart(Name = "PART_Search", Type = typeof(Button))]
    [TemplatePart(Name = "PART_FindNext", Type = typeof(Button))]
    [TemplatePart(Name = "PART_FindPrevious", Type = typeof(Button))]
    [TemplatePart(Name = "PART_Close", Type = typeof(Button))]
    [TemplatePart(Name = "PART_ApplyFiltering", Type = typeof(CheckBox))]
    [TemplatePart(Name = "PART_ComboBox", Type = typeof(ComboBox))]
    [TemplatePart(Name = "PART_AdornerLayer", Type = typeof(AdornerDecorator))]
    [TemplatePart(Name = "PART_CaseSensitiveSearch", Type = typeof(CheckBox))]
    [TemplatePart(Name = "PART_SearchOnTextChange", Type = typeof(CheckBox))]
    [TemplatePart(Name = "PART_SearchBrush", Type = typeof(ColorPickerPalette))]
    [TemplatePart(Name = "PART_HighLightBrush", Type = typeof(ColorPickerPalette))]
    public class SearchControl : Control,IDisposable
    {
        #region Fields
        internal Button FindNextButton;
        internal Button FindPreviousButton;
        internal Button CloseButton;
        internal Button SearchButton;
        internal TextBox SearchTextBox;
        internal CheckBox CaseSensitiveSearchCheckBox;
        internal CheckBox SearchOnTextChangeCheckBox;
        internal ColorPickerPalette HighlightColorPickerPalette;
        internal ColorPickerPalette SearchColorPickerPalette;
        internal AdornerDecorator AdornerLayer;
        internal ComboBox ComboBox;
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the TreeView for the corresponding search operation.
        /// </summary>
        public TreeViewAdv Treeview
        {
            get { return (TreeViewAdv)GetValue(TreeViewProperty); }
            set { SetValue(TreeViewProperty, value); }
        }
        public static readonly DependencyProperty TreeViewProperty =
            DependencyProperty.Register("Treeview", typeof(TreeViewAdv), typeof(SearchControl), new PropertyMetadata(null));

        #endregion

        #region Ctor

        public SearchControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SearchControl), new FrameworkPropertyMetadata(typeof(SearchControl)));
        }

        public SearchControl(TreeViewAdv treeview)
        {
            Treeview = treeview;
        }

        #endregion

        #region Methods
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            SearchButton = this.GetTemplateChild("PART_Search") as Button;
            FindNextButton = this.GetTemplateChild("PART_FindNext") as Button;
            FindPreviousButton = this.GetTemplateChild("PART_FindPrevious") as Button;
            CloseButton = this.GetTemplateChild("PART_Close") as Button;
            ComboBox = this.GetTemplateChild("PART_SearchType") as ComboBox;
            ComboBox.SelectedIndex = 0;
            SearchTextBox = this.GetTemplateChild("PART_TextBox") as TextBox;
            CaseSensitiveSearchCheckBox = this.GetTemplateChild("PART_CaseSensitiveSearch") as CheckBox;
            SearchOnTextChangeCheckBox = this.GetTemplateChild("PART_SearchOnTextChange") as CheckBox;          
            AdornerLayer = this.GetTemplateChild("PART_AdornerLayer") as AdornerDecorator;
            SearchColorPickerPalette = this.GetTemplateChild("PART_SearchBrush") as ColorPickerPalette;
            HighlightColorPickerPalette = this.GetTemplateChild("PART_HighLightBrush") as ColorPickerPalette;
            SearchColorPickerPalette.Color = Color.FromRgb(255, 255, 0);
            HighlightColorPickerPalette.Color = Color.FromRgb(250, 160, 122);
            this.SearchTextBox.Focus();
            this.WireEvents();
        }

        #endregion

        #region Events

        /// <summary>
        /// Method to wire the required events.
        /// </summary>
        private void WireEvents()
        {
            SearchButton.Click += SearchButton_Click;
            FindNextButton.Click += OnFindNextButtonClick;
            FindPreviousButton.Click += OnFindPreviousButtonClick;
            CloseButton.Click += OnCloseButtonClick;
            SearchTextBox.TextChanged += OnTextChanged;
            CaseSensitiveSearchCheckBox.Click += OnCaseSensitiveSearchCheckBoxClick;
           
        }



      
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
           
            if (ComboBox.SelectedIndex == 0)
                SearchType = SearchType.Contains;
            else if (ComboBox.SelectedIndex == 2)
                SearchType = SearchType.StartsWith;
            else if (ComboBox.SelectedIndex == 1)
                SearchType = SearchType.EndsWith;
             SearchBrush = new SolidColorBrush(SearchColorPickerPalette.Color);
            SearchHighLightBrush = new SolidColorBrush(HighlightColorPickerPalette.Color);

           Search();
        }



        /// <summary>
        /// Event handler to handle CaseSensitive search check box click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCaseSensitiveSearchCheckBoxClick(object sender, RoutedEventArgs e)
        {
            
            return;
        }


        /// <summary>
        /// Event handler to handle when text value is changed in SearchTextBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if ((bool)SearchOnTextChangeCheckBox.IsChecked)
            {
               
                if (ComboBox.SelectedIndex == 0)
                   SearchType = SearchType.Contains;
                else if (ComboBox.SelectedIndex == 2)
                    SearchType = SearchType.StartsWith;
                else if (ComboBox.SelectedIndex == 1)
                    SearchType = SearchType.EndsWith;
                SearchBrush = new SolidColorBrush(SearchColorPickerPalette.Color);
               SearchHighLightBrush = new SolidColorBrush(HighlightColorPickerPalette.Color);

                Search();
            }

        }


        /// <summary>
        ///  Event handler to handle when clicking on FindNext button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnFindNextButtonClick(object sender, RoutedEventArgs e)
        {
            FindNext();
        }


        /// <summary>
        /// Event handler to handle when clicking on FindPrevious button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnFindPreviousButtonClick(object sender, RoutedEventArgs e)
        {
            FindPrevious();
        }

        /// <summary>
        /// Event handler to handle when clicking on Close button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCloseButtonClick(object sender, RoutedEventArgs e)
        {
            this.SearchTextBox.Text = string.Empty;
            ClearSearch();
          

        }
       
        #endregion

        /// <summary>
        /// Method to UnWire the wired events.
        /// </summary>
        private void UnWireEvents()
        {
            FindNextButton.Click -= OnFindNextButtonClick;
            FindPreviousButton.Click -= OnFindPreviousButtonClick;
            CloseButton.Click -= OnCloseButtonClick;
            SearchTextBox.TextChanged -= OnTextChanged;
        }


        #region Search
        private int matchindex = -1;
        public SearchType SearchType
        {
            get;
            set;
        }

        public Brush SearchHighLightBrush
        {
            get;
            set;
        }

        public Brush SearchBrush
        {
            get;
            set;
        }

        public ObservableCollection<TreeViewItemAdv> SearchedItems
        {
            get;
            set;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }
        private void Search()
        {
            ClearSearch();

            if (!string.IsNullOrEmpty(SearchTextBox.Text))
            {
                string searchText = SearchTextBox.Text;

                foreach (Model model in Treeview.ItemsSource)
                {
                    TreeViewItemAdv mainitem = Treeview.ItemContainerGenerator.ContainerFromItem(model) as TreeViewItemAdv;

                    if (mainitem != null && MatchSearchText(model.Header))
                    {
                        //Function which is used to apply inline to the TextBlock
                        ApplyInline(mainitem, false);                      
                    }

                    foreach (Model level2 in model.Children)
                    {
                        if (MatchSearchText(level2.Header) || level2.Children.Count > 0)
                        {
                            mainitem.IsExpanded = true;
                            Treeview.UpdateLayout();
                        }
                        TreeViewItemAdv level2item = mainitem.ItemContainerGenerator.ContainerFromItem(level2) as TreeViewItemAdv;

                        if (level2item != null)
                        {
                            ApplyInline(level2item, false);
                          

                        }
                        if (level2item != null)
                        {
                            foreach (Model level3 in level2.Children)
                            {
                                if (MatchSearchText(level3.Header) || level3.Children.Count > 0)
                                {
                                    level2item.IsExpanded = true;
                                    Treeview.UpdateLayout();

                                }
                                TreeViewItemAdv level3item = level2item.ItemContainerGenerator.ContainerFromItem(level3) as TreeViewItemAdv;
                                if (level3item != null)
                                {
                                    ApplyInline(level3item, false);
                                   

                                }
                            }
                        }
                    }
                }

                if (SearchedItems != null && SearchedItems.Count > 0)
                {
                    ApplyInline(SearchedItems[0], true);
                    SearchedItems[0].BringIntoView();
                    matchindex = 0;
                }
            }

        }

        protected bool MatchSearchText(string str)
        {
            string SearchText = SearchTextBox.Text;

            if (this.SearchType == SearchType.Contains)
            {
                if (!(bool)CaseSensitiveSearchCheckBox.IsChecked)
                    return str.ToLower().Contains(SearchText.ToString().ToLower());
                else
                    return str.Contains(SearchText.ToString());

            }
            else if (this.SearchType == SearchType.EndsWith)
            {
                if (!(bool)CaseSensitiveSearchCheckBox.IsChecked)
                    return str.ToLower().EndsWith(SearchText.ToString().ToLower());
                else
                    return str.EndsWith(SearchText.ToString());

            }
            else
            {
                if (!(bool)CaseSensitiveSearchCheckBox.IsChecked)
                    return str.ToLower().StartsWith(SearchText.ToString().ToLower());
                else
                    return str.StartsWith(SearchText.ToString());


            }

        }


        private void ClearSearch()
        {

            SearchedItems = new ObservableCollection<TreeViewItemAdv>();
            foreach (Model model in Treeview.ItemsSource)
            {
                TreeViewItemAdv mainitem = Treeview.ItemContainerGenerator.ContainerFromItem(model) as TreeViewItemAdv;

                if (mainitem != null)
                    ApplyInline(mainitem, false);


                foreach (Model level2 in model.Children)
                {

                    TreeViewItemAdv level2item = mainitem.ItemContainerGenerator.ContainerFromItem(level2) as TreeViewItemAdv;
                    if (level2item != null)
                        ApplyInline(level2item, false);
                    if (level2item != null)
                    {
                        foreach (Model level3 in level2.Children)
                        {

                            TreeViewItemAdv level3item = level2item.ItemContainerGenerator.ContainerFromItem(level3) as TreeViewItemAdv;
                            if (level3item != null)
                            {
                                ApplyInline(level3item, false);

                            }

                        }


                    }

                }


            }
            Treeview.UpdateLayout();



        }

        public  void FindNext()
        {
            if (SearchedItems != null && SearchedItems.Count > 0)
            {
                ApplyInline(SearchedItems[matchindex], false);
                if (matchindex + 1 < SearchedItems.Count)
                {
                    ApplyInline(SearchedItems[matchindex + 1], true);
                    Treeview.BringIntoView(SearchedItems[matchindex + 1]);
                    matchindex++;
                }
                else
                {
                    ApplyInline(SearchedItems[matchindex], false);
                    matchindex = 0;
                    ApplyInline(SearchedItems[0], true);
                    Treeview.BringIntoView(SearchedItems[0]);
                }

            }
        }

        public  void FindPrevious()
        {
            if (SearchedItems != null && SearchedItems.Count > 0)
            {
                if (matchindex - 1 < SearchedItems.Count && matchindex != 0)
                {
                    ApplyInline(SearchedItems[matchindex], false);
                    ApplyInline(SearchedItems[matchindex - 1], true);
                    Treeview.BringIntoView(SearchedItems[matchindex - 1]);
                    matchindex--;
                }
                else
                {
                    ApplyInline(SearchedItems[matchindex], false);
                    matchindex = SearchedItems.Count - 1;
                    ApplyInline(SearchedItems[matchindex], true);
                    Treeview.BringIntoView(SearchedItems[matchindex]);
                }

            }
        }

        internal void ApplyInline(TreeViewItemAdv item,bool issearchhighlightbrush)
        {
            var tempSearchText = SearchTextBox.Text;
            String[] metaCharacters = { "\\", "^", "$", "{", "}", "[", "]", "(", ")", ".", "*", "+", "?", "|", "<", ">", "-", "&" };
            if (metaCharacters.Any(tempSearchText.Contains))
            {
                for (int i = 0; i < metaCharacters.Length; i++)
                {
                    if (tempSearchText.Contains(metaCharacters[i]))
                        tempSearchText = tempSearchText.Replace(metaCharacters[i], "\\" + metaCharacters[i]);
                }
            }

            Regex regex = null;
            if (!string.IsNullOrEmpty(tempSearchText))
            {
                if (this.SearchType == SearchType.StartsWith)
                {
                    if (!(bool)CaseSensitiveSearchCheckBox.IsChecked)
                        regex = new Regex("^(" + tempSearchText + ")", RegexOptions.IgnoreCase);
                    else
                        regex = new Regex("^(" + tempSearchText + ")", RegexOptions.None);
                }
                else if (this.SearchType == SearchType.EndsWith)
                {
                    if (!(bool)CaseSensitiveSearchCheckBox.IsChecked)
                        regex = new Regex("(" + tempSearchText + ")$", RegexOptions.IgnoreCase);
                    else
                        regex = new Regex("(" + tempSearchText + ")$", RegexOptions.None);
                }
                else
                {
                    if (!(bool)CaseSensitiveSearchCheckBox.IsChecked)
                        regex = new Regex("(" + tempSearchText + ")", RegexOptions.IgnoreCase);
                    else
                        regex = new Regex("(" + tempSearchText + ")", RegexOptions.None);
                }

            }

            var textBlock = GetEditableTextBlock(item);
            string[] substrings = { textBlock.Text };
            //get all the words from the 'content'
            if (regex != null)
                substrings = regex.Split(textBlock.Text.ToString());
            textBlock.Inlines.Clear();
            foreach (var item1 in substrings)
            {
                if (regex != null && regex.Match(item1).Success)
                {
                    //create a 'Run' and add it to the TextBlock
                    Run run = new Run(item1);
                    if(!issearchhighlightbrush)
                    run.Background = SearchBrush;
                    else
                        run.Background = SearchHighLightBrush;
                    textBlock.Inlines.Add(run);

                }
                else
                {
                    textBlock.Inlines.Add(item1);
                }

            }
            if (substrings.Count() > 1 && !SearchedItems.Contains(item))
            {
                SearchedItems.Add(item);
                
            }
        }

     
        internal static TextBlock GetEditableTextBlock(TreeViewItemAdv item)
        {
            if (item != null)
            {
                TextBlock oobj = null;

                if (oobj == null)
                    oobj = VisualUtils.FindDescendant(item.Template.FindName("PART_Header", item) as ContentPresenter, typeof(TextBlock)) as TextBlock;

                return oobj;
            }
            else
            {
                return null;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = string.Empty;

            ClearSearch();
        }

        private void searchtextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(SearchTextBox.Text))
            {
                ClearSearch();
            }
            else
            {
                Search();
            }
        }
        #endregion
        public void Dispose()
        {
            this.UnWireEvents();
            this.Treeview = null;
        }
    }
}
