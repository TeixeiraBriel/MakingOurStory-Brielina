using MakingOurStory_Brielina.Models;
using RestSharp;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MakingOurStory_Brielina.Controllers
{
    public class FragmentosController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        [Route("Fragmentos/Index/{id}")]
        public ActionResult Index(int id)
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
        public ActionResult Sequencia(int id)
        {
            fragmentoHistoria fragmento = new fragmentoHistoria { IdFragmentoHistoria = id };
            fragmento = coletaApi(fragmento); 

            return View(fragmento);
        }

        [Route("Fragmentos/Novo/{id}")]
        public ActionResult Novo(int id)
        {
            fragmentoHistoria fragmento = new fragmentoHistoria { IdFragmentoHistoria = id };
            fragmento.obtemInfos(id);

            return View(fragmento);
        }

        public fragmentoHistoria coletaApi(fragmentoHistoria fragBase)
        {
            int id = fragBase.IdFragmentoHistoria;
            fragmentoHistoria resposta = new fragmentoHistoria { IdFragmentoHistoria = id };
            resposta.obtemInfos(id);
            resposta.obtemSegmentos(id);

            return resposta;
        }


        [HttpPost]
        [Route("Fragmentos/GravarFragmento/")]
        public ActionResult GravarFragmento(fragmentoHistoria novo)
        {
            novo.gravarnovo(novo);

            fragmentoHistoria fragmentoPai = new fragmentoHistoria { IdFragmentoHistoria = novo.IdFragmentoParent };
            fragmentoPai = coletaApi(fragmentoPai);

            return View("Sequencia", fragmentoPai);
        }
    }
}
