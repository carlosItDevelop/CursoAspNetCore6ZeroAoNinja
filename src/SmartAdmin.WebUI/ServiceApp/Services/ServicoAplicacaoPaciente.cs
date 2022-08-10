using Cooperchip.ITDeveloper.Application.ViewModels;
using Cooperchip.ITDeveloper.Domain.Interfaces.Repository;
using Cooperchip.ITDeveloper.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cooperchip.ITDeveloper.Application.Interfaces;
using AutoMapper;
using Cooperchip.ITDeveloper.Domain.Interfaces;

namespace Cooperchip.ITDeveloper.Application.Services
{
    public class ServicoAplicacaoPaciente : IServicoAplicacaoPaciente
    {
        private readonly IPacienteTesteRepository _teste;

        // (Query) - Get ==> Fala dreto com a camada de Dados
        private readonly IRepositoryPaciente _repo;

        // (Command) - Post, Put, Patch, Delete - Fala com Domain
        private readonly IPacienteService _pacienteService;

        // Cuida do Mapeamento Model/ViewModel + Reverse,
        // antes de passar para Repositório ou Seviços de Domain
        private readonly IMapper _mapper;

        public ServicoAplicacaoPaciente(IRepositoryPaciente repo,
                                        IPacienteService pacienteService,
                                        IMapper mapper, IPacienteTesteRepository teste)
        {
            _repo = repo;
            _pacienteService = pacienteService;
            _mapper = mapper;
            _teste = teste;
        }

        public List<EstadoPaciente> ListaEstadoPacienteApplication()
        {
            return _repo.ListaEstadoPaciente();
        }


        public async Task<PacienteViewModel> ObterPacienteComEstadoPaciente(Guid pacienteId)
        {
            return _mapper.Map<PacienteViewModel>(await _repo.ObterPacienteComEstadoPaciente(pacienteId));
        }

        public async Task<IEnumerable<PacienteViewModel>> ObterPacientesPorEstadoPaciente(Guid estadoPacienteId)
        {
            return _mapper.Map<IEnumerable<PacienteViewModel>>(await _repo.ObterPacientesPorEstadoPaciente(estadoPacienteId));
        }

        public bool TemPaciente(Guid pacienteId)
        {
            return _repo.TemPaciente(pacienteId);
        }
        public async Task<PacienteViewModel> ObterPacientePorIdApplication(Guid id)
        {
            return _mapper.Map<PacienteViewModel>(await _repo.SelecionarPorId(id));
        }

        public async Task<IEnumerable<PacienteViewModel>> PacienteParaPacienteViewModel()
        {
            var pacientes = await _repo.ListaPacientesComEstado();
            List<PacienteViewModel> listaView = new List<PacienteViewModel>();

            foreach (var item in pacientes)
            {
                listaView.Add(new PacienteViewModel
                {
                    Ativo = item.Ativo,
                    Cpf = item.Cpf,
                    DataInternacao = item.DataInternacao,
                    DataNascimento = item.DataNascimento,
                    Email = item.Email,
                    EstadoPaciente = item.EstadoPaciente,
                    EstadoPacienteId = item.EstadoPacienteId,
                    Id = item.Id,
                    Nome = item.Nome,
                    Rg = item.Rg,
                    RgDataEmissao = item.RgDataEmissao,
                    RgOrgao = item.RgOrgao,
                    Sexo = item.Sexo,
                    TipoDeCliente = item.TipoDeCliente,
                    Motivo = item.Motivo
                });
            }

            return listaView;
        }

        public async Task<IEnumerable<PacienteTeste>> ObterPacientesTeste()
        {
            return await _teste.SelecionarTodos();
        }

        // =======================================================================


        // =======================================================================
        /* ===/ Estes três métodos abaixo delegam a responsabilidade para os Domain Srvices, 
         * pois lá haverá as validações de Regra de Negócios e Validações 
         */
        public async Task RemoverPacienteApplication(Guid id)
        {
            var paciente = await _repo.SelecionarPorId(id);
            await _pacienteService.ExcluirPaciente(paciente);
        }
        public async Task AdicionarPacienteApplication(PacienteViewModel pacienteViewModel)
        {
            await _pacienteService.AdicionarPaciente(_mapper.Map<Paciente>(pacienteViewModel));
        }
        public async Task EditarPacienteApplication(PacienteViewModel pacienteViewModel)
        {
            await _pacienteService.AtualizarPaciente(_mapper.Map<Paciente>(pacienteViewModel));
        }

    }
}
