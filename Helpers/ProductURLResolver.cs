using AutoMapper;
using Core.Entities;
using Microsoft.IdentityModel.Tokens;
using SKINET_Ecommerce.DTOs;

namespace SKINET_Ecommerce.Helpers
{
    //To Resolve PictureURL Automatically before sending data to client
    public class ProductURLResolver : IValueResolver<Product, ProductToReturnDTO, string>
    {
        private readonly IConfiguration _config;
        public ProductURLResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Product source, ProductToReturnDTO destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureURL))
            {
                //var launchSettingsUrl = _config.GetValue<string>("APIURL");
                return _config["APIURL"] + source.PictureURL;
            }

            return null;
        }
    }
}
