using System.Collections.Generic;

namespace Say.Ivona.Model
{
    public class SpeechFileRequest
    {
        /// <summary>
        ///     text to process
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        ///     content type of the uploaded text, currently there are three contentTypes supported: "text/plain", "text/html" and
        ///     "text/ssml"
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        ///     voice identifier (<see cref="IvonaRestApi.ListVoices()" />)
        /// </summary>
        public string VoiceId { get; set; }

        /// <summary>
        ///     codec identifier (<see cref="IvonaRestApi.ListCodecs()" />)
        /// </summary>
        public string CodecId { get; set; }

        /// <summary>
        ///     additional sound parameters for speech file encoding
        /// </summary>
        public Dictionary<string, string> Params { get; set; }

        internal Params CreateSpeechFileParams()
        {
            var param = new Params
            {
                {"text", Text},
                {"contentType", ContentType},
                {"voiceId", VoiceId},
                {"codecId", CodecId}
            };

            if (Params != null && Params.Count > 0)
            {
                // I'm not sure if this is a correct and expected way of posting additional parameters and not going to test this as of now...
                int index = 0;
                foreach (var p in Params)
                {
                    param.Add("params[" + index + "].key", p.Key);
                    param.Add("params[" + index + "].value", p.Value);
                    index++;
                }
            }
            return param;
        }
    }
}