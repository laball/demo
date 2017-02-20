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
        public IList<Advice> advices { get; set; }
        /// <summary>
        /// 个性化建议图片地址
        /// </summary>
        public string advicesBadge { get; set; }
        /// <summary>
        /// 受检人出生日期
        /// </summary>
        public string birthday { get; set; }
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
        public IList<CheckItem> items { get; set; }
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
    public class Advice
    {
        /// <summary>
        /// 生活习惯
        /// </summary>
        public string custom { get; set; }
        /// <summary>
        /// 生活习惯改善建议
        /// </summary>
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
        public string itemBadge { get; set; }
        /// <summary>
        /// 检测套餐中对应检测单项名称
        /// </summary>
        public string itemTokenText { get; set; }
        /// <summary>
        /// 预防建议详细信息
        /// </summary>
        public IList<Precaution> precautions { get; set; }
        /// <summary>
        /// 参考文献
        /// </summary>
        public IList<string> refs { get; set; }
        /// <summary>
        /// 结果
        /// </summary>
        public Risk risk { get; set; }
        public IList<SnpDesc> snpDescs { get; set; }
        public IList<SnpResult> snpResults { get; set; }
        /// <summary>
        /// 结果解读
        /// </summary>
        public string snpSummary { get; set; }
    }

    /// <summary>
    /// 预防建议详细信息项
    /// </summary>
    public class Precaution
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

    /// <summary>
    /// 结果
    /// </summary>
    public class Risk
    {
        /// <summary>
        /// 危险等级图片url
        /// LOW,LOW_PLUS,NORMAL,HIGH_MINUS,HIGH
        /// </summary>
        public string level { get; set; }
        /// <summary>
        /// 危险等级图片url
        /// </summary>
        public string levelBadge { get; set; }
        /// <summary>
        /// 风险描述
        /// </summary>
        public string riskDesc { get; set; }
    }

    public class SnpDesc
    {
        /// <summary>
        /// SNP对应的基因编号
        /// </summary>
        public string geneSerial { get; set; }
        /// <summary>
        /// 当前单项选择该SNP的解析
        /// </summary>
        public string snpDesc { get; set; }
    }

    public class SnpResult
    {
        /// <summary>
        /// 基因型
        /// TT，CG等，当前位置的碱基序列
        /// </summary>
        public string geneCode { get; set; }
        /// <summary>
        /// 该基因型对当前疾病的影响
        /// →：无影响；↑：增加发病风险；：↓降低发病风险
        /// </summary>
        public string geneEffect { get; set; }
        /// <summary>
        /// 该基因型在普通人群中出现的百分比
        /// </summary>
        public string geneFrequencey { get; set; }
        /// <summary>
        /// 位置在当前基因中的编号
        /// </summary>
        public string genePoint { get; set; }
        /// <summary>
        /// 基因编号
        /// </summary>
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
