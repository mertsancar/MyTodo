using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Services
{
    public class DynamoDBUserService : DynamoDBService
    {
        public static DynamoDBUserService _instance = new DynamoDBUserService();

        public DynamoDBUserService()
        {

        }

        public async Task<bool> AddUser(string username, string password)
        {
            try
            {
                var requestForUsername = new QueryRequest
                {
                    TableName = this.TableName,
                    KeyConditionExpression = "PK = :pk",
                    ExpressionAttributeNames = new Dictionary<string, string>
                    {
                        {"#username", "Username"},
                    },
                    ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                    {
                        {":pk", new AttributeValue { S =  "User" }},
                        {":username", new AttributeValue { S =  username }},
                    },
                    FilterExpression = "#username = :username"
                };
                var response = await ddbClient.QueryAsync(requestForUsername);
                if (response.Items.Count > 0)
                {
                    return false;
                }

                var request = new PutItemRequest
                {
                    TableName = this.TableName,
                    Item = new Dictionary<string, AttributeValue>()
                    {
                      { "PK", new AttributeValue { S = "User" }},
                      { "SK", new AttributeValue { S =  "User" + "_" + username + "_" + DateTime.Now.ToString("yyyyMMddHHmmssffff") }},
                      { "Username", new AttributeValue { S = username }},
                      { "Password", new AttributeValue { S = password }},
                    }
                };
                await ddbClient.PutItemAsync(request);
                return true;

            }
            catch (Exception)
            {
                Console.WriteLine("MERTMERT: Something wrong in DynamoDBUserService.cs - AddUser()");
                throw;
            }
        }

        public async Task<bool> UserAuth(string username, string password)
        {
            try
            {
                var request = new QueryRequest
                {
                    TableName = this.TableName,
                    KeyConditionExpression = "PK = :pk",
                    ExpressionAttributeNames = new Dictionary<string, string>
                    {
                        {"#username", "Username"},
                        {"#password", "Password"}
                    },
                    ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                    {
                        {":pk", new AttributeValue { S =  "User" }},
                        {":username", new AttributeValue { S =  username }},
                        {":password", new AttributeValue { S =  password }}
                    },
                    FilterExpression = "#username = :username AND #password = :password"
                };
                var response = await ddbClient.QueryAsync(request);

                if (response.Items.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("MERTMERT: Something wrong in DynamoDBUserService.cs - UserAuth()");
                throw;
            }

            //var request = new QueryRequest
            //{
            //    TableName = this.TableName,
            //    KeyConditionExpression = "PK = :pk",
            //    ExpressionAttributeNames = new Dictionary<string, string>
            //    {
            //        {"#username", "Username"},
            //        {"#password", "Password"}
            //    },
            //    ExpressionAttributeValues = new Dictionary<string, AttributeValue>
            //    {
            //        {":pk", new AttributeValue { S =  "User" }},
            //        {":username", new AttributeValue { S =  username }},
            //        {":password", new AttributeValue { S =  password }}
            //    },
            //    FilterExpression = "#username = :username AND #password = :password"
            //};
            //var response = await ddbClient.QueryAsync(request);

            //if (response.Items.Count > 0)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }
    }
}
