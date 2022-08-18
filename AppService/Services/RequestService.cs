using AppService.DepartmentRequests.MRQueries.RequestsQueryMR;
using Core.Interfaces;
using Core.Mocking;
using Core.Models;
using GraphQL;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppService.Services
{
    public class RequestService : IRequestService
    {
        private readonly IRequestRepoMock _requestRepoMock;
        private readonly IMediator _mediatR;

        public RequestService(IRequestRepoMock requestRepoMock, IMediator mediatR)
        {
            _requestRepoMock = requestRepoMock;
            _mediatR = mediatR;
        }

        [GraphQLMetadata("requests")]
        public  List<Request> GetAllRequests()
        {
            var query = new GetAllRequestsQueryMR();
            var result = _mediatR.Send(query).Result;

            return result;
            
        }

        [GraphQLMetadata("request")]
        public Request? GetRequestById(int id)
        {
            return _requestRepoMock.GetAllRequests().SingleOrDefault(o => o.Id == id);
        }

        
    }
}
