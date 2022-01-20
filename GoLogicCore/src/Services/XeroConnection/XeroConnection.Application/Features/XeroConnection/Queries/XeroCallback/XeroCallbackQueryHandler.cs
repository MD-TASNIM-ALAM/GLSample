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

namespace XeroConnection.Application.Features.XeroConnection.Queries.XeroCallback
{
    public class XeroCallbackQueryHandler : IRequestHandler<XeroCallbackQuery, Result<XeroCallbackViewModel>>
    {
        private readonly IXeroConnectionRepository _xeroConnectionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<XeroCallbackQueryHandler> _logger;

        public XeroCallbackQueryHandler(
            IXeroConnectionRepository xeroConnectionRepository,
            IMapper mapper,
            ILogger<XeroCallbackQueryHandler> logger)
        {
            _xeroConnectionRepository = xeroConnectionRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<XeroCallbackViewModel>> Handle(XeroCallbackQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"[x] XeroCallbackQueryHandler: Getting Xero callback");

                var xeroConnection = await _xeroConnectionRepository.XeroCallback(request.Code, request.State);
                if (xeroConnection == null)
                {
                    _logger.LogInformation($"[x] XeroCallbackQueryHandler: Failed to get Xero callback");
                    return Result<XeroCallbackViewModel>.Error(404, "Xero callback not found");
                }

                _logger.LogInformation($"[x] XeroCallbackQueryHandler: Success to get Xero callback");

                // Map contact
                var mappedXeroConnection = _mapper.Map<XeroCallbackViewModel>(xeroConnection);

                return new Result<XeroCallbackViewModel>(_mapper.Map<XeroCallbackViewModel>(mappedXeroConnection))
                {
                    Success = true,
                    StatusCode = Convert.ToInt32(HttpStatusCode.OK),
                    Message = Convert.ToString(HttpStatusCode.OK)
                };
            }
            catch (Exception e)
            {
                _logger.LogError($"[x] XeroCallbackQueryHandler: {e.ToString()}");
                throw;
            }
        }
    }
}
