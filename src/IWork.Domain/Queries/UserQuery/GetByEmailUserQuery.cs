using IWork.Domain.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWork.Domain.Queries.UserQuery
{
    public class GetByEmailUserQuery : IRequest<UserViewModel>
    {
        public GetByEmailUserQuery(string email)
        {
            Email = email;
        }

        public string Email { get; set; }
    }
}
