using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordle.Shared
{
    public class ToastModel
    {
        public int Id { get; set; }
        public int padding { get; set; }
        public string Heading { get; set; }
        public string Message { get; set; }
        public bool IsVisible { get; set; }
        public string BackgroundCssClass { get; set; }
        public string IconCssClass { get; set; }
    }
}
