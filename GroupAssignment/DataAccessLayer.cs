using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GroupAssignment
{
    
    public class DataAccessLayer
    {
        private string ConnectionString { get; set; }

        public DataAccessLayer()
        {
            //ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\"C:\Users\belet\Downloads\GroupAssignment Fixed\GroupAssignment\GroupAssignment\InvoicesDatabase.mdb"";
            ConnectionString = $"Provider=Microsoft.Jet.OLEDB.4.0;Data source= {Directory.GetCurrentDirectory()}\\InvoicesDatabase.mdb";
        }

        public DataSet ExecuteSQLStatement(string sSQL)
        {
            try
            {
                DataSet ds = new DataSet();

                using (OleDbConnection conn = new OleDbConnection(ConnectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {
                        conn.Open();
                        adapter.SelectCommand = new OleDbCommand(sSQL, conn);
                        adapter.SelectCommand.CommandTimeout = 0;
                        adapter.Fill(ds);
                    }
                }
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method takes an SQL statment that is passed in and executes it.  The resulting single 
        /// value is returned.
        /// </summary>
        /// <param name="sSQL">The SQL statement to be executed.</param>
        /// <returns>Returns a string from the scalar SQL statement.</returns>
		public string ExecuteScalarSQL(string sSQL)
        {
            try
            {
                object obj;

                using (OleDbConnection conn = new OleDbConnection(ConnectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {
                        conn.Open();
                        adapter.SelectCommand = new OleDbCommand(sSQL, conn);
                        adapter.SelectCommand.CommandTimeout = 0;
                        obj = adapter.SelectCommand.ExecuteScalar();
                    }
                }
                if (obj == null)
                {
                    return "";
                }
                else
                {
                    //Return the value
                    return obj.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method takes an SQL statment that is a non query and executes it.
        /// </summary>
        /// <param name="sSQL">The SQL statement to be executed.</param>
        /// <returns>Returns the number of rows affected by the SQL statement.</returns>
        public int ExecuteNonQuery(string sSQL)
        {
            try
            {
                //Number of rows affected
                int iNumRows;

                using (OleDbConnection conn = new OleDbConnection(ConnectionString))
                {
                    conn.Open();
                    OleDbCommand cmd = new OleDbCommand(sSQL, conn);
                    cmd.CommandTimeout = 0;

                    //Execute the non query SQL statement
                    iNumRows = cmd.ExecuteNonQuery();
                }

                //return the number of rows affected
                return iNumRows;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
