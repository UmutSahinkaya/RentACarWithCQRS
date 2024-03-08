using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Commands.Delete;

public class DeleteBrandCommand:IRequest<DeletedBrandResponse>
{
    public Guid Id { get; set; }

    public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, DeletedBrandResponse>, ICacheRemoverRequest
    {
        private readonly IBrandRepository _brandrepository;
        private readonly IMapper _mapper;

        public DeleteBrandCommandHandler(IBrandRepository brandrepository, IMapper mapper)
        {
            _brandrepository = brandrepository;
            _mapper = mapper;
        }

        public string? CacheKey => "";

        public bool BypassCache => false;

        public string? CacheGroupKey => "GetBrands";

        public async Task<DeletedBrandResponse> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            Brand? brand = await _brandrepository.GetAsync(predicate: b => b.Id == request.Id,cancellationToken:cancellationToken);

            await _brandrepository.DeleteAsync(brand); //SoftDelete

            DeletedBrandResponse response = _mapper.Map<DeletedBrandResponse>(brand);

            return response;
        }
    }
}
