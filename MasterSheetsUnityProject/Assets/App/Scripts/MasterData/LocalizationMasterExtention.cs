namespace App.MasterData
{
    public static class LocalizationMasterExtension
    {
        public static string GetLocalizedString(this LocalizationData localization, string language)
        {
            switch (language)
            {
                case "jp":
                    return localization.Jp;
                case "en":
                    return localization.En;
                default:
                    return localization.Jp; // Default to Japanese if the language is not recognized
            }
        }
    }
}
