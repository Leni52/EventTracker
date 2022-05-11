using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTracker.DAL.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        ICommentRepository Comments { get; }
        IEventRepository Events { get; }
        Task SaveAsync();
    }
}
