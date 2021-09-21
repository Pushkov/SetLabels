using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Text.RegularExpressions;

namespace SetLabels.utils
{
    class ViewUtils
    {

        public static string getDrawingName(IView view, int viewId)
        {
            string name = view.GetName2();   
            int viewType = view.Type;
            switch ((swDrawingViewTypes_e)viewType)
            {
                case (swDrawingViewTypes_e.swDrawingSectionView):
                    name = view.GetName2() + " (" + view.GetUniqueName().Split(' ')[1] + ")";

                    break;
                case (swDrawingViewTypes_e.swDrawingDetailView):
                    IView parent = view.GetBaseView();
                    name = view.GetName2() + " ( №" + viewId  + " от " + getSecondWord(parent) + ")";
                    break;
            }
            return name;
        }

        public static string getSecondWord(IView view)
        {
            string second = view.GetName2();
            int viewType = view.Type;
            switch ((swDrawingViewTypes_e)viewType)
            {

                case (swDrawingViewTypes_e.swDrawingSectionView):
                    second = view.GetUniqueName().Split(' ')[1];
                    break;
                case (swDrawingViewTypes_e.swDrawingDetailView):
                    second = view.GetUniqueName().Split(' ')[1] + " " + view.GetUniqueName().Split(' ')[2];
                    break;
                default:
                    second = view.GetUniqueName().Split(' ')[1];
                    break;
            }

            return second;
        }


        public static bool isArrow(IView swView)
        {
            IProjectionArrow swArrow = swView.IGetProjectionArrow();
            return (swArrow != null && swArrow.Visible);
        }

        public static string getViewLabel(IView view)
        {
            int viewType = view.Type;
            string label = "";
            switch ((swDrawingViewTypes_e)viewType)
            {
                case (swDrawingViewTypes_e.swDrawingAuxiliaryView):
                case (swDrawingViewTypes_e.swDrawingProjectedView):
                    IProjectionArrow arr = view.GetProjectionArrow();
                    label = arr.GetLabel();
                    break;
                case (swDrawingViewTypes_e.swDrawingSectionView):
                    DrSection section = view.GetSection();
                    label = section.GetLabel();
                    break;
                case (swDrawingViewTypes_e.swDrawingDetailView):
                    DetailCircle circle = view.GetDetail();
                    label = circle.GetLabel();
                    break;
            }
            return label;
        }

        public static bool isLinkedUtils(IView view)
        {
            if (view.GetAnnotationCount() > 0)
            {
                return view.GetFirstAnnotation3().GetName().StartsWith("<link>");
            }
            return false;
        }

        public static string getLinkedName(IView view)
        {
            string result = "";
            if (view.GetAnnotationCount() > 0)
            {
                string link = view.GetFirstAnnotation3().GetName();
                if (link.StartsWith("<link>"))
                {
                    link = link.Replace("<link>", "");
                    result = Regex.Split(link, "</link>")[0];
                }
            }
            return result;
        }

        public static void setLinkedAnnotationInView( ref IView view, string linkedView)
        {
            Annotation ann = view.GetFirstAnnotation3();
            string ann_name = ann.GetName();
            if (!isLinkedUtils(view))
            {
                if (!"".Equals(linkedView))
                {
                    ann_name = "<link>" + linkedView + "</link>" + ann_name;
                    ann.SetName(ann_name);
                    Console.WriteLine("if  linkView -" + linkedView + ";  ann_name - " + ann_name);
                }
            }
            else
            {
                ann_name = Regex.Split(ann_name, "</link>")[1];
                Console.WriteLine("else  linkView -" + linkedView + ";  ann_name[1] - " + ann_name);

                if (!"".Equals(linkedView))
                {
                    ann_name = "<link>" + linkedView + "</link>" + ann_name;
                }
                ann.SetName(ann_name);
            }
        }

        public static bool inSheet(IView view)
        {
            Sheet sheet = view.Sheet;
            double width = 0;
            double height = 0;
            double[] poz = (double[])view.Position;
            swDwgPaperSizes_e paperSize = (swDwgPaperSizes_e)sheet.GetSize(ref width, ref height);
            return (poz[0] > 0 && poz[0] < width) && (poz[1] > 0 && poz[1] < height);
        }
    }
}
