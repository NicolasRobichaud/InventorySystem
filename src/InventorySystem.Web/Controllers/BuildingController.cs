using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using InventorySystem.Web.ViewModel;
using InventorySystem.Data.Repositories;
using InventorySystem.Data.Entity;

namespace InventorySystem.Web.Controllers
{
    [Route("building")]
    public class BuildingController : Controller
    {
        private readonly BaseRepository _baseRepository;

        public BuildingController(BaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

        [HttpGet]
        [Route("add")]
        public async Task<IActionResult> AddBuildingFormAsync()
        {
            return View();
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddBuildingFormAsync(AddBuildingFormViewModel viewModel)
        {
            await _baseRepository.AddNewItemAsync(new BuildingEntity
            {
                Name = viewModel.Name
            });

            return View(viewModel);
        }
    }
}
