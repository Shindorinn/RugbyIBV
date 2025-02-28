﻿using INFOIBV.Presentation;

using System.Windows;

namespace INFOIBV
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            //new FilterSettingsWindow() { DataContext = new FilterSettingsViewModel() }.Show();
            new MainWindow() { DataContext = new MainViewModel() }.Show();
        }
    }
}