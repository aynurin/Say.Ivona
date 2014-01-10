using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Say.Ivona.Model;

namespace Say.Ivona
{
    public class IvonaRestApi
    {
        private readonly string _apiKeyMd5;
        private readonly Uri _apiUrl = new Uri("http://api.ivona.com/api/saas/rest/");
        private readonly JsonSerializer _json = JsonSerializer.Create(new JsonSerializerSettings());
        private readonly MD5 _md5 = MD5.Create();
        private readonly string _userEmail;
        private readonly string _httpProxy;

        public IvonaRestApi(string apiKey, string userEmail, string httpProxy = null)
        {
            _userEmail = userEmail;
            _httpProxy = httpProxy;
            _apiKeyMd5 = Md5Hash(apiKey);
        }

        /// <summary>
        ///     Get all available voices list (<see cref="IvonaVoices" />)
        /// </summary>
        /// <returns>Array of <see cref="VoiceData" /></returns>
        public VoiceData[] ListVoices()
        {
            string token = GetToken();
            return GetJson<VoiceData[]>("voices", token);
        }

        /// <summary>
        ///     Get all available codecs list
        /// </summary>
        /// <returns>Array of <see cref="CodecData" /></returns>
        public CodecData[] ListCodecs()
        {
            string token = GetToken();
            return GetJson<CodecData[]>("codecs", token);
        }

        /// <summary>
        ///     Check the characters price for a specified speech parameters (text and voice pair)
        /// </summary>
        /// <param name="text">text to synthesize</param>
        /// <param name="voiceId">voice identifier, <see cref="ListVoices()" /> and <see cref="IvonaVoices" /></param>
        /// <returns>characters price for a text</returns>
        public int CheckTextPrice(string text, string voiceId)
        {
            string token = GetToken();
            string data = Get("textprice", token, new Params {{"text", text}, {"voiceId", voiceId}});
            return Int32.Parse(data);
        }

        /// <summary>
        ///     Get single voice data
        /// </summary>
        /// <param name="voiceId">voice identifier</param>
        /// <returns>
        ///     <see cref="VoiceData" />
        /// </returns>
        public VoiceData GetVoiceData(string voiceId)
        {
            string token = GetToken();
            return GetJson<VoiceData>("voices/" + voiceId, token);
        }

        /// <summary>
        ///     Show user TTS SaaS agreement data (this method will result in an error if there isn’t a SaaS agreement currently
        ///     active for user)
        /// </summary>
        /// <returns>
        ///     <see cref="AgreementData" />
        /// </returns>
        public AgreementData GetUserAgreementData()
        {
            string token = GetToken();
            return GetJson<AgreementData>("agreementdata", token);
        }

        /// <summary>
        ///     Get the data for all user’s pronunciation rules for selected language
        /// </summary>
        /// <param name="langId">identifier of a language</param>
        /// <returns>Array of <see cref="CodecData" /></returns>
        public PronunciationRule[] ListPronunciationRules(string langId)
        {
            string token = GetToken();
            var rules = GetJson<PronunciationRule[]>("pronunciationrules", token, new Params {{"langId", langId}});
            foreach (PronunciationRule rule in rules)
                rule.LangId = langId;
            return rules;
        }

        /// <summary>
        ///     Add a pronunciation rule for selected language into user’s account
        /// </summary>
        /// <param name="rule">the pronunciation rule to add</param>
        /// <returns>success (<code>true</code>) or failure (<code>false</code>) of the operation</returns>
        public bool AddPronunciationRule(PronunciationRule rule)
        {
            string token = GetToken();
            return Post("pronunciationrules", token, rule.AddRuleHttpParams()) == "1";
        }

        /// <summary>
        ///     Modify a pronunciation rule in user’s account
        /// </summary>
        /// <param name="rule">the pronunciation rule to modify</param>
        /// <returns>success (<code>true</code>) or failure (<code>false</code>) of the operation</returns>
        public bool ModifyPronunciationRule(PronunciationRule rule)
        {
            string token = GetToken();
            return Http("pronunciationrules/" + rule.Id, token, HttpMethod.Put, rule.ModifyRuleHttpParams()) == "1";
        }

        /// <summary>
        ///     Delete a pronunciation rule from user’s account
        /// </summary>
        /// <param name="rule">the pronunciation rule to delete</param>
        /// <returns>success (<code>true</code>) or failure (<code>false</code>) of the operation</returns>
        public bool DeletePronunciationRule(PronunciationRule rule)
        {
            string token = GetToken();
            return Http("pronunciationrules/" + rule.Id, token, HttpMethod.Delete, rule.DeleteRuleHttpParams()) == "1";
        }

        /// <summary>
        ///     Get an url for a speech file generated from uploaded text.
        /// </summary>
        /// <param name="fileInfo">the request parameters</param>
        /// <param name="withMarks">include speech marks describing the positions of the text entities in a speech file.</param>
        /// <returns>
        ///     <see cref="SpeechFileData" />
        /// </returns>
        public SpeechFileData CreateSpeechFile(SpeechFileRequest fileInfo, bool withMarks = false)
        {
            string token = GetToken();
            return PostJson<SpeechFileData>(withMarks ? "speechfileswithmarks" : "speechfiles", token,
                fileInfo.CreateSpeechFileParams());
        }

