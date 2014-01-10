namespace Say.Ivona.Model
{
    public class VoiceData
    {
        public string VoiceId { get; set; }
        public string LangId { get; set; }

        /// <summary>
        ///     m/f/c – voice gender (male, female, child)
        /// </summary>
        public string Gender { get; set; }

        public string VoiceName { get; set; }
        public string VoiceDescription { get; set; }
        public string ProviderName { get; set; }
        public string ProductName { get; set; }
    }
}