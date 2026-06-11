using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace ZumasRevenge
{
	internal static class StartupError
	{
		public static string LogPath => Path.Combine(AppContext.BaseDirectory, "startup.log");

		public static void Log(string message)
		{
			try
			{
				File.AppendAllText(LogPath, DateTime.Now.ToString("u") + " " + message + Environment.NewLine, Encoding.UTF8);
			}
			catch
			{
			}
			Debug.WriteLine(message);
		}

		public static void Show(string title, string message)
		{
			Log(title + ": " + message);
			try
			{
				MessageBoxW(IntPtr.Zero, message, title, 0x10u);
			}
			catch
			{
			}
		}

		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		private static extern int MessageBoxW(IntPtr hWnd, string text, string caption, uint type);
	}
}
