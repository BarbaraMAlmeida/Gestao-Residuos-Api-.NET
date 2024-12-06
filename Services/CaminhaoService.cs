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
                Placa = c.Placa,
                Capacidade = c.Capacidade
            });
        }

        public async Task AddAsync(CaminhaoCadastroViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException(nameof(viewModel), "O modelo de cadastro do caminhao não pode ser nulo.");
            }

            if (viewModel.Capacidade == null)
            {
                throw new InvalidOperationException("Capacidade não pode ser nulo.");
            }


            if (viewModel.Placa == null)
            {
                throw new InvalidOperationException("Placa não pode ser nulo.");
            }



            var caminhao = new CaminhaoModel
            {
                Placa = viewModel.Placa,
                Capacidade = viewModel.Capacidade
            };

            await _repository.AddAsync(caminhao);
            await _repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<CaminhaoExibicaoViewModel>> GetPagedAsync(int pageNumber, int pageSize)
        {
            var recipientes = await _repository.GetPagedAsync(pageNumber, pageSize);

            return recipientes.Select(r => new CaminhaoExibicaoViewModel
            {
                Placa = r.Placa,
                Capacidade = r.Capacidade
            });
        }
    }
}
