using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Application.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XeroConnection.Core.Persistence.Repositories;

namespace XeroConnection.Application.Features.XeroConnection.Queries.CheckXeroConnection
{
    public class CheckXeroConnectionQueryHandler : IRequestHandler<CheckXeroConnectionQuery, Result<CheckXeroConnectionViewModel>>
    {
        private readonly IXeroConnectionRepository _xeroConnectionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CheckXeroConnectionQueryHandler> _logger;

        public CheckXeroConnectionQueryHandler(
            IXeroConnectionRepository xeroConnectionRepository,
            IMapper mapper,
            ILogger<CheckXeroConnectionQueryHandler> logger)
        {
            _xeroConnectionRepository = xeroConnectionRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<CheckXeroConnectionViewModel>> Handle(CheckXeroConnectionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"[x] CheckXeroConnectionQueryHandler: Checking Xero Connection");

                var xeroConnection = await _xeroConnectionRepository.CheckXeroConnection();
                if (xeroConnection == null)
                {
                    _logger.LogInformation($"[x] CheckXeroConnectionQueryHandler: Failed to check Xero connection");
                    return Result<CheckXeroConnectionViewModel>.Error(404, "Xero connection not found");
                }

                _logger.LogInformation($"[x] CheckXeroConnectionQueryHandler: Success to check Xero connection");

                // Map contact
                var mappedXeroConnection = _mapper.Map<CheckXeroConnectionViewModel>(xeroConnection);

                return new Result<CheckXeroConnectionViewModel>(_mapper.Map<CheckXeroConnectionViewModel>(mappedXeroConnection))
                {
                    Success = true,
                    StatusCode = Convert.ToInt32(HttpStatusCode.OK),
                    Message = Convert.ToString(HttpStatusCode.OK)
                };
            }
            catch (Exception e)
            {
                _logger.LogError($"[x] CheckXeroConnectionQueryHandler: {e.ToString()}");
                throw;
            }
        }
    }
}
