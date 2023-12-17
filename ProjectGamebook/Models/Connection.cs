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

        public bool? CanBeUsed { get; set; }
        public Connection(int from, int target, string top, string left, string width, string height, Func<int, bool>? ConditionCheck, string? special)
        {
            From = from;
            Target = target;
            Top = top;
            Left = left;
            Width = width;
            Height = height;
            if (ConditionCheck != null)
            {
                CanBeUsed = ConditionCheck(From);
            }
            else CanBeUsed = true;
            TargetSpecialPage = special;
        }
    }
}
