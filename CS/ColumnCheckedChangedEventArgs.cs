using DevExpress.XtraGrid.Columns;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DXSample
{
    public class ColumnCheckedChangedEventArgs : EventArgs
    {
        public ColumnCheckedChangedEventArgs(GridColumn _col, bool _checked)
        {
            Column = _col;
            Checked = _checked;
        }
        public bool Checked { get; set; }
        public GridColumn Column { get; private set; }
    }
}
