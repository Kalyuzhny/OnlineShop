using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using Order.Infrastructure.Configuration;
using Amazon.Runtime.Internal;
using GrpcStockClient;

namespace Order.Api.Integration
{
    public class StockApiClient : IStockApiClient
    {        
        private readonly string _stockApiURI;
        public StockApiClient(IOptions<IntegrationSettings> integrationSettings) {
            _stockApiURI = integrationSettings.Value.StockApiURI;
        }

        public async Task<bool> OrderProductAsync(string productId, int quantity)
        {
            var httpHandler = new HttpClientHandler();
            // Return `true` to allow certificates that are untrusted/invalid
            httpHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            using var channel = GrpcChannel.ForAddress(_stockApiURI, new GrpcChannelOptions { HttpHandler = httpHandler });
            var client = new Stock.StockClient(channel);

            var request = new OrderProductRequest()
            {
                Id = productId,
                Quantity = quantity
            };

            //Todo: Add Polly
            var reply = await client.OrderProductAsync(request);

            return reply.Status;
        }

        public async Task<bool> AddProductAsync(string productId, int quantity)
        {
            var httpHandler = new HttpClientHandler();
            // Return `true` to allow certificates that are untrusted/invalid
            httpHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            using var channel = GrpcChannel.ForAddress(_stockApiURI, new GrpcChannelOptions { HttpHandler = httpHandler });
            var client = new Stock.StockClient(channel);

            var request = new AddProductRequest()
            {
                Id = productId,
                Quantity = quantity
            };

            //Todo: Add Polly
            var reply = await client.AddProductAsync(request);

            return reply.Status;
        }
    }
}
