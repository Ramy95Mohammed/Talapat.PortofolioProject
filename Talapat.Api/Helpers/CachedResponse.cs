using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
using Talapat.BLL.Interfaces;

namespace Talapat.Api.Helpers
{
    public class CachedResponse : Attribute, IAsyncActionFilter
    {
        private readonly int timeToLiveInSeconds;
    

        public CachedResponse(int timeToLiveInSeconds)
        {
            this.timeToLiveInSeconds = timeToLiveInSeconds;
          
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();
            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);
            var cachedResponse = await cacheService.GetCachedResponse(cacheKey);
            if(!string.IsNullOrEmpty(cachedResponse))
            {
                var contentResult = new ContentResult()
                { Content = cachedResponse, ContentType = "application/json", StatusCode = 200 };
                context.Result = contentResult;
                return;
            }

         var excutedEndPointContext   =  await next();
            if (excutedEndPointContext.Result is OkObjectResult okObjectResult)
               await cacheService.CacheResponseAsync(cacheKey, okObjectResult.Value,TimeSpan.FromSeconds( timeToLiveInSeconds));

        }

        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();
            keyBuilder.Append($"{request.Path}");
            foreach (var (key,value) in request.Query.OrderBy(x=>x.Key))
            {
                keyBuilder.Append($"|{key}-{value}");
            }
            return keyBuilder.ToString();
        }
    }
}
;