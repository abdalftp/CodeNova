﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace WebAPIJWT
{
    public class CustomAuthenticationFilter : AuthorizeAttribute, IAuthenticationFilter
    {
        public bool AllowMultiple
        {
            get { return false; }
        }
        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            string authParameter = string.Empty;
            HttpRequestMessage request = context.Request;
            AuthenticationHeaderValue authorization = request.Headers.Authorization;
            if(authorization == null)
            {
                context.ErrorResult = new AuthenticationFailureResult("Missing Authorization Missing", request);
                return;
            }
            if(authorization == null && authorization.Scheme != "Bearer")
            {
                context.ErrorResult = new AuthenticationFailureResult("Invalid Authentication Scheme",request);
                return;
            }
            if(string.IsNullOrEmpty(authorization.Parameter))
            {
                context.ErrorResult = new AuthenticationFailureResult("Missing Token", request);
                return;
            }
            context.Principal = WMS.Models.VM.TokenManager.GetPrincipal(authorization.Parameter);
        }

        public async Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            var result = await context.Result.ExecuteAsync(cancellationToken);
            if(result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                result.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue(scheme: "Basic", parameter: "realm=localhost"));
            }
            context.Result = new ResponseMessageResult(result);
        }
    }
    public class AuthenticationFailureResult : IHttpActionResult
    {
        public string ReasonPhrase;
        public HttpRequestMessage Request { get; set; }
        public AuthenticationFailureResult(string reasonPhrase , HttpRequestMessage request)
        {
            ReasonPhrase = reasonPhrase;
            Request = request;
        }
        
        public HttpResponseMessage Execute()
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            responseMessage.RequestMessage = Request;
            responseMessage.ReasonPhrase = ReasonPhrase;
            return responseMessage;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }
    }
}