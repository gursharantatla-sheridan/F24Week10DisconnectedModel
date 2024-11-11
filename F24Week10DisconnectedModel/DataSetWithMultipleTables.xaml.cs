using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace F24Week10DisconnectedModel
{
    /// <summary>
    /// Interaction logic for DataSetWithMultipleTables.xaml
    /// </summary>
    public partial class DataSetWithMultipleTables : Window
    {
        public DataSetWithMultipleTables()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(Data.GetConnectionString());

            string query = "select * from Categories; select * from Products";
            SqlDataAdapter adp = new SqlDataAdapter(query, conn);

            DataSet ds = new DataSet();

            adp.Fill(ds);

            ds.Tables[0].TableName = "Categories";
            ds.Tables[1].TableName = "Products";

            DataTable tblCat = ds.Tables[0];
            DataTable tblProd = ds.Tables[1];

            grdCategories.ItemsSource = tblCat.DefaultView;
            grdProducts.ItemsSource = tblProd.DefaultView;
        }
    }
}
