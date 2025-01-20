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

        public CarController(ICarService carService, IMapper mapper)
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
        public async Task<IActionResult> CreateCar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCar(CarCreateDTO cardDto)
        {
            if (ModelState.IsValid)
            {
                var response = await _carService.CreateAsync<APIResponse>(cardDto);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Car Created Successfully"; 
                    return RedirectToAction(nameof(IndexCars));
                }
            }
            TempData["error"] = "Error Encountered";
            return View(cardDto);
        }

        public async Task<IActionResult> UpdateCar(int carId)
        {
            if (carId != 0)
            {
                var response = await _carService.GetAsync<APIResponse>(carId);
                if (response != null && response.IsSuccess)
                {
                    CarUpdateDTO updateDto = new CarUpdateDTO();
                    updateDto = JsonConvert.DeserializeObject<CarUpdateDTO>(Convert.ToString(response.Result));
                    return View(updateDto);
                }
            }


            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCar(CarUpdateDTO cardDto)
        {
            if (ModelState.IsValid)
            {
                var response = await _carService.UpdateAsync<APIResponse>(cardDto);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Car Updated Successfully";

                    return RedirectToAction(nameof(IndexCars));
                }
            }
            TempData["error"] = "Error Encountered";
            return View(cardDto);
        }

        public async Task<IActionResult> DeleteCar(int carId)
        {

            var response = await _carService.GetAsync<APIResponse>(carId);
            if (response != null && response.IsSuccess)
            {
                CarUpdateDTO updateDto = new CarUpdateDTO();
                updateDto = JsonConvert.DeserializeObject<CarUpdateDTO>(Convert.ToString(response.Result));
                return View(updateDto);
            }
            TempData["error"] = "Error Encountered";
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCar(CarUpdateDTO cardDto)
        {

            var response = await _carService.DeleteAsync<APIResponse>(cardDto.Id);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Car Deleted Successfully";
                return RedirectToAction(nameof(IndexCars));
            }
            TempData["error"] = "Error Encountered";
            return View(cardDto);
        }
    }
}
