using System;

namespace ByteCarrot.Mapongo
{
    public interface IMapongoManager
    {
        void Configure(Action<IMapongoConfiguration> action);
    }
}