namespace Frame.Models.SysModels.Log
{
    public class LogAllResponseModel
    {
        public int RowId { get; set; }

        public int LogId { get; set; }

        public string LoginName { get; set; }

        public string LoginTime { get; set; }

        public string LoginRole { get; set; }

        public string LoginMach { get; set; }

        public string LoginCpu { get; set; }
    }
}
