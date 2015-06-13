using System;
using System.ComponentModel.DataAnnotations;

namespace InventorySystem.Web.ViewModel.Company
{
    public class CompanyFormViewModel
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "Le nom est requis")]
        public string Name { get; set; }
    }
}
