using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WindowsFormsApp
{
    public partial class menuForm : Form
    {
        private DatabaseManager _DBM;
        public menuForm()
        {
            InitializeComponent();
            _DBM = new DatabaseManager("s103806269", "150803");
        }
        private void button1_Click(object sender, EventArgs e) //ADD CUST 
        {
            Add_Cust Check1 = new Add_Cust();
            Check1.Show();
        }
        private void button2_Click(object sender, EventArgs e) //DELETE ALL PRODUCT 
        {
            string sql = "DELETE_ALL_PRODUCTS_FROM_DB";

            OracleConnection conn = _DBM._connection;
            OracleCommand command = _DBM._connection.CreateCommand();

            command.CommandText = sql;
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.Add("returnVal", OracleDbType.Int32, 10, ParameterDirection.ReturnValue);

            

            try
            {
                conn.Open();
                command.Transaction = _DBM._connection.BeginTransaction(IsolationLevel.ReadCommitted);

                command.ExecuteNonQuery();
                var returnVal = command.Parameters["returnVal"].Value;
                MessageBox.Show("Deleting all product rows. Row deleted: "+ returnVal);

                command.Transaction.Commit();
                MessageBox.Show("Transaction commited");

            }
            catch (Exception ex)
            {
                command.Transaction.Rollback();
                MessageBox.Show(ex.Message, "Error");
            }
            finally
            {
                conn.Close();
                MessageBox.Show("Connection Closed");
            }
        }

        private void button3_Click(object sender, EventArgs e) //DELETE ALL CUST BUTTON
        {
            string sql = "DELETE_ALL_CUSTOMERS_FROM_DB";

            OracleConnection conn = _DBM._connection;
            OracleCommand command = _DBM._connection.CreateCommand();

            command.CommandText = sql;
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.Add("returnVal", OracleDbType.Int32, 10, ParameterDirection.ReturnValue);

            try
            {
                conn.Open();
                command.Transaction = _DBM._connection.BeginTransaction(IsolationLevel.ReadCommitted);

                command.ExecuteNonQuery();
                var returnVal = command.Parameters["returnVal"].Value;

                MessageBox.Show("Deleting all customer rows. Row deleted: " + returnVal);

                command.Transaction.Commit();
                MessageBox.Show("Transaction commited");

            }
            catch (Exception ex)
            {
                command.Transaction.Rollback();
                MessageBox.Show(ex.Message, "Error");
            }
            finally
            {
                conn.Close();
                MessageBox.Show("Connection Closed");
            }
        }
        private void button4_Click(object sender, EventArgs e) // get cust string
        {
            Get_Cust Check2 = new Get_Cust();
            Check2.Show();
        }
        private void button5_Click(object sender, EventArgs e) // add prod
        {
            Add_Prod Check3 = new Add_Prod();
            Check3.Show();
        }

        private void button6_Click(object sender, EventArgs e) // update cust sale
        {
            Upd_Custsales Check4 = new Upd_Custsales();
            Check4.Show();
        } 
        private void button7_Click(object sender, EventArgs e) // update product sale
        {
            Upd_prod Check5 =  new Upd_prod();
            Check5.Show();
        }

        private void button8_Click(object sender, EventArgs e)// get prod string
        {
            Get_prod Check6 = new Get_prod();
            Check6.Show();
        }
        private void button9_Click(object sender, EventArgs e)// update customer status
        {
            Upd_Cust_Status Check7 = new Upd_Cust_Status();
            Check7.Show();
        }

        private void button10_Click(object sender, EventArgs e)// add simple sales
        {
            Add_Simple_Sales Check8 = new Add_Simple_Sales();
            Check8.Show();
        }

        private void button11_Click(object sender, EventArgs e)// sum cust sales
        {
            string sql = "SUM_CUST_SALESYTD";

            OracleConnection conn = _DBM._connection;
            OracleCommand command = _DBM._connection.CreateCommand();

            command.CommandText = sql;
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.Add("returnVal", OracleDbType.Int32, 10, ParameterDirection.ReturnValue);

            try
            {
                conn.Open();
                command.Transaction = _DBM._connection.BeginTransaction(IsolationLevel.ReadCommitted);

                command.ExecuteNonQuery();
                var returnVal = command.Parameters["returnVal"].Value.ToString();

                MessageBox.Show("All Customer Total: " + returnVal);
            }
            catch (Exception ex)
            {
                command.Transaction.Rollback();
                MessageBox.Show(ex.Message, "Error");
            }
            finally
            {
                conn.Close();
                MessageBox.Show("Connection Closed");
            }
        }
        private void button12_Click(object sender, EventArgs e)//close button
        {
            Application.Exit();
        }

        private void Group1_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //------------------------------------------------------------------------------------------------------------------------------------
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //------------------------------------------------------------------------------------------------------------------------------------

        private void button13_Click(object sender, EventArgs e) // get all product
        {
            string sql = "GET_ALL_PKG.GET_ALLPROD_FROM_DB";
            OracleConnection conn = _DBM._connection;
            OracleCommand command = _DBM._connection.CreateCommand();
            command.CommandText = sql;
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.Add("returnVal", OracleDbType.RefCursor, ParameterDirection.ReturnValue);

            OracleDataReader reader;

            Int32 prodid;
            string prodname;
            double saleYTD, price;
            string Fresult = "";


            try
            {
                conn.Open();
                //command.ExecuteReader();
                reader = command.ExecuteReader();
                MessageBox.Show("Start");
                while (reader.Read())
                {
                    prodid = reader.GetInt32(0);
                    prodname = reader.GetString(1);
                    price = reader.GetDouble(2);
                    saleYTD = reader.GetDouble(3);

                    string vresult = "ProdID: " + prodid.ToString() + " Name: " + prodname.ToString() + " Price: " + price.ToString() + " SalesYTD: " + saleYTD.ToString() + ".\n\n";
                    Fresult += vresult;
                }

                MessageBox.Show(Fresult);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            finally
            {
                conn.Close();
                MessageBox.Show("Connection Closed");
            }
        }

        private void button14_Click(object sender, EventArgs e)// get all customer
        {
            string sql = "GET_ALL_PKG.GET_ALLCUST";
            OracleConnection conn = _DBM._connection;
            OracleCommand command = _DBM._connection.CreateCommand();
            command.CommandText = sql;
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.Add("returnVal", OracleDbType.RefCursor, ParameterDirection.ReturnValue);

            OracleDataReader reader;

            Int32 custid;
            string custname, status;
            double saleYTD;
            string Fresult = "";


            try
            {
                conn.Open();
                //command.ExecuteReader();
                reader = command.ExecuteReader();
                MessageBox.Show("Start");
                while (reader.Read())
                {
                    custid = reader.GetInt32(0);
                    custname = reader.GetString(1);
                    status = reader.GetString(3);
                    saleYTD = reader.GetDouble(2);

                    string vresult = "CustID: " + custid.ToString() + " Name: " + custname.ToString() + " Price: " + status.ToString() + " SalesYTD: " + saleYTD.ToString() + ".\n\n";
                    Fresult += vresult;
                }

                MessageBox.Show(Fresult);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            finally
            {
                conn.Close();
                MessageBox.Show("Connection Closed");
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------
        //====================================================================================================================================
        //------------------------------------------------------------------------------------------------------------------------------------

        private void button15_Click(object sender, EventArgs e) //add location
        {
            Add_location Check9 = new Add_location();
            Check9.Show();
        }

        private void button16_Click(object sender, EventArgs e) //add complex sale
        {
            Add_Complex_Sale Check10 = new Add_Complex_Sale();
            Check10.Show();
        }

        private void button17_Click(object sender, EventArgs e) //get all sales
        {
            string sql = "GET_ALL_PKG.GET_ALLSALES_FROM_DB";
            OracleConnection conn = _DBM._connection;
            OracleCommand command = _DBM._connection.CreateCommand();
            command.CommandText = sql;
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.Add("returnVal", OracleDbType.RefCursor, ParameterDirection.ReturnValue);

            OracleDataReader reader = null;

            Int32 proid,custid ,saleid;
            DateTime saledate;
            double qty,price;
            string Fresult = "";
        

            try
            {
                conn.Open();
                //command.ExecuteReader();
                reader = command.ExecuteReader();
                MessageBox.Show("Start");
                while (reader.Read())
                {
                   saleid = reader.GetInt32(0);
                   custid = reader.GetInt32(1);
                   proid = reader.GetInt32(2);
                   saledate = reader.GetDateTime(5);
                   qty = reader.GetDouble(3);
                   price = reader.GetDouble(4);

                   string vresult = "SaleId: " + saleid.ToString() +" CustId: " + custid.ToString() + " ProdId: " + proid.ToString() 
                                    + " Date: " + saledate.ToString("yyyy-mm-dd") + " Amount: " + (qty*price).ToString() + ".\n\n";
                   Fresult += vresult;
                }

                MessageBox.Show(Fresult);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            finally
            {
                conn.Close();
                MessageBox.Show("Connection Closed");
            }
        }

        private void button18_Click(object sender, EventArgs e) //count product sale
        {
            Count_Product_Sales Check11 =new Count_Product_Sales();
            Check11.Show();
        }

        private void button19_Click(object sender, EventArgs e) //delete sale
        {
            string sql = "DELETE_SALE_FROM_DB";

            OracleConnection conn = _DBM._connection;
            OracleCommand command = _DBM._connection.CreateCommand();

            command.CommandText = sql;
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.Add("returnVal", OracleDbType.Int32,50, ParameterDirection.ReturnValue);

            try
            {
                conn.Open();
                command.Transaction = _DBM._connection.BeginTransaction(IsolationLevel.ReadCommitted);

                command.ExecuteNonQuery();

                var returnVal = command.Parameters["returnVal"].Value.ToString();

                MessageBox.Show("Deleted Smallest Sale OK. SaleID: " + returnVal);

                command.Transaction.Commit();
                MessageBox.Show("Transaction commited");
            }
            catch (Exception ex)
            {
                command.Transaction.Rollback();
                MessageBox.Show(ex.Message, "Error");
            }
            finally
            {
                conn.Close();
                MessageBox.Show("Connection Closed");
            }
        }

        private void button20_Click(object sender, EventArgs e) //delete all sale
        {
            string sql = "DELETE_ALL_SALES_FROM_DB";

            OracleConnection conn = _DBM._connection;
            OracleCommand command = _DBM._connection.CreateCommand();

            command.CommandText = sql;
            command.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                conn.Open();
                command.Transaction = _DBM._connection.BeginTransaction(IsolationLevel.ReadCommitted);

                command.ExecuteNonQuery();

                MessageBox.Show("Deleting OK" );

                command.Transaction.Commit();
                MessageBox.Show("Transaction commited");

            }
            catch (Exception ex)
            {
                command.Transaction.Rollback();
                MessageBox.Show(ex.Message, "Error");
            }
            finally
            {
                conn.Close();
                MessageBox.Show("Connection Closed");
            }
        }

        private void button21_Click(object sender, EventArgs e) //delete customer
        {
            Delete_customer Check12 = new Delete_customer();
            Check12.Show();
        }

        private void button22_Click(object sender, EventArgs e) //delete product
        {
            Delete_Prod Check12A = new Delete_Prod();
            Check12A.Show();
        }
    }
}
