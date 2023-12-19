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
    public partial class Upd_Cust_Status : Form
    {
        private string pcustid;
        private string pstatus;
        private DatabaseManager _DBM;
        public Upd_Cust_Status()
        {
            InitializeComponent();
            pstatus = "SUSPEND";
            _DBM = new DatabaseManager("s103806269", "150803");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //pcustid = textBox1.Text;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //if (radioButton1.Checked)
            //{
            //    pstatus = "OK";
            //}
            //pstatus = "SUSPEND";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pcustid = textBox1.Text;
            if (radioButton1.Checked)
            {
                pstatus = "OK";
            }
            else {pstatus = "SUSPEND";}

            string sql = "UPD_CUST_STATUS_IN_DB";
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
            pram.Value = pstatus;
            pram.OracleDbType = OracleDbType.Varchar2;
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
