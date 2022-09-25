using System;
using Amazon.DynamoDBv2.Model;

namespace LibrarianApi.Client
{
    public static class UpdateDB
    {
        private static string? St;

        public static UpdateItemRequest UpdateItem(string action, string id,string tableName)
        {
          
            switch(action)
            {
                case "Return":
                    St = "Available";
                    break;
                case "Reserve":
                    St = "Reserved";
                    break;
                case "Borrow":
                    St = "Borrowed";
                    break;
            }
            return new UpdateItemRequest
            {
                TableName = tableName,
                Key = new Dictionary<string, AttributeValue> { { "ID", new AttributeValue { N = id } } },
                ExpressionAttributeNames = new Dictionary<string, string>()
            {
                {"#S","Status"},
            },
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    {":st",new AttributeValue{S=St} },
                },
                UpdateExpression = "Set #S=:st",
            };
                
                    

        }

    }
}

