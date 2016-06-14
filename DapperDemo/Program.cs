using Dapper;
using HZ.Interface;
using HZ.MODEL;
using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DapperDemo
{

    /// <summary>
    /// 短信统计结果项
    /// </summary>
    public class SmsStatisticResultItem
    {
        /// <summary>
        /// 日期
        /// 如果是总计，则日期没有意义
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// 发送量
        /// </summary>
        public int SmsSendCount { get; set; }
        /// <summary>
        /// 点击量
        /// </summary>
        public int Hits { get; set; }
        /// <summary>
        /// 下载量
        /// </summary>
        public int Dloaded { get; set; }
        /// <summary>
        /// 注册量
        /// </summary>
        public int Registrations { get; set; }
        /// <summary>
        /// 点击率
        /// </summary>
        public double ClickRate { get; set; }
        /// <summary>
        /// 下载率
        /// </summary>
        public double DownloadRate { get; set; }
        /// <summary>
        /// 注册率
        /// </summary>
        public double EnrollmentRate { get; set; }
    }
    public class DataCenterCustomerInfoModel
    {
        public Guid RID { get; set; }
        /// <summary>
        /// 机构编号
        /// </summary>
        public string CheckUnitCode { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobilphone { get; set; }
        /// <summary>
        /// 体检结果时间
        /// </summary>
        public DateTime RecordDate { get; set; }
    }

    internal class Program
    {
        private static string HZ_ConnectionString = "server=HZSWVDSQL01;database=HzHmisDev;uid=apidev;pwd=haozhuo2015;";
        private static string DC_ConnectionString = "server=HZSWVDSQL01;database=hzdatacenter;uid=Dcdbprg;pwd=a1234567;";
        private static System.Data.IDbConnection HZ_Conn;
        private static System.Data.IDbConnection DC_Conn;

        string ddd = "showmethemoney请点击http://Test.com?org=14&mobile=15900860546&task=6";
        private static void Main(string[] args)
        {
            HZ_Conn = new SqlConnection(HZ_ConnectionString);
            HZ_Conn.Open();

            DC_Conn = new SqlConnection(DC_ConnectionString);
            DC_Conn.Open();


            var temlate = "【安宁鑫湖医院】温馨提示，您的体检报告已经完成，请查看您的健康状况及阳性指标，永久保存报告详情请点击";
            //var temlate = "【河南省直三院】温馨提示，您的体检报告已经完成，请查看您的健康状况及阳性指标，永久保存报告详情请点击";
            var url = "http://webapp.hc.ihaozhuo.com/SMSPromotion.html#/{0}/{1}/{2}";
            //         http://webapp.hc.ihaozhuo.com/SMSPromotion.html#/{task}/{mobile}/{org}


            var table = "bjbr001";

            //var mobiles = new string[] { "15900860546","18297300619","17783055953" };
            var mobiles = HZ_Conn.Query<string>("select distinct Mobilphone from " + table);

            var shortLinkProxy = SortLinkServerProxy.ShareInstance();

            var time = DateTime.Now;
            var sms = (from c in mobiles
                       where  !string.IsNullOrEmpty(c) && Regex.Match(c,"1[2|3|5|7|8|][0-9]{9}").Success
                       select new
                       {
                           mobile = c,
                           sms = shortLinkProxy.getSortLink(string.Format(url,1,c,table)) + " "
                       }).ToList();

            Trace.WriteLine(string.Format("cost:{0}",DateTime.Now.Subtract(time).TotalSeconds));

            using(StreamWriter sw2 = new StreamWriter(table + ".txt",true,Encoding.Default))
            {
                var now = DateTime.Now;
                Trace.WriteLine("write start");

                foreach(var item in sms)
                {


                    sw2.WriteLine(item.mobile + "    " + item.sms);
                    //Trace.WriteLine(item);
                }

                sw2.Flush();

                Trace.WriteLine(string.Format("write cost:{0}",DateTime.Now.Subtract(now).TotalSeconds));

                Trace.WriteLine(string.Format("电话号码数量：{0}，有效电话号码：{1}",mobiles.Count(),sms.Count()));
            }


            return;


            var examSQL = @"SELECT
	                            t.AGE AS Age,
	                            t.BIRTHDAY AS Birthday,
	                            t.CHECK_DATE AS CheckDate,
	                            t.CHECK_UNIT_CODE AS CheckUnitCode,
	                            t.CHECK_UNIT_NAME AS CheckUnitName,
	                            t.COMMIT_USER_NAME AS CommitUserName,
	                            t.CUSTOMER_ID AS CustomerId,
	                            t.CUSTOMER_NAME AS CustomerName,
	                            t.ID AS Id,
	                            t.REG_DATE AS RegDate,
	                            t.REPORT_DATE AS ReportDate,
	                            t.WORK_NO AS WorkNo,
	                            t.Created_By AS CreatedBy,
	                            t.Created_On AS CreatedOn,
	                            t.Guid AS Guid,
	                            t.Is_Delete AS IsDelete,
	                            t.Modified_By AS ModifiedBy,
	                            t.Modified_On AS ModifiedOn,
	                            t.Version AS Version
                            FROM
	                            RPT_EXAM t
	                            where t.CHECK_UNIT_CODE = @checkUnitCode
	                            and t.WORK_NO = @workNo";

            //            var examSQL = @"SELECT
            //	t.AGE AS Age,
            //	t.BIRTHDAY AS Birthday,
            //	t.CHECK_DATE AS CheckDate,
            //	t.CHECK_UNIT_CODE AS CheckUnitCode,
            //	t.CHECK_UNIT_NAME AS CheckUnitName,
            //	t.COMMIT_USER_NAME AS CommitUserName,
            //	t.CUSTOMER_ID AS CustomerId,
            //	t.CUSTOMER_NAME AS CustomerName,
            //	t.ID AS Id,
            //	t.REG_DATE AS RegDate,
            //	t.REPORT_DATE AS ReportDate,
            //	t.WORK_NO AS WorkNo,
            //	t.Created_By AS CreatedBy,
            //	t.Created_On AS CreatedOn,
            //	t.Guid AS Guid,
            //	t.Is_Delete AS IsDelete,
            //	t.Modified_By AS ModifiedBy,
            //	t.Modified_On AS ModifiedOn,
            //	t.Version AS Version
            //FROM
            //	RPT_EXAM t
            //WHERE
            //	(t.CHECK_UNIT_CODE = :unitCode1
            //	AND t.WORK_NO = '0000000023')
            //	OR (t.CHECK_UNIT_CODE = 'bjbr002'
            //	AND t.WORK_NO = '0000000025')
            //	OR (t.CHECK_UNIT_CODE = 'bjbr002'
            //	AND t.WORK_NO = '0000000061')";

            var sql = @"SELECT
	t.CHECK_ITEM_CODE AS CheckItemCode,
	t.CHECK_ITEM_NAME AS CheckItemName,
	t.CHECK_STATE_ID AS CheckStateId,
	t.CHECK_USER_NAME AS CheckUserName,
	t.DEPARTMENT_NAME AS DepartmentName,
	t.ID AS Id,
	t.RPT_EXAM_ID AS RptExamId,
	t.SALE_PRICE AS SalePrice,
	t.SHOW_INDEX AS ShowIndex,
	t.Created_By AS CreatedBy,
	t.Created_On AS CreatedOn,
	t.Guid AS Guid,
	t.Is_Delete AS IsDelete,
	t.Modified_By AS ModifiedBy,
	t.Modified_On AS ModifiedOn,
	t.Version AS Version
FROM
	RPT_CHECK_ITEM t
WHERE
	t.Is_Delete = 0
	AND t.RPT_EXAM_ID = @examID

SELECT
	t.APPEND_INFO AS AppendInfo,
	t.CHECK_INDEX_CODE AS CheckIndexCode,
	t.CHECK_INDEX_NAME AS CheckIndexName,
	t.HIGH_VALUE_REF AS HighValueRef,
	t.HZ_CHECK_INDEX_NAME AS HzCheckIndexName,
	t.ID AS Id,
	t.IS_ABANDON AS IsAbandon,
	t.IS_CALC AS IsCalc,
	t.LOW_VALUE_REF AS LowValueRef,
	t.RESULT_FLAG_ID AS ResultFlagId,
	t.RESULT_TYPE_ID AS ResultTypeId,
	t.RESULT_VALUE AS ResultValue,
	t.RPT_EXAMITEM_ID AS RptExamitemId,
	t.SHOW_INDEX AS ShowIndex,
	t.TEXT_REF AS TextRef,
	t.UNIT AS Unit,
	t.Created_By AS CreatedBy,
	t.Created_On AS CreatedOn,
	t.Guid AS Guid,
	t.Is_Delete AS IsDelete,
	t.Modified_By AS ModifiedBy,
	t.Modified_On AS ModifiedOn,
	t.Version AS Version
FROM
	RPT_CHECK_RESULTS t
WHERE
	t.Is_Delete = 0
	AND t.RPT_EXAMITEM_ID IN (SELECT
			t1.ID
		FROM RPT_CHECK_ITEM t1
		WHERE t1.RPT_EXAM_ID = @examID)

SELECT
	t.Id AS Id,
	t.Is_Privacy AS IsPrivacy,
	t.Medical_Explanation AS MedicalExplanation,
	t.Reason_Result AS ReasonResult,
	t.Review_Advice AS ReviewAdvice,
	t.Rpt_Exam_Id AS RptExamId,
	t.Summary_Advice AS SummaryAdvice,
	t.Summary_Code AS SummaryCode,
	t.Summary_Description AS SummaryDescription,
	t.Summary_Name AS SummaryName,
	t.Created_By AS CreatedBy,
	t.Created_On AS CreatedOn,
	t.Guid AS Guid,
	t.Is_Delete AS IsDelete,
	t.Modified_By AS ModifiedBy,
	t.Modified_On AS ModifiedOn,
	t.Version AS Version
FROM
	RPT_SUMMARYS t
WHERE
	t.Is_Delete = 0
	AND t.RPT_EXAM_ID = @examID

SELECT
	t.Group_Info_Id
FROM
	RPT_LABELS t
WHERE
	t.Is_Delete = 0
	AND t.RPT_EXAM_ID = @examID";//


            var testsql = @"select count(t1.ID) 
                            from DOCTOR t1,DOCTOR_ACCOUNT t2
                            where t1.ID = t2.DOCTOR_ID
                            and t1.IS_DELETE = 0
                            and t2.IS_DELETE = 0
                            and t2.IS_ENABLED = 1
                            and t1.SERVICE_DEPT_ID = @deptID
                         
                            select count(t2.ID)
                            from DOCTOR t1,CUSTOMER t2,CUST_SERVICE_INFO t3,DOCTOR_ACCOUNT t4
                            where t1.ID = t3.DOCTOR_ID
                            and t2.ID = t3.CUSTOMER_ID
                            and t4.DOCTOR_ID = t1.ID
                            and t1.IS_DELETE = 0
                            and t2.IS_DELETE = 0
                            and t3.IS_DELETE = 0
                            and t4.IS_DELETE = 0
                            and t4.IS_ENABLED = 1
                            and t1.SERVICE_DEPT_ID = @deptID";



            var sql_dc = @"SELECT
	                            t1.RID as RID,
	                            t2.Code as CheckUnitCode,
	                            t1.Mobilphone as Mobilphone,
	                            t1.RecordDate as RecordDate
                            FROM
	                            CustomerReg t1,
	                            CheckUnits t2
                            WHERE
	                            t1.CheckUnitsRID = t2.RID
	                            AND t1.Mobilphone IS NOT NULL
	                            AND t1.Mobilphone != ''
	                            AND t1.RecordDate >= CAST(@dateStart AS DATETIME)
	                            AND t1.RecordDate <= CAST(@dateEnd AS DATETIME)
	                            AND t2.Code IN (@deptCodes)";

            var testSql = @"SELECT
	t5.date,
	ISNULL(t1.count, 0) AS SmsSendCount,
	ISNULL(t2.count, 0) AS Hits,
	ISNULL(t3.count, 0) AS Dloaded,
	ISNULL(t4.count, 0) AS Registrations
FROM

	(SELECT
			CAST(t.CREATED_ON AS DATE) AS date,
			COUNT(t.CREATED_ON) AS count
		FROM dbo.SMS_DETAIL t
		WHERE t.CREATED_ON >= @start
		AND t.CREATED_ON <= DATEADD(day,1,@end)
		--and t.SERVICE_DEPT_ID = 
		GROUP BY CAST(t.CREATED_ON AS DATE)) t1
	FULL OUTER JOIN (SELECT
			CAST(t.CREATED_ON AS DATE) AS date,
			COUNT(t.CREATED_ON) AS count
		FROM dbo.SMS_POPULARIZE_TRAFFIC t
		WHERE t.CREATED_ON >= @start
		AND t.CREATED_ON <= DATEADD(day,1,@end)
		--and t.SERVICE_DEPT_ID = 
		GROUP BY CAST(t.CREATED_ON AS DATE)) t2 ON t1.date = t2.date
	FULL OUTER JOIN (SELECT
			CAST(t.CREATED_ON AS DATE) AS date,
			COUNT(t.CREATED_ON) AS count
		FROM dbo.SMS_POPULARIZE_DOWNLOADS t
		WHERE t.CREATED_ON >= @start
		AND t.CREATED_ON <= DATEADD(day,1,@end)
		--and t.SERVICE_DEPT_ID = 
		GROUP BY CAST(t.CREATED_ON AS DATE)) t3 ON t2.date = t3.date
	FULL OUTER JOIN (SELECT
			CAST(t1.CREATED_ON AS DATE) AS date,
			COUNT(t1.CREATED_ON) AS count
		FROM	dbo.CUSTOMER t1,
				dbo.CUST_SERVICE_INFO t2
		WHERE t1.ID = t2.CUSTOMER_ID
		AND t1.CREATED_ON >= @start
		AND t1.CREATED_ON <= DATEADD(day,1,@end)
		--and t2.SERVICE_DEPT_ID = 
		GROUP BY CAST(t1.CREATED_ON AS DATE)) t4 ON t3.date = t4.date
	RIGHT OUTER JOIN (SELECT
			CONVERT(VARCHAR(10), DATEADD(dd, number, CONVERT(VARCHAR(8), @start, 120) + '01'), 120) AS date
		FROM master..spt_values
		WHERE type = 'P'
		AND DATEADD(dd, number, CONVERT(VARCHAR(8), @start, 120) + '01') <= @end
		AND DATEADD(dd, number, CONVERT(VARCHAR(8), @start, 120) + '01') >= @start) t5 ON t4.date = t5.date";


            var testSql2 = @"SELECT
	t5.date,
	ISNULL(t1.count, 0) AS SmsSendCount,
	ISNULL(t2.count, 0) AS Hits,
	ISNULL(t3.count, 0) AS Dloaded,
	ISNULL(t4.count, 0) AS Registrations
FROM

	(SELECT
			CAST(t.CREATED_ON AS DATE) AS date,
			COUNT(t.CREATED_ON) AS count
		FROM dbo.SMS_DETAIL t
		WHERE t.CREATED_ON >= @start
		AND t.CREATED_ON <= DATEADD(DAY, 1, @end)
		and t.SERVICE_DEPT_ID = @deptID
		GROUP BY CAST(t.CREATED_ON AS DATE)) t1
	FULL OUTER JOIN (SELECT
			CAST(t.CREATED_ON AS DATE) AS date,
			COUNT(t.CREATED_ON) AS count
		FROM dbo.SMS_POPULARIZE_TRAFFIC t
		WHERE t.CREATED_ON >= @start
		AND t.CREATED_ON <= DATEADD(DAY, 1, @end)
		and t.SERVICE_DEPT_ID = @deptID
		GROUP BY CAST(t.CREATED_ON AS DATE)) t2 ON t1.date = t2.date
	FULL OUTER JOIN (SELECT
			CAST(t.CREATED_ON AS DATE) AS date,
			COUNT(t.CREATED_ON) AS count
		FROM dbo.SMS_POPULARIZE_DOWNLOADS t
		WHERE t.CREATED_ON >= @start
		AND t.CREATED_ON <= DATEADD(DAY, 1, @end)
		and t.SERVICE_DEPT_ID = @deptID
		GROUP BY CAST(t.CREATED_ON AS DATE)) t3 ON t2.date = t3.date
	FULL OUTER JOIN (SELECT
			CAST(t1.CREATED_ON AS DATE) AS date,
			COUNT(t1.CREATED_ON) AS count
		FROM	dbo.CUSTOMER t1,
				dbo.CUST_SERVICE_INFO t2
		WHERE t1.ID = t2.CUSTOMER_ID
		AND t1.CREATED_ON >= @start
		AND t1.CREATED_ON <= DATEADD(DAY, 1, @end)
		and t2.SERVICE_DEPT_ID = @deptID
		GROUP BY CAST(t1.CREATED_ON AS DATE)) t4 ON t3.date = t4.date
	RIGHT OUTER JOIN (SELECT
			CONVERT(VARCHAR(10), DATEADD(dd, number, CONVERT(VARCHAR(8), @start, 120) + '01'), 120) AS date
		FROM master..spt_values
		WHERE type = 'P'
		AND DATEADD(dd, number, CONVERT(VARCHAR(8), @start, 120) + '01') <= @end
		AND DATEADD(dd, number, CONVERT(VARCHAR(8), @start, 120) + '01') >= @start) t5 ON t4.date = t5.date";














            var items = HZ_Conn.Query<SmsStatisticResultItem>(testSql2,new { start = new DateTime(2016,5,1),end = new DateTime(2016,5,30),deptID = 4 });

            //Console.ReadLine();


            //var dddd = DC_Conn.Query<DataCenterCustomerInfoModel>(sql_dc,new { deptCodes = new string[] { "bjbr002" },dateStart = new DateTime(2016,2,17),dateEnd = new DateTime(2016,2,18) });

            //Stopwatch timer = new Stopwatch();


            //var testKeys = new int[] { 100,200,500,1000,1500,2000 };

            //foreach(var key in testKeys)
            //{
            //    var mobiles = dddd.Take(key).Select(c => c.Mobilphone);
            //    var sql_hz_mobile = @"select t.MOBILE from CUSTOMER t where t.MOBILE in @mobile";

            //    timer.Start();

            //    var ddddddd = HZ_Conn.Query<string>(sql_hz_mobile,new { mobile = mobiles });

            //    timer.Stop();

            //    var second = timer.Elapsed.TotalSeconds;
            //    Trace.WriteLine(string.Format("{1} phones Execution time was {0:F1} s.",second,key));
            //}

            //Console.ReadLine();


            //var query1 = HZ_Conn.QueryMultiple(testsql,new { deptID = 4 });
            //var count1 = query1.Read<int>();
            //var count2 = query1.Read<int>();

            var exams = HZ_Conn.Query<RptExam>(examSQL,new { checkUnitCode = "bjbr002",workNo = "0000068516" });
            var exam = exams.FirstOrDefault();
            if(exam != null)
            {
                using(var query = HZ_Conn.QueryMultiple(sql,new { examID = exams.FirstOrDefault().Id }))
                {
                    var chechItems = query.Read<RptCheckItem>().ToList();
                    var checkResults = query.Read<RptCheckResult>().ToList();
                    var summarys = query.Read<RptSummary>().ToList();
                    var labels = query.Read<int>().ToList();
                }
            }
        }
    }
}