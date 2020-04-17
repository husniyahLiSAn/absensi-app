using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interface
{
    public interface IPresenceRepository
    {
        Task<IEnumerable<PresenceVM>> Get(DateTime startDate, DateTime finishDate);
        int Create(PresenceVM presenceVM);
    }
}
