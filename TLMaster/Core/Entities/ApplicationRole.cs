using System;
using Microsoft.AspNetCore.Identity;

namespace TLMaster.Core.Entities;

public class ApplicationRole : IdentityRole<Guid>
{
    public ApplicationRole()
    {

    }

    public ApplicationRole(string roleName) : base(roleName)
    {

    }

}
