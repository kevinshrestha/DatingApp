using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthRepository _repo;
        // Injecting IConfiguration

        // add underscore because it's private field
        private readonly IConfiguration _config;

        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _config = config;
            _repo = repo;
        }

        [HttpPost("register")]
        // getting UserForRegisterDto from UserForRegisterDto Dtos folder
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            // validate request

            // to prevent username having upper or lowercase of the same name
            // EX: John vs john
            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

            if (await _repo.UserExists(userForRegisterDto.Username))
                // AuthConroller : ControllerBase
                return BadRequest("Username already exists");

            //import models
            var userToCreate = new User
            {
                Username = userForRegisterDto.Username
            };

            var createdUser = await _repo.Register(userToCreate, userForRegisterDto.Password);

            // return the success message to calling method
            // takes the string of the root name and the object value itself
            // send back out to our clients the locatino of where to get the newly created entity

            // return CreatedAtRoute()
            return StatusCode(201);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {

            // we write this for both username and password because if someone tries to get into an account
            // we don't want to show that the username they're using is correct but the password is incorrect
            // prevent brute force attack

            // we confirm that we have a user and password that matches in our datebase
            var userFromRepo = await _repo.Login(userForLoginDto.UserName.ToLower(), userForLoginDto.Password);

            //so we specify both the username and password are incorrect and throw an unauthorized to them
            if (userFromRepo == null)
                return Unauthorized();


            var claims = new[]
            {
                // This claim is for the user's ID
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                // This claim is for the user's username
                new Claim(ClaimTypes.Name, userFromRepo.Username)
            };

            // we create a security key and then using the key as part
            // of the signin credentials and encrypting the key with the hashing algorithm
            var key = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(_config.GetSection("AppSettings:Token").Value));

            // This takes our security key that we generated above and the algorithm
            // we're going to use to has this key
                                                // insert key and then specify the string algorithm
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            

            // this contains our claims, our expiry date for our token, the signing credentials
            
            // After everything above, we create the token and the descriptor and pass in our claims as the subjects
            // with an expiry date of 24 hours, and pass in the signing credentials as well
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            // creates token
            // After creating the token, descriptor, and expiry date, we create a new Jwt security token handler
            var tokenHandler = new JwtSecurityTokenHandler();

            // contains the Jwt token that returns to client
            // Once the Jwt token is created, we are now able to create the token based on the decriptor 
            // being passed in. It is then stored the token variable
            var token = tokenHandler.CreateToken(tokenDescriptor);


            // We use this token variable, to write the response that we send back to the client
            return Ok(new {
                token = tokenHandler.WriteToken(token)
            });


        }

    }

}