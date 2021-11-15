using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace UtilityClasses
{
    public static class DataTableExtensionsTolist
    {
        public static List<T> ToGenericList<T>(this DataTable datatable, Func<DataRow, T> converter)
        {
            return (from row in datatable.AsEnumerable()
                    select converter(row)).ToList();
        }

    }
}
