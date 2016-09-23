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
using System.Threading.Tasks;
using System.Threading;

namespace DapperDemo
{

    public class CustomerCreateOrUpdateModel
    {
        /// <summary>
        /// 客户ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 是否新创建客户
        /// </summary>
        public bool IsNew { get; set; }
    }

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
        private static string HZ_ConnectionString = "Pooling=True;Min Pool Size=20;Max Pool Size=1000;Connection Timeout=30;server=HZSWVDSQL01;database=HzHmisTest;uid=apidev;pwd=haozhuo2015;";
        private static string DC_ConnectionString = "Pooling=True;Min Pool Size=20;Max Pool Size=1000;Connection Timeout=30;server=HZSWVDSQL01;database=hzdatacenter;uid=Dcdbprg;pwd=a1234567;";
        private static System.Data.IDbConnection HZ_Conn;
        private static System.Data.IDbConnection DC_Conn;

        string ddd = "showmethemoney请点击http://Test.com?org=14&mobile=15900860546&task=6";
        private static void Main(string[] args)
        {
            HZ_Conn = new SqlConnection(HZ_ConnectionString);
            HZ_Conn.Open();


            var ddddd = @"DECLARE @cname nvarchar(50);
DECLARE @nikename nvarchar(50);
DECLARE @mobile varchar(30);
DECLARE @certificatetype nvarchar(20);
DECLARE @certificatecode nvarchar(50);
DECLARE @address nvarchar(200);
DECLARE @photourl nvarchar(500);
DECLARE @birthday date;
DECLARE @career nvarchar(20);
DECLARE @gender nvarchar(2);
DECLARE @heigth decimal(9,2);
DECLARE @weight decimal(9,2);
DECLARE @regison datetime;

set @cname = 'test_cname21';
SET @nikename = 'nikename_';
SET @mobile = 'mobile';
SET @certificatetype = '1';
SET @certificatecode = 'certificatecode';
SET @address = 'address';
SET @photourl = 'photourl';
SET @birthday = '1887-03-15';
SET @career = 'career';
SET @gender = '1';
SET @heigth = 2.08;
SET @weight = 66.66;
set @regison = GETDATE();

DECLARE @accountID varchar(40);
set @accountID = 'debc83c4-e7ba-4174-a40b-cc56b1540325';

DECLARE @ID int;
DECLARE @isNew as bit = 0;

MERGE CUSTOMER as target
using(select @accountID as AccountID) as source
on target.ACCOUNT_ID = Source.AccountID AND target.IS_DELETE = 0
WHEN MATCHED THEN
	UPDATE set CNAME = @cname
	          ,NICKNAME = @nikename
	          ,MOBILE = @mobile
	          ,CERTIFICATE_TYPE = @certificatetype
	          ,CERTIFICATE_CODE = @certificatecode
	          ,ADDRESS = @address
	          ,PHOTO_URL = @photourl
	          ,BIRTHDAY = @birthday
	          ,CAREER = @career
	          ,GENDER = @gender
	          ,HEIGHT = @heigth
	          ,WEIGHT = @weight
	          ,REGIST_ON = @regison
			  ,VERSION = target.VERSION + 1
			  ,@ID = target.ID
WHEN NOT MATCHED BY TARGET THEN
	INSERT (CNAME
	       ,NICKNAME
	       ,MOBILE
	       ,CERTIFICATE_TYPE
	       ,CERTIFICATE_CODE
	       ,ADDRESS
	       ,PHOTO_URL
	       ,BIRTHDAY
	       ,CAREER
	       ,GENDER
	       ,HEIGHT
	       ,WEIGHT
	       ,REGIST_ON
	       ,ACCOUNT_ID
	       ,IS_DELETE
	       ,CREATED_ON
	       ,MODIFIED_ON
	       ,VERSION
	       ,GUID)
	values(@cname
		  ,@nikename
		  ,@mobile
		  ,@certificatetype
		  ,@certificatecode
		  ,@address
		  ,@photourl
		  ,@birthday
		  ,@career
		  ,@gender
		  ,@heigth
		  ,@weight
		  ,@regison
		  ,@accountID
		  ,0
		  ,GETDATE()
		  ,GETDATE()
		  ,1
		  ,NEWID());

if @ID is NULL 
begin
set @ID = SCOPE_IDENTITY()
set @isNew = 1;
end;
SELECT @ID as ID,@isNew as IsNew";



            for (int i = 0; i < 1000; i++)
            {
                Task.Factory.StartNew(() =>
                {
                    try
                    {
                        using (var conn = new SqlConnection(HZ_ConnectionString))
                        {
                            conn.Open();

                            for (int j= 0; j < 100; j++)
                            {
                                conn.Execute(ddddd);

                                Thread.Sleep(50);
                            }
                        }                      
                    }
                    catch (System.Exception ex)
                    {
                        Trace.WriteLine(ex.StackTrace);
                    }
                });
            }

            Console.WriteLine("完成");
            Console.ReadLine();

            //DC_Conn = new SqlConnection(DC_ConnectionString);
            //DC_Conn.Open();


            var sql56 = @"SELECT
	Date,ISNULL([4],0) AS [4],ISNULL([7],0) AS [7],ISNULL([14],0) AS [14],ISNULL([15],0) AS [15],ISNULL([19],0) AS [19],ISNULL([20],0) AS [20],ISNULL([21],0) AS [21],ISNULL([22],0) AS [22],ISNULL([23],0) AS [23],ISNULL([24],0) AS [24],ISNULL([25],0) AS [25],ISNULL([26],0) AS [26],ISNULL([27],0) AS [27],ISNULL([28],0) AS [28],ISNULL([29],0) AS [29],ISNULL([30],0) AS [30],ISNULL([1020],0) AS [1020],ISNULL([1021],0) AS [1021],ISNULL([1022],0) AS [1022],ISNULL([1023],0) AS [1023],ISNULL([1024],0) AS [1024],ISNULL([1025],0) AS [1025],ISNULL([1026],0) AS [1026],ISNULL([1027],0) AS [1027],ISNULL([1028],0) AS [1028],ISNULL([1029],0) AS [1029],ISNULL([1030],0) AS [1030],ISNULL([1031],0) AS [1031],ISNULL([1032],0) AS [1032],ISNULL([1033],0) AS [1033],ISNULL([1034],0) AS [1034],ISNULL([1035],0) AS [1035],ISNULL([1036],0) AS [1036],ISNULL([1037],0) AS [1037],ISNULL([1038],0) AS [1038],ISNULL([1039],0) AS [1039],ISNULL([1040],0) AS [1040],ISNULL([1041],0) AS [1041],ISNULL([1042],0) AS [1042],ISNULL([1043],0) AS [1043],ISNULL([1044],0) AS [1044],ISNULL([1045],0) AS [1045],ISNULL([1046],0) AS [1046],ISNULL([1047],0) AS [1047],ISNULL([1048],0) AS [1048],ISNULL([1049],0) AS [1049],ISNULL([1050],0) AS [1050],ISNULL([1051],0) AS [1051],ISNULL([1052],0) AS [1052],ISNULL([1053],0) AS [1053],ISNULL([1054],0) AS [1054],ISNULL([1055],0) AS [1055],ISNULL([1056],0) AS [1056],ISNULL([1057],0) AS [1057],ISNULL([1058],0) AS [1058],ISNULL([1059],0) AS [1059],ISNULL([1060],0) AS [1060],ISNULL([1061],0) AS [1061],ISNULL([1062],0) AS [1062],ISNULL([1063],0) AS [1063],ISNULL([1066],0) AS [1066],ISNULL([1067],0) AS [1067],ISNULL([1068],0) AS [1068],ISNULL([1069],0) AS [1069],ISNULL([1070],0) AS [1070],ISNULL([1071],0) AS [1071],ISNULL([1072],0) AS [1072],ISNULL([1073],0) AS [1073],ISNULL([1074],0) AS [1074],ISNULL([1075],0) AS [1075],ISNULL([1076],0) AS [1076],ISNULL([1077],0) AS [1077],ISNULL([1078],0) AS [1078],ISNULL([1079],0) AS [1079],ISNULL([1080],0) AS [1080],ISNULL([1081],0) AS [1081],ISNULL([1082],0) AS [1082],ISNULL([1083],0) AS [1083],ISNULL([1084],0) AS [1084],ISNULL([1085],0) AS [1085],ISNULL([1086],0) AS [1086],ISNULL([1087],0) AS [1087],ISNULL([2081],0) AS [2081],ISNULL([2082],0) AS [2082],ISNULL([2083],0) AS [2083],ISNULL([2084],0) AS [2084],ISNULL([2085],0) AS [2085],ISNULL([2086],0) AS [2086],ISNULL([2087],0) AS [2087],ISNULL([2088],0) AS [2088],ISNULL([2089],0) AS [2089],ISNULL([2090],0) AS [2090],ISNULL([2091],0) AS [2091],ISNULL([2092],0) AS [2092],ISNULL([2093],0) AS [2093],ISNULL([2094],0) AS [2094],ISNULL([2095],0) AS [2095],ISNULL([2096],0) AS [2096],ISNULL([2097],0) AS [2097],ISNULL([2098],0) AS [2098],ISNULL([2099],0) AS [2099],ISNULL([2104],0) AS [2104],ISNULL([2105],0) AS [2105],ISNULL([2106],0) AS [2106],ISNULL([2107],0) AS [2107],ISNULL([2108],0) AS [2108],ISNULL([2109],0) AS [2109],ISNULL([2110],0) AS [2110],ISNULL([2111],0) AS [2111],ISNULL([2112],0) AS [2112],ISNULL([2113],0) AS [2113],ISNULL([2114],0) AS [2114],ISNULL([2115],0) AS [2115],ISNULL([2116],0) AS [2116],ISNULL([2117],0) AS [2117],ISNULL([2118],0) AS [2118],ISNULL([2119],0) AS [2119],ISNULL([2120],0) AS [2120],ISNULL([2121],0) AS [2121],ISNULL([2122],0) AS [2122],ISNULL([2123],0) AS [2123],ISNULL([2124],0) AS [2124],ISNULL([2125],0) AS [2125],ISNULL([2126],0) AS [2126],ISNULL([2127],0) AS [2127],ISNULL([2128],0) AS [2128],ISNULL([2129],0) AS [2129],ISNULL([2130],0) AS [2130],ISNULL([2131],0) AS [2131],ISNULL([2132],0) AS [2132],ISNULL([2133],0) AS [2133],ISNULL([2134],0) AS [2134],ISNULL([2135],0) AS [2135],ISNULL([2136],0) AS [2136],ISNULL([2137],0) AS [2137],ISNULL([2138],0) AS [2138],ISNULL([3137],0) AS [3137],ISNULL([3138],0) AS [3138],ISNULL([3139],0) AS [3139],ISNULL([3140],0) AS [3140],ISNULL([3143],0) AS [3143],ISNULL([3144],0) AS [3144],ISNULL([3145],0) AS [3145],ISNULL([3146],0) AS [3146],ISNULL([3147],0) AS [3147],ISNULL([3148],0) AS [3148],ISNULL([3149],0) AS [3149]
FROM
	(SELECT
			t1.date AS Date
			,ISNULL(SEVICE_DEPT_ID, 0) AS DeptID
			,ISNULL(RegistCount, 0) AS RegistCount
		FROM (SELECT
				CONVERT(VARCHAR(10), DATEADD(dd, number, CONVERT(VARCHAR(8), '2016-08-05', 120) + '01'), 120) AS date
			FROM master..spt_values
			WHERE type = 'P'
			AND DATEADD(dd, number, CONVERT(VARCHAR(8), '2016-08-05', 120) + '01') <= '2016-08-23'
			AND DATEADD(dd, number, CONVERT(VARCHAR(8), '2016-08-05', 120) + '01') >= '2016-08-05') t1
		LEFT JOIN (SELECT
				STAT_DATE
				,SEVICE_DEPT_ID
				,SUM(REGIST_COUNT) RegistCount
			FROM STAT_SCAN_REGIST_DAILY
			WHERE IS_DELETE = 0
			 AND STAT_DATE >= '2016-08-05'
			AND STAT_DATE < '2016-08-23'
			GROUP BY	STAT_DATE
						,SEVICE_DEPT_ID) t2
			ON t1.date = t2.STAT_DATE) tt1

	PIVOT (SUM(RegistCount) FOR DeptID IN ([4],[7],[14],[15],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[1020],[1021],[1022],[1023],[1024],[1025],[1026],[1027],[1028],[1029],[1030],[1031],[1032],[1033],[1034],[1035],[1036],[1037],[1038],[1039],[1040],[1041],[1042],[1043],[1044],[1045],[1046],[1047],[1048],[1049],[1050],[1051],[1052],[1053],[1054],[1055],[1056],[1057],[1058],[1059],[1060],[1061],[1062],[1063],[1066],[1067],[1068],[1069],[1070],[1071],[1072],[1073],[1074],[1075],[1076],[1077],[1078],[1079],[1080],[1081],[1082],[1083],[1084],[1085],[1086],[1087],[2081],[2082],[2083],[2084],[2085],[2086],[2087],[2088],[2089],[2090],[2091],[2092],[2093],[2094],[2095],[2096],[2097],[2098],[2099],[2104],[2105],[2106],[2107],[2108],[2109],[2110],[2111],[2112],[2113],[2114],[2115],[2116],[2117],[2118],[2119],[2120],[2121],[2122],[2123],[2124],[2125],[2126],[2127],[2128],[2129],[2130],[2131],[2132],[2133],[2134],[2135],[2136],[2137],[2138],[3137],[3138],[3139],[3140],[3143],[3144],[3145],[3146],[3147],[3148],[3149])) AS ReGist";

            var result = HZ_Conn.Query(sql56);





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
                       where !string.IsNullOrEmpty(c) && Regex.Match(c, "1[2|3|5|7|8|][0-9]{9}").Success
                       select new
                       {
                           mobile = c,
                           sms = shortLinkProxy.getSortLink(string.Format(url, 1, c, table)) + " "
                       }).ToList();

            Trace.WriteLine(string.Format("cost:{0}", DateTime.Now.Subtract(time).TotalSeconds));

            using (StreamWriter sw2 = new StreamWriter(table + ".txt", true, Encoding.Default))
            {
                var now = DateTime.Now;
                Trace.WriteLine("write start");

                foreach (var item in sms)
                {


                    sw2.WriteLine(item.mobile + "    " + item.sms);
                    //Trace.WriteLine(item);
                }

                sw2.Flush();

                Trace.WriteLine(string.Format("write cost:{0}", DateTime.Now.Subtract(now).TotalSeconds));

                Trace.WriteLine(string.Format("电话号码数量：{0}，有效电话号码：{1}", mobiles.Count(), sms.Count()));
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














            var items = HZ_Conn.Query<SmsStatisticResultItem>(testSql2, new { start = new DateTime(2016, 5, 1), end = new DateTime(2016, 5, 30), deptID = 4 });

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

            var exams = HZ_Conn.Query<RptExam>(examSQL, new { checkUnitCode = "bjbr002", workNo = "0000068516" });
            var exam = exams.FirstOrDefault();
            if (exam != null)
            {
                using (var query = HZ_Conn.QueryMultiple(sql, new { examID = exams.FirstOrDefault().Id }))
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