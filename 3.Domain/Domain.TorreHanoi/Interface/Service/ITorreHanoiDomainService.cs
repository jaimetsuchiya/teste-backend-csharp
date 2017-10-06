using System;
using System.Collections.Generic;

namespace Domain.TorreHanoi.Interface.Service
{
    public interface ITorreHanoiDomainService
    {
        Guid Criar(int numeroDiscos);

        TorreHanoi ObterPor(Guid id);

        ICollection<TorreHanoi> ObterTodos();
    }
}
