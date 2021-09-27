using SetLabels.service;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

using static SetLabels.utils.ViewUtils;

namespace SetLabels
{
    class SetLabels
    {
        private const string VERSION = "2.4.0";

        private SldWorks swApp;
        private string swPath;
        private SetLabelsForm form;

        private ListViews listViews;

        private OptionsService optionsService;
        private ViewService viewService;
        private GtolService gtolService;
        private SurfaceService surfaceService;
        private ViewDraw viewDraw;
        private LabelsService labelService;

        string exlude = "ЙЗХЪЫЬОЧ";
        private bool isColor = true;
        private bool isOut = true;
        private bool isSheetsNumbers = true;
        private bool isLinkGtols = true;

        public SetLabels(SldWorks _app, string _path, SetLabelsForm _form)
        {
            swApp = _app;
            swPath = _path;
            form = _form;
            optionsService = new OptionsService(swPath);
            initOnce();
        }

        private void initOnce()
        {
            form.Text = "Set Labels v." + VERSION;
            viewService = new ViewService();
            gtolService = new GtolService();
            surfaceService = new SurfaceService();
            viewDraw = new ViewDraw();
            initExludeLetters();
            initFieldsColors();
            hasColor();
            hasOut();
            initTestOptions();
            drawTitle();
        }

