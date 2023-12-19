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
    public partial class Upd_prod : Form
    {
        private string prodid;
        private double pamt;
        private DatabaseManager _DBM;
        public Upd_prod()
        {
            InitializeComponent();
            _DBM = new DatabaseManager("s103806269", "150803");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //prodid = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //pamt = Convert.ToDouble(textBox2.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            prodid = textBox1.Text;
            pamt = Convert.ToDouble(textBox2.Text);

            string sql = "UPD_PROD_SALESYTD_IN_DB";
            OracleConnection conn = _DBM._connection;
            OracleCommand command = _DBM._connection.CreateCommand();
            command.CommandText = sql;
            command.CommandType = System.Data.CommandType.StoredProcedure;

            OracleParameter pram;

            pram = new OracleParameter();
            pram.Direction = ParameterDirection.Input;
            pram.Value = prodid;
            pram.OracleDbType = OracleDbType.Int32;
            command.Parameters.Add(pram);

            pram = new OracleParameter();
            pram.Direction = ParameterDirection.Input;
            pram.Value = pamt;
            pram.OracleDbType = OracleDbType.Double;
            command.Parameters.Add(pram);

            try
            {
                conn.Open();
                command.Transaction = _DBM._connection.BeginTransaction(IsolationLevel.ReadCommitted);

                command.ExecuteNonQuery();

                MessageBox.Show("Update OK");

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
