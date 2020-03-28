using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFirstCoreApi.Models;

namespace MyFirstCoreApi.Controllers
{
    [Route("nw/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public List<Logins> GetAllLogins()
        {
            NorthwindContext db = new NorthwindContext();
            List<Logins> tunnukset = db.Logins.ToList();
            return tunnukset;
        }

        [HttpGet]
        [Route("{id}")]
        public Logins GetOneLogin(int id)
        {
            NorthwindContext db = new NorthwindContext();
            Logins tunnus = db.Logins.Find(id);
            return tunnus;
        }

        [HttpGet]
        [Route("lastname/{key}")]
        public List<Logins> GetMeSomeLogins(string key)
        {
            NorthwindContext db = new NorthwindContext();

            var SomeLogins = from c in db.Logins
                                where c.Lastname == key
                                select c;

            return SomeLogins.ToList();
        }

        [HttpPost] //<--filtteri rajoittaa alapuolella olevan metodin vain POST-pyyntöihin (uuden asian luominen tai lisääminen)
        [Route("")] //<--tyhjä reitinmääritys (ei ole pakko laittaa), eli ei mitään lisättävää reittiin, jolloin
        public ActionResult PostCreateNew([FromBody] Logins tunnus) //<-- [FromBody] tarkoittaa, että HTTP-pyynnön Body:ssä välitetään JSON-muodossa oleva objekti ,joka om Documentation-tyyppinen Login-niminen
        {
            NorthwindContext db = new NorthwindContext();
            try
            {


                db.Logins.Add(tunnus);
                db.SaveChanges();

                return Ok(tunnus.LoginId);
            }

            catch (Exception)
            {
                return BadRequest("Jokin meni pieleen tunnusta lisättäessä");
            }

            finally
            {
                db.Dispose();
            }

            /*return doku.DocumentationId.ToString; //k*//*uittaus Frontille, että päivitys meni oikein --> Frontti voi tsekata, että kontrolleri palauttaa saman id:n mitä käsitteli*/
        }
        [HttpPut] //<-- filtteri, joka sallii vain PUT-metodit (Http-verbit)
        [Route("{key}")] //<-- Routemääritys tunnusavaimelle key=CustomerID
        public ActionResult PutEdit(int key, [FromBody] Logins tunnus) //<-- [FromBody] tarkoittaa, että HTTP-pyynnön Body:ssä välitetään JSON-muodossa oleva objekti ,joka on Logins-tyyppinen tunnus-niminen
        {
            NorthwindContext db = new NorthwindContext(); //Context = Kuten entities muodostettu Scaffold DBContext -työkalulla. Voisi olla myös entiteetti frameworkCore
            try
            {
                Logins login = db.Logins.Find(key);
                if (login != null)
                {
                    login.Firstname = tunnus.Firstname;
                    login.Lastname = tunnus.Lastname;
                    login.Email = tunnus.Email;
                    login.Password = tunnus.Password;
                    db.SaveChanges();
                    return Ok(login.LoginId);
                }

                else
                {
                    return NotFound("Päivitettävää tunnusta ei löytynyt!");
                }
            }
            catch (Exception)
            {
                return BadRequest("Jokin meni pieleen tunnusta lpäivittäessä");
            }

            finally
            {
                db.Dispose();
            }
        }
        [HttpDelete]
        [Route("{key}")]
        public ActionResult DeleteLogin(int key) //
        {
            NorthwindContext db = new NorthwindContext();
            Logins tunnus = db.Logins.Find(key);
            if (tunnus != null)
            {
                db.Logins.Remove(tunnus);
                db.SaveChanges();
                return Ok("tunnus " + key + " poistettiin");
            }
            else
            {
                return NotFound("tunnusta " + key + " ei löydy");
            }

        }

        [HttpGet]
        [Route("R")]

        public IActionResult GetSomeLogins(int offset, int limit, string lastname)
        {
            if (lastname != null)
            {
                NorthwindContext db = new NorthwindContext();
                List<Logins> tunnukset = db.Logins.Where(d => d.Lastname == lastname).ToList();
                return Ok(tunnukset);
            }

            else
            {
                NorthwindContext db = new NorthwindContext();
                List<Logins> tunnukset = db.Logins.Skip(offset).Take(limit).ToList();
                return Ok(tunnukset);
            }

        }
    }
}