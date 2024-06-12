using Framework.Core;
using IdGen;

namespace Framework.Snowflake;

public class SnowflakeIdGenerator : IIdGenerator
{
    private static readonly IdGenerator IdGen = new(0);

    public long Create()
    {
        return IdGen.CreateId();
    }
}