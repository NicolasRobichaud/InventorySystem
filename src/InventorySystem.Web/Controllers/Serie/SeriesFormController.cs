using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using InventorySystem.Data.Manager;
using InventorySystem.Data.Repositories;
using InventorySystem.Data.Entity;
using InventorySystem.Web.ViewModel.Series;
using InventorySystem.Data.Helper;

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
        public IActionResult Edit()
        {
            return View(ViewName);
        }

        [HttpGet]
        [Route("edit/{id}")]
        public async Task<IActionResult> EditAsync(string id)
        {
            var brand = await _baseRepository.LoadAsync<SeriesEntity>(ParserHelper.ToGuid(id));

            return View(ViewName, new SeriesFormViewModel
            {
                Id = brand?.Id.ToString(),
                Name = brand?.Name
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
                    },
                    () =>
                    {
                        return new SeriesEntity
                        {
                            Name = viewModel.Name
                        };
                    });
            }

            return View(ViewName, viewModel);
        }
    }
}
