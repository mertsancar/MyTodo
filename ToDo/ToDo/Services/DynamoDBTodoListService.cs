using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToDo.Model;

namespace ToDo.Services
{
    class DynamoDBTodoListService : DynamoDBService
    {
        public static DynamoDBTodoListService _instance = new DynamoDBTodoListService();

        public DynamoDBTodoListService()
        {
        }

        public async Task AddList(string listName)
        {
            var request = new PutItemRequest
            {
                TableName = this.TableName,
                Item = new Dictionary<string, AttributeValue>()
                {
                  { "PK", new AttributeValue { S = "List" }},
                  { "SK", new AttributeValue { S =  "List" + "_" + listName + "_" + DateTime.Now.ToString("yyyyMMddHHmmssffff") }},
                  { "Name", new AttributeValue { S = listName }},
                  { "Owner", new AttributeValue { S = CurrentUser.Username }},
                }
            };
            await ddbClient.PutItemAsync(request);
        }

        public async Task AddItem(Item item)
        {

            var request = new PutItemRequest
            {
                TableName = this.TableName,
                Item = new Dictionary<string, AttributeValue>()
                {
                  { "PK", new AttributeValue { S = "Item" }},
                  { "SK", new AttributeValue { S = item.Id.ToString() }},
                  { "Name", new AttributeValue { S = item.Name }},
                  { "Owner", new AttributeValue { S = item.Owner }},
                  { "ItemListName", new AttributeValue { S = item.ItemListName  }},
                  { "ItemGenre", new AttributeValue { S = item.ItemGenre.ToString()  }},
                  { "AssignTo", new AttributeValue { S = item.AssignTo }},
                  { "Notes", new AttributeValue { S = item.Notes }},
                  { "IsDone", new AttributeValue { BOOL = item.IsDone }},
                }
            };
            await ddbClient.PutItemAsync(request);
        }

        public async Task AddGameItem(GameItem item)
        {

            var request = new PutItemRequest
            {
                TableName = this.TableName,
                Item = new Dictionary<string, AttributeValue>()
                {
                  { "PK", new AttributeValue { S = "Item" }},
                  { "SK", new AttributeValue { S = item.Id.ToString() }},
                  { "Name", new AttributeValue { S = item.Name }},
                  { "Owner", new AttributeValue { S = item.Owner }},
                  { "ItemListName", new AttributeValue { S = item.ItemListName }},
                  { "ItemGenre", new AttributeValue { S = item.ItemGenre.ToString() }},
                  { "AssignTo", new AttributeValue { S = item.AssignTo }},
                  { "Notes", new AttributeValue { S = item.Notes }},
                  { "IsDone", new AttributeValue { BOOL = item.IsDone }},
                  { "GamePlatform", new AttributeValue { S =  item.GamePlatform.ToString() }},
                  { "GameGenre", new AttributeValue { S =  item.GameGenre.ToString() }},
                }
            };
            await ddbClient.PutItemAsync(request);
        }

        public async Task UpdateItem(Item item)
        {

            Dictionary<string, AttributeValue> key = new Dictionary<string, AttributeValue>
            {
                { "PK", new AttributeValue { S = "Item" } },
                { "SK", new AttributeValue { S = item.Id.ToString() } }
            };

            Dictionary<string, AttributeValueUpdate> updates = new Dictionary<string, AttributeValueUpdate>();
            updates["AssignTo"] = new AttributeValueUpdate()
            {
                Action = AttributeAction.PUT,
                Value = new AttributeValue { S = item.AssignTo }
            };
            updates["Notes"] = new AttributeValueUpdate()
            {
                Action = AttributeAction.PUT,
                Value = new AttributeValue { S = item.Notes }
            };
            updates["IsDone"] = new AttributeValueUpdate()
            {
                Action = AttributeAction.PUT,
                Value = new AttributeValue { BOOL = item.IsDone }
            };

            // Create UpdateItem request
            UpdateItemRequest request = new UpdateItemRequest
            {
                TableName = this.TableName,
                Key = key,
                AttributeUpdates = updates
            };
            await ddbClient.UpdateItemAsync(request);
        }

        public async Task UpdateGameItem(Item item)
        {

            Dictionary<string, AttributeValue> key = new Dictionary<string, AttributeValue>
            {
                { "PK", new AttributeValue { S = "Item" } },
                { "SK", new AttributeValue { S = item.Id.ToString() } }
            };

            Dictionary<string, AttributeValueUpdate> updates = new Dictionary<string, AttributeValueUpdate>();
            updates["AssignTo"] = new AttributeValueUpdate()
            {
                Action = AttributeAction.PUT,
                Value = new AttributeValue { S = item.AssignTo }
            };
            updates["Notes"] = new AttributeValueUpdate()
            {
                Action = AttributeAction.PUT,
                Value = new AttributeValue { S = item.Notes }
            };
            updates["IsDone"] = new AttributeValueUpdate()
            {
                Action = AttributeAction.PUT,
                Value = new AttributeValue { BOOL = item.IsDone }
            };


            GameItem gameItem = (GameItem)item;
            updates["GamePlatform"] = new AttributeValueUpdate()
            {
                Action = AttributeAction.PUT,
                Value = new AttributeValue { S = gameItem.GamePlatform.ToString() }
            };
            updates["GameGenre"] = new AttributeValueUpdate()
            {
                Action = AttributeAction.PUT,
                Value = new AttributeValue { S = gameItem.GameGenre.ToString() }
            };

            // Create UpdateItem request
            UpdateItemRequest request = new UpdateItemRequest
            {
                TableName = this.TableName,
                Key = key,
                AttributeUpdates = updates
            };
            await ddbClient.UpdateItemAsync(request);
        }

