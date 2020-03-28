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
    public class CustomerController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public List<Customers> GetAllCustomers()
        {
            NorthwindContext db = new NorthwindContext();
            List<Customers> asiakkaat = db.Customers.ToList();
            return asiakkaat;
        }


        [HttpGet]
        [Route("{id}")]
        public Customers GetOneCustomer(string id)
        {
            NorthwindContext db = new NorthwindContext();
            Customers asiakas = db.Customers.Find(id);
            return asiakas;
        }

        [HttpGet]
        [Route("country/{key}")]
        public List<Customers> GetMeSomeCustomers(string key)
        {
            NorthwindContext db = new NorthwindContext();

            var SomeCustomers = from c in db.Customers
                                where c.Country == key
                                select c;

            return SomeCustomers.ToList();
        }

        [HttpPost] //<--filtteri rajoittaa alapuolella olevan metodin vain POST-pyyntöihin (uuden asian luominen tai lisääminen)
        [Route("")] //<--tyhjä reitinmääritys (ei ole pakko laittaa), eli ei mitään lisättävää reittiin, jolloin
        public ActionResult PostCreateNew([FromBody] Customers asiakas) //<-- [FromBody] tarkoittaa, että HTTP-pyynnön Body:ssä välitetään JSON-muodossa oleva objekti ,joka om Documentation-tyyppinen customer-niminen
        {
            NorthwindContext db = new NorthwindContext();
            try
            {


                db.Customers.Add(asiakas);
                db.SaveChanges();

                return Ok(asiakas.CustomerId);
            }

            catch (Exception)
            {
                return BadRequest("Jokin meni pieleen asiakasta lisättäessä");
            }

            finally
            {
                db.Dispose();
            }

            /*return doku.DocumentationId.ToString; //k*//*uittaus Frontille, että päivitys meni oikein --> Frontti voi tsekata, että kontrolleri palauttaa saman id:n mitä käsitteli*/
        }
        [HttpPut] //<-- filtteri, joka sallii vain PUT-metodit (Http-verbit)
        [Route("{key}")] //<-- Routemääritys asiakasavaimelle key=CustomerID
        public ActionResult PutEdit(string key, [FromBody] Customers asiakas) //<-- [FromBody] tarkoittaa, että HTTP-pyynnön Body:ssä välitetään JSON-muodossa oleva objekti ,joka on Customers-tyyppinen asiakas-niminen
        {
            NorthwindContext db = new NorthwindContext(); //Context = Kuten entities muodostettu Scaffold DBContext -työkalulla. Voisi olla myös entiteetti frameworkCore
            try
            {
                Customers customer = db.Customers.Find(key);
                if (customer != null)
                {
                    customer.CompanyName = asiakas.CompanyName;
                    customer.ContactName = asiakas.ContactName;
                    customer.ContactTitle = asiakas.ContactTitle;
                    customer.Country = asiakas.Country;
                    customer.Address = asiakas.Address;
                    customer.City = asiakas.City;
                    customer.PostalCode = asiakas.PostalCode;
                    customer.Phone = asiakas.Phone;

                    db.SaveChanges();
                    return Ok(customer.CustomerId);
                }

                else
                {
                    return NotFound("Päivitettävää asiakasta ei löytynyt!");
                }
            }
            catch (Exception)
            {
                return BadRequest("Jokin meni pieleen asiakasta päivittäessä");
            }

            finally
            {
                db.Dispose();
            }
        }
        [HttpDelete]
        [Route("{key}")]
        public ActionResult DeleteCustomer(string key) //
        {
            NorthwindContext db = new NorthwindContext();
            Customers asiakas = db.Customers.Find(key);
            if (asiakas != null)
            {
                db.Customers.Remove(asiakas);
                db.SaveChanges();
                return Ok("Asiakas " + key + " poistettiin");
            }
            else
            {
                return NotFound("Asiakasta " + key + " ei löydy");
            }

        }

        [HttpGet]
        [Route("R")]

        public IActionResult GetSomeCustomers(int offset, int limit, string country)
        {
            if (country != null)
            {
                NorthwindContext db = new NorthwindContext();
                List<Customers> asiakkaat = db.Customers.Where(d => d.Country == country).ToList();
                return Ok(asiakkaat);
            }

            else
            {
                NorthwindContext db = new NorthwindContext();
                List<Customers> asiakkaat = db.Customers.Skip(offset).Take(limit).ToList();
                return Ok(asiakkaat);
            }

        }
    }
}