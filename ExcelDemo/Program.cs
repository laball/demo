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

namespace ExcelDemo
{
    public class Model
    {
        public string Mobile { get; set; }
        public string Code { get; set; }
        public string Name1 { get; set; }

        public DateTime DateFinalExamed { get; set; }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Sheet1");

            var rowStart = 2;
            var columnStart = 1;

            using (var stream = new StreamReader("TextFile1.txt"))
            {
                var str = string.Empty;
                while ((str = stream.ReadLine()) != null)
                {
                    var fields = str.Split(',');

                    worksheet.Cell(rowStart, columnStart).Value = fields[0];
                    worksheet.Cell(rowStart, columnStart + 1).Value = fields[1];
                    worksheet.Cell(rowStart, columnStart + 2).Value = fields[2];
                    //worksheet.Cell(rowStart, columnStart + 3).Value = item.sms;

                    rowStart++;
                }
            }

            Console.WriteLine("end");

            workbook.SaveAs("HelloWorld.xlsx");
        }
    }
}