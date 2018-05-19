Imports DevExpress.XtraGrid.Columns
Imports System
Imports System.Collections.Generic
Imports System.Linq

Namespace DXSample
	Public Class ColumnCheckedChangedEventArgs
		Inherits EventArgs

		Public Sub New(ByVal _col As GridColumn, ByVal _checked As Boolean)
			Column = _col
			Checked = _checked
		End Sub
		Public Property Checked() As Boolean
		Private privateColumn As GridColumn
		Public Property Column() As GridColumn
			Get
				Return privateColumn
			End Get
			Private Set(ByVal value As GridColumn)
				privateColumn = value
			End Set
		End Property
	End Class
End Namespace
