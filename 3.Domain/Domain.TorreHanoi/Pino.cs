using System;
using System.Collections.Generic;

namespace Domain.TorreHanoi
{
    public class Pino
    {
        internal Pino(string tipo, List<Disco> discos)
        {
            Discos = new Stack<Disco>();
            Tipo = CastTipoPino(tipo);
            discos.ForEach(AdicionarDisco);
        }

        public TipoPino Tipo { get; }
        public Stack<Disco> Discos { get; }

        public Disco RemoverDisco()
        {
            if (Discos.Count <= 0)
            {
                throw new Exception($"O pino {Tipo} ja nao possui mais nenhum disco");
            }
            var disco = Discos.Pop();
            return disco;
        }

        public void AdicionarDisco(Disco disco)
        {
            if (ValidarNovoDisco(disco))
            {
                Discos.Push(disco);
            }
            else
            {
                throw new Exception($"O novo disco {disco.Id} adicionado é maior do que o disco anterios {Discos.Peek().Id}");
            }
        }

        private static TipoPino CastTipoPino(string tipo)
        {
            return (TipoPino)Enum.Parse(typeof(TipoPino), tipo);
        }

        private bool ValidarNovoDisco(Disco novoDisco)
        {
            if (Discos.Count <= 0)
            {
                return true;
            }
            var topDisco = Discos.Peek();
            return novoDisco.Id < topDisco.Id;
        }
    }
}
