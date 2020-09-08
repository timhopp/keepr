using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Keepr.Models;
using Keepr.Services;
using Keppr.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Keepr.Controllers
{
  [Authorize]
  [ApiController]
  [Route("api/[controller]")]
  public class VaultsController : ControllerBase
  {
    private readonly VaultsService _vs;
    private readonly VaultKeepsService _vks;
    public VaultsController(VaultsService vs, VaultKeepsService vks)
    {
      _vs = vs;
      _vks = vks;

    }
    [Authorize]
    [HttpGet("{id}")]
    public ActionResult<Vault> GetById(int id)
    {
      try
      {
        Claim user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (user == null)
        {
          throw new Exception("You must be logged in to get your Keeps, sir");
        }
        return Ok(_vs.GetById(user.Value, id));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [Authorize]
    [HttpGet]
    public ActionResult<Vault> GetMyVaults()
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

    [Authorize]
    [HttpGet("{vaultId}/keeps")]
    public ActionResult<VaultKeep> GetKeepsByVaultId(int vaultId)
    {
      try
      {
        return Ok(_vks.GetKeepsByVaultId(vaultId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpPost]
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

    [Authorize]
    [HttpDelete("{id}")]
    public ActionResult<Vault> Delete(int id)
    {
      try
      {
        Claim user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (user == null)
        {
          throw new Exception("You must be logged in to create a Vault, sir");
        }

        return Ok(_vs.Delete(user.Value, id));
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