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
    public partial class Add_Prod : Form
    {
        private string prodid;
        private string prodname;
        private DatabaseManager _DBM;
        private double proprice;
        public Add_Prod()
        {
            InitializeComponent();
            _DBM = new DatabaseManager("s103806269", "150803");
        }

        private void Add_Prod_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //prodid = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //prodname = textBox2.Text;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //proprice = Convert.ToDouble(textBox3.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            prodid = prodname = null;
            proprice = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            prodid = textBox1.Text;
            prodname = textBox2.Text;
            proprice = Convert.ToDouble(textBox3.Text);

            string sql = "ADD_PRODUCT_TO_DB";
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
            pram.Value = prodname;
            pram.OracleDbType = OracleDbType.Varchar2;
            command.Parameters.Add(pram);

            pram = new OracleParameter();
            pram.Direction = ParameterDirection.Input;
            pram.Value = proprice;
            pram.OracleDbType = OracleDbType.Double;
            command.Parameters.Add(pram);


            try
            {
                conn.Open();
                command.Transaction = _DBM._connection.BeginTransaction(IsolationLevel.ReadCommitted);
                command.ExecuteNonQuery();

                MessageBox.Show("Product Added OK");

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
