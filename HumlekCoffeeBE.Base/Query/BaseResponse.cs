using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumlekCoffeeBE.Base.Query
{
    public enum BaseResponseStatus
    {
        ok,
        fail
    }

    public interface IBaseResponse<T> where T : class
    {
        T Data { get; set; }
        BaseResponsePagingData PagingData { get; set; }
        BaseResponseMetaData MetaData { get; set; }
    }

    public class BaseResponse<T> : IBaseResponse<T>
        where T : class
    {
        public T Data { get; set; }
        public BaseResponsePagingData PagingData { get; set; }
        public BaseResponseMetaData MetaData { get; set; }

        public BaseResponse()
        {
            MetaData = new BaseResponseMetaData()
            {
                Status = BaseResponseStatus.ok.ToString(),
                ErrorCode = "",
                Message = "",
                Errors = new List<BaseResponseMetaDataErrors>()
            };
        }
    }

    public class BaseResponseMetaData
    {
        public BaseResponseMetaData()
        {
            Errors = new List<BaseResponseMetaDataErrors>();
        }

        public string Status { get; set; }
        public string ErrorCode { get; set; }
        public string Message { get; set; }
        public List<BaseResponseMetaDataErrors> Errors { get; set; }
    }

    public class BaseResponseMetaDataErrors
    {
        public string Field { get; set; }
        public string ErrorCode { get; set; }
        public string Message { get; set; }
        public object Value { get; set; }
    }

    public class BaseResponsePagingData
    {
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }

        public int FirstRowOnPage
        {
            get { return (CurrentPage - 1) * PageSize + 1; }
        }

        public int LastRowOnPage
        {
            get { return Math.Min(CurrentPage * PageSize, RowCount); }
        }
    }

}
