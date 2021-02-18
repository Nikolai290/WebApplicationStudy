using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models.ViewModels {
    public class ResultViewModel {
        public virtual string Message { get; set; }
        public virtual string Title { get; set; }
        public virtual string Link { get; set; }
        public virtual string ButtonText { get; set; }
        public ResultViewModel(string title, string message, string link, string buttonText) {
            Title = title;
            Message = message;
            Link = String.IsNullOrEmpty(link) ? "~/Home/" : link;
            ButtonText = String.IsNullOrEmpty(buttonText)? "На главную" : buttonText;
        }
    }
}
