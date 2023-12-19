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
    public partial class Add_Complex_Sale : Form
    {
        private string pcustid;
        private string prodid;
        private int pqty;
        private string pdate;
        private DatabaseManager _DBM;
        
        public Add_Complex_Sale()
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
            //prodid = textBox2.Text;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //pqty = Convert.ToInt32(textBox3.Text);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //pdate = dateTimePicker1.Value.ToString("yyyyMMdd");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pcustid = textBox1.Text;
            prodid = textBox2.Text;
            pqty = Convert.ToInt32(textBox3.Text);
            pdate = dateTimePicker1.Value.ToString("yyyyMMdd");

            string sql = "ADD_COMPLEX_SALE_TO_DB";
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

            pram = new OracleParameter();
            pram.Direction = ParameterDirection.Input;
            pram.Value = pdate;
            pram.OracleDbType = OracleDbType.Varchar2;
            command.Parameters.Add(pram);

            try
            {
                conn.Open();
                command.Transaction = _DBM._connection.BeginTransaction(IsolationLevel.ReadCommitted);
                command.ExecuteNonQuery();

                MessageBox.Show(
                    "Adding Complex Sale. Cust Id: " + pcustid + " Prod ID: " + prodid + "Amount: " + pqty + "\n"+
                    "Added Complex Sale OK"
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
