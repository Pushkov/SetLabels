﻿using SetLabels.service;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Runtime.InteropServices;
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

        private void btnTest1_Click(object sender, EventArgs e)
        {
            ModelDoc2 model = swApp.ActiveDoc;
            SelectionMgr selMgr = model.SelectionManager;

            if (selMgr.GetSelectedObjectType3(1, -1) == (int)swSelectType_e.swSelNOTES)
                //if (selMgr.GetSelectedObjectType3(1, -1) == (int)swSelectType_e.swSelDRAWINGVIEWS)
            {
                Note view = (Note)selMgr.GetSelectedObject6(1, 0);
                //IView view = (IView)selMgr.GetSelectedObject6(1, 0);
                //view.PropertyLinkedText += " (2)";
                info("note name: " + view.GetName());
            }
            else
            {
                info(" вид не выбран");
            }

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            ModelDoc2 model = swApp.ActiveDoc;
            SelectionMgr selMgr = model.SelectionManager;

            if (selMgr.GetSelectedObjectType3(1, -1) == (int)swSelectType_e.swSelDRAWINGVIEWS)
            {
                IView view = (IView)selMgr.GetSelectedObject6(1, 0);
                if(view.Type == (int) swDrawingViewTypes_e.swDrawingProjectedView)
                {
                    double[] coords;
                    IProjectionArrow arr = view.GetProjectionArrow();
                    coords = arr.GetCoordinates();

                    DrawingDoc drw = (DrawingDoc)model;
                    string currName = view.GetName2();
                    string sheet = view.GetBaseView().Sheet.GetName();
                    drw.ActivateSheet(sheet);
                    string viewName = view.GetBaseView().GetName2();
                    drw.ActivateView(viewName);
                    string sheetIndex = "(2)";
                    
                    Note note1 = drw.CreateText2(sheetIndex, coords[21] + 0.005, coords[22] + 0.0045, coords[23], 0.007, 0);
                }
                else
                {
                    info(" вид не тот");
                }
            }
            else
            {
                info(" вид не выбран");
            }
        }
    }
}
