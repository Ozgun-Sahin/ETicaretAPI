using MediatR;

namespace ETicaretAPI.Application.Features.Queries.AppUser.GetRolesToUser
{
    public class GetRolestoUserQueryRequest :IRequest<GetRolesToUserQueryResponse>
    {
        public string UserId { get; set; }
    }
}