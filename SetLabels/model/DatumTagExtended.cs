using SetLabels.utils;
using SolidWorks.Interop.sldworks;
using System.Collections.Generic;

namespace SetLabels
{
    class DatumTagExtended
    {
        public DatumTag tag;
        public int tagIndex;
        public string drawingName;
        public IView view;
        public string newLabel;
        public string oldLabel;
        public List<Gtol> gtols = new List<Gtol>();

        public DatumTagExtended(DatumTag _tag, IView _view, int viewId)
        {
            tag = _tag;
            view = _view;
            drawingName = ViewUtils.getDrawingName(view, viewId);
            oldLabel = _tag.GetLabel();
        }

        public void clearGtols()
        {
            gtols.Clear();
        }

        public void addGtol(Gtol gtol)
        {
            gtols.Add(gtol);
        }
    }
}
