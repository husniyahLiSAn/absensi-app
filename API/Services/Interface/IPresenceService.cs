using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Services.Interface
{
    public interface IPresenceService
    {
        Task<IEnumerable<PresenceVM>> Get(DateTime startDate, DateTime finishDate);
        int Create(PresenceVM presenceVM);
    }
}
