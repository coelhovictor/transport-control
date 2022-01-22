namespace Core.Domain.Account
{
    public interface ISeedUserRoleInitial
    {
        void SeedUsers(string passUsuario, string passAdmin);
        void SeedRoles();
    }
}
