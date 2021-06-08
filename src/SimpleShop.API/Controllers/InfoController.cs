using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleShop.API.Core;
using System.Reflection;

namespace SimpleShop.API.Controllers
{

    public class InfoController : BaseApiController
    {
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get()
        {
            var assembly = Assembly.GetExecutingAssembly();

            var info = new Info()
            {
                Name = assembly.GetName().Name,
                Version = assembly.GetName().Version.ToString(),
            };
            return Ok(info);
        }

        public class Info
        {
            public string Name { get; set; }
            public string Version { get; set; }
        }
    }
}
