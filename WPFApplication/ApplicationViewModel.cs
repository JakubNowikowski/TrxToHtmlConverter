using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFApplication
{
    class ApplicationViewModel : ViewModelBase
    {
        ApplicationModel app = new ApplicationModel();
        public ICommand SearchCommand { private set; get; }
        public ICommand ConvertCommand { private set; get; }

        public string InputPath
        {
            set => SetProperty(ref app.inputPath, value);
            private get => app.inputPath;
        }
        public string OutputPath
        {
            private set => SetProperty(ref app.outputPath, value);
            get => app.outputPath;
        }
        public string Result
        {
            private set => SetProperty(ref app.result, value);
            get => app.result;
        }

        public ApplicationViewModel()
        {
            SearchCommand = new RelayCommand((obj) => { InputPath = app.OpenFileDialog(); OutputPath = InputPath.Replace(".trx",".html"); });
            ConvertCommand = new RelayCommand((obj) => { app.Convert(); Result = "Wyeksportowano plik do :\n" + OutputPath; });
        }
    }
}
