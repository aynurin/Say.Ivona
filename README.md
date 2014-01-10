# IVONA

`Say.Ivona` is an implementation of [IVONA Text-to-Speech SaaS](http://www.ivona.com/en/saas/) REST API written in C#.

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
