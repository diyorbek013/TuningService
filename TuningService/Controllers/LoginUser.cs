using MediatR;
using TuningService.DTOs;
using TuningService.Models;
using TuningService.Services;
using TuningService.Settings;

namespace TuningService.Controllers
{
    public class LoginUser
    {
        public class Command : IRequest<ApiBaseResultModel>
        {
            public Command(LoginDto model)
            {
                Model = model;
            }
            public LoginDto Model { get; set; }
        }

        public class Handler : IRequestHandler<Command, ApiBaseResultModel>
        {
            private readonly UserService _userService;
            private readonly JwtService _jwtService;

            public Handler(UserService userService, JwtService jwtService)
            {
                _userService = userService;
                _jwtService = jwtService;
            }

            public async Task<ApiBaseResultModel> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var model = request.Model;
                    var user = await _userService.FindByEmailAsync(model.Email);

                    if (user != null && await _userService.CheckPasswordAsync(user, model.Password))
                    {
                        var roles = await _userService.GetUserRolesAsync(user);
                        var token = _jwtService.GenerateToken(user, roles);
                        return new ApiBaseResultModel { Data = token };
                    }

                    return new ApiBaseResultModel("Invalid login attempt.");
                }
                catch (Exception ex)
                {
                    return new ApiBaseResultModel(ErrorHelper.GetError(ErrorHelperType.ERROR_INTERNAL, ex.Message));
                }
            }
        }
    }
}
