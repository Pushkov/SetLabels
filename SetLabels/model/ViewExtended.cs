using SetLabels.utils;
using SolidWorks.Interop.sldworks;

namespace SetLabels
{
    class ViewExtended
    {
        public IView view;
        public int viewId;
        public string drawingName;
        public bool inSheet;
        public string linkedName;
        public string newLabel;
        public string oldLabel;

        public ViewExtended(IView _view, int _viewId)
        {
            this.view = _view;
            viewId = _viewId;
            inSheet = true;
            linkedName = "";
            drawingName = ViewUtils.getDrawingName(_view, _viewId);
        }
    }
}
