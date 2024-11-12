﻿using System.Windows;
using Dark.Net;
using Fluxor;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using MudBlazor.Services;

namespace Fuyu.Launcher
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DarkNet.Instance.SetWindowThemeWpf(this, Theme.Dark);

            var services = new ServiceCollection();
            services.AddWpfBlazorWebView();
            services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
            });

            var currentAssembly = typeof(MainWindow).Assembly;
            services.AddFluxor(options => options.ScanAssemblies(currentAssembly));

            services.AddBlazorWebViewDeveloperTools();
            Resources.Add("services", services.BuildServiceProvider());
        }
    }
}
