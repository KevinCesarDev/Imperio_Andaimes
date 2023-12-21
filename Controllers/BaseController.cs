using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using TCC.Database;
using TCC.Models;
using TCC.Repository.Usuario;


namespace TCC.Controllers;

public class BaseController : Controller
{
    private readonly IUsuarioRepository _usuarioRepository;

    public BaseController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public BaseController()
    {
        var name = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Name);
        string nome = name?.Value;
    }

}