using AutoMapper;
using Comments.Host.Data.Entities;
using Comments.Host.Models.Dtos;
using Comments.Host.Models.Request;

namespace Comments.Host.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CommentEntity, CommentDto>();
            CreateMap<AddRequest, CommentEntity>();
        }
    }
}