using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.DTOs.Customer;
using WebApplication1.Interfaces;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{
    
    [Route("api/auth-customer")]
    [ApiController]
    public class AuthCustomerController : ControllerBase
    {
        private readonly IAuthCustomerService _service;

        public AuthCustomerController(IAuthCustomerService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public IActionResult Register(CustomerRegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _service.Register(dto);

            return Ok(result);
        }
        


        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _service.Login(dto);
            

            return Ok( result);

           
        }
    }
}