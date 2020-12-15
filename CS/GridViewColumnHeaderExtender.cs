using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.XtraGrid.Views.Grid;
using System.Drawing;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Drawing;
using DevExpress.Utils.Drawing;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils;
using System.ComponentModel;

namespace DXSample
{
    public class GridViewColumnHeaderExtender : Component
    {
        private GridView view = null;
        readonly Size checkBoxSize;
        public bool DrawCheckBoxByDefault { get; set; }
        bool inHeader = false;
        ImageCollection checkBoxCollection = null;
        ImageCollection glyphCollection = null;
        GridColumn column = null;

        public GridViewColumnHeaderExtender()
        {
            checkBoxSize = new Size(14, 14);
            DrawCheckBoxByDefault = true;
        }

        public GridView View
        {
            get
            {
                return view;
            }
            set
            {
                if (view == value) return;
                OnViewChanging();
                view = value;
                OnViewChanged();
            }
        }

        protected virtual void OnViewChanging()
        {
            ViewEvents(false);
        }

        protected virtual void OnViewChanged()
        {
            ViewEvents(true);
        }

        void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (!ColumnHeaderContainsCursor(e.Location)) return;
            if (CheckBoxContainsCursor(e.Location, column))
            {
                ResetChecked(column);
                SetCheckBoxState(column, ObjectState.Normal);
                DXMouseEventArgs.GetMouseArgs(e).Handled = true;
            }
        }

