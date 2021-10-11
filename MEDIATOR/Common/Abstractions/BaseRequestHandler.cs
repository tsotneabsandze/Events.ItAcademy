using System;
using System.Threading;
using System.Threading.Tasks;
using CORE.Entities;
using CORE.Interfaces;
using INFRASTRUCTURE.Identity.Services.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;


namespace MEDIATOR.Common.Abstractions
{
    public abstract class BaseRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IServiceProvider _service;
        
        protected BaseRequestHandler(IServiceProvider service)
        {
            _service = service;
        }
        
        private  IGenericRepository<Event> _eventRepo;
        private  IUserService _userService;
        private  IAccountService _accountService;
        private  ITokenService _tokenService;


        protected IGenericRepository<Event> EventRepo
            => _eventRepo ??= _service.GetRequiredService<IGenericRepository<Event>>();

        protected IUserService UserService =>
            _userService ??= _service.GetRequiredService<IUserService>();

        protected IAccountService AccountService
            => _accountService ??= _service.GetRequiredService<IAccountService>();

        protected ITokenService TokenService
            => _tokenService ??= _service.GetRequiredService<ITokenService>();

        
        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}