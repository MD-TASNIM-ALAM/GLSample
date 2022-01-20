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

namespace XeroConnection.Application.Queries.GetXeroConnection
{
    public class GetXeroConnectionQueryHandler : IRequestHandler<GetXeroConnectionQuery, Result<GetXeroConnectionViewModel>>
    {
        private readonly IXeroConnectionRepository _xeroConnectionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetXeroConnectionQueryHandler> _logger;

        public GetXeroConnectionQueryHandler(
            IXeroConnectionRepository xeroConnectionRepository,
            IMapper mapper,
            ILogger<GetXeroConnectionQueryHandler> logger)
        {
            _xeroConnectionRepository = xeroConnectionRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<GetXeroConnectionViewModel>> Handle(GetXeroConnectionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"[x] GetXeroConnectionQueryHandler: Stablishing Xero connection");

                var xeroConnection = _xeroConnectionRepository.GetXeroConnection();
                if (xeroConnection == null)
                {
                    _logger.LogInformation($"[x] GetXeroConnectionQueryHandler: Failed to Stablish Xero connection");
                    return Result<GetXeroConnectionViewModel>.Error(404, "Xero connection not found");
                }

                _logger.LogInformation($"[x] GetXeroConnectionQueryHandler: Success to stablish Xero connection");

                // Map contact
                var mappedXeroConnection = _mapper.Map<GetXeroConnectionViewModel>(xeroConnection);

                return new Result<GetXeroConnectionViewModel>(_mapper.Map<GetXeroConnectionViewModel>(mappedXeroConnection))
                {
                    Success = true,
                    StatusCode = Convert.ToInt32(HttpStatusCode.OK),
                    Message = Convert.ToString(HttpStatusCode.OK)
                };
            }
            catch (Exception e)
            {
                _logger.LogError($"[x] GetXeroConnectionQueryHandler: {e.ToString()}");
                throw;
            }
        }
    }
}
