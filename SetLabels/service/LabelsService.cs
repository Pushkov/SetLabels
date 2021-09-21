using SetLabels.utils;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SetLabels
{
    class LabelsService
    {
        string exlude;
        public LabelsService(string _exlude)
        {
            exlude = _exlude;
        }

        public void generateLabels(ref ListViews list, bool isOut)
        {
            string label = "А";

            List<ViewExtended> linkedList = new List<ViewExtended>();
            List<SurfaceExtended> linkedSurfList = new List<SurfaceExtended>();

            foreach (ViewExtended view in list.getProjected())
            {
                view.newLabel = label;

                if (isOut && !view.inSheet)
                {
                    view.newLabel = "";
                }
                if (!"".Equals(view.linkedName))
                {
                    view.newLabel = "";
                    linkedList.Add(view);
                }
                if ("".Equals(view.linkedName) && ((isOut && view.inSheet) || ("".Equals(view.linkedName) && !isOut)))
                {
                    label = GetNextLabel(label);
                }
            }
            foreach (ViewExtended view in list.getAuxiliaty())
            {
                view.newLabel = label;
                //linked[view.view.Name] = label;
                if (isOut && !view.inSheet)
                {
                    view.newLabel = "";
                }
                if (!"".Equals(view.linkedName))
                {
                    view.newLabel = "";
                    linkedList.Add(view);
                }
                if ("".Equals(view.linkedName) && ((isOut && view.inSheet) || ("".Equals(view.linkedName) && !isOut)))
                {
                    label = GetNextLabel(label);
                }

            }
            foreach (ViewExtended view in list.getSection())
            {
                view.newLabel = label;
                if (isOut && !view.inSheet)
                {
                    view.newLabel = "";
                }
                if (!"".Equals(view.linkedName))
                {
                    view.newLabel = "";
                    linkedList.Add(view);
                }
                if ("".Equals(view.linkedName) && ((isOut && view.inSheet) || ("".Equals(view.linkedName) && !isOut)))
                {
                    label = GetNextLabel(label);
                }
            }
            foreach (ViewExtended view in list.getDetail())
            {
                view.newLabel = label;
                if (isOut && !view.inSheet)
                {
                    view.newLabel = "";
                }
                if (!"".Equals(view.linkedName))
                {
                    view.newLabel = "";
                    linkedList.Add(view);
                }
                if ("".Equals(view.linkedName) && ((isOut && view.inSheet) || ("".Equals(view.linkedName) && !isOut)))
                {
                    label = GetNextLabel(label);
                }
            }
            foreach (DatumTagExtended datumTag in list.getDatumTags())
            {
                datumTag.newLabel = label;
                label = GetNextLabel(label);
            }
            foreach (SurfaceExtended surface in list.getSurfaces())
            {
                surface.newLabel = label;
                if(surface.isLinked)
                {
                    surface.newLabel = "";
                    linkedSurfList.Add(surface);
                }
                else
                {
                    label = GetNextLabel(label);
                }
            }

            if (linkedList.Count > 0)
            {
                for (int i=0; i < linkedList.Count; i++)
                {
                    for ( int j=0; j < list.getAllViews().Count; j++)
                    {
                        ViewExtended linkView = linkedList[i];
                        ViewExtended compareView = list.getAllViews()[j];

                        if (compareLinkedNames( linkView, compareView) || compareView.drawingName.Equals(linkView.linkedName))
                        {
                            linkedList[i].newLabel = compareView.newLabel;
                        }
                    }
                }
            }
            if(linkedSurfList.Count > 0)
            {
                foreach(SurfaceExtended surf in linkedSurfList)
                {
                    surf.newLabel = list.getSurfaces()[surf.linkedId].newLabel;
                }
            }
        }

        private bool compareLinkedNames(ViewExtended linked, ViewExtended compared)
        {
            IView linkedView = linked.view;
            string link = linked.linkedName;

            bool result = false;
            string word = "";
            switch ((swDrawingViewTypes_e)linkedView.Type)
            {

                case swDrawingViewTypes_e.swDrawingAuxiliaryView:
                case swDrawingViewTypes_e.swDrawingProjectedView:
                    result = linkedView.GetUniqueName().Equals(compared.drawingName);
                    break;
                case swDrawingViewTypes_e.swDrawingSectionView:
                    word =ViewUtils.getSecondWord(compared.view);
                    result = link.Contains("Разрез") && link.Contains(word);
                    break;
                case swDrawingViewTypes_e.swDrawingDetailView:
                    word = ViewUtils.getSecondWord(compared.view);
                    result = link.Contains("Местный вид") && link.Contains(word) && link.Contains("№" + compared.viewId);
                    break;
                default:
                    result = false;
                    break;
            }

            //Console.WriteLine("link: " + link + " ; compared: " + compared.drawingName + " ; word: " + word + " ; result: " + result);
            return result;
        }



        public void setLabelsToViews(ref ListViews list)
        {
            foreach (ViewExtended exV in list.getProjected())
            {
                IProjectionArrow arr = exV.view.GetProjectionArrow();
                arr.SetLabel(exV.newLabel);
                ViewUtils.setLinkedAnnotationInView(ref exV.view, exV.linkedName);
                if (!exV.newLabel.Equals(arr.GetLabel()))
                {
                    list.addErrorView(exV);
                }
            }
            foreach (ViewExtended exV in list.getAuxiliaty())
            {
                IProjectionArrow arr = exV.view.GetProjectionArrow();
                arr.SetLabel(exV.newLabel);
                ViewUtils.setLinkedAnnotationInView(ref exV.view, exV.linkedName);
                if (!exV.newLabel.Equals(arr.GetLabel()))
                {
                    list.addErrorView(exV);
                }
            }
            foreach (ViewExtended exV in list.getSection())
            {
                DrSection section = exV.view.GetSection();
                section.SetLabel(exV.newLabel);
                ViewUtils.setLinkedAnnotationInView(ref exV.view, exV.linkedName);
                if (!exV.newLabel.Equals(section.GetLabel()))
                {
                    list.addErrorView(exV);
                }
            }
            foreach (ViewExtended exV in list.getDetail())
            {
                DetailCircle circle = exV.view.GetDetail();
                circle.SetLabel(exV.newLabel);
                ViewUtils.setLinkedAnnotationInView(ref exV.view, exV.linkedName);
                if (!exV.newLabel.Equals(circle.GetLabel()))
                {
                    list.addErrorView(exV);
                }
            }
            foreach (DatumTagExtended datumTag in list.getDatumTags())
            {
                DatumTag tag = datumTag.tag;
                tag.SetLabel(datumTag.newLabel);

                if (!datumTag.newLabel.Equals(tag.GetLabel()))
                {
                    list.addErrorDatumTad(datumTag);
                }
            }
            foreach (SurfaceExtended surface in list.getSurfaces())
            {
                Note note = surface.surface;
                string name = "<surf>";
                if (surface.isLinked)
                {
                    name += "<link>";
                }
                name += surface.newLabel;
                note.SetName(name);
                note.SetText(surface.newLabel);
                if (!surface.newLabel.Equals(note.GetText()))
                {
                    list.addErroeSurface(surface);
                }
            }

        }

        public string GetNextLabel(string _label)
        {
            int round = 0;
            char l = _label[0];
            if (_label.Length > 0)
            {
                try
                {
                    string stringRound = _label.Substring(1);
                    //stringRound = stringRound.Replace(@"<font size=7>", "").Replace(@"<font size=5>", "");
                    round = int.Parse(stringRound);
                }
                catch { }
            }

            if (l.Equals('Я'))
            {
                l = 'А';
                round++;
            }
            else
                l++;
            while (exlude.Contains(l))
            {
                l++;
            }

            string res = l.ToString(); ;
            if (round > 0)
                res = res + round.ToString();
            //res = res + @"<font size=5>" + round.ToString() + @"<font size=7>"; // Херово работает (разрез вообще не понимает, местный вид - удлиняет полку выноски

            return res;
        }
    }
}
