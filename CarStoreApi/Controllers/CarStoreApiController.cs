using System.Diagnostics;
using System.Linq;
using System.Net;
using AutoMapper;
using CarStoreApi.Data;
using CarStoreApi.Models;
using CarStoreApi.Models.Dto;
using CarStoreApi.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarStoreApi.Controllers
{
    //[Route("api/[[conroller]]")]
    [Route("api/CarStoreApi")]
    [ApiController]
    public class CarStoreApiController : ControllerBase
    {
        private readonly ILogger<CarStoreApiController> _logger;
        private readonly ICarRepository _dbCars;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public CarStoreApiController(ILogger<CarStoreApiController> logger, ICarRepository dbCars, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _dbCars = dbCars;
            this._response = new APIResponse();
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<APIResponse>> GetAllCars()
        {
            try
            {
                var actionStartTime = DateTime.Now;
                _logger.LogInformation("Action completed in {actionStartTime}", actionStartTime);

                IEnumerable<Car> carsList = await _dbCars.GetAllAsync();
                _response.Result = _mapper.Map<List<CarDTO>>(carsList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;
        }


        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<APIResponse>> GetCar(int id)
        {

            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }

                var car = await _dbCars.GetAsync(u => u.Id == id);
                if (car == null)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("This item is not Exist");
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<CarDTO>(car);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<APIResponse>> AddCar([FromBody] CarCreateDTO carCreateDto)
        {

            try
            {
                if (carCreateDto == null || !ModelState.IsValid)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Please Check Inserted Item");
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var existedCar = await _dbCars.GetAsync(u => u.Model == carCreateDto.Model);
                if (existedCar != null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Item Model Alerady Existed");
                    return BadRequest(_response);
                }
;
                Car car = _mapper.Map<Car>(carCreateDto);
                await _dbCars.CreateAsync(car);

                _response.Result = _mapper.Map<CarDTO>(car);
                _response.StatusCode = HttpStatusCode.Created;
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;
        }


        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<APIResponse>> DeleteCar(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Please Choose Valid Item");
                    return BadRequest(_response);
                }
                var car = await _dbCars.GetAsync(u => u.Id == id);
                if (car == null)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("This item is not Exist");
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _dbCars.RemoveAsync(car);
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<APIResponse>> UpdateCar(int id, [FromBody] CarUpdateDTO carUpdateDto)
        {

            try
            {
                if (carUpdateDto == null || id != carUpdateDto.Id)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }

                Car model = _mapper.Map<Car>(carUpdateDto);
                await _dbCars.UpdateAsync(model);
                await _dbCars.SaveAsync();
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;

        }


        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPatch]
        public async Task<ActionResult> UpdatePartialCar(int id, JsonPatchDocument<CarUpdateDTO> carUpdateDTO)
        {
            if (carUpdateDTO == null || id == 0)
            {
                return BadRequest();
            }

            var oldCar = await _dbCars.GetAsync(u => u.Id == id, tracked: false);
            if (oldCar == null)
            {
                return NotFound();
            }

            CarUpdateDTO carDto = _mapper.Map<CarUpdateDTO>(oldCar);
            carUpdateDTO.ApplyTo(carDto, ModelState);
            Car model = _mapper.Map<Car>(carDto);

            await _dbCars.UpdateAsync(model);
            await _dbCars.SaveAsync();
            return NoContent();

        }

    }
}
