using System.Net;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Annotations;
using Amazon.Lambda.Annotations.APIGateway;
using ApiCrudAWSSeries.Repositories;
using ApiCrudAWSSeries.Models;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ApiCrudAWSSeries;

public class Functions
{
    private RepositorySeries repo;
    /// <summary>
    /// Default constructor that Lambda will invoke.
    /// </summary>
    public Functions(RepositorySeries repo)
    {
        this.repo = repo;
    }

    [LambdaFunction]
    [RestApi(LambdaHttpMethod.Get, "/")]
    public async Task<IHttpResult> Get(ILambdaContext context)
    {
        context.Logger.LogInformation("Handling the 'Get' Request");
        List<Serie> series = await this.repo.GetSeriesAsync();
        return HttpResults.Ok(series);
    }
}
