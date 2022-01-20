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
using XeroConnection.Application.Queries.GetXeroConnection;
using XeroConnection.Core.Persistence.Repositories;

namespace XeroConnection.Application.Features.XeroConnection.Queries.DeleteXeroConnection
{

    public class DeleteXeroConnectionQueryHandler : IRequestHandler<DeleteXeroConnectionQuery, Result<DeleteXeroConnectionViewModel>>
    {
        private readonly IXeroConnectionRepository _xeroConnectionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteXeroConnectionQueryHandler> _logger;

        public DeleteXeroConnectionQueryHandler(
            IXeroConnectionRepository xeroConnectionRepository,
            IMapper mapper,
            ILogger<DeleteXeroConnectionQueryHandler> logger)
        {
            _xeroConnectionRepository = xeroConnectionRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<DeleteXeroConnectionViewModel>> Handle(DeleteXeroConnectionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"[x] DeleteXeroConnectionQueryHandler: Deleting Xero connection");

                var deleteXeroConnection = await _xeroConnectionRepository.DeleteXeroConnection();
                if (deleteXeroConnection == null)
                {
                    _logger.LogInformation($"[x] DeleteXeroConnectionQueryHandler: Failed to Delete Xero connection");
                    return Result<DeleteXeroConnectionViewModel>.Error(404, "Xero connection not found");
                }

                _logger.LogInformation($"[x] DEleteXeroConnectionQueryHandler: Success to delete Xero connection");

                // Map contact
                var mappedXeroConnection = _mapper.Map<DeleteXeroConnectionViewModel>(deleteXeroConnection);

                return new Result<DeleteXeroConnectionViewModel>(_mapper.Map<DeleteXeroConnectionViewModel>(mappedXeroConnection))
                {
                    Success = true,
                    StatusCode = Convert.ToInt32(HttpStatusCode.OK),
                    Message = Convert.ToString(HttpStatusCode.OK)
                };
            }
            catch (Exception e)
            {
                _logger.LogError($"[x] DeleteXeroConnectionQueryHandler: {e.ToString()}");
                throw;
            }
        }
    }
}
