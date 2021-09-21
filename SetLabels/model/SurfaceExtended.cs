using SetLabels.utils;
using SolidWorks.Interop.sldworks;

namespace SetLabels
{
    class SurfaceExtended
    {
        public Note surface;
        public int surfaceIndex;
        public string drawingName;
        public int linkedId = -1;
        public bool isLinked = false;
        public IView view;
        public string newLabel;
        public string oldLabel;

        public SurfaceExtended(Note _surf, IView _view, int viewId)
        {
            surface = _surf;
            view = _view;
            surfaceIndex = viewId;
            drawingName = ViewUtils.getDrawingName(view, viewId);
            oldLabel = _surf.GetText();
        }
    }
}
