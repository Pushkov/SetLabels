using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SetLabels.service
{
    class SheetManager
    {
        public void  controlSheetNumber( ref ListViews list, string[] sheets, ModelDoc2 model)
        {
            list.getAllViews().ForEach(x => removePropertyLinkedNote(x.view, model));
            list.getAllViews().ForEach(x => setDifferentSheet(x.view, sheets, model));
        }

        private void setDifferentSheet(IView view, string[] sheets, ModelDoc2 model)
        {
            Note letter = view.GetFirstNote();
            string props = letter.PropertyLinkedText;
            string parentSheet = view.GetBaseView().Sheet.GetName();
            
            if ("".Equals(parentSheet) || view.Sheet.GetName().Equals(parentSheet))
            {
                letter.PropertyLinkedText = clearPropertyLinkedText(props);
                
            }
            else
            {
                letter.PropertyLinkedText = clearPropertyLinkedText(props) + " (" + (sheets.ToList().IndexOf(parentSheet) + 1 ) + ")";
                createPropertyLinkedNote(view, sheets, model);
            }
        }


        private string clearPropertyLinkedText(string text)
        {
            string pattern = @"\s+\(\d+\)$";
            Regex regex = new Regex(pattern);
            string p = regex.Replace(text, "");
            return p;

        }

        private void removePropertyLinkedNote(IView view, ModelDoc2 model)
        {
            SelectionMgr selMgr = model.SelectionManager;
            IView pView = view.GetBaseView();
            if (pView.GetNoteCount() > 0)
            {
                object[] notes = pView.GetNotes();
                foreach (Note n in notes)
                {
                    if (n.GetName().StartsWith("<shname>"))
                    {
                        Annotation ann = n.GetAnnotation();
                        double[] poz = ann.GetPosition();
                        
                        Console.WriteLine("<shname> - " + n.GetName());
                        bool s = model.Extension.SelectByID2("", "NOTE", poz[0], poz[1], poz[2], false, 0, null, 0);

                        model.DeleteSelection(true);
                        break;
                    }
                }
            }
        }

        private void createPropertyLinkedNote(IView view, string[] sheets, ModelDoc2 model)
        {
            int viewType = view.Type;
                DrawingDoc drw = (DrawingDoc)model;

                double[] coords = { 0, 0, 0 };

                switch ((swDrawingViewTypes_e)viewType)
                {
                    case (swDrawingViewTypes_e.swDrawingAuxiliaryView):
                    case (swDrawingViewTypes_e.swDrawingProjectedView):
                        IProjectionArrow arr = view.GetProjectionArrow();
                        coords = arr.GetCoordinates();
                        break;
                    case (swDrawingViewTypes_e.swDrawingSectionView):
                        DrSection section = view.GetSection();
                        coords = section.GetArrowInfo();
                        break;
                    case (swDrawingViewTypes_e.swDrawingDetailView):
                        DetailCircle circle = view.GetDetail();
                        coords = circle.GetArrowInfo();
                        break;
                }


            string currName = view.GetName2();
            Console.WriteLine("view ****> " + currName);
            
            string sheet = view.GetBaseView().Sheet.GetName();
            Console.WriteLine("pview ****> " + view.GetBaseView().GetName2());
            drw.ActivateSheet(sheet);
            string viewName = view.GetBaseView().GetName2();
            drw.ActivateView(viewName);

            string sheetIndex = "(" + (sheets.ToList().IndexOf(view.Sheet.GetName()) + 1) + ")";

            Note note = drw.CreateText2(sheetIndex, coords[0], coords[1], coords[2], 0.007, 0);
            note.SetName("<shname>" + view.GetUniqueName());
        }



    }
}
