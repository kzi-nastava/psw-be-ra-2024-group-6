using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.Problems;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class ImageService : CrudService<ImageDto,Image>, IImageService
    {
        private readonly ICrudRepository<Image> _crudRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IMapper mapper;

        public ImageService(ICrudRepository<Image> crudRepository, IImageRepository imageRepository, IMapper mapper) : base(crudRepository,mapper)
        {
            _crudRepository = crudRepository;
            _imageRepository = imageRepository;
            this.mapper = mapper;
        }
        public new Result<ImageDto> Create(ImageDto imageDto)
        {
            try
            {
                _imageRepository.Add(MapToDomain(imageDto));
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(FailureCode.Internal).WithError(ex.Message);
            }
        }
        public Result Delete(long id)
        {
            try
            {
                _imageRepository.Delete(id);
                return Result.Ok();
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<ImageDto> Get(long id)
        {
            var image = _imageRepository.Get(id);
            var result = MapToDto(image);
            return result;
        }
    }
}
