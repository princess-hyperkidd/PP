﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using PP.API.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.OAuth;
using PP.BL.Interfaces;

namespace PP.API.Providers
{
    public class SimpleAuthorizationServerProvider
        : OAuthAuthorizationServerProvider
    {

        //public SimpleAuthorizationServerProvider(IAuthBl bl)
        //{
            
        //}

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-All-Origin", new[] { "*" });

            var authService = (IAuthBl)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IAuthBl));
            var user = await authService.FindUser(context.UserName, context.Password);
            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect");
                return;
            } 

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("role", "user"));

            context.Validated(identity);
        }
    }
}