using MakingOurStory_Brielina.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MakingOurStory_Brielina.Controllers
{
    public class FragmentosController : Controller
    {
        [Route("Fragmentos/Index/{id}")]
        public ActionResult Index([FromRoute] int id)
        {
            return View(new fragmentoHistoria
            {
                IdFragmentoHistoria = id,
                TituloFragmento = "Titulo",
                TextoFragmento = "Texto",
                Segmentos = new List<fragmentoHistoria>
                {
                    new fragmentoHistoria
                    {
                        IdFragmentoHistoria = id + 1,
                        TituloFragmento = $"Titulo {id + 1}",
                        TextoFragmento = $"Texto {id + 1}",
                    },
                    new fragmentoHistoria
                    {
                        IdFragmentoHistoria = id + 2,
                        TituloFragmento = $"Titulo {id + 2}",
                        TextoFragmento = $"Texto {id + 2}",
                    }
                }
            });
        }

        [Route("Fragmentos/Sequencia/{id}")]
        public ActionResult Sequencia([FromRoute]int id)
        {
            fragmentoHistoria fragmento = new fragmentoHistoria { IdFragmentoHistoria = id };

            fragmentoHistoria resposta = coletaApi(id);

            fragmento.TituloFragmento = resposta.TituloFragmento;
            fragmento.TextoFragmento = resposta.TextoFragmento;        
            fragmento.Segmentos = new List<fragmentoHistoria>();

            foreach (var segmento in resposta.Segmentos)
            {
                fragmento.Segmentos.Add(segmento);
            }

            return View(fragmento);
        }

        [Route("Fragmentos/Novo/{id}")]
        public ActionResult Novo([FromRoute] int id)
        {
            fragmentoHistoria fragmento = new fragmentoHistoria { IdFragmentoHistoria = id };
            fragmento.obtemInfos(id);

            return View(fragmento);
        }

        public fragmentoHistoria coletaApi(int id)
        {
            fragmentoHistoria resposta = new fragmentoHistoria { IdFragmentoHistoria = id };
            resposta.obtemInfos(id);
            resposta.obtemSegmentos(id);


            return resposta;
        }


        [HttpPost]
        [Route("Fragmentos/Novo/novoFragmento")]
        public void PostComplex(fragmentoHistoria novo)
        {
            novo.gravarnovo(novo);
            var x = novo;
        }
    }
}
