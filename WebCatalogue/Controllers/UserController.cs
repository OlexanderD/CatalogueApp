using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebCatalogue.ViewModels;

namespace WebCatalogue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly SignInManager<IdentityUser> _signInManager;

        private readonly IMapper _mapper;

        private readonly IValidator <UserViewModel> _validator;

        private readonly ILogger<UserController> _logger;

        public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IMapper mapper, IValidator<UserViewModel> validator, ILogger<UserController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserViewModel userViewModel)
        {
            var validationResult = _validator.Validate(userViewModel);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => new { Property = e.PropertyName, e.ErrorMessage });

                return BadRequest(new { Errors = errors });
            }
            var user = _mapper.Map<IdentityUser>(userViewModel);

            var result = await _userManager.CreateAsync(user,userViewModel.Password);

            if (!result.Succeeded)
            {
                return BadRequest("RegistationFailed");
            }
            await _signInManager.SignInAsync(user,false);

            _logger.LogInformation("Registration Completed");

            return Ok("Registration Completed");
        }
        [HttpGet("{Login}")]
        public async Task<IActionResult> SignIn([FromQuery] UserViewModel userViewModel)
        {
            var validationResult = _validator.Validate(userViewModel);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => new { Property = e.PropertyName, e.ErrorMessage });

                return BadRequest(new { Errors = errors });
            }

            var user = _userManager.FindByNameAsync(userViewModel.UserName);

            if(user == null)
            {
                return BadRequest("Invalid username or password");
            }

            var result = await _signInManager.PasswordSignInAsync(userViewModel.UserName, userViewModel.Password, isPersistent: false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                return BadRequest("Invalid username or password");
            }

            _logger.LogInformation("Login completed");

            return Ok("Login completed");
        }
        [HttpPost("Logout")]
        public async Task<IActionResult> SignOut()
        {
           
            if (!User.Identity.IsAuthenticated)
            {
                return BadRequest("You need to sign in first");
            }

            await _signInManager.SignOutAsync();

            _logger.LogInformation("You have been signed out succesfully");

            return Ok("You have been signed out successfully");
        }
    }
}
