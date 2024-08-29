<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128624491/24.2.1%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T325446)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->

# WinForms Data Grid - How to display a check box within a column header

This example creates a `GridViewColumnHeaderExtender` component that displays check boxes in column headers of a [Grid View](https://docs.devexpress.com/WindowsForms/DevExpress.XtraGrid.Views.Grid.GridView).

<img src="https://raw.githubusercontent.com/DevExpress-Examples/gridcontrol-how-to-add-a-check-box-to-a-column-header-t325446/17.2.3+/media/af2b4a40-acaf-11e5-80bf-00155d62480c.png">

Set the `GridViewColumnHeaderExtender.DrawCheckBoxByDefault` option to `false` to display check boxes when the mouse cursor hovers over column headers:

<img src="https://raw.githubusercontent.com/DevExpress-Examples/gridcontrol-how-to-add-a-check-box-to-a-column-header-t325446/17.2.3+/media/5556cf9f-acb4-11e5-80bf-00155d62480c.png">

The example handles the `GridViewColumnHeaderExtender.ColumnCheckedChanged` event to respond to changes in a column's check state.

<!-- default file list -->
## Files to Review

* [ColumnCheckedChangedEventArgs.cs](./CS/ColumnCheckedChangedEventArgs.cs) (VB: [ColumnCheckedChangedEventArgs.vb](./VB/ColumnCheckedChangedEventArgs.vb))
* [ColumnStateRepository.cs](./CS/ColumnStateRepository.cs) (VB: [ColumnStateRepository.vb](./VB/ColumnStateRepository.vb))
* [GridViewColumnHeaderExtender.cs](./CS/GridViewColumnHeaderExtender.cs) (VB: [GridViewColumnHeaderExtender.vb](./VB/GridViewColumnHeaderExtender.vb))
* [Main.cs](./CS/Main.cs) (VB: [Main.vb](./VB/Main.vb))
<!-- default file list end -->

## See Also
- [How to display a custom button in a grid column header](https://www.devexpress.com/Support/Center/p/E2793)
- [Custom Draw Templates](https://docs.devexpress.com/WindowsForms/404153/common-features/html-css-based-desktop-ui/custom-draw-with-html-templates)
- [HTML and CSS Support](https://docs.devexpress.com/WindowsForms/403397/common-features/html-css-based-desktop-ui)
<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=winforms-grid-add-check-box-to-column-header&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=winforms-grid-add-check-box-to-column-header&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
