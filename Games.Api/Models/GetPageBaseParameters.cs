namespace Games.Api.Models
{
    public class GetPageBaseParameters
    {
        public int? Size { get; set; }
        public int? PageIndex { get; set; }
        public string OrderBy { get; set; }
        public bool? IsDescending { get; set; }
    }
}
