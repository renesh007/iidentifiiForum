using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Forum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        /// <summary>
        /// Gets the current user's ID from claims.
        /// Returns null if the claim is missing or invalid.
        /// </summary>
        protected Guid UserId
        {
            get
            {
                Claim? claim = User.FindFirst(ClaimTypes.NameIdentifier);

                if (claim == null)
                {
                    throw new UnauthorizedAccessException("User ID claim missing or invalid.");
                }

                Guid.TryParse(claim.Value, out Guid userId);
                return userId;
            }
        }
    }
}
