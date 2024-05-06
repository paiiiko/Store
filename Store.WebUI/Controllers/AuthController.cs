using Microsoft.AspNetCore.Mvc;
using Store.Domain.Interfaces;
using Store.WebUI.ViewModels;
using Store.Domain.Model;
using Store.Domain.Tools;
using Store.Domain.Tools.Security;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Store.WebUI.Controllers
{
    public class AuthController : Controller
    {
        private IUsersDbContext db;
        private readonly IConfiguration Config;
        public AuthController(IUsersDbContext _db, IConfiguration _config)
        {
            db = _db;
            Config = _config;
        }

        [HttpGet]
        public ViewResult Login()
        {
            return View();
        }

        [HttpGet]
        public ViewResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var pathBase = HttpContext.Request.HttpContext;
                User user = db.FindByEmail(viewModel.Email).Result;
                if (user != null && 
                    user.Email == viewModel.Email &&
                    user.PasswordHash == AuthSecurity.GenerateHash(viewModel.Password, AuthSecurity.StringToByte(user.Salt)))
                {
                    var options = Config.GetSection("Jwt").Get<AuthOptions>();
                    SymmetricSecurityKey key = new SymmetricSecurityKey(AuthSecurity.StringToByte(options.SecretKey));
                    var jwt = new JwtSecurityToken(issuer: options.Issuer,
                                                   audience: options.Audience,
                                                   expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(options.TokenLifetime)),
                                                   signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
                    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                    string tokenString = handler.WriteToken(jwt);
                    pathBase.Response.Cookies.Append("token", tokenString);
                    return RedirectToAction("CartList", "Cart");
                }
                else
                {
                    ModelState.AddModelError("Password", "Введёна неверная почта или пароль, повторите попытку.");
                }
            }
            return View(viewModel);
        }
        [HttpPost]
        public async Task<ActionResult> Registration(RegistrationViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (db.FindByEmail(viewModel.Email).Result == null)
                {
                    byte[] salt = AuthSecurity.GenerateSalt();
                    User user = new User()
                    {
                        Name = viewModel.Name,
                        Email = viewModel.Email,
                        Salt = AuthSecurity.BytesToString(salt),
                        PasswordHash = AuthSecurity.GenerateHash(viewModel.Password, salt),
                        Cart = new Cart()
                    };
                    db.Add(user);
                    await db.SaveChangesAsync(CancellationToken.None);
                    string url = Url.Action("EmailConfirmation", "Auth", new {id = user.Id}, protocol: Request.Scheme);
                    EmailSender.SendRegistationConfirmingEmail(user.Email, url);
                    return RedirectToAction("RegistrationEnd", "Auth");
                }
                else
                {
                    ModelState.AddModelError("Email", "Эта почта уже зарегистрирована");
                }
            }
            return View(viewModel);
        }
        [HttpGet]
        public ViewResult RegistrationEnd()
        {

            return View("RegistrationEnd");
        }
        [HttpPut]
        public ViewResult EmailConfirmation(int id)
        {
            User? user = db.FindById(id);
            user.Confirmed = true;
            db.SaveChangesAsync(CancellationToken.None);
            return View("EmailConfirmation");
        }
    }
}
