using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using Say.Ivona.Model;

namespace Say.Ivona.Tests
{
    [TestFixture]
    internal class AdditionalMethodsTests : IvonaApiTestBase
    {
        [Test]
        public void CheckTextPrice()
        {
            int price = Api().CheckTextPrice("Hello World", IvonaVoices.EN_US_SALLI);
            Trace.WriteLine("CheckTextPrice: Hello World price is: " + price);
            Assert.IsTrue(price > 0);
        }

        [Test]
        public void GetUserAgreementData()
        {
            AgreementData agreement = Api().GetUserAgreementData();
            Assert.IsNotNull(agreement);
            Trace.WriteLine("GetUserAgreementData: Agreement is trial: " + agreement.IsTrial);
            Assert.IsNotNull(agreement.Limits);
            Assert.IsTrue(agreement.AllCharacters > 0);
        }

        [Test]
        public void GetVoiceData()
        {
            VoiceData voice = Api().GetVoiceData(IvonaVoices.EN_WLS_GERAINT);
            Assert.IsNotNull(voice);
            Trace.WriteLine("GetVoiceData: Voice name is: " + voice.VoiceName);
            Assert.AreEqual("en_wls", voice.LangId);
        }

        [Test]
        public void ListCodecs()
        {
            CodecData[] codecs = Api().ListCodecs();
            Assert.IsNotNull(codecs);
            Assert.IsTrue(codecs.Length > 0);
            Trace.WriteLine("ListCodecs: Codecs received: " + codecs.Length);
            Assert.IsTrue(codecs.Any(codec => codec.Codec == "ogg"));
        }

        [Test]
        public void ListVoices()
        {
            VoiceData[] voices = Api().ListVoices();
            Assert.IsNotNull(voices);
            Trace.WriteLine("ListVoices: Downloaded voices: " + voices.Length);
            Assert.IsTrue(voices.Length > 0);
        }
    }
}