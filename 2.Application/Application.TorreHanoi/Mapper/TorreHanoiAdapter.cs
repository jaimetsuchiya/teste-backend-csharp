using Application.TorreHanoi.Dto;
using Infrastructure.TorreHanoi.ImagemHelper.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Application.TorreHanoi.Mapper
{
    internal class TorreHanoiAdapter
    {
        internal TorreHanoiCompletaDto DomainParaApplicationDto(Domain.TorreHanoi.TorreHanoi domain)
        {
            var dto = new TorreHanoiCompletaDto
            {
                Id = domain.Id.ToString(),
                Destino = DomainParaApplicationDto(domain.Destino),
                Intermediario = DomainParaApplicationDto(domain.Intermediario),
                Origem = DomainParaApplicationDto(domain.Origem),
                DataCriacao = domain.DataCriacao,
                DataFinalizacao = domain.DataFinalizacao,
                Status = domain.Status.ToString()
            };

            domain.PassoAPasso.ToList().ForEach(p => dto.PassoAPasso.Add(p));

            return dto;
        }

        internal ICollection<TorreHanoiResumoDto> DomainParaApplicationDto(ICollection<Domain.TorreHanoi.TorreHanoi> domains)
        {
            return domains.Select(domain => new TorreHanoiResumoDto
                {
                    Id = domain.Id.ToString(),
                    Destino = DomainParaApplicationDto(domain.Destino),
                    Intermediario = DomainParaApplicationDto(domain.Intermediario),
                    Origem = DomainParaApplicationDto(domain.Origem),
                    Status = domain.Status.ToString()
                })
                .ToList();
        }

        private static Dto.PinoDto DomainParaApplicationDto(Domain.TorreHanoi.Pino domain)
        {
            var dto = new Dto.PinoDto {Tipo = domain.Tipo.ToString()};

            domain.Discos.ToList().ForEach(d => dto.Discos.Add(DomainParaApplicationDto(d)));

            return dto;
        }

        private static Dto.DiscoDto DomainParaApplicationDto(Domain.TorreHanoi.Disco domain)
        {
            var dto = new Dto.DiscoDto {Id = domain.Id};


            return dto;
        }


        internal TorreHanoiDto DomainParaDesignerDto(Domain.TorreHanoi.TorreHanoi domain)
        {
            var dto = new TorreHanoiDto
            {
                Id = domain.Id.ToString(),
                Destino = DomainParaDesignerDto(domain.Destino),
                Intermediario = DomainParaDesignerDto(domain.Intermediario),
                Origem = DomainParaDesignerDto(domain.Origem)
            };

            domain.Discos.ToList().ForEach(d => dto.Discos.Add(DomainParaDesignerDto(d)));

            return dto;
        }

        private static Infrastructure.TorreHanoi.ImagemHelper.Dto.PinoDto DomainParaDesignerDto(Domain.TorreHanoi.Pino domain)
        {
            var dto = new Infrastructure.TorreHanoi.ImagemHelper.Dto.PinoDto {Tipo = domain.Tipo.GetHashCode()};

            domain.Discos.ToList().ForEach(d => dto.Discos.Add(DomainParaDesignerDto(d)));

            return dto;
        }

        private static Infrastructure.TorreHanoi.ImagemHelper.Dto.DiscoDto DomainParaDesignerDto(Domain.TorreHanoi.Disco domain)
        {
            var dto = new Infrastructure.TorreHanoi.ImagemHelper.Dto.DiscoDto {Id = domain.Id};


            return dto;
        }
    }
}
