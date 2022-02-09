<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128624491/17.2.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T325446)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->

# Data Grid for Windows Forms - How to add a check box to a column header

This example creates a `GridViewColumnHeaderExtender` component that displays check boxes in column headers of a target [Grid View](https://docs.devexpress.com/WindowsForms/DevExpress.XtraGrid.Views.Grid.GridView).

<img src="https://raw.githubusercontent.com/DevExpress-Examples/gridcontrol-how-to-add-a-check-box-to-a-column-header-t325446/17.2.3+/media/af2b4a40-acaf-11e5-80bf-00155d62480c.png">

Set the `GridViewColumnHeaderExtender.DrawCheckBoxByDefault` option to `false` to only display check boxes when the mouse cursor hovers over column headers:

<img src="https://raw.githubusercontent.com/DevExpress-Examples/gridcontrol-how-to-add-a-check-box-to-a-column-header-t325446/17.2.3+/media/5556cf9f-acb4-11e5-80bf-00155d62480c.png">

The example handles the `GridViewColumnHeaderExtender.ColumnCheckedChanged` event to respond to changes in a column's check state.

<!-- default file list -->
## Files to Look At

* [ColumnCheckedChangedEventArgs.cs](./CS/ColumnCheckedChangedEventArgs.cs) (VB: [ColumnCheckedChangedEventArgs.vb](./VB/ColumnCheckedChangedEventArgs.vb))
* [ColumnStateRepository.cs](./CS/ColumnStateRepository.cs) (VB: [ColumnStateRepository.vb](./VB/ColumnStateRepository.vb))
* [DataHelper.cs](./CS/DataHelper.cs) (VB: [DataHelper.vb](./VB/DataHelper.vb))
* [GridViewColumnHeaderExtender.cs](./CS/GridViewColumnHeaderExtender.cs) (VB: [GridViewColumnHeaderExtender.vb](./VB/GridViewColumnHeaderExtender.vb))
* [Main.cs](./CS/Main.cs) (VB: [Main.vb](./VB/Main.vb))
* [Program.cs](./CS/Program.cs) (VB: [Program.vb](./VB/Program.vb))
<!-- default file list end -->

## See Also
- [How to add a custom button to a column header in a grid](https://www.devexpress.com/Support/Center/p/E2793)

