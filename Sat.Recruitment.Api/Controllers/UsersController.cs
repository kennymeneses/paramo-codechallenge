using BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        ILogger<UsersController> _logger;
        IUserManager _manager;
        public UsersController(IUserManager manager, ILogger<UsersController> logger)
        {
            _manager = manager;
            _logger = logger;
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                if (!string.IsNullOrEmpty(ValidateErrors(user)))
                {
                    var _result = new Result()
                    {
                        IsSuccess = false,
                        Errors = ValidateErrors(user)
                    };

                    return BadRequest(_result);
                }

                var result = await _manager.CreateUser(user);
                return StatusCode(201, result);

            }
            catch (Exception ex)
            {
                _logger.LogError("An error ocurred in /created-user controller: " + ex.Message);
                throw;
            }
        }

        private string ValidateErrors(User user)
        {
            StringBuilder sb = new StringBuilder();

            if (string.IsNullOrEmpty(user.Name))

                sb.Append(Constants.nameRequired);
            if (string.IsNullOrEmpty(user.Email))

                sb.Append(Constants.emailRequired);
            if (string.IsNullOrEmpty(user.Address))

                sb.Append(Constants.adressRequired);
            if (string.IsNullOrEmpty(user.Phone))

                sb.Append(Constants.phoneRequired);

            return sb.ToString();
        }
    }
}
