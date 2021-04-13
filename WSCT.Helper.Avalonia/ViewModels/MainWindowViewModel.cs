using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WSCT.Helpers;
using WSCT.Helpers.BasicEncodingRules;

namespace WSCT.Helper.Avalonia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _sourceData;
        public string Greeting => "Welcome to Avalonia!";

        public ObservableCollection<TlvData> TlvDataList { get; set; } = new();

        public string SourceData
        {
            get => _sourceData;
            set
            {
                _sourceData = value;
                try
                {
                    TlvDataList.Clear();
                    TlvDataList.Add(new TlvData(SourceData.FromHexa()));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public MainWindowViewModel()
        {
            SourceData = "880102";

            TlvDataList.Add(new TlvData("6F0488023132".FromHexa()));
            TlvDataList.Add(new TlvData("880102".FromHexa()));
        }
    }
}
