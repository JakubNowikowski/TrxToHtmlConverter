using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using HtmlAgilityPack;

namespace TrxToHtmlConverter
{
    class HtmlGeneration
    {
        private string _OutputPath;
        private string _TemplatePath;
        public HtmlGeneration(string path)
        {
            _OutputPath = path;
            _TemplatePath = @"C:\Users\ivakrupa\Downloads\xd.html";
        }
        public void Generation()
        {
            var document = LoadTemplate(_TemplatePath);
            document.DocumentNode.AddClass("p");
            
            ExportToFile(document.DocumentNode.InnerHtml);
        }

        private void ExportToFile(string fileContent)
        {
            StreamWriter fw = new StreamWriter(_OutputPath);
            fw.Write(fileContent);
            fw.Close();
        }

        private HtmlDocument LoadTemplate(string templatePath)
        {
            var doc = new HtmlDocument();
            doc.Load(templatePath);

            return doc;
        }
    }
}
