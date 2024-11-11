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
            FillDataSet();
            return tbl;
        }

        public DataRow GetProductById(int id)
        {
            var row = tbl.Rows.Find(id);
            return row;
        }

        public void InsertProduct(string name, decimal price, short quantity)
        {
            var newRow = tbl.NewRow();
            newRow["ProductName"] = name;
            newRow["UnitPrice"] = price;
            newRow["UnitsInStock"] = quantity;
            tbl.Rows.Add(newRow);

            adp.InsertCommand = cmdBuilder.GetInsertCommand();
            adp.Update(tbl);
        }

        public void UpdateProduct(int id, string name, decimal price, short quantity)
        {
            var row = tbl.Rows.Find(id);
            row["ProductName"] = name;
            row["UnitPrice"] = price;
            row["UnitsInStock"] = quantity;
            
            adp.UpdateCommand = cmdBuilder.GetUpdateCommand();
            adp.Update(tbl);
        }

        public void DeleteProduct(int id)
        {
            var row = tbl.Rows.Find(id);
            row.Delete();

            adp.DeleteCommand = cmdBuilder.GetDeleteCommand();
            adp.Update(tbl);
        }
    }
}
