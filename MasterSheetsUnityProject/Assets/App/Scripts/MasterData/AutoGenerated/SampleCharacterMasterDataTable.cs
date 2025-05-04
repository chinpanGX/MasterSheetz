using System;
using System.Collections.Generic;
using UnityEngine;
using MasterData.Runtime;

namespace App.MasterData
{
    [Serializable]
    public class SampleCharacter : IMasterData
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

    public partial class SampleCharacterMasterDataTable : MasterDataTable<SampleCharacter>
    {
        public SampleCharacterMasterDataTable(IEnumerable<SampleCharacter> masterData) : base(masterData) { }
    }
}
