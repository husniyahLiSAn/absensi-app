using API.Services.Interface;
using Data.Repositories.Interface;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class PresenceService : IPresenceService
    {
        public IPresenceRepository presenceRepository;

        public PresenceService(IPresenceRepository _presenceRepository)
        {
            this.presenceRepository = _presenceRepository;
        }

        public int Create(PresenceVM presenceVM)
        {
            return presenceRepository.Create(presenceVM);
        }

        public async Task<IEnumerable<PresenceVM>> Get(DateTime startDate, DateTime finishDate)
        {
            return await presenceRepository.Get(startDate, finishDate);
        }
    }
}
