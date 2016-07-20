using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Dapper;
using HZ.Interface;

namespace ExcelDemo
{
    class Program
    {
        private static string HZ_ConnectionString = "server=HZSWVDSQL01;database=HzHmisDev;uid=apidev;pwd=haozhuo2015;";
        private static string DC_ConnectionString = "server=HZSWVDSQL01;database=hzdatacenter;uid=Dcdbprg;pwd=a1234567;";
        private static System.Data.IDbConnection HZ_Conn;
        private static System.Data.IDbConnection DC_Conn;

        /*
         * 使用微软提供的操作Offfice的库DocumentFormat.OpenXml
         * ClosedXML在此基础上进行了封装，简化操作。
         * 
         * */
        static void Main(string[] args)
        {

            HZ_Conn = new SqlConnection(HZ_ConnectionString);
            HZ_Conn.Open();

            DC_Conn = new SqlConnection(DC_ConnectionString);
            DC_Conn.Open();

            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Sheet1");

            var url = "http://webapp.hc.ihaozhuo.com/SMSPromotion.html#/{0}/{1}/{2}";
            //http://webapp.hc.ihaozhuo.com/SMSPromotion.html#/{task}/{mobile}/{org}

            var table = "bjbr008";
            var deptCode = "bjbr008";

            //黎波，刘述正，刘倩倩,
            //var mobiles = new string[] { "15900860546", "17783055953","15601815186"};
            var mobiles = HZ_Conn.Query<string>("select  distinct telephone from " + table);//distinct

            var shortLinkProxy = SortLinkServerProxy.ShareInstance();

            var time = DateTime.Now;
            var sms = (from c in mobiles
                       where !string.IsNullOrEmpty(c) && Regex.Match(c, "1[2|3|5|7|8|][0-9]{9}").Success
                       select new
                       {
                           mobile = c,
                           sms = shortLinkProxy.getSortLink(string.Format(url, 1, c, deptCode)) + " "
                       }).ToList();

            Trace.WriteLine(string.Format("cost:{0}", DateTime.Now.Subtract(time).TotalSeconds));

            var rowStart = 2;
            var columnStart = 1;

            foreach (var item in sms)
            {
                worksheet.Cell(rowStart, columnStart).Value = item.mobile;
                worksheet.Cell(rowStart, columnStart + 1).Value = "【新乡第一人民医院】温馨提示,您的体检报告已完成，查看您的健康状况及阳性指标，永久保存报告详情请点击";
                worksheet.Cell(rowStart, columnStart + 2).Value = item.sms + " 退订回N";
                worksheet.Cell(rowStart, columnStart + 3).Value = item.sms;

                rowStart++;
            }

            workbook.SaveAs("HelloWorld.xlsx");
        }
    }
}
