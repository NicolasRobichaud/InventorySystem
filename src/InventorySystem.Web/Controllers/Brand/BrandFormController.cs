using AutoMapper;
using InventorySystem.Data.Entity;
using InventorySystem.Data.Helper;
using InventorySystem.Data.Repositories;
using InventorySystem.Web.Controllers.Base;
using InventorySystem.Web.ViewModel.Brand;
using Microsoft.AspNet.Mvc;
using System.Threading.Tasks;

namespace InventorySystem.Web.Brand.Controllers
{
    [Route("brand")]
    public class BrandFormController : BaseController
    {
        public BrandFormController(BaseRepository baseRepository) : base(baseRepository)
        {
            
        }

        [HttpGet]
        [Route("edit")]
        public IActionResult Edit()
        {
            return View(EditFormViewName);
        }

        [HttpGet]
        [Route("edit/{id}")]
        public async Task<IActionResult> EditAsync(string id)
        {
            return View(EditFormViewName, Mapper.Map<BrandFormViewModel>(await _baseRepository.LoadAsync<BrandEntity>(ParserHelper.ToGuid(id))));
        }

        [HttpPost]
        [Route("edit/{id?}")]
        public async Task<IActionResult> SubmitAsync(BrandFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _baseRepository.CreateOrUpdateAsync<BrandEntity>(ParserHelper.ToGuid(viewModel.Id), viewModel);
            }

            return View(EditFormViewName, viewModel);
        }
    }
}
