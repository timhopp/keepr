using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Keepr.Services;
using Keppr.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class VaultsController : ControllerBase
  {
    private readonly VaultsService _vs;
    public VaultsController(VaultsService vs)
    {
      _vs = vs;
    }

    [HttpGet("{id}")]
    public ActionResult<Vault> GetById(int id)
    {
      try
      {
        return Ok(_vs.GetById(id));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    //NOTE Need to test this still once front end user is established
    [HttpGet("user")]
    public ActionResult<Vault> GetMyKeeps()
    {
      try
      {
        Claim user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (user == null)
        {
          throw new Exception("You must be logged in to get your Keeps, sir");
        }

        return Ok(_vs.GetMyVaults(user.Value));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpPost]
    [Authorize]
    public ActionResult<Vault> Post([FromBody] Vault newVault)
    {
      try
      {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        newVault.UserId = userId;
        return Ok(_vs.Create(newVault));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // [Authorize]
    [HttpDelete("{id}")]
    //user.Value,
    public ActionResult<Vault> Delete(int id)
    {
      try
      {
        // Claim user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        // if (user == null)
        // {
        //   throw new Exception("You must be logged in to create a Vault, sir");
        // }

        return Ok(_vs.Delete(id));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [Authorize]
    [HttpPut("{id}")]
    public ActionResult<Vault> Update(int id, [FromBody] Vault updatedVault)
    {
      try
      {
        Claim user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (user == null)
        {
          throw new Exception("You must be logged in to make a Vault, sir.");
        }
        updatedVault.UserId = user.Value;
        updatedVault.Id = id;
        return Ok(_vs.Update(updatedVault));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }



  }
}