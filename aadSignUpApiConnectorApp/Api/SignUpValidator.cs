using aadSignUpApiConnectorApp.Models;
using aadSignUpApiConnectorApp.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace aadSignUpApiConnectorApp.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignUpValidator : ControllerBase
    {
        // GET: api/<SignUpValidator>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<SignUpValidator>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SignUpValidator>
        [HttpPost]
        public IActionResult Post([FromBody] JsonElement value)
        {
            if (!HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                return BadRequest("Authorization header missing");
            }            

            var authHeader = HttpContext.Request.Headers["Authorization"];
            var authHeaderValue = AuthenticationHeaderValue.Parse(authHeader);

            var credentialBytes = Convert.FromBase64String(authHeaderValue.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
            var username = credentials[0];
            var password = credentials[1];

            if(!AuthenticationService.ValidateUser(username, password))
            {
                return Unauthorized("Wrong credentials!!");
            }

            var userSignUpObj = JsonSerializer.Deserialize<UserValidationModel>(value.GetRawText());

            if (!AuthenticationService.UserInValidDomain(userSignUpObj.email))
            {
                return BadRequest("Email domain not allowed!!");
            }

            return Ok();
        }

        

        // PUT api/<SignUpValidator>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SignUpValidator>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
