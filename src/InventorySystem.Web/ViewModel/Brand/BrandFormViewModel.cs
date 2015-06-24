using System;
using System.ComponentModel.DataAnnotations;

namespace InventorySystem.Web.ViewModel.Brand
{
    public class BrandFormViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Le nom est requis")]
        public string Name { get; set; }
    }
}
