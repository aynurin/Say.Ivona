namespace Say.Ivona.Model
{
    public class CodecData
    {
        /// <summary>
        ///     codec identifier
        /// </summary>
        public string CodecId { get; set; }

        /// <summary>
        ///     codec description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     wav/mp3/ogg/alaw/ulaw – encoding format for audio file
        /// </summary>
        public string Codec { get; set; }

        /// <summary>
        ///     8/12/16/22.05 – rate of sound (in khz)
        /// </summary>
        public string Rate { get; set; }

        /// <summary>
        ///     0/8/16 – sound sample size (in bits) (0 – in case of mp3 and ogg format)
        /// </summary>
        public string Sample { get; set; }
    }
}