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
    public class SeriesListViewModel :ViewModelBase
    {
        private ServiceApiSeries service;
        public SeriesListViewModel(ServiceApiSeries service)
        {
            this.service = service;
            Task.Run(async () =>
            {
                await this.LoadSeries();
            });

        }
        private async Task LoadSeries()
        {
            List<Serie> series =
                await this.service.GetSeriesAsync();
            this.Series = new ObservableCollection<Serie> (series);
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
        public Command ShowModificar
        {
            get
            {
                return new Command(async () =>
                {
                    ModificarView view = new ModificarView();
                    ModificarPersonajeViewModel viewmodel =
                    App.ServiceLocator.ModificarPersonajeViewModel;
                    view.BindingContext = viewmodel;
                    await
                    Application.Current.MainPage.Navigation.PushModalAsync(view);
                });
            }
        }
        public Command ShowDetails
        {
            get
            {
                return new Command(async (serier) =>
                {
                    Serie serie = serier as Serie;
                    SerieView view = new SerieView();
                    SerieViewModel viewmodel =
                    App.ServiceLocator.SerieViewModel;
                    viewmodel.serie = serie;
                    viewmodel.Personajes =
                     new ObservableCollection<Personaje>( await this.service.GetPersonajesSerieAsync(serie.idSerie));
                    view.BindingContext = viewmodel;
                    await
                    Application.Current.MainPage.Navigation.PushModalAsync(view);

                });
            }
        }



    }
}
