using System.Collections.Generic;

namespace SetLabels
{
    class ListViews
    {
        public List<string> drawingNames = new List<string>();

        private List<ViewExtended> listProjectionView = new List<ViewExtended>();
        private List<ViewExtended> listSectionView = new List<ViewExtended>();
        private List<ViewExtended> listDetailView = new List<ViewExtended>();
        private List<ViewExtended> listAuxiliaryView = new List<ViewExtended>();
        private List<DatumTagExtended> listDatumTag = new List<DatumTagExtended>();
        private List<GtolExtended> listGtols = new List<GtolExtended>();
        private List<SurfaceExtended> listSurfaces = new List<SurfaceExtended>();

        private List<ViewExtended> listErrorView = new List<ViewExtended>();
        private List<DatumTagExtended> listErrorDatumTag = new List<DatumTagExtended>();
        private List<SurfaceExtended> listErrorSurfaces = new List<SurfaceExtended>();

        public List<ViewExtended> getProjected()
        {
            return this.listProjectionView;
        }
        public List<ViewExtended> getSection()
        {
            return this.listSectionView;
        }
        public List<ViewExtended> getAuxiliaty()
        {
            return this.listAuxiliaryView;
        }
        public List<ViewExtended> getDetail()
        {
            return this.listDetailView;
        }

        public List<DatumTagExtended> getDatumTags()
        {
            return this.listDatumTag;
        }
        public List<GtolExtended> getGtols()
        {
            return this.listGtols;
        }
        public List<SurfaceExtended> getSurfaces()
        {
            return this.listSurfaces;
        }
        public List<ViewExtended> getErrorLists()
        {
            return this.listErrorView;
        }
        public List<DatumTagExtended> getErrorDatumTag()
        {
            return this.listErrorDatumTag;
        }
        public List<SurfaceExtended> getErrorSurfaces()
        {
            return this.listErrorSurfaces;
        }



        public void clear()
        {
            listProjectionView.Clear();
            listSectionView.Clear();
            listDetailView.Clear();
            listAuxiliaryView.Clear();
            listDatumTag.Clear();
            listGtols.Clear();
            listSurfaces.Clear();
            listErrorView.Clear();
            listErrorDatumTag.Clear();
            listErrorSurfaces.Clear();
        }

        public void addProjectionView(ViewExtended view)
        {
            listProjectionView.Add(view);
        }

        public void addSectionView(ViewExtended view)
        {
            listSectionView.Add(view);
        }

        public void addDetailView(ViewExtended view)
        {
            listDetailView.Add(view);
        }

        public void addAuxiliaryView(ViewExtended view)
        {
            listAuxiliaryView.Add(view);
        }

        public void addDatumTad(DatumTagExtended tag)
        {
            listDatumTag.Add(tag);
        }

        public void addGtol(GtolExtended gtol)
        {
            listGtols.Add(gtol);
        }

        public void addSurface(SurfaceExtended surface)
        {
            listSurfaces.Add(surface);
        }
        public void addErrorView(ViewExtended view)
        {
            listErrorView.Add(view);
        }

        public void addErrorDatumTad(DatumTagExtended tag)
        {
            listErrorDatumTag.Add(tag);
        }
        public void addErroeSurface(SurfaceExtended surface)
        {
            listErrorSurfaces.Add(surface);
        }

        public List<ViewExtended> getAllViews()
        {
            List<ViewExtended> result = new List<ViewExtended>();
            result.AddRange(listProjectionView);
            result.AddRange(listAuxiliaryView);
            result.AddRange(listSectionView);
            result.AddRange(listDetailView);
            return result;
        }

    }
}
