using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetLabels.model
{
    class LinkedUnit
    {
        public string uniqueName { get; private set; }
        public string viewName { get; private set; }
        public string label;

        public LinkedUnit( string uniqueName, string viewName)
        {
            this.uniqueName = uniqueName;
            this.viewName = viewName;
        }

        public LinkedUnit(string uniqueName, string viewName, string label)
        {
            this.uniqueName = uniqueName;
            this.viewName = viewName;
            this.label = label;
        }
    }
}
