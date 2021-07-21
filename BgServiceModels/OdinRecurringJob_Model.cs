using System;

namespace OdinPlugs.OdinHostedService.BgServiceModels
{
    public class OdinRecurringJob_Model : OdinJob_Model
    {
        public TimeSpan Period { get; set; }
    }
}