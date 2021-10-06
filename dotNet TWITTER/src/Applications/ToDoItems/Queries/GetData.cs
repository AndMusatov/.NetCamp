using dotNet_TWITTER.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace dotNet_TWITTER.Applications.Common.ToDoItems.Queries
{
    public static class GetData
    {
        //Data to execute  
        public record Query(int Id) : IRequest<Response>;

        //Writing Business logic and returns response.  
        public class Handler : IRequestHandler<Query, Response>
        {
            private readonly Repository _repository;
            public Handler(Repository repository)
            {
                _repository = repository;
            }
            public async Task<Response> Handle(Query query, CancellationToken cancellationToken)
            {
                var result = _repository.ToDos.FirstOrDefault(c => c.Id.Equals(query.Id));
                return result == null ? null : new Response(result.Id, result.Task, result.IsCompleted);
            }
        }

        //Data to return  
        public record Response(int Id, string Task, bool IsCompleted);
    }
}
