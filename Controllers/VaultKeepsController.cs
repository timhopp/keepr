using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Keepr.Controllers
{
  [Authorize]
  [ApiController]
  [Route("api/[controller]")]
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepsService _vks;

    public VaultKeepsController(VaultKeepsService vks)
    {
      _vks = vks;
    }

    [HttpPost]
    public ActionResult<VaultKeep> Create([FromBody] VaultKeep newVaultKeep)
    {
      try
      {
        Claim user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (user == null)
        {
          throw new Exception("You must be logged in to add a keep to a vault");
        }
        newVaultKeep.UserId = user.Value;
        return Ok(_vks.Create(newVaultKeep));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpDelete("{id}")]
    public ActionResult<VaultKeep> Delete(int id)
    {
      try
      {
        Claim user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (user == null)
        {
          throw new Exception("You must be logged in to delete a vault Keep, sir");
        }

        return Ok(_vks.Delete(user.Value, id));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }

    }

    // [HttpGet]
    // public ActionResult<IEnumerable<VaultKeep>> Get()
    // {
    //   try
    //   {
    //     Claim user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
    //     if (user == null)
    //     {
    //       throw new Exception("You must be logged in to view your vault keeps");
    //     }
    //     return Ok(_vks.Get(user.Value));
    //   }
    //   catch (Exception err)
    //   {
    //     return BadRequest(err.Message);
    //   }
    // }

  }

}
