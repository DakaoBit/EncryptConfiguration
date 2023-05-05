using EncryptConfiguration.Utils;
using Microsoft.AspNetCore.Mvc;

namespace EncryptConfiguration.Controllers
{
    public class SecretsController : ControllerBase
    {
        private readonly IConfiguration config;

        private readonly ConnectionStrings dBConStr;
        private readonly Authentication auth;
        private readonly JwtSettings jwt;

        public SecretsController(IServiceProvider service, IConfiguration config)
        {
            this.config = config;

            this.dBConStr = service.GetService<ConnectionStrings>();
            this.auth = service.GetService<Authentication>();
            this.jwt = service.GetService<JwtSettings>();
        }

        [HttpGet]
        public IActionResult Default()
        {
            return Ok(
                new
                {
                    DBConStr = config["ConnectionStrings:DBConStr"],
                    UserHashKey = config["Authentication:UserHashKey"],
                    JwtIssuer = config["JwtSettings:Issuer"],
                    JwtSignKey = config["JwtSettings:SignKey"],
                }); ;
        }

        [HttpGet]
        public IActionResult Encrypt()
        {
            return Ok(
                new
                {
                    DBConStr = dBConStr.DBConStr,
                    UserHashKey = auth.UserHashKey,
                    JwtIssuer = jwt.Issuer,
                    JwtSignKey = jwt.SignKey
                }); ;
        }
    }
}
