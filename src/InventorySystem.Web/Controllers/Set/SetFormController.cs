using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using InventorySystem.Web.Controllers.Base;
using InventorySystem.Data.Repositories;
using AutoMapper;
using InventorySystem.Data.Entity;
using InventorySystem.Data.Helper;
using InventorySystem.Web.ViewModel.Accessory;
using InventorySystem.Web.ViewModel.Set;

namespace InventorySystem.Web.Controllers.Accessory
{
    [Route("set")]
    public class SetFormController : BaseController
    {
        public SetFormController(BaseRepository baseRepository) : base(baseRepository)
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
            return View(EditFormViewName, Mapper.Map<SetFormViewModel>(await _baseRepository.LoadAsync<SetEntity>(ParserHelper.ToGuid(id))));
        }

        [HttpPost]
        [Route("edit/{id?}")]
        public async Task<IActionResult> SubmitAsync(SetFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _baseRepository.CreateOrUpdateAsync<SetEntity>(ParserHelper.ToGuid(viewModel.Id), viewModel);
            }

            return View(EditFormViewName, viewModel);
        }
    }
}
