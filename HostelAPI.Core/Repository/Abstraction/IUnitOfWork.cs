using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelAPI.Core.Repository.Abstraction
{
    public interface IUnitOfWork : IDisposable
    {
        IRoomTypeRepository RoomType { get; }

        Task Save();
    }
}
