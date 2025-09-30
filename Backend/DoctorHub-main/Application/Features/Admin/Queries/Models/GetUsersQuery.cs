using Application.Bases;
using Application.Features.Admin.Queries.Responses;
using MediatR;

namespace Application.Features.Admin.Queries.Models

{
    public class GetUsersQuery : IRequest<Response<List<UserResponse>>>
    {
        public string Role { get; set; }
        public bool? IsApproved { get; set; }
    }
}
