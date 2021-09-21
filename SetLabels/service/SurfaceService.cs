using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SetLabels
{
    class SurfaceService
    {
        public void readSurfaces(IView view, ref ListViews list)
        {
            if (view.GetAnnotationCount() > 0)
            {
                List<SurfaceExtended> linkedSurf = new List<SurfaceExtended>();
                object[] anns = view.GetNotes();
                if (anns != null)
                {
                    foreach (Note a in anns)
                    {
                        if (a.GetName().StartsWith("<surf>"))
                        {
                            SurfaceExtended extSurf = new SurfaceExtended(a, view, list.getSurfaces().Count);
                            extSurf.isLinked = isLinkSurface(a);
                            list.addSurface(extSurf);
                            if (extSurf.isLinked)
                            {
                                linkedSurf.Add(extSurf);
                            }
                        }
                    }
                }
                if (linkedSurf.Count > 0)
                {
                    foreach (SurfaceExtended surf in linkedSurf)
                    {
                        foreach (SurfaceExtended s1 in list.getSurfaces())
                        {
                            if (!s1.isLinked && surf.oldLabel.Equals(s1.oldLabel))
                            {
                                surf.linkedId = list.getSurfaces().IndexOf(s1);
                            }
                        }
                        if(surf.linkedId < 0)
                        {
                            surf.isLinked = false;
                        }

                    }
                }
            }
        }

        private bool isLinkSurface( Note note)
        {
            return note.GetName().Contains("<link>");
        }


        public void setSurface(Note note)
        {
            string text = note.GetText();
            note.SetName("<surf>" + text);

        }

        public void removeSurface(Note note)
        {
            string text = note.GetName();
            note.SetName( text.Replace("<surf>", "").Replace("<link>", ""));
        }

    }
}
