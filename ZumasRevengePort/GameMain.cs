using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Phone.Shell;
using Microsoft.Xna.Framework;
// using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using SexyFramework;
using SexyFramework.Drivers.App;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	// Token: 0x02000106 RID: 262
	public class GameMain : Game
	{
		// Token: 0x06000F5E RID: 3934 RVA: 0x0009F54C File Offset: 0x0009D74C
		public GameMain()
		{
			base.Content = new WP7ContentManager(base.Services);
			base.Content.RootDirectory = "Content";
			base.TargetElapsedTime = TimeSpan.FromTicks(166666L);
			base.IsFixedTimeStep = true;
			base.IsMouseVisible = true;
			this.SexyZuma = new GameApp(this, false);
			GlobalMembers.gSexyApp = this.SexyZuma;
			GlobalMembers.gSexyAppBase = this.SexyZuma;
			this.gApplicationService = PhoneApplicationService.Current;
			this.gApplicationService.Deactivated += new EventHandler<DeactivatedEventArgs>(this.OnServiceDeactivated);
			this.gApplicationService.Activated += new EventHandler<ActivatedEventArgs>(this.OnServiceActivated);
			// Guide.SimulateTrialMode = false;
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x0009F680 File Offset: 0x0009D880
		protected override void Initialize()
		{
			base.Initialize();
			this.spriteBatch = new SpriteBatch(base.GraphicsDevice);
			try
			{
				this.mSpriteFont = base.Content.Load<SpriteFont>("Arial_20");
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Failed to load Arial_20 font: " + ex.ToString());
				this.mSpriteFont = null;
			}
			base.Window.OrientationChanged += new EventHandler<EventArgs>(this.OrientationChanged);
			this.TrySetWindowIcon();
			this.SexyZuma.InitText();
			try
			{
				if ((int)Localization.GetCurrentLanguage() != 1)
				{
					this.splash = base.Content.Load<Texture2D>("Default-Landscape");
					return;
				}
				this.splash = base.Content.Load<Texture2D>("LoadingImage_DarkFrog_French");
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Failed to load splash texture: " + ex.ToString());
				this.splash = null;
			}
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x0009F711 File Offset: 0x0009D911
		protected override void LoadContent()
		{
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x0009F713 File Offset: 0x0009D913
		protected override void UnloadContent()
		{
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x0009F718 File Offset: 0x0009D918
		protected override void Update(GameTime gameTime)
		{
			if (GameApp.mExit)
			{
				base.Exit();
			}
			bool isRunningSlowly = gameTime.IsRunningSlowly;
			try
			{
				base.Update(gameTime);
			}
			catch (Exception ex)
			{
				Debug.WriteLine("GameMain.Update: " + ex);
			}
			this.UpdateInput(gameTime);

			if (!this.isLoading)
			{
				this.SexyZuma.Update((int)gameTime.ElapsedGameTime.TotalMilliseconds);
				return;
			}
			this.mElipseTime += gameTime.ElapsedGameTime.TotalSeconds;
			if (!this.mInitBegin)
			{
				GC.Collect();
				this.SexyZuma.StartThreadInit();
				this.mInitBegin = true;
				return;
			}
			if (this.SexyZuma.mInitFinished)
			{
				if (this.SexyZuma.mPendingDesktopDisplayApply)
				{
					this.SexyZuma.mPendingDesktopDisplayApply = false;
					try
					{
						this.SexyZuma.ApplySavedDesktopDisplay();
					}
					catch (Exception ex)
					{
						Debug.WriteLine("ApplySavedDesktopDisplay failed: " + ex);
					}
				}
				if (this.SexyZuma.mInitFailed)
				{
					string reason = this.SexyZuma.mInitFailureReason;
					if (string.IsNullOrEmpty(reason))
					{
						reason = "Неизвестная ошибка инициализации. См. startup.log рядом с exe.";
					}
					StartupError.Show("Zuma's Revenge — ошибка запуска", reason);
					this.SexyZuma.OnExiting();
					base.Exit();
					return;
				}
				if (this.mElipseTime >= 4.0)
				{
					this.SexyZuma.ShowLoadingScreen();
					this.isLoading = false;
				}
			}

		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x0009F824 File Offset: 0x0009DA24
		protected override void Draw(GameTime gameTime)
		{
			if (this.isLoading)
			{
				base.GraphicsDevice.Clear(Color.Black);
				this.spriteBatch.Begin();
				this.mColor = new Color((int)((byte)MathHelper.Clamp(this.mAlpha, 0f, 255f)), (int)((byte)MathHelper.Clamp(this.mAlpha, 0f, 255f)), (int)((byte)MathHelper.Clamp(this.mAlpha, 0f, 255f)), (int)((byte)MathHelper.Clamp(this.mAlpha, 0f, 255f)));
				int splashW = base.GraphicsDevice.Viewport.Width;
				int splashH = base.GraphicsDevice.Viewport.Height;
				if (this.splash != null)
				{
					this.spriteBatch.Draw(this.splash, new Rectangle(0, 0, splashW, splashH), this.mColor);
				}
				this.spriteBatch.End();
			}
			else
			{
				if (this.splash != null)
				{
					this.splash.Dispose();
					this.splash = null;
				}
				this.SexyZuma.Draw(0);
				this.DrawFpsOverlay();
			}
			base.Draw(gameTime);
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x0009F91C File Offset: 0x0009DB1C
		protected void OnExiting(object sender, EventArgs args)
		{
			this.SexyZuma.OnExiting();
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x0009F929 File Offset: 0x0009DB29
		protected override void OnActivated(object sender, EventArgs args)
		{
			this.SexyZuma.OnActivated();
			base.OnActivated(sender, args);
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x0009F940 File Offset: 0x0009DB40
		protected override void OnDeactivated(object sender, EventArgs args)
		{
			if (!this.SexyZuma.mInitFinished)
			{
				this.mElipseTime -= 2.0;
			}
			this.SexyZuma.OnExiting();
			this.SexyZuma.OnDeactivated();
			base.OnDeactivated(sender, args);
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x0009F98E File Offset: 0x0009DB8E
		protected void OnServiceActivated(object sender, EventArgs args)
		{
			this.SexyZuma.OnServiceActivated();
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x0009F99B File Offset: 0x0009DB9B
		protected void OnServiceDeactivated(object sender, EventArgs args)
		{
			this.SexyZuma.OnServiceDeactivated();
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x0009F9A8 File Offset: 0x0009DBA8
		private void UpdateInput(GameTime gameTime)
		{
			if (GamePad.GetState(0).Buttons.Back == ButtonState.Pressed)
			{
				this.HandleBackButton();
			}
			KeyboardState keyboardState = Keyboard.GetState();
			bool escapePressed = keyboardState.IsKeyDown(Keys.Escape);
			if (escapePressed && !this.mEscapeWasDown)
			{
				this.HandleBackButton();
			}
			this.mEscapeWasDown = escapePressed;
			this.SyncInputMouseRects();
			if (!this.isLoading)
			{
				this.UpdateMouseInput();
			}
			KeyboardState keys = Keyboard.GetState();
			bool tutorialKey = keys.IsKeyDown(Keys.Space) || keys.IsKeyDown(Keys.Enter);
			if (tutorialKey && !this.mTutorialKeyWasDown && this.SexyZuma != null)
			{
				Board board = this.SexyZuma.GetBoard();
				if (board != null && Common.size<ZumaTip>(board.mZumaTips) > 0 && board.mZumaTips[0].mClickDismiss && board.mZumaTips[0].mUpdateCount >= 15)
				{
					bool allowFire;
					board.BlockInputForTutorial(board.mWidth / 2, board.mHeight / 2, out allowFire);
				}
			}
			this.mTutorialKeyWasDown = tutorialKey;
		}

		private void HandleBackButton()
		{
			if (this.isLoading)
			{
				return;
			}
			this.SexyZuma.OnHardwareBackButtonPressed();
		}

		private void SyncInputMouseRects()
		{
			if (this.SexyZuma == null)
			{
				return;
			}
			Viewport viewport = base.GraphicsDevice.Viewport;
			WidgetManager widgetManager = this.SexyZuma.mWidgetManager;
			if (widgetManager.mMouseSourceRect.mWidth == viewport.Width && widgetManager.mMouseSourceRect.mHeight == viewport.Height && widgetManager.mMouseDestRect.mWidth == this.SexyZuma.mWidth && widgetManager.mMouseDestRect.mHeight == this.SexyZuma.mHeight)
			{
				return;
			}
			this.SexyZuma.SyncMouseRectsFromViewport();
		}

		private void UpdateMouseInput()
		{
			MouseState mouseState = Mouse.GetState();
			int x = mouseState.X;
			int y = mouseState.Y;
			double timestamp = DateTime.Now.TimeOfDay.TotalMilliseconds;
			WidgetManager widgetManager = this.SexyZuma.mWidgetManager;
			if (mouseState.LeftButton == ButtonState.Pressed)
			{
				if (!this.mMouseLeftDown)
				{
					this.mMouseLeftDown = true;
					this.touch.SetTouchInfo(new Point(x, y), (_TouchPhase)0, timestamp);
					this.SexyZuma.TouchBegan(this.touch);
				}
				else if (x != this.mLastMouseX || y != this.mLastMouseY)
				{
					this.touch.SetTouchInfo(new Point(x, y), (_TouchPhase)1, timestamp);
					this.SexyZuma.TouchMoved(this.touch);
				}
			}
			else if (this.mMouseLeftDown)
			{
				this.mMouseLeftDown = false;
				this.touch.SetTouchInfo(new Point(x, y), (_TouchPhase)3, timestamp);
				this.SexyZuma.TouchEnded(this.touch);
				widgetManager.MouseMove(x, y);
			}
			else
			{
				widgetManager.MouseMove(x, y);
			}
			this.mLastMouseX = x;
			this.mLastMouseY = y;
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x0009FBA8 File Offset: 0x0009DDA8
		public void DrawSysString(string str, float x, float y)
		{
			this.spriteBatch.Begin();
			this.spriteBatch.DrawString(this.mSpriteFont, str, new Vector2(x, y), Color.Yellow);
			this.spriteBatch.End();
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x0009FBDE File Offset: 0x0009DDDE
		public void OrientationChanged(object sender, EventArgs e)
		{
			if ((int)base.Window.CurrentOrientation == 1)
			{
				if (this.SexyZuma != null)
				{
					this.SexyZuma.SetOrientation(0);
					return;
				}
			}
			else if (this.SexyZuma != null)
			{
				this.SexyZuma.SetOrientation(1);
			}
		}

		// Token: 0x040018E5 RID: 6373
		private GameApp SexyZuma;

		// Token: 0x040018E6 RID: 6374
		private Texture2D splashEA;

		// Token: 0x040018E7 RID: 6375
		private Texture2D splash;

		// Token: 0x040018E8 RID: 6376
		private Color mColor = new Color(255, 255, 255, 255);

		// Token: 0x040018E9 RID: 6377
		private float mAlpha = 255f;

		// Token: 0x040018EA RID: 6378
		private float mAlphaInc = -6f;

		// Token: 0x040018EB RID: 6379
		private double mAlphaDelay = 1.0;

		// Token: 0x040018EC RID: 6380
		private int mSplashId = 1;

		// Token: 0x040018ED RID: 6381
		private SpriteBatch spriteBatch;

		// Token: 0x040018EE RID: 6382
		private bool isLoading = true;

		// Token: 0x040018EF RID: 6383
		private bool mInitBegin;

		// Token: 0x040018F0 RID: 6384
		private int FirstLoad;

		// Token: 0x040018F1 RID: 6385
		private SpriteFont mSpriteFont;

		// Token: 0x040018F2 RID: 6386
		private double mElipseTime;

		// Token: 0x040018F3 RID: 6387
		private int mCurrentTouchId = -1;

		// Token: 0x040018F4 RID: 6388
		private static int frames = 0;

		// Token: 0x040018F5 RID: 6389
		private static DateTime now;

		// Token: 0x040018F6 RID: 6390
		private static DateTime preFPSTime;

		// Token: 0x040018F7 RID: 6391
		private static string fpsDisplayText = "";

		// Token: 0x040018F8 RID: 6392
		public PhoneApplicationService gApplicationService;

		// Token: 0x040018F9 RID: 6393
		private long totalBytes;

		// Token: 0x040018FA RID: 6394
		private long currentBytes;

		// Token: 0x040018FB RID: 6395
		private long peakBytes;

		// Token: 0x040018FC RID: 6396
		private long limitBytes;

		// Token: 0x040018FD RID: 6397
		private Vector2 mFPSPos = new Vector2(60f, 10f);

		// Token: 0x04001901 RID: 6401
		private SexyAppBase.Touch touch = new SexyAppBase.Touch();

		private bool mMouseLeftDown;

		private bool mEscapeWasDown;

		private bool mTutorialKeyWasDown;

		private int mLastMouseX = -1;

		private int mLastMouseY = -1;

		private void DrawFpsOverlay()
		{
			if (this.SexyZuma == null || !this.SexyZuma.mShowFPS || this.mSpriteFont == null)
			{
				return;
			}
			GameMain.frames++;
			GameMain.now = DateTime.Now;
			if ((GameMain.now - GameMain.preFPSTime).TotalSeconds >= 1.0)
			{
				GameMain.fpsDisplayText = "FPS: " + GameMain.frames;
				GameMain.frames = 0;
				GameMain.preFPSTime = GameMain.now;
			}
			if (GameMain.fpsDisplayText.Length == 0)
			{
				GameMain.fpsDisplayText = "FPS: ...";
			}
			this.spriteBatch.Begin();
			this.spriteBatch.DrawString(this.mSpriteFont, GameMain.fpsDisplayText, this.mFPSPos, Color.Yellow);
			if (this.SexyZuma.mDebugOverlayText.Length > 0)
			{
				this.spriteBatch.DrawString(this.mSpriteFont, this.SexyZuma.mDebugOverlayText, new Vector2(this.mFPSPos.X, this.mFPSPos.Y + 24f), Color.White);
			}
			this.spriteBatch.End();
		}

		private void TrySetWindowIcon()
		{
			if (!OperatingSystem.IsWindows())
			{
				return;
			}
			string iconPath = Path.Combine(AppContext.BaseDirectory, "Zuma's Revenge!.ico");
			if (!File.Exists(iconPath))
			{
				return;
			}
			IntPtr windowHandle = base.Window.Handle;
			if (windowHandle == IntPtr.Zero)
			{
				return;
			}
			const uint IMAGE_ICON = 1U;
			const uint LR_LOADFROMFILE = 16U;
			const int WM_SETICON = 128;
			const int ICON_SMALL = 0;
			const int ICON_BIG = 1;
			IntPtr iconHandle = LoadImage(IntPtr.Zero, iconPath, IMAGE_ICON, 0, 0, LR_LOADFROMFILE);
			if (iconHandle == IntPtr.Zero)
			{
				return;
			}
			SendMessage(windowHandle, WM_SETICON, (IntPtr)ICON_SMALL, iconHandle);
			SendMessage(windowHandle, WM_SETICON, (IntPtr)ICON_BIG, iconHandle);
		}

		[DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern IntPtr LoadImage(IntPtr hInst, string name, uint type, int cx, int cy, uint fuLoad);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
	}
}
