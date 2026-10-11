using Amazon.DynamoDBv2.Model;

/*
 * Eskim Trilles - 301434046
 * COMP306 - API Engineering & Cloud Computing - Sec402
 * Lab2
 * October 11, 2026
 */

namespace _301434046_eskim__Lab2.Services
{
    internal interface IAmazonDynamoDb
    {
        Task QueryAsync(QueryRequest request);
    }
}