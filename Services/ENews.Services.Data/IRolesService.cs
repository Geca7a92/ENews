namespace ENews.Services.Data
{
    public interface IRolesService
    {
        string GetAdministratorId();

        string GetRoleNameById(string roleId);

        string GetReporterId();
    }
}
