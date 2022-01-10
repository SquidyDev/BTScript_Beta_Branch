namespace BTScript.Debug
{
    //Allow message to be written to a file
    //Only works when attached to a debugger
    class FileDebugger : Debugger
    {
        LogLevel fileLevel;
        string filePath;

        public FileDebugger() 
        {
            fileLevel = LogLevel.DEBUG;
            filePath = GetDate() + ".log";
            CheckFile();
        }

        public FileDebugger(LogLevel baseLevel, string filePath)
        {
            fileLevel = baseLevel;
            this.filePath = filePath + ".log";
            CheckFile();
        }

        //If the file exists, it will erase it's content
        //If the file doesn't exist, it will create a new file
        void CheckFile() 
        {
            File.Create(filePath).Close();
        }

        public new LogLevel GetLevel() => fileLevel;
        public string GetFilePath() => filePath;
        public static string GetDate() => DateTime.Now.ToString().Replace(':', '-');

        protected override void GetOutput(string message, LogLevel level)
        {
            if (fileLevel > level) return;
            
            File.AppendAllText(filePath, $"{level} : {message}\n");
        }
    }
}