        void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!ColumnHeaderContainsCursor(e.Location)) return;
            ObjectState state = ObjectState.Normal;
            if (CheckBoxContainsCursor(e.Location, column))
                state = ObjectState.Hot;
            SetCheckBoxState(column, state);
        }

        void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (!ColumnHeaderContainsCursor(e.Location)) return;
            if (CheckBoxContainsCursor(e.Location, column))
                SetCheckBoxState(column, ObjectState.Pressed);
        }

        void view_MouseLeave(object sender, EventArgs e)
        {
            if (!DrawCheckBoxByDefault && column != null)
            {
                inHeader = false;
                view.InvalidateColumnHeader(column);
            }
        }

        private void ResetChecked(GridColumn col)
        {
            CheckColumnTag(column);
            ColumnStateRepository temp = (col.Tag as ColumnStateRepository);
            temp.Checked = !temp.Checked;
            RaiseColumnCheckedChanged(new ColumnCheckedChangedEventArgs(col, temp.Checked));
        }

        private void SetCheckBoxState(GridColumn column, ObjectState state)
        {
            CheckColumnTag(column);
            (column.Tag as ColumnStateRepository).State = state;
            view.InvalidateColumnHeader(column);
        }

        private bool ColumnHeaderContainsCursor(Point pt)
        {
            GridHitInfo hitInfo = view.CalcHitInfo(pt);
            column = hitInfo.Column;
            return inHeader = hitInfo.HitTest == GridHitTest.Column;
        }

        private bool CheckBoxContainsCursor(Point point, GridColumn col)
        {
            Rectangle rect = CalcCheckBoxRectangle(col);
            return rect.Contains(point);
        }

        private Rectangle CalcCheckBoxRectangle(GridColumn col)
        {
            GraphicsInfo.Default.AddGraphics(null);
            GridViewInfo viewInfo = view.GetViewInfo() as GridViewInfo;
            GridColumnInfoArgs columnArgs = viewInfo.ColumnsInfo[col];
            Rectangle rect;
            try
            {
                rect = GetCheckBoxRectangle(columnArgs, GraphicsInfo.Default.Graphics);
            }
            finally
            {
                GraphicsInfo.Default.ReleaseGraphics();
            }

            return rect;
        }

        private Rectangle GetCheckBoxRectangle(GridColumnInfoArgs columnArgs, Graphics gr)
        {
            Rectangle columnRect = columnArgs.Bounds;
            int innerElementsWidth = CalcInnerElementsMinWidth(columnArgs, gr);
            Rectangle Rect = new Rectangle(columnRect.Right - innerElementsWidth - checkBoxSize.Width - 5,
                columnRect.Y + columnRect.Height / 2 - checkBoxSize.Height / 2, checkBoxSize.Width, checkBoxSize.Height);
            return Rect;
        }

        private int CalcInnerElementsMinWidth(GridColumnInfoArgs columnArgs, Graphics gr)
        {
            bool canDrawMode = true;
            return columnArgs.InnerElements.CalcMinSize(gr, ref canDrawMode).Width + 5;
        }

        void OnCustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            if (e.Column == null) return;
            DefaultDrawColumnHeader(e);

            if (CanDrawCheckBox(e.Column))
                DrawCheckBox(e);

            e.Handled = true;
        }

        private void CheckColumnTag(GridColumn col)
        {
            if (col.Tag != null && col.Tag.GetType() == typeof(ColumnStateRepository)) return;
            col.Tag = new ColumnStateRepository { State = ObjectState.Normal, Checked = false };
        }

        private bool CanDrawCheckBox(GridColumn col)
        {
            return DrawCheckBoxByDefault || (inHeader && col == column);
        }

        private void DrawCheckBox(ColumnHeaderCustomDrawEventArgs e)
        {
            int index = 0;
            CheckColumnTag(e.Column);
            ColumnStateRepository temp = (e.Column.Tag as ColumnStateRepository);
            int offset = temp.Checked == true ? 4 : 0;
            switch (temp.State)
            {
                case ObjectState.Normal:
                    index = offset;
                    break;
                case ObjectState.Hot:
                    index = offset + 1;
                    break;
                case ObjectState.Hot | ObjectState.Pressed:
                    index = offset + 2;
                    break;
            }
            Rectangle rect = CalcCheckBoxRectangle(e.Column);
            CheckImageCollections();
            CheckGlyphCollection();
            e.Cache.DrawImage(checkBoxCollection.Images[index], rect);
            if (!skipGlyph)
                e.Cache.DrawImage(glyphCollection.Images[index], rect);
        }

        private void CheckImageCollections()
        {
            if (checkBoxCollection == null)
                checkBoxCollection = GetCheckBoxImages();
        }

        private void CheckGlyphCollection()
        {
            if (!skipGlyph && glyphCollection == null)
                glyphCollection = GetGlyphImages();

        }

        protected virtual ImageCollection GetCheckBoxImages()
        {
            SkinElement skinElement = GetSkinElement();
            if (skinElement == null)
                return null;

            return skinElement.Image.GetImages();
        }

        bool skipGlyph = false;
        protected virtual ImageCollection GetGlyphImages()
        {
            SkinElement skinElement = GetSkinElement();
            if (skinElement == null)
                return null;
            if (skinElement.Glyph == null)
            {
                skipGlyph = true;
                return null;
            }
            return skinElement.Glyph.GetImages();
        }

        private static SkinElement GetSkinElement()
        {
            Skin skin = EditorsSkins.GetSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default);
            SkinElement skinElement = skin["CheckBox"];
            return skinElement;
        }
        private void DefaultDrawColumnHeader(ColumnHeaderCustomDrawEventArgs e)
        {
            e.Painter.DrawObject(e.Info);
        }

        private void ViewEvents(bool subscribe)
        {
            if (view == null) return;
            if (!subscribe)
            {
                view.CustomDrawColumnHeader -= OnCustomDrawColumnHeader;
                view.MouseDown -= OnMouseDown;
                view.MouseUp -= OnMouseUp;
                view.MouseMove -= OnMouseMove;
                view.MouseLeave -= view_MouseLeave;
                return;
            }

            view.CustomDrawColumnHeader += OnCustomDrawColumnHeader;
            view.MouseDown += OnMouseDown;
            view.MouseUp += OnMouseUp;
            view.MouseMove += OnMouseMove;
            view.MouseLeave += view_MouseLeave;
        }

        protected override void Dispose(bool disposing)
        {
            ViewEvents(false);
            base.Dispose(disposing);
        }

        public event EventHandler<ColumnCheckedChangedEventArgs> ColumnCheckedChanged;
        public virtual void RaiseColumnCheckedChanged(ColumnCheckedChangedEventArgs ea)
        {
            EventHandler<ColumnCheckedChangedEventArgs> handler = ColumnCheckedChanged;
            if (handler != null)
                handler(view, ea);
        }
    }
}