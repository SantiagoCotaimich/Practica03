using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;


namespace DataDLL.Data
{
    public class DataHelper
    {
        private static DataHelper _instance;
        private string _cnnString;
        private SqlConnection _cnn;

        private DataHelper()
        {
            _cnnString = @"Data Source=SANTIAGO\SQLEXPRESS;Initial Catalog=Db_Facturacion;Integrated Security=True;TrustServerCertificate=True";
            _cnn = new SqlConnection(_cnnString);
        }

        public static DataHelper GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DataHelper();
            }
            return _instance;
        }
        public SqlConnection GetConnection()
        {
            return _cnn;
        }
        private void CloseConnection()
        {
            if (_cnn != null && _cnn.State == ConnectionState.Open)
            {
                _cnn.Close();
            }
        }
        public int ExecuteSPNonQuery(string sp, List<Parameter>? parametros)
        {
            int rows = 0;
            try
            {
                _cnn.Open();
                var cmd = new SqlCommand(sp, _cnn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (parametros != null)
                {
                    cmd = Parameter.LoadToCMD(parametros, cmd);
                }
                rows = cmd.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
            return rows;
        }
        public DataTable ExecuteSPQuery(string sp, List<Parameter>? parametros)
        {
            var dt = new DataTable();
            try
            {
                _cnn.Open();
                var cmd = new SqlCommand(sp, _cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (parametros != null)
                {
                    cmd = Parameter.LoadToCMD(parametros, cmd);
                }
                dt.Load(cmd.ExecuteReader());
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
            return dt;
        }
    }
}
