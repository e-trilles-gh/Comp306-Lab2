using _301434046_eskim__Lab2.Models;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Eskim Trilles - 301434046
 * COMP306 - API Engineering & Cloud Computing - Sec402
 * Lab2
 * October 11, 2026
 */

namespace _301434046_eskim__Lab2.Services
{
    public class BookService
    {
        private readonly IAmazonDynamoDB _dynamoDB;

        public BookService(IAmazonDynamoDB dynamoDb)
        {
            _dynamoDB = dynamoDb;
        }

        public async Task<List<Book>> GetBooksForUserAsync(string userId)
        {
            var request = new QueryRequest
            {
                TableName = "bookshelf",

                KeyConditionExpression = "UserId = :userId",

                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    {
                        ":userId",
                        new AttributeValue { S = userId }
                    }
                }
            };

            var response = await _dynamoDB.QueryAsync(request);

            var books = new List<Book>();

            foreach (var item in response.Items)
            {
                var book = new Book
                {
                    UserId = item["UserId"].S,
                    BookId = item["BookId"].S,
                    Title = item["Title"].S,
                    Author = item["Author"].S,
                    BucketName = item["BucketName"].S,
                    KeyName = item["KeyName"].S,
                    LastReadPage = int.Parse(item["LastReadPage"].N)
                };

                if (item.ContainsKey("LastOpenedAt") && !string.IsNullOrEmpty(item["LastOpenedAt"].S))
                {
                    book.LastOpenedAt
                        = DateTime.Parse(item["LastOpenedAt"].S);
                }
                books.Add(book);
            }
            return books;
        }

        public async Task UpdateLastReadPageAsync(string userId, string bookId, int page)
        {
            var request = new UpdateItemRequest
            {
                TableName = "bookshelf",

                Key = new Dictionary<string, AttributeValue>
                {
                    {"UserId", new AttributeValue { S = userId } },
                    {"BookId", new AttributeValue { S= bookId} }
                },

                UpdateExpression = "SET LastReadPage = :page, LastOpenedAt = :time",

                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    {":page", new AttributeValue { N= page.ToString()} },
                    {":time", new AttributeValue { S= DateTime.UtcNow.ToString("o")} }
                }
            };
            await _dynamoDB.UpdateItemAsync(request);
        }
    }
}