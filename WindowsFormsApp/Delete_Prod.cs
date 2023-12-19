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
    public partial class Delete_Prod : Form
    {
        private string pprodid;
        private DatabaseManager _DBM;
        public Delete_Prod()
        {
            InitializeComponent();
            _DBM = new DatabaseManager("s103806269", "150803");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            pprodid = textBox1.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "DELETE_PROD_FROM_DB";
            OracleConnection conn = _DBM._connection;
            OracleCommand command = _DBM._connection.CreateCommand();
            command.CommandText = sql;
            command.CommandType = System.Data.CommandType.StoredProcedure;

            OracleParameter pram;

            pram = new OracleParameter();
            pram.Direction = ParameterDirection.Input;
            pram.Value = pprodid;
            pram.OracleDbType = OracleDbType.Int32;
            command.Parameters.Add(pram);


            try
            {
                conn.Open();
                command.Transaction = _DBM._connection.BeginTransaction(IsolationLevel.ReadCommitted);

                command.ExecuteNonQuery();

                MessageBox.Show("Product " + pprodid+ " Deleted");

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
