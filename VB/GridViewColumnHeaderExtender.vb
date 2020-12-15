Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms
Imports DevExpress.Skins
Imports DevExpress.XtraGrid.Views.Grid
Imports System.Drawing
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid.Drawing
Imports DevExpress.Utils.Drawing
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Utils
Imports System.ComponentModel

Namespace DXSample
	Public Class GridViewColumnHeaderExtender
		Inherits Component


        Private _view As GridView = Nothing
        Private ReadOnly checkBoxSize As Size

        Private skipGlyph As Boolean = False
        Public Property DrawCheckBoxByDefault() As Boolean
        Private inHeader As Boolean = False
        Private isCheckBoxCollectionInitialized As Boolean = False
        Private glyphCollection As ImageCollection = Nothing
        Private checkBoxCollection As ImageCollection = Nothing
        Private column As GridColumn = Nothing

        Public Sub New()
            checkBoxSize = New Size(14, 14)
            DrawCheckBoxByDefault = True
        End Sub

        Public Property View() As GridView
            Get
                Return _view
            End Get
            Set(ByVal value As GridView)
                If _view Is value Then
                    Return
                End If
                OnViewChanging()
                _view = value
                OnViewChanged()
            End Set
        End Property

        Protected Overridable Sub OnViewChanging()
            ViewEvents(False)
        End Sub

        Protected Overridable Sub OnViewChanged()
            ViewEvents(True)
        End Sub

        Private Sub OnMouseUp(ByVal sender As Object, ByVal e As MouseEventArgs)
            If Not ColumnHeaderContainsCursor(e.Location) Then
                Return
            End If
            If CheckBoxContainsCursor(e.Location, column) Then
                ResetChecked(column)
                SetCheckBoxState(column, ObjectState.Normal)
                DXMouseEventArgs.GetMouseArgs(e).Handled = True
            End If
        End Sub

        Private Sub OnMouseMove(ByVal sender As Object, ByVal e As MouseEventArgs)
            If Not ColumnHeaderContainsCursor(e.Location) Then
                Return
            End If
            Dim state As ObjectState = ObjectState.Normal
            If CheckBoxContainsCursor(e.Location, column) Then
                state = ObjectState.Hot
            End If
            SetCheckBoxState(column, state)
        End Sub

        Private Sub OnMouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)
            If Not ColumnHeaderContainsCursor(e.Location) Then
                Return
            End If
            If CheckBoxContainsCursor(e.Location, column) Then
                SetCheckBoxState(column, ObjectState.Pressed)
            End If
        End Sub

        Private Sub view_MouseLeave(ByVal sender As Object, ByVal e As EventArgs)
            If Not DrawCheckBoxByDefault AndAlso column IsNot Nothing Then
                inHeader = False
                _view.InvalidateColumnHeader(column)
            End If
        End Sub

        Private Sub ResetChecked(ByVal col As GridColumn)
            CheckColumnTag(column)
            Dim temp As ColumnStateRepository = (TryCast(col.Tag, ColumnStateRepository))
            temp.Checked = Not temp.Checked
            RaiseColumnCheckedChanged(New ColumnCheckedChangedEventArgs(col, temp.Checked))
        End Sub

        Private Sub SetCheckBoxState(ByVal column As GridColumn, ByVal state As ObjectState)
            CheckColumnTag(column)
            TryCast(column.Tag, ColumnStateRepository).State = state
            _view.InvalidateColumnHeader(column)
        End Sub

        Private Function ColumnHeaderContainsCursor(ByVal pt As Point) As Boolean
            Dim hitInfo As GridHitInfo = _view.CalcHitInfo(pt)
            column = hitInfo.Column
            inHeader = hitInfo.HitTest = GridHitTest.Column
            Return inHeader
        End Function

        Private Function CheckBoxContainsCursor(ByVal point As Point, ByVal col As GridColumn) As Boolean
            Dim rect As Rectangle = CalcCheckBoxRectangle(col)
            Return rect.Contains(point)
        End Function

        Private Function CalcCheckBoxRectangle(ByVal col As GridColumn) As Rectangle
            GraphicsInfo.Default.AddGraphics(Nothing)
            Dim viewInfo As GridViewInfo = TryCast(_view.GetViewInfo(), GridViewInfo)
            Dim columnArgs As GridColumnInfoArgs = viewInfo.ColumnsInfo(col)
            Dim rect As Rectangle
            Try
                rect = GetCheckBoxRectangle(columnArgs, GraphicsInfo.Default.Graphics)
            Finally
                GraphicsInfo.Default.ReleaseGraphics()
            End Try

            Return rect
        End Function

        Private Function GetCheckBoxRectangle(ByVal columnArgs As GridColumnInfoArgs, ByVal gr As Graphics) As Rectangle
            Dim columnRect As Rectangle = columnArgs.Bounds
            Dim innerElementsWidth As Integer = CalcInnerElementsMinWidth(columnArgs, gr)
            Dim Rect As New Rectangle(columnRect.Right - innerElementsWidth - checkBoxSize.Width - 5, columnRect.Y + columnRect.Height \ 2 - checkBoxSize.Height \ 2, checkBoxSize.Width, checkBoxSize.Height)
            Return Rect
        End Function

        Private Function CalcInnerElementsMinWidth(ByVal columnArgs As GridColumnInfoArgs, ByVal gr As Graphics) As Integer
            Dim canDrawMode As Boolean = True
            Return columnArgs.InnerElements.CalcMinSize(gr, canDrawMode).Width
        End Function

        Private Sub OnCustomDrawColumnHeader(ByVal sender As Object, ByVal e As ColumnHeaderCustomDrawEventArgs)
            If e.Column Is Nothing Then
                Return
            End If
            DefaultDrawColumnHeader(e)

            If CanDrawCheckBox(e.Column) Then
                DrawCheckBox(e)
            End If

            e.Handled = True
        End Sub

        Private Sub CheckColumnTag(ByVal col As GridColumn)
            If col.Tag IsNot Nothing AndAlso col.Tag.GetType() Is GetType(ColumnStateRepository) Then
                Return
            End If
            col.Tag = New ColumnStateRepository With {
                .State = ObjectState.Normal,
                .Checked = False
            }
        End Sub

        Private Function CanDrawCheckBox(ByVal col As GridColumn) As Boolean
            Return DrawCheckBoxByDefault OrElse (inHeader AndAlso col Is column)
        End Function

        Private Sub DrawCheckBox(ByVal e As ColumnHeaderCustomDrawEventArgs)
            Dim index As Integer = 0
            CheckColumnTag(e.Column)
            Dim temp As ColumnStateRepository = (TryCast(e.Column.Tag, ColumnStateRepository))
            Dim offset As Integer = If(temp.Checked = True, 4, 0)
            Select Case temp.State
                Case ObjectState.Normal
                    index = offset
                Case ObjectState.Hot
                    index = offset + 1
                Case ObjectState.Hot Or ObjectState.Pressed
                    index = offset + 2
            End Select
            Dim rect As Rectangle = CalcCheckBoxRectangle(e.Column)
            CheckImageCollections()
            CheckGlyphCollection()
            e.Cache.DrawImage(checkBoxCollection.Images(index), rect)
            If Not skipGlyph Then e.Cache.DrawImage(glyphCollection.Images(index), rect)
        End Sub

        Private Sub CheckImageCollections()
            If checkBoxCollection Is Nothing Then checkBoxCollection = GetCheckBoxImages()
        End Sub

        Private Sub CheckGlyphCollection()
            If Not skipGlyph AndAlso glyphCollection Is Nothing Then glyphCollection = GetGlyphImages()
        End Sub

        Protected Overridable Function GetCheckBoxImages() As ImageCollection
            Dim skinElement As SkinElement = GetSkinElement()
            If skinElement Is Nothing Then Return Nothing
            Return skinElement.Image.GetImages()
        End Function

        Protected Overridable Function GetGlyphImages() As ImageCollection
            Dim skinElement As SkinElement = GetSkinElement()
            If skinElement Is Nothing Then Return Nothing

            If skinElement.Glyph Is Nothing Then
                skipGlyph = True
                Return Nothing
            End If

            Return skinElement.Glyph.GetImages()
        End Function

        Private Shared Function GetSkinElement() As SkinElement
            Dim skin As Skin = EditorsSkins.GetSkin(DevExpress.LookAndFeel.UserLookAndFeel.[Default])
            Dim skinElement As SkinElement = skin("CheckBox")
            Return skinElement
        End Function

        Private Sub DefaultDrawColumnHeader(ByVal e As ColumnHeaderCustomDrawEventArgs)
            e.Painter.DrawObject(e.Info)
        End Sub

        Private Sub ViewEvents(ByVal subscribe As Boolean)
            If _view Is Nothing Then
                Return
            End If
            If Not subscribe Then
                RemoveHandler _view.CustomDrawColumnHeader, AddressOf OnCustomDrawColumnHeader
                RemoveHandler _view.MouseDown, AddressOf OnMouseDown
                RemoveHandler _view.MouseUp, AddressOf OnMouseUp
                RemoveHandler _view.MouseMove, AddressOf OnMouseMove
                RemoveHandler _view.MouseLeave, AddressOf view_MouseLeave
                Return
            End If

            AddHandler _view.CustomDrawColumnHeader, AddressOf OnCustomDrawColumnHeader
            AddHandler _view.MouseDown, AddressOf OnMouseDown
            AddHandler _view.MouseUp, AddressOf OnMouseUp
            AddHandler _view.MouseMove, AddressOf OnMouseMove
            AddHandler _view.MouseLeave, AddressOf view_MouseLeave
        End Sub

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                ViewEvents(False)
                View = Nothing
            End If

            MyBase.Dispose(disposing)
        End Sub

        Public Event ColumnCheckedChanged As EventHandler(Of ColumnCheckedChangedEventArgs)
        Public Overridable Sub RaiseColumnCheckedChanged(ByVal ea As ColumnCheckedChangedEventArgs)
            Dim handler As EventHandler(Of ColumnCheckedChangedEventArgs) = ColumnCheckedChangedEvent
            If handler IsNot Nothing Then
                handler(_view, ea)
            End If
		End Sub

	End Class
End Namespace