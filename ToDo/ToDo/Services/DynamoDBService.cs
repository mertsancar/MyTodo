using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Runtime;
using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo.Services
{
    public class DynamoDBService
    {
        public string TableName { get; set; }
        public BasicAWSCredentials credentials;
        public AmazonDynamoDBClient ddbClient;
        public DynamoDBService()
        {
            credentials = new BasicAWSCredentials("", "");
            ddbClient = new AmazonDynamoDBClient(credentials, RegionEndpoint.USEast1);
            TableName = "ToDo";
        }
    }
}
