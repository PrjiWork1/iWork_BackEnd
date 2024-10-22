using IWork.Domain.Queries.UserQuery;
using IWork.Domain.ViewModels;
using IWork.Service.Interfaces;
using IWork.Service.Services;
using MediatR;

namespace IWork.API.Handlers.UserHandler
{
    public class GetByEmailUserQueryHandler : IRequestHandler<GetByEmailUserQuery, UserViewModel>
    {
        private readonly IUserService _userService;

        public GetByEmailUserQueryHandler(IUserService service)
        {
            _userService = service; 
        }

        public async Task<UserViewModel> Handle(GetByEmailUserQuery request, CancellationToken cancellationToken)
        {
            var result = await _userService.GetByEmailAsync(request.Email);

            var user = new UserViewModel
                (
                    result.Id,
                    result.CompleteName,
                    result.UserName,
                    result.Email,
                    result.Role,
                    result.IsActive
                );
            
            return user;
        }
    }
}
