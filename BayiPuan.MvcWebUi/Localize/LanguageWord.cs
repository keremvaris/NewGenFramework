namespace BayiPuan.MvcWebUi.Localize
{
    public static class LanguageWord
    {
        public static string Get(string word, params object[] args)
        {
            if (args != null && args.Length != 0)
                return new LocalizedString(string.Format(word, args)).ToString();
            return new LocalizedString(word).ToString();
        }
    }
}