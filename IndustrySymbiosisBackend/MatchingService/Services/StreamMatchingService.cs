using DataManagementService.Services;
using MatchingService.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MatchingService.Services
{
    public class StreamMatchingService : IStreamMatchingService
    {

        public StreamMatchingService()
        {
        }


        /*
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

        */

        public int propose(int enterpriseId, bool selectedIsInput, int selectedStreamId, int requestedStreamId, float amount, float priceProposal, string comment)
        {
            string streams = string.Empty;
            int matchID = -1;

            //int result = -1;

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


            IDictionary<string, object> parameterPairs = new Dictionary<string, object>();

            // Insert parameters
            if (selectedIsInput) // input = selectedStreamId
            {
                parameterPairs.Add("@fk_input_stream", selectedStreamId.ToString());
                parameterPairs.Add("@fk_output_stream", requestedStreamId.ToString());
            }
            else // input = requestedStreamId
            {
                parameterPairs.Add("@fk_input_stream", requestedStreamId.ToString());
                parameterPairs.Add("@fk_output_stream", selectedStreamId.ToString());
            }

            parameterPairs.Add("@amount", amount);
            parameterPairs.Add("@fk_proposing_party", enterpriseId);
            parameterPairs.Add("@price_proposal", priceProposal);
            parameterPairs.Add("@comment", comment);
            parameterPairs.Add("@fk_status", 1); // 0 == 1 == 2 == 


            matchID = SqlConnectionHelper.CreateEntry(queryString, parameterPairs);

            Console.WriteLine(queryString);
            Console.WriteLine("production line process entry was successfully created!");
            NotifyUserService notifyUserService = new NotifyUserService("team@industriesymbiose.de", "weis_ecom@mailbox.org", "Neue Match-Anfrage Details", "Sie haben ein neues Match");
           
            return matchID;
        }

        public int cancle(int matchId)
        {
            IDictionary<string, object?> parameterPairs = new Dictionary<string, object?>();
            parameterPairs.Add("fk_status", 5); // 5 = canclled

            int result = SqlConnectionHelper.UpdateEntry("stream", matchId.ToString(), parameterPairs);

            return result;
        }

        public int recall(int matchId)
        {
            IDictionary<string, object?> parameterPairs = new Dictionary<string, object?>();
            parameterPairs.Add("fk_status", 6); // 5 = recalled

            int result = SqlConnectionHelper.UpdateEntry("stream", matchId.ToString(), parameterPairs);

            return result;
        }

        public int reject(int matchId)
        {
            IDictionary<string, object?> parameterPairs = new Dictionary<string, object?>();
            parameterPairs.Add("fk_status", 6); // 6 = recall

            int result = SqlConnectionHelper.UpdateEntry("stream", matchId.ToString(), parameterPairs);

            return result;
        }

        public int accept(int matchId)
        {
            IDictionary<string, object?> parameterPairs = new Dictionary<string, object?>();
            parameterPairs.Add("fk_status", 1); // 1 = active

            int result = SqlConnectionHelper.UpdateEntry("stream", matchId.ToString(), parameterPairs);

            return result;
        }
    
    }
}
