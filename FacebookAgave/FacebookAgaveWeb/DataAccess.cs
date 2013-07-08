using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
namespace FacebookAgaveWeb
{
    public class DataAccess
    {
        SqlConnection MyConnection;
        private DataAccess()
        {
            if (MyConnection == null)
            {
                MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["FriendForOfficeSQL"].ConnectionString);
            }
        }
        public static DataAccess Instance
        {
            get
            {
                return new DataAccess();
            }
        }
        public string WhereClauseInterpreter(params string[] clauses)
        {
            if (clauses == null || clauses.Count() <= 0) return string.Empty;
            clauses = clauses.Where(s => !string.IsNullOrEmpty(s)).ToArray();
            string whereClause = "WHERE " + string.Join(" AND ", clauses);
            return whereClause;
        }
        public void LogAction(string UserID, string ActionText, string Error, string Environment, string Message)
        {
            SqlCommand cmd = MyConnection.CreateCommand();
            cmd.CommandText = "INSERT INTO [ActionLog](Action, UserID, Error, Environment, Message) VALUES (@Action, @UserID, @Error, @Environment, @Message)";
            cmd.Parameters.AddWithValue("@Action", ActionText);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.Parameters.AddWithValue("@Error", Error);
            cmd.Parameters.AddWithValue("@Environment", Environment);
            cmd.Parameters.AddWithValue("@Message", Message);

            MyConnection.Open();
            cmd.ExecuteNonQuery();
            MyConnection.Close();
        }
        #region Private Helpers
        private int GetDBInt(SqlDataReader reader, string columnName)
        {
            try
            {
                int ordinal = reader.GetOrdinal(columnName);
                return reader.IsDBNull(ordinal) ? -1 : reader.GetInt32(ordinal);
            }
            catch (IndexOutOfRangeException)
            {
                return -1;
            }
        }
        private double GetDBDouble(SqlDataReader reader, string columnName)
        {
            try
            {
                int ordinal = reader.GetOrdinal(columnName);
                return reader.IsDBNull(ordinal) ? -1 : reader.GetDouble(ordinal);
            }
            catch (IndexOutOfRangeException)
            {
                return -1;
            }
        }
        private string GetDBString(SqlDataReader reader, string columnName)
        {
            try
            {
                int ordinal = reader.GetOrdinal(columnName);
                return reader.IsDBNull(ordinal) ? string.Empty : reader.GetString(ordinal);
            }
            catch (IndexOutOfRangeException)
            {
                return string.Empty;
            }
        }
        private bool GetDBBool(SqlDataReader reader, string columnName)
        {
            try
            {
                int ordinal = reader.GetOrdinal(columnName);
                return reader.IsDBNull(ordinal) ? false : reader.GetBoolean(ordinal);
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
        }
        private DateTime GetDBDate(SqlDataReader reader, string columnName)
        {
            try
            {
                int ordinal = reader.GetOrdinal(columnName);
                return reader.IsDBNull(ordinal) ? DateTime.MinValue : reader.GetDateTime(ordinal);
            }
            catch (IndexOutOfRangeException)
            {
                return DateTime.MinValue;
            }
        }
        #endregion
    }
}
