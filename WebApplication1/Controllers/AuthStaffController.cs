using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Interfaces;

namespace WebApplication1.Controllers
{
    [Route("api/auth-staff")]
    [ApiController]
    public class AuthStaffController : ControllerBase
    {
        private readonly IAuthStaffService _service;

        public AuthStaffController(IAuthStaffService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public IActionResult Register(StaffRegisterDto dto)
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
            return Ok(result);
        }
    }
}