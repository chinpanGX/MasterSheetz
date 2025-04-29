using UnityEditor;
using UnityEngine;

namespace MasterData.Editor
{
    internal class GoogleDriveLinkAsset : ScriptableObject
    {
        [TextArea(3, 4)] 
        [SerializeField] private string googleDriveLink;
        public string GoogleDriveLink => googleDriveLink;

        [MenuItem("Development Tools/MasterData/Create GoogleDriveLinkAsset", false, 1)]
        public static void CreateGoogleDriveLinkAssetAsset()
        {
            MasterDataConfigAssetCreator.CreateAssetIfNeeded<GoogleDriveLinkAsset>("GoogleDriveLinkAsset");
        }
        
        [MenuItem("Development Tools/MasterData/Select ProjectWindow GoogleSpreadSheet", false, 2)]
        public static void OpenGoogleDriveLinkAsset()
        {
            var guids =
                AssetDatabase.FindAssets("t:GoogleDriveLinkAsset");
            if (guids.Length <= 0)
            {
                EditorUtility.DisplayDialog("Error", "GoogleDriveLinkAsset not found", "OK");
                return;
            }

            var path = AssetDatabase.GUIDToAssetPath(guids[0]);
            var asset = AssetDatabase.LoadAssetAtPath<GoogleDriveLinkAsset>(path);
            if (asset == null)
            {
                EditorUtility.DisplayDialog("Error", "Failed to load the asset", "OK");
                return;
            }

            Selection.activeObject = asset;
            EditorGUIUtility.PingObject(asset);
        }
    }
}