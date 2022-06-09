using ExamenXamarin.Base;
using ExamenXamarin.Models;
using ExamenXamarin.Services;
using ExamenXamarin.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExamenXamarin.ViewModels
{
    public class NuevoPersonaje :ViewModelBase
    {
        private ServiceApiSeries service;
        public NuevoPersonaje(ServiceApiSeries service)
        {
            this.service = service;
            this.personaje = new Personaje();
            Task.Run(async () =>
            {
                await this.LoadSeries();
            });

        }

        private async Task LoadSeries()
        {
            List<Serie> series =
                await this.service.GetSeriesAsync();
            this.Series = new ObservableCollection<Serie>(series);
        }
        private Personaje _personaje;

        public Personaje personaje
        {
            get { return this._personaje; }
            set
            {
                this._personaje = value;
                OnPropertyChanged("personaje");
            }
        }
        private ObservableCollection<Serie> _Series;

        public ObservableCollection<Serie> Series
        {
            get { return this._Series; }
            set
            {
                this._Series = value;
                OnPropertyChanged("Series");
            }
        }
        private Serie _serie;

        public Serie serie
        {
            get { return this._serie; }
            set
            {
                this._serie = value;
                OnPropertyChanged("serie");
            }
        }
        public Command NuevoPersoanjeCom
        {
            get
            {
                return new Command(async () =>
                {
                    int id = await this.service.GetMaxIdPersonaje();
                    await this.service.InserPersonaje(id,personaje.nombre,personaje.imagen,serie.idSerie);

                    SeriesView view = new SeriesView();
                    SeriesListViewModel viewmodel =
                    App.ServiceLocator.SeriesListViewModel;
                    view.BindingContext = viewmodel;
                    MainDepartamentosView main = App.ServiceLocator.MainDepartamentosView;
                    main.Detail = new NavigationPage(view);
                });
            }
        }
    }
}
