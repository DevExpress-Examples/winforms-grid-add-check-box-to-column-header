using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;


namespace DXSample
{
    public partial class Main : XtraForm
    {
        public Main()
        {
            InitializeComponent();
            gridControl1.DataSource = DataHelper.GetData(5);
        }

        private void gridViewColumnHeaderExtender1_ColumnCheckedChanged(object sender, ColumnCheckedChangedEventArgs e)
        {
            MessageBox.Show(string.Format("{0} Checked: {1}", e.Column.FieldName, e.Checked));
        }
    }
}
