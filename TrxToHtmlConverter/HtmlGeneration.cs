using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace TrxToHtmlConverter
{
    class HtmlGeneration
    {
        private string _Path;
        public HtmlGeneration(string path)
        {
            _Path = path;
        }
        public void Generation()
        {
            StringWriter stringWriter = new StringWriter();
            using (HtmlTextWriter writer = new HtmlTextWriter(stringWriter))
            {
                writer.WriteFullBeginTag("head");
                writer.WriteFullBeginTag("style");
                writer.WriteLine("body");
                writer.WriteLine("{");
                writer.WriteStyleAttribute("font-family", "arial");
                writer.WriteLine("}");
                writer.WriteLine("div.header1");
                writer.WriteLine("{");
                writer.WriteStyleAttribute("background-color", "#3F51B5");
                writer.WriteStyleAttribute("height", "84px");
                writer.WriteStyleAttribute("position", "relative");
                writer.WriteLine("}");
                writer.WriteLine("div.header1 h1");
                writer.WriteLine("{");
                writer.WriteStyleAttribute("position", "absolute");
                writer.WriteStyleAttribute("color", "#FFFFFF");
                writer.WriteStyleAttribute("margin", "0");
                writer.WriteStyleAttribute("top", "50%");
                writer.WriteStyleAttribute("left", "84px");
                writer.WriteStyleAttribute("transform", "translate(0, -50%)");
                writer.WriteLine("}");
                writer.WriteEndTag("style");
                writer.WriteEndTag("head");
            }
            ExportToFile(stringWriter);
        }

        private void ExportToFile(StringWriter fileContent)
        {
            StreamWriter fw = new StreamWriter(_Path);
            fw.Write(fileContent.ToString());
            fw.Close();
        }
    }
}
