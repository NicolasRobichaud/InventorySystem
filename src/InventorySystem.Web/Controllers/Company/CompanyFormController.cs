using InventorySystem.Data.Entity;
using InventorySystem.Data.Manager;
using InventorySystem.Data.Repositories;
using InventorySystem.Web.ViewModel.Company;
using Microsoft.AspNet.Mvc;
using Raven.Client;
using System;
using System.Threading.Tasks;

namespace InventorySystem.Web.Company.Controllers
{
    [Route("company")]
    public class CompanyFormController : Controller
    {
        private const string ViewName = "EditForm";
        private readonly IDocumentStoreManager _database;
        private readonly BaseRepository _baseRepository;

        public CompanyFormController(IDocumentStoreManager database, BaseRepository baseRepository)
        {
            _database = database;
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
        public async Task<IActionResult> EditAsync(Guid id)
        {
            using (var session = _database.Store.OpenAsyncSession())
            {
                var company = await session.LoadAsync<CompanyEntity>(id);

                return View(ViewName, new CompanyFormViewModel
                {
                    Id = company?.Id,
                    Name = company?.Name
                });
            }
        }

        [HttpPost]
        [Route("edit/{id?}")]
        public async Task<IActionResult> SubmitAsync(CompanyFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.Id.HasValue)
                {
                    var exist = await _baseRepository.UpdateAsync<CompanyEntity>(viewModel.Id.Value, c =>
                    {
                        c.Name = viewModel.Name;
                    });

                    if (!exist)
                    {
                        viewModel.Id = null;
                    }
                }

                if (!viewModel.Id.HasValue)
                {
                    await _baseRepository.CreateAsync(new CompanyEntity
                    {
                        Name = viewModel.Name
                    });
                }
            }

            return View(ViewName, viewModel);
        }
    }
}
