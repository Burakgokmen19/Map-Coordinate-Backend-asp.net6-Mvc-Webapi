using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using staj3._1.Data;

namespace staj3._1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {

            private readonly DataContext _dbContext;
            public LocationController(DataContext dbContext)
            {
                _dbContext = dbContext;
            }
            [HttpGet]
            public Response Getall()
            {
                var response = new Response();
                response.Value = _dbContext.Location4.ToList();

                response.Result = "Başla";
                return response;
            }



            [HttpPost]
            public Response Add(Location loc)
            {
                Response rsp = new Response();
                if (loc.Name == "string" || loc.X == 0 || loc.Y == 0)
                {
                    rsp.Result = "değer gir";
                }
                else
                {
                    var location = _dbContext.Location4.FirstOrDefault(x => x.Name.ToLower() == loc.Name.ToLower());

                    if (location != null)
                    {
                        rsp.Result = "aynı isme sahip veri var";
                    }
                    else
                    {
                        _dbContext.Add(loc);
                        _dbContext.SaveChanges();
                    }
                }
                return rsp;

            }

            [HttpGet("id")]
            public Response Get(int id)
            {
                Response _Response = new Response();
                var abc = _dbContext.Location4.FirstOrDefault(x => x.Id == id);
                _Response.Value = abc;
                _Response.Result = "getirildi";
                return _Response;
            }


            [HttpDelete]
            public Response delete(int id)
            {
                Response _Response = new Response();
                _Response.Result = "silindi";
                var abc = _dbContext.Location4.FirstOrDefault(x => x.Id == id);
                _dbContext.Location4.Remove((Location)abc);
                _dbContext.SaveChanges();
                _Response.Status = true;
                return _Response;
            }

            [HttpPut]

            public Response Update(Location loc)
            {
                Response rs = new Response();

                var location = _dbContext.Location4.FirstOrDefault(x => x.Id == loc.Id);
                if (location == null)
                {
                    rs.Result = "not found";
                    rs.Status = false;
                    return rs;
                }
                else
                {

                    if (loc.Name != "string")
                    {
                        location.Name = loc.Name;
                    }

                    if (loc.X !=    0)
                    {
                        location.X = loc.X;
                    }

                    if (loc.Y != 0)
                    {
                        location.Y = loc.Y;
                    }

                    _dbContext.SaveChanges();
                    rs.Result = "güncellendi";
                    rs.Value = true;


                }
                return rs;
            }
        } 


    } 

