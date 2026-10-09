using Amazon.DynamoDBv2.Model;

namespace _301434046_eskim__Lab2.Services
{
    internal interface IAmazonDynamoDb
    {
        Task QueryAsync(QueryRequest request);
    }
}