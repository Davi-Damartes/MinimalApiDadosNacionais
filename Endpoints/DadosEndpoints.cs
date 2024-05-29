using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using MinimalApiDadosNacionais.Service;

namespace MinimalApiDadosNacionais.Endpoints
{
    public static class DadosEndpoints
    {

        public static void RegistrarDadosEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/BuscarCep{cep}", async ([FromRoute] string cep,
                                     [FromServices] IDadosNacionaisService service) =>
            {
                var response = await service.BuscarCEP(cep);

                if (response == null)
                {
                    return Results.NotFound("CEP não encontrado!!!");
                }

            return Results.Ok(response);
             })
            .WithName("BuscaCep")
            .WithOpenApi(x => new OpenApiOperation(x)
            {
                Summary = "Busca CEP",
                Description = "Localização precisa e detalhes de CEPs de bairros nacionais.",
                Tags = new List<OpenApiTag> { new OpenApiTag { Name = "CEP" } }
            });


            app.MapGet("/BuscarCnpj{cnpj}", async ([FromRoute] string cnpj,
                                                   [FromServices] IDadosNacionaisService service) =>
            {

                var response = await service.BuscarCNPJ(cnpj);

                if (response == null)
                {
                    return Results.NotFound("CNPJ não encontrado!!!");
                }

                return Results.Ok(response);
            })
            .WithName("BuscaCnpj")
            .WithOpenApi(x => new OpenApiOperation(x)
            {
                Summary = "Buscar Cnpj",
                Description = "Recupera informações detalhadas sobre empresas nacionais por CNPJ",
                Tags = new List<OpenApiTag> { new OpenApiTag { Name = "CNPJ" } }
            });

            app.MapGet("/BuscarDDD{ddd}", async ([FromRoute] string ddd,
                                                 [FromServices] IDadosNacionaisService service) =>
            {
                var response = await service.BuscarDDD(ddd);

                if (response == null)
                {
                    return Results.NotFound("DDD não encontrado!!!");
                }

                return Results.Ok(response);
            })
            .WithName("BuscaDDD")
            .WithOpenApi(x => new OpenApiOperation(x)
            {
                Summary = "Busca DDD",
                Description = "Consulta de dados relevantes com base no código de áreaDDD",
                Tags = new List<OpenApiTag> { new OpenApiTag { Name = "DDD" } }
            });
        }
    }
}
