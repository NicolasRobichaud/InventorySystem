using InventorySystem.Web.ViewModel.Base;
using Microsoft.AspNet.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InventorySystem.Web.ViewModel.Series
{
    public class SeriesFormViewModel : BaseFormViewModel
    {
        [Required(ErrorMessage = "La marque est requise")]
        public Guid? BrandId { get; set; }

        public IEnumerable<SelectListItem> Brands { get; set; } = new List<SelectListItem>();
    }
}
