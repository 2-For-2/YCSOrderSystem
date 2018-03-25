using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using YCSOrderSystem.Models;

[assembly: OwinStartupAttribute(typeof(YCSOrderSystem.Startup))]
namespace YCSOrderSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesAndUsers();
        }
        //this method creates user roles and an admin user for login
        private void createRolesAndUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            //Creating the first admin role and user on startup
            if(!roleManager.RoleExists("Admin"))
            {
                //1) create Admin Role
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //2) Create Admin Superuser
                var user = new ApplicationUser();
                user.UserName = "YCSAdmin";
                user.Email = "YaseenCatering@gmail.com";
                string userPWD = "YCSPassword:1";
                var chkUser = userManager.Create(user, userPWD);

                //3) Add the Admin User to the Admin role
                if(chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "Admin");
                }
            }

            //Creating the Manager Role
            if(!roleManager.RoleExists("Manager"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Manager";
                roleManager.Create(role);
            }
            //Creating the Employee Role
            if(!roleManager.RoleExists("Employee"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Employee";
                roleManager.Create(role);
            }

            //Creating Customer Role
            if(!roleManager.RoleExists("Customer"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Customer";
                roleManager.Create(role);
            }
        }
    }
}
