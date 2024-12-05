using GestaoResiduosApi.Data.Repository;
using GestaoResiduosApi.Models;
using GestaoResiduosApi.ViewModels;

namespace GestaoResiduosApi.Services
{
    public class RecipienteService : IRecipienteService
    {
        private readonly IRecipienteRepository _repository;

        public RecipienteService(IRecipienteRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<RecipienteExibicaoViewModel>> GetAllAsync()
        {
            var recipientes = await _repository.GetAllAsync();

            return recipientes.Select(r => new RecipienteExibicaoViewModel
            {
                MaxCapacidade = r.MaxCapacidade,
                AtualNivel = r.AtualNivel,
                Status = r.Status,
                UltimaAtualizacao = r.UltimaAtualizacao,
                Latitude = r.Latitude,
                Longitude = r.Longitude
            });
        }

        public async Task AddAsync(RecipienteCadastroViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException(nameof(viewModel), "O modelo de cadastro do recipiente não pode ser nulo.");
            }

            var recipiente = new RecipienteModel
            {
                MaxCapacidade = viewModel.MaxCapacidade ?? throw new InvalidOperationException("MaxCapacidade não pode ser nulo."),
                AtualNivel = viewModel.AtualNivel ?? throw new InvalidOperationException("AtualNivel não pode ser nulo."),
                Status = viewModel.Status ?? throw new InvalidOperationException("Status não pode ser nulo."),
                UltimaAtualizacao = viewModel.UltimaAtualizacao ?? throw new InvalidOperationException("UltimaAtualizacao não pode ser nula."),
                Latitude = viewModel.Latitude ?? throw new InvalidOperationException("Latitude não pode ser nula."),
                Longitude = viewModel.Longitude ?? throw new InvalidOperationException("Longitude não pode ser nula.")
            };

            await _repository.AddAsync(recipiente);
            await _repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<RecipienteExibicaoViewModel>> GetPagedAsync(int pageNumber, int pageSize)
        {
            var recipientes = await _repository.GetPagedAsync(pageNumber, pageSize);

            return recipientes.Select(r => new RecipienteExibicaoViewModel
            {
                MaxCapacidade = r.MaxCapacidade,
                AtualNivel = r.AtualNivel,
                Status = r.Status,
                UltimaAtualizacao = r.UltimaAtualizacao,
                Latitude = r.Latitude,
                Longitude = r.Longitude
            });
        }
    }
}
