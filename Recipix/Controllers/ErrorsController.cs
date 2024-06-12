using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;

namespace Recipix.Controllers;

public class ErrorsController : ControllerBase{

    [Route("/error")]
    public IActionResult Error(){
        return Problem();
    }

}