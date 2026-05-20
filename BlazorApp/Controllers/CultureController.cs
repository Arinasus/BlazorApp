using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BlazorApp.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class CultureController : ControllerBase
    {
        [HttpGet]
        public IActionResult SetCulture(string culture, string redirectUri)
        {
            if (culture != null)
            {
                // ИСПРАВЛЕНИЕ: Передаем культуру напрямую в конструктор RequestCulture
                var cookieValue = CookieRequestCultureProvider.MakeCookieValue(
                    new RequestCulture(culture));

                Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    cookieValue,
                    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1), Path = "/" }
                );
            }

            // Перенаправляем обратно на ту страницу, откуда пришел пользователь
            return LocalRedirect(redirectUri ?? "/");
        }
    }
}
