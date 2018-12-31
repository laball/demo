using Abp.AspNetCore.Mvc.Controllers;
using Lee.Abp.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Lee.Abp.Web.Common
{
    /// <summary>
    /// 控制器基类
    /// </summary>
    public class ApiControllerBase : AbpController
    {
        /// <summary>
        /// 返回数据结果
        /// see https://stackoverflow.com/questions/168901/howto-count-the-items-from-a-ienumerablet-without-iterating
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [NonAction]
        public HttpResponse<T> Success<T>(T data, string message = "")
        {
            return new HttpResponse<T>()
            {
                Data = data,
                Status = WcsHttpResponseStatus.Success,
                Message = ((data as ICollection)?.Count == 0 && string.IsNullOrEmpty(message)) ? "无数据！" : message
            };
        }

        /// <summary>
        /// 返回数据结果（分页版本）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pagedResult"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [NonAction]
        public HttpPagedResponse<T> Success<T>(PagedResult<T> pagedResult, string message = "")
        {
            if (pagedResult == null)
            {
                throw new ArgumentNullException(nameof(pagedResult));
            }

            return new HttpPagedResponse<T>()
            {
                Data = pagedResult.Data,
                TotalCount = pagedResult.TotalCount,
                Count = pagedResult.Data.Count,
                PageCount = (int)Math.Ceiling(pagedResult.TotalCount * 1.0d / pagedResult.PageSize),
                Status = WcsHttpResponseStatus.Success,
                Message = (pagedResult?.Data?.Count == 0 && string.IsNullOrEmpty(message)) ? "无数据！" : message
            };
        }

        /// <summary>
        /// 返回失败
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [NonAction]
        public HttpResponse<T> Fail<T>(string message = "")
        {
            return new HttpResponse<T>()
            {
                Status = WcsHttpResponseStatus.Failure,
                Message = message
            };
        }

        /// <summary>
        /// 返回异常
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [NonAction]
        public HttpResponse<T> Error<T>(string message = "")
        {
            return new HttpResponse<T>()
            {
                Status = WcsHttpResponseStatus.Exceptional,
                Message = message
            };
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="memoryStream"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [NonAction]
        public HttpResponseMessage Export(MemoryStream memoryStream, string fileName)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(memoryStream)
            };

            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            fileName += "_" + Guid.NewGuid().ToString("N").Substring(0, 6) + ".xlsx";
#if DEBUG
            using (FileStream file = new FileStream("E:\\" + fileName, FileMode.Create, FileAccess.Write))
            {
                memoryStream.WriteTo(file);
            }
#endif
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = HttpUtility.UrlEncode(fileName)
            };

            return response;
        }
    }
}
