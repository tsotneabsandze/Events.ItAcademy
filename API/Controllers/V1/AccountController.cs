using System;
using System.Threading.Tasks;
using MEDIATOR.Account.Commands.DeleteUser;
using MEDIATOR.Account.Commands.PartialUpdateUser;
using MEDIATOR.Account.Commands.RegisterUser;
using MEDIATOR.Account.Commands.SignInUser;
using MEDIATOR.Account.Queries.GetUserDetails;
using MEDIATOR.Account.Queries.GetUserDetails.GetUserDetailsByEmail;
using MEDIATOR.Account.Queries.GetUserDetails.GetUserDetailsById;
using MEDIATOR.Account.Queries.GetUsersList;
using MEDIATOR.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers.V1
{
    [Authorize]
    [ApiVersion("1.0")]
    public class AccountController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation("gets list of all users")]
        public async Task<ActionResult<UserDetailsVm>> GetAll()
        {
            var vm = await Mediator.Send(new GetUsersListQuery());
            return Ok(vm);
        }

        [HttpGet("{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation("gets user by email")]
        public async Task<ActionResult<UserDetailsVm>> GetUserByEmail(string email)
        {
            var vm = await Mediator.Send(new GetUserDetailsByEmailQuery { Email = email });
            return Ok(vm);
        }


        [HttpGet("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation("gets user by id")]
        public async Task<ActionResult<UserDetailsVm>> GetUserById(string id)
        {
            var vm = await Mediator.Send(new GetUserDetailsByIdQuery { Id = id });
            return Ok(vm);
        }


        [HttpDelete("{email}")]
        [Authorize(policy: "RequireAdminRole")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [SwaggerOperation("Deletes user with provided email")]
        public async Task<IActionResult> DeleteUser(string email)
        {
            await Mediator.Send(new DeleteUserCommand { Email = email });
            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        [SwaggerOperation("Register")]
        public async Task<ActionResult<AuthResult>> Register([FromBody] RegisterUserCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        
        [AllowAnonymous]
        [HttpPost("SignIn")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation("Sign in")]
        public async Task<ActionResult<AuthResult>> SignIn([FromBody] SignInUserCommand command)
        {
            var res = await Mediator.Send(command);
            return Ok(res);
        }


        /// <summary>
        /// update user
        /// </summary>
        /// <param name="id">user id</param>
        /// <param name="command">data</param>
        /// <remarks>
        /// Sample request
        /// 
        ///     {
        ///         "id": "b1fa6849-6ff1-410c-a8fa-01087089c089",
        ///         "email": "grisha@gmail.com",
        ///         "name": "grisha",
        ///         "lastName": "oniani"
        ///     }
        ///
        /// </remarks>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PartiallyUpdateUser(string id, [FromBody] PartialUpdateUserCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }
    }
}