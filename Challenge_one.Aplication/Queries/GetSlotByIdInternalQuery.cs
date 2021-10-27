using Challenge_one.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
namespace Challenge_one.Aplication.Queries
{
    public class GetSlotByIdInternalQuery : IRequest<Slot>
    {
        public int Id { get; }
        public GetSlotByIdInternalQuery(int id)
        {
            Id = id;
        }
    }
}
