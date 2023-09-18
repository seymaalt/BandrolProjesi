using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ProjeBandrol.Models;

// Add profile data for application users by adding properties to the User class

[Table("Users")]

public class AppUser : IdentityUser
{

    public string TcKimlikNo { get; set; }
    public string Ad { get; set; }
    public string Soyad { get; set; }

    public virtual List<Vehicle> Vehicles { get; set; }

    public virtual List<Bandrol> Bandrols { get; set; }

    public virtual List<Payment> Payments { get; set; }

}

