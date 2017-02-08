using API;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace API
{
    public  class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
