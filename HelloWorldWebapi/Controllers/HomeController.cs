using HelloWorldWebapi.Models;
using Microsoft.AspNetCore.Mvc; //ControllerBase ekliyoruz
using System.Net;

namespace HelloWorldWebapi.Controllers
{
    [ApiController] //ekliyoruz 
    [Route("home")] //ekliyoruz
    public class HomeController : ControllerBase //ControllersBase Controller özelliklerini kazanmasını sağlıyor

    {
        [HttpGet]
        public IActionResult GetMessage()
        {
            var result= new ResponseModel()
            {
                HttpStatus = 200,
                Message = "Hello Wold Web APi"
            };
            return Ok(result);
        }
    }
}
