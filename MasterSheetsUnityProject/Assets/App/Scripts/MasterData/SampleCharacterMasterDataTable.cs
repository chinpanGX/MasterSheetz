using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MasterData.Runtime.Domain;

namespace App.MasterData
{
    [Serializable]
    public class SampleCharacter : IMasterData
    {
        [SerializeField] private int id;
        [SerializeField] private string name;
        [SerializeField] private int type;
        [SerializeField] private int playable;
        
        public int Id => id;
        public string Name => name;
        public SampleCharacterType Type => (SampleCharacterType)type;
        public bool Playable => playable == 0;
    }

    public sealed class SampleCharacterMasterDataTable : MasterDataTable<SampleCharacter>
    {
        public SampleCharacterMasterDataTable(IEnumerable<SampleCharacter> masterData) : base(masterData)
        {

        }
        
    }
}