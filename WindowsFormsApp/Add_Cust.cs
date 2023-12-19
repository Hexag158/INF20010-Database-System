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
    public partial class Add_Cust : Form
    {
        private string pcustid;
        private string pcustname;
        private DatabaseManager _DBM;

        public Add_Cust()
        {
            InitializeComponent();
            _DBM = new DatabaseManager("s103806269", "150803");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //pcustid = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //pcustname = textBox2.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pcustid = textBox1.Text;
            pcustname = textBox2.Text;

            string sql = "ADD_CUST_TO_DB";
            OracleConnection conn = _DBM._connection;
            OracleCommand command = _DBM._connection.CreateCommand();
            command.CommandText = sql;
            command.CommandType = System.Data.CommandType.StoredProcedure;

            OracleParameter pram;

            pram = new OracleParameter();
            pram.Direction = ParameterDirection.Input;
            pram.Value = pcustid;
            pram.OracleDbType = OracleDbType.Int32;
            command.Parameters.Add(pram);

            pram = new OracleParameter();
            pram.Direction = ParameterDirection.Input;
            pram.Value = pcustname;
            pram.OracleDbType = OracleDbType.Varchar2;
            command.Parameters.Add(pram);

            try
            {
                conn.Open();
                command.Transaction = _DBM._connection.BeginTransaction(IsolationLevel.ReadCommitted);
                //command.CommandText = sql;
                //command.CommandType = System.Data.CommandType.StoredProcedure;
                command.ExecuteNonQuery();

                MessageBox.Show("Customer Added OK");

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



        private void Button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = textBox1.Text = null;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}

