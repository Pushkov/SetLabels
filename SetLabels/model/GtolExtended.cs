using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;

namespace SetLabels
{
    class GtolExtended
    {
        private Gtol gtol;
        public int gtolIndex;    
        public List<string> newBases = new List<string>();
        public List<string> oldBases = new List<string>();
        public GtolExtended(Gtol _gtol)
        {
            gtol = _gtol;
            
            oldBases = getLabels(_gtol);
        }

        private List<string> getLabels(Gtol _gtol)
        {
            List<string> result = new List<string>();
            for (int idx = 1; idx <= _gtol.GetFrameCount(); idx++)
            {
                Object values;
                values = gtol.GetFrameValues((short)idx);
                Array valuesArray = (Array)values;
                result.Add(valuesArray.GetValue(2).ToString());
            }
            return result;
        }

        public Gtol getGtol()
        {
            return gtol;
        }
    }
}
