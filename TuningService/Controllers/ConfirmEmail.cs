using MediatR;
using TuningService.DTOs;
using TuningService.Models;
using TuningService.Services;
using TuningService.Settings;

namespace TuningService.Controllers
{
    public class ConfirmEmail
    {
        public class Command : IRequest<ApiBaseResultModel>
        {
            public Command(ConfirmEmailDto model)
            {
                Model = model;
            }
            public ConfirmEmailDto Model { get; set; }
        }

        public class Handler : IRequestHandler<Command, ApiBaseResultModel>
        {
            private readonly UserService _userService;

            public Handler(UserService userService)
            {
                _userService = userService;
            }

            public async Task<ApiBaseResultModel> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var model = request.Model;
                    var result = await _userService.ConfirmEmailAsync(model.UserId, model.Token);

                    if (result.Succeeded)
                    {
                        return new ApiBaseResultModel();
                    }

                    return new ApiBaseResultModel("Email confirmation failed.");
                }
                catch (Exception ex)
                {
                    return new ApiBaseResultModel(ErrorHelper.GetError(ErrorHelperType.ERROR_INTERNAL, ex.Message));
                }
            }
        }
    }
}
