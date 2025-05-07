using System;

namespace MasterSheetz.Runtime
{
    public class MasterDataException : Exception
    {
        public MasterDataException(string message) : base(message)
        {
        }
    }
}