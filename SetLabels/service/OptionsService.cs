using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace SetLabels
{
    class OptionsService
    {
        string swPath;

        public OptionsService(string _path)
        {
            this.swPath = _path;
        }


        private XmlDocument getFile(string _path)
        {
            XmlDocument xDoc = null;
            try
            {
                xDoc = new XmlDocument();
                xDoc.Load(swPath + "options.xml");
            }
            catch
            {
                MessageBox.Show("XML -" + swPath);
            }
            return xDoc;
        }

        public string isSheetsNames()
        {
            XmlDocument xDoc = getFile(swPath);
            XmlElement xRoot = xDoc.DocumentElement;
            XmlNode xSheet = xRoot.SelectSingleNode("sheets");
            return xSheet.InnerText;
        }

        public string isLinkGtols()
        {
            XmlDocument xDoc = getFile(swPath);
            XmlElement xRoot = xDoc.DocumentElement;
            XmlNode xGtol = xRoot.SelectSingleNode("linkgtols");
            return xGtol.InnerText;
        }


        public string GetLetters()
        {
            XmlDocument xDoc = getFile(swPath);
            XmlElement xRoot = xDoc.DocumentElement;
            XmlNode xLet = xRoot.SelectSingleNode("letters");
            return xLet.InnerText;
        }

        public void SetLetters(string val)
        {
            XmlDocument xDoc = getFile(swPath);
            XmlElement xRoot = xDoc.DocumentElement;
            XmlNode xLet = xRoot.SelectSingleNode("letters");
            xLet.InnerText = val;
            xDoc.Save(swPath + "options.xml");
        }

        public bool isColor()
        {
            XmlDocument xDoc = getFile(swPath);
            XmlElement xRoot = xDoc.DocumentElement;
            XmlNode xLet = xRoot.SelectSingleNode("color");
            return xLet.InnerText.Equals("1");
        }



        public void setColor(bool val)
        {
            XmlDocument xDoc = getFile(swPath);
            XmlElement xRoot = xDoc.DocumentElement;
            XmlNode xLet = xRoot.SelectSingleNode("color");
            xLet.InnerText = val ?"1":"0";
            xDoc.Save(swPath + "options.xml");
        }

        public bool isOut()
        {
            XmlDocument xDoc = getFile(swPath);
            XmlElement xRoot = xDoc.DocumentElement;
            XmlNode xLet = xRoot.SelectSingleNode("out");
            return xLet.InnerText.Equals("1");
        }

        public void setOut(bool val)
        {
            XmlDocument xDoc = getFile(swPath);
            XmlElement xRoot = xDoc.DocumentElement;
            XmlNode xLet = xRoot.SelectSingleNode("out");
            xLet.InnerText = val ? "1" : "0";
            xDoc.Save(swPath + "options.xml");
        }

        public ListColors getListColors()
        {
            ListColors list = new ListColors();
            try
            {
                XmlDocument xDoc = getFile(swPath);
                XmlElement xRoot = xDoc.DocumentElement;
                XmlNode xLet = xRoot.SelectSingleNode("projColor");
                list.PROJECTED_COLOR = ColorTranslator.FromHtml(xLet.InnerText);
                XmlNode xLet1 = xRoot.SelectSingleNode("auxColor");
                list.AUXILIARY_COLOR = ColorTranslator.FromHtml(xLet1.InnerText);
                XmlNode xLet2 = xRoot.SelectSingleNode("sectColor");
                list.SECTION_COLOR = ColorTranslator.FromHtml(xLet2.InnerText);
                XmlNode xLet3 = xRoot.SelectSingleNode("detColor");
                list.DETAIL_COLOR = ColorTranslator.FromHtml(xLet3.InnerText);
                XmlNode xLet4 = xRoot.SelectSingleNode("gtolColor");
                list.GTOL_COLOR = ColorTranslator.FromHtml(xLet4.InnerText);
                XmlNode xLet5 = xRoot.SelectSingleNode("surfColor");
                list.SURFACE_COLOR = ColorTranslator.FromHtml(xLet5.InnerText);
                XmlNode xLet6 = xRoot.SelectSingleNode("outColor");
                list.OUT_COLOR = ColorTranslator.FromHtml(xLet6.InnerText);
            }
            catch
            {
                MessageBox.Show("Ошибка загрузки XML файла.\nЗагружены настройки по умолчанию.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return list;
        }

        public double readSheetLabelFontSize()
        {
            double data;
            XmlDocument xDoc = getFile(swPath);
            XmlElement xRoot = xDoc.DocumentElement;
            try
            {
                XmlNode xLet = xRoot.SelectSingleNode("sheet_label_font_size");
                data = double.Parse(xLet.InnerText);
            }
            catch
            {
                data = 7;
            }
            

            return data / 1000;
        }
    }
}
