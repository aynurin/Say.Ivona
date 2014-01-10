using System;
using System.Diagnostics;
using NUnit.Framework;
using Say.Ivona.Model;

namespace Say.Ivona.Tests
{
    [TestFixture]
    internal class SpeechGenerationTests : IvonaApiTestBase
    {
        [TestFixtureTearDown]
        public void DeleteAllSpeechFiles()
        {
            IvonaRestApi api = Api();
            SpeechFileData[] files = api.ListSpeechFiles();
            foreach (SpeechFileData f in files)
                api.DeleteSpeechFile(f.FileId);
        }

        private static SpeechFileRequest SampleFile1()
        {
            var fileInfo = new SpeechFileRequest
            {
                VoiceId = IvonaVoices.ES_CONCHITA,
                Text =
                    "La construcción comenzó en estilo neogótico, pero, al asumir el proyecto Gaudí en 1883, fue completamente replanteada.",
                ContentType = "text/plain",
                CodecId = IvonaCodecs.Ogg22050
            };
            return fileInfo;
        }

        private static SpeechFileRequest SampleFile2()
        {
            var fileInfo = new SpeechFileRequest
            {
                VoiceId = IvonaVoices.ES_ENRIQUE,
                Text =
                    "Según su proceder habitual, a partir de bocetos generales del edificio improvisó la construcción a medida que avanzaba.",
                ContentType = "text/html",
                CodecId = IvonaCodecs.Mp322050
            };
            return fileInfo;
        }

        [Test]
        public void CreateSpeechFile()
        {
            DeleteAllSpeechFiles();

            IvonaRestApi api = Api();
            SpeechFileRequest fileInfo = SampleFile2();
            SpeechFileData fileData = api.CreateSpeechFile(fileInfo);
            Assert.IsNotNull(fileData);
            Assert.IsNotNull(fileData.FileId);
            Assert.IsTrue(!String.IsNullOrWhiteSpace(fileData.FileId));
            Trace.WriteLine("CreateSpeechFile: File created: " + fileData.FileId);
        }

        [Test]
        public void CreateSpeechFileWithMarks()
        {
            DeleteAllSpeechFiles();

            IvonaRestApi api = Api();
            SpeechFileRequest fileInfo = SampleFile1();
            SpeechFileData fileData = api.CreateSpeechFile(fileInfo, true);
            Assert.IsNotNull(fileData);
            Assert.IsNotNull(fileData.FileId);
            Assert.IsNotNull(fileData.MarksUrl);
            Assert.IsTrue(!String.IsNullOrWhiteSpace(fileData.FileId));
            Trace.WriteLine("CreateSpeechFileWithMarks: File created: " + fileData.FileId + ", marks url: " +
                            fileData.MarksUrl);
        }

        [Test]
        public void DeleteSpeechFile()
        {
            DeleteAllSpeechFiles();

            IvonaRestApi api = Api();
            SpeechFileData[] allFiles = api.ListSpeechFiles();
            Assert.IsNotNull(allFiles);
            Assert.IsTrue(allFiles.Length == 0);

            SpeechFileData file = api.CreateSpeechFile(SampleFile1());

            allFiles = api.ListSpeechFiles();
            Assert.IsNotNull(allFiles);
            Assert.IsTrue(allFiles.Length == 1);

            api.DeleteSpeechFile(file.FileId);

            allFiles = api.ListSpeechFiles();
            Assert.IsNotNull(allFiles);
            Assert.IsTrue(allFiles.Length == 0);
        }

        [Test]
        public void GetSpeechFileData()
        {
            DeleteAllSpeechFiles();

            IvonaRestApi api = Api();

            SpeechFileData file = api.CreateSpeechFile(SampleFile2());

            SpeechFileData fileData = api.GetSpeechFileData(file.FileId);

            Assert.IsNotNull(fileData);
            Assert.AreEqual(file.SoundUrl, fileData.SoundUrl);
            Assert.IsTrue(!String.IsNullOrWhiteSpace(fileData.SoundUrl));
            Assert.IsTrue(!String.IsNullOrWhiteSpace(fileData.Text));
        }

        [Test]
        public void ListSpeechFiles()
        {
            DeleteAllSpeechFiles();

            IvonaRestApi api = Api();
            SpeechFileData[] allFiles = api.ListSpeechFiles();
            Assert.IsNotNull(allFiles);
            Assert.IsTrue(allFiles.Length == 0);

            CreateSpeechFile();

            allFiles = api.ListSpeechFiles();
            Assert.IsNotNull(allFiles);
            Assert.IsTrue(allFiles.Length == 1);
            Assert.IsTrue(!String.IsNullOrWhiteSpace(allFiles[0].TextHead));
        }
    }
}