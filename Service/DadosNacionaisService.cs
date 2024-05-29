using MinimalApiDadosNacionais.Entities;
using System.IO;
using System.Net;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;

namespace MinimalApiDadosNacionais.Service
{
    public class DadosNacionaisService : IDadosNacionaisService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<DadosNacionaisService> _logger;
        public DadosNacionaisService(HttpClient httpClient,
                                     ILogger<DadosNacionaisService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<CepModel> BuscarCEP(string cep)
        {
            var url = $"https://brasilapi.com.br/api/cep/v1/{cep}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CepModel>() ?? null!;
            }
            if (!response.IsSuccessStatusCode)
            {
                return null!;
            }
            else
            {
                throw new Exception($"CEP Inválido");
            }         
        }

        public async Task<CnpjModel> BuscarCNPJ(string cnpj)
        {
            string cnpjDecodificado = WebUtility.UrlDecode(cnpj);

            string cnpjSemSimbolos = RemoverSimbolosCnpj(cnpjDecodificado);

            var url = $"https://brasilapi.com.br/api/cnpj/v1/{cnpjSemSimbolos}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CnpjModel>()?? null!;
            }
            if (!response.IsSuccessStatusCode)
            {
                return null!;
            }
            else
            {
                throw new Exception($"CNPJ Inválido");
            }
        }

        public async Task<DDDModel> BuscarDDD(string ddd)
        {
            var url = $"https://brasilapi.com.br/api/ddd/v1/{ddd}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<DDDModel>() ?? null!;
            }
            if (!response.IsSuccessStatusCode)
            {
                var mensagem = response.Content.ReadFromJsonAsync<string>();
                return null!;
            }
            else
            {
                throw new Exception($"DDD Inválido");
            }
        }
    private string RemoverSimbolosCnpj(string cnpj)
    {
        return cnpj.Replace(".", "").Replace("/", "").Replace("-", "");
    }
    }


}
