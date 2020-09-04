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
  [ApiController]
  [Route("api/[controller]")]
  public class KeepsController : ControllerBase
  {
    private readonly KeepsService _ks;
    public KeepsController(KeepsService ks)
    {
      _ks = ks;
    }
    [HttpGet]
    public ActionResult<IEnumerable<Keep>> Get()
    {
      try
      {
        return Ok(_ks.Get());
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      };
    }
    [HttpGet("{id}")]
    public ActionResult<Keep> GetById(int id)
    {
      try
      {
        return Ok(_ks.GetById(id));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpPost]
    //NOTE  ADD BACK WHEN GETTING USER AUTH
    // [Authorize]
    public ActionResult<Keep> Post([FromBody] Keep newKeep)
    {
      try
      {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        newKeep.UserId = userId;
        return Ok(_ks.Create(newKeep));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    //[Authorize]
    [HttpDelete("{id}")]
    public ActionResult<Keep> Delete(int id)
    {
      try
      {
        Claim user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (user == null)
        {
          throw new Exception("You must be logged in to create a Keep, sir");
        }
        return Ok(_ks.Delete(user.Value, id));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    //[Authorized]
    [HttpPut("{id}")]
    public ActionResult<Keep> Update(int id, [FromBody] Keep updatedKeep)
    {
      try
      {
        //NOTE  Add back after testing!!
        //  Claim user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        //         if (user == null)
        //         {
        //             throw new Exception("You must be logged in to make a Keep, sir.");
        //         }
        // updatedKeep.UserId = user.Value;
        // updatedKeep.Id = id;
        return Ok(_ks.Update(updatedKeep));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }






  }
}