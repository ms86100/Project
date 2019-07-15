using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Brothers.Entities.Models
{
    public class utblMstTourPackage
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long PackageID { get; set; }
        [Required(ErrorMessage="Enter the package name.")]
        [StringLength(128)]
        [DisplayName("Tour Package Name")]
        public string PackageName { get; set; }
        [StringLength(512)]
        [DisplayName("Tour Package Routing")]
        public string PackageRouting { get; set; }
        [Required(ErrorMessage = "Enter pick-up point.")]
        [StringLength(128)]
        [DisplayName("Pick-up Point")]
        public string PickupPoint { get; set; }
        [Required(ErrorMessage = "Enter drop point.")]
        [StringLength(128)]
        [DisplayName("Drop Point")]
        public string DropPoint { get; set; }
        [Required(ErrorMessage = "Enter no. of days for the package.")]
        [DisplayName("Number of Days")]
        public Int16 TotalDays { get; set; }

        [Required(ErrorMessage = "Price per person.")]
        [DisplayName("Price/Person")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Select package type.")]
        [DisplayName("Package Type")]
        public long PackageTypeID { get; set; }
        [DisplayName("Package Remarks")]
        public string PackageRemarks { get; set; }
        public long PackageHitCount { get; set; }
    }
}
