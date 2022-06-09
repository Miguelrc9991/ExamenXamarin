using ExamenXamarin.Base;
using ExamenXamarin.Models;
using ExamenXamarin.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ExamenXamarin.ViewModels
{
    public class SerieViewModel : ViewModelBase
    {
        private ServiceApiSeries service;
        public SerieViewModel(ServiceApiSeries service)
        {
            this.service = service;
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
    }
}
