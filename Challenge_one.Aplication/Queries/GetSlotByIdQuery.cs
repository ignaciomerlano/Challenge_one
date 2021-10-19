﻿using Challenge_one.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Challenge_one.Aplication.Queries
{
    public class GetSlotByIdQuery : IRequest<Slot>
    {
        public Guid Id { get; }
        public GetSlotByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
