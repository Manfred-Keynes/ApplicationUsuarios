using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PostgreSQLEF.Models;
using PostgreSQLEF.Models.DB;
using PostgreSQLEF.Models.ViewModels;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PostgreSQLEF.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationUsuariosContext _contexto;
        private readonly IConfiguration _config;
        public HomeController(ApplicationUsuariosContext contexto, IConfiguration config)
        {
            _contexto = contexto;
            _config = config;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {

            return View(await _contexto.Usuarios.ToListAsync());
        }

        //public IActionResult Login()
        //{
        //    UsuarioM usuario = new UsuarioM();
        //    return View(usuario);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Usuario _usuario)
        {
            EncriptarContrasenia(_usuario.Contrasenia, out string contraseniaHash);

            var _usuarioe= _contexto.Usuarios.FirstOrDefault(x => x.Usuario1 == _usuario.Usuario1 && x.Contrasenia == contraseniaHash);
            
            if (_usuarioe != null)
            {
                //string token = CreateToken(user);
                //var claims = new[]
                //{
                //    new Claim(ClaimTypes.NameIdentifier, _usuarioe.Id.ToString()),
                //    new Claim(ClaimTypes.Name, _usuarioe.Nombre.ToString()),
                //};

                ///*Generacion del token*/
                //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
                //var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                //var tokenDescriptor = new SecurityTokenDescriptor
                //{
                //    Subject = new ClaimsIdentity(claims),
                //    Expires = DateTime.Now.AddDays(1),
                //    SigningCredentials = credenciales,
                //};

                //var tokenHandler = new JwtSecurityTokenHandler();
                //var token = tokenHandler.CreateToken(tokenDescriptor);
                
                //var _usuarioT = new UsuarioT() {
                //    Usuario = claims[1].Value.ToString(),
                //    Token = tokenHandler.WriteToken(token)
                //};

                //if (_usuarioT.Token == null)
                //{
                //    TempData["alert"] = "los datos son incorrectos";
                //    return View();
                //}
                //var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                //identity.AddClaim(new Claim(ClaimTypes.Name, _usuarioT.Usuario));
                //var principal = new ClaimsPrincipal(identity);
                //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                //HttpContext.Session.SetString("JWToken", _usuarioT.Token);
                //TempData["alert"] = "Bienvenido/a" + _usuarioT.Usuario;
                //return RedirectToAction("Index");   
                //return Ok(new
                //{
                //    usuario = claims[1].Value.ToString(),
                //    token = tokenHandler.WriteToken(token)
                //});
                return RedirectToAction("Index");

            }
            else
            {
                return View();
            }
            
        }

        private string CreateToken(Usuario user)
        {

            return string.Empty;
        }

        //public async Task<IActionResult> Login(UsuarioM obj)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        UsuarioM objUser = await _accRepo.LoginAsync(CT.RutaUsuarios + "Login", obj);
        //        if (objUser.Token == null)
        //        {
        //            TempData["alert"] = "Los datos son incorrectos";
        //            return View();
        //        }

        //        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
        //        identity.AddClaim(new Claim(ClaimTypes.Name, objUser.Usuario));

        //        var principal = new ClaimsPrincipal(identity);
        //        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        //        HttpContext.Session.SetString("JWToken", objUser.Token);
        //        TempData["alert"] = "Bienvenido/a " + objUser.Usuario;
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}

        public ActionResult Registrar()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Registrar(Usuario _usuario)
        {
            _contexto.Add(_usuario);
            _contexto.SaveChanges();
            return View();
        }
        [HttpGet]
        public IActionResult Crear()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(UsuarioM _usuario)
        {
            if (ModelState.IsValid)
            {

                Usuario usuario = new Usuario();

                usuario.Nombre = _usuario.Nombre;
                usuario.Apellido = _usuario.Apellido;
                usuario.Telefono = _usuario.Telefono;
                usuario.Puesto = _usuario.Puesto;
                usuario.Email = _usuario.Email;
                GenerarCredenciales(_usuario.Nombre, _usuario.Apellido, out string nombreUsuario);
                usuario.Usuario1 = nombreUsuario;
                usuario.FechaNacimiento = _usuario.FechaNacimiento;
                EncriptarContrasenia(_usuario.Contrasenia, out string contraseniaHash);
                usuario.Contrasenia = contraseniaHash;

                try
                {
                    _contexto.Usuarios.Add(usuario);
                    await _contexto.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    return View(ex);

                }

            }

            return View();
        }

        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = _contexto.Usuarios.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        private void GenerarCredenciales(string nombre, string apellido, out string nombreUsuario)
        {



            if (apellido == null)
            {
                nombreUsuario = nombre;
            }
            else
            {
                if (nombre.Length >= 3 && apellido.Length >= 3)
                {
                    string concatenado = nombre + apellido.Substring(0, 3);

                    nombreUsuario = concatenado.ToUpper();
                }
                else
                {
                    nombreUsuario = null;
                }
            }


        }
        private void EncriptarContrasenia(string clave, out string contraseniaHash)
        {
            if (clave == null)
            {
                contraseniaHash = null;
            }
            else
            {
                SHA256 sHA256 = SHA256Managed.Create();
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] stream = null;
                StringBuilder sb = new StringBuilder();
                stream = sHA256.ComputeHash(Encoding.UTF8.GetBytes(clave));
                for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
                contraseniaHash = sb.ToString();
            }

        }
    }
}