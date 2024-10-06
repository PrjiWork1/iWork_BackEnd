using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWork.Domain.Commands.UploadCommands
{
    public class UploadAddCommand : IRequest<string>
    {
        public IFormFile File { get; }
        public UploadAddCommand(IFormFile file)
        {
            File = file;
        }
    }
}
