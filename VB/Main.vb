Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms
Imports DevExpress.XtraEditors


Namespace DXSample
	Partial Public Class Main
		Inherits XtraForm

		Public Sub New()
			InitializeComponent()
			gridControl1.DataSource = DataHelper.GetData(5)
		End Sub

		Private Sub gridViewColumnHeaderExtender1_ColumnCheckedChanged(ByVal sender As Object, ByVal e As ColumnCheckedChangedEventArgs) Handles gridViewColumnHeaderExtender1.ColumnCheckedChanged
			MessageBox.Show(String.Format("{0} Checked: {1}", e.Column.FieldName, e.Checked))
		End Sub
	End Class
End Namespace
