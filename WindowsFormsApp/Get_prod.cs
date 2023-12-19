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
    public partial class Get_prod : Form
    {
        private string pprodid;
        private DatabaseManager _DBM;
        public Get_prod()
        {
            InitializeComponent();
            _DBM = new DatabaseManager("s103806269", "150803");
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            pprodid = textBox1.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sql = "GET_PROD_STRING_FROM_DB";
            OracleConnection conn = _DBM._connection;
            OracleCommand command = _DBM._connection.CreateCommand();
            command.CommandText = sql;
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.Add("returnVal", OracleDbType.Varchar2, ParameterDirection.ReturnValue).Size = 800;

            OracleParameter pram;

            pram = new OracleParameter();
            pram.Direction = ParameterDirection.Input;
            pram.Value = pprodid;
            pram.OracleDbType = OracleDbType.Int32;
            command.Parameters.Add(pram);

            try
            {
                conn.Open();

                command.ExecuteNonQuery();

                MessageBox.Show("Getting Details for ProdId: " + pprodid);

                var returnVal = command.Parameters["returnVal"].Value;

                MessageBox.Show("Getting Details for ProdId: " + pprodid + "\n" + returnVal);
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
