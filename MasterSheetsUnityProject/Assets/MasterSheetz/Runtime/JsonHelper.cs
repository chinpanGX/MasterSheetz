using System;
using UnityEngine;

namespace MasterSheetz.Runtime
{
    internal static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                throw new ArgumentException("JSON string cannot be null or empty.", nameof(json));
            }
            var master = JsonUtility.FromJson<MasterDataElement<T>>(json);
            return master.data;
        }

        [Serializable]
        private class MasterDataElement<T>
        {
            public T[] data;
        }
    }
}