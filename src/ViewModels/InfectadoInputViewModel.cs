using System;
using System.ComponentModel.DataAnnotations;


namespace Dio.MongoApi.ViewModels
{
    public class InfectadoInputViewModel
    {
        [Required(ErrorMessage = "DataNascimento é obrigatório")]
        public DateTime DataNascimento { get; set; }
        
        [Required(ErrorMessage = "Sexo é obrigatório")]
        public string Sexo { get; set; }
        
        [Required(ErrorMessage = "Letitude é obrigatório")]
        public double Latitude { get; set; }

        [Required(ErrorMessage = "Longitude é obrigatório")]
        public double Longitude { get; set; }
    }
}