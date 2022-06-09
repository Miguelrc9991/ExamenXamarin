using ExamenXamarin.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ExamenXamarin.Services
{
    public class ServiceApiSeries
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue Header;

        public ServiceApiSeries
        (IConfiguration configuration)
        {
            this.UrlApi = configuration["ApiUrls:ApiSeries"];
            this.Header =
            new MediaTypeWithQualityHeaderValue("application/json");
        }
        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Uri uri = new Uri(this.UrlApi + request);
                HttpResponseMessage response =
                await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Serie>> GetSeriesAsync()
        {
            string request = "/api/Series";
            List<Serie> series =
            await this.CallApiAsync<List<Serie>>(request);
            return series;
        }

        public async Task<Serie> FindSerieAsync(int idserie)
        {
            string request = "/api/Series/" + idserie;
            Serie serie =
            await this.CallApiAsync<Serie>(request);
            return serie;
        }
        public async Task<Personaje> FindPersonaje(int id)
        {
            string request = "/api/Personajes/" + id;
            Personaje perso =
            await this.CallApiAsync<Personaje>(request);
            return perso;
        }

        public async Task<List<Personaje>> GetPersonajesAsync()
        {
            string request = "/api/Personajes";
            List<Personaje> personajes =
            await this.CallApiAsync<List<Personaje>>(request);
            return personajes;
        }

        public async Task<List<Personaje>> GetPersonajesSerieAsync(int idserie)
        {
            string request = "/api/Series/PersonajesSerie/"+idserie;
            List<Personaje> personajesSerie =
            await this.CallApiAsync<List<Personaje>>(request);
            return personajesSerie;
        }

        private async Task<int> GetMaxIdPersonaje()
        {
            List<Personaje> personajes =
            await this.GetPersonajesAsync();
            return personajes.Max(x => x.idPersonaje) + 1;
        }

        public async Task InserPersonaje(int idpersonaje,string nombre, string imagen,int idserie)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Personaje perso = new Personaje
                {
                    idPersonaje = await this.GetMaxIdPersonaje(),
                    nombre = nombre,
                    imagen = imagen,
                    idSerie=idserie
                    
                };
                string json = JsonConvert.SerializeObject(perso);
                StringContent content =
                new StringContent(json, Encoding.UTF8, "application/json");
                string request = "/api/Personajes";
                Uri uri = new Uri(this.UrlApi + request);
                await client.PostAsync(uri, content);
            }
        }

        public async Task UpdatePersonaje(int idpersonaje,int idserie)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Personaje personaje = await this.FindPersonaje(idpersonaje);
                personaje.idSerie = idserie;
                string json = JsonConvert.SerializeObject(personaje );
                StringContent content =
                new StringContent(json, Encoding.UTF8, "application/json");
                string request = "/api/Personajes";
                Uri uri = new Uri(this.UrlApi + request);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                await client.PutAsync(uri, content);
            }
        }




    }
}
