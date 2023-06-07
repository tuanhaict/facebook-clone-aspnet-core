using Fadebook.Application.Interfaces.Repositories;
using Fadebook.Domain.Entities;
using Fadebook.Infracstructure.Data;
using Fadebook.Infracstructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Infracstructure.Repositories
{
    public class IntroductionRepository : RepositoryBase<Introduction>, IIntroductionRepository
    {
        private readonly ApplicationContext _context;
        public IntroductionRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
    }
}
