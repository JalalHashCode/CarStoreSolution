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
    public class UsersAuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        protected APIResponse _response;
        public UsersAuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            this._response = new APIResponse();

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            if (loginRequestDTO == null)
            {
                return BadRequest();
            }
            var loginResponse = await _userRepository.Login(loginRequestDTO);
            if (loginResponse.User == null && loginResponse.ErrorMessage != string.Empty)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add(loginResponse.ErrorMessage);
                return BadRequest(_response);
            }

            if (loginResponse.User != null && !string.IsNullOrEmpty(loginResponse.Token))
            {
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = loginResponse;
                return Ok(_response);
            }
            return BadRequest("Unexpected Error happened");


        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterationRequestDTO registerRequestDTO)
        {
            if (registerRequestDTO == null)
            {
                return BadRequest();
            }

            var registerResponse = await _userRepository.Register(registerRequestDTO);
            if (registerResponse.User == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = registerResponse.errors;
                return BadRequest(_response);
            }

            _response.StatusCode = HttpStatusCode.Created;
            _response.Result = registerResponse.User;
            _response.IsSuccess = true;
            return Ok(_response);
        }
    }
}
