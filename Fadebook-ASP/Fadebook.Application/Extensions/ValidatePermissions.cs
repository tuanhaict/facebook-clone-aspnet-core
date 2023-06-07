using Fadebook.Application.Interfaces.Repositories.Base;
using Fadebook.Domain.Entities.Base;
using Fadebook.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Application.Extensions
{
    public static class ValidatePermissions<T> where T : IContainsUserId
    {
        public static async Task<T> Validate(IRepository<T> repository, Guid TId, Guid userId)
        {
            var entity = await repository.GetByIdAsync(TId);
            if (entity == null)
            {
                throw new BadRequestException($"{typeof(T).Name} with id: {TId} doesn't exist");
            }
            if (entity.UserId != userId)
            {
                throw new BadRequestException("You don't have permissions to do this!");
            } 
            return entity;
        }
    }
}
