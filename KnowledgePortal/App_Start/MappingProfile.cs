using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using KnowledgePortal.Dtos;
using KnowledgePortal.Models;

namespace KnowledgePortal.App_Start
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Post, PostsDto>();
            Mapper.CreateMap<PostsDto, Post>();
        }
    }
}