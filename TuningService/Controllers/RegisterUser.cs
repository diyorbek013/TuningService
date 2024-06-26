using MediatR;
using System.Net;
using Tuning.Library.Base;
using TuningService.DTOs;
using TuningService.Services;
using TuningService.Settings;

namespace TuningService.Controllers
{
    public class RegisterUser
    {
        public class Command : IRequest<ApiBaseResultModel>
        {
            public Command(UserRegistrationDto model)
            {
                Model = model;
            }
            public UserRegistrationDto Model { get; set; }
        }

        public class Handler : IRequestHandler<Command, ApiBaseResultModel>
        {
            private readonly UserService _userService;
            private readonly EmailService _emailService;

            public Handler(UserService userService, EmailService emailService)
            {
                _userService = userService;
                _emailService = emailService;
            }

            public async Task<ApiBaseResultModel> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var model = request.Model;

                    if (model != null)
                    {
                        var user = await _userService.RegisterUserAsync(model);
                        var token = await _userService.GenerateEmailConfirmationTokenAsync(user);

                        var encodedToken = WebUtility.UrlEncode(token);

                        var confirmationLink = $"https://localhost:7025/api/auth/confirmemail?userId={user.Id}&token={encodedToken}";
                        var subject = "Confirm your email";
                        var message = $"Please confirm your email by clicking on this link: {confirmationLink}";

                        await _emailService.SendEmailAsync(user.Email, subject, message);
                    }

                    return new ApiBaseResultModel();
                }
                catch (Exception ex)
                {
                    return new ApiBaseResultModel(ErrorHelper.GetError(ErrorHelperType.ERROR_INTERNAL, ex.Message));
                }
            }
        }
    }
}
