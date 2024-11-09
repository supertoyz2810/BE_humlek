using HumlekCoffeeBE.Base.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HumlekCoffeeBE.Base.Service
{
    public abstract class BaseService
    {
        protected static Guid GenenrateGuid()
        {
            return Guid.NewGuid();
        }

        protected static BaseResponse<T> ConvertResponseForPaging<T>(
            int pageNumber,
            int pageSize,
            int rowCount,
            T results) where T : class
        {
            var result = ConvertResponse(results);

            result.PagingData = new BaseResponsePagingData();

            result.PagingData.CurrentPage = pageNumber;
            result.PagingData.PageSize = pageSize;
            result.PagingData.RowCount = rowCount;

            var pageCount = (double)result.PagingData.RowCount / pageSize;
            result.PagingData.PageCount = (int)Math.Ceiling(pageCount);

            return result;
        }

        protected static BaseResponse<T> ConvertResponse<T>(T value) where T : class
        {
            var result = new BaseResponse<T>();
            result.Data = value;

            return result;
        }

        protected static BaseResponse<T> ConvertResponse<T>() where T : class
        {
            var result = new BaseResponse<T>();
            result.Data = null;

            return result;
        }

        protected static BaseResponse<T> ConvertResponseForError<T>(
            string errorCode,
            string message) where T : class
        {
            var result = new BaseResponse<T>();
            result.Data = null;

            result.MetaData.Status = BaseResponseStatus.fail.ToString();
            result.MetaData.ErrorCode = errorCode;
            result.MetaData.Message = message;

            return result;
        }

        protected static BaseResponse<T> ConvertResponseForError<T>(
        string errorCode,
        string message,
            params BaseResponseMetaDataErrors[] errors) where T : class
        {
            var result = new BaseResponse<T>();
            result.Data = null;

            result.MetaData.Status = BaseResponseStatus.fail.ToString();
            result.MetaData.ErrorCode = errorCode;
            result.MetaData.Message = message;

            if (errors.Length > 0)
                result.MetaData.Errors.AddRange(errors);

            return result;
        }

        protected static string ReplaceParamsInEmailTemplate(
            string template,
            Dictionary<string, string> replaceParams)
        {
            Regex re = new Regex(@"\{(\w+)\}", RegexOptions.Compiled);
            return re.Replace(template, match => replaceParams[match.Groups[1].Value]);
        }
    }
}
