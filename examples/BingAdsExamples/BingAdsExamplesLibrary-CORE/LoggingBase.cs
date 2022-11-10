namespace BingAdsExamplesLibrary_CORE
{
    public static class LogggingBase
    {
        public delegate void SendMessageDelegate(string msg);
        public static SendMessageDelegate OutputStatusMessage = OutputStatusMessageDefault;

        public static void OutputStatusMessageDefault(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
