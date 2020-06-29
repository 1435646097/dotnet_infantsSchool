namespace Model.Helper
{
    public class MessageModel<T> where T : class
    {
        public bool Success { get; set; } = true;
        public int Code { get; set; } = 200;
        public string Msg { get; set; } = "成功！！！";
        public T Data { get; set; }
    }
}