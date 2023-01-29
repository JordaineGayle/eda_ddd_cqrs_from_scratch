using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Common.DTOs
{
    public record NewPostResponse(string Message, Guid Id) : BaseResponse(Message);
}
