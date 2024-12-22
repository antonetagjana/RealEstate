// In WebApplication2.Registers namespace
using Lamar;
using WebApplication2.Data;
using WebApplication2.Repositories.Property;
using WebApplication2.Repositories.PropertyPhoto;
using WebApplication2.Repositories.Reservation;
using WebApplication2.Repositories.Role;
using WebApplication2.Repositories.User;
using WebApplication2.Repositories.UserRole;
using WebApplication2.Services.Property;
using WebApplication2.Services.PropertyPhoto;
using WebApplication2.Services.Reservation;
using WebApplication2.Services.Role;
using WebApplication2.Services.User;
using WebApplication2.Services.UserRoles;

namespace WebApplication2.Registers
{
    public class ServiceRegistry : Lamar.ServiceRegistry
    {
        public ServiceRegistry()
        {
            For<ApplicationDbContext>().Use<ApplicationDbContext>().Scoped();
            For<IRoleRepository>().Use<RoleRepository>().Scoped();
            For<IUserRepository>().Use<UserRepository>().Scoped();
            For<IPropertyRepository>().Use<PropertyRepository>().Scoped();
            For<IReservationRepository>().Use<ReservationRepository>().Scoped();
            For<IPropertyPhotoRepository>().Use<PropertyPhotoRepository>().Scoped();
            For<IUserRoleRepository>().Use<UserRoleRepository>().Scoped();
            
            For<IRoleService>().Use<RoleService>().Scoped();
            For<IUserService>().Use<UserService>().Scoped();
            For<IPropertyService>().Use<PropertyService>().Scoped();
            For<IReservationService>().Use<ReservationService>().Scoped();
            For<IPropertyPhotoService>().Use<PropertyPhotoService>().Scoped();
            For<IUserRolesService>().Use<UserRoleService>().Scoped();
        }
    }
}