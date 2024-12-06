using GestaoResiduosApi.Data.Repository;
using GestaoResiduosApi.Enums;
using GestaoResiduosApi.Models;
using GestaoResiduosApi.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace GestaoResiduosApi.Services
{
    public class EmergenciaService: IEmergenciaService
    {
        private readonly IEmergenciaRepository _repository;

        public EmergenciaService(IEmergenciaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<EmergenciaExibicaoViewModel>> GetAllAsync()
        {
            var emergencias = await _repository.GetAllAsync();
            return emergencias.Select(e => new EmergenciaExibicaoViewModel
            {
                DtEmergencia = e.DtEmergencia,
                Status = e.Status,
                Descricao = e.Descricao,
                RecipienteId = e.Recipiente?.IdRecipiente ?? 0,
                CaminhaoId = e.Caminhao?.IdCaminhao ?? 0
            });
        }

        public async Task<EmergenciaExibicaoViewModel> AddAsync(EmergenciaCadastroViewModel model)
        {
            if (model.DtEmergencia == DateTime.MinValue)
            {
                throw new InvalidOperationException("A data da emergência não pode ser nula ou inválida.");
            }

            if (!Enum.IsDefined(typeof(StatusEmergencia), model.Status))
            {
                throw new InvalidOperationException("O status da emergência é inválido.");
            }

            var recipiente = await _repository.GetRecipienteByIdAsync(model.RecipienteId);
            if (recipiente == null)
                throw new KeyNotFoundException("Recipiente não encontrado.");

            var caminhao = await _repository.GetCaminhaoByIdAsync(model.CaminhaoId);
            if (caminhao == null)
                throw new KeyNotFoundException("Caminhão não encontrado.");

            var emergencia = new EmergenciaModel
            {
                DtEmergencia = model.DtEmergencia,
                Status = model.Status,
                Descricao = model.Descricao,
                Recipiente = recipiente,
                Caminhao = caminhao
            };

            var savedEmergencia = await _repository.AddAsync(emergencia);

            return new EmergenciaExibicaoViewModel
            {
                DtEmergencia = savedEmergencia.DtEmergencia,
                Status = savedEmergencia.Status,
                Descricao = savedEmergencia.Descricao,
                RecipienteId = savedEmergencia.Recipiente.IdRecipiente,
                CaminhaoId = savedEmergencia.Caminhao.IdCaminhao
            };

        }



        public async Task<IEnumerable<EmergenciaExibicaoViewModel>> GetPagedAsync(int pageNumber, int pageSize)
        {
            var emergencia = await _repository.GetPagedAsync(pageNumber, pageSize);
            return emergencia.Select(a => new EmergenciaExibicaoViewModel
            {
                DtEmergencia = a.DtEmergencia,
                Status = a.Status,
                Descricao = a.Descricao,
                RecipienteId = a.Recipiente?.IdRecipiente ?? 0,
                CaminhaoId = a.Caminhao?.IdCaminhao ?? 0
            });
        }
    }
}
