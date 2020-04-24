namespace ScriptPlayer.Shared
{
    public class OsrConnectionSettings
    {
        public string ComPort { get; set; }
        public string BaudRate { get; set; }

        public const string DefaultComPort = "None";
        public const string DefaultBaudRate = "115200";

        public OsrConnectionSettings()
        {
            ComPort = DefaultComPort;
            BaudRate = DefaultBaudRate;
        }
    }
}