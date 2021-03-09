using System;
using Dio.MongoApi.Data.Collections;
using Dio.MongoApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Linq;
using MongoDB.Driver.GeoJsonObjectModel;

namespace Dio.MongoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InfectadoController : ControllerBase
    {
        private Data.MongoDB _mongoDB;
        private IMongoCollection<Infectado> _infectadoCollection;

        public InfectadoController(Data.MongoDB mongoDB)
        {
            _mongoDB = mongoDB;
            _infectadoCollection = _mongoDB.DB.GetCollection<Infectado>(typeof(Infectado).Name.ToLower()); ;
        }

        [HttpPost]
        public ActionResult SalvarInfectado([FromBody]InfectadoInputViewModel infectadoInputViewModel)
        {
            var infectado = new Infectado(
                infectadoInputViewModel.DataNascimento, 
                infectadoInputViewModel.Sexo, 
                infectadoInputViewModel.Latitude, 
                infectadoInputViewModel.Longitude
            );

            _infectadoCollection.InsertOne(infectado);

            return Created(String.Empty, new InfectadoOutputViewModel() { 
                Id = infectado.Id,
                DataNascimento = infectado.DataNascimento,
                Sexo = infectado.Sexo,
                Latitude = infectado.Localizacao.Latitude,
                Longitude = infectado.Localizacao.Longitude
            });
        }

        [HttpGet]
        public ActionResult Obterinfectados()
        {
            var infectados = _infectadoCollection
                                    .Find(Builders<Infectado>.Filter.Empty)
                                    .ToList()
                                    .Select(i => new InfectadoOutputViewModel()
                                        {
                                            Id = i.Id,
                                            DataNascimento = i.DataNascimento,
                                            Sexo = i.Sexo,
                                            Latitude = i.Localizacao.Latitude,
                                            Longitude = i.Localizacao.Longitude
                                        });
            
            return Ok(infectados);
        }

        [HttpPut("{id}")]
        public ActionResult AtualizarInfectado(string id, [FromBody]InfectadoInputViewModel infectadoInputViewModel)
        {
            var filter = Builders<Infectado>.Filter.Eq(i => i.Id, id);
            
            var infectado = _infectadoCollection.Find(filter).ToList();

            if (infectado.Count == 0)
                return NotFound($"Não existe infectado com o id { id }");

            var update = Builders<Infectado>.Update
                .Set(i => i.DataNascimento, infectadoInputViewModel.DataNascimento)
                .Set(i => i.Sexo, infectadoInputViewModel.Sexo)
                .Set(i => i.Localizacao, new GeoJson2DGeographicCoordinates(infectadoInputViewModel.Latitude, infectadoInputViewModel.Longitude)
            );

            _infectadoCollection.UpdateOne(filter, update);

            return Ok($"Infectado id: { id } atualizado com sucesso");
        }

        [HttpDelete("{id}")]
        public ActionResult ExcluirInfectado(string id)
        {
            var filter = Builders<Infectado>.Filter.Eq(i => i.Id, id);

            var infectado = _infectadoCollection.Find(filter).ToList();

            if (infectado.Count == 0)
                return NotFound($"Não existe infectado com o id { id }");

            _infectadoCollection.DeleteOne(filter);

            return Ok($"Infectado id: { id } excluído com sucesso");
        }

    }
}