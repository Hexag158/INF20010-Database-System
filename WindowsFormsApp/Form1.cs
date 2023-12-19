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
    public partial class Form1 : Form
    {
        private string _connect;
        private DatabaseManager _databaseManager;
        private string username;
        private string password;
        

        public Form1()
        {
            InitializeComponent();
            username = "s103806269";
            password = "150803";
            _databaseManager = new DatabaseManager(username,password);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _databaseManager.Connect();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool v = true;
            try
            {
                v = _databaseManager.Connect();
                MessageBox.Show("Orace Connected!");
                menuForm Check = new menuForm();
                Check.Show();
                Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot connect to Oracle!");
                Application.Exit();
            } 
        }
    }
}
