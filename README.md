Sorry to say, but this code seems to be deprecated with Ivone moving to Amazon hosted API. Try requesting their proprietary SDK at http://developer.ivona.com/.

# Say.Ivona

`Say.Ivona` is an implementation of [IVONA Text-to-Speech SaaS](http://www.ivona.com/en/saas/) REST API written in C#.

## Installation

 To install Say.Ivona.dll, run the following command in the [Package Manager Console](http://docs.nuget.org/docs/start-here/using-the-package-manager-console) 
```
PM> Install-Package Say.Ivona.dll
```

## Usage

```c#
var ivona = new IvonaRestApi("api key", "email", "http proxy");
```

### API ([documentation](http://developer.ivona.com/ivona-tts-saas))

The client automatically handles token authentication.

#### Create Speech File

```c#
var fileInfo = new SpeechFileRequest
{
    VoiceId = IvonaVoices.ES_CONCHITA,
    Text =
        "La construcción comenzó en estilo neogótico, pero, al asumir el proyecto Gaudí en 1883, fue completamente replanteada.",
    ContentType = "text/plain",
    CodecId = IvonaCodecs.Ogg22050
};
var fileData = ivona.CreateSpeechFile(fileInfo);
Trace.WriteLine("CreateSpeechFile: File created: " + fileData.SoundUrl);
```

## Credits

  See the [contributors](https://github.com/aynurin/Say.Ivona/graphs/contributors).

## License

  `Say.Ivona` is released under the [MIT License](http://opensource.org/licenses/MIT).
