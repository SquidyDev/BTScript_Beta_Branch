using System;

namespace BTScript.Debug
{
    enum LogLevel 
    {
        DEBUG = 0,
        INFO,
        WARN,
        ERROR,
        FATAL
    }

    class Debugger
    {
        LogLevel level;
        List<Debugger> attachedDebugger = new List<Debugger>();

        public Debugger() 
        {
            level = LogLevel.DEBUG;
        }

        public Debugger(LogLevel baseLevel) 
        {
            level = baseLevel;
        }

        public LogLevel GetLevel() => level;
        public List<Debugger> GetAttachedDebugger() => attachedDebugger;

        //Attach a new debugger to the same output
        public void Attach(Debugger newDebugger) 
        {
            attachedDebugger.Add(newDebugger);
        }

        //Same Principal as Attach but works with an array
        public void Attach(Debugger[] newDebuggers) 
        {
            foreach(Debugger newDebugger in newDebuggers) attachedDebugger.Add(newDebugger);
        }

        //Detach a specific debugger from the current output
        public void Detach(Debugger debugger) 
        {
            attachedDebugger.Remove(debugger);
        }

        public void SetLevel(LogLevel newLevel) 
        {
            level = newLevel;
        }

        public void Debug(string message) 
        {
            UpdateAttachedStream(message, LogLevel.DEBUG);

            if (level > LogLevel.DEBUG) return;

            SetConsoleColor(LogLevel.DEBUG);
            Console.WriteLine($"DEBUG : {message}");
        }

        public void Info(string message)
        {
            UpdateAttachedStream(message, LogLevel.INFO);

            if(level > LogLevel.INFO) return;

            SetConsoleColor(LogLevel.INFO);
            Console.WriteLine($"INFO : {message}");
        }

        public void Warn(string message)
        {
            UpdateAttachedStream(message, LogLevel.WARN);

            if(level > LogLevel.WARN) return;

            SetConsoleColor(LogLevel.WARN);
            Console.WriteLine($"WARN : {message}");
        }

        public void Error(string message)
        {
            UpdateAttachedStream(message, LogLevel.ERROR);

            if(level > LogLevel.ERROR) return;

            SetConsoleColor(LogLevel.ERROR);
            Console.WriteLine($"ERROR : {message}");
        }

        public void Fatal(string message)
        {
            UpdateAttachedStream(message, LogLevel.FATAL);

            if(level > LogLevel.FATAL) return;

            SetConsoleColor(LogLevel.FATAL);
            Console.WriteLine($"FATAL : {message}");
            Console.WriteLine("Program exited with code -1");
            Environment.Exit(-1);
        }

        //Set the Console Color based on the log Level
        protected void SetConsoleColor(LogLevel level) 
        {
            switch (level) 
            {
                case LogLevel.DEBUG:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case LogLevel.INFO:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case LogLevel.WARN:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogLevel.ERROR:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogLevel.FATAL:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
            }
        }

        //Call GetOutput On all atached stream
        void UpdateAttachedStream(string message, LogLevel level) 
        {
            foreach(Debugger debugger in attachedDebugger) 
            {
                debugger.GetOutput(message, level);
            }
        }

        //Use if you want to create debugger based on this class
        //message => The current message
        //level => The current output level
        protected virtual void GetOutput(string message, LogLevel level) 
        {

        }
    }
}
