    namespace MinimalApiDadosNacionais.Entities
{
    public class CnpjModel
    {
        public string? Cnpj { get; set; }
        public string? UF { get; set; }
        public string? RazaoSocial { get; set; }

        public string? NomeFantasia { get; set; }

        public string? Municipio { get; set; }

        public decimal Capital_social { get; set; }

        public string? Data_inicio_atividade { get; set; }

    }
}
