using System;
using System.Collections.Generic;
using UnityEngine;
using MasterSheetz.Runtime;

namespace App.MasterData
{
    [Serializable]
    public class LocalizationData : IMasterData
    {
        [SerializeField] private int id;
        [SerializeField] private string key;
        [SerializeField] private string jp;
        [SerializeField] private string en;

        public int Id => id;
        public string Key => key;
        public string Jp => jp;
        public string En => en;
    }

    public partial class LocalizationMasterDataTable : MasterDataTable<LocalizationData>
    {
        public LocalizationMasterDataTable(IEnumerable<LocalizationData> masterData) : base(masterData) { }
    }
}
