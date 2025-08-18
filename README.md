# How to search and highlight item in TreeViewAdv

This example demonstrates how to search and highlighted the item of `TreeViewAdv` control in WPF platform.

# About Sample

This sample provides the demo on how to search and highlight the text in `TreeViewAdv`.

To search and highlight the item in `TreeViewAdv`, Search method has been implemented which is used to compare the search text with model items. Highlighted the matched text in TextBlock inside the `TreeViewItemAdv`.

``` c#
if (mainitem != null && MatchSearchText(model.Header))
{
    //Function which is used to apply inline to the TextBlock
    ApplyInline(mainitem, false);                      
}
```

Options like Search, FindPrevious, Find Next, SearchType (Contains, EndsWith, StartsWith options) has been used to match search text using ApplyInline method and apply the SearchBrush for highlighting the search text.

``` c#
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
```
