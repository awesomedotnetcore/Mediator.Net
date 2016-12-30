﻿using System.Collections;
using System.Collections.Generic;
using Mediator.Net.Context;
using Mediator.Net.Contracts;


namespace Mediator.Net.Pipeline
{
    public class RequestPipeConfigurator<TContext> : IRequestPipeConfigurator<TContext>
        where TContext : IReceiveContext<IRequest>
    {
        private readonly IList<IPipeSpecification<TContext>> _specifications;

        public RequestPipeConfigurator()
        {
            _specifications = new List<IPipeSpecification<TContext>>();
        }



        public IRequestPipe<TContext> Build()
        {
            IRequestPipe<TContext> current = null;
            for (int i = _specifications.Count - 1; i >= 0; i--)
            {
                if (i == _specifications.Count - 1)
                {
                    var thisPipe =
                        new RequestPipe<TContext>(_specifications[i], null);
                    current = thisPipe;
                }
                else
                {
                    var thisPipe =
                        new RequestPipe<TContext>(_specifications[i], current);
                    current = thisPipe;
                }


            }
            return current;
        }


        public void AddPipeSpecification(IPipeSpecification<TContext> specification)
        {
            _specifications.Add(specification);
        }
    }
}