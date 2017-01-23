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

    public class Model
    {
        public string Mobile { get; set; }
        public string Code { get; set; }
        public string Name1 { get; set; }

        public DateTime DateFinalExamed { get; set; }
    }

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

            //DC_Conn = new SqlConnection(DC_ConnectionString);
            //DC_Conn.Open();

            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Sheet1");

            var url = "http://webapp.hc.ihaozhuo.com/SMSPromotion.html#/{0}/{1}/{2}";
            //http://webapp.hc.ihaozhuo.com/SMSPromotion.html#/{task}/{mobile}/{org}

            var table = "bjbr008";
            var deptCode = "bjbr008";

            var shortLinkProxy = SortLinkServerProxy.ShareInstance();

            var rrrr = shortLinkProxy.getSortLink("https://www.feelapp.cc/goal/brief/53804/channel/meinian?_app=youjiankang");

            //黎波，刘述正，刘倩倩,
            //var mobiles = new string[] { "15900860546", "17783055953","15601815186"};
            //var mobiles = new Model[]
            //{
            //    new Model { Mobile = "15900860546", Code = "001",Name1 = "111" },
            //    new Model { Mobile = "17783055953", Code = "001",Name1 = "111" },
            //    new Model { Mobile = "15601815186", Code = "001",Name1 = "111" }
            //};
            var mobiles = HZ_Conn.Query<Model>("  SELECT   DISTINCT   t.Mobile,t.Name1,t.Code,t.DateFinalExamed FROM [dbo].[Sheet1$] t  WHERE t.Mobile is NOT NULL");//distinct



            var time = DateTime.Now;
            var sms = (from c in mobiles
                       where !string.IsNullOrEmpty(c.Mobile) && Regex.Match(c.Mobile, "1[2|3|5|7|8|][0-9]{9}").Success
                       select new
                       {
                           mobile = c.Mobile,
                           sms = shortLinkProxy.getSortLink(string.Format(url, 1, c.Mobile, c.Code)) + " ",
                           name = c.Name1,
                           time = c.DateFinalExamed
                       }).ToList();

            Trace.WriteLine(string.Format("cost:{0}", DateTime.Now.Subtract(time).TotalSeconds));

            var rowStart = 2;
            var columnStart = 1;

            foreach (var item in sms)
            {
                worksheet.Cell(rowStart, columnStart).Value = item.mobile;
                worksheet.Cell(rowStart, columnStart + 1).Value = "【" + item.name + "】您的体检报告于" + item.time.Month + "月" + item.time.Day + "日（总检时间）已完成，";
                worksheet.Cell(rowStart, columnStart + 2).Value = item.sms + " 退订回N";
                //worksheet.Cell(rowStart, columnStart + 3).Value = item.sms;

                rowStart++;
            }

            Console.WriteLine("end");

            workbook.SaveAs("HelloWorld.xlsx");
        }
    }
}
