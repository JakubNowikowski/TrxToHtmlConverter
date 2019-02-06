using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WPFApplication
{
    class ApplicationViewModel : ViewModelBase
    {
        static ApplicationModel app = new ApplicationModel();
        public ICommand SearchCommand { private set; get; }
        public ICommand ConvertCommand { private set; get; }
        public ICommand OpenCommand { private set; get; }

        public string InputPath
        {
            set => SetProperty(ref app.inputPath, value);
            get => app.inputPath;
        }

        public string ChangeSetNumber
        {
            set => SetProperty(ref app.changeSetNumber, value);
            get => app.changeSetNumber;
        }

        public string PbiNumber
        {
            set => SetProperty(ref app.pbiNumber, value);
            get => app.pbiNumber;
        }

        public string OutputPath
        {
            set => SetProperty(ref app.outputPath, value);
            get => app.outputPath;
        }

        public string Result
        {
            private set => SetProperty(ref app.result, value);
            get => app.result;
        }

        public string EnableToOpen
        {
            private set => SetProperty(ref app.enableToOpen, value);
            get => app.enableToOpen;
        }

        void OpenHtmlFile()
        {
            System.Diagnostics.Process.Start(OutputPath);
        }

        public ApplicationViewModel()
        {
            SearchCommand = new RelayCommand((obj) => { InputPath = app.OpenFileDialog(); });
            ConvertCommand = new RelayCommand((obj) =>
            {
                if (InputPath != null)
                {
                    OutputPath = InputPath.Replace(".trx", ".html");
                    app.Convert();
                    Result = "Exported file to:\n" + OutputPath;
                    EnableToOpen = "true";
                }
            });
            OpenCommand = new RelayCommand((obj) => { OpenHtmlFile(); });
        }
    }
}
