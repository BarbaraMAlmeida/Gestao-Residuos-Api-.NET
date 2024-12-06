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

        public async Task<IEnumerable<RotaExibicaoViewModel>> GetAllAsync()
        {
            var rotas = await _rotaRepository.GetAllAsync();
            return rotas.Select(a => new RotaExibicaoViewModel
            {
                DtRota = a.DtRota,
                RecipienteId = a.Recipiente.IdRecipiente,
                CaminhaoId = a.Caminhao.IdCaminhao
            });
        }

        public async Task<IEnumerable<RotaExibicaoViewModel>> GetPagedAsync(int pageNumber, int pageSize)
        {
            var rotas = await _rotaRepository.GetPagedAsync(pageNumber, pageSize);
            return rotas.Select(a => new RotaExibicaoViewModel
            {
                DtRota = a.DtRota,
                RecipienteId = a.Recipiente?.IdRecipiente??0,
                CaminhaoId = a.Caminhao?.IdCaminhao??0
            });
        }

        public async Task<RotaExibicaoViewModel> CreateAsync(RotaCadastroViewModel model)
        {
            var errors = new List<string>();

            // Validar rota
            var caminhao = await _caminhaoRepository.GetByIdAsync(model.CaminhaoId);
            if (caminhao == null)
            {
                errors.Add($"Caminhão com ID {model.CaminhaoId} não foi encontrado.");
            }

          
            var recipiente = await _recipienteRepository.GetByIdAsync(model.RecipienteId);
            if (recipiente == null)
            {
                errors.Add($"Recipiente com ID {model.RecipienteId} não foi encontrado.");
            }

            // Validar data
            if (model.DtRota < DateTime.Now)
            {
                errors.Add("A data da Rota deve ser uma data futura.");
            }

            // Lançar exceção com todos os erros
            if (errors.Any())
            {
                throw new ArgumentException(string.Join(" | ", errors));
            }

            // Criação do agendamento
            var rota = new RotaModel
            {
                DtRota = model.DtRota,
                Recipiente = recipiente,
                Caminhao = caminhao
            };

            var savedRota = await _rotaRepository.AddAsync(rota);

            return new RotaExibicaoViewModel
            {
               DtRota = savedRota.DtRota,
                RecipienteId = savedRota.Recipiente.IdRecipiente,
                CaminhaoId = savedRota.Caminhao.IdCaminhao
            };
        }
      }
    }
  