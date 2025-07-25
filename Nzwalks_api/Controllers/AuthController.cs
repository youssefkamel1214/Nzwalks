using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nzwalks_api.CustomActionFilter;
using Nzwalks_api.Models.DTO;
using Nzwalks_api.Models.DTO.Requestes;
using Nzwalks_api.Repostories.Interfaces;

namespace Nzwalks_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;
        public AuthController(UserManager<IdentityUser> userManager,ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
            // Constructor logic can go here if needed
        }

        [HttpPost]
        [Route("Register")]
        [ValidateModel]
        public async Task<IActionResult>Register([FromBody] RegisterRequestDto registerRequest)
        {
            // Here you would typically call a service to handle the registration logic
            // For example, saving the user to the database, sending a confirmation email, etc.
            // This is just a placeholder response for demonstration purposes.
            var user = new IdentityUser
            {
                UserName = registerRequest.Email,
                Email = registerRequest.Email
            };
            var result = await _userManager.CreateAsync(user, registerRequest.Password);
            if (result.Succeeded) 
            {
                if (registerRequest.Roles != null && registerRequest.Roles.Any())
                {
                    result = await _userManager.AddToRolesAsync(user, registerRequest.Roles);
                    if (result.Succeeded)
                    {
                        return Ok("User was registred please login");
                    }
                }
            }
            return BadRequest("somthing went wrong");
        }
        [HttpPost]
        [Route("Login")]
        [ValidateModel]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            // Here you would typically call a service to handle the login logic
            // For example, checking the user's credentials, generating a JWT token, etc.
            // This is just a placeholder response for demonstration purposes.
            var user = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginRequest.Password))
            {
                //create a token and return it
                var roles = await _userManager.GetRolesAsync(user);
                if (roles != null || roles.Any())
                {
                    var jwttoken = _tokenRepository.CreateJWTToken(user, roles.ToList());
                    var response = new LoginResponseDto
                    {
                        Token = jwttoken,
                    };
                    return Ok(response);
                }
            }
            return Unauthorized("Invalid Username or Password");
        }
    }

}
/*
 {
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJyZWFkZXJAZXhhbXBsZS5jb20iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJSZWFkZXIiLCJleHAiOjE3NTIzNjc4MjAsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcxMDEifQ.y77LOjK0X4JV1Xytqmd3Gf1kFpAm2ZvsOrspD5xtHC4"
}*/