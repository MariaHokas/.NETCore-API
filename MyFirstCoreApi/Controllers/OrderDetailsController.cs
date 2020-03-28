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
    public class OrderDetailsController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public List<OrderDetails> GetAllOrderDetails()
        {
            NorthwindContext db = new NorthwindContext();
            List<OrderDetails> dtilaus = db.OrderDetails.ToList();
            return dtilaus;
        }

        [HttpGet]
        [Route("{id}")]
        public List<OrderDetails> GetOneOrderDetails(int id)
        {
            NorthwindContext db = new NorthwindContext();

            var SomeOrderDetails = from c in db.OrderDetails
                                   where c.OrderId == id
                                   select c;

            return SomeOrderDetails.ToList();
        }

        [HttpGet]
        [Route("OrderId/{key}/{key2}")]
        public List<OrderDetails> GetSomeOrderDetails(int key, int key2)
        {
            NorthwindContext db = new NorthwindContext();

            var SomeOrderDetails = from c in db.OrderDetails
                             where c.OrderId == key && c.ProductId == key2
                             select c;

            return SomeOrderDetails.ToList();
        }

        [HttpPost] //<--filtteri rajoittaa alapuolella olevan metodin vain POST-pyyntöihin (uuden asian luominen tai lisääminen)
        [Route("")] //<--tyhjä reitinmääritys (ei ole pakko laittaa), eli ei mitään lisättävää reittiin, jolloin
        public ActionResult PostCreateNew([FromBody] OrderDetails dtilaus) //<-- [FromBody] tarkoittaa, että HTTP-pyynnön Body:ssä välitetään JSON-muodossa oleva objekti ,joka om Documentation-tyyppinen customer-niminen
        {
            NorthwindContext db = new NorthwindContext();
            try
            {


                db.OrderDetails.Add(dtilaus);
                db.SaveChanges();

                return Ok(dtilaus.OrderId);
            }

            catch (Exception)
            {
                return BadRequest("Jokin meni pieleen tilauksen tietoja lisättäessä");
            }

            finally
            {
                db.Dispose();
            }

            /*return doku.DocumentationId.ToString; //k*//*uittaus Frontille, että päivitys meni oikein --> Frontti voi tsekata, että kontrolleri palauttaa saman id:n mitä käsitteli*/
        }
        [HttpPut] //<-- filtteri, joka sallii vain PUT-metodit (Http-verbit)
        [Route("{key}")] //<-- Routemääritys asiakasavaimelle key=CustomerID
        public ActionResult PutEdit(int key, [FromBody] OrderDetails dtilaus) //<-- [FromBody] tarkoittaa, että HTTP-pyynnön Body:ssä välitetään JSON-muodossa oleva objekti ,joka on Customers-tyyppinen asiakas-niminen
        {
            NorthwindContext db = new NorthwindContext(); //Context = Kuten entities muodostettu Scaffold DBContext -työkalulla. Voisi olla myös entiteetti frameworkCore
            try
            {
                OrderDetails order = db.OrderDetails.Find(key);
                if (order != null)
                {
                    order.ProductId = dtilaus.ProductId;
                    order.UnitPrice = dtilaus.UnitPrice;
                    order.Quantity = dtilaus.Quantity;
                    order.Discount = dtilaus.Discount;
                    db.SaveChanges();
                    return Ok(order.OrderId);
                }

                else
                {
                    return NotFound("Päivitettävää tilausta ei löytynyt!");
                }
            }
            catch (Exception)
            {
                return BadRequest("Jokin meni pieleen atilausta päivittäessä");
            }

            finally
            {
                db.Dispose();
            }
        }
        [HttpDelete]
        [Route("{key}/{key2}")]
        public ActionResult DeleteOrder(int key, int key2) //
        {
            NorthwindContext db = new NorthwindContext();
            OrderDetails dtilaus = db.OrderDetails.Find(key, key2);

            if (dtilaus != null)
            {
                db.OrderDetails.Remove(dtilaus);
                db.SaveChanges();
                return Ok("Tilaus " + key + " poistettiin");
            }
            else
            {
                return NotFound("Tilausta " + key + " ei löydy");
            }

        }
    }
}