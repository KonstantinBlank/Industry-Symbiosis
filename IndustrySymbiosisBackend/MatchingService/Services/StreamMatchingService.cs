using DataManagementService.Services;
using MatchingService.Interfaces;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace MatchingService.Services
{
    public class StreamMatchingService : IStreamMatchingService
    {

        public StreamMatchingService()
        {
        }

        public string GetAvailableInputStreams(int enterpriseId)
        {
            string streams = string.Empty;

            SqlConnectionHelper.Connect((connection) =>
            {
                string queryString;
                queryString = @$"SELECT stream_enterprise_id.enterprise_id, stream_view.* FROM stream_unmatched
                                 LEFT JOIN stream_enterprise_id
                                 ON stream_unmatched.id = stream_enterprise_id.stream_id
	                                LEFT join stream_view
	                                ON stream_unmatched.id = stream_view.id
                                 WHERE stream_view.is_input = 1 AND stream_enterprise_id.enterprise_id = {enterpriseId};";

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

        public string GetAvailableOutputStreams(int enterpriseId)
        {
            string streams = string.Empty;

            SqlConnectionHelper.Connect((connection) =>
            {
                string queryString;
                queryString = @$"SELECT stream_enterprise_id.enterprise_id, stream_view.* FROM stream_unmatched
                                 LEFT JOIN stream_enterprise_id
                                 ON stream_unmatched.id = stream_enterprise_id.stream_id
	                                LEFT join stream_view
	                                ON stream_unmatched.id = stream_view.id;
                                 WHERE stream_view.is_input = 0 AND stream_enterprise_id.enterprise_id = {enterpriseId};";

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

        public string GetMatchingInputStreams(int outputstreamId)
        {
            string streams = string.Empty;

            int? materialFK = null;
            int? energySourceFK = null;
            int? amount = null;



            // Attribute vom Outputstream laden
            SqlConnectionHelper.Connect((connection) =>
            {
                string queryString;
                queryString = @$"SELECT stream_view.fk_material, stream_view.fk_energy_source, stream_view.amount FROM stream_unmatched
	                                LEFT join stream_view
	                                ON stream_unmatched.id = stream_view.id
                                    WHERE stream_unmatched.id = {outputstreamId}";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Connection.Open();

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        dataReader.Read();
                        materialFK = dataReader.GetInt32(0);
                        energySourceFK = dataReader.GetInt32(1);
                        amount = dataReader.GetInt32(3);
                    }
                }
            });

            // Nach passenden InputStreams filtern
            SqlConnectionHelper.Connect((connection) =>
            {
                string queryString = null;

                if (materialFK == null)
                {
                    queryString = @$"SELECT stream_enterprise_id.enterprise_id, stream_view.* FROM stream_unmatched
                                                LEFT JOIN stream_enterprise_id
                                                ON stream_unmatched.id = stream_enterprise_id.stream_id
	                                            LEFT join stream_view
	                                            ON stream_unmatched.id = stream_view.id
                                                WHERE stream_view.is_input = 1 AND stream_view.fk_material = {materialFK};";
                }

                else // energySourceFk != null
                {
                    queryString = @$"SELECT stream_enterprise_id.enterprise_id, stream_view.* FROM stream_unmatched
                                                LEFT JOIN stream_enterprise_id
                                                ON stream_unmatched.id = stream_enterprise_id.stream_id
	                                            LEFT join stream_view
	                                            ON stream_unmatched.id = stream_view.id
                                                WHERE stream_view.is_input = 1 AND stream_view.fk_energy_source = {energySourceFK};";

                }

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

        public string GetMatchingOutputStreams(int inputstreamId)
        {
            string streams = string.Empty;

            int? materialFK = null;
            int? energySourceFK = null;
            int? amount = null;



            // Attribute vom Outputstream laden
            SqlConnectionHelper.Connect((connection) =>
            {
                string queryString;
                queryString = @$"SELECT stream_view.fk_material, stream_view.fk_energy_source, stream_view.amount FROM stream_unmatched
	                                LEFT join stream_view
	                                ON stream_unmatched.id = stream_view.id
                                    WHERE stream_unmatched.id = {inputstreamId}";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Connection.Open();

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        dataReader.Read();
                        materialFK = dataReader.GetInt32(0);
                        energySourceFK = dataReader.GetInt32(1);
                        amount = dataReader.GetInt32(3);
                    }
                }
            });



            // Nach passenden InputStreams filtern
            SqlConnectionHelper.Connect((connection) =>
            {
                string queryString = null;

                if (materialFK == null)
                {
                    queryString = @$"SELECT stream_enterprise_id.enterprise_id, stream_view.* FROM stream_unmatched
                                                LEFT JOIN stream_enterprise_id
                                                ON stream_unmatched.id = stream_enterprise_id.stream_id
	                                            LEFT join stream_view
	                                            ON stream_unmatched.id = stream_view.id
                                                WHERE stream_view.is_output = 1 AND stream_view.fk_material = {materialFK};";
                }

                else // energySourceFk != null
                {
                    queryString = @$"SELECT stream_enterprise_id.enterprise_id, stream_view.* FROM stream_unmatched
                                                LEFT JOIN stream_enterprise_id
                                                ON stream_unmatched.id = stream_enterprise_id.stream_id
	                                            LEFT join stream_view
	                                            ON stream_unmatched.id = stream_view.id
                                                WHERE stream_view.is_output = 1 AND stream_view.fk_energy_source = {energySourceFK};";

                }

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

        public int create(int enterpriseId, bool selectedIsInput, string selectedStreamId, string requestedStreamId, float amount, float priceProposal, string comment)
        {
            string streams = string.Empty;
            int result = 0;



            SqlConnectionHelper.Connect((connection) =>
            {
                string queryString;

                queryString = @$"INSERT INTO stream_match
                                     (fk_input_stream
                                     ,fk_output_stream
                                     ,amount
                                     ,fk_proposing_party
                                     ,price_proposal
                                     ,comment
                                     ,fk_status
                                     ,creation_date)
                                 VALUES
                                     (@fk_input_stream
                                     ,@fk_output_stream
                                     ,amount
                                     ,fk_proposing_party
                                     ,@price_proposal
                                     ,@comment
                                     ,@fk_status
                                     ,GETDATE());
                               SELECT SCOPE_IDENTITY();";


                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    // Insert parameters
                    if (selectedIsInput) // input = selectedStreamId
                    {
                        command.Parameters.AddWithValue("@fk_input_stream", selectedStreamId);
                        command.Parameters.AddWithValue("@fk_output_stream", requestedStreamId);
                    }
                    else // input = requestedStreamId
                    {
                        command.Parameters.AddWithValue("@fk_input_stream", requestedStreamId);
                        command.Parameters.AddWithValue("@fk_output_stream", selectedStreamId);
                    }

                    command.Parameters.AddWithValue("@amount", amount);
                    command.Parameters.AddWithValue("@fk_proposing_party", enterpriseId);
                    command.Parameters.AddWithValue("@price_proposal", priceProposal);
                    command.Parameters.AddWithValue("@comment", comment);
                    command.Parameters.AddWithValue("@fk_status", 1); // 0 == 1 == 2 == 
                    command.Connection.Open();

                    int result = command.ExecuteNonQuery();
                }
            });
            return result;
        }
    }
}
