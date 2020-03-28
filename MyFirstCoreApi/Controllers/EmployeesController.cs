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
    public class EmployeesController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public List<Employees> GetAllEmployees()
        {
            NorthwindContext db = new NorthwindContext();
            List<Employees> tekija = db.Employees.ToList();
            return tekija;
        }

        [HttpGet]
        [Route("{id}")]
        public Employees GetOneEmployees(int id)
        {
            NorthwindContext db = new NorthwindContext();
            Employees tyontekija = db.Employees.Find(id);
            return tyontekija;
        }

        [HttpGet]
        [Route("country/{key}")]
        public List<Employees> GetSomeCustomers(string key)
        {
            NorthwindContext db = new NorthwindContext();

            var SomeEmployees = from c in db.Employees
                                where c.LastName == key
                                select c;

            return SomeEmployees.ToList();
        }

        [HttpPost] //<--filtteri rajoittaa alapuolella olevan metodin vain POST-pyyntöihin (uuden asian luominen tai lisääminen)
        [Route("")] //<--tyhjä reitinmääritys (ei ole pakko laittaa), eli ei mitään lisättävää reittiin, jolloin
        public ActionResult PostCreateNew([FromBody] Employees tyontekija) //<-- [FromBody] tarkoittaa, että HTTP-pyynnön Body:ssä välitetään JSON-muodossa oleva objekti ,joka om Documentation-tyyppinen customer-niminen
        {
            NorthwindContext db = new NorthwindContext();
            try
            {


                db.Employees.Add(tyontekija);
                db.SaveChanges();

                return Ok(tyontekija.EmployeeId);
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
        public ActionResult PutEdit(int key, [FromBody] Employees tyontekija) //<-- [FromBody] tarkoittaa, että HTTP-pyynnön Body:ssä välitetään JSON-muodossa oleva objekti ,joka on Customers-tyyppinen asiakas-niminen
        {
            NorthwindContext db = new NorthwindContext(); //Context = Kuten entities muodostettu Scaffold DBContext -työkalulla. Voisi olla myös entiteetti frameworkCore
            try
            {
                Employees Emp = db.Employees.Find(key);
                if (Emp != null)
                {
                    Emp.LastName = tyontekija.LastName;
                    Emp.FirstName = tyontekija.FirstName;
                    Emp.Title = tyontekija.Title;
                    Emp.Country = tyontekija.Country;
                    Emp.City = tyontekija.City;



                    db.SaveChanges();
                    return Ok(Emp.EmployeeId);
                }

                else
                {
                    return NotFound("Päivitettävää asiakasta ei löytynyt!");
                }
            }
            catch (Exception)
            {
                return BadRequest("Jokin meni pieleen asiakasta lpäivittäessä");
            }

            finally
            {
                db.Dispose();
            }
        }
        [HttpDelete]
        [Route("{key}")]
        public ActionResult DeleteEmployee(int key) //
        {
            NorthwindContext db = new NorthwindContext();
            Employees emp = db.Employees.Find(key);
            if (emp != null)
            {
                db.Employees.Remove(emp);
                db.SaveChanges();
                return Ok("Työntekijä " + key + " poistettiin");
            }
            else
            {
                return NotFound("Työntekijää " + key + " ei löydy");
            }

        }
    }
}