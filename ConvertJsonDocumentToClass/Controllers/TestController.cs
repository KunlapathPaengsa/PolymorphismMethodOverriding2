using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace PolymorphismMethodOverriding.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        [HttpPost]
        public string? Post([FromBody] JsonDocument request, [FromQuery] int type)
        {
            switch (type)
            {
                case 1:
                    var request1 = request.Deserialize<Westerner>();
                    return ConvertName(request1);
                case 2:
                    var request2 = request.Deserialize<Japanese>();
                    return ConvertName(request2);
                default:
                    var request3 = request.Deserialize<NameClass>();
                    return ConvertName(request3);
            }
        }

        public string ConvertName(NameClass name)
        => name.ToString();
    }
}
public class NameClass
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

}

public class Westerner : NameClass
{
    public string MiddleName { get; set; }

    public override string? ToString()
        => new StringBuilder(FirstName)
        .Append(MiddleName)
        .Append(LastName).ToString();

}

public class Japanese : NameClass
{
    public override string? ToString()
        => new StringBuilder(LastName)
        .Append(FirstName).ToString();
}
