using MinimalApiDadosNacionais.Entities;

namespace MinimalApiDadosNacionais.Service
{
    public interface IDadosNacionaisService
    {
        Task<CepModel> BuscarCEP(string cep);

        Task<CnpjModel> BuscarCNPJ(string cep);

        Task<DDDModel> BuscarDDD(string cep);

    }
}
