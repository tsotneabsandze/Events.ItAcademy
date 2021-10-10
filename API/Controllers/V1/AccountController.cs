using System.Threading.Tasks;
using MEDIATOR.Account.Commands.DeleteUser;
using MEDIATOR.Account.Commands.RegisterUser;
using MEDIATOR.Account.Queries.GetUserDetails;
using MEDIATOR.Account.Queries.GetUsersList;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1
{
    [ApiVersion("1.0")]
    public class AccountController : BaseApiController
    {

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserDetailsVm>> GetAll()
        {
            var vm = await Mediator.Send(new GetUsersListQuery());
            return Ok(vm);
        }

        [HttpGet("{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDetailsVm>> GetUserByEmail(string email)
        {
            var vm = await Mediator.Send(new GetUserDetailsQuery { Email = email});
            return Ok(vm);
        }


        [HttpDelete("{email}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser(string email)
        {
            await Mediator.Send(new DeleteUserCommand{Email = email});
            return NoContent();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterUserCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}