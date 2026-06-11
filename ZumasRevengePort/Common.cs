using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SexyFramework;
using SexyFramework.Drivers;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.PIL;
using SexyFramework.Resource;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	// Token: 0x02000005 RID: 5
	public static class Common
	{
		// Token: 0x06000017 RID: 23 RVA: 0x000029E7 File Offset: 0x00000BE7
		public static bool IsDeprecatedPowerUp(PowerType ptype)
		{
			return ptype == PowerType.PowerType_Fireball || ptype == PowerType.PowerType_ShieldFrog || ptype == PowerType.PowerType_FreezeBoss || ptype == PowerType.PowerType_BallEater || ptype == PowerType.PowerType_BombBullet || ptype == PowerType.PowerType_Lob;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002A06 File Offset: 0x00000C06
		public static bool StrEquals(string str1, string str2, bool pIgnoreCase)
		{
			if (!pIgnoreCase)
			{
				return str1 == str2;
			}
			return string.Compare(str1, str2, true) == 0;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002A1E File Offset: 0x00000C1E
		public static bool StrEquals(string str1, string str2)
		{
			return Common.StrEquals(str1, str2, true);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002A28 File Offset: 0x00000C28
		public static bool StrICaseEquals(string str1, string str2)
		{
			return string.Compare(str1, str2, true) == 0;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002A35 File Offset: 0x00000C35
		public static int GetDefaultBallRadius()
		{
			if (GameApp.gApp.mGraphicsDriver.Is3D())
			{
				return 18;
			}
			return 17;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002A4D File Offset: 0x00000C4D
		public static int GetDefaultBallSize()
		{
			return Common.GetDefaultBallRadius() * 2;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002A58 File Offset: 0x00000C58
		public static void MirrorPoint(ref float x, ref float y, MirrorType theMirror)
		{
			switch (theMirror)
			{
			case MirrorType.MirrorType_X:
				x = (float)GameApp.gApp.mWidth - x;
				return;
			case MirrorType.MirrorType_Y:
				y = (float)GameApp.gApp.mHeight - y;
				return;
			case MirrorType.MirrorType_XY:
				x = (float)GameApp.gApp.mWidth - x;
				y = (float)GameApp.gApp.mHeight - y;
				return;
			default:
				return;
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002AC0 File Offset: 0x00000CC0
		public static void SetupDialog(Dialog theDialog)
		{
			theDialog.SetHeaderFont(Res.GetFontByID(ResID.FONT_SHAGLOUNGE38_YELLOW));
			theDialog.SetLinesFont(Res.GetFontByID(ResID.FONT_SHAGLOUNGE38_YELLOW));
			theDialog.SetColor(0, new Color(203, 201, 187));
			theDialog.SetColor(1, new Color(244, 148, 28));
			theDialog.mPriority = 1;
			Common.SetupDialogButton(theDialog.mYesButton);
			Common.SetupDialogButton(theDialog.mNoButton);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002B38 File Offset: 0x00000D38
		public static void SetupDialogButton(DialogButton theButton)
		{
			if (theButton == null)
			{
				return;
			}
			theButton.mTranslateX = -1;
			theButton.mTranslateY = 1;
			int mNumCols = theButton.mComponentImage.mNumCols;
			int num = theButton.mComponentImage.mWidth / mNumCols;
			int mHeight = theButton.mComponentImage.mHeight;
			if (mNumCols == 3)
			{
				theButton.mNormalRect = new Rect(0, 0, num, mHeight);
				theButton.mOverRect = new Rect(num, 0, num, mHeight);
				theButton.mDownRect = new Rect(num * 2, 0, num, mHeight);
			}
			theButton.SetFont(Res.GetFontByID(ResID.FONT_SHAGLOUNGE28_GREEN));
			theButton.SetColor(1, new Color(16777215));
			theButton.mHasAlpha = true;
			theButton.mHasTransparencies = true;
			if (theButton.mWidth == 0)
			{
				int mX = theButton.mX;
				int mY = theButton.mY;
				int num2 = theButton.mFont.StringWidth(theButton.mLabel);
				int mHeight2 = theButton.mComponentImage.mHeight;
				theButton.Resize(mX, mY, num2, mHeight2);
			}
			theButton.mIsDown = false;
			theButton.mIsOver = false;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002C30 File Offset: 0x00000E30
		public static DialogButton MakeButton(int theId, ButtonListener theListener, string theText)
		{
			DialogButton dialogButton = new DialogButton(Res.GetImageByID(ResID.IMAGE_GUI_DIALOG_BUTTON), theId, theListener);
			dialogButton.mLabel = theText;
			Common.SetupDialogButton(dialogButton);
			return dialogButton;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002C60 File Offset: 0x00000E60
		public static DialogButton MakeButton(int theId, Image theButtonImage, ButtonListener theListener, string theText)
		{
			DialogButton dialogButton = new DialogButton(theButtonImage, theId, theListener);
			dialogButton.mLabel = theText;
			Common.SetupDialogButton(dialogButton);
			return dialogButton;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002C84 File Offset: 0x00000E84
		public static void SizeButtonsToLabel(ButtonWidget[] inButtons, int inButtonCount, int inXPadding)
		{
			int num = 0;
			for (int i = 0; i < inButtonCount; i++)
			{
				ButtonWidget buttonWidget = inButtons[i];
				if (buttonWidget.mFont == null)
				{
					return;
				}
				int num2 = buttonWidget.mFont.StringWidth(buttonWidget.mLabel);
				if (num2 > num)
				{
					num = num2;
				}
			}
			num += inXPadding * 2;
			for (int j = 0; j < inButtonCount; j++)
			{
				ButtonWidget buttonWidget2 = inButtons[j];
				buttonWidget2.Resize((int)((float)buttonWidget2.mX - (float)(num - buttonWidget2.mWidth) * 0.5f), buttonWidget2.mY, num, buttonWidget2.mHeight);
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002D10 File Offset: 0x00000F10
		public static void SetFXNumScale(PIEffect p, float scale)
		{
			if (p == null)
			{
				return;
			}
			int num = 0;
			for (;;)
			{
				PILayer layer = p.GetLayer(num);
				if (layer == null)
				{
					break;
				}
				int num2 = 0;
				for (;;)
				{
					PIEmitterInstance emitter = layer.GetEmitter(num2);
					if (emitter == null)
					{
						break;
					}
					emitter.mNumberScale = scale;
					num2++;
				}
				num++;
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002D50 File Offset: 0x00000F50
		public static void DrawCommonDialogBorder(SexyGraphics g, int x, int y, int width, int height)
		{
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_BAMBOOTOPEDGE);
			Image imageByID2 = Res.GetImageByID(ResID.IMAGE_GUI_BAMBOOBOTEDGE);
			Image imageByID3 = Res.GetImageByID(ResID.IMAGE_GUI_BAMBOOBOT);
			Image imageByID4 = Res.GetImageByID(ResID.IMAGE_GUI_BAMBOOSIDE);
			g.SetColorizeImages(false);
			g.ClearClipRect();
			g.DrawImage(imageByID, x, y);
			g.DrawImageMirror(imageByID, x + width - imageByID.GetWidth(), y);
			g.DrawImage(imageByID2, x, y + height - imageByID2.GetHeight());
			g.DrawImageMirror(imageByID2, x + width - imageByID2.GetWidth(), y + height - imageByID2.GetHeight());
			g.SetClipRect(x + imageByID.GetWidth(), y, width - imageByID.GetWidth() * 2, height);
			for (int i = x + imageByID.GetWidth(); i < x + width - imageByID.GetWidth(); i += imageByID3.GetWidth())
			{
				g.DrawImage(imageByID3, i, y - 1);
				g.DrawImage(imageByID3, i, y + height - imageByID3.GetHeight() + 1);
			}
			g.ClearClipRect();
			g.SetClipRect(x, y + imageByID.GetHeight(), width, height - imageByID.GetHeight() * 2);
			for (int j = y + imageByID.GetHeight(); j < y + height - imageByID.GetHeight(); j += imageByID4.GetHeight())
			{
				g.DrawImage(imageByID4, x, j);
				g.DrawImage(imageByID4, x + width - imageByID4.GetWidth(), j);
			}
			g.ClearClipRect();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002EAC File Offset: 0x000010AC
		public static int _GetWordWrappedHeight(string inText, Font inFont, int inWidth)
		{
			List<string> list = Common.Split(inText);
			int num = 0;
			int num2 = 1;
			int num3 = inFont.CharWidth(' ');
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i] == "\n")
				{
					num2++;
					num = 0;
				}
				else
				{
					int num4 = inFont.StringWidth(list[i]);
					if (num + num4 + num3 <= inWidth)
					{
						num += num4 + num3;
					}
					else if (num + num4 <= inWidth)
					{
						num += num4;
					}
					else
					{
						num2++;
						num = num4 + num3;
					}
				}
			}
			int num5 = inFont.GetHeight() - inFont.GetAscent();
			return num2 * inFont.GetHeight() - num5;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002F58 File Offset: 0x00001158
		public static void DrawCommonDialogBacking(SexyGraphics g, int x, int y, int width, int height)
		{
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_DIALOG_BOX_MAINMENU_FRAME_WOOD);
			Image imageByID2 = Res.GetImageByID(ResID.IMAGE_GUI_BAMBOOSIDE);
			Image imageByID3 = Res.GetImageByID(ResID.IMAGE_GUI_BAMBOOBOT);
			Image imageByID4 = Res.GetImageByID(ResID.IMAGE_GUI_BAMBOOTOPEDGE);
			Image imageByID5 = Res.GetImageByID(ResID.IMAGE_GUI_BAMBOOBOTEDGE);
			g.ClearClipRect();
			g.ClipRect(x + imageByID2.GetWidth() - 2, y + imageByID3.GetHeight() - 3, width + 4 - imageByID2.GetWidth() * 2, height + 10 - imageByID3.GetHeight() * 2);
			int i = x;
			int j = y;
			bool flag = false;
			while (j <= y + height + imageByID.GetHeight())
			{
				while (i < x + width + imageByID.GetWidth())
				{
					if (flag)
					{
						g.DrawImageMirror(imageByID, i, j);
					}
					else
					{
						g.DrawImage(imageByID, i, j);
					}
					i += imageByID.GetWidth();
					flag = !flag;
				}
				i = x;
				j += imageByID.GetHeight();
			}
			g.ClearClipRect();
			g.DrawImage(imageByID4, x, y);
			g.DrawImageMirror(imageByID4, x + width - imageByID4.GetWidth(), y);
			g.DrawImage(imageByID5, x, y + height - imageByID5.GetHeight());
			g.DrawImageMirror(imageByID5, x + width - imageByID5.GetWidth(), y + height - imageByID5.GetHeight());
			g.SetClipRect(x + imageByID4.GetWidth(), y, width - imageByID4.GetWidth() * 2, height);
			for (int k = x + imageByID4.GetWidth(); k < x + width - imageByID4.GetWidth(); k += imageByID3.GetWidth())
			{
				g.DrawImage(imageByID3, k, y - 1);
				g.DrawImage(imageByID3, k, y + height - imageByID3.GetHeight() + 1);
			}
			g.ClearClipRect();
			g.SetClipRect(x, y + imageByID4.GetHeight(), width, height - imageByID4.GetHeight() * 2);
			for (int l = y + imageByID4.GetHeight(); l < y + height - imageByID4.GetHeight(); l += imageByID2.GetHeight())
			{
				g.DrawImage(imageByID2, x, l);
				g.DrawImage(imageByID2, x + width - imageByID2.GetWidth(), l);
			}
			g.ClearClipRect();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000315C File Offset: 0x0000135C
		public static bool ExtractAdventureStatsResources(ResourceManager res)
		{
			return true;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000315F File Offset: 0x0000135F
		public static int GetIdByStringId(string theStringId)
		{
			return 0;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003164 File Offset: 0x00001364
		public static int GetBoardStateCount()
		{
			Board board = ((GameApp)GlobalMembers.gSexyApp).GetBoard();
			if (board == null)
			{
				return 0;
			}
			return board.GetStateCount();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000318C File Offset: 0x0000138C
		public static uint GetBoardTickCount()
		{
			return (uint)(Common.GetBoardStateCount() * 10);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00003196 File Offset: 0x00001396
		public static float _S(float value)
		{
			return GameApp.ScaleNum(value);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000319E File Offset: 0x0000139E
		public static int _S(int value)
		{
			return GameApp.ScaleNum(value);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000031A6 File Offset: 0x000013A6
		public static float _SS(float value)
		{
			return GameApp.ScreenScaleNum(value);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000031AE File Offset: 0x000013AE
		public static int _SS(int value)
		{
			return GameApp.ScreenScaleNum(value);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000031B6 File Offset: 0x000013B6
		public static string _MP(string value)
		{
			return value;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000031B9 File Offset: 0x000013B9
		public static float _M(float value)
		{
			return value;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000031BC File Offset: 0x000013BC
		public static float _M1(float value)
		{
			return Common._M(value);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000031C4 File Offset: 0x000013C4
		public static float _M2(float value)
		{
			return Common._M(value);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000031CC File Offset: 0x000013CC
		public static float _M3(float value)
		{
			return Common._M(value);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000031D4 File Offset: 0x000013D4
		public static float _M4(float value)
		{
			return Common._M(value);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000031DC File Offset: 0x000013DC
		public static float _M5(float value)
		{
			return Common._M(value);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000031E4 File Offset: 0x000013E4
		public static float _M6(float value)
		{
			return Common._M(value);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000031EC File Offset: 0x000013EC
		public static float _M7(float value)
		{
			return Common._M(value);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000031F4 File Offset: 0x000013F4
		public static int _M(int value)
		{
			return value;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000031F7 File Offset: 0x000013F7
		public static int _M1(int value)
		{
			return Common._M(value);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000031FF File Offset: 0x000013FF
		public static int _M2(int value)
		{
			return Common._M(value);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003207 File Offset: 0x00001407
		public static int _M3(int value)
		{
			return Common._M(value);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000320F File Offset: 0x0000140F
		public static int _M4(int value)
		{
			return Common._M(value);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003217 File Offset: 0x00001417
		public static int _M5(int value)
		{
			return Common._M(value);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000321F File Offset: 0x0000141F
		public static int _M6(int value)
		{
			return Common._M(value);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003227 File Offset: 0x00001427
		public static int _M7(int value)
		{
			return Common._M(value);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000322F File Offset: 0x0000142F
		public static int _M8(int value)
		{
			return Common._M(value);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003237 File Offset: 0x00001437
		public static int _M9(int value)
		{
			return Common._M(value);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000323F File Offset: 0x0000143F
		public static float _SA(float value, float add)
		{
			return value;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003242 File Offset: 0x00001442
		public static float _DS(float value)
		{
			return GameApp.DownScaleNum(value);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000324A File Offset: 0x0000144A
		public static int _DS(int value)
		{
			return GameApp.DownScaleNum(value);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003252 File Offset: 0x00001452
		public static float _DSA(float value, float add)
		{
			return GameApp.DownScaleNum(value, add);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000325C File Offset: 0x0000145C
		public static List<string> Split(string inText)
		{
			Common.mTotalWords.Clear();
			string[] array = inText.Split(new char[]
			{
				'\n'
			});
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(new char[]
				{
					' '
				});
				for (int j = 0; j < array2.Length; j++)
				{
					Common.mTotalWords.Add(array2[j]);
				}
				if (array.Length > 1)
				{
					Common.mTotalWords.Add("\n");
				}
			}
			return Common.mTotalWords;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000032E6 File Offset: 0x000014E6
		public static bool BossLevel(Level level)
		{
			return level != null && (level.IsFinalBossLevel() || level.mBoss != null || level.mEndSequence > 0);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003308 File Offset: 0x00001508
		public static string PowerupToStr(PowerType t, bool all_caps)
		{
			int id = 0;
			switch (t)
			{
			case PowerType.PowerType_ProximityBomb:
				id = (all_caps ? 696 : 697);
				break;
			case PowerType.PowerType_SlowDown:
				id = (all_caps ? 698 : 699);
				break;
			case PowerType.PowerType_Accuracy:
				id = (all_caps ? 700 : 701);
				break;
			case PowerType.PowerType_MoveBackwards:
				id = (all_caps ? 702 : 703);
				break;
			case PowerType.PowerType_Cannon:
				id = (all_caps ? 704 : 705);
				break;
			case PowerType.PowerType_ColorNuke:
				id = (all_caps ? 706 : 707);
				break;
			case PowerType.PowerType_Laser:
				id = (all_caps ? 708 : 709);
				break;
			case PowerType.PowerType_GauntletMultBall:
				id = (all_caps ? 710 : 711);
				break;
			}
			return TextManager.getInstance().getString(id);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000033FD File Offset: 0x000015FD
		public static bool LinesIntersect(FPoint a1, FPoint a2, FPoint b1, FPoint b2)
		{
			return Common.LinesIntersect(a1, a2, b1, b2, null);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000340C File Offset: 0x0000160C
		public static bool LinesIntersect(FPoint a1, FPoint a2, FPoint b1, FPoint b2, FPoint intersectFPoint)
		{
			if ((a1.mX == a2.mX && a1.mY == a2.mY) || (b1.mX == b2.mX && b1.mY == b2.mY))
			{
				return false;
			}
			a2.mX -= a1.mX;
			a2.mY -= a1.mY;
			b1.mX -= a1.mX;
			b1.mY -= a1.mY;
			b2.mX -= a1.mX;
			b2.mY -= a1.mY;
			double num = Math.Sqrt((double)(a2.mX * a2.mX + a2.mY * a2.mY));
			double num2 = (double)a2.mX / num;
			double num3 = (double)a2.mY / num;
			double num4 = (double)b1.mX * num2 + (double)b1.mY * num3;
			b1.mY = (float)((double)b1.mY * num2 - (double)b1.mX * num3);
			b1.mX = (float)num4;
			num4 = (double)b2.mX * num2 + (double)b2.mY * num3;
			b2.mY = (float)((double)b2.mY * num2 - (double)b2.mX * num3);
			b2.mX = (float)num4;
			if ((b1.mY < 0f && b2.mY < 0f) || (b1.mY >= 0f && b2.mY >= 0f))
			{
				return false;
			}
			double num5 = (double)(b2.mX + (b1.mX - b2.mX) * b2.mY / (b2.mY - b1.mY));
			if (num5 < 0.0 || num5 > num)
			{
				return false;
			}
			if (intersectFPoint != null)
			{
				intersectFPoint.mX = (float)((double)a1.mX + num5 * num2);
				intersectFPoint.mY = (float)((double)a1.mY + num5 * num3);
			}
			return true;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003610 File Offset: 0x00001810
		public static float GetCanonicalAngleRad(float theRad)
		{
			if (theRad >= 0f && theRad < 6.2831855f)
			{
				return theRad;
			}
			return Common.AceModF(theRad, 6.2831855f);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000362F File Offset: 0x0000182F
		private static float AceModF(float x, float y)
		{
			if (x < 0f)
			{
				return y - -x % y;
			}
			return x % y;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003643 File Offset: 0x00001843
		public static string PILGetNameByImage(Image img)
		{
			return img.mNameForRes;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x0000364C File Offset: 0x0000184C
		public static Image PILGetImageByName(string name)
		{
			SharedImageRef sharedImageRef = GameApp.gApp.mResourceManager.LoadImage(name);
			if (sharedImageRef != null)
			{
				return sharedImageRef.GetImage();
			}
			return null;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003675 File Offset: 0x00001875
		public static int PILGetIDByImage(Image img)
		{
			return Res.GetIDByImage(img);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000367D File Offset: 0x0000187D
		public static Image PILGetImageByID(int id)
		{
			return Res.GetImageByID((ResID)id);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003688 File Offset: 0x00001888
		public static void SerializePIEffect(PIEffect s, DataSync sync)
		{
			Buffer buffer = new Buffer();
			s.SaveState(buffer);
			Buffer buffer2 = sync.GetBuffer();
			buffer2.WriteLong((long)buffer.GetDataLen());
			buffer2.WriteBytes(buffer.GetDataPtr(), buffer.GetDataLen());
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000036CC File Offset: 0x000018CC
		public static void DeserializePIEffect(PIEffect s, DataSync sync)
		{
			Buffer buffer = sync.GetBuffer();
			int num = (int)buffer.ReadLong();
			byte[] array = new byte[num];
			buffer.ReadBytes(ref array, num);
			Buffer buffer2 = new Buffer();
			buffer2.SetData(array, num);
			s.LoadState(buffer2);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003710 File Offset: 0x00001910
		public static void SerializeParticleSystem(SexyFramework.PIL.System s, DataSync sync)
		{
			Buffer buffer = new Buffer();
			s.Serialize(buffer, new GlobalMembers.GetIdByImageFunc(Common.PILGetIDByImage));
			Buffer buffer2 = sync.GetBuffer();
			buffer2.WriteLong((long)buffer.GetDataLen());
			buffer2.WriteBytes(buffer.GetDataPtr(), buffer.GetDataLen());
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000375C File Offset: 0x0000195C
		public static SexyFramework.PIL.System DeserializeParticleSystem(DataSync sync)
		{
			Buffer buffer = sync.GetBuffer();
			int num = (int)buffer.ReadLong();
			byte[] array = new byte[num];
			buffer.ReadBytes(ref array, num);
			Buffer buffer2 = new Buffer();
			buffer2.SetData(array, num);
			SexyFramework.PIL.System system = SexyFramework.PIL.System.Deserialize(buffer2, new GlobalMembers.GetImageByIdFunc(Common.PILGetImageByID));
			system.mScale = Common._S(1f);
			return system;
		}

		// Token: 0x04000759 RID: 1881
		public const int MIN_LEVEL_FOR_BRONZE = 5;

		// Token: 0x0400075A RID: 1882
		public const int MIN_LEVEL_FOR_SILVER = 10;

		// Token: 0x0400075B RID: 1883
		public const int MIN_LEVEL_FOR_GOLD = 15;

		// Token: 0x0400075C RID: 1884
		public const int MAX_DRAW_PRIORITY = 5;

		// Token: 0x0400075D RID: 1885
		public const float MY_PI = 3.14159f;

		// Token: 0x0400075E RID: 1886
		public const int MAX_CURVES = 4;

		// Token: 0x0400075F RID: 1887
		public const int MAX_GUN_POINTS = 5;

		// Token: 0x04000760 RID: 1888
		public const int POINTS_FOR_EXTRA_LIFE = 50000;

		// Token: 0x04000761 RID: 1889
		public const int HOLE_SIZE = 96;

		// Token: 0x04000762 RID: 1890
		public const int PROXIMITY_BOMB_RADIUS = 56;

		// Token: 0x04000763 RID: 1891
		public const float EPSILON = 1E-06f;

		// Token: 0x04000764 RID: 1892
		public const float JL_PI = 3.1415927f;

		// Token: 0x04000765 RID: 1893
		public const float M_PI = 3.14159f;

		// Token: 0x04000766 RID: 1894
		public const float FLT_MAX = 3.4028235E+38f;

		// Token: 0x04000767 RID: 1895
		public const int MUSIC_LOADING = 0;

		// Token: 0x04000768 RID: 1896
		public const int MUSIC_MENU = 1;

		// Token: 0x04000769 RID: 1897
		public const int MUSIC_TUNE1 = 12;

		// Token: 0x0400076A RID: 1898
		public const int MUSIC_TUNE2 = 24;

		// Token: 0x0400076B RID: 1899
		public const int MUSIC_TUNE3 = 35;

		// Token: 0x0400076C RID: 1900
		public const int MUSIC_TUNE4 = 45;

		// Token: 0x0400076D RID: 1901
		public const int MUSIC_TUNE5 = 58;

		// Token: 0x0400076E RID: 1902
		public const int MUSIC_TUNE6 = 71;

		// Token: 0x0400076F RID: 1903
		public const int MUSIC_INTRO1 = 12;

		// Token: 0x04000770 RID: 1904
		public const int MUSIC_INTRO2 = 24;

		// Token: 0x04000771 RID: 1905
		public const int MUSIC_INTRO3 = 35;

		// Token: 0x04000772 RID: 1906
		public const int MUSIC_INTRO4 = 45;

		// Token: 0x04000773 RID: 1907
		public const int MUSIC_INTRO5 = 58;

		// Token: 0x04000774 RID: 1908
		public const int MUSIC_INTRO6 = 71;

		// Token: 0x04000775 RID: 1909
		public const int MUSIC_HI_SCORE = 116;

		// Token: 0x04000776 RID: 1910
		public const int MUSIC_GAME_OVER = 126;

		// Token: 0x04000777 RID: 1911
		public const int MUSIC_WON1 = 120;

		// Token: 0x04000778 RID: 1912
		public const int MUSIC_WON2 = 121;

		// Token: 0x04000779 RID: 1913
		public const int MUSIC_WON3 = 122;

		// Token: 0x0400077A RID: 1914
		public const int MUSIC_WON4 = 123;

		// Token: 0x0400077B RID: 1915
		public const int MUSIC_WON5 = 124;

		// Token: 0x0400077C RID: 1916
		public const int MUSIC_WON6 = 125;

		// Token: 0x0400077D RID: 1917
		public const int MUSIC_BOSS = 127;

		// Token: 0x0400077E RID: 1918
		public const int MUSIC_BOSS_WIN = 137;

		// Token: 0x0400077F RID: 1919
		public const int MUSIC_BONUS = 138;

		// Token: 0x04000780 RID: 1920
		public const int MUSIC_WON_GAME = 144;

		// Token: 0x04000781 RID: 1921
		public const int MUSIC_MISC1 = 95;

		// Token: 0x04000782 RID: 1922
		public const int MUSIC_MISC2 = 100;

		// Token: 0x04000783 RID: 1923
		public const int MUSIC_MISC3 = 105;

		// Token: 0x04000784 RID: 1924
		public const int MUSIC_MISC4 = 110;

		// Token: 0x04000785 RID: 1925
		public const int MUSIC_DANGER1 = 32;

		// Token: 0x04000786 RID: 1926
		public const int MUSIC_DANGER2 = 33;

		// Token: 0x04000787 RID: 1927
		public const int MUSIC_DANGER3 = 34;

		// Token: 0x04000788 RID: 1928
		public static List<string> mTotalWords = new List<string>();

		// Token: 0x04000789 RID: 1929
		public static bool[] gGotPowerUp = new bool[14];

		// Token: 0x0400078A RID: 1930
		public static bool gSuckMode = false;

		// Token: 0x0400078B RID: 1931
		public static bool gDieAtEnd = true;

		// Token: 0x0400078C RID: 1932
		public static bool gAddBalls = true;

		// Token: 0x0400078D RID: 1933
		public static int[] gBallColors = new int[]
		{
			1671423,
			16776960,
			16711680,
			65280,
			16711935,
			16777215
		};

		// Token: 0x0400078E RID: 1934
		public static int[] gBrightBallColors = new int[]
		{
			8454143,
			16777024,
			16755370,
			8454016,
			16744703,
			16777215
		};

		// Token: 0x0400078F RID: 1935
		public static int[] gDarkBallColors = new int[]
		{
			2299513,
			6312202,
			10489620,
			2114594,
			5641795,
			3676962
		};

		// Token: 0x04000790 RID: 1936
		public static int[] gTextBallColors = new int[]
		{
			2984959,
			16776960,
			16711680,
			65280,
			16711935,
			16777215
		};

		// Auto-generated forwards (MonoGame port)
		public static int size<T>(List<T> list) => SexyFramework.Common.size(list);
		public static T back<T>(List<T> list) => SexyFramework.Common.back(list);
		public static T front<T>(List<T> list) => SexyFramework.Common.front(list);
		public static void Reserve<T>(List<T> list, int newSize) => SexyFramework.Common.Reserve(list, newSize);
		public static void Resize<T>(List<T> list, int newSize) => SexyFramework.Common.Resize(list, newSize);
		public static T[] CreateObjectArray<T>(int size) => SexyFramework.Common.CreateObjectArray<T>(size);
		public static uint SexyTime() => SexyFramework.Common.SexyTime();
		public static string StringToWString(string theString) => SexyFramework.Common.StringToWString(theString);
		public static string WStringToString(string theString) => SexyFramework.Common.WStringToString(theString);
		public static bool StringToInt(string theString, ref int theIntVal) => SexyFramework.Common.StringToInt(theString, ref theIntVal);
		public static bool StringToDouble(string aTempString, ref double theDouble) => SexyFramework.Common.StringToDouble(aTempString, ref theDouble);
		public static string XMLDecodeString(string theString) => SexyFramework.Common.XMLDecodeString(theString);
		public static string GetFileName(string thePath, bool noExtension) => SexyFramework.Common.GetFileName(thePath, noExtension);
		public static string RemoveTrailingSlash(string theDirectory) => SexyFramework.Common.RemoveTrailingSlash(theDirectory);
		public static string GetCurDir() => SexyFramework.Common.GetCurDir();
		public static string GetFullPath(string theRelPath) => SexyFramework.Common.GetFullPath(theRelPath);
		public static string GetFileDir(string thePath, bool withSlash) => SexyFramework.Common.GetFileDir(thePath, withSlash);
		public static string GetPathFrom(string theRelPath, string theDir) => SexyFramework.Common.GetPathFrom(theRelPath, theDir);
		public static bool isSpace(char c) => SexyFramework.Common.isSpace(c);
		public static string Trim(string theString) => SexyFramework.Common.Trim(theString);
		public static bool DividePoly(Vector2[] v, int n, Vector2[,] theTris, int theMaxTris, ref int theNumTris) => SexyFramework.Common.DividePoly(v, n, theTris, theMaxTris, ref theNumTris);
		public static int Rand() => SexyFramework.Common.Rand();
		public static int Rand(int range) => SexyFramework.Common.Rand(range);
		public static float Rand(float range) => SexyFramework.Common.Rand(range);
		public static void SRand(uint theSeed) => SexyFramework.Common.SRand(theSeed);
		public static int SafeRand() => SexyFramework.Common.SafeRand();
		public static string CommaSeperate(int theValue) => SexyFramework.Common.CommaSeperate(theValue);
		public static string UCommaSeparate(uint theValue) => SexyFramework.Common.UCommaSeparate(theValue);
		public static string CommaSeperate64(long theValue) => SexyFramework.Common.CommaSeperate64(theValue);
		public static string UCommaSeparate64(uint theValue) => SexyFramework.Common.UCommaSeparate64(theValue);
		public static void SexySleep(int milliseconds) => SexyFramework.Common.SexySleep(milliseconds);
		public static IFileDriver GetGameFileDriver() => SexyFramework.Common.GetGameFileDriver();
		public static string GetAppDataFolder() => SexyFramework.Common.GetAppDataFolder();
		public static string SetAppDataFolder(string thePath) => SexyFramework.Common.SetAppDataFolder(thePath);
		public static int IntRange(int min_val, int max_val) => SexyFramework.Common.IntRange(min_val, max_val);
		public static float FloatRange(float min_val, float max_val) => SexyFramework.Common.FloatRange(min_val, max_val);
		public static float SAFE_RAND(float val) => SexyFramework.Common.SAFE_RAND(val);
		public static bool _eq(float n1, float n2, float tolerance) => SexyFramework.Common._eq(n1, n2, tolerance);
		public static bool _leq(float n1, float n2, float tolerance) => SexyFramework.Common._leq(n1, n2, tolerance);
		public static bool _geq(float n1, float n2, float tolerance) => SexyFramework.Common._geq(n1, n2, tolerance);
		public static bool _eq(float n1, float n2) => SexyFramework.Common._eq(n1, n2);
		public static bool _leq(float n1, float n2) => SexyFramework.Common._leq(n1, n2);
		public static bool _geq(float n1, float n2) => SexyFramework.Common._geq(n1, n2);
		public static int Sign(int val) => SexyFramework.Common.Sign(val);
		public static float Sign(float val) => SexyFramework.Common.Sign(val);
		public static float AngleBetweenPoints(float p1x, float p1y, float p2x, float p2y) => SexyFramework.Common.AngleBetweenPoints(p1x, p1y, p2x, p2y);
		public static float AngleBetweenPoints(Point p1, Point p2) => SexyFramework.Common.AngleBetweenPoints(p1, p2);
		public static SexyVector2 RotatePoint(float pAngle, SexyVector2 pVector, SexyVector2 pCenter) => SexyFramework.Common.RotatePoint(pAngle, pVector, pCenter);
		public static SexyVector2 RotatePoint(float pAngle, SexyVector2 pVector) => SexyFramework.Common.RotatePoint(pAngle, pVector);
		public static void RotatePoint(float pAngle, ref float x, ref float y, float cx, float cy) => SexyFramework.Common.RotatePoint(pAngle, ref x, ref y, cx, cy);
		public static void _RotatePointClockwise(Point p, float angle) => SexyFramework.Common._RotatePointClockwise(p, angle);
		public static bool RotatedRectsIntersect(Rect r1, float r1_angle, Rect r2, float r2_angle) => SexyFramework.Common.RotatedRectsIntersect(r1, r1_angle, r2, r2_angle);
		public static float DistFromPointToLine(Point line_p1, Point line_p2, Point p, ref float t) => SexyFramework.Common.DistFromPointToLine(line_p1, line_p2, p, ref t);
		public static float DistFromPointToLine(FPoint line_p1, FPoint line_p2, FPoint p, ref float t) => SexyFramework.Common.DistFromPointToLine(line_p1, line_p2, p, ref t);
		public static float DistFromPointToLine(Vector2 line_p1, Vector2 line_p2, Vector2 p, ref float t) => SexyFramework.Common.DistFromPointToLine(line_p1, line_p2, p, ref t);
		public static float Distance(float p1x, float p1y, float p2x, float p2y, bool sqrt) => SexyFramework.Common.Distance(p1x, p1y, p2x, p2y, sqrt);
		public static float Distance(float p1x, float p1y, float p2x, float p2y) => SexyFramework.Common.Distance(p1x, p1y, p2x, p2y);
		public static float StrToFloat(string str) => SexyFramework.Common.StrToFloat(str);
		public static int StrToInt(string str) => SexyFramework.Common.StrToInt(str);
		public static float RadiansToDegrees(float pRads) => SexyFramework.Common.RadiansToDegrees(pRads);
		public static float DegreesToRadians(float pDegs) => SexyFramework.Common.DegreesToRadians(pDegs);
		public static float CaculatePowValume(float volume) => SexyFramework.Common.CaculatePowValume(volume);
		public static bool _ATLIMIT(float cc, float mc, float d) => JeffLib.JCommon._ATLIMIT(cc, mc, d);
		public static bool DoneMoving(float coord, float vel, float target) => JeffLib.JCommon.DoneMoving(coord, vel, target);
		public static int GetAlphaFromUpdateCount(int update_count, int modifier) => JeffLib.JCommon.GetAlphaFromUpdateCount(update_count, modifier);
		public static void StringDimensions(string str, Font f, out int widest, out int height) => JeffLib.JCommon.StringDimensions(str, f, out widest, out height);
		public static void StringDimensions(string str, Font f, out int widest, out int height, bool real_newline) => JeffLib.JCommon.StringDimensions(str, f, out widest, out height, real_newline);
		public static string UpdateToTimeStr(int u) => JeffLib.JCommon.UpdateToTimeStr(u);
		public static string UpdateToTimeStr(int u, bool use_hour_field) => JeffLib.JCommon.UpdateToTimeStr(u, use_hour_field);
		public static string UpdateToTimeStr(int u, bool use_hour_field, int min_hour_digits) => JeffLib.JCommon.UpdateToTimeStr(u, use_hour_field, min_hour_digits);
		public static uint StrToHex(string str) => JeffLib.JCommon.StrToHex(str);
		public static int StrFindNoCase(string str, string cmp) => JeffLib.JCommon.StrFindNoCase(str, cmp);
		public static bool RightClick(int c) => JeffLib.JCommon.RightClick(c);
		public static string PathToResName(string path, string start_dir, string start_dir_replace_string) => JeffLib.JCommon.PathToResName(path, start_dir, start_dir_replace_string);
		public static string StripFileExtension(string fname) => JeffLib.JCommon.StripFileExtension(fname);
		public static string TruncateStr(string str, Font f, int width) => JeffLib.JCommon.TruncateStr(str, f, width);

	}
}
