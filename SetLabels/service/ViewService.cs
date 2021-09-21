using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

using static SetLabels.utils.ViewUtils;

namespace SetLabels
{
    class ViewService
    {
        bool res;
        GtolService gtolService = new GtolService();
        SurfaceService surfaceService = new SurfaceService();


        public void readViews(ModelDoc2 model, ref ListViews list)
        {
            DrawingDoc swDrawing = (DrawingDoc)model;
            string[] arrSheetName = (string[])swDrawing.GetSheetNames();
            foreach(string sheetName in arrSheetName)
            {
                res = swDrawing.ActivateSheet(sheetName);
                IView view = (IView)swDrawing.GetFirstView();
                while(view != null)
                {
                    swDrawingViewTypes_e viewType = (swDrawingViewTypes_e) view.Type;
                    if (viewType == swDrawingViewTypes_e.swDrawingSheet)
                    {
                        view = view.IGetNextView();
                        continue;
                    }
                    ViewExtended extView;
                    
                    switch (viewType)
                    {
                        case swDrawingViewTypes_e.swDrawingProjectedView: // здесь уже возможна метка, так что проверяем
                            {
                                if (isArrow(view))
                                {
                                    extView = createExtView(view, list.getProjected().Count);
                                    list.addProjectionView(extView);
                                    list.drawingNames.Add(extView.drawingName);
                                }
                            }
                            break;
                        case swDrawingViewTypes_e.swDrawingAuxiliaryView:
                            {
                                if (isArrow(view))
                                {
                                    extView = createExtView(view, list.getAuxiliaty().Count);
                                    list.addAuxiliaryView(extView);
                                    list.drawingNames.Add(extView.drawingName);
                                }
                            }
                            break;
                        case swDrawingViewTypes_e.swDrawingSectionView:
                            {
                                extView = createExtView(view, list.getSection().Count);
                                list.addSectionView(extView);
                                list.drawingNames.Add(extView.drawingName);
                            }
                            break;
                        case swDrawingViewTypes_e.swDrawingDetailView:
                            {
                                extView = createExtView(view, list.getDetail().Count);
                                list.addDetailView(extView);
                                list.drawingNames.Add(extView.drawingName);
                            }
                            break;
                    }
                    gtolService.readDatumTags(view, ref list);
                    gtolService.readGtols(view, ref list);
                    surfaceService.readSurfaces(view, ref list);
                    view = view.IGetNextView();
                }
            }
        }

        private ViewExtended createExtView(IView view, int viewId)
        {
            ViewExtended eView = new ViewExtended(view, viewId);
            if (isLinkedUtils(view))
            {
                eView.linkedName = getLinkedName(view);
            }
            eView.oldLabel = getViewLabel(view);
            eView.inSheet = inSheet(view);
            return eView;
        }

        public void setLinkedAnnotation(string viewName, string linkedView, ModelDoc2 model)
        {
            SelectionMgr swSelmgr = (SelectionMgr)model.SelectionManager;
            bool s = model.Extension.SelectByID2(viewName, "DRAWINGVIEW", 0, 0, 0, false, 0, null, 0);
            IView view = (IView)swSelmgr.GetSelectedObject6(1, 0);
            setLinkedAnnotationInView( ref view, linkedView);
        }
    }
}
