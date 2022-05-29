using System.Collections.Generic;

namespace TripleT.User.Application.Common.Models.Infrastructure.Persistence
{
    public class QueryModel
    {
        private List<Query> _queriesList = new();

        public void AddCondition(string propertyName, QueryType @operator, params object[] values)
        {
            _queriesList.Add(new Query(propertyName, @operator, values));
        }

        public List<Query> GetQueryQueries()
        {
            return _queriesList;
        }

        public class Query
        {
            public Query(string propertyName, QueryType @operator, IEnumerable<object> values)
            {
                PropertyName = propertyName;
                Operator = @operator;
                Values = values;
            }

            public string PropertyName { get; set; }

            public QueryType Operator { get; set; }

            public IEnumerable<object> Values { get; set; }
        }

        public enum QueryType
        {
            Equal,
            NotEqual,
            LessThanOrEqual,
            LessThan,
            GreaterThanOrEqual,
            GreaterThan,
            IsNotNull,
            IsNull,
            Contains,
            NotContains,
            BeginsWith,
            In,
            Between,
        }
    }
}