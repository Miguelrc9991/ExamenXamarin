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
    public class ModificarPersonajeViewModel : ViewModelBase
    {
        private ServiceApiSeries service;
        public ModificarPersonajeViewModel(ServiceApiSeries service)
        {
            this.service = service;
            Task.Run(async () =>
            {
                await this.LoadSeries();
                await this.LoadPersonajes();
            });

        }
        private async Task LoadSeries()
        {
            List<Serie> series =
                await this.service.GetSeriesAsync();
            List<string> tseries = new List<string>();
            foreach(Serie serie in series)
            {
                tseries.Add(serie.nombre);
            }
            this.nseries = new ObservableCollection<string>(tseries);
        }
        private async Task LoadPersonajes()
        {
            List<Personaje> personajes =
                await this.service.GetPersonajesAsync();
            List<string> tpersonajes = new List<string>();
            foreach (Personaje personaje in personajes)
            {
                tpersonajes.Add(personaje.nombre);
            }
            this.npersonajes = new ObservableCollection<string>(tpersonajes);
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
        private int  _idserie;

        public int  idserie
        {
            get { return this._idserie; }
            set
            {
                this._idserie = value;
                OnPropertyChanged("idserie");
            }
        }
        private int _idpersonaje;

        public int idpersonaje
        {
            get { return this._idpersonaje; }
            set
            {
                this._idpersonaje = value;
                OnPropertyChanged("idpersonaje");
            }
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
        private ObservableCollection<Personaje> _Personajes;

        public ObservableCollection<Personaje> Personajes
        {
            get { return this._Personajes; }
            set
            {
                this._Personajes = value;
                OnPropertyChanged("Personajes");
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

        private ObservableCollection<string> _nseries;
        public ObservableCollection<string> nseries
        {
            get { return this._nseries; }
            set
            {
                this._nseries = value;
                OnPropertyChanged("nseries");
            }
        }
        private ObservableCollection<string> _npersonajes;
        public ObservableCollection<string> npersonajes
        {
            get { return this._npersonajes; }
            set
            {
                this._npersonajes = value;
                OnPropertyChanged("npersonajes");
            }
        }
        public Command GuardarCambios
        {
            get
            {
                return new Command(async () =>
                {
                    await this.service.UpdatePersonaje(this.idpersonaje+1, this.idserie+1);
                   
                    SeriesView view = new SeriesView();
                    SeriesListViewModel viewmodel =
                    App.ServiceLocator.SeriesListViewModel;
                    view.BindingContext = viewmodel;
                    await
                    Application.Current.MainPage.Navigation.PushModalAsync(view);
                });
            }
        }
    }
}
