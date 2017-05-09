namespace Frame.Models.SysModels
{
    public class CommandResult<T> where T : class
    {
        public ResultStatus ResultStatus { get; set; } = ResultStatus.Error;
        public string Message { get; set; }
        public T Data { get; set; }
    }

    public enum ResultStatus
    {
        Success,
        Error
    }
}
