﻿using ChatApp.Files;
using ChatApp.Chat;
using ChatApp.Services;
using ChatApp.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void ConfigureServcies(IConfiguration configuration, IServiceCollection services)
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind(nameof(JwtSettings), jwtSettings);

            var fileSettings = new ImageSettings();
            configuration.Bind(nameof(ImageSettings), fileSettings);
            services.AddSingleton(fileSettings);
            services.AddSingleton(jwtSettings);
            services.AddScoped<ICreateChatValidator, CreateChatValidator>();
            services.AddSingleton(_ =>
            {
                return new ChatDictionary();
               
            });
            var tokenValidationParameters = new TokenValidationParameters
            {

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true

            };
            services.AddSingleton(tokenValidationParameters);
            services.AddAuthentication(x =>
            {

                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {

                x.SaveToken = true;
                x.TokenValidationParameters = tokenValidationParameters;
                
               

            }
              );
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IChatService, ChatService>();
          
            services.AddSingleton<IImageManager, ImageManager>();
           
        }
    }
}