        private void initExludeLetters()
        {
            try
            {
                exlude = optionsService.GetLetters();
                labelService = new LabelsService(exlude);
                form.saveExludeLetters(exlude);
            }
            catch
            {
                MessageBox.Show("Проблема загрузки файла настроек. \r\n Буквы исключенные из обозначения видов, заданы по умолчанию.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                exlude = "ЙЗХЪЫЬОЧ";
            }
        }

        private void initFieldsColors()
        {
            try
            {
                ListColors colors = optionsService.getListColors();
                viewDraw.setColor(colors);
            }
            catch
            {
                MessageBox.Show("Проблема загрузки файла настроек.\r\n Будут установлены цвета по умолчанию.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                viewDraw.setColor(new ListColors());
            }
        }

        private void hasColor()
        {
            isColor = optionsService.isColor();
            form.hasColor(isColor);
        }

        private void hasOut()
        {
            isOut = optionsService.isOut();
            form.hasOut(isOut);
        }

        private void initTestOptions()
        {
            try
            {
                isSheetsNumbers = optionsService.isSheetsNames().Equals("1");
                isLinkGtols = optionsService.isLinkGtols().Equals("1");
            }
            catch
            {
                MessageBox.Show("Проблема загрузки файла настроек. \r\n Буквы исключенные из обозначения видов, заданы по умолчанию.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                exlude = "ЙЗХЪЫЬОЧ";
            }
        }

        private void clearPanels()
        {
            form.clearPanels();
        }

        public void readViews()
        {
            ModelDoc2 model = swApp.IActiveDoc2;
            if (isDrawing(model))
            {
                try
                {
                    setGraficsUpdate(false);
                    info("Идет чтение буквенных меток");
                    clearPanels();
                    listViews = new ListViews();
                    viewService.readViews(model, ref listViews);
                    labelService.generateLabels(ref listViews, isOut);
                    drawAllLists(listViews);
                    info("Чтение буквенных меток закончено.");


                }
                catch
                {
                    info("ГДЕ_ТО КОСЯК !!!");
                }
                finally
                {
                    setGraficsUpdate(true);
                }
            }
        }

        public void setLabels()
        {
            ModelDoc2 model = swApp.IActiveDoc2;
            try
            {
                setGraficsUpdate(false);
                model.SetAddToDB(true);
                model.SetDisplayWhenAdded(false);
                info("Идет обновление буквенных обозначений");
                labelService.setLabelsToViews(ref listViews);
                if (isLinkGtols)
                {
                    gtolService.relinkGtolByTatumtag(ref listViews);
                }
                if (isSheetsNumbers)
                {
                    checkSheetName();
                }
                info("Обновление буквенных обозначений закончено.");
                //model.Rebuild(1);
                (model as DrawingDoc).ForceRebuild();

                model.WindowRedraw();
                //model.GraphicsRedraw2();
                errorsChecking();
            }
            finally
            {
                model.SetAddToDB(false);
                model.SetDisplayWhenAdded(true);
                setGraficsUpdate(true);
            }
        }

        private void errorsChecking()
        {
            if (checkErrorsInList())
            {
                clearPanels();
                drawAllErrors(listViews);
                info("Переименуйте данные виды вручную");
                form.setButtonRename(false);
            }
            else
            {
                //form.exit();
                info("ВСЕ МЕТКИ ПЕРЕИМНОВАНЫ");
            }
        }

        // Возможно выделить в отдельную опцию а пока не надо...
        /*
        public void finalCheck()
        {
            bool res = labelService.errorsLabelsCheck(ref listViews);
            errorsChecking();
        }
        */

        private void drawAllLists(ListViews list)
        {
            string[] viewNames = list.drawingNames.ToArray();
            drawView(list.getProjected(), viewNames);
            drawView(list.getAuxiliaty(), viewNames);
            drawView(list.getSection(), viewNames);
            drawView(list.getDetail(), viewNames);
            drawDatumTag(list.getDatumTags());
            drawSurface(list.getSurfaces());
        }

        private void drawAllErrors(ListViews list)
        {
            string[] viewNames = list.drawingNames.ToArray();

            if (list.getErrorLists().Count != 0)
            {
                drawView(list.getErrorLists(), viewNames);
            }
            if(list.getErrorDatumTag().Count != 0)
            {
                drawDatumTag(list.getErrorDatumTag());
            }
            if(list.getErrorSurfaces().Count != 0)
            {
                drawSurface(list.getErrorSurfaces());
            }
        }

        private void drawTitle()
        {
                form.addTitle(viewDraw.getTitleInfoPanel());
        }

        private void drawView(List<ViewExtended> list, string[] viewNames)
        {
            foreach (ViewExtended view in list) 
            {
                Panel panel = viewDraw.getViewInfoPanel(view, isColor, isOut, viewNames, form.textBox_MouseDoubleClick, form.comboBox__SelectedIndexChanged);
                panel.MouseDoubleClick += form.panel_MouseDoubleClick;
                form.addPanel(panel);
            } 
        }

        private void drawDatumTag(List<DatumTagExtended> list)
        {
            foreach (DatumTagExtended tag in list)
            {
                Panel panel = viewDraw.getDatumTagPanel(tag, isColor, isOut, form.textBox_MouseDoubleClick);
                panel.MouseDoubleClick += form.panel_MouseDoubleClick;
                form.addPanel(panel);
            }
        }

        private void drawSurface(List<SurfaceExtended> list)
        {
            List<string> surfnamesList = new List<string>();
            listViews.getSurfaces().ForEach(x => surfnamesList.Add(x.surfaceIndex.ToString()));

            string[] surfNames = surfnamesList.ToArray();

            foreach (SurfaceExtended surf in list)
            {
                Panel panel = viewDraw.getSurfacePanel(surf, isColor, isOut,surfNames , form.textBox_MouseDoubleClick, form.comboBoxSurface__SelectedIndexChanged);
                panel.MouseDoubleClick += form.panel_MouseDoubleClick;
                form.addPanel(panel);
            }
        }


        public void zoomTo(string viewName)
        {
            ModelDoc2 swModel = swApp.IActiveDoc2;
            ModelDocExtension swModelDocExt = (ModelDocExtension)swModel.Extension;
            DrawingDoc drawing = (DrawingDoc)swModel;
            SelectionMgr swSelMgr = swModel.SelectionManager;
            try
            {
                IView view = null;
                foreach(ViewExtended v in listViews.getAllViews())
                {
                    if (v.drawingName.Equals(viewName))
                    {
                        view = v.view;
                    }
                }

                if(view != null)
                {

                    Sheet sheet = view.Sheet;
                    drawing.ActivateSheet(sheet.GetName());
                    double[] outline = view.GetOutline();
                    swModel.ViewZoomTo2(outline[0] , outline[1], 0, outline[2], outline[3] , 1);
                    drawing.ActivateView(view.GetName2());


                    switch (view.Type)
                    {
                        case (int)swDrawingViewTypes_e.swDrawingAuxiliaryView:
                        case (int)swDrawingViewTypes_e.swDrawingProjectedView:
                            ProjectionArrow arr = view.GetProjectionArrow();
                            double[] arrCoords = arr.GetCoordinates();
                            bool s = swModel.Extension.SelectByID2("", "VIEWARROW", arrCoords[0], arrCoords[1], arrCoords[2], false, 0, null, 0);
                            break;

                        case (int)swDrawingViewTypes_e.swDrawingSectionView:
                            DrSection sec = view.GetSection();
                            double[] asec = sec.GetTextInfo();
                            s = swModel.Extension.SelectByID2("", "", asec[0] + 0.00035, asec[1] - 0.00036, asec[1], false, 0, null, 0);
                            break;
                        case (int)swDrawingViewTypes_e.swDrawingDetailView:
                            DetailCircle dc = view.GetDetail();
                            double pX = 0;
                            double pY = 0;
                            dc.GetLabelPosition(out pX, out pY);
                            s = swModel.Extension.SelectByID2("", "", pX, pY, 0, false, 0, null, 0);
                            break;

                    }
                }

                
            }
            catch
            {
                info("Ошибка при зум-е на вид: " + viewName);
            }
        }

        public void setLinked(string viewName, int linkIndex)
        {
            string link = "";
            if(linkIndex > 0)
            {
                link = listViews.drawingNames[linkIndex - 1].ToString();
            }
            string label = "";
            ViewExtended targetView = null;
            foreach (ViewExtended extV in listViews.getAllViews())
            {
                if (extV.drawingName.Equals(link))
                {
                    label = extV.newLabel;
                }
                if (extV.drawingName.Equals(viewName))
                {
                    targetView = extV;
                }
            }
            if(targetView != null)
            {
                targetView.newLabel = label;
                targetView.linkedName = link;
            }
            updatePanels();

        }

        public void setLinkedSurface(string index, int linkedIndex)
        {
            int surfIndex = -1;
            try
            {
                 surfIndex = int.Parse(index.Replace("surface", ""));

            }
            catch
            {
                info("index " + index + "  не может быть преобразован в число");
            }
            listViews.getSurfaces()[surfIndex].linkedId = linkedIndex - 1;

            listViews.getSurfaces()[surfIndex].isLinked = linkedIndex >= 0;
            info("index " + index + " link " + listViews.getSurfaces()[surfIndex].linkedId + "  isLink " + listViews.getSurfaces()[surfIndex].isLinked);
            updatePanels();
        }

        private void updatePanels()
        {
            labelService.generateLabels(ref listViews, isOut);
            listViews.getAllViews().ForEach(x => updatePanel(x));
            listViews.getDatumTags().ForEach(x => updateTagPanel(x));
            listViews.getSurfaces().ForEach(x => updateSurfasePanel(x));
        }


        private void updatePanel(ViewExtended view)
        {
            Panel basePanel = form.getPanel();
            foreach (Control panel in basePanel.Controls)
            {
                if (panel.Name.Equals(view.drawingName))
                {
                    Control[] con = panel.Controls.Find("newLabel", true);
                    TextBox box = (TextBox)con[0];
                    box.Text = view.newLabel;
                }
            }
        }

        private void updateTagPanel(DatumTagExtended _tag)
        {
            Panel basePanel = form.getPanel();
            foreach (Control panel in basePanel.Controls)
            {
                if (panel.Name.Equals("Tag" + _tag.tagIndex + _tag.drawingName))
                {
                    Control[] con = panel.Controls.Find("newLabel", true);
                    TextBox box = (TextBox)con[0];
                    box.Text = _tag.newLabel;
                }
            }
        }

        private void updateSurfasePanel(SurfaceExtended surf)
        {
            Panel basePanel = form.getPanel();
            foreach (Control panel in basePanel.Controls)
            {
                if (panel.Name.Equals("Surf" + surf.surfaceIndex + surf.drawingName))
                {
                    Control[] con = panel.Controls.Find("newLabel", true);
                    TextBox box = (TextBox)con[0];
                    box.Text = surf.newLabel;
                }
            }
        }

        private bool checkErrorsInList()
        {
            return listViews.getErrorDatumTag().Count != 0
                || listViews.getErrorLists().Count != 0
                || listViews.getErrorSurfaces().Count != 0;
        }


        public void setSurfaceNote()
        {
            Note note = getSelectedNote();
            if (note != null)
            {
                surfaceService.setSurface(note);
            }
            else
            {
                info("Для задания ссылки на поверхность выберите ЗАМЕТКУ");
            }
        }

        public void removeSurfaceNote()
        {
            Note note = getSelectedNote();
            if (note != null)
            {
                surfaceService.removeSurface(note);
            }
            else
            {
                info("Для удаления ссылки на поверхность выберите ЗАМЕТКУ");
            }
        }

        private Note getSelectedNote()
        {
            ModelDoc2 swModel = swApp.IActiveDoc2;
            SelectionMgr swSelMgr = (SelectionMgr)swModel.SelectionManager;
            object selected = swSelMgr.GetSelectedObject6(1, -1);
            int type = swSelMgr.GetSelectedObjectType3(1, -1);
            if (type != (int)swSelectType_e.swSelNOTES)
            {
                return null;
            }
            return (Note)selected;
        }

        public void hasColor(bool value)
        {
            info("COLOR is " + value);
            isColor = value;
            optionsService.setColor(value);

            clearPanels();
            drawAllLists(listViews);
        }

        public void hasOut(bool value)
        {
            info("OUT is " + value);
            isOut = value;
            optionsService.setOut(value);
            clearPanels();
            drawAllLists(listViews);
        }

        public void setExludeLetters(string letters)
        {
            try
            {
                optionsService.SetLetters(letters);
                exlude = letters;
                form.saveExludeLetters(exlude);
            }
            catch
            {
                MessageBox.Show("Проблема сохранения в файл настроек.\r\n Изменения не сохранены.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void setGraficsUpdate(bool value)
        {
            swApp.IActiveDoc2.IActiveView.EnableGraphicsUpdate = value;
            swApp.IActiveDoc2.FeatureManager.EnableFeatureTree = value;
        }

        private bool isDrawing(ModelDoc2 model)
        {
            if (model == null)
            {
                MessageBox.Show("Загрузите документ SolidWorks-a", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            int docType = model.GetType();
            if (docType != (int)swDocumentTypes_e.swDocDRAWING)
            {
                MessageBox.Show("Функция работает только с файлами чертежей(*.slddrw)",
                             "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void info(string message)
        {
            form.info(message);
        }


        public bool isLinked(IView view)
        {
            return isLinkedUtils(view);
        }

        public string getNameLinked(IView view)
        {
            return getLinkedName(view);
        }

        public void checkSheetName()
        {
            ModelDoc2 model = swApp.ActiveDoc;
            DrawingDoc drw = (DrawingDoc)model;
            SheetManager mgr = new SheetManager();
            mgr.controlSheetNumber(ref listViews, drw.GetSheetNames(), swApp.ActiveDoc);
        }

    }
}
