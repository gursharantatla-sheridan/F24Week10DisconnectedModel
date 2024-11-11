using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace F24Week10DisconnectedModel
{
    public class Crud
    {
        SqlConnection conn;
        SqlDataAdapter adp;
        SqlCommandBuilder cmdBuilder;
        DataSet ds;
        DataTable tbl;

        public Crud()
        {
            string query = "select ProductID, ProductName, UnitPrice, UnitsInStock from Products";

            conn = new SqlConnection(Data.GetConnectionString());
            adp = new SqlDataAdapter(query, conn);
            cmdBuilder = new SqlCommandBuilder(adp);

            FillDataSet();
        }

        private void FillDataSet()
        {
            ds = new DataSet();

            adp.Fill(ds);
            tbl = ds.Tables[0];

            // define the primary key
            DataColumn[] pk = new DataColumn[1];
            pk[0] = tbl.Columns["ProductID"];
            pk[0].AutoIncrement = true;
            tbl.PrimaryKey = pk;
        }

        public DataTable GetAllProducts()
        {
            return tbl;
        }

        public DataRow GetProductById(int id)
        {
            var row = tbl.Rows.Find(id);
            return row;
        }
    }
}
