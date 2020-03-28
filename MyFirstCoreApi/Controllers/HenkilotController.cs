using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyFirstCoreApi.Controllers
{
    [Route("omareitti/[controller]")]
    [ApiController]
    public class HenkilotController : ControllerBase
    {
        //   [HttpGet] 
        //   [Route ("merkkijono")]
        //   public string Merkkijono()
        //    {
        //        return "Päivää maailma";
        //    }

        //    [HttpGet]
        //    [Route("paivamaara")]
        //    public DateTime Pvm()
        //    {
        //        return DateTime.Now;
        //    }

        //    [HttpGet]
        //    [Route("olio")]
        //    public Henkilo Olio()
        //    {
        //        return new Henkilo()
        //        {
        //            Nimi = "Paavo Pesusieni",
        //            Osoite = "Vesipolku 11",
        //            Ika = 11
        //        };
        //    }

        //    [HttpGet]
        //    [Route("olioLista")]
        //    public List<Henkilo> OlioLista()
        //    {
        //        List<Henkilo> henkilot = new List<Henkilo>()
        //        {
        //       new Henkilo()
        //        {
        //            Nimi = "Paavo Pesusieni",
        //            Osoite = "Vesipolku 11",
        //            Ika = 11
        //        },
        //        new Henkilo()
        //        {
        //            Nimi = "Liisa Pesusieni",
        //            Osoite = "Vesipolku 11",
        //            Ika = 15
        //        },
        //        new Henkilo()
        //        {
        //            Nimi = "Kaisa Pesusieni",
        //            Osoite = "Vesipolku 11",
        //            Ika = 12
        //        }

        //        };

        //          return henkilot;

        //    }
    }
}
