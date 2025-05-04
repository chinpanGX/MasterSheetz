using System.Linq;
using App.MasterData;
using Cysharp.Threading.Tasks;
using MasterData.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.Sample
{
    public class Sample : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMeshProUGUI;
        [SerializeField] private Button jpButton;
        [SerializeField] private Button enButton;
        
        private MasterDataRepository masterDataRepository;
        
        private void Start()
        {
            jpButton.onClick.RemoveAllListeners();
            enButton.onClick.RemoveAllListeners();
            
            jpButton.onClick.AddListener(() => SetLanguage("ja"));
            enButton.onClick.AddListener(() => SetLanguage("en"));
            
            SetupAsync().Forget();
        }
        
        private async UniTaskVoid SetupAsync()
        {
            masterDataRepository = new MasterDataRepository(new MasterDataLoader());
            try
            {
                await masterDataRepository.LoadAsync();
            }
            catch (MasterDataException e)
            {
                Debug.LogError($"Failed to load master data: {e.Message}");
            }
            
            SetLanguage("ja");
        }
        
        private void SetLanguage(string language)
        {
            var localization = masterDataRepository.GetTable<LocalizationMasterDataTable>();
            textMeshProUGUI.text =
                localization.GetAll().FirstOrDefault(x => x.Key == "こんにちは").GetLocalizedString(language);
        }
    }
}