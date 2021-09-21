﻿using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SetLabels
{
    public partial class SetLabelsForm : Form
    {
        SldWorks swApp;
        string swPath;
        ModelDoc2 swModel;

        bool isExludeChange = false;

        SetLabels setLabels;

        private static SetLabelsForm instance;

        private SetLabelsForm(SldWorks _app, string _path)
        {
            InitializeComponent();
            TopMost = true;
            swApp = _app;
            swPath = _path;
        }

        public static SetLabelsForm getInstance(SldWorks _app, string _path)
        {
            if (instance == null)
                instance = new SetLabelsForm(_app, _path);
            return instance;
        }

        private void SetLabelsForm_Load(object sender, EventArgs e)
        {
            setLabels = new SetLabels(swApp, swPath, this);
        }

        private void SetLabelsForm_Shown(object sender, EventArgs e)
        {
            setLabels.readViews();
        }

        public void panel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FlowLayoutPanel panel = (FlowLayoutPanel)sender;
            string name = panel.Name;
            setLabels.zoomTo(name);
        }

        public void textBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TextBox box = (TextBox)sender;
            string name = box.Parent.Name;
            setLabels.zoomTo(name);
        }

        public void comboBox__SelectedIndexChanged(object sender, EventArgs e) {
            ComboBox box = (ComboBox)sender;
            string name = box.Parent.Name;
            if (box.Focused )
            {
                if (!name.Equals(box.SelectedItem.ToString()))
                {
                    setLabels.setLinked(name, box.SelectedIndex);
                }
                if (name.Equals(box.SelectedItem.ToString()))
                {
                    box.SelectedIndex = 0;
                }
            }   
        }

        public void comboBoxSurface__SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox box = (ComboBox)sender;
            string name = box.Parent.Name;
            if (box.Focused)
            {

                if (box.SelectedIndex == 0 || box.Name.EndsWith(box.SelectedItem.ToString()))
                {
                    setLabels.setLinkedSurface(box.Name, -1);
                    box.SelectedIndex = 0;
                }
                else
                {
                    setLabels.setLinkedSurface(box.Name, box.SelectedIndex);
                }

            }
        }

        private void showInfo(string message)
        {
            lbInfo.Text = message;
        }

        #region BUTTONS
        private void btnReRead_Click(object sender, EventArgs e)
        {
                btnRename.Enabled = true;
                setLabels.readViews();
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
                setLabels.setLabels();
        }

        private void btnChangeExclude_Click(object sender, EventArgs e)
        {
            isExludeChange = !isExludeChange;
            if (isExludeChange)
            {
                btnExcludeChange.Text = "Сохранить";
                txtExlude.ReadOnly = false;
            }
            else
            {
                btnExcludeChange.Text = "Изменить";
                setLabels.setExludeLetters(txtExlude.Text);
            }
        }

        private void checkOut_CheckedChanged(object sender, EventArgs e)
        {
            if (checkOut.Focused)
            {
                setLabels.hasOut(checkOut.Checked);
            }
        }

        private void checkColor_CheckedChanged(object sender, EventArgs e)
        {
            if (checkColor.Focused)
            {
                setLabels.hasColor(checkColor.Checked);
            }
        }

        private void btnAddSurf_Click(object sender, EventArgs e)
        {
            setLabels.setSurfaceNote();
        }

        private void btnDelSurf_Click(object sender, EventArgs e)
        {
            setLabels.removeSurfaceNote();
        }
        #endregion

        #region INTERFACE
        public Panel getPanel()
        {
            return viewsPanel;
        }

        public void saveExludeLetters(string _letters)
        {
            txtExlude.Text = _letters;
            txtExlude.ReadOnly = true;
        }

        public void hasColor(bool value)
        {
            checkColor.Checked = value;
        }

        public void hasOut(bool value)
        {
            checkOut.Checked = value;
        }

        public void setButtonRename(bool value)
        {
            btnRename.Enabled = false;
        }

        public void clearPanels()
        {
            viewsPanel.Controls.Clear();
        }

        public void addTitle(Panel panel)
        {
            titlePanel.Controls.Add(panel);
        }

        public void addPanel(Panel panel)
        {
            viewsPanel.Controls.Add(panel);
        }

        public void info(string message)
        {
            lbInfo.Text = message;
        }

        public void exit()
        {
            Application.Exit();
        }
        #endregion


        // ТЕСТОВЫЕ - В РЕЛИЗЕ УДАЛИТЬ !!!!
        private void button1_Click(object sender, EventArgs e)
        {
            swModel = swApp.IActiveDoc2;
            SelectionMgr swSelmgr = (SelectionMgr)swModel.SelectionManager;
            //bool s = swModel.Extension.SelectByID2("Чертежный вид38", "DRAWINGVIEW", 0, 0, 0, false, 0, null, 0);
            IView view = (IView)swSelmgr.GetSelectedObject6(1, 0);
            if (view != null)
            {
                DrawingDoc drw = (DrawingDoc)swModel;
                drw.ActivateView(view.Name);
                if(view.Type == (int)swDrawingViewTypes_e.swDrawingProjectedView)
                {
                    Annotation ann =  view.GetFirstAnnotation3();
                    double[] arrPoz = ann.GetPosition();

                    //ProjectionArrow arr = view.GetProjectionArrow();
                    //double[] arrPoz = arr.GetCoordinates();


                    info("X: " + arrPoz[0] + " ; Y: " + arrPoz[1] + " ; Z: " + arrPoz[2]);
                    Note nn = drw.CreateText2("O", arrPoz[0], arrPoz[1], arrPoz[2],0.007,0);

                }
                else
                {
                    info("Это не проекционный вид");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            swModel = swApp.IActiveDoc2;
            SelectionMgr swSelmgr = (SelectionMgr)swModel.SelectionManager;
            //IView view = (IView)swSelmgr.GetSelectedObject6(1, 0);
            Note view = (Note)swSelmgr.GetSelectedObject6(1, 0);
            if (view != null)
            {
                info(view.GetText());
            }
            else
            {
                showInfo(" ничего не выбрано");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            swModel = swApp.IActiveDoc2;
            SelectionMgr swSelmgr = (SelectionMgr)swModel.SelectionManager;
            IView view = (IView)swSelmgr.GetSelectedObject6(1, 0);
            if (view != null)
            {
                Annotation ann = view.GetFirstAnnotation3();
                string ann_name = ann.GetName();
                ann_name = Regex.Split(ann_name, "</link>")[1];
                ann.SetName(ann_name);
                info("link deleted");
            }
            else
            {
                showInfo(" ничего не выбрано");
            }
        }

        private void edit_button4_Click(object sender, EventArgs e)
        {
            swModel = swApp.IActiveDoc2;
            SelectionMgr swSelmgr = (SelectionMgr)swModel.SelectionManager;
            object view = swSelmgr.GetSelectedObject6(1, 0);
            DrawingDoc drw = (DrawingDoc)swModel;

            drw.InsertGroup();

            Array arr = (Array)view;

            info("count: " + swSelmgr.GetSelectedObjectCount2(-1));
        }
    }
}