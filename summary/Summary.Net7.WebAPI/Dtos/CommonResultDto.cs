namespace Summary.Net7.WebAPI.Dtos
{
    public class CommonResultDto<T>
    {
        public T Data { get; set; }

        public int Code { get; set; }

        public string Message { get; set; }

        public string MoreMessages { get; set; }

        public int TipType { get; set; }
    }
}