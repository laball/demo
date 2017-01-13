using System.Collections.Generic;

namespace ExcelDemo
{
    public class GeneReportResponse
    {
        public string code { get; set; }

        public GeneReport data { get; set; }

        public string msg { get; set; }
    }

    /// <summary>
    /// 基因报告
    /// </summary>
    public class GeneReport
    {
        /// <summary>
        /// 个性化建议
        /// </summary>
        public IEnumerable<AdviceItem> advices { get; set; }
        /// <summary>
        /// 个性化建议图片地址
        /// </summary>
        public string advicesBadge { get; set; }
        /// <summary>
        /// 受检人出生日期
        /// </summary>
        public string birthday { get; set; }
        /// <summary>
        /// 生活习惯
        /// </summary>
        public string custom { get; set; }
        /// <summary>
        /// 背景知识HTML5 url
        /// </summary>
        public string desc { get; set; }
        /// <summary>
        /// 背景知识图片url
        /// </summary>
        public string descBadge { get; set; }
        /// <summary>
        /// 数据有效截止时间
        /// </summary>
        public string enddate { get; set; }
        /// <summary>
        /// 受检人性别
        /// </summary>
        public string gender { get; set; }
        /// <summary>
        /// 检测项目
        /// </summary>
        public IEnumerable<CheckItem> items { get; set; }
        /// <summary>
        /// 受检人姓名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 用户须知 h5 url
        /// </summary>
        public string notice { get; set; }
        /// <summary>
        /// 用户须知图片url
        /// </summary>
        public string noticeBadge { get; set; }
        /// <summary>
        /// 报告时间
        /// </summary>
        public string reportDate { get; set; }
        public Sample sample { get; set; }
        /// <summary>
        /// 样本号
        /// </summary>
        public string sampleSerial { get; set; }
        /// <summary>
        ///套餐名称
        /// </summary>
        public string suiteTokenText { get; set; }
    }

    /// <summary>
    /// 个性化建议
    /// </summary>
    public class AdviceItem
    {
        public string custom { get; set; }
        public string desc { get; set; }
    }

    /// <summary>
    /// 检测项目
    /// </summary>
    public class CheckItem
    {
        /// <summary>
        /// 单项相关背景知识h5 url
        /// </summary>
        public string illnessDesc { get; set; }
        /// <summary>
        /// 项目图片url
        /// </summary>
        public string itemDadge { get; set; }
        /// <summary>
        /// 检测套餐中对应检测单项名称
        /// </summary>
        public string itemTokenText { get; set; }
        /// <summary>
        /// 预防建议详细信息
        /// </summary>
        public IEnumerable<PrecautionDetailItem> precautionDetails { get; set; }
        /// <summary>
        /// 参考文献
        /// </summary>
        public IEnumerable<string> refs { get; set; }
        public Risk risk { get; set; }
        public IEnumerable<SnpDescItem> precautionBadge { get; set; }
        public IEnumerable<SnpResultItem> snpResults { get; set; }
        /// <summary>
        /// 结果解读
        /// </summary>
        public string snpSummary { get; set; }
    }

    public class PrecautionDetailItem
    {
        /// <summary>
        /// 详情
        /// </summary>
        public string details { get; set; }
        /// <summary>
        /// 建议图片URL
        /// </summary>
        public string precautionBadge { get; set; }
    }

    public class Risk
    {
        /// <summary>
        /// 危险等级图片url
        /// </summary>
        public string levelBadge { get; set; }
        /// <summary>
        /// 危险等级
        /// </summary>
        public string levelText { get; set; }
        /// <summary>
        /// 风险描述
        /// </summary>
        public string riskDesc { get; set; }
    }

    public class SnpDescItem
    {
        public string geneSerial { get; set; }
        public string snpDesc { get; set; }
    }

    public class SnpResultItem
    {
        public string geneCode { get; set; }
        public string geneEffect { get; set; }
        public string geneFrequencey { get; set; }
        public string genePoint { get; set; }
        public string geneSerial { get; set; }
    }

    public class Sample
    {
        /// <summary>
        /// 收养日期
        /// </summary>
        public string reception { get; set; }
        /// <summary>
        /// 采样日期
        /// </summary>
        public string sampling { get; set; }
        /// <summary>
        /// 样本类型
        /// </summary>
        public string type { get; set; }
    }
}
