using UnityEditor;
using UnityEngine;

namespace MasterData.Editor
{
    internal class GoogleDriveLinkAsset : ScriptableObject
    {
        [TextArea(3, 4)] 
        [SerializeField] private string googleDriveLink;
        public string GoogleDriveLink => googleDriveLink;

        [MenuItem("Development Tools/MasterData/Config/Create GoogleDriveLinkAsset", false, 1)]
        public static void CreateGoogleDriveLinkAssetAsset()
        {
            MasterDataConfigAssetCreator.CreateAssetIfNeeded<GoogleDriveLinkAsset>("GoogleDriveLinkAsset");
        }
        
        [MenuItem("Development Tools/MasterData/Config/Select ProjectWindow GoogleDriveLinkAsset", false, 2)]
        public static void OpenGoogleDriveLinkAsset()
        {
            MasterDataConfigAssetCreator.OpenOrCreateAsset<GoogleDriveLinkAsset>("GoogleDriveLinkAsset");
        }
    }
}