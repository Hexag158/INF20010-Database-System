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
    public partial class Add_Simple_Sales : Form
    {
        private string prodid;
        private string pcustid;
        private DatabaseManager _DBM;
        private int pqty;
        public Add_Simple_Sales()
        {
            InitializeComponent();
            _DBM = new DatabaseManager("s103806269", "150803");
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //pcustid = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //prodid = textBox2.Text;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //pqty = Convert.ToInt32(textBox3.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pcustid = textBox1.Text;
            prodid = textBox2.Text;
            pqty = Convert.ToInt32(textBox3.Text);

            string sql = "ADD_SIMPLE_SALE_TO_DB";
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
            pram.Value = prodid;
            pram.OracleDbType = OracleDbType.Int32;
            command.Parameters.Add(pram);

            pram = new OracleParameter();
            pram.Direction = ParameterDirection.Input;
            pram.Value = pqty;
            pram.OracleDbType = OracleDbType.Int32;
            command.Parameters.Add(pram);


            try
            {
                conn.Open();
                command.Transaction = _DBM._connection.BeginTransaction(IsolationLevel.ReadCommitted);
                command.ExecuteNonQuery();

                MessageBox.Show(
                    "Adding Simple Sale. Cust Id: " + pcustid +  " Prod ID: " + prodid +  "Qty:"  + pqty + "\n"+
                    "Added Simple Sale OK"
                    );

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
    }
}
