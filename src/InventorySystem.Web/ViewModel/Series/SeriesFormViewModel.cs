using Microsoft.AspNet.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InventorySystem.Web.ViewModel.Series
{
    public class SeriesFormViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        [Required(ErrorMessage = "La marque est requise")]
        public Guid? BrandId { get; set; }

        public IEnumerable<SelectListItem> Brands { get; set; } = new List<SelectListItem>();
    }
}
