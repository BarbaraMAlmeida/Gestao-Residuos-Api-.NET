using GestaoResiduosApi.Data.Repository;
using GestaoResiduosApi.Models;
using GestaoResiduosApi.ViewModels;

namespace GestaoResiduosApi.Services
{
    public class CaminhaoService : ICaminhaoService
    {
        private readonly ICaminhaoRepository _repository;

        public CaminhaoService(ICaminhaoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CaminhaoExibicaoViewModel>> GetAllAsync()
        {
            var caminhoes = await _repository.GetAllAsync();
            return caminhoes.Select(c => new CaminhaoExibicaoViewModel
            {
                IdCaminhao = c.IdCaminhao,
                Placa = c.Placa,
                Capacidade = c.Capacidade
            });
        }

        public async Task<CaminhaoExibicaoViewModel> AddAsync(CaminhaoCadastroViewModel model)
        {
            var caminhao = new CaminhaoModel
            {
                Placa = model.Placa,
                Capacidade = model.Capacidade
            };

            var savedCaminhao = await _repository.AddAsync(caminhao);

            return new CaminhaoExibicaoViewModel
            {
                IdCaminhao = savedCaminhao.IdCaminhao,
                Placa = savedCaminhao.Placa,
                Capacidade = savedCaminhao.Capacidade
            };
        }
    }
}
