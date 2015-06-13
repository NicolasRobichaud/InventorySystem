using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InventorySystem.Web.ViewModel
{
    public class AddBuildingFormViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "L'identifiant de l'item est requis")]
        public string ItemId { get; set; }

        [Required(ErrorMessage = "Le nom est requis")]
        public string Name { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? SerieId { get; set; }
        public string Description { get; set; }
        public double? UsPrice { get; set; }
        public double? CdnPrice { get; set; }
        public double? PaidPrice { get; set; }
        public DateTime? Introduced { get; set; }
        public DateTime? Retired { get; set; }
        public string ImageUrl { get; set; }
        public List<Guid> Connections { get; set; } = new List<Guid>();
        public string IncludedParts { get; set; }
    }
}
