using System;

// Stubs for Windows Phone / Xbox LIVE APIs removed from MonoGame DesktopGL.

namespace Microsoft.Phone.Shell
{
	public class PhoneApplicationService
	{
		public static PhoneApplicationService Current { get; } = new PhoneApplicationService();

		public event EventHandler<ActivatedEventArgs> Activated;
		public event EventHandler<DeactivatedEventArgs> Deactivated;
	}

	public class ActivatedEventArgs : EventArgs
	{
	}

	public class DeactivatedEventArgs : EventArgs
	{
	}
}

namespace Microsoft.Phone.Tasks
{
	public class WebBrowserTask
	{
		public Uri Uri { get; set; }

		public void Show()
		{
			if (Uri != null)
			{
				try
				{
					System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(Uri.AbsoluteUri) { UseShellExecute = true });
				}
				catch
				{
				}
			}
		}
	}

	public class MarketplaceDetailTask
	{
		public int ContentType { get; set; }
		public string ContentIdentifier { get; set; }

		public void Show()
		{
		}
	}
}

namespace Microsoft.Xna.Framework.GamerServices
{
	public class GameUpdateRequiredException : Exception
	{
	}

	public static class Guide
	{
		public static bool IsTrialMode { get; set; }
		public static bool IsVisible { get; set; }
		public static bool SimulateTrialMode { get; set; }

		public static void ShowMarketplace(int playerIndex)
		{
		}

		public static void BeginShowMessageBox(string title, string text, System.Collections.Generic.List<string> buttons, int focusButton, int icon, AsyncCallback callback, object state)
		{
			callback?.Invoke(null);
		}

		public static int? EndShowMessageBox(IAsyncResult result)
		{
			return 0;
		}
	}
}
