using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class clsQuestionSlider : clsQuestion
    {
        private int startValue;
        private int endValue;
        private string startCaption;
        private string endCaption;

        public int StartValue { get { return startValue; } set { startValue = value; } }
        public int EndValue { get { return endValue; } set {  endValue = value; } } 
        public string StartCaption { get {  return startCaption; } set { startCaption = value; } }
        public string EndCaption { get { return endCaption; } set { endCaption = value; } }
    }
}
