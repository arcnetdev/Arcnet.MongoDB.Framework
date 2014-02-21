! Utilizando o MongoDB.Framework

!! Configuração
A ConnectionString *PADRÃO* é denominada por "MongoServerSettings"

{code: xml}
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <connectionStrings>
    <add name="MongoServerSettings" connectionString="mongodb://localhost/test" />
  </connectionStrings>
</configuration>
{code: xml}

*NOTA:* Construtores sem parâmetros usam automaticamente a ConnectionString padrão.

!! Utilizando o MongoRepository

!!! Defina um IdGenerator
 Alguns destes IdGenerators são usados para os tipos mais comuns: 

* GuidGenerator é usado para Guid
* ObjectIdGenerator é usado para ObjectId
* StringObjectIdGenerator é usado a string do ObjectId
Mais informações : [ http://docs.mongodb.org/ecosystem/tutorial/serialize-documents-with-the-csharp-driver/ ]

!!! Definindo a Entidade
{code:c#}
using MongoDAO;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

public class Entity: TDocument
{
    [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
    public override object Id { get; set; }
}

[MongoCollectionName("Pessoas")]
public class Pessoa : Entity
{
    public string Nome { get; set; }
    public int Idade { get; set; }
}
{code:c#}
*NOTA: * A anotação "MongoCollectionName" nas entidades é obrigatória na utilização do MongoRepository.

!!! Inserindo Dados

{code:c#}
using Arcnet.MongoDB.Framework.Attributes;
using Arcnet.MongoDB.Framework.Repository;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using NUnit.Framework;

[TestFixture]
public class Test
{
    private readonly IMongoRepository<Pessoa> _repository = new MongoRepository<Pessoa>();

    [Test]
    public void Insert()
    {
        _repository.Insert(new Pessoa()
       {
           Idade = 20,
           Nome = "Lucas"
       });
    }
}
{code:c#}


