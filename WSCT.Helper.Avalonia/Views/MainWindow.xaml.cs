using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using WSCT.Helper.Avalonia.ViewModels;
using WSCT.Helpers;
using WSCT.Helpers.BasicEncodingRules;

namespace WSCT.Helper.Avalonia.Views
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void QuitTheApplicationClick(object? sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnTextInput(object? sender, TextInputEventArgs e)
        {
            var viewModel = DataContext as MainWindowViewModel;
            if (viewModel == null)
            {
                throw new NullReferenceException("viewModel");
            }

            try
            {
                viewModel.TlvDataList.Clear();
                viewModel.TlvDataList.Add(new TlvData(e.Text.FromHexa()));
            }
            catch (Exception)
            {
            }
        }
    }
}
