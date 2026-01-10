using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PetMarket.Models;
using PetMarket.Services.Interfaces;
using System.Threading.Tasks;

namespace PetMarket.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("admin/pets")]
    public class PetController : Controller
    {

        private readonly IPetService _petService;
        private readonly IFileService _fileService; 

        public PetController(IPetService petService, IFileService fileService) 
        {
            _petService = petService;
            _fileService = fileService;
        }


        [HttpGet("")]
        public async Task<IActionResult> PetList()
        {
            ViewData["Title"] = "Manage Pets";

            var pets = await _petService.GetAllPetsAsync();
            return View(pets);
        }


        [HttpGet("create")]
        public async Task<IActionResult> AddPet()
        {
            ViewData["Title"] = "Add Pet";


            return View();
        }


        [HttpPost("create")]
        public async Task<IActionResult> AddPet(Pet pet)
        {
            if (ModelState.IsValid)
            {
               
                if (pet.ImagePet != null)
                {
                   
                    pet.ImagePetUrl = await _fileService.SaveFileAsync(pet.ImagePet, "pets");
                }

                if (await _petService.CreatePetAsync(pet))
                {
                    return RedirectToAction(nameof(PetList));
                }

                ModelState.AddModelError(string.Empty, "A apărut o eroare la crearea noului Pet.");

                
                if (!string.IsNullOrEmpty(pet.ImagePetUrl))
                {
                    _fileService.DeleteFile(pet.ImagePetUrl);
                }
            }


            return View(pet);
        }


        [HttpGet("edit/{id}")]
        public async Task<IActionResult> EditPet(int id)
        {

            var pet = await _petService.GetPetByIdAsync(id);
            if (pet == null) return NotFound();

            ViewData["Title"] = "Edit Pet";

            return View(pet);
        }


        [HttpPost("edit/{id}")]
        public async Task<IActionResult> EditPet(int id, Pet pet)
        {
            if (id != pet.IdPet) return BadRequest();

            if (ModelState.IsValid)
            {
                
                var existingPet = await _petService.GetPetByIdAsync(id);
                if (existingPet == null) return NotFound();

                string oldImageUrl = existingPet.ImagePetUrl;

                
                if (pet.ImagePet != null)
                {
                    
                    string newImageUrl = await _fileService.SaveFileAsync(pet.ImagePet, "pets");
                    pet.ImagePetUrl = newImageUrl;

                   
                    if (!string.IsNullOrEmpty(oldImageUrl))
                    {
                        _fileService.DeleteFile(oldImageUrl);
                    }
                }
                else
                {
                    
                    pet.ImagePetUrl = oldImageUrl;
                }


                
                if (await _petService.UpdatePetAsync(pet))
                {
                    return RedirectToAction(nameof(PetList));
                }

                
                if (pet.ImagePet != null && pet.ImagePetUrl != oldImageUrl && !string.IsNullOrEmpty(pet.ImagePetUrl))
                {
                    _fileService.DeleteFile(pet.ImagePetUrl);
                }

                ModelState.AddModelError(string.Empty, "A apărut o eroare la actualizarea Pet-ului.");
            }

            return View(pet);
        }


        [HttpGet("delete/{id}")]
        public async Task<IActionResult> DeletePet(int id)
        {
           
            var pet = await _petService.GetPetByIdAsync(id);
            if (pet == null) return NotFound();

            ViewData["Title"] = "Delete Pet";
            return View(pet);
        }


        [HttpPost("delete/{id}"), ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmedPet(int id)
        {
            
            var petToDelete = await _petService.GetPetByIdAsync(id);
            if (petToDelete == null) return NotFound();

            if (await _petService.DeletePetAsync(id))
            {
                
                if (!string.IsNullOrEmpty(petToDelete.ImagePetUrl))
                {
                    _fileService.DeleteFile(petToDelete.ImagePetUrl);
                }

                return RedirectToAction(nameof(PetList));
            }

            return NotFound();
        }
    }
}