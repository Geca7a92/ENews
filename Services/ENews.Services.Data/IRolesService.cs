using System;
using System.Collections.Generic;
using System.Text;

namespace ENews.Services.Data
{
    public interface IRolesService
    {
        string GetAdministratorId();

        string GetRoleNameById(string roleId);
    }
}
