using SolidWorks.Interop.sldworks;
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
        public void  controlSheetNumber( ref ListViews list, string[] sheets)
        {
            foreach(ViewExtended view in list.getAllViews())
            {
                //if (isDifferentSheets(view.view))
                //{
                //    setDifferentSheet(view.view, sheets);
                //    Console.WriteLine("diff sheet: " + view.drawingName);
                //}
                //else
                //{
                //    Console.WriteLine("same sheet: " + view.drawingName);
                //}

                setDifferentSheet(view.view, sheets);
            }

        }

        private bool isDifferentSheets(IView _view)
        {
            string parent = _view.GetBaseView().Sheet.GetName();
            string current = _view.Sheet.GetName();
            return !current.Equals(parent);
        }

        private void setDifferentSheet(IView view, string[] sheets)
        {
            Note letter = view.GetFirstNote();
            string props = letter.PropertyLinkedText;
            string parentSheet = view.GetBaseView().Sheet.GetName();
            Console.WriteLine("***> " + view.Sheet.GetName() + "  ***> " + parentSheet);
            if ("".Equals(parentSheet) || view.Sheet.GetName().Equals(parentSheet))
            {
                letter.PropertyLinkedText = clearPropertyLinkedText(props);
            }
            else
            {
                letter.PropertyLinkedText = clearPropertyLinkedText(props) + " (" + (sheets.ToList().IndexOf(parentSheet) + 1 ) + ")";
            }
        }


        private string clearPropertyLinkedText(string text)
        {
            string pattern = @"\s+\(\d+\)$";
            Console.WriteLine("> " + text);
            Regex regex = new Regex(pattern);
            string p = regex.Replace(text, "");
            Console.WriteLine("> " + p);
            return p;

        }
    }
}
