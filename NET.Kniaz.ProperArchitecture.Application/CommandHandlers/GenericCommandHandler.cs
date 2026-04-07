using NET.Kniaz.ProperArchitecture.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET.Kniaz.ProperArchitecture.Application.CommandHandlers
{
    public class GenericCommandHandler
    {
        protected IUnitOfWork _unitOfWork;
        public GenericCommandHandler(IUnitOfWork unitOfWork) 
        { 
            _unitOfWork = unitOfWork;
        }

        public async Task CommitAsync()
        {
            await _unitOfWork.SaveChangesAsync();
        }

        public void Commit()
        {
            _unitOfWork.SaveChanges();
        }
    }
}
