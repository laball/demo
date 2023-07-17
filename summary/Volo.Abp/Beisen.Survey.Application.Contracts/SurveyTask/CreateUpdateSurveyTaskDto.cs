﻿namespace Beisen.Survey.Application.Contracts
{
    public class CreateUpdateSurveyTaskDto
    {
        /// <summary>
        /// 申请ID
        /// </summary>
        public Guid ApplyId { get; set; }

        /// <summary>
        /// 应聘者ID
        /// </summary>
        public Guid ApplicantId { get; set; }

        /// <summary>
        /// 职位ID
        /// </summary>
        public Guid JobId { get; set; }

        /// <summary>
        /// 任务状态
        /// 1-已发送
        /// 2-已完成
        /// 3-已过期
        /// 4-已取消
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 问卷ID
        /// </summary>
        public string SurveyId { get; set; }

        /// <summary>
        /// 作答明细ID
        /// </summary>
        public string AnswerId { get; set; }

        /// <summary>
        /// 有效截止时间
        /// </summary>
        public DateTime Deadline { get; set; }
    }
}