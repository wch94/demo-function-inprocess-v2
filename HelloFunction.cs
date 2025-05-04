using System.Net;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;

public static class HelloFunction
{
    [FunctionName("HelloFunction")]
    [OpenApiOperation(operationId: "Run", tags: new[] { "hello" })]
    [OpenApiParameter(name: "name", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Name param")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
    public static IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req)
    {
        string name = req.Query["name"];
        string responseMessage = string.IsNullOrEmpty(name)
            ? "Please pass a name in the query string"
            : $"Hello, {name}!";
        return new OkObjectResult(responseMessage);
    }
}
