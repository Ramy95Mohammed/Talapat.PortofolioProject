﻿using System.Net;
using System.Text.Json;
using Talapat.Api.Errors;

namespace Talapat.Api.Middlwares
{
    public class ExeptionMiddlware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExeptionMiddlware> logger;
        private readonly IHostEnvironment env;

        public ExeptionMiddlware(RequestDelegate next,ILogger<ExeptionMiddlware> logger,IHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var responseMessage = env.IsDevelopment() ? new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString()) :
                    new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString());
                var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(responseMessage,options);
                await context.Response.WriteAsync(json);
            }
         }
    }
}