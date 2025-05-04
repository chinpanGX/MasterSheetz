using System;
using System.Collections.Generic;
using UnityEngine;
using MasterData.Runtime.Domain;

namespace App.MasterData
{
    [Serializable]
    public class LocalizationMaster : IMasterData
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

    public partial class LocalizationMasterMasterDataTable : MasterDataTable<LocalizationMaster>
    {
        public LocalizationMasterMasterDataTable(IEnumerable<LocalizationMaster> masterData) : base(masterData) { }
    }
}
