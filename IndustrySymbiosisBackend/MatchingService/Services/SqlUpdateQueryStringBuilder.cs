using System;
using System.Collections.Generic;
using System.Text;

namespace DataManagementService.Services
{
    public class SqlUpdateQueryStringBuilder
    {
        private List<string> _sqlArgList = new List<string>();

        private string _queryString = string.Empty;

        private string _queryStringSqlUpdatePart = string.Empty;
        private string _queryStringSqlSetPart = string.Empty;
        private string _queryStringSqlWherePart = string.Empty;


        public SqlUpdateQueryStringBuilder(string tableName, string idName, string id)
        {

            _queryStringSqlUpdatePart = $"UPDATE {tableName}";
            _queryStringSqlWherePart = $" WHERE {idName} = {id}";

            //   UPDATE _table_name_
            //   SET _column1_ = _value1_, _column2_ = _value2_, ...  
            //   WHERE _condition_;
        }


        public void AddQueryArg(string arg)
        {
            if (!string.IsNullOrWhiteSpace(arg))
            {
                _sqlArgList.Add(arg);
            }
            else
            {
                throw new ArgumentException("The argument 'arg' can not be null or empty. Please reference a string value.");
            }
        }

        public string GetSqlQueryString()
        {
            querySetBuilder();

            _queryString = new StringBuilder().Append(_queryStringSqlUpdatePart).Append(_queryStringSqlSetPart).Append(_queryStringSqlWherePart).ToString();

            return _queryString;
        }

        private void querySetBuilder()
        {
            var stringbuilder = new StringBuilder();

            stringbuilder.Append(" SET ");
            bool firstlab = true;
            foreach (string sqlArg in _sqlArgList)
            {
                if (!firstlab)
                {
                    stringbuilder.Append(", ");
                }
                stringbuilder.Append(sqlArg);
                stringbuilder.Append(" = @");
                stringbuilder.Append(sqlArg);
            }

            _queryStringSqlSetPart = stringbuilder.ToString();
        }
    }
}
