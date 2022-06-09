using ExamenXamarin.Services;
using ExamenXamarin.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExamenXamarin
{
    public partial class App : Application
    {
        private static ServiceIoC _ServiceLocator;

        public static ServiceIoC ServiceLocator
        {
            get
            {
                return _ServiceLocator = _ServiceLocator ?? new ServiceIoC();
            }
        }

        public App()
        {
            InitializeComponent();

            MainDepartamentosView main = App.ServiceLocator.MainDepartamentosView;
            main.Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(SeriesView)));

            //MainPage = App.ServiceLocator.MainMenuView;

            MainPage = main;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
