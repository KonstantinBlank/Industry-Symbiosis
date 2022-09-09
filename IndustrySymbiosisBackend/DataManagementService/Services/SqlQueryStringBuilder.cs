using System;
using System.Collections.Generic;
using System.Text;

namespace DataManagementService.Services
{
    public class SqlQueryStringBuilder
    {
        private List<string> _argumentList = new List<string>();

        private string _queryString = string.Empty;

        private string _updatePart = string.Empty;
        private string _setPart = string.Empty;
        private string _wherePart = string.Empty;


        public SqlQueryStringBuilder(string tableName, string idName, string id)
        {

            _updatePart = $"UPDATE {tableName}";
            _wherePart = $" WHERE {idName} = {id}";

            //   UPDATE _table_name_
            //   SET _column1_ = _value1_, _column2_ = _value2_, ...  
            //   WHERE _condition_;
        }


        public void AddQueryArgument(string key, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                _argumentList.Add(key);
            }
        }

        public string GetSqlQueryString()
        {
            if (_argumentList.Count == 0)
            {
                return "";
            }
            querySetBuilder();

            _queryString = new StringBuilder().Append(_updatePart).Append(_setPart).Append(_wherePart).ToString();

            return _queryString;
        }

        private void querySetBuilder()
        {
            var stringbuilder = new StringBuilder();

            stringbuilder.Append(" SET ");
            bool firstlab = true;
            foreach (string sqlArg in _argumentList)
            {
                if (!firstlab)
                {
                    stringbuilder.Append(", ");
                }
                stringbuilder.Append(sqlArg);
                stringbuilder.Append(" = @");
                stringbuilder.Append(sqlArg);
                firstlab = false;
            }

            _setPart = stringbuilder.ToString();
        }
    }
}
