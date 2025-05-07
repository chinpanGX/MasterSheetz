using System;
using System.Collections.Generic;
using UnityEngine;
using MasterSheetz.Runtime;

namespace App.MasterData
{
    [Serializable]
    public class SampleCharacterData : IMasterData
    {
        [SerializeField] private int id;
        [SerializeField] private string name;
        [SerializeField] private int type;
        [SerializeField] private bool playable;

        public int Id => id;
        public string Name => name;
        public int Type => type;
        public bool Playable => playable;
    }

    public partial class SampleCharacterMasterDataTable : MasterDataTable<SampleCharacterData>
    {
        public SampleCharacterMasterDataTable(IEnumerable<SampleCharacterData> masterData) : base(masterData) { }
    }
}
