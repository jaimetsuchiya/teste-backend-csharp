using System;
using System.Collections.Generic;

namespace Domain.TorreHanoi.Interface.Repository
{
    public interface IQueueRepository
    {
        bool AdicionarNaFila(TorreHanoi torre);

        TorreHanoi ObterPor(Guid id);

        ICollection<TorreHanoi> ObterTodos();
    }
}
