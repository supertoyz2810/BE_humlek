using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumlekCoffeeBE.Base.Query
{
    public class BaseQueryFilter
    {
        [FromQuery(Name = "pageNumber")]
        public int PageNumber { get; set; } = 1;

        [FromQuery(Name = "pageSize")]
        public int PageSize { get; set; } = 10;
    }

    public class BaseQuery : BaseQueryFilter
    {
        [FromQuery(Name = "filter")]
        public string Filter { get; set; }

        [FromQuery(Name = "orderBy")]
        public string OrderBy { get; set; } = TypeOrderBy.Asc;

        [FromQuery(Name = "sortBy")]
        public string SortBy { get; set; }

        public List<string> FilterField { get; set; } = new();
    }

    public class TypeOrderBy
    {
        public const string Asc = "asc";
        public const string Desc = "desc";
    }
}
