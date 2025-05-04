using System;
using System.Collections.Generic;
using UnityEngine;
using MasterData.Runtime.Domain;

namespace App.MasterData
{
    [Serializable]
    public class Sample : IMasterData
    {
        [SerializeField] private int id;
        [SerializeField] private string name;

        public int Id => id;
        public string Name => name;
    }

    public partial class SampleMasterDataTable : MasterDataTable<Sample>
    {
        public SampleMasterDataTable(IEnumerable<Sample> masterData) : base(masterData) { }
    }
}
