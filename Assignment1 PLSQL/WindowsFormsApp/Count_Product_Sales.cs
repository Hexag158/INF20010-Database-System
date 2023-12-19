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

namespace WindowsFormsApp
{
    public partial class Count_Product_Sales : Form
    {
        private string pdays;
        private DatabaseManager _DBM;
        public Count_Product_Sales()
        {
            InitializeComponent();
            _DBM = new DatabaseManager("s103806269", "150803");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //pdays = textBox1.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pdays = textBox1.Text;

            string sql = "COUNT_PRODUCT_SALES_FROM_DB";

            OracleConnection conn = _DBM._connection;
            OracleCommand command = _DBM._connection.CreateCommand();

            command.CommandText = sql;
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.Add("returnVal", OracleDbType.Int32, 40, ParameterDirection.ReturnValue);

            OracleParameter pram;

            pram = new OracleParameter();
            pram.Direction = ParameterDirection.Input;
            pram.Value = pdays;
            pram.OracleDbType = OracleDbType.Int32;
            command.Parameters.Add(pram);

            try
            {
                conn.Open();

                command.ExecuteNonQuery();
                var returnVal = command.Parameters["returnVal"].Value.ToString();

                MessageBox.Show("Total number of sales: " + returnVal);
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
    }
}
