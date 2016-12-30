﻿using System.Collections;
using System.Collections.Generic;
using Mediator.Net.Context;
using Mediator.Net.Contracts;


namespace Mediator.Net.Pipeline
{
    public class ReceivePipeConfigurator : IReceivePipeConfigurator
    {
        private readonly IList<IPipeSpecification<IReceiveContext<IMessage>>> _specifications;

        public ReceivePipeConfigurator()
        {
            _specifications = new List<IPipeSpecification<IReceiveContext<IMessage>>>();
        }


  
        public void AddPipeSpecification(IPipeSpecification<IReceiveContext<IMessage>> specification)
        {
            _specifications.Add(specification);
        }

        public IReceivePipe<IReceiveContext<IMessage>> Build()
        {
            dynamic current = null;
            for (int i = _specifications.Count - 1; i >= 0; i--)
            {
                if (i == _specifications.Count - 1)
                {
                    var thisPipe =
                        new ReceivePipe<IReceiveContext<IMessage>>(_specifications[i], null);
                    current = thisPipe;
                }
                else
                {
                    var thisPipe =
                        new ReceivePipe<IReceiveContext<IMessage>>(_specifications[i], current);
                    current = thisPipe;
                }


            }
            return current;
        }
    }
}