        /// <summary>
        ///     Delete a single speech file data
        /// </summary>
        /// <param name="fileId">
        ///     speech file identifier (returned from <see cref="CreateSpeechFile" />,
        ///     <see cref="GetSpeechFileData" /> and <see cref="ListSpeechFiles" /> methods)
        /// </param>
        /// <returns>success (<code>true</code>) or failure (<code>false</code>) of the operation</returns>
        public bool DeleteSpeechFile(string fileId)
        {
            string token = GetToken();
            return Http("speechfiles/" + fileId, token, HttpMethod.Delete, new Params {{"fileId", fileId}}) == "1";
        }

        /// <summary>
        ///     Getting data of single utterance
        /// </summary>
        /// <param name="fileId">
        ///     speech file identifier (returned from <see cref="CreateSpeechFile" />,
        ///     <see cref="GetSpeechFileData" /> and <see cref="ListSpeechFiles" /> methods)
        /// </param>
        /// <returns>
        ///     <see cref="SpeechFileData" />
        /// </returns>
        public SpeechFileData GetSpeechFileData(string fileId)
        {
            string token = GetToken();
            return GetJson<SpeechFileData>("speechfiles/" + fileId, token);
        }

        /// <summary>
        ///     Get a list of all active speech files for the current user
        /// </summary>
        /// <returns>Array of <see cref="CodecData" /></returns>
        public SpeechFileData[] ListSpeechFiles()
        {
            string token = GetToken();
            return GetJson<SpeechFileData[]>(" speechfiles", token);
        }

        private string GetToken()
        {
            return Post("tokens", null, new Params {{"email", _userEmail}})
                .Trim('"');
        }

        private string Md5Hash(string data)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] hash = _md5.ComputeHash(Encoding.UTF8.GetBytes(_apiKeyMd5 + data));

            // Create a new StringBuilder to collect the bytes
            // and create a string.
            var stringBuilder = new StringBuilder(hash.Length*2);

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            foreach (byte b in hash)
                stringBuilder.Append(b.ToString("x2"));

            // Return the hexadecimal string.
            return stringBuilder.ToString();
        }

        private string Http(string resource, string token, string method, Params p = null)
        {
            if (token != null)
                p = Params.Merge(p, token, Md5Hash(token));
            HttpWebRequest req = PrepareHttp(new Uri(_apiUrl, resource), method, p);
            return ReadHttp(req);
        }

        private string Get(string resource, string token, Params p = null)
        {
            return Http(resource, token, HttpMethod.Get, p);
        }

        private T GetJson<T>(string resource, string token, Params p = null)
        {
            string data = Get(resource, token, p);
            using (var reader = new StringReader(data))
                return _json.Deserialize<T>(new JsonTextReader(reader));
        }

        private string Post(string resource, string token, Params p = null)
        {
            return Http(resource, token, HttpMethod.Post, p);
        }

        private T PostJson<T>(string resource, string token, Params p = null)
        {
            string data = Post(resource, token, p);
            using (var reader = new StringReader(data))
                return _json.Deserialize<T>(new JsonTextReader(reader));
        }

        private HttpWebRequest PrepareHttp(Uri uri, string method, Params p)
        {
            method = method.ToUpper();
            if (p != null && p.Count > 0 && method != HttpMethod.Post)
                uri = new Uri(uri, "?" + p);
            var req = (HttpWebRequest) WebRequest.Create(uri);
            req.Method = method;
            req.SendChunked = false;
            req.ServicePoint.Expect100Continue = false;
            if (_httpProxy != null)
                req.Proxy = new WebProxy(_httpProxy);
            if (method == HttpMethod.Post)
            {
                req.ContentType = "application/x-www-form-urlencoded";
                if (p != null && p.Count > 0)
                {
                    using (Stream stream = req.GetRequestStream())
                    using (var writer = new StreamWriter(stream))
                        writer.Write(p);
                }
            }
            return req;
        }

        private string ReadHttp(HttpWebRequest req)
        {
            try
            {
                var res = (HttpWebResponse) req.GetResponse();
                return ReadHttpData(res);
            }
            catch (WebException ex)
            {
                var res = ex.Response as HttpWebResponse;
                if (res != null)
                {
                    string data = ReadHttpData(res);
                    using (var reader = new StringReader(data))
                    {
                        var err = _json.Deserialize<IvonaExceptionDetails>(new JsonTextReader(reader));
                        throw new IvonaException(err, ex);
                    }
                }
                throw;
            }
        }

        private static string ReadHttpData(HttpWebResponse res)
        {
            using (Stream stream = res.GetResponseStream())
            {
                if (stream == null)
                    throw new NullReferenceException("No stream received");
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                    return reader.ReadToEnd();
            }
        }

        private static class HttpMethod
        {
            public const string Get = "GET";
            public const string Put = "PUT";
            public const string Post = "POST";
            public const string Delete = "DELETE";
        }
    }
}