        public async Task RemoveItemFromList(int itemId)
        {
            Dictionary<string, AttributeValue> key = new Dictionary<string, AttributeValue>
            {
                { "PK", new AttributeValue { S = "Item" } },
                { "SK", new AttributeValue { S = itemId.ToString() } }
            };

            DeleteItemRequest request = new DeleteItemRequest
            {
                TableName = this.TableName,
                Key = key,
            };

            await ddbClient.DeleteItemAsync(request);
        }

        public async Task<ItemList> GetListWithID(string listName)
        {
            var request = new QueryRequest
            {
                TableName = this.TableName,
                KeyConditionExpression = "PK = :pk",
                ExpressionAttributeNames = new Dictionary<string, string>
                {
                    {"#name", "Name"}
                },
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    {":pk", new AttributeValue { S =  "List" }},
                    {":name", new AttributeValue { S =  listName }}
                },
                FilterExpression = "#name = :name"
            };
            var response = await ddbClient.QueryAsync(request);

            ItemList itemList = new ItemList
            {
                Id = 1,
                Name = response.Items[0]["Name"].S,
            };

            //CurrentToDoList.Name = itemList.Name;
            //CurrentToDoList.ItemList = itemList.Items;

            return itemList;

        }

        public async Task<List<ItemList>> GetLists()
        {
            var request = new QueryRequest
            {
                TableName = this.TableName,
                KeyConditionExpression = "PK = :pk",
                ExpressionAttributeNames = new Dictionary<string, string>
                {
                    {"#owner", "Owner"}
                },
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    {":pk", new AttributeValue { S =  "List" }},
                    {":owner", new AttributeValue { S =  CurrentUser.Username }}
                },
                FilterExpression = "#owner = :owner"
            };
            var response = await ddbClient.QueryAsync(request);

            List<ItemList> itemLists = new List<ItemList>();
            for (int i = 0; i < response.Items.Count; i++)
            {
                ItemList itemList = new ItemList
                {
                    Id = 1,
                    Name = response.Items[i]["Name"].S,
                };
                itemLists.Add(itemList);
            }
            return itemLists;
        }

        public async Task<List<Item>> GetItems(string itemListName)
        {
            var request = new QueryRequest
            {
                TableName = this.TableName,
                KeyConditionExpression = "PK = :pk",
                ExpressionAttributeNames = new Dictionary<string, string>
                {
                    {"#itemListName", "ItemListName"}
                },
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    {":pk", new AttributeValue { S =  "Item" }},
                    {":itemListName", new AttributeValue { S =  itemListName }}
                },
                FilterExpression = "#itemListName = :itemListName"
            };
            var response = await ddbClient.QueryAsync(request);


            List<Item> itemList = new List<Item>();
            for (int i = 0; i < response.Items.Count; i++)
            {
                Item item = new Item
                {
                    Id = 1,
                    Name = response.Items[i]["Name"].S,
                    Owner = response.Items[i]["Owner"].S,
                    ItemListName = response.Items[i]["ItemListName"].S,
                    AssignTo = response.Items[i]["AssignTo"].S,
                    Notes = response.Items[i]["Notes"].S,
                    IsDone = response.Items[i]["AssignTo"].BOOL,
                };

                switch ((ItemGenre)Enum.Parse(typeof(ItemGenre), response.Items[i]["ItemGenre"].S, true))
                {
                    case ItemGenre.Game:
                        GameItem gameItem = new GameItem(item,
                            (GamePlatform)Enum.Parse(typeof(GamePlatform), response.Items[i]["GamePlatform"].S, true),
                            (GameGenre)Enum.Parse(typeof(GameGenre), response.Items[i]["GameGenre"].S, true));
                        itemList.Add(gameItem);
                        break;
                    case ItemGenre.Film:
                        break;
                    case ItemGenre.Book:
                        break;
                    case ItemGenre.Event:
                        break;
                    case ItemGenre.None:
                        break;
                    default:
                        break;
                }
            }
            return itemList;
        }

        public async Task<Item> GetItemWithId(string itemId)
        {
            var request = new QueryRequest
            {
                TableName = this.TableName,
                KeyConditionExpression = "PK = :pk and SK = :sk",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    {":pk", new AttributeValue { S =  "Item" }},
                    {":sk", new AttributeValue { S =  itemId }},
                },
            };
            var response = await ddbClient.QueryAsync(request);

            Item item = new Item
            {
                Id = 1,
                Name = response.Items[0]["Name"].S,
                ItemListName = response.Items[0]["ItemListName"].S,
                AssignTo = response.Items[0]["AssignTo"].S,
                Notes = response.Items[0]["Notes"].S,
                IsDone = response.Items[0]["AssignTo"].BOOL,
            };

            switch ((ItemGenre)Enum.Parse(typeof(ItemGenre), response.Items[0]["ItemGenre"].S, true))
            {
                case ItemGenre.Game:
                    GameItem gameItem = new GameItem(item,
                        (GamePlatform)Enum.Parse(typeof(GamePlatform), response.Items[0]["GamePlatform"].S, true),
                        (GameGenre)Enum.Parse(typeof(GameGenre), response.Items[0]["GameGenre"].S, true));
                    return gameItem;
                case ItemGenre.Film:
                    break;
                case ItemGenre.Book:
                    break;
                case ItemGenre.Event:
                    break;
                case ItemGenre.None:
                    break;
                default:
                    break;
            }

            return item;
        }
    }

}
