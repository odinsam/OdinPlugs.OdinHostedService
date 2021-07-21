using System;

namespace OdinPlugs.OdinHostedService.BgServiceModels
{
    public class OdinBgJob_Model : OdinJob_Model
    {
        public Action StartAsyncAction { get; set; }
        public Action ExecuteAsyncAction { get; set; }
        public Action StopAsyncAction { get; set; }
        public Action DisposeAction { get; set; }
    }
}