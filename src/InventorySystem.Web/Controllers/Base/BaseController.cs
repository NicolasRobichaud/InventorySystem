using InventorySystem.Data.Entity;
using InventorySystem.Data.Repositories;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventorySystem.Web.Controllers.Base
{
    public class BaseController : Controller
    {
        protected const string EditFormViewName = "EditForm";
        protected readonly BaseRepository _baseRepository;

        public BaseController(BaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

        protected async Task<List<SelectListItem>> GetBrands(Guid? currentBrandId)
        {
            var brandEntities = await _baseRepository.ToListAsync(() => _baseRepository.Query<BrandEntity>());
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
