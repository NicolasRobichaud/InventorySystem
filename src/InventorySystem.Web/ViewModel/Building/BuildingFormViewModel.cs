using InventorySystem.Web.ViewModel.Base;
using Microsoft.AspNet.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InventorySystem.Web.ViewModel.Building
{
    public class BuildingFormViewModel : BaseFormViewModel
    {
        [Required(ErrorMessage = "L'identifiant de l'item est requis")]
        public string ItemId { get; set; }

        public Guid? BrandId { get; set; }
        public Guid? SerieId { get; set; }
        public string Description { get; set; }
        public double? PriceUsDollar { get; set; }
        public double? PriceCanadianDollar { get; set; }
        public double? PricePaid { get; set; }
        public DateTime? Introduced { get; set; }
        public DateTime? Retired { get; set; }
        public string ImageUrl { get; set; }
        public List<Guid> Connections { get; set; } = new List<Guid>();
        public string IncludedParts { get; set; }

        public IEnumerable<SelectListItem> Brands { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Series { get; set; } = new List<SelectListItem>();
    }
}
