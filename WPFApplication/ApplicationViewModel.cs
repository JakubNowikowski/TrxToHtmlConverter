﻿using System;
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

        public string InputPath
        {
            set => SetProperty(ref app.inputPath, value);
            get => app.inputPath;
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

        public ApplicationViewModel()
        {
            SearchCommand = new RelayCommand((obj) => { InputPath = app.OpenFileDialog(); });
            ConvertCommand = new RelayCommand((obj) => { OutputPath = InputPath.Replace(".trx", ".html"); app.Convert(); Result = "Exported file to:\n" + OutputPath; });
        }

    }
}