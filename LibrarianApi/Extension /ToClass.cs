using System;
using Amazon.DynamoDBv2.Model;
using LibrarianApi.Models;

namespace LibrarianApi.Extension
{
    public static class Extension
    {
        public static T ToClass<T>(this Dictionary<string, AttributeValue> dict)
        {
            var type = typeof(T);
            var obj = Activator.CreateInstance(type);
            foreach (var kv in dict)
            {
                var property = type.GetProperty(kv.Key);
                if (property != null)
                {
                    if (!string.IsNullOrEmpty(kv.Value.S))
                    {

                        property.SetValue(obj, kv.Value.S);

                    }
                    else if (!string.IsNullOrEmpty(kv.Value.N))
                    {
                        property.SetValue(obj, int.Parse(kv.Value.N));
                    }
                    else if (kv.Value.SS != null)
                    {
                        property.SetValue(obj, kv.Value.SS);
                    }
                }
            }
            return (T)obj;
        }
        public static ScanRequest Scaning(string? key, string? ID)
        {
            if (string.IsNullOrEmpty(key) && string.IsNullOrEmpty(ID))
            {
                return new ScanRequest
                {
                    TableName = Config.TableName,
                };
            }

            if(key=="ID")
            {
                return new ScanRequest
                {

                    TableName = Config.TableName,

                    ExpressionAttributeValues = new Dictionary<string, AttributeValue> {
                {
                    ":v_"+key,new AttributeValue{N=ID} }
                },
                    FilterExpression = $"{key}=:v_{key}",

                };
            }

            return new ScanRequest
            {

                TableName = Config.TableName,

                ExpressionAttributeValues = new Dictionary<string, AttributeValue> {
                {
                    ":v_"+key,new AttributeValue{S=ID} }
                },
                FilterExpression = $"{key}=:v_{key}",

            };
        }

        public static Book Map(Dictionary<string, AttributeValue> res)
        {
            var result = res.ToClass<Book>();
            return result;
        }

        public static string GenerateRandom()
        {
            string id = Guid.NewGuid().ToString();

            return new string(id.Where(char.IsDigit).ToArray()).Remove(3,15);
        }
    }
}

