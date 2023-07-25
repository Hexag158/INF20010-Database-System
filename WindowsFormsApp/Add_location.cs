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
    public partial class Add_location : Form
    {
        private string ploccode;
        private string pminqty;
        private string pmaxqty;
        private DatabaseManager _DBM;
        public Add_location()
        {
            InitializeComponent();
            _DBM = new DatabaseManager("s103806269", "150803");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //ploccode = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //pminqty = textBox2.Text;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //pmaxqty = textBox3.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ploccode = textBox1.Text;
            pminqty = textBox2.Text;
            pmaxqty = textBox3.Text;

            string sql = "ADD_LOCATION_TO_DB";
            OracleConnection conn = _DBM._connection;
            OracleCommand command = _DBM._connection.CreateCommand();
            command.CommandText = sql;
            command.CommandType = System.Data.CommandType.StoredProcedure;

            OracleParameter pram;

            pram = new OracleParameter();
            pram.Direction = ParameterDirection.Input;
            pram.Value = ploccode;
            pram.OracleDbType = OracleDbType.Varchar2;
            command.Parameters.Add(pram);

            pram = new OracleParameter();
            pram.Direction = ParameterDirection.Input;
            pram.Value = pminqty;
            pram.OracleDbType = OracleDbType.Int32;
            command.Parameters.Add(pram);

            pram = new OracleParameter();
            pram.Direction = ParameterDirection.Input;
            pram.Value = pmaxqty;
            pram.OracleDbType = OracleDbType.Int32;
            command.Parameters.Add(pram);

            try
            {
                conn.Open();
                command.Transaction = _DBM._connection.BeginTransaction(IsolationLevel.ReadCommitted);
   
                command.ExecuteNonQuery();

                MessageBox.Show("Location Added OK");

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
