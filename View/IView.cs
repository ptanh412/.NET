using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Project.View
{
    internal interface IView
    {
        public void SetDataToText(Object item);
        object GetDataFromText();
    }
}
