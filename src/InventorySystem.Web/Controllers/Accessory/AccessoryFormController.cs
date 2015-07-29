using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using InventorySystem.Web.Controllers.Base;
using InventorySystem.Data.Repositories;
using AutoMapper;
using InventorySystem.Data.Entity;
using InventorySystem.Data.Helper;
using InventorySystem.Web.ViewModel.Accessory;

namespace InventorySystem.Web.Controllers.Accessory
{
    [Route("accessory")]
    public class AccessoryFormController : BaseController
    {
        public AccessoryFormController(BaseRepository baseRepository) : base(baseRepository)
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
            return View(EditFormViewName, Mapper.Map<AccessoryFormViewModel>(await _baseRepository.LoadAsync<AccessoryEntity>(ParserHelper.ToGuid(id))));
        }

        [HttpPost]
        [Route("edit/{id?}")]
        public async Task<IActionResult> SubmitAsync(AccessoryFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _baseRepository.CreateOrUpdateAsync<AccessoryEntity>(ParserHelper.ToGuid(viewModel.Id), viewModel);
            }

            return View(EditFormViewName, viewModel);
        }
    }
}
