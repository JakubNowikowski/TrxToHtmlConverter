﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrxToHtmlConverter;

namespace WPFApplication
{
    class ApplicationModel
    {
        //public string filePath = @"C:\Users\OptiNav\source\repos\MSTest.Net Framework\UnitTests\bin\Debug\UnitTests.dll";
        public string filePath;
        public string testCategory;
        public bool isTestCategorySearchChecked = false;
        public string vsTestConsolePath = @"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\IDE\Extensions\TestPlatform\vstest.console.exe";
        public string changeSetNumber;
        public string pbiNumber;
        public string outputPath;
        public string enableToConvert = "false";
        public string result = "";
        public bool enableToOpen = false;
        public bool testCategoryEnable = false;

        public string OpenFileDialog(string extension)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            switch (extension)
            {
                case "exe":
                    dlg.DefaultExt = ".exe";
                    dlg.Filter = "EXE Files (*.exe)|*.exe";
                    break;
                case "dll":
                    dlg.DefaultExt = ".dll";
                    dlg.Filter = "DLL Files (*.dll)|*.dll";
                    break;
                case "trx":
                    dlg.DefaultExt = ".trx";
                    dlg.Filter = "TRX Files (*.trx)|*.trx";
                    break;
            }

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

        //Constr with all params or each method individually with requaire param
        public void Convert()
        {
            HtmlGeneration Html = new HtmlGeneration(outputPath, filePath, pbiNumber, changeSetNumber);
            Html.CreateTrxFile(vsTestConsolePath);
            Html.InitializeTrxData();
            Html.Generation();
        }
    }
}
