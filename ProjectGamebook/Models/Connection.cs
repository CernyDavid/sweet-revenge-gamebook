namespace ProjectGamebook.Models
{
    public class Connection
    {
        public int From { get; set; }
        public string TargetSpecialPage { get; set; } = null;
        public int Target { get; set; }
        public string Top { get; set; }
        public string Left { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
    }
}
