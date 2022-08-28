using Blockbuster.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blockbuster.UI.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        protected IActionResult Return<TError, TSuccess>(Either<TError, TSuccess> result)
        {
            if (result.IsFailure)
            {
                return StatusCode(result.StatusCode, result.Error);
            }

            if (result.Value != null)
            {
                return StatusCode(result.StatusCode, result.Value);
            }

            return NoContent();
        }
    }
}