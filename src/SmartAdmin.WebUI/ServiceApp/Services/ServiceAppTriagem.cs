using AutoMapper;
using Cooperchip.ITDeveloper.Application.Interfaces;
using Cooperchip.ITDeveloper.Domain.Entities;
using Cooperchip.ITDeveloper.Domain.Interfaces;
using Cooperchip.ITDeveloper.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cooperchip.ITDeveloper.Application.Services
{
    public class ServiceAppTriagem : IAppTriagem
    {

        // (Query) - Get ==> Fala dreto com a camada de Dados
        private readonly IRepositoryTriagem _repo;

        // (Command) - Post, Put, Patch, Delete - Fala com Domain
        private readonly ITriagemService _triagemService;

        // Cuida do Mapeamento Model/ViewModel + Reverse,
        // antes de passar para Repositório ou Seviços de Domain
        private readonly IMapper _mapper;

        public ServiceAppTriagem(IRepositoryTriagem repo,
                                                   ITriagemService triagemService,
                                                   IMapper mapper)
        {
            _repo = repo;
            _triagemService = triagemService;
            _mapper = mapper;
        }



        /* NotifyProntuario */

        public async Task ExcluirTriagemPorId(Guid id)
        {
            await _repo.ExcluirPorId(id);
        }

        public async Task IncluirTriagem(Triagem triagem)
        {
            await _triagemService.AdicionarTriagem(triagem);
        }


        public async Task<Triagem> ObterPorId(Guid id)
        {
            return await _repo.SelecionarPorId(id);
        }

        public async Task<Triagem> ObterTriagemPorId(Guid id)
        {
            return await _repo.ObterTriagemPorId(id);
        }

        public async Task<IEnumerable<Triagem>> ListaTriagemPorData()
        {
            return await _repo.ListaTriagemPorData();
        }

        public async Task<Triagem> ObterPorIdDoPaciente(Guid pacienteId)
        {
            return await _repo.ObterTriagemPorIdPaciente(pacienteId);
        }
    }
}
