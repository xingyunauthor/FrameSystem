using System;

namespace Frame.Models.SysModels.Log
{
    public class LogAddRequestModel
    {
        public string LoginName { get; set; }

        public DateTime LoginTime { get; set; }

        public string LoginRole { get; set; }

        public string LoginMach { get; set; }

        public string LoginCpu { get; set; }
    }
}
