using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MakingOurStory_Brielina.Models
{
    public class fragmentoHistoria
    {
        private fragmentoHistoria fragmento { get; set; }

        public int IdFragmentoHistoria { get; set; }
        public string TituloFragmento { get; set; }
        public string TextoFragmento { get; set; }
        public int IdFragmentoParent { get; set; } = 0;

        public List<fragmentoHistoria> Segmentos { get; set; }

        public void obtemSegmentos(int parent)
        {
            List<fragmentoHistoria> _Segmentos = new List<fragmentoHistoria>();
            List<fragmentoHistoria> SegmentosAll = new List<fragmentoHistoria>();
            string _UrlBrielinaApi = "http://25.82.74.248:8080/api/";

            using (HttpClient cliente = new HttpClient())
            {
                var response = cliente.GetAsync("http://25.82.74.248:8080/api/RpgCSharp/").Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    SegmentosAll = JsonConvert.DeserializeObject<List<fragmentoHistoria>>(result);                    
                }
            }
            foreach (var segmento in SegmentosAll)
            {
                if (segmento.IdFragmentoParent == parent)
                {
                    _Segmentos.Add(segmento);
                }
            }
            Segmentos = _Segmentos;
        }

        public void obtemInfos(int id)
        {
            fragmento = new fragmentoHistoria();

            string _UrlBrielinaApi = "http://25.82.74.248:8080/api/RpgCSharp/" + id;

            using (HttpClient cliente = new HttpClient())
            {
                var response = cliente.GetAsync(_UrlBrielinaApi).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    fragmento = JsonConvert.DeserializeObject<fragmentoHistoria>(result);

                    TituloFragmento = fragmento.TituloFragmento;
                    TextoFragmento = fragmento.TextoFragmento;
                    IdFragmentoParent = fragmento.IdFragmentoParent;
                }
            }
        }

        public void gravarnovo(fragmentoHistoria novo)
        {
            string _UrlBrielinaApi = "http://25.82.74.248:8080/api/RpgCSharp/";
            var teste = JsonConvert.SerializeObject(novo);

            using (HttpClient cliente = new HttpClient())
            {
                var response = cliente.GetAsync(_UrlBrielinaApi).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    fragmento = JsonConvert.DeserializeObject<fragmentoHistoria>(result);

                    TituloFragmento = fragmento.TituloFragmento;
                    TextoFragmento = fragmento.TextoFragmento;
                    IdFragmentoParent = fragmento.IdFragmentoParent;
                }
            }
        }
    }
}
