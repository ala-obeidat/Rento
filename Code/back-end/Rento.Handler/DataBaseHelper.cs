using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Rento.Helper
{
    public class DataBaseHelper
    {
        public static DataBaseHelper Instance = new DataBaseHelper();

        private static readonly string CONNECTION_STRING = ConfigurationManager.ConnectionStrings["CONNECTION_STRING"].ConnectionString;

        public async Task ExecuteNonQuery(string storedProcedure, Action<SqlCommand> FillCommand)
        {
            try
            {
                using (var command = new SqlCommand(storedProcedure, new SqlConnection(CONNECTION_STRING)))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    FillCommand(command);
                    await command.Connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    command.Connection.Close();
                }
            }
            catch (SqlException s)
            {
                Logger.Exception(s, storedProcedure);
                throw s;
            }
            catch (Exception e)
            {
                Logger.Exception(e, storedProcedure);
                throw e;
            }

        }

        public async Task ExecuteReader(string storedProcedure, Func<SqlDataReader,Task> FetchReader)
        {
            try
            {
                using (var command = new SqlCommand(storedProcedure, new SqlConnection(CONNECTION_STRING)))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    await command.Connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        await FetchReader(reader);
                    }
                    command.Connection.Close();
                }
            }
            catch (SqlException s)
            {
                Logger.Exception(s, storedProcedure);
                throw s;
            }
            catch (Exception e)
            {
                Logger.Exception(e, storedProcedure);
                throw e;
            }
        }
        public void ExecuteReaderSync(string storedProcedure, Action<SqlDataReader> FetchReader)
        {
            try
            {
                using (var command = new SqlCommand(storedProcedure, new SqlConnection(CONNECTION_STRING)))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        FetchReader(reader);
                    }
                    command.Connection.Close();
                }
            }
            catch (SqlException s)
            {
                Logger.Exception(s, storedProcedure);
                throw s;
            }
            catch (Exception e)
            {
                Logger.Exception(e, storedProcedure);
                throw e;
            }
        }

        public async Task ExecuteReader(string storedProcedure, Action<SqlCommand> FillCommand, Func<SqlDataReader,Task> FetchReader)
        {
            try
            {
                using (var command = new SqlCommand(storedProcedure, new SqlConnection(CONNECTION_STRING)))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    FillCommand(command);
                    await command.Connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        await FetchReader(reader);
                    }
                        command.Connection.Close();
                    
                }
            }
            catch (SqlException s)
            {
                Logger.Exception(s, storedProcedure);
                throw s;
            }
            catch (Exception e)
            {
                Logger.Exception(e, storedProcedure);
                throw e;
            }
        }

        public void ExecuteReaderSync(string storedProcedure, Action<SqlCommand> FillCommand, Action<SqlDataReader> FetchReader)
        {
            try
            {
                using (var command = new SqlCommand(storedProcedure, new SqlConnection(CONNECTION_STRING)))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    FillCommand(command);
                    command.Connection.Open();
                    using (SqlDataReader reader =  command.ExecuteReader())
                    {
                        FetchReader(reader);
                    }
                    command.Connection.Close();

                }
            }
            catch (SqlException s)
            {
                Logger.Exception(s, storedProcedure);
                throw s;
            }
            catch (Exception e)
            {
                Logger.Exception(e, storedProcedure);
                throw e;
            }
        }

        public async Task<T> ExecuteScaler<T>(string storedProcedure, Action<SqlCommand> FillCommand)
        {
            try
            {
                object scalerObject = null;
                using (var command = new SqlCommand(storedProcedure, new SqlConnection(CONNECTION_STRING)))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    FillCommand(command);
                    await command.Connection.OpenAsync();

                    scalerObject = await command.ExecuteScalarAsync();
                    command.Connection.Close();
                }
                if ((scalerObject != null) && (scalerObject != DBNull.Value))
                {
                    return (T)Convert.ChangeType(scalerObject, typeof(T));
                }
                return default(T);
            }
            catch (SqlException s)
            {
                Logger.Exception(s, storedProcedure);
                throw s;
            }
            catch (Exception e)
            {
                Logger.Exception(e, storedProcedure);
                throw e;
            }
        }

        public async Task ExecuteTransaction(Func<SqlCommand,Task> ProcessCommand, Action<Exception> HandleException)
        {
            try
            {
                using (var command = new SqlCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = new SqlConnection(CONNECTION_STRING);
                    await command.Connection.OpenAsync();
                    command.Transaction = command.Connection.BeginTransaction(IsolationLevel.Snapshot);
                    try
                    {
                        await ProcessCommand(command);
                    }
                    catch (SqlException s)
                    {
                        Logger.Exception(s, "Command: " + command.CommandText);
                        command.Transaction.Rollback();
                        command.Connection.Close();
                        throw s;
                    }
                    catch (Exception ex)
                    {
                        HandleException(ex);
                        command.Transaction.Rollback();
                        command.Connection.Close();
                        throw ex;
                    }
                    command.Transaction.Commit();
                    command.Connection.Close();
                }
            }
            catch (SqlException s)
            {
                Logger.Exception(s, "SqlException");
                throw s;
            }
            catch (Exception e)
            {
                Logger.Exception(e, "General");
                throw e;
            }
        }

    }
}