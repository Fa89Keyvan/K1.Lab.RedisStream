using K1.Lab.RedisStream.Shared.Models;
using StackExchange.Redis;

namespace K1.Lab.RedisStream.Publisher.Redis.Repository;

public sealed class RequestStateChangedRedisStreamRepository(IDatabase db)
    : IRequestStateChangedRepository
{
    private const string _streamName = "UserRequest.StateChanges";
    private const string _groupName = "UserRequest.StateChanges.Group";

    public async Task AddAsync(UserRequest userRequest)
    {
        await InitiateConsumerGroup(db);

        var idtifier = userRequest.Identifier.ToString();
        await db.StreamAddAsync(_streamName,
        [
            new("event","userRequestState_changes"),
            new("newState", userRequest.RequestState.ToString()),
            new("userIdentifier", idtifier)
        ]);
    }

    private static async Task InitiateConsumerGroup(IDatabase db)
    {
        var isExists = await IsExistsConsumerGroupAsync(db);
        if (isExists)
        {
            return;
        }

        await db.StreamCreateConsumerGroupAsync(_streamName, _groupName, "0-0", true);
    }

    private static async Task<bool> IsExistsConsumerGroupAsync(IDatabase db)
    {
        var keyExists = await db.KeyExistsAsync(_streamName);
        var groupInfo = await db.StreamGroupInfoAsync(_streamName);
        var groupExists = groupInfo.Any(x => x.Name == _groupName);

        return keyExists && groupExists;
    }
}
