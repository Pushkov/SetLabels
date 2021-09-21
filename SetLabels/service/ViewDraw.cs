using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SetLabels
{
    class ViewDraw
    {
        private int WIDTH_PANEL = 875;
        private int WIDTH_OLD_LABEL = 30;
        private int WIDTH_NAME = 235;
        private int WIDTH_TYPE = 205;
        private int WIDTH_PARENT_SHEET = 57;
        private int WIDTH_VIEW_SHEET = 57;
        private int WIDTH_NEW_LABEL = 35;
        private int WIDTH_LINK = 175;

        private int FONTSIZE_TITLE = 11;
        private int FONTSIZE_ROW = 9;
        private HorizontalAlignment CENTER = HorizontalAlignment.Center;
        private HorizontalAlignment LEFT = HorizontalAlignment.Left;

        private Color PROJECTED_COLOR = ColorTranslator.FromHtml("#A194B3");
        private Color AUXILIARY_COLOR = ColorTranslator.FromHtml("#f3a3ab");
        private Color SECTION_COLOR = ColorTranslator.FromHtml("#c5f9c6");
        private Color DETAIL_COLOR = ColorTranslator.FromHtml("#FFFBCE");
        private Color GTOL_COLOR = ColorTranslator.FromHtml("#bdf7ee");
        private Color SURFACE_COLOR = ColorTranslator.FromHtml("#9cadfa");
        private Color OUT_COLOR = ColorTranslator.FromHtml("#8B9EAA");

        
        public Panel getTitleInfoPanel()
        {
            FlowLayoutPanel panel = new FlowLayoutPanel();
            panel.WrapContents = false;
            panel.Padding = new Padding(20, 0, 0, 20);
            panel.Width = WIDTH_PANEL;
            panel.Height = 42;
            panel.BorderStyle = BorderStyle.FixedSingle;

            TextBox oldLabel = getTextBox("Сущ. метка", FONTSIZE_TITLE, CENTER);
            oldLabel.Width = WIDTH_OLD_LABEL;
            oldLabel.Multiline = true;
            panel.Controls.Add(oldLabel);

            TextBox name = getTextBox("Наименование вида", FONTSIZE_TITLE, CENTER);
            name.Width = WIDTH_NAME;
            panel.Controls.Add(name);

            TextBox type = getTextBox("Тип вида", FONTSIZE_TITLE, CENTER);
            type.Width = WIDTH_TYPE;
            panel.Controls.Add(type);

            TextBox parentSheet = getTextBox("Лист обозн.", FONTSIZE_TITLE, CENTER);
            parentSheet.Width = WIDTH_PARENT_SHEET;
            parentSheet.Multiline = true;
            panel.Controls.Add(parentSheet);

            TextBox sheet = getTextBox("Лист вид", FONTSIZE_TITLE, CENTER);
            sheet.Width = WIDTH_VIEW_SHEET;
            sheet.Multiline = true;
            panel.Controls.Add(sheet);

            TextBox newLabel = getTextBox("Нов. метка", FONTSIZE_TITLE, CENTER);
            newLabel.Width = WIDTH_NEW_LABEL;
            newLabel.Multiline = true;
            panel.Controls.Add(newLabel);

            TextBox linkedLabel = getTextBox("Вид указывающий на обозначение", FONTSIZE_TITLE, CENTER);
            linkedLabel.Width = WIDTH_LINK;
            linkedLabel.Multiline = true;
            panel.Controls.Add(linkedLabel);

            return panel;
        }


        public Panel getViewInfoPanel(ViewExtended extView, bool isColor, bool isOut, string[] viewNames, MouseEventHandler mouseDblClick, EventHandler cbChange)
        {
            IView view = extView.view;

            Color backColor = getViewColor(view.Type, isColor);
            if (!extView.inSheet && isOut)
            {
                backColor = Color.LightGray;
            }

            FlowLayoutPanel panel = new FlowLayoutPanel();
            panel.Name = extView.drawingName;
            panel.WrapContents = false;
            panel.Padding = new Padding(20, 0, 0, 20);
            panel.Width = WIDTH_PANEL;
            panel.MaximumSize = new System.Drawing.Size(900, 37);
            panel.Height = 32;
            panel.BackColor = backColor;

            TextBox oldLabel = getTextBox(extView.oldLabel, FONTSIZE_ROW, CENTER);
            oldLabel.Width = WIDTH_OLD_LABEL;
            oldLabel.BackColor = backColor;
            oldLabel.MouseDoubleClick += mouseDblClick;
            panel.Controls.Add(oldLabel);

            TextBox name = getTextBox(extView.drawingName, FONTSIZE_ROW, LEFT);
            name.Width = WIDTH_NAME;
            name.BackColor = backColor;
            name.MouseDoubleClick += mouseDblClick;
            panel.Controls.Add(name);

            TextBox type = getTextBox(((DrawingViewType)view.Type).ToString(), FONTSIZE_ROW, LEFT);
            type.Width = WIDTH_TYPE;
            type.BackColor = backColor;
            type.MouseDoubleClick += mouseDblClick;
            panel.Controls.Add(type);

            TextBox parentSheet = getTextBox(view.GetBaseView().Sheet.GetName(), FONTSIZE_ROW, CENTER);
            parentSheet.Width = WIDTH_PARENT_SHEET;
            parentSheet.BackColor = backColor;
            parentSheet.MouseDoubleClick += mouseDblClick;
            panel.Controls.Add(parentSheet);

            TextBox sheet = getTextBox(view.Sheet.GetName(), FONTSIZE_ROW, CENTER);
            sheet.Width = WIDTH_VIEW_SHEET;
            sheet.BackColor = backColor;
            sheet.MouseDoubleClick += mouseDblClick;
            panel.Controls.Add(sheet);

            TextBox newLabel = getTextBox(extView.newLabel, FONTSIZE_ROW, CENTER);
            newLabel.Name = "newLabel";
            newLabel.Width = WIDTH_NEW_LABEL;
            newLabel.BackColor = backColor;
            newLabel.MouseDoubleClick += mouseDblClick;
            panel.Controls.Add(newLabel);

            ComboBox linkedLabel = new ComboBox();
            linkedLabel.Width = WIDTH_LINK;
            linkedLabel.Margin = new Padding(5, 3, 5, 0);
            linkedLabel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            linkedLabel.Items.Add("");
            linkedLabel.Items.AddRange(viewNames);
            linkedLabel.SelectedItem = extView.linkedName;
            linkedLabel.SelectedIndexChanged += cbChange;
            panel.Controls.Add(linkedLabel);

            return panel;
        }

        public Panel getDatumTagPanel(DatumTagExtended extDatumTag, bool isColor, bool isOut, MouseEventHandler mouseDblClick)
        {
            IView view = extDatumTag.view;

            Color backColor = isColor ? GTOL_COLOR : System.Drawing.SystemColors.Menu;

            FlowLayoutPanel panel = new FlowLayoutPanel();
            panel.Name = "Tag" + extDatumTag.tagIndex + extDatumTag.drawingName;
            panel.WrapContents = false;
            panel.Padding = new Padding(20, 0, 0, 20);
            panel.Width = WIDTH_PANEL;
            panel.MaximumSize = new System.Drawing.Size(900, 37);
            panel.Height = 32;
            panel.BackColor = backColor;

            //TextBox oldLabel = getTextBox(extDatumTag.tag.GetLabel(), FONTSIZE_ROW, CENTER);
            TextBox oldLabel = getTextBox(extDatumTag.oldLabel, FONTSIZE_ROW, CENTER);
            oldLabel.Width = WIDTH_OLD_LABEL;
            oldLabel.BackColor = backColor;
            oldLabel.MouseDoubleClick += mouseDblClick;
            panel.Controls.Add(oldLabel);

            TextBox name = getTextBox("База: " + extDatumTag.tag.GetLabel(), FONTSIZE_ROW, LEFT);
            name.Width = WIDTH_NAME;
            name.BackColor = backColor;
            name.MouseDoubleClick += mouseDblClick;
            panel.Controls.Add(name);

            TextBox type = getTextBox("ОБОЗНАЧЕНИЕ БАЗЫ", FONTSIZE_ROW, LEFT);
            type.Width = WIDTH_TYPE;
            type.BackColor = backColor;
            type.MouseDoubleClick += mouseDblClick;
            panel.Controls.Add(type);

            TextBox parentSheet = getTextBox("", FONTSIZE_ROW, CENTER);
            parentSheet.Width = WIDTH_PARENT_SHEET;
            parentSheet.BackColor = backColor;
            parentSheet.MouseDoubleClick += mouseDblClick;
            panel.Controls.Add(parentSheet);

            TextBox sheet = getTextBox(view.Sheet.GetName(), FONTSIZE_ROW, CENTER);
            sheet.Width = WIDTH_VIEW_SHEET;
            sheet.BackColor = backColor;
            sheet.MouseDoubleClick += mouseDblClick;
            panel.Controls.Add(sheet);

            TextBox newLabel = getTextBox(extDatumTag.newLabel, FONTSIZE_ROW, CENTER);
            newLabel.Name = "newLabel";
            newLabel.Width = WIDTH_NEW_LABEL;
            newLabel.BackColor = backColor;
            newLabel.MouseDoubleClick += mouseDblClick;
            panel.Controls.Add(newLabel);

            TextBox qtyLinks = getTextBox(/*"На базу ссылается " + extDatumTag.gtols.Count.ToString() + " допусков."*/ "", FONTSIZE_ROW, CENTER);
            qtyLinks.Width = WIDTH_LINK;
            qtyLinks.BackColor = backColor;
            qtyLinks.MouseDoubleClick += mouseDblClick;
            panel.Controls.Add(qtyLinks);

            return panel;
        }

        public Panel getSurfacePanel(SurfaceExtended extSurface, bool isColor, bool isOut, string[] surfNames, MouseEventHandler mouseDblClick, EventHandler cbChange)
        {
            IView view = extSurface.view;

            Color backColor = isColor ? SURFACE_COLOR : System.Drawing.SystemColors.Menu;

            FlowLayoutPanel panel = new FlowLayoutPanel();
            panel.Name = "Surf" + extSurface.surfaceIndex + extSurface.drawingName;
            panel.WrapContents = false;
            panel.Padding = new Padding(20, 0, 0, 20);
            panel.Width = WIDTH_PANEL;
            panel.MaximumSize = new System.Drawing.Size(900, 37);
            panel.Height = 32;
            panel.BackColor = backColor;

            TextBox oldLabel = getTextBox(extSurface.surface.GetText(), FONTSIZE_ROW, CENTER);
            oldLabel.Width = WIDTH_OLD_LABEL;
            oldLabel.BackColor = backColor;
            oldLabel.MouseDoubleClick += mouseDblClick;
            panel.Controls.Add(oldLabel);

            TextBox name = getTextBox("Поверхность ID: " + extSurface.surfaceIndex, FONTSIZE_ROW, LEFT);
            name.Width = WIDTH_NAME;
            name.BackColor = backColor;
            name.MouseDoubleClick += mouseDblClick;
            panel.Controls.Add(name);

            TextBox type = getTextBox("ОБОЗНАЧЕНИЕ ПОВЕРХНОСТИ", FONTSIZE_ROW, LEFT);
            type.Width = WIDTH_TYPE;
            type.BackColor = backColor;
            type.MouseDoubleClick += mouseDblClick;
            panel.Controls.Add(type);

            TextBox parentSheet = getTextBox("", FONTSIZE_ROW, CENTER);
            parentSheet.Width = WIDTH_PARENT_SHEET;
            parentSheet.BackColor = backColor;
            parentSheet.MouseDoubleClick += mouseDblClick;
            panel.Controls.Add(parentSheet);

            TextBox sheet = getTextBox(view.Sheet.GetName(), FONTSIZE_ROW, CENTER);
            sheet.Width = WIDTH_VIEW_SHEET;
            sheet.BackColor = backColor;
            sheet.MouseDoubleClick += mouseDblClick;
            panel.Controls.Add(sheet);

            TextBox newLabel = getTextBox(extSurface.newLabel, FONTSIZE_ROW, CENTER);
            newLabel.Name = "newLabel";
            newLabel.Width = WIDTH_NEW_LABEL;
            newLabel.BackColor = backColor;
            newLabel.MouseDoubleClick += mouseDblClick;
            panel.Controls.Add(newLabel);


            TextBox id = getTextBox("ID Поверхности =", FONTSIZE_ROW, CENTER);
            id.Width = WIDTH_LINK - 55;
            id.BackColor = backColor;
            panel.Controls.Add(id);

            ComboBox linkedLabel = new ComboBox();
            linkedLabel.Name = "surface" + extSurface.surfaceIndex;
            linkedLabel.Width = 50;
            linkedLabel.Margin = new Padding(2, 3, 5, 0);
            linkedLabel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            linkedLabel.Items.Add("");
            linkedLabel.Items.AddRange(surfNames);
            linkedLabel.SelectedItem = extSurface.linkedId.ToString();
            linkedLabel.SelectedIndexChanged += cbChange;
            panel.Controls.Add(linkedLabel);

            return panel;
        }






        private TextBox getTextBox(string text, int fontSize, HorizontalAlignment textAlign)
        {
            TextBox box = new TextBox();
            box.Name = text;
            box.Text = text;
            box.Font = new Font("Microsoft Sans Serif", fontSize);
            box.Height = 60;
            box.ReadOnly = true;
            box.BorderStyle = BorderStyle.None;
            box.TextAlign = textAlign;
            return box;
        }

        private Color getViewColor(int type, bool isColor)
        {
            if (isColor) { 
            switch ((swDrawingViewTypes_e)type)
            {
                case swDrawingViewTypes_e.swDrawingProjectedView:
                    return PROJECTED_COLOR;
                case swDrawingViewTypes_e.swDrawingAuxiliaryView:
                    return AUXILIARY_COLOR;
                case swDrawingViewTypes_e.swDrawingSectionView:
                    return SECTION_COLOR;
                case swDrawingViewTypes_e.swDrawingDetailView:
                    return DETAIL_COLOR;
                default:
                    return System.Drawing.SystemColors.Menu;
            }
        }
            else
            {
                return System.Drawing.SystemColors.Menu;
            }
        }

        public void setColor(ListColors list)
        {
            PROJECTED_COLOR = list.PROJECTED_COLOR;
            AUXILIARY_COLOR = list.AUXILIARY_COLOR;
            SECTION_COLOR = list.SECTION_COLOR;
            DETAIL_COLOR = list.DETAIL_COLOR;
            OUT_COLOR = list.OUT_COLOR;
            GTOL_COLOR = list.GTOL_COLOR;
            SURFACE_COLOR = list.SURFACE_COLOR;

        }
    }
}
