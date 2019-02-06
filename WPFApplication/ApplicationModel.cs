using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrxToHtmlConverter;

namespace WPFApplication
{
    class ApplicationModel
    {
        public string inputPath;
        public string changeSetNumber;
        public string pbiNumber;
        public string outputPath;
        public string enableToOpen = "false";
        public string enableToConvert = "false";
        public string result = "";

        public string OpenFileDialog()
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".trx";
            dlg.Filter = "TRX Files (*.trx)|*.trx";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                return filename;
            }
            return null;
        }

        public void Convert()
        {
            HtmlGeneration Html = new HtmlGeneration(inputPath, outputPath, pbiNumber, changeSetNumber);
            Html.InitializeTrxData();
            Html.Generation();
        }
    }
}
