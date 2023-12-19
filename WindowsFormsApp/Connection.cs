using System;
using System.Data;
using System.Linq;
using Oracle.ManagedDataAccess.Client;
namespace WindowsFormsApp
{
    public class DatabaseManager
    {
        private readonly string _connectionInfo;
        public readonly OracleConnection _connection;
        public readonly OracleDataAdapter _dataAdapter;
        public OracleCommand _command;
        public DataSet _dataSet;
        public OracleTransaction _transaction;
        public readonly int _varcharMax;
        public DatabaseManager(string username, string password)
        {
            _connectionInfo = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=feenix-oracle.swin.edu.au)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=DMS)));User Id=" + username + ";Password=" + password + ";Connection Timeout=120;";
            _connection = new OracleConnection(_connectionInfo);
            _dataAdapter = new OracleDataAdapter();
            _varcharMax = 32767;
        }
        private string StripException(string ex)
        {
            string firstLine = ex.Split(new[] { '\r', '\n' }).FirstOrDefault();
            string subex = firstLine.Substring(firstLine.IndexOf(' ') + 1);
            subex = "ERROR: " + subex;
            return subex;
        }
        public bool Connect()
        {
            Console.WriteLine("Connecting...");
            try
            {
                _connection.Open();
                Console.WriteLine("Connected to: " + _connection.ServerVersion);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex + "Failed to connect to server.");
                return false;
            }
        }
        public bool Disconnect()
        {
            Console.WriteLine("Disconnecting...");
            try
            {
                _connection.Close();
                Console.WriteLine("Disconnected from server.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex + "Failed to disconnect from server.");
                return false;
            }
        }
    }
}