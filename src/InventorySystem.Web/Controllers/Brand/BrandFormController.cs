using InventorySystem.Data.Entity;
using InventorySystem.Data.Helper;
using InventorySystem.Data.Repositories;
using InventorySystem.Web.ViewModel.Brand;
using Microsoft.AspNet.Mvc;
using System.Threading.Tasks;


namespace InventorySystem.Web.Brand.Controllers
{
    [Route("brand")]
    public class BrandFormController : Controller
    {
        private const string ViewName = "EditForm";
        private readonly BaseRepository _baseRepository;

        public BrandFormController(BaseRepository baseRepository)
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
            var brand = await _baseRepository.LoadAsync<BrandEntity>(ParserHelper.ToGuid(id));

            return View(ViewName, new BrandFormViewModel
            {
                Id = brand?.Id.ToString(),
                Name = brand?.Name
            });
        }

        [HttpPost]
        [Route("edit/{id?}")]
        public async Task<IActionResult> SubmitAsync(BrandFormViewModel viewModel)
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
                        return new BrandEntity
                        {
                            Name = viewModel.Name
                        };
                    });
            }

            return View(ViewName, viewModel);
        }
    }
}
