namespace Beisen.Survey.Domain.Shared.SurveyTask
{
    /// <summary>
    /// 问卷任务状态
    /// </summary>
    public enum SurveyTaskStatus
    {
        /// <summary>
        /// 已发送
        /// </summary>
        Sent = 1,

        /// <summary>
        /// 已完成
        /// </summary>
        Complete,

        /// <summary>
        /// 已过期
        /// </summary>
        Expired,

        /// <summary>
        /// 已取消
        /// </summary>
        Cancelled
    }
}