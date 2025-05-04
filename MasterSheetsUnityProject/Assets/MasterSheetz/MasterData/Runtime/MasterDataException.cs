using System;

namespace MasterData.Runtime
{
    public class MasterDataException : Exception
    {
        public MasterDataException(string message) : base(message)
        {
        }
    }
}