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
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public List<Products> GetAllProducts()
        {
            NorthwindContext db = new NorthwindContext();
            List<Products> tuotteet = db.Products.ToList();
            return tuotteet;
        }

        [HttpGet]
        [Route("C")]
        public IActionResult Model(int offset, int limit, string tuotenimi)
        {
            //Tämä malli antaa enemmän mahdollisuuksia
            NorthwindContext db = new NorthwindContext();
            var Model = (from p in db.Products
                         join c in db.Categories on p.CategoryId equals c.CategoryId
                         where c.CategoryId == p.CategoryId
                         orderby p.ProductId descending
                         select new
                         {
                            p.ProductId,
                            p.ProductName,
                            p.SupplierId,
                        Kategoria = c.CategoryName,
                            p.CategoryId,
                            p.QuantityPerUnit,
                            p.UnitPrice,
                            p.UnitsInStock,
                            p.ReorderLevel,
                            p.Discontinued
                         }).Skip(offset).Take(limit).ToList();


            return Ok(Model);
        }


        [HttpGet]
        [Route("{id}")]
        public Products GetOneProduct(int id)
        {
            NorthwindContext db = new NorthwindContext();
            Products tuote = db.Products.Find(id);
            return tuote;
        }

        [HttpGet]
        [Route("SupplierID/{key}")]
        public List<Products> GetSomeProduct(int key)
        {
            NorthwindContext db = new NorthwindContext();

            var SomeProducts = from c in db.Products
                               where c.SupplierId == key
                               select c;

            return SomeProducts.ToList();
        }

        [HttpPost] //<--filtteri rajoittaa alapuolella olevan metodin vain POST-pyyntöihin (uuden asian luominen tai lisääminen)
        [Route("")] //<--tyhjä reitinmääritys (ei ole pakko laittaa), eli ei mitään lisättävää reittiin, jolloin
        public ActionResult PostCreateNew([FromBody] Products tuotteet) //<-- [FromBody] tarkoittaa, että HTTP-pyynnön Body:ssä välitetään JSON-muodossa oleva objekti ,joka om Documentation-tyyppinen customer-niminen
        {
            NorthwindContext db = new NorthwindContext();
            try
            {
                db.Products.Add(tuotteet);
                db.SaveChanges();

                return Ok(tuotteet.ProductId);
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

        //[HttpPost] //<--filtteri rajoittaa alapuolella olevan metodin vain POST-pyyntöihin (uuden asian luominen tai lisääminen)
        //[Route("")] //<--tyhjä reitinmääritys (ei ole pakko laittaa), eli ei mitään lisättävää reittiin, jolloin
        //public string PostCreateNew([FromBody] Products tuote) //<-- [FromBody] tarkoittaa, että HTTP-pyynnön Body:ssä välitetään JSON-muodossa oleva objekti ,joka om Documentation-tyyppinen customer-niminen
        //{
        //    NorthwindContext context = new NorthwindContext(); //Context = Kuten entities muodostettu Scaffold DBContext -tykalulla. Voisi olla myös entiteetti frameworkCore
        //    context.Products.Add(tuote);
        //    context.SaveChanges();

        //    return tuote.Doc.ToString(); //kuittaus Frontille, että päivitys meni oikein --> Frontti voi tsekata, että kontrolleri palauttaa saman id:n mitä käsitteli
        //}

        [HttpPut] //<-- filtteri, joka sallii vain PUT-metodit (Http-verbit)
        [Route("{key}")] //<-- Routemääritys asiakasavaimelle key=CustomerID
        public ActionResult PutEdit(int key, [FromBody] Products tuotteet ) //<-- [FromBody] tarkoittaa, että HTTP-pyynnön Body:ssä välitetään JSON-muodossa oleva objekti ,joka on Customers-tyyppinen asiakas-niminen
        {
            NorthwindContext db = new NorthwindContext(); //Context = Kuten entities muodostettu Scaffold DBContext -työkalulla. Voisi olla myös entiteetti frameworkCore
            try
            {
                Products product = db.Products.Find(key);


                if (product != null)
                {
                    product.ProductName = tuotteet.ProductName;
                    product.SupplierId = tuotteet.SupplierId;
                    product.CategoryId = tuotteet.CategoryId;
                    product.QuantityPerUnit = tuotteet.QuantityPerUnit;
                    product.UnitPrice = tuotteet.UnitPrice;
                    product.UnitsInStock = tuotteet.UnitsInStock;
                    product.Discontinued = tuotteet.Discontinued;
                    db.SaveChanges();
                    return Ok(tuotteet.ProductId);
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
        public string DeleteSingle(int key)
        {
            NorthwindContext context = new NorthwindContext();
            Products tuotteet = context.Products.Find(key);

            if (tuotteet != null)
            {
                context.Products.Remove(tuotteet);
                context.SaveChanges();
                return "DELETED";
            }
            return "NOT FOUND";
        }

        [HttpGet]
        [Route("R")]

        public IActionResult GetSomeProducts(int offset, int limit, string tuotenimi)
        {
            if (tuotenimi != null)
            {
                NorthwindContext db = new NorthwindContext();
                List<Products> tuotteet = db.Products.Where(d => d.ProductName == tuotenimi).ToList();
                return Ok(tuotteet);
            }

            else
            {
                NorthwindContext db = new NorthwindContext();
                List<Products> tuotteet = db.Products.Skip(offset).Take(limit).ToList();
                return Ok(tuotteet);
            }

        }
    }
}