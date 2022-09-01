using System;
using System.Collections.Generic;
using System.Text;

namespace DataManagementService.Services
{
    public class SqlQueryStringBuilder
    {
        private List<string> SqlArgList = new List<string>();

        private string QueryString = string.Empty;

        private string QueryStringSqlUpdatePart = string.Empty;
        private string QueryStringSqlSetPart = string.Empty;
        private string QueryStringSqlWherePart = string.Empty;



        public SqlQueryStringBuilder(string tableName, string idname, string id)
        {

            QueryStringSqlUpdatePart = @$"UPDATE {tableName}";
            QueryStringSqlWherePart = $" WHERE {idname} = {id}  ";

            //       UPDATE _table_name_
            //   SET _column1_ = _value1_, _column2_ = _value2_, ...  
            //WHERE _condition_;
        }


        public void AddqueryArg(string arg)
        {
            if (!string.IsNullOrWhiteSpace(arg))
            {
                SqlArgList.Add(arg);
            }
        }

        public string GetSqlQueryString()
        {
            querySetBuilder();

            QueryString = new StringBuilder().Append(QueryStringSqlUpdatePart).Append(QueryStringSqlSetPart).Append(QueryStringSqlWherePart).ToString();

            return QueryString;
        }



        private void querySetBuilder()
        {
            var stringbuilder = new StringBuilder();

            stringbuilder.Append(" SET ");
            bool firstlab = true;
            foreach (string sqlArg in SqlArgList)
            {
                if (!firstlab)
                {
                    stringbuilder.Append(", ");
                }
                stringbuilder.Append(sqlArg);
                stringbuilder.Append(" = @");
                stringbuilder.Append(sqlArg);
            }

            QueryStringSqlSetPart = stringbuilder.ToString();
        }
    }
}
