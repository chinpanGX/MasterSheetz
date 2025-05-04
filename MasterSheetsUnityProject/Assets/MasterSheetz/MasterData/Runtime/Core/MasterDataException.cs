using System;

namespace MasterData.Runtime.Core
{
    public class MasterDataException : Exception
    {
        public MasterDataException(string message) : base(message)
        {
        }
    }
}