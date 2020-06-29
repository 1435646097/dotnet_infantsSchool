namespace Model.Dtos
{
    public class LoginDto
    {
        public string Account { get; set; }
        public string Pwd { get; set; }
        public string Captcha { get; set; }
        public string Cid { get; set; }
    }
}