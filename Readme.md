<!-- default file list -->
*Files to look at*:

* [ColumnCheckedChangedEventArgs.cs](./CS/ColumnCheckedChangedEventArgs.cs) (VB: [ColumnCheckedChangedEventArgs.vb](./VB/ColumnCheckedChangedEventArgs.vb))
* [ColumnStateRepository.cs](./CS/ColumnStateRepository.cs) (VB: [ColumnStateRepository.vb](./VB/ColumnStateRepository.vb))
* **[GridViewColumnHeaderExtender.cs](./CS/GridViewColumnHeaderExtender.cs) (VB: [GridViewColumnHeaderExtender.vb](./VB/GridViewColumnHeaderExtender.vb))**
* [Main.cs](./CS/Main.cs) (VB: [Main.vb](./VB/Main.vb))
<!-- default file list end -->
# GridControl - How to add a check box to a column header


<p><br>This example demonstrates how to add a check box to a column header.</p>
<br>Pic. 1<img src="https://raw.githubusercontent.com/DevExpress-Examples/gridcontrol-how-to-add-a-check-box-to-a-column-header-t325446/15.1.8+/media/af2b4a40-acaf-11e5-80bf-00155d62480c.png"><br><br><br>Pic.2<img src="https://raw.githubusercontent.com/DevExpress-Examples/gridcontrol-how-to-add-a-check-box-to-a-column-header-t325446/15.1.8+/media/5556cf9f-acb4-11e5-80bf-00155d62480c.png"><br><br><br>
<p>To use this solution in your application, execute the following steps:<br><br><strong>1.</strong> Drop the <strong>GridViewColumnHeaderExtender </strong>component onto the target form.<br><strong>2.</strong> Assign <strong>GridView</strong> to this component.</p>
<br><br>
<p><strong>Description<br></strong><br><strong>GridViewColumnHeaderExtender </strong>provides the <strong>DrawCheckBoxByDefault </strong>property. Set this property to <strong>true</strong> to make check boxes always visible (as shown in Pic. 1).  If you wish to hide these check boxes and show a check box only when the cursor is above a column, set this property to <strong>false </strong>(Pic. 2).<br><br>When a column is checked / unchecked, the <strong>GridViewColumnHeaderExtender.ColumnCheckedChanged </strong>event is raised. You can handle this event in order to notify related objects of this change. The event handler receives an argument of the <strong>ColumnCheckedChangedEventArgs </strong>type containing data related to this event. <br><br><strong>See also:</strong><br><a href="https://www.devexpress.com/Support/Center/p/E2793">How to add a custom button to a column header in a grid</a><br><a href="https://documentation.devexpress.com/#WindowsForms/DevExpressXtraGridViewsGridGridView_CustomDrawColumnHeadertopic">GridView.CustomDrawColumnHeader Event</a></p>

<br/>


