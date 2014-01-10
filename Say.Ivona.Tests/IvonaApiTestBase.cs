namespace Say.Ivona.Tests
{
    internal abstract class IvonaApiTestBase
    {
        private const string ApiKey = "...";
        private const string UserEmail = "...";

        protected static IvonaRestApi Api()
        {
            return new IvonaRestApi(ApiKey, UserEmail, "localhost:8888");
        }
    }
}