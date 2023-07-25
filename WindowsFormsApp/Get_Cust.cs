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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp
{
    public partial class Get_Cust : Form
    {
        private string pcustid;
        private DatabaseManager _DBM;
        public Get_Cust()
        {
            InitializeComponent();
            _DBM = new DatabaseManager("s103806269", "150803");
        }

        private void Get_Cust_Load(object sender, EventArgs e){}

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            pcustid = textBox1.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sql = "GET_CUST_STRING_FROM_DB";
            OracleConnection conn = _DBM._connection;
            OracleCommand command = _DBM._connection.CreateCommand();
            command.CommandText = sql;
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.Add("returnVal", OracleDbType.Varchar2, ParameterDirection.ReturnValue).Size = 800;

            OracleParameter pram;

            pram = new OracleParameter();
            pram.Direction = ParameterDirection.Input;
            pram.Value = pcustid;
            pram.OracleDbType = OracleDbType.Int32;
            command.Parameters.Add(pram);

            try
            {
                conn.Open();

                command.ExecuteNonQuery();

                MessageBox.Show("Getting Details for CustId: " + pcustid );

                var returnVal = command.Parameters["returnVal"].Value;

                MessageBox.Show("Getting Details for CustId: " + pcustid + "\n" + returnVal);
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
