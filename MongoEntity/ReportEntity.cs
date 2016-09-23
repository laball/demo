using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoEntity
{
    public class ReportEntity: MongoEntityBase
    {
        public string CheckUnitCode { get; set; }
        public string WorkNo { get; set; }
        public string Age { get; set; }
        public string CheckItems { get; set; }
        public string CommitUserName { get; set; }
        public string CustomerName { get; set; }
        public string RegDate { get; set; }
        public string ReportDate { get; set; }
        public int[] GroupIds { get; set; }
        public string OrderCode { get; set; }
        public string OrderName { get; set; }
        public string Birthday { get; set; }
        public string IDCardNo { get; set; }
        public string Mobilphone { get; set; }
        public string CheckDate { get; set; }
        public DateTime UpdateTime { get; set; }
        public DateTime CreateTime { get; set; }
        public string GeneralSummarys { get; set; }
        public string GeneralAdvices { get; set; }
    }
}
