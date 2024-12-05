using GestaoResiduosApi.Data.Repository;
using GestaoResiduosApi.Models;
using GestaoResiduosApi.ViewModels;

namespace GestaoResiduosApi.Services
{
    public class RotaService : IRotaService
    {
        private readonly IRotaRepository _rotaRepository;
        private readonly ICaminhaoRepository _caminhaoRepository;
        private readonly IRecipienteRepository _recipienteRepository;

        public RotaService(IRotaRepository rotaRepository, ICaminhaoRepository caminhaoRepository, IRecipienteRepository recipienteRepository)
        {
            _rotaRepository = rotaRepository;
            _caminhaoRepository = caminhaoRepository;
            _recipienteRepository = recipienteRepository;
        }

        public async Task<RotaExibicaoViewModel> CriarRotaAsync(RotaCadastroViewModel model)
        {
            var recipiente = await _recipienteRepository.GetByIdAsync(model.RecipienteId);
            if (recipiente == null)
                throw new KeyNotFoundException("Recipiente não encontrado.");

            var caminhao = await _caminhaoRepository.GetByIdAsync(model.CaminhaoId);
            if (caminhao == null)
                throw new KeyNotFoundException("Caminhão não encontrado.");

            var rota = new RotaModel
            {
                DtRota = model.DtRota,
                Recipiente = recipiente,
                Caminhao = caminhao
            };

            var rotaCriada = await _rotaRepository.AddAsync(rota);

            return new RotaExibicaoViewModel
            {
                IdRota = rotaCriada.IdRota,
                DtRota = rotaCriada.DtRota,
                RecipienteId = rotaCriada.Recipiente.IdRecipiente,
                CaminhaoId = rotaCriada.Caminhao.IdCaminhao
            };
        }

        public async Task DeletarRotaAsync(long id)
        {
            var exists = await _rotaRepository.ExistsByIdAsync(id);
            if (!exists)
                throw new KeyNotFoundException("Rota não encontrada.");

            await _rotaRepository.DeleteByIdAsync(id);
        }
    }
}