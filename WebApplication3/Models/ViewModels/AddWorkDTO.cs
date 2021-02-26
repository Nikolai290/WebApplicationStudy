using System;

namespace WebApplication3.Models.ViewModels {
    public class AddWorkDTO {
        public virtual int MoSId { get; set; }
        public virtual int SortId { get; set; }
        public virtual int TypeWorkId { get; set; }
        public virtual int WorkId { get; set; }
        public virtual int Wagons { get; set; }
        public virtual DateTime StartTime { get; set; }
        public virtual DateTime EndTime { get; set; }
        public virtual string Note { get; set; }
        // string input
        public virtual string volume { get; set; }
        public virtual string weight { get; set; }
        public virtual string ash { get; set; }
        public virtual string heat { get; set; }
        public virtual string wet { get; set; }
        // convert to double
        public virtual double Volume { get; set; }
        public virtual double Weight { get; set; }
        public virtual double Ash { get; set; }
        public virtual double Heat { get; set; }
        public virtual double Wet { get; set; }

        public virtual AddWorkDTO ConvertStringToDouble() {
            Volume = double.TryParse(volume?.Replace('.', ','), out double vol) ? vol : 0;
            Weight = double.TryParse(weight?.Replace('.', ','), out double weight1) ? weight1 : 0;
            Ash = double.TryParse(ash?.Replace('.', ','), out double ash1) ? ash1 : 0;
            Heat = double.TryParse(heat?.Replace('.', ','), out double heat1) ? heat1 : 0;
            Wet = double.TryParse(wet?.Replace('.', ','), out double wet1) ? wet1 : 0;
            return this;
        }

        private bool IsRight(string line) {
            if (String.IsNullOrEmpty(line)) 
                return false;
            else {
                // проверка на соответсвие шаблону для чисел double
            }


            return true;
        }
    }
}
