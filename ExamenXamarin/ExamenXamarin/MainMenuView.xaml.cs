using ExamenXamarin.Code;
using ExamenXamarin.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExamenXamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainDepartamentosView : MasterDetailPage
    {
        public MainDepartamentosView()
        {
            InitializeComponent();
            ObservableCollection<MenuPageItem> menu =
                new ObservableCollection<MenuPageItem>
                {
new MenuPageItem{
    Titulo = "Menu"
    , TypePage=typeof(SeriesView)},
new MenuPageItem{
    Titulo = "Nuevo Personaje"
    , TypePage=typeof(NuevoPersonajeView)},
new MenuPageItem{
    Titulo = " Modigicar Personajes Series"
    , TypePage=typeof(ModificarView)}


                };
            this.lsvMenu.ItemsSource = menu;
            Detail =
                new NavigationPage
                ((Page)Activator.CreateInstance(typeof(MainPage)));
            this.lsvMenu.ItemSelected += LsvMenu_ItemSelected;
        }

        private void LsvMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            MenuPageItem item = e.SelectedItem as MenuPageItem;
            Detail =
                new NavigationPage((Page)Activator.CreateInstance(item.TypePage));
            IsPresented = false;
        }
    }
}