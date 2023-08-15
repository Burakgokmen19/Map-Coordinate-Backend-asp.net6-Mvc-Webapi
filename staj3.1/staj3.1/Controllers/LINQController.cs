using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using staj3._1.Data;
using Microsoft.EntityFrameworkCore;

namespace staj3._1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LINQController : ControllerBase
    {

        Response rsp = new Response();
        List<Location> locations = new List<Location>();


        private readonly DataContext _dbContext;
        public LINQController(DataContext dbContext)
        {
            _dbContext = dbContext;

        }

        [HttpGet]
        public Response Getall()
        {

            var LINQdeneme = from cust in _dbContext.Location4
                             select cust;

            LINQdeneme.ToList();
            rsp.Value = LINQdeneme;
            return rsp;
            /*  foreach (var location in LINQdeneme)
              {
                  locations.Add(location);    
              }*/
            // return locations;
            //  return LINQdeneme;
            /*var response = new Response();
            response.Value = _dbContext.Location4.ToList();

            response.Result = "Başla";
            return response;*/


        }
        [HttpPost]
        public Response Add(Location loc)
        {
            var LINQdeneme = (from _TempLocation in _dbContext.Location4.Where(_TempLocation => _TempLocation.Name == loc.Name)
                              select _TempLocation);

            Response rsp = new Response();
            if (loc.Name == "string" || loc.X ==0|| loc.Y == 0)
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
            var abc = (from _TempLocation in _dbContext.Location4.Where(_TempLocation => _TempLocation.Id == id)
                       select _TempLocation);
            _Response.Value = abc;
            _Response.Result = "getirildi";
            return _Response;
        }

        [HttpDelete]  

        public Response Delete(Location loc )
        {
            Response _Response = new Response();
            var location = _dbContext.Location4.FirstOrDefault();
            _dbContext.Location4.Remove((Location)loc);
            _dbContext.SaveChanges();
            _Response.Result = "silindi";
            _Response.Status = true;
            return _Response;
        }
        [HttpPut]
          public Response Update(Location loc)
        {
            Response rsp = new Response();
            var temploc = (from getid in _dbContext.Location4 where getid.Id == loc.Id select getid).FirstOrDefault();
            if (temploc == null )
            {
                rsp.Result = "Id bulunamadı";
            }
            else
            {
                if (temploc.Name != "string")
                {
                    temploc.Name = loc.Name;
                }

                if (temploc.X != 0)
                {
                    temploc.X = loc.X;
                }

                if (temploc.Y != 0)
                {
                    temploc.Y = loc.Y;
                }

                _dbContext.SaveChanges();
                rsp.Result = "güncellendi";
                rsp.Value = true;
             
            }
            return rsp;
        }
    }
}
