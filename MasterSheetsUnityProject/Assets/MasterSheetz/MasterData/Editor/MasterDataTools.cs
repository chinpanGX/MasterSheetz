using System;
using UnityEditor;
using UnityEngine;

namespace MasterData.Editor
{
    public static class MasterDataTools
    {
        [MenuItem("Development Tools/MasterData/Open Link GoogleDrive", false, 100)]
        public static void OpenMasterDataDrive()
        {
            var guilds =
                AssetDatabase.FindAssets("t:GoogleDriveLinkAsset");
            if (guilds.Length <= 0)
            {
                EditorUtility.DisplayDialog("Error", "GoogleDriveLinkAsset not found", "OK");
                return;
            }

            var path = AssetDatabase.GUIDToAssetPath(guilds[0]);
            var link = AssetDatabase.LoadAssetAtPath<GoogleDriveLinkAsset>(path).GoogleDriveLink;
            if (string.IsNullOrEmpty(link))
            {
                EditorUtility.DisplayDialog("Error", "Link is not entered", "OK");
                return;
            }
            
            if (!link.StartsWith("https://drive.google.com"))
            {
                EditorUtility.DisplayDialog("Error", "Link is not a Google Drive link", "OK");
                return;
            }
            Application.OpenURL(link);
        }
    }
}