using System;
using System.Collections.Generic;
using UnityEngine;
using MasterSheetz.Runtime;

namespace App.MasterData
{
    [Serializable]
    public class SampleData : IMasterData
    {
        [SerializeField] private int id;
        [SerializeField] private string name;

        public int Id => id;
        public string Name => name;
    }

    public partial class SampleMasterDataTable : MasterDataTable<SampleData>
    {
        public SampleMasterDataTable(IEnumerable<SampleData> masterData) : base(masterData) { }
    }
}
