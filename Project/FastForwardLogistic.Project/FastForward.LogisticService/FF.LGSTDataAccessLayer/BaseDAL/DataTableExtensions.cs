using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.DataAccessLayer.BaseDAL
{
    public static class DataTableExtensions
    {
        public static List<T> ToGenericList<T>(this DataTable datatable, Func<DataRow, T> converter)
        {
            return (from row in datatable.AsEnumerable()
                    select converter(row)).ToList();
        }
    }
}
