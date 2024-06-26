using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ChatApp.Domain.Entities
{
    public class AppRole : IdentityRole<Guid>
    {
        [Key]
        public override Guid Id { get; set; }
    }
}
