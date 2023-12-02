using System;
using System.Linq;

public class RandomHelper 
{
    public static T GetRandomEnumValue<T>() where T : Enum
    {
        return Enum.GetValues(typeof(T))
            .OfType<T>()
            .OrderBy(e => Guid.NewGuid())
            .FirstOrDefault();
    }
}
