using AutoMapper;
using GestaoResiduosApi.Models;
using GestaoResiduosApi.ViewModels;

namespace GestaoResiduosApi.Controllers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Configuração do mapeamento de UsuarioCadastroViewModel para UsuarioModel
            CreateMap<UsuarioCadastroViewModel, UsuarioModel>();
        }
    }
}
