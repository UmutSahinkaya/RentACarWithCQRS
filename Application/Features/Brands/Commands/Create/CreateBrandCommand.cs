using Application.Services.Repositories;
using MediatR;

namespace Application.Features.Brands.Commands.Create;

public class CreateBrandCommand:IRequest<CreatedBrandResponse>
{
    public string Name { get; set; }

    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, CreatedBrandResponse>
    {
        private readonly IBrandRepository _brandRepository;

        public CreateBrandCommandHandler(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public Task<CreatedBrandResponse> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            CreatedBrandResponse response = new(); //Cqrs anlamaya çalıştığım için hata almamak kaydıyla çalıştığını görmek için yazıldı bu kısım
            response.Name = request.Name;
            response.Id = new Guid();
            return null;
        }
    }
}
