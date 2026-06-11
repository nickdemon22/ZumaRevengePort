using System;
using System.Linq;
using System.Text;
using SexyFramework;
using SexyFramework.Drivers.App;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	public class DebugDialog : Widget, ButtonListener
	{
		private const int BtnClose = 101;
		private const int Btn1066 = 102;
		private const int Btn1280 = 103;
		private const int Btn1600 = 104;
		private const int Btn1920 = 105;
		private const int BtnFullscreen = 106;
		private const int BtnFps = 107;
		private const int BtnResources = 108;

		private ButtonWidget mCloseButton;
		private ButtonWidget mRes1066Button;
		private ButtonWidget mRes1280Button;
		private ButtonWidget mRes1600Button;
		private ButtonWidget mRes1920Button;
		private ButtonWidget mFullscreenButton;
		private ButtonWidget mFpsButton;
		private ButtonWidget mResourcesButton;
		private ScrollWidget mScrollWidget;
		private DebugInfoWidget mInfoWidget;
		private string mInfoText = "";

		public DebugDialog()
		{
			this.mClip = false;
			this.mPriority = 10;
			int btnW = Common._DS(280);
			int btnH = Common._DS(70);
			int left = Common._DS(40);
			int top = Common._DS(80);
			int gap = Common._DS(12);
			this.mRes1066Button = this.MakeButton(Btn1066, "1066 x 640", left, top, btnW, btnH);
			this.mRes1280Button = this.MakeButton(Btn1280, "1280 x 720", left, top + (btnH + gap), btnW, btnH);
			this.mRes1600Button = this.MakeButton(Btn1600, "1600 x 900", left, top + (btnH + gap) * 2, btnW, btnH);
			this.mRes1920Button = this.MakeButton(Btn1920, "1920 x 1080", left, top + (btnH + gap) * 3, btnW, btnH);
			this.mFullscreenButton = this.MakeButton(BtnFullscreen, "Toggle Fullscreen", left, top + (btnH + gap) * 4, btnW, btnH);
			this.mFpsButton = this.MakeButton(BtnFps, "FPS: OFF", left, top + (btnH + gap) * 5, btnW, btnH);
			this.mResourcesButton = this.MakeButton(BtnResources, "Resources: OFF", left, top + (btnH + gap) * 6, btnW, btnH);
			this.mCloseButton = this.MakeButton(BtnClose, "Close", left, top + (btnH + gap) * 7, btnW, btnH);
			this.mInfoWidget = new DebugInfoWidget(this);
			this.mScrollWidget = new ScrollWidget();
			int scrollX = left + btnW + Common._DS(30);
			int scrollY = top;
			int scrollW = Common._DS(620);
			int scrollH = Common._DS(520);
			this.mScrollWidget.Resize(scrollX, scrollY, scrollW, scrollH);
			this.mInfoWidget.Resize(0, 0, scrollW - Common._DS(20), scrollH);
			this.mScrollWidget.AddWidget(this.mInfoWidget);
			this.AddWidget(this.mRes1066Button);
			this.AddWidget(this.mRes1280Button);
			this.AddWidget(this.mRes1600Button);
			this.AddWidget(this.mRes1920Button);
			this.AddWidget(this.mFullscreenButton);
			this.AddWidget(this.mFpsButton);
			this.AddWidget(this.mResourcesButton);
			this.AddWidget(this.mCloseButton);
			this.AddWidget(this.mScrollWidget);
		}

		private ButtonWidget MakeButton(int id, string label, int x, int y, int w, int h)
		{
			ButtonWidget buttonWidget = Common.MakeButton(id, this, label);
			buttonWidget.mDoFinger = true;
			buttonWidget.Resize(x, y, w, h);
			return buttonWidget;
		}

		public void RefreshInfo()
		{
			GameApp app = GameApp.gApp;
			WP7AppDriver driver = app.mAppDriver as WP7AppDriver;
			int backW = app.mWidth;
			int backH = app.mHeight;
			bool fullscreen = false;
			if (driver != null && driver.mXNAGraphicsDriver != null && driver.mXNAGraphicsDriver.mXNARenderDevice != null)
			{
				var device = driver.mXNAGraphicsDriver.mXNARenderDevice.mDevice;
				backW = device.PreferredBackBufferWidth;
				backH = device.PreferredBackBufferHeight;
				fullscreen = device.IsFullScreen;
			}
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("=== DEBUG INFO ===");
			sb.Append("Back buffer: ").Append(backW).Append(" x ").Append(backH);
			sb.Append(fullscreen ? " (fullscreen)" : " (windowed)").AppendLine();
			sb.Append("Logical game: ").Append(app.mWidth).Append(" x ").Append(app.mHeight).AppendLine();
			sb.Append("FPS overlay: ").Append(app.mShowFPS ? "ON" : "OFF").AppendLine();
			sb.Append("Resource overlay: ").Append(app.mShowDebugResourceList ? "ON" : "OFF").AppendLine();
			sb.Append("Managed memory: ").Append(GC.GetTotalMemory(false) / 1048576L).Append(" MB").AppendLine();
			sb.AppendLine();
			if (app.mResourceManager != null && app.mResourceManager.mLoadedGroups != null)
			{
				sb.AppendLine("Loaded resource groups:");
				foreach (string group in app.mResourceManager.mLoadedGroups.OrderBy((string g) => g))
				{
					sb.Append("  - ").AppendLine(group);
				}
				sb.AppendLine();
				sb.Append("Images: ").Append(CountMap(0));
				sb.Append(" | Sounds: ").Append(CountMap(1));
				sb.Append(" | Fonts: ").Append(CountMap(2));
				sb.AppendLine();
				sb.Append("PopAnims: ").Append(CountMap(3));
				sb.Append(" | PIEffects: ").Append(CountMap(4));
				sb.Append(" | Other: ").Append(CountMap(5) + CountMap(6));
				sb.AppendLine();
				string errorText = app.mResourceManager.GetErrorText();
				if (errorText != null && errorText.Length > 0)
				{
					sb.AppendLine();
					sb.Append("Last resource error: ").Append(errorText);
				}
			}
			this.mInfoText = sb.ToString();
			this.mInfoWidget.SetText(this.mInfoText);
			this.mFpsButton.mLabel = app.mShowFPS ? "FPS: ON" : "FPS: OFF";
			this.mResourcesButton.mLabel = app.mShowDebugResourceList ? "Resources: ON" : "Resources: OFF";
			this.mFullscreenButton.mLabel = fullscreen ? "Fullscreen: ON" : "Fullscreen: OFF";
			app.UpdateDebugOverlayText();
		}

		private int CountMap(int index)
		{
			if (GameApp.gApp.mResourceManager == null || GameApp.gApp.mResourceManager.mResMaps == null || GameApp.gApp.mResourceManager.mResMaps[index] == null)
			{
				return 0;
			}
			return GameApp.gApp.mResourceManager.mResMaps[index].Count;
		}

		public void ButtonDepress(int theId)
		{
			GameApp app = GameApp.gApp;
			switch (theId)
			{
			case Btn1066:
				app.ApplyDesktopDisplay(1066, 640, true);
				break;
			case Btn1280:
				app.ApplyDesktopDisplay(1280, 720, true);
				break;
			case Btn1600:
				app.ApplyDesktopDisplay(1600, 900, true);
				break;
			case Btn1920:
				app.ApplyDesktopDisplay(1920, 1080, true);
				break;
			case BtnFullscreen:
			{
				WP7AppDriver driver = app.mAppDriver as WP7AppDriver;
				bool wantWindowed = true;
				if (driver != null && driver.mXNAGraphicsDriver != null && driver.mXNAGraphicsDriver.mXNARenderDevice != null)
				{
					wantWindowed = driver.mXNAGraphicsDriver.mXNARenderDevice.mDevice.IsFullScreen;
				}
				int w = app.mPreferredWidth > 0 ? app.mPreferredWidth : 1066;
				int h = app.mPreferredHeight > 0 ? app.mPreferredHeight : 640;
				app.ApplyDesktopDisplay(w, h, wantWindowed);
				break;
			}
			case BtnFps:
				app.mShowFPS = !app.mShowFPS;
				app.UpdateDebugOverlayText();
				break;
			case BtnResources:
				app.mShowDebugResourceList = !app.mShowDebugResourceList;
				app.UpdateDebugOverlayText();
				break;
			case BtnClose:
				app.HideDebugDialog();
				return;
			}
			this.RefreshInfo();
		}

		public void ButtonPress(int theId, int theClickCount)
		{
		}

		public void ButtonPress(int theId)
		{
		}

		public void ButtonMouseEnter(int theId)
		{
		}

		public void ButtonMouseLeave(int theId)
		{
		}

		public void ButtonMouseMove(int theId, int theX, int theY)
		{
		}

		public void ButtonDownTick(int theId)
		{
		}

		public override void Draw(SexyGraphics g)
		{
			g.SetColor(0, 0, 0, 180);
			g.FillRect(0, 0, this.mWidth, this.mHeight);
			Font font = Res.GetFontByID(ResID.FONT_SHAGLOUNGE38_GAUNTLET);
			if (font != null)
			{
				g.SetFont(font);
				g.SetColor(255, 220, 80, 255);
				g.DrawString("DEBUG", Common._DS(40), Common._DS(30));
			}
			base.Draw(g);
		}

		private class DebugInfoWidget : Widget
		{
			private readonly DebugDialog mOwner;
			private string mText = "";

			public DebugInfoWidget(DebugDialog owner)
			{
				this.mOwner = owner;
			}

			public void SetText(string text)
			{
				this.mText = text ?? "";
				int lineCount = 1;
				for (int i = 0; i < this.mText.Length; i++)
				{
					if (this.mText[i] == '\n')
					{
						lineCount++;
					}
				}
				this.Resize(0, 0, this.mWidth, Math.Max(this.mHeight, lineCount * Common._DS(22) + Common._DS(20)));
				this.mOwner.mScrollWidget.ScrollToMin(false);
			}

			public override void Draw(SexyGraphics g)
			{
				Font font = Res.GetFontByID(ResID.FONT_SHAGLOUNGE38_GAUNTLET);
				if (font == null)
				{
					return;
				}
				g.SetFont(font);
				g.SetColor(220, 255, 220, 255);
				g.WriteWordWrapped(new Rect(Common._DS(8), Common._DS(8), this.mWidth - Common._DS(16), this.mHeight), this.mText, -1, 0);
			}
		}
	}
}
