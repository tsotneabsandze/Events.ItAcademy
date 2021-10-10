using System.Threading.Tasks;
using MEDIATOR.Account.Queries.GetUserDetails;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1
{
    [ApiVersion("1.0")]
    public class AccountController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<UserDetailsVm>> Get(string email)
        {
            var vm = await Mediator.Send(new GetUserDetailsQuery { Email = email});
            return Ok(vm);
        }
    }
}