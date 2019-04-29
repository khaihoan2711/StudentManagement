using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StudentManagement_ASP.NET_MCV5.Startup))]
namespace StudentManagement_ASP.NET_MCV5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
