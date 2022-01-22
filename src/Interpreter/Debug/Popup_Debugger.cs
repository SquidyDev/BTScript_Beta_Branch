using System.Windows.Forms;
using BTScript.Debug;

namespace BTScript.Debug
{
	class Popup_Debugger : Debugger
	{
		LogLevel level;

		public Popup_Debugger() 
		{
			level = LogLevel.FATAL;
		}
		
		public Popup_Debugger(LogLevel baseLevel) 
		{
			level = baseLevel;
		}

		/*Return an MessageBoxIcon associated to logLevel
		 static.
		 */
		public static MessageBoxIcon GetIcon(LogLevel level) 
		{
			switch (level) 
			{
				case LogLevel.DEBUG:
					return MessageBoxIcon.Information;
				case LogLevel.INFO:
					return MessageBoxIcon.Information;
				case LogLevel.WARN:
					return MessageBoxIcon.Warning;
				case LogLevel.ERROR:
					return MessageBoxIcon.Error;
				case LogLevel.FATAL:
					return MessageBoxIcon.Stop;
			}
			return MessageBoxIcon.Error;
		}

		protected override void GetOutput(string message, LogLevel level)
		{
			if (this.level > level) return;

			MessageBoxButtons box = MessageBoxButtons.OK;
			MessageBox.Show(message, level.ToString(), box, GetIcon(level));
		}

		// Use to get level
		public new LogLevel GetLevel() => level;

		// Use to change the level
		public void SetLevel(LogLevel newLevel) 
		{
			level = newLevel;
		}
	}
}
