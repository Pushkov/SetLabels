﻿using SolidWorks.Interop.sldworks;
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
        public void controlSheetNumber(ref ListViews list, string[] sheets, ModelDoc2 model)
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
                letter.PropertyLinkedText = clearPropertyLinkedText(props) + " (" + (sheets.ToList().IndexOf(parentSheet) + 1) + ")";
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
            DrawingDoc drw = (DrawingDoc)model;
            IView pView = view.GetBaseView();
            if (pView.GetNoteCount() > 0)
            {
                object[] notes = pView.GetNotes();
                foreach (Note n in notes)
                {
                    if (n.GetName().StartsWith("<shname>"))
                    {
                        Annotation ann = n.GetAnnotation();
                        ann.Select3(false, null);
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
                    coords[0] = coords[21] + 0.005;
                    coords[1] = coords[22] + 0.0045;
                    break;
                case (swDrawingViewTypes_e.swDrawingSectionView):
                    DrSection section = view.GetSection();
                    //coords = section.GetArrowInfo();
                    coords = section.GetTextInfo();
                    double c0 = 0;
                    double c1 = 0;
                    if (coords[1] > coords[4])
                    {
                        c0 = coords[0];
                        c1 = coords[1];
                    }
                    else
                    {
                        if(coords[0] > coords[3])
                        {
                            c0 = coords[0];
                            c1 = coords[1];
                        }
                        else
                        {
                            c0 = coords[3];
                            c1 = coords[4];
                        }

                    }

                    coords[0] = c0 + 0.005;
                    coords[1] = c1;
                    //section.GetL

                    break;
                case (swDrawingViewTypes_e.swDrawingDetailView):
                    DetailCircle circle = view.GetDetail();
                    circle.GetLabelPosition(out coords[0], out coords[1]);
                    string lab = circle.GetLabel().Trim();
                    coords[0] = coords[0] + 0.0035;
                    coords[1] = coords[1] + 0.0045;
                    break;
            }
            string currName = view.GetName2();
            string sheet = view.GetBaseView().Sheet.GetName();
            drw.ActivateSheet(sheet);
            string viewName = view.GetBaseView().GetName2();
            drw.ActivateView(viewName);
            string sheetIndex = "(" + (sheets.ToList().IndexOf(view.Sheet.GetName()) + 1) + ")";
            Note note = drw.CreateText2(sheetIndex, coords[0], coords[1], coords[2], SetLabels.SHEET_LABEL_FONT_SIZE, 0);
            note.SetName("<shname>" + view.GetUniqueName());
        }
    }
}
