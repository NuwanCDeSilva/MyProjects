using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
  public  class MasterSalesPriorityHierarchy
    {
        /// <summary>
        /// Written By Prabhath on 05/07/2012
        /// Table: MST_PC_INFO (in EMS)
        /// </summary>

        #region Private Members
        private Boolean _mpi_act;
        private string _mpi_cd;
        private string _mpi_com_cd;
        private string _mpi_pc_cd;
        private string _mpi_tp;
        private string _mpi_val;
      //Added by Prabhath on 03/04/2013
        private string _description;
        #endregion

        public Boolean Mpi_act { get { return _mpi_act; } set { _mpi_act = value; } }
        public string Mpi_cd { get { return _mpi_cd; } set { _mpi_cd = value; } }
        public string Mpi_com_cd { get { return _mpi_com_cd; } set { _mpi_com_cd = value; } }
        public string Mpi_pc_cd { get { return _mpi_pc_cd; } set { _mpi_pc_cd = value; } }
        public string Mpi_tp { get { return _mpi_tp; } set { _mpi_tp = value; } }
        public string Mpi_val { get { return _mpi_val; } set { _mpi_val = value; } }
        //Added by Prabhath on 03/04/2013
        public string Description { get { return _description; } set { _description = value; } }

        public static MasterSalesPriorityHierarchy Converter(DataRow row)
        {
            return new MasterSalesPriorityHierarchy
            {
                Mpi_act = row["MPI_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["MPI_ACT"]),
                Mpi_cd = row["MPI_CD"] == DBNull.Value ? string.Empty : row["MPI_CD"].ToString(),
                Mpi_com_cd = row["MPI_COM_CD"] == DBNull.Value ? string.Empty : row["MPI_COM_CD"].ToString(),
                Mpi_pc_cd = row["MPI_PC_CD"] == DBNull.Value ? string.Empty : row["MPI_PC_CD"].ToString(),
                Mpi_tp = row["MPI_TP"] == DBNull.Value ? string.Empty : row["MPI_TP"].ToString(),
                Mpi_val = row["MPI_VAL"] == DBNull.Value ? string.Empty : row["MPI_VAL"].ToString()

            };
        }

        public static MasterSalesPriorityHierarchy ConvertVal(DataRow row)
        {
            return new MasterSalesPriorityHierarchy
            {
                Mpi_val = row["MPI_VAL"] == DBNull.Value ? string.Empty : row["MPI_VAL"].ToString()

            };
        }


        public static MasterSalesPriorityHierarchy ConvertWithDescription(DataRow row)
        {
            return new MasterSalesPriorityHierarchy
            {
                Mpi_act = row["MPI_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["MPI_ACT"]),
                Mpi_cd = row["MPI_CD"] == DBNull.Value ? string.Empty : row["MPI_CD"].ToString(),
                Mpi_com_cd = row["MPI_COM_CD"] == DBNull.Value ? string.Empty : row["MPI_COM_CD"].ToString(),
                Mpi_pc_cd = row["MPI_PC_CD"] == DBNull.Value ? string.Empty : row["MPI_PC_CD"].ToString(),
                Mpi_tp = row["MPI_TP"] == DBNull.Value ? string.Empty : row["MPI_TP"].ToString(),
                Mpi_val = row["MPI_VAL"] == DBNull.Value ? string.Empty : row["MPI_VAL"].ToString(),
                Description = row["Description"] == DBNull.Value ? string.Empty : row["Description"].ToString()

            };
        }


    }
}
