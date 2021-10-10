using System.Threading.Tasks;
using MEDIATOR.Account.Queries.GetUserDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1
{
    [ApiVersion("1.0")]
    public class AccountController : BaseApiController
    {
        /// <summary>
        /// gets user details by id
        /// </summary>
        /// <param name="email">user email</param>
        /// <returns></returns>
        [HttpGet("{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDetailsVm>> GetUserByEmail(string email)
        {
            var vm = await Mediator.Send(new GetUserDetailsQuery { Email = email});
            return Ok(vm);
        }
    }
}