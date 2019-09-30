using DungeonMastersScreen.Service.Implementation;
using DungeonMastersScreen.View;
using DungeonMastersScreen.ViewModel;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DungeonMastersScreen
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        readonly FrameNavigationService _navigationService;
        static ViewModelLocator _locator;
        public static ViewModelLocator Locator => _locator ?? (_locator = new ViewModelLocator());

        public App()
        {

            this.InitializeComponent();

            if (!SimpleIoc.Default.IsRegistered<INavigationService>())
            {
                _navigationService = new FrameNavigationService();
                _navigationService.Configure(ViewModelLocator.CombatTrackerPageKey, new Uri("./View/CombatTrackerPage.xaml", UriKind.Relative));


                SimpleIoc.Default.Register<INavigationService>(() => _navigationService);
            }
            else
            {
                _navigationService = (FrameNavigationService)SimpleIoc.Default.GetInstance<INavigationService>();
            }
        }
    }
}
