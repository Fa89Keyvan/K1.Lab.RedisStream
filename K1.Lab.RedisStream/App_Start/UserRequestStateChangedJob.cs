using K1.Lab.RedisStream.Consumer.Controllers;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace K1.Lab.RedisStream.Consumer.App_Start
{
    public static class UserRequestStateChangedJob
    {
        private const string _streamName = "UserRequest.StateChanges";
        private const string _groupName = "UserRequest.StateChanges.Group";
        public static void Start()
        {
            var backgroundThread = new Thread(async () =>
            {
                while (true)
                {
                    var db = SimpleContainer.RedisDb;

                    var result = await db.StreamReadGroupAsync
                    (
                        key: _streamName,
                        groupName: _groupName,
                        consumerName: "con-2",
                        position: ">",
                        count: 1
                    );


                    string messageId = null;
                    if (result.Any())
                    {
                        messageId = result.First().Id;
                        var dict = ParseResult(result.First());
                    }

                    if (messageId != null)
                    {
                        db.StreamAcknowledge
                        (
                            key: _streamName,
                            groupName: _groupName,
                            messageId: messageId
                        );
                    }

                    await Task.Delay(2000);
                }
            });

            backgroundThread.Start();
        }

        private static Dictionary<string, string> ParseResult(StreamEntry entry) => entry.Values.ToDictionary(x => x.Name.ToString(), x => x.Value.ToString());
    }
}