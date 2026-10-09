using _301434046_eskim__Lab2.Models;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _301434046_eskim__Lab2.Services
{
    public class AmazonS3PdfService
    {
        public async Task<MemoryStream> GetPdfAsync(Book book)
        {
            using var s3Client = new AmazonS3Client(RegionEndpoint.USEast1);

            var request = new GetObjectRequest
            {
                BucketName = book.BucketName,
                Key = book.KeyName
            };

            using var response = await s3Client.GetObjectAsync(request);

            var stream = new MemoryStream();

            await response.ResponseStream.CopyToAsync(stream);

            stream.Position = 0;

            return stream;
        }
    }
}
