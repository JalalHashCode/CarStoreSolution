using System.Net;
using CarStoreApi.Models;
using CarStoreApi.Models.Dto;
using CarStoreApi.Repository.IRepository;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace CarStoreApi.Controllers
{
    [Route("api/UsersAuth")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        protected APIResponse _response; 
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            this._response = new APIResponse();

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var loginResponse = await _userRepository.Login(loginRequestDTO);
            if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = true;
                _response.ErrorMessages.Add("Username or Password is incorrect"); 
                return BadRequest(_response);
            }
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = loginResponse; 
            return Ok(_response);

        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterationRequestDTO registerRequestDTO)
        {
           bool UserNameUnique = await _userRepository.IsUniqueUser(registerRequestDTO.UserName);
            if (!UserNameUnique)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Username already exists");
                return BadRequest(_response);
            }
            var user = await _userRepository.Register(registerRequestDTO);
            if (user == null) 
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Error while Registering");
                return BadRequest(_response);
            }

            return Ok(_response);
        }
    }
}
