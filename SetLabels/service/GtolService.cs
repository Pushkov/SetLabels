using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SetLabels
{
    class GtolService
    {
        public void readDatumTags(IView view, ref ListViews list)
        {
            object[] dtags = view.GetDatumTags();
            if (dtags != null)
            {
                foreach (DatumTag tag in dtags)
                {
                    DatumTagExtended extTag = new DatumTagExtended(tag, view, list.getDatumTags().Count );
                    extTag.tagIndex = list.getDatumTags().Count;
                    list.addDatumTad(extTag);
                }
            }

        }

        public void readGtols(IView view, ref ListViews list)
        {
            object[] dtols = view.GetGTols();
            if(dtols != null) {
                foreach (Gtol swGtol in dtols)
                {
                    GtolExtended extGtol = new GtolExtended(swGtol);
                    extGtol.gtolIndex = list.getGtols().Count;
                    list.addGtol(extGtol);
                }
            }
        }

        public void relinkGtolByTatumtag(ref ListViews list)
        {
            List<DatumTagExtended> exDatums = list.getDatumTags();
            List<GtolExtended> dtols = list.getGtols();
            foreach (GtolExtended extGtol in dtols)
            {
                Gtol gtol = extGtol.getGtol();
                List<List<string>> newBases = new List<List<string>>();
                for (int idx = 1; idx <= gtol.GetFrameCount(); idx++)
                {
                    Object myParams;
                    myParams = gtol.GetFrameValues((short)idx);
                    Array myParamArray = (Array)myParams;
                    string letters = "";
                    if (extGtol.oldBases[idx - 1] != null)
                    {
                        letters = extGtol.oldBases[idx - 1];
                    }

                    if (!"".Equals(letters))
                    {
                        string newLabel = "";
                        List<string> gtols = new List<string>();
                        Regex regex = new Regex(@"[А-Я,A-Z]\d?");
                        MatchCollection matches = regex.Matches(letters);
                        if (matches.Count > 0)
                        {
                            foreach (Match match in matches)
                            {
                                gtols.Add(match.Value);
                            }
                        }
                        for( int i = 0; i < gtols.Count; i++)
                        {
                            foreach (DatumTagExtended tag in exDatums)
                            {
                                if (gtols[i].Equals(tag.oldLabel))
                                {
                                    gtols[i] = tag.newLabel;
                                    break;
                                }
                            }
                        }
                        gtols.Sort();
                        gtols.ForEach(x => newLabel += x);
                        gtol.SetFrameValues2(
                            (short)idx,
                            myParamArray.GetValue(0).ToString(),
                            myParamArray.GetValue(1).ToString(),
                            newLabel,
                            myParamArray.GetValue(3).ToString(),
                            myParamArray.GetValue(4).ToString());
                    }
                }
            }
        }
    }
}
