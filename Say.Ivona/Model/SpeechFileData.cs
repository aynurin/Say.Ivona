using System;
using System.Collections.Generic;

namespace Say.Ivona.Model
{
    public class SpeechFileData
    {
        /// <summary>
        ///     identifier of a speech file
        /// </summary>
        public string FileId { get; set; }

        /// <summary>
        ///     first 100 characters of speech (returned with <see cref="IvonaRestApi.ListSpeechFiles" /> method)
        /// </summary>
        public string TextHead { get; set; }

        /// <summary>
        ///     text of speech (returned with <see cref="IvonaRestApi.GetSpeechFileData" /> method)
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        ///     characters price for each download of the file
        /// </summary>
        public string CharactersPrice { get; set; }

        /// <summary>
        ///     voice identifier (<see cref="IvonaRestApi.ListVoices()" />)
        /// </summary>
        public string VoiceId { get; set; }

        /// <summary>
        ///     codec identifier (<see cref="IvonaRestApi.ListCodecs()" />)
        /// </summary>
        public string CodecId { get; set; }

        /// <summary>
        ///     the GMT time of the file creation
        /// </summary>
        public DateTime? Created { get; set; }

        /// <summary>
        ///     url for sound file available for download
        /// </summary>
        public string SoundUrl { get; set; }

        /// <summary>
        ///     HTML with flash player embedding code playing generated speech file
        /// </summary>
        public string EmbedCode { get; set; }

        /// <summary>
        ///     url for additional text file with speech marks
        /// </summary>
        public string MarksUrl { get; set; }

        /// <summary>
        ///     additional sound parameters for speech file encoding
        /// </summary>
        public Dictionary<string, string> Params { get; set; }
    }
}