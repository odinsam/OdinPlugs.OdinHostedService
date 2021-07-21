using System;

namespace OdinPlugs.OdinHostedService.BgServiceModels
{
    public class OdinScheduleJob_Model : OdinJob_Model
    {
        /// <summary>
        /// 延迟的时间量 ms
        /// </summary>
        /// <value></value>
        public int DueTime { get; set; }

    }
}