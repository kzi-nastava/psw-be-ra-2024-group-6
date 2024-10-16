using Explorer.Blog.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.API.Public
{
    public interface ICommentService
    {
        Result<CommentDto> Create(CommentDto comment);
        Result<CommentDto> Update(CommentDto comment);
        Result<CommentDto> Delete(int id);
        Result<CommentDto> Get(int id);
    }
}
