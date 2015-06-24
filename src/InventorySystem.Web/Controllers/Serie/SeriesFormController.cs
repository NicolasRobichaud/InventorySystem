using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using InventorySystem.Data.Repositories;
using InventorySystem.Data.Entity;
using InventorySystem.Web.ViewModel.Series;
using InventorySystem.Data.Helper;
using Microsoft.AspNet.Mvc.Rendering;
using System.Linq;

namespace InventorySystem.Web.Controllers.Serie
{
    [Route("series")]
    public class SeriesFormController : Controller
    {
        private const string ViewName = "EditForm";
        private readonly BaseRepository _baseRepository;

        public SeriesFormController(BaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

        [HttpGet]
        [Route("edit")]
        public async Task<IActionResult> Edit()
        {
            return View(ViewName, new SeriesFormViewModel
            {
                Brands = await GetBrands(null)
            });
        }

        [HttpGet]
        [Route("edit/{id}")]
        public async Task<IActionResult> EditAsync(string id)
        {
            var series = await _baseRepository.LoadAsync<SeriesEntity>(ParserHelper.ToGuid(id));


            return View(ViewName, new SeriesFormViewModel
            {
                Id = series?.Id.ToString(),
                Name = series?.Name,
                Brands = await GetBrands(series?.BrandId)
            });
        }

        [HttpPost]
        [Route("edit/{id?}")]
        public async Task<IActionResult> SubmitAsync(SeriesFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _baseRepository.CreateOrUpdateAsync(ParserHelper.ToGuid(viewModel.Id),
                    c =>
                    {
                        c.Name = viewModel.Name;
                        c.BrandId = viewModel.BrandId.HasValue ? viewModel.BrandId.Value : Guid.Empty;
                    },
                    () =>
                    {
                        return new SeriesEntity
                        {
                            Name = viewModel.Name,
                            BrandId = viewModel.BrandId.HasValue ? viewModel.BrandId.Value : Guid.Empty
                        };
                    });
            }

            viewModel.Brands = await GetBrands(viewModel.BrandId);

            return View(ViewName, viewModel);
        }

        private async Task<List<SelectListItem>> GetBrands(Guid? currentBrandId)
        {
            var brandEntities = await Raven.Client.LinqExtensions.ToListAsync(_baseRepository.Query<BrandEntity>());
            var brands = brandEntities.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.Id.ToString(),
                Selected = currentBrandId.HasValue ? p.Id == currentBrandId.Value : false
            }).ToList();

            brands.Insert(0, new SelectListItem());

            return brands;
        }
    }
}
