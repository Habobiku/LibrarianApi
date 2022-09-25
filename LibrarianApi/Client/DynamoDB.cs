using System;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime.Internal.Transform;
using LibrarianApi.Models;
using LibrarianApi.Responce;
using static LibrarianApi.Extension.Extension;
using static LibrarianApi.Client.UpdateDB;
using Amazon.Runtime.Internal;

namespace LibrarianApi.Client
{
    public class DynamoDB:IDynamoDB
    {
        private readonly IAmazonDynamoDB _dynamoDb;
        private readonly string _tablename;

        public DynamoDB(IAmazonDynamoDB dynamoDB)
        {
            _dynamoDb = dynamoDB;
            _tablename = Config.TableName;
        }

        public async Task<Book> GetBook(GetResponce get)
        {
            var responce = await _dynamoDb.ScanAsync(Scaning(get.Key, get.Id));

            if (responce.Items.Count == 0)
                return null;
            var result = responce.Items.Select(Map).First();

            return result;

        }   

        public async Task<Books> GetBooks()
        {
            var responce = await _dynamoDb.ScanAsync(Scaning(null, null));

            if (responce.Items.Count == 0)
                return null;

                return new Books
            {
                ListBooks = responce.Items.Select(Map),
            };
        }

        public async Task<bool> PostBook(PostResponce post)
        {
                var request = new PutItemRequest
                {
                    TableName = _tablename,
                    Item = new Dictionary<string, AttributeValue>
                {
                    {"ID", new AttributeValue{N=post.Id } },
                    {"Title", new AttributeValue{S=post.Title} },
                    {"Author", new AttributeValue{S=post.Author} },
                    {"Publisher", new AttributeValue{S=post.Publisher} },
                    {"Status", new AttributeValue{S=post.Status} },
                    {"Isbn",new AttributeValue{S=post.Isbn } },
                    {"Date",new AttributeValue{S=post.Date } },
                    {"Genre",new AttributeValue{S=post.Genre} },
                },


                };
                try
                {
                    var responce = await _dynamoDb.PutItemAsync(request);

                    return responce.HttpStatusCode == System.Net.HttpStatusCode.OK;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Here is mistake" + e);
                    return false;
                }
            
        }

        public async Task<bool> UpdateStatus(string action,string id)
        {
            try
            {
                var responce = await _dynamoDb.UpdateItemAsync(UpdateItem(action, id, _tablename));
                return responce.HttpStatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                Console.WriteLine("Here is mistake" + e);
                return false;
            }
            
          
        }

        
    }
}

