using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace MyFirstCoreApi.Controllers
{
    [Route("nw/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        //[HttpGet]
        //[Route("")]
        //public List<Orders> GetAllOrders()
        //{
        //    NorthwindContext db = new NorthwindContext();
        //    List<Orders> tilaus = db.Orders.ToList();
        //    return tilaus;
        //}

        //[HttpGet]
        //[Route("{id}")]
        //public Orders GetOneOrder(int id)
        //{
        //    NorthwindContext db = new NorthwindContext();
        //    Orders tilaus = db.Orders.Find(id);
        //    return tilaus;
        //}

        //[HttpGet]
        //[Route("shipCity/{key}")]
        //public List<Orders> GetSomeOrders(string key)
        //{
        //    NorthwindContext db = new NorthwindContext();

        //    var SomeOrders = from c in db.Orders
        //                        where c.ShipCity == key
        //                        select c;

        //    return SomeOrders.ToList();
        //}

        //[HttpPost] //<--filtteri rajoittaa alapuolella olevan metodin vain POST-pyyntöihin (uuden asian luominen tai lisääminen)
        //[Route("")] //<--tyhjä reitinmääritys (ei ole pakko laittaa), eli ei mitään lisättävää reittiin, jolloin
        //public ActionResult PostCreateNew([FromBody] Orders tilaus) //<-- [FromBody] tarkoittaa, että HTTP-pyynnön Body:ssä välitetään JSON-muodossa oleva objekti ,joka om Documentation-tyyppinen customer-niminen
        //{
        //    NorthwindContext db = new NorthwindContext();
        //    try
        //    {


        //        db.Orders.Add(tilaus);
        //        db.SaveChanges();

        //        return Ok(tilaus.OrderId);
        //    }

        //    catch (Exception)
        //    {
        //        return BadRequest("Jokin meni pieleen asiakasta lisättäessä");
        //    }

        //    finally
        //    {
        //        db.Dispose();
        //    }

        //    /*return doku.DocumentationId.ToString; //k*//*uittaus Frontille, että päivitys meni oikein --> Frontti voi tsekata, että kontrolleri palauttaa saman id:n mitä käsitteli*/
        //}
        //[HttpPut] //<-- filtteri, joka sallii vain PUT-metodit (Http-verbit)
        //[Route("{key}")] //<-- Routemääritys asiakasavaimelle key=CustomerID
        //public ActionResult PutEdit(int key, [FromBody] Orders tilaus) //<-- [FromBody] tarkoittaa, että HTTP-pyynnön Body:ssä välitetään JSON-muodossa oleva objekti ,joka on Customers-tyyppinen asiakas-niminen
        //{
        //    NorthwindContext db = new NorthwindContext(); //Context = Kuten entities muodostettu Scaffold DBContext -työkalulla. Voisi olla myös entiteetti frameworkCore
        //    try
        //    {
        //        Orders order = db.Orders.Find(key);
        //        if (order != null)
        //        {
        //            order.CustomerId = tilaus.CustomerId;
        //            order.EmployeeId = tilaus.EmployeeId;
        //            order.ShipCountry = tilaus.ShipCountry;
        //            order.OrderDate = tilaus.OrderDate;
        //            db.SaveChanges();
        //            return Ok(order.OrderId);
        //        }

        //        else
        //        {
        //            return NotFound("Päivitettävää tilausta ei löytynyt!");
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return BadRequest("Jokin meni pieleen atilausta päivittäessä");
        //    }

        //    finally
        //    {
        //        db.Dispose();
        //    }
        //}
        //[HttpDelete]
        //[Route("{key}")]
        //public ActionResult DeleteCustomer(int key) //
        //{
        //    NorthwindContext db = new NorthwindContext();
        //    Orders tilaus = db.Orders.Find(key);
        //    if (tilaus != null)
        //    {
        //        db.Orders.Remove(tilaus);
        //        db.SaveChanges();
        //        return Ok("Tilaus " + key + " poistettiin");
        //    }
        //    else
        //    {
        //        return NotFound("Tilausta " + key + " ei löydy");
        //    }

        //}
    }
}