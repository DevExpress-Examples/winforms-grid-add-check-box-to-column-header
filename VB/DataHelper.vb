Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Linq

Namespace DXSample
	Public Class DataHelper
		Public Shared Function GetData(ByVal count As Integer) As DataTable
			Dim dt = New DataTable()
			dt.Columns.Add(New DataColumn("Id", GetType(Integer)))
			dt.Columns.Add(New DataColumn("Text", GetType(String)))
			dt.Columns.Add(New DataColumn("Date", GetType(Date)))

			Dim j As Integer = 0
			Dim r As New Random()
			For i As Integer = 0 To count - 1
				If i Mod r.Next(1, 5) = 0 Then
					j += r.Next(1, 10)
				End If
				dt.Rows.Add(New Object() { j, String.Format("Text{0}", i), Date.Now })
			Next i

			Return dt
		End Function
	End Class
End Namespace
