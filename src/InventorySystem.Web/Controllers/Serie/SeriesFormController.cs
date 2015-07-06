using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using InventorySystem.Data.Repositories;
using InventorySystem.Data.Entity;
using InventorySystem.Web.ViewModel.Series;
using InventorySystem.Data.Helper;
using InventorySystem.Web.Controllers.Base;
using AutoMapper;

namespace InventorySystem.Web.Controllers.Serie
{
    [Route("series")]
    public class SeriesFormController : BaseController
    {
        public SeriesFormController(BaseRepository baseRepository) : base (baseRepository)
        {
            
        }

        [HttpGet]
        [Route("edit")]
        public async Task<IActionResult> EditAsync()
        {
            return View(EditFormViewName, new SeriesFormViewModel
            {
                Brands = await GetBrands(null)
            });
        }

        [HttpGet]
        [Route("edit/{id}")]
        public async Task<IActionResult> EditAsync(string id)
        {
            var entity = await _baseRepository.LoadAsync<SeriesEntity>(ParserHelper.ToGuid(id));

            var viewModel = new SeriesFormViewModel
            {
                Brands = await GetBrands(null)
            };

            if (entity != null)
            {
                viewModel = Mapper.Map(entity, viewModel);
            }

            return View(EditFormViewName, viewModel);
        }

        [HttpPost]
        [Route("edit/{id?}")]
        public async Task<IActionResult> SubmitAsync(SeriesFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _baseRepository.CreateOrUpdateAsync<SeriesEntity>(ParserHelper.ToGuid(viewModel.Id), viewModel);
            }

            viewModel.Brands = await GetBrands(viewModel.BrandId);

            return View(EditFormViewName, viewModel);
        }
    }
}
