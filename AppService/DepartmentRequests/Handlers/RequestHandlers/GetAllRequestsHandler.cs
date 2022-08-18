using AppService.DepartmentRequests.MRQueries.RequestsQueryMR;
using Core.Interfaces;
using Core.Mocking;
using Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppService.DepartmentRequests.Handlers.RequestHandlers
{
    public class GetAllRequestsHandler : IRequestHandler<GetAllRequestsQueryMR, List<Request>>
    {

        private readonly IRequestRepoMock _requestRepoMock;

        public GetAllRequestsHandler(IRequestRepoMock requestRepoMock)
        {
            //Will Be Changed To Repo
            _requestRepoMock = requestRepoMock;
        }

        public async Task<List<Request>> Handle(GetAllRequestsQueryMR request, CancellationToken cancellationToken)
        {
            var requests = _requestRepoMock.GetAllRequests().ToList();
            // Mapper Will Added here
            await RunAsync();
            return requests;

        }

        public virtual Task RunAsync()
        {
            return Task.FromResult(false);
        }


    }

}
