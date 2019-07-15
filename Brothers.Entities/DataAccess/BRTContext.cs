using Brothers.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brothers.Entities.DataAccess
{
    class BRTContext:DbContext
    {
        public DbSet<utblMstArticleEnquire> utblMstArticleEnquires { get; set; }
        public DbSet<utblGenEnquiry> utblGenEnquiries { get; set; }
        public DbSet<utblMstCountry> utblMstCountries { get; set; }
        public DbSet<utblMstDestination> utblMstDestinations { get; set; }
        public DbSet<utblMstPackageType> utblMstPackageTypes { get; set; }
        public DbSet<utblMstActivity> utblMstActivities { get; set; }
        public DbSet<utblMstTourPackage> utblMstTourPackages { get; set; }
        public DbSet<utblMstTourPackageActivity> utblMstTourPackageActivities { get; set; }
        public DbSet<utblMstTourPackagePic> utblMstTourPackagePics { get; set; }
        public DbSet<utblMstTourPackageMap> utblMstTourPackageMaps { get; set; }
        public DbSet<utblTourPackageBooking> utblTourPackageBookings { get; set; }
        public DbSet<utblGenCodeSeed> utblGenCodeSeeds { get; set; }
        public DbSet<utblBlog> utblBlogs { get; set; }


    }
}
    