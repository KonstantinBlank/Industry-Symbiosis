using System;
using System.Data;
using System.Data.SqlClient;
using DataManagementService.Data;
using DataManagementService.Interfaces;
using Newtonsoft.Json;

namespace DataManagementService.Services
{
    public class StreamService : IStreamService
    {
        public StreamService()
        {
        }

        public string Get(int productionLineProcessId)
        {
            string streams = string.Empty;

            SqlConnectionHelper.Connect((connection) =>
            {
                string queryString;
                queryString = @$"SELECT * 
                                 FROM stream
                                 WHERE fk_production_line_process = {productionLineProcessId};";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Connection.Open();
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(dataReader);
                        streams = JsonConvert.SerializeObject(dataTable);
                    }
                }
            });

            return streams;
        }

        public string Create(int productionLineProcessId, bool isInput, int? materialId, int? energyId, int amount, int interval)
        {
            Stream stream = new Stream(productionLineProcessId, isInput, materialId, energyId, amount, interval);

            SqlConnectionHelper.Connect((connection) =>
            {
                string queryString = getCreateQuery(stream);

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    // Insert parameters
                    command.Parameters.AddWithValue("@fk_production_line_process", stream.ProductionLineProcessId);
                    command.Parameters.AddWithValue("@is_input", stream.IsInput);
                    if (stream.MaterialId != null)
                    {
                        command.Parameters.AddWithValue("@fk_material", stream.MaterialId);
                    }
                    if (stream.EnergyId != null)
                    {
                        command.Parameters.AddWithValue("@fk_energy_source", stream.EnergyId);
                    }
                    command.Parameters.AddWithValue("@amount", stream.Amount);
                    command.Parameters.AddWithValue("@interval", stream.Interval);
                    command.Connection.Open();
                    int streamId = Convert.ToInt32(command.ExecuteScalar());
                    stream.SetStreamId(streamId);
                }
            });

            return JsonConvert.SerializeObject(stream);
        }

        private string getCreateQuery(Stream stream)
        {
            string query;
            string column = (stream.MaterialId != null) ? "fk_material" : "fk_energy_source"; 
            query = @$"INSERT INTO stream
                            (fk_production_line_process,
                            is_input,
                            {column},
                            amount,
                            interval)
                           VALUES
                            (@fk_production_line_process,
                            @is_input,
                            @{column},
                            @amount,
                            @interval)
                           SELECT SCOPE_IDENTITY()";
            
            return query;
        }


        public int Update(int id, int? productionLineProcessId, bool? isInput, int? materialId, int? energyId, int? amount, int? interval)
        {
            int result = 0;
            Stream stream = new Stream(id, productionLineProcessId, isInput, materialId, energyId, amount, interval);

            SqlConnectionHelper.Connect((connection) =>
            {
                string query = getUpdateQuery(stream);

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Connection.Open();

                    // Start a local transaction.
                    SqlTransaction sqlTransaction = connection.BeginTransaction();

                    // Enlist a command in the current transaction.
                    command.Transaction = sqlTransaction;

                    try
                    {
                        insertCommandParameters(stream, command);

                        result += command.ExecuteNonQuery();

                        sqlTransaction.Commit();
                    }
                    catch (SqlException error)
                    {
                        Console.Write(error.ToString());
                        sqlTransaction.Rollback();
                        throw error;
                    }
                }
            });

            return result;
        }

        private void insertCommandParameters(Stream stream, SqlCommand command)
        {
            // Insert parameters
            if (stream.ProductionLineProcessId != null)
            {
                command.Parameters.AddWithValue("@fk_production_line_process", stream.ProductionLineProcessId);
            }

            if (stream.IsInput != null)
            {
                command.Parameters.AddWithValue("@is_input", stream.IsInput);
            }

            if (stream.MaterialId != null)
            {
                command.Parameters.AddWithValue("@fk_material", stream.MaterialId);
            }

            if (stream.EnergyId != null)
            {
                command.Parameters.AddWithValue("@fk_energy_source", stream.EnergyId);
            }

            if (stream.Amount != null)
            {
                command.Parameters.AddWithValue("@amount", stream.Amount);
            }

            if (stream.Interval != null)
            {
                command.Parameters.AddWithValue("@interval", stream.Interval);
            }
        }

        private string getUpdateQuery(Stream stream)
        {
            SqlQueryStringBuilder queryBuilder = new SqlQueryStringBuilder("stream", "id", stream.Id.ToString());

            if (stream.ProductionLineProcessId != null)
            {
                queryBuilder.AddQueryArg("fk_production_line_process");
            }

            if (stream.IsInput != null)
            {
                queryBuilder.AddQueryArg("is_input");
            }

            if (stream.MaterialId != null)
            {
                queryBuilder.AddQueryArg("fk_material");
            }

            if (stream.EnergyId != null)
            {
                queryBuilder.AddQueryArg("fk_energy_source");
            }

            if (stream.Amount != null)
            {
                queryBuilder.AddQueryArg("amount");
            }

            if (stream.Interval != null)
            {
                queryBuilder.AddQueryArg("interval");
            }

            string query = queryBuilder.GetSqlQueryString();
            Console.WriteLine(query);
            return query;
        }
    }
}

