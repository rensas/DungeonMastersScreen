/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:DungeonMastersScreen"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using DungeonMastersScreen.Service.Implementation;
using DungeonMastersScreen.Service.Interface;
using DungeonMastersScreen.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using System;

namespace DungeonMastersScreen.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {

        public const string MainPageKey = nameof(MainWindow);
        public const string CombatTrackerPageKey = nameof(CombatTrackerPage);
        public const string PlayerCharacterManagerPageKey = nameof(PlayerCharacterManagerPage);


        public CombatTrackerViewModel CombatTrackerVM => SimpleIoc.Default.GetInstance<CombatTrackerViewModel>();
        public PlayerCharacterManagerViewModel PlayerCharManagerVM => SimpleIoc.Default.GetInstance<PlayerCharacterManagerViewModel>();
        public MainViewModel Main => SimpleIoc.Default.GetInstance<MainViewModel>();
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SetupNavigation();

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<CombatTrackerViewModel>();
            SimpleIoc.Default.Register<PlayerCharacterManagerViewModel>();

            SimpleIoc.Default.Register<ILocalStorageService, LocalFileStorageService>();
        }
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }

        private static void SetupNavigation()
        {
            //var navigationService = new FrameNavigationService();
            
        }
    }
}