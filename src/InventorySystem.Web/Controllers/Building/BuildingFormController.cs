using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using InventorySystem.Data.Repositories;
using InventorySystem.Data.Entity;
using InventorySystem.Web.ViewModel.Building;
using InventorySystem.Web.Controllers.Base;
using InventorySystem.Data.Helper;
using AutoMapper;

namespace InventorySystem.Web.Controllers.Building
{
    [Route("building")]
    public class BuildingFormController : BaseController
    {
        public BuildingFormController(BaseRepository baseRepository) : base(baseRepository)
        {
            
        }

        [HttpGet]
        [Route("edit")]
        public async Task<IActionResult> EditAsync()
        {
            return View(EditFormViewName, new BuildingFormViewModel
            {
                Brands = await GetBrands(null)
            });
        }

        [HttpGet]
        [Route("edit/{id}")]
        public async Task<IActionResult> EditAsync(string id)
        {
            var entity = await _baseRepository.LoadAsync<BuildingEntity>(ParserHelper.ToGuid(id));

            var viewModel = new BuildingFormViewModel
            {
                Brands = await GetBrands(null)
            };

            if (entity != null)
            {
                Mapper.Map(entity, viewModel);
            }

            return View(EditFormViewName, viewModel);
        }

        [HttpPost]
        [Route("edit/{id?}")]
        public async Task<IActionResult> SubmitAsync(BuildingFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _baseRepository.CreateOrUpdateAsync<BuildingEntity>(ParserHelper.ToGuid(viewModel.Id), viewModel);
            }

            viewModel.Brands = await GetBrands(viewModel.BrandId);

            return View(EditFormViewName, viewModel);
        }
    }
}
