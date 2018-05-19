using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DXSample
{
    public class DataHelper
    {
        public static DataTable GetData(int count)
        {
            var dt = new DataTable();
            dt.Columns.Add(new DataColumn("Id", typeof(int)));
            dt.Columns.Add(new DataColumn("Text", typeof(string)));
            dt.Columns.Add(new DataColumn("Date", typeof(DateTime)));

            int j = 0;
            Random r = new Random();
            for (int i = 0; i < count; i++)
            {
                if (i % r.Next(1, 5) == 0)
                    j += r.Next(1, 10);
                dt.Rows.Add(new object[] { j, string.Format("Text{0}", i), DateTime.Now });
            }

            return dt;
        }
    }
}
