using AutoMapper;
using Talapat.Api.DTOS;
using Talapat.DAL.Entities;

namespace Talapat.Api.Helpers
{
    public class ProductPictureUrlResolver : IValueResolver<Product , ProductToReturnDto , string>
    {
        private readonly IConfiguration configuration;

        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                return $"{configuration["ApiUrl"]}{source.PictureUrl}";
            return null;
        }
    }
}
