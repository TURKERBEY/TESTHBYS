using System.Collections.Generic;

namespace Web.Models
{
    public class SubHeaderVM
    {
        public SubHeaderVM()
        {
            Button = new HashSet<ButtonVM>();
        }
        public string MainCategory { get; set; } = string.Empty;
        public string FirstSubCategory { get; set; } = string.Empty;
        public string FirstUrl { get; set; } = string.Empty;
        public string SecondSubCategory { get; set; } = string.Empty;
        public string SecondUrl { get; set; } = string.Empty;
        public string ThirdSubCategory { get; set; } = string.Empty;
        public bool ShowButton { get; set; } = false;
        public ICollection<ButtonVM> Button { get; set; }
    }

    public class ButtonVM
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string TargetModalId { get; set; } = string.Empty;
    }
}
