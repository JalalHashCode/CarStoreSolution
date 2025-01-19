using AutoMapper;
using CarStore.Models;
using CarStore.Models.Dto;
using CarStore.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarStore.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _carService;
        private readonly IMapper _mapper;

        public CarController( ICarService carService, IMapper mapper)
        {
            _carService = carService;
            _mapper = mapper;
        }
        public async Task<IActionResult> IndexCars()
        {
            List<CarDTO> carsList = new List<CarDTO>();
            var response = await _carService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                carsList = JsonConvert.DeserializeObject<List<CarDTO>>(Convert.ToString(response.Result)); 
            }
            return View(carsList);
        }
    }
}
