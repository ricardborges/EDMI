using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DDD
{
    public interface IAggregateRoot
    {
        Guid Id { get; set; }
        
    }
}
