using System;
using System.Collections.Generic;
using System.Linq;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	// Token: 0x02000025 RID: 37
	public class MapScreen : ButtonListener, IDisposable
	{
		// Token: 0x06000479 RID: 1145 RVA: 0x0003D40C File Offset: 0x0003B60C
		protected bool MouseOverCard(int idx)
		{
			return this.mCards[idx].Contains(this.mLastMouseX, this.mLastMouseY) && (idx == 0 || this.mOverlays[idx - 1].mUnlocked) && Enumerable.Count<KeyValuePair<int, Dialog>>(GameApp.gApp.mDialogMap) == 0;
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0003D460 File Offset: 0x0003B660
		protected void DrawDesaturatedZone(SexyGraphics g, int theZoneId, float theDesaturationPct)
		{
			if (theDesaturationPct <= 0f)
			{
				return;
			}
			g.Get3D();
			ResID id = ResID.IMAGE_UI_MAP_JUNGLE_OVERLAY + theZoneId - 1;
			Image imageByID = Res.GetImageByID(id);
			g.SetColor(255, 255, 255, (int)((double)(255f * theDesaturationPct) * this.mAlpha));
			g.DrawImage(imageByID, Common._DS(Res.GetOffsetXByID(id)) + Common._S(0) + (int)this.mUnlockScrollAmt, Common._DS(Res.GetOffsetYByID(id)));
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0003D4E8 File Offset: 0x0003B6E8
		public MapScreen()
		{
			Image imageByID = Res.GetImageByID(ResID.IMAGE_UI_MAP_OPENBOOK_PAGES);
			if (!GameApp.gApp.mResourceManager.IsGroupLoaded("CommonGame") && !GameApp.gApp.mResourceManager.LoadResources("CommonGame"))
			{
				GameApp.gApp.ShowResourceError(false);
			}
			if (!GameApp.gApp.mResourceManager.IsGroupLoaded("Map") && !GameApp.gApp.mResourceManager.LoadResources("Map"))
			{
				GameApp.gApp.ShowResourceError(false);
			}
			this.mParent = GlobalMembers.gSexyAppBase.mWidgetManager;
			this.mAlpha.SetConstant(1.0);
			this.mDirty = true;
			this.mUnlockScrollAmt = 0f;
			this.mHighestDot = 1;
			this.mSelectedZone = -1;
			this.mNewZoneTextSize = 1f;
			this.mNewZoneTextImg = null;
			this.mZoneEffect = null;
			this.mSlideDir = 0;
			this.mXOff = 0f;
			this.SetupClouds();
			this.mContinueBtn = null;
			this.mBackBtn = null;
			this.mSelectZoneBackBtn = null;
			this.mZoneBtn = null;
			this.mZoneOverPct = 0f;
			this.mExtrasAlpha.SetConstant(1.0);
			this.mIsTrialEnd = false;
			int num = (GameApp.gApp.mWidth - imageByID.mWidth) / 2 + Common._DS(Common._M(0)) - GameApp.gApp.mWideScreenXOffset / 2;
			int num2 = (GameApp.gApp.mHeight - imageByID.mHeight) / 2;
			int num3 = Common._DS(Common._M(200));
			for (int i = 0; i < 6; i++)
			{
				int num4 = -46 + num + ((i % 2 == 0) ? Common._DS(Common._M(250)) : Common._DS(Common._M1(845)));
				int num5 = Common._DS(Common._M(290)) + 10;
				int num6 = num2 + num3 + num5 * (i / 2) - 8;
				this.mCards[i] = new Rect(num4, num6, (int)(266f * Common._S(0.55f)), (int)(200f * Common._S(0.55f)));
			}
			this.mDisplayZoneAlpha = 0f;
			this.mIncDisplayZoneAlpha = true;
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0003D96C File Offset: 0x0003BB6C
		public virtual void Dispose()
		{
			if (this.mContinueBtn != null)
			{
				this.mParent.RemoveWidget(this.mContinueBtn);
			}
			if (this.mZoneBtn != null)
			{
				this.mParent.RemoveWidget(this.mZoneBtn);
			}
			if (this.mBackBtn != null)
			{
				this.mParent.RemoveWidget(this.mBackBtn);
			}
			if (this.mSelectZoneBackBtn != null)
			{
				this.mParent.RemoveWidget(this.mSelectZoneBackBtn);
			}
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0003D9E0 File Offset: 0x0003BBE0
		public void SetupClouds()
		{
			FPoint[] array = new FPoint[]
			{
				new FPoint((float)Common._M(900), (float)Common._M1(125)),
				new FPoint((float)Common._M2(870), (float)Common._M3(183)),
				new FPoint((float)Common._M4(850), (float)Common._M5(285)),
				new FPoint((float)Common._M(815), (float)Common._M1(380)),
				new FPoint((float)Common._M2(828), (float)Common._M3(477)),
				new FPoint((float)Common._M4(850), (float)Common._M5(570)),
				new FPoint((float)Common._M(430), (float)Common._M1(330)),
				new FPoint((float)Common._M2(470), (float)Common._M3(459)),
				new FPoint((float)Common._M4(544), (float)Common._M5(569)),
				new FPoint((float)Common._M(340), (float)Common._M1(530)),
				new FPoint((float)Common._M2(390), (float)Common._M3(596)),
				new FPoint((float)Common._M4(390), (float)Common._M5(704)),
				new FPoint((float)Common._M(540), (float)Common._M1(91)),
				new FPoint((float)Common._M2(463), (float)Common._M3(165)),
				new FPoint((float)Common._M4(460), (float)Common._M5(269))
			};
			FPoint[] array2 = new FPoint[]
			{
				new FPoint(Common._M(1.2f), Common._M1(1f)),
				new FPoint(Common._M2(1.1f), Common._M3(1.1f)),
				new FPoint(Common._M4(1.4f), Common._M5(1.3f)),
				new FPoint(Common._M(1.3f), Common._M1(1.35f)),
				new FPoint(Common._M2(1.1f), Common._M3(1f)),
				new FPoint(Common._M4(1.2f), Common._M5(1.25f)),
				new FPoint(Common._M(1.17f), Common._M1(1.4f)),
				new FPoint(Common._M2(0.9f), Common._M3(1f)),
				new FPoint(Common._M4(0.9f), Common._M5(1.2f)),
				new FPoint(Common._M(0.5f), Common._M1(1f)),
				new FPoint(Common._M2(0.5f), Common._M3(1.2f)),
				new FPoint(Common._M4(1f), Common._M5(1.5f)),
				new FPoint(Common._M(1.05f), Common._M1(1.1f)),
				new FPoint(Common._M2(1.05f), Common._M3(1.1f)),
				new FPoint(Common._M4(1.25f), Common._M5(1.15f))
			};
			for (int i = 0; i < 5; i++)
			{
				string theStringId = string.Format("IMAGE_UI_MAP_{0}", LevelMgr.GetTerseZoneName(i + 2).ToUpper());
				Common.GetIdByStringId(theStringId);
				this.mOverlays[i].mAlpha = (float)Common._M(255);
				this.mOverlays[i].mUnlocked = false;
				for (int j = 0; j < 3; j++)
				{
					this.mOverlays[i].mCloudSizes[j] = array2[i * 3 + j];
					this.mOverlays[i].mCloudPoints[j] = array[i * 3 + j];
				}
			}
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0003DE26 File Offset: 0x0003C026
		public void CloseDone()
		{
			this.mClosing = false;
			this.CleanButtons();
			this.mRemove = true;
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0003DE3C File Offset: 0x0003C03C
		public void CleanButtons()
		{
			this.mParent.RemoveWidget(this.mContinueBtn);
			GameApp.gApp.SafeDeleteWidget(this.mContinueBtn);
			this.mContinueBtn = null;
			this.mParent.RemoveWidget(this.mZoneBtn);
			GameApp.gApp.SafeDeleteWidget(this.mZoneBtn);
			this.mZoneBtn = null;
			this.mParent.RemoveWidget(this.mBackBtn);
			GameApp.gApp.SafeDeleteWidget(this.mBackBtn);
			this.mBackBtn = null;
			this.mParent.RemoveWidget(this.mSelectZoneBackBtn);
			GameApp.gApp.SafeDeleteWidget(this.mSelectZoneBackBtn);
			this.mSelectZoneBackBtn = null;
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0003DEEC File Offset: 0x0003C0EC
		public void Hide(bool h)
		{
			if (this.mContinueBtn != null)
			{
				this.mContinueBtn.SetVisible(!h);
				this.mContinueBtn.SetDisabled(h);
			}
			if (this.mZoneBtn != null)
			{
				this.mZoneBtn.SetVisible(!h);
				this.mZoneBtn.SetDisabled(h);
			}
			ButtonWidget buttonWidget = this.mBackBtn;
			MapGenericButton mapGenericButton = this.mSelectZoneBackBtn;
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0003DF50 File Offset: 0x0003C150
		public void Init(bool zone_completed, int disp_zone, int disp_level, bool from_checkpoint, bool from_load)
		{
			Image imageByID = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_HOME);
			Image imageByID2 = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_HOME_SELECT);
			Image imageByID3 = Res.GetImageByID(ResID.IMAGE_UI_MAP_OPENBOOK_BACK);
			Image imageByID4 = Res.GetImageByID(ResID.IMAGE_UI_MAP_OPENBOOK_BACK_DWN);
			Image imageByID5 = Res.GetImageByID(ResID.IMAGE_UI_MAP_OPENBOOK_PAGES);
			Image imageByID6 = Res.GetImageByID(ResID.IMAGE_UI_MAP_CONTINUE_BUTTON);
			Image imageByID7 = Res.GetImageByID(ResID.IMAGE_UI_MAP_BOOK_ANIM);
			Font fontByID = Res.GetFontByID(ResID.FONT_SHAGEXOTICA68_STROKE);
			this.mClosing = false;
			this.mIntroClosing = false;
			this.mExtrasAlpha.SetConstant(1.0);
			this.mUpdateCount = 0;
			this.mFromIntro = (!zone_completed && disp_zone == 1 && disp_level == 1 && !from_load);
			this.mHasPlayedZoneUnlockedSound = false;
			int num = GameApp.gApp.mClickedHardMode ? GameApp.gApp.mUserProfile.mHeroicModeVars.mHighestZoneBeat : GameApp.gApp.mUserProfile.mAdvModeVars.mHighestZoneBeat;
			this.mBeatGame = (num >= 6);
			this.mUnlockScrollAmt = 0f;
			this.mDisplayedZone = disp_zone;
			this.mCompletedZone = zone_completed;
			this.mContinueFromCheckpoint = false;
			this.mFromCheckpoint = (from_checkpoint && !from_load);
			this.mContinueGoesToCheckpoint = (from_load && from_checkpoint);
			if (this.mContinueBtn != null)
			{
				this.mParent.RemoveWidget(this.mContinueBtn);
			}
			if (this.mZoneBtn != null)
			{
				this.mParent.RemoveWidget(this.mZoneBtn);
			}
			if (this.mBackBtn != null)
			{
				this.mParent.RemoveWidget(this.mBackBtn);
			}
			if (this.mSelectZoneBackBtn != null)
			{
				this.mParent.RemoveWidget(this.mSelectZoneBackBtn);
			}
			this.mBackBtn = null;
			this.mContinueBtn = null;
			this.mZoneBtn = null;
			this.mSelectZoneBackBtn = null;
			this.mBackBtn = null;
			this.mContinueBtn = null;
			this.mZoneBtn = null;
			this.mSelectZoneBackBtn = null;
			this.mDisplayingZones = false;
			int num2 = (GameApp.gApp.GetBoard() == null) ? GameApp.gApp.mUserProfile.GetAdvModeVars().mCurrentAdvScore : GameApp.gApp.GetBoard().mScore;
			if (from_checkpoint && from_load)
			{
				Level checkpointLevel = GameApp.gApp.GetBoard().GetCheckpointLevel();
				disp_level = checkpointLevel.mNum;
				disp_zone = checkpointLevel.mZone;
				num2 = GameApp.gApp.GetBoard().GetCheckpointScore();
			}
			int[] array = new int[]
			{
				25,
				51,
				85,
				115,
				145,
				175
			};
			if (disp_level == 2147483647 || disp_level == 10)
			{
				this.mHighestDot = array[disp_zone - 1];
			}
			else if (disp_zone == 1)
			{
				this.mHighestDot = (int)((float)disp_level / 10f * (float)array[0]);
			}
			else
			{
				this.mHighestDot = (int)((float)disp_level / 10f * (float)(array[disp_zone - 1] - array[disp_zone - 2]) + (float)array[disp_zone - 2]);
			}
			if (!zone_completed && !this.mFromIntro)
			{
				this.mContinueBtn = new MapButton(1, this);
				this.mContinueBtn.mUsesAnimators = false;
				this.mContinueBtn.mMapScreen = this;
				this.mContinueBtn.mDoFinger = true;
				this.mContinueBtn.mPriority = 2;
				this.mContinueBtn.Resize(Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_MAP_CONTINUE_BUTTON) - 80) + Common._DS(14), Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_MAP_CONTINUE_BUTTON)) - Common._DS(Common._M1(6)), imageByID6.GetCelWidth(), imageByID6.GetCelHeight() + Common._DS(Common._M3(40)));
				this.mParent.AddWidget(this.mContinueBtn);
				string @string = TextManager.getInstance().getString(778);
				string string2 = TextManager.getInstance().getString(798);
				if ((int)Localization.GetCurrentLanguage() == 5 || (int)Localization.GetCurrentLanguage() == 10)
				{
					string text = @string.Substring(0, @string.IndexOf("$"));
					string text2 = string2.Substring(0, string2.IndexOf("（"));
					text2 = text2.Replace("$1", "{0}");
					this.mContinueBtn.mLevel = ((disp_level == int.MaxValue) ? string.Format(text + " {0}", disp_zone) : string.Format(text2, (disp_zone - 1) * 10 + disp_level));
				}
				else
				{
					string text3 = @string.Substring(0, @string.IndexOf(" "));
					string text4 = string2.Substring(0, string2.IndexOf(" "));
					this.mContinueBtn.mLevel = ((disp_level == int.MaxValue) ? string.Format(text3 + " {0}", disp_zone) : string.Format(text4 + " {0}", (disp_zone - 1) * 10 + disp_level));
				}
				if (GameApp.USE_TRIAL_VERSION && disp_level == 2147483647)
				{
					this.mIsTrialEnd = true;
				}
				string string3 = TextManager.getInstance().getString(863);
				this.mContinueBtn.mScore = string.Format("{0} " + string3, Common.CommaSeperate(num2));
				int num3 = (GameApp.gApp.GetBoard() == null) ? GameApp.gApp.mUserProfile.GetAdvModeVars().mCurrentAdvLives : GameApp.gApp.GetBoard().GetNumLives();
				if (num3 > 99)
				{
					num3 = 99;
				}
				num3--;
				if (num3 < 0)
				{
					num3 = 2;
				}
				this.mContinueBtn.mLives = string.Format("x{0}", num3);
				this.mZoneBtn = new MapGenericButton(2, this);
				this.mZoneBtn.mUsesAnimators = false;
				this.mZoneBtn.mDoFinger = true;
				this.mZoneBtn.mPriority = 2;
				this.mZoneBtn.mButtonImage = (this.mZoneBtn.mOverImage = (this.mZoneBtn.mDownImage = imageByID7));
				this.mZoneBtn.mNormalRect = this.mZoneBtn.mButtonImage.GetCelRect(0);
				this.mZoneBtn.mOverRect = this.mZoneBtn.mButtonImage.GetCelRect(0);
				this.mZoneBtn.mDownRect = this.mZoneBtn.mButtonImage.GetCelRect(1);
				this.mZoneBtn.Resize(Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_MAP_BOOK_ANIM) - 80), Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_MAP_BOOK_ANIM)), this.mZoneBtn.mButtonImage.GetCelWidth(), this.mZoneBtn.mButtonImage.GetCelHeight());
				this.mParent.AddWidget(this.mZoneBtn);
				this.mBackBtn = new ButtonWidget(3, this);
				this.mBackBtn.SetVisible(false);
				this.mBackBtn.SetDisabled(true);
				this.mBackBtn.mDoFinger = true;
				this.mBackBtn.mPriority = 2;
				this.mBackBtn.mButtonImage = imageByID;
				this.mBackBtn.mDownImage = imageByID2;
				float num4 = (float)(imageByID2.GetWidth() - imageByID.GetWidth()) / 2f;
				float num5 = (float)(imageByID2.GetHeight() - imageByID.GetHeight()) / 2f;
				this.mBackBtn.Resize(Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_CHALLENGESCREEN_HOME_SELECT)) - Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_CHALLENGESCREEN_HOME_SELECT)) + (int)num4, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_HOME_SELECT)) + (int)num5, imageByID2.GetWidth(), imageByID2.GetHeight());
				this.mBackBtn.mNormalRect = new Rect(0, 0, imageByID.GetWidth(), imageByID.GetHeight());
				float num6 = (float)((imageByID2.GetWidth() - imageByID.GetWidth()) / 2);
				float num7 = (float)((imageByID2.GetHeight() - imageByID.GetHeight()) / 2);
				this.mBackBtn.mDownRect = new Rect((int)num6, (int)num7, imageByID2.GetWidth() - (int)num6, imageByID2.GetHeight() - (int)num7);
				this.mParent.AddWidget(this.mBackBtn);
				this.mSelectZoneBackBtn = new MapGenericButton(4, this);
				this.mSelectZoneBackBtn.SetVisible(false);
				this.mSelectZoneBackBtn.SetDisabled(true);
				this.mSelectZoneBackBtn.mUsesAnimators = false;
				this.mSelectZoneBackBtn.mDoFinger = true;
				this.mSelectZoneBackBtn.mPriority = 2;
				this.mSelectZoneBackBtn.mButtonImage = imageByID3;
				this.mSelectZoneBackBtn.mDownImage = imageByID4;
				int num8 = (GameApp.gApp.GetScreenRect().mWidth - imageByID5.mWidth) / 2;
				this.mSelectZoneBackBtn.Resize(num8 + Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_MAP_OPENBOOK_BACK)), Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_MAP_OPENBOOK_BACK)), imageByID3.GetWidth(), imageByID3.GetHeight());
				this.mParent.AddWidget(this.mSelectZoneBackBtn);
				this.mSelectZoneBackBtn.SetVisible(false);
				this.mSelectZoneBackBtn.SetDisabled(true);
				MapScreen.gZoneNames[0] = TextManager.getInstance().getString(839);
				MapScreen.gZoneNames[1] = TextManager.getInstance().getString(840);
				MapScreen.gZoneNames[2] = TextManager.getInstance().getString(841);
				MapScreen.gZoneNames[3] = TextManager.getInstance().getString(842);
				MapScreen.gZoneNames[4] = TextManager.getInstance().getString(843);
				MapScreen.gZoneNames[5] = TextManager.getInstance().getString(844);
				int num9 = (disp_zone == 0) ? disp_zone : (disp_zone - 1);
				string text5 = MapScreen.gZoneNames[num9];
				this.mNewZoneTextImg = new DeviceImage();
				this.mNewZoneTextImg.SetImageMode(true, true);
				this.mNewZoneTextImg.AddImageFlags(16U);
				this.mNewZoneTextImg.Create(fontByID.StringWidth(text5) + 60, fontByID.mHeight * 2);
				SexyGraphics graphics = new SexyGraphics(this.mNewZoneTextImg);
				graphics.Get3D().ClearColorBuffer(new Color(0, 0));
				graphics.SetFont(fontByID);
				graphics.SetColor(Color.White);
				graphics.DrawString(text5, 0, fontByID.GetAscent());
				graphics.ClearRenderContext();
			}
			else if (disp_zone > 1 || this.mFromIntro)
			{
				this.mNewZoneTextImg = new DeviceImage();
				this.mNewZoneTextImg.SetImageMode(true, true);
				this.mNewZoneTextImg.AddImageFlags(16U);
				this.mNewZoneTextImg.Create(fontByID.StringWidth("Underwater Grotto!") + 60, fontByID.mHeight * 2);
				SexyGraphics graphics2 = new SexyGraphics(this.mNewZoneTextImg);
				graphics2.Get3D().ClearColorBuffer(new Color(0, 0));
				graphics2.SetFont(fontByID);
				graphics2.SetColor(Color.White);
				string text6 = string.Format("{0} {1}", LevelMgr.GetZoneName(disp_zone - 1), this.mBeatGame ? "..." : "");
				if (this.mFromIntro)
				{
					graphics2.DrawString(TextManager.getInstance().getString(661), (this.mNewZoneTextImg.mWidth - fontByID.StringWidth(TextManager.getInstance().getString(661))) / 2, fontByID.GetAscent());
					graphics2.DrawString(TextManager.getInstance().getString(662), (this.mNewZoneTextImg.mWidth - fontByID.StringWidth(TextManager.getInstance().getString(662))) / 2, fontByID.GetAscent() + fontByID.mHeight - Common._DS(Common._M(30)) + Localization.GetCurrentFontOffsetY());
				}
				else if (!this.mBeatGame)
				{
					graphics2.DrawString(text6, (this.mNewZoneTextImg.mWidth - fontByID.StringWidth(text6)) / 2, fontByID.GetAscent());
					graphics2.DrawString(TextManager.getInstance().getString(663), (this.mNewZoneTextImg.mWidth - fontByID.StringWidth(TextManager.getInstance().getString(663))) / 2, fontByID.GetAscent() + fontByID.mHeight - Common._DS(Common._M(10)));
				}
				else
				{
					graphics2.DrawString(TextManager.getInstance().getString(661), (this.mNewZoneTextImg.mWidth - fontByID.StringWidth(TextManager.getInstance().getString(661))) / 2, fontByID.GetAscent());
					graphics2.DrawString(text6, (this.mNewZoneTextImg.mWidth - fontByID.StringWidth(text6)) / 2, fontByID.GetAscent() + fontByID.mHeight - Common._DS(Common._M(30)));
				}
				this.mNewZoneTextSize = 0f;
				this.mNewZoneTextBounceCount = 0;
				graphics2.ClearRenderContext();
			}
			for (int i = 0; i < 5; i++)
			{
				this.mOverlays[i].mUnlocked = (i + 1 <= num);
				if (this.mOverlays[i].mUnlocked)
				{
					this.mOverlays[i].mAlpha = 0f;
				}
			}
			this.zone_effects[0] = null;
			this.zone_effects[1] = GameApp.gApp.GetPIEffect("goldsparkle_area_L2");
			this.zone_effects[2] = GameApp.gApp.GetPIEffect("goldsparkle_area_L3");
			this.zone_effects[3] = GameApp.gApp.GetPIEffect("goldsparkle_area_L4");
			this.zone_effects[4] = GameApp.gApp.GetPIEffect("goldsparkle_area_L5");
			this.zone_effects[5] = GameApp.gApp.GetPIEffect("goldsparkle_area_L6");
			this.mUnlockNameAlpha.SetConstant(1.0);
			this.mUnlockNameHilite.SetConstant(1.0);
			this.mUnlockOutlineAlpha.SetConstant(1.0);
			this.mUnlockIconAlpha.SetConstant(1.0);
			this.mClickToEnterAlpha.SetConstant(1.0);
			if (zone_completed)
			{
				this.mUnlockScrollAmt = (float)Common._DS(Common._M(-206));
				this.mZoneEffect = this.zone_effects[disp_zone - 1];
				this.mZoneEffect.ResetAnim();
				this.mZoneEffect.mEmitAfterTimeline = true;
				ResID resID = ResID.IMAGE_UI_MAP_JUNGLE_OVERLAY + disp_zone - 1;
				this.mZoneEffect.GetLayer("general sparkle").GetEmitter("sparkle area").mMaskImage = GameApp.gApp.mResourceManager.GetResourceRef(0, resID.ToString()).GetSharedImageRef();
				this.mZoneEffect.mDrawTransform.LoadIdentity();
				float num10 = GameApp.DownScaleNum(1f);
				this.mZoneEffect.mDrawTransform.Scale(num10, num10);
				this.mZoneEffect.mDrawTransform.Translate((float)(Common._DS(Res.GetOffsetXByID(resID)) - Common._DS(80)), (float)Common._DS(Res.GetOffsetYByID(resID)));
				for (int j = 0; j < disp_zone - 1; j++)
				{
					this.mOverlays[j].mUnlocked = true;
				}
				if (disp_zone > 1 && num < 6)
				{
					this.mOverlays[disp_zone - 2].mAlpha = 255f;
				}
				this.mUnlockNameAlpha.SetCurve(Common._MP("b;0,1,0.002,1,#########   z#### K~###  (~###  T~###"));
				this.mUnlockNameHilite.SetCurve(Common._MP("b+0,1,0,1,####  R#### =~m&F    a#### Q####"), this.mUnlockNameAlpha);
				this.mUnlockOutlineAlpha.SetCurve(Common._MP("b;0,1,0.001429,1,#########   }%###      $f###"));
				this.mUnlockIconAlpha.SetCurve(Common._MP("b+0,1,0,1,####  b#### .?### R#### oL###jL###  (####"), this.mUnlockNameAlpha);
				this.mClickToEnterAlpha.SetCurve(Common._MP("b+0,1,0,1,####     y#### b~###  D~###"), this.mUnlockNameAlpha);
				this.mDisableInput = true;
			}
			if (this.mFromIntro)
			{
				this.mUnlockNameAlpha.SetCurve(Common._MP("b+0,1,0.002,1,#########      :#### &~###  d~###"));
				this.mUnlockNameHilite.SetCurve(Common._MP("b+0,1,0,1,####     r#### T~### h####o####"), this.mUnlockNameAlpha);
				this.mUnlockOutlineAlpha.SetCurve(Common._MP("b+0,1,0.001429,1,#########   }%###      $N###"));
				this.mUnlockIconAlpha.SetCurve(Common._MP("b+0,1,0,1,####  b#### .?### R#### oL###jL###  (####"), this.mUnlockNameAlpha);
				this.mClickToEnterAlpha.SetCurve(Common._MP("b+0,1,0,1,####       w####h~### @~####~###"), this.mUnlockNameAlpha);
			}
			this.mRemove = false;
			this.mSelectedZone = -1;
			if (this.mFromCheckpoint)
			{
				this.ButtonDepress(this.mZoneBtn.mId);
			}
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0003EF0C File Offset: 0x0003D10C
		public void Update()
		{
			if (GameApp.gApp.IsHardwareBackButtonPressed())
			{
				this.ProcessHardwareBackButton();
			}
			this.mDirty = false;
			int num = Common._DS(Common._M(-206));
			foreach (Widget widget in this.mParent.mWidgets)
			{
				widget.SetDisabled(this.mAlpha.mRamp == 6);
			}
			if (this.mAlpha.mRamp == 6)
			{
				this.mDirty = true;
				if (!this.mAlpha.IncInVal())
				{
					if (this.mAlpha == 0.0)
					{
						this.CleanButtons();
						GameApp.gApp.mClickedHardMode = false;
						GameApp.gApp.HideAdventureModeMapScreen();
						return;
					}
					this.mAlpha.SetConstant(this.mAlpha);
				}
				GameApp.gApp.mMainMenu.MarkAllDirty();
				return;
			}
			if (this.mSlideDir != 0)
			{
				this.mDirty = true;
				float num2 = Common._M(60f);
				this.mXOff += (float)this.mSlideDir * num2;
				this.mZoneBtn.mX += (int)(num2 * (float)this.mSlideDir);
				this.mBackBtn.mX += (int)(num2 * (float)this.mSlideDir);
				this.mContinueBtn.mX += (int)(num2 * (float)this.mSlideDir);
				if (this.mSlideDir == -1)
				{
					if (this.mXOff <= 0f)
					{
						this.mSlideDir = 0;
						float num3 = -this.mXOff;
						this.mXOff = 0f;
						this.mZoneBtn.mX += (int)num3;
						this.mBackBtn.mX += (int)num3;
						this.mContinueBtn.mX += (int)num3;
					}
				}
				else if (this.mSlideDir == 1 && this.mXOff >= (float)(GameApp.gApp.mWidth + Common._S(80)))
				{
					this.mXOff = (float)(GameApp.gApp.mWidth + Common._S(80));
					this.mSlideDir = 0;
					this.CleanButtons();
					GameApp.gApp.mClickedHardMode = false;
					GameApp.gApp.HideAdventureModeMapScreen();
				}
				GameApp.gApp.mMainMenu.MarkAllDirty();
				return;
			}
			if (this.mIncDisplayZoneAlpha)
			{
				this.mDisplayZoneAlpha += 5f;
				if (this.mDisplayZoneAlpha >= 255f)
				{
					this.mDisplayZoneAlpha = 255f;
					this.mIncDisplayZoneAlpha = false;
				}
			}
			else
			{
				this.mDisplayZoneAlpha -= 5f;
				if (this.mDisplayZoneAlpha <= 0f)
				{
					this.mDisplayZoneAlpha = 0f;
					this.mIncDisplayZoneAlpha = true;
				}
			}
			Board board = GameApp.gApp.GetBoard();
			if (board != null && board.mEndBossFadeAmt > 0f)
			{
				this.mDirty = true;
				board.mEndBossFadeAmt -= Common._M(2f);
				if (board.mEndBossFadeAmt >= 0f)
				{
					return;
				}
				board.mEndBossFadeAmt = 0f;
			}
			this.mUpdateCount++;
			Common._M(50);
			Common._M(60);
			if (this.mUnlockScrollAmt <= (float)num && !this.mHasPlayedZoneUnlockedSound && this.mNewZoneTextImg != null && !this.mBeatGame && this.mUpdateCount >= Common._M(150))
			{
				this.mHasPlayedZoneUnlockedSound = true;
			}
			if (this.mFromIntro && this.mUpdateCount >= Common._M(100))
			{
				this.mUnlockNameAlpha.IncInVal();
				this.mUnlockOutlineAlpha.IncInVal();
			}
			if (this.mUpdateCount >= Common._M(60) && (this.mUnlockScrollAmt <= (float)num || (this.mFromIntro && this.mUpdateCount >= Common._M(130))))
			{
				if (this.mNewZoneTextBounceCount < Common._M(5))
				{
					this.mDirty = true;
					float num4 = Common._M(0.1f) * (float)((this.mNewZoneTextBounceCount % 2 == 0) ? 1 : -1);
					float num5 = Common._M(1.5f);
					if (this.mNewZoneTextBounceCount >= 2)
					{
						num5 /= 2f * (float)(this.mNewZoneTextBounceCount / 2);
					}
					this.mNewZoneTextSize += num4;
					if (num4 > 0f && this.mNewZoneTextSize > 1f + num5)
					{
						this.mNewZoneTextSize = 1f + num5;
						this.mNewZoneTextBounceCount++;
					}
					else if (num4 < 0f && this.mNewZoneTextSize <= 1f)
					{
						this.mNewZoneTextSize = 1f;
						this.mNewZoneTextBounceCount++;
					}
				}
				else if (this.mDisableInput)
				{
					this.mDisableInput = false;
				}
				if (!this.mFromIntro)
				{
					this.mZoneEffect.mDrawTransform.LoadIdentity();
					float num6 = GameApp.DownScaleNum(1f);
					this.mZoneEffect.mDrawTransform.Scale(num6, num6);
					this.mZoneEffect.mDrawTransform.Translate((float)(this.mAreaCoords[this.mDisplayedZone - 1].mX - Common._DS(80)) + this.mUnlockScrollAmt, (float)(this.mAreaCoords[this.mDisplayedZone - 1].mY + Common._DS(this.mZoneEffect.mHeight / 2)));
					this.mZoneEffect.Update();
					if (this.mZoneEffect.mCurNumParticles > 0)
					{
						this.mDirty = true;
					}
				}
			}
			if (this.mUpdateCount >= Common._M(25))
			{
				for (int i = 0; i < 5; i++)
				{
					if (this.mOverlays[i].mUnlocked && this.mOverlays[i].mAlpha > 0f)
					{
						if (this.mOverlays[i].mAlpha > 0f)
						{
							this.mDirty = true;
						}
						this.mOverlays[i].mAlpha -= Common._M(0.95f);
						if (this.mOverlays[i].mAlpha < 0f)
						{
							this.mOverlays[i].mAlpha = 0f;
						}
						for (int j = 0; j < 3; j++)
						{
							float num7 = Common._M(0.35f) + (float)j * Common._M1(0.15f);
							if (j == 1)
							{
								num7 *= -1f;
							}
							this.mOverlays[i].mCloudPoints[j].mX -= num7;
						}
					}
				}
			}
			if (this.mZoneOver)
			{
				this.mZoneOverPct = Math.Min(1f, this.mZoneOverPct + Common._M(0.05f));
			}
			else
			{
				this.mZoneOverPct = Math.Max(0f, this.mZoneOverPct - Common._M(0.075f));
			}
			if (GameApp.gApp.mHasFocus)
			{
				this.mDirty = true;
			}
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0003F610 File Offset: 0x0003D810
		public void Draw(SexyGraphics g)
		{
			Image imageByID = Res.GetImageByID(ResID.IMAGE_UI_MAP_BKGRND);
			Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_HOME);
			Image imageByID2 = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_HOME_BACKING);
			Font fontByID = Res.GetFontByID(ResID.FONT_SHAGLOUNGE28_BASE);
			g.SetColorizeImages(true);
			g.SetColor(this.mAlpha);
			g.DrawImage(imageByID, (int)((float)Common._S(0) + this.mXOff + this.mUnlockScrollAmt), 0);
			for (int i = 1; i < 7; i++)
			{
				if (i == this.mDisplayedZone)
				{
					g.SetColor(this.mAlpha);
					int id = 1294 + i - 1;
					int num = Common._DS(Res.GetOffsetXByID((ResID)id)) + Common._S(0) + (int)this.mUnlockScrollAmt;
					int num2 = Common._DS(Res.GetOffsetYByID((ResID)id));
					Image imageByID3 = Res.GetImageByID((ResID)id);
					double num3 = 0.0;
					g.SetColor(255, 255, 255, (int)(255.0 * Math.Min(1.0, num3)));
					g.DrawImage(imageByID3, num, num2);
					if (this.mBackBtn != null)
					{
						bool mVisible = this.mBackBtn.mVisible;
					}
					bool flag = false;
					if (GameApp.gApp.mBoard != null && GameApp.gApp.mBoard.mDoingFirstTimeIntro && GameApp.gApp.mBoard.mShowMapScreen && GameApp.gApp.mBoard.mIntroMapScale == 0.0 && !GameApp.gApp.mBoard.mDoIntroFrogJump)
					{
						flag = false;
					}
					else if (GameApp.gApp.mBoard != null && GameApp.gApp.mBoard.mDoingFirstTimeIntro && GameApp.gApp.mBoard.mShowMapScreen)
					{
						flag = true;
					}
					bool flag2 = false;
					if (GameApp.gApp.mBoard != null)
					{
						flag2 = GameApp.gApp.mBoard.mDoingFirstTimeIntro;
					}
					if ((this.mCompletedZone || flag2) && !flag)
					{
						g.SetDrawMode(1);
						g.SetColor(255, 255, 255, (int)this.mDisplayZoneAlpha);
						g.DrawImage(imageByID3, num, num2);
						g.SetDrawMode(0);
					}
				}
				else
				{
					this.DrawDesaturatedZone(g, i, Common._M(1f));
				}
			}
			g.SetColor(this.mAlpha);
			for (int j = 0; j < 5; j++)
			{
				if (this.mOverlays[j].mAlpha > 0f)
				{
					g.SetColorizeImages(true);
					g.SetColor(255, 255, 255, (int)((double)this.mOverlays[j].mAlpha * this.mAlpha));
					for (int k = 0; k < 3; k++)
					{
						Image imageByID4 = Res.GetImageByID(ResID.IMAGE_UI_MAP_FOG1 + k);
						float num4 = (float)imageByID4.mWidth * this.mOverlays[j].mCloudSizes[k].mX * 2f;
						float num5 = (float)imageByID4.mHeight * this.mOverlays[j].mCloudSizes[k].mY * 2f;
						g.DrawImage(imageByID4, (int)(Common._DS(this.mOverlays[j].mCloudPoints[k].mX - 0f) + this.mXOff + this.mUnlockScrollAmt), (int)Common._DS(this.mOverlays[j].mCloudPoints[k].mY), (int)num4, (int)num5);
					}
				}
			}
			if (this.mDisplayingZones)
			{
				g.SetFont(fontByID);
				this.DrawZoneSelectBackground(g);
				for (int l = 0; l < 6; l++)
				{
					Image inZoneImage;
					Rect inZoneRect;
					this.GetZoneImage(l, out inZoneImage, out inZoneRect);
					this.DrawZoneImage(g, l, inZoneImage, inZoneRect);
					this.DrawZoneName(g, l, inZoneRect);
					this.DrawZoneLockedOverlay(g, l, inZoneRect);
					Common.DrawCommonDialogBorder(g, inZoneRect.mX - Common._DS(10), inZoneRect.mY - Common._DS(7), inZoneRect.mWidth + Common._DS(20), inZoneRect.mHeight + Common._DS(14));
				}
			}
			else
			{
				this.DrawMapZoneName(g);
			}
			if (this.mBackBtn != null && this.mBackBtn.mVisible)
			{
				g.DrawImage(imageByID2, -84, 0);
			}
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0003FA5C File Offset: 0x0003DC5C
		public void MouseMove(int x, int y)
		{
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0003FA5E File Offset: 0x0003DC5E
		public void ButtonPress(int theId, int theClickCount)
		{
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0003FA60 File Offset: 0x0003DC60
		public void MouseDown(int x, int y)
		{
			if (this.mDisableInput)
			{
				return;
			}
			if (GameApp.gApp.mBambooTransition != null && GameApp.gApp.mBambooTransition.IsInProgress())
			{
				return;
			}
			this.mLastMouseX = x;
			this.mLastMouseY = y;
			if (this.mFromIntro)
			{
				if (this.mZoneOver)
				{
					GameApp.gApp.PlaySample(Res.GetSoundByID(ResID.SOUND_MAPZOOMIN));
					GameApp.gApp.mUserProfile.mNeedsFirstTimeIntro = false;
					GameApp.gApp.PlaySong(12);
				}
				return;
			}
			if (this.mSlideDir != 0)
			{
				return;
			}
			if (this.mRemove)
			{
				return;
			}
			if (this.mCompletedZone)
			{
				GameApp.gApp.PlaySample(Res.GetSoundByID(ResID.SOUND_MAPZOOMIN));
				GameApp.gApp.SetCursor((ECURSOR)0);
				this.mClosing = true;
			}
			this.OnZoneCardSelected();
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0003FB29 File Offset: 0x0003DD29
		public void MouseLeave()
		{
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0003FB2B File Offset: 0x0003DD2B
		public void ButtonMouseMove(int theId, int theX, int theY)
		{
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0003FB2D File Offset: 0x0003DD2D
		public void ButtonDownTick(int theId)
		{
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0003FB2F File Offset: 0x0003DD2F
		public void ButtonMouseLeave(int theId)
		{
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0003FB34 File Offset: 0x0003DD34
		public void MouseUp(int x, int y)
		{
			if (this.mDisableInput)
			{
				return;
			}
			if (GameApp.gApp.mBambooTransition != null && GameApp.gApp.mBambooTransition.IsInProgress())
			{
				return;
			}
			if (this.mSelectedZone == -1)
			{
				return;
			}
			if (this.mCards[this.mSelectedZone - 1].Contains(x, y) && (this.mSelectedZone - 1 == 0 || this.mOverlays[this.mSelectedZone - 2].mUnlocked) && Enumerable.Count<KeyValuePair<int, Dialog>>(GameApp.gApp.mDialogMap) == 0)
			{
				GameApp.gApp.DoYesNoDialog("", TextManager.getInstance().getString(452), true, TextManager.getInstance().getString(446), TextManager.getInstance().getString(447), false, -1, 1, 0);
				GameApp.gApp.mYesNoDialogDelegate = new GameApp.YesNoDialogDelegate(this.ProcessYesNo);
				return;
			}
			this.mSelectedZone = -1;
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0003FC22 File Offset: 0x0003DE22
		public void ProcessYesNo(int theId)
		{
			GameApp gameApp = (GameApp)GlobalMembers.gSexyApp;
			if (theId == 1000)
			{
				this.mRemove = true;
				this.CleanButtons();
				return;
			}
			this.mSelectedZone = -1;
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0003FC4C File Offset: 0x0003DE4C
		public void DoSlide(bool slide_in)
		{
			if (slide_in)
			{
				this.mAlpha.SetCurve(Common._MP("b-0,1,0.02,1,####        n~### 3~###"));
				return;
			}
			this.mFadingOut = true;
			this.mAlpha.SetCurve(Common._MP("b-0,1,0.02,1,~###         ~####"));
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0003FC83 File Offset: 0x0003DE83
		public Point GetZoneCenter(int theZoneNum)
		{
			return this.mZoneCenters[theZoneNum - 1];
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0003FC90 File Offset: 0x0003DE90
		public virtual void ButtonDepress(int id)
		{
			if (GameApp.gApp.mBambooTransition != null && GameApp.gApp.mBambooTransition.IsInProgress())
			{
				return;
			}
			if (this.mSlideDir != 0)
			{
				return;
			}
			if (id == this.mContinueBtn.mId)
			{
				if (GameApp.USE_TRIAL_VERSION && this.mIsTrialEnd)
				{
					if (GameApp.gApp.mBoard != null)
					{
						GameApp.gApp.mBoard.Pause(true, true);
					}
					string @string = TextManager.getInstance().getString(832);
					int width_pad = Common._DS(Common._M(20));
					GameApp.gApp.DoYesNoDialog(TextManager.getInstance().getString(448), @string, true, TextManager.getInstance().getString(446), TextManager.getInstance().getString(447), false, Common._S(Common._M(50)), 1, width_pad);
					GameApp.gApp.mYesNoDialogDelegate = new GameApp.YesNoDialogDelegate(this.ProcessTrialYesNo);
					this.mIsTryAndBuyDialogShowing = true;
					return;
				}
				if (GameApp.gApp.mResourceManager.IsGroupLoaded("MenuRelated"))
				{
					GameApp.gApp.mResourceManager.DeleteResources("MenuRelated");
				}
				this.mContinueBtn.mLevel = "";
				this.mContinueBtn.mScore = "";
				this.mContinueBtn.mLives = "";
				this.mRemove = true;
				if (this.mContinueGoesToCheckpoint)
				{
					this.mContinueFromCheckpoint = true;
					return;
				}
			}
			else
			{
				if (id == this.mBackBtn.mId)
				{
					if (GameApp.gApp.GetBoard() != null)
					{
						this.CleanButtons();
						GameApp.gApp.mClickedHardMode = false;
						GameApp.gApp.EndCurrentGame();
						GameApp.gApp.ShowMainMenu();
					}
					else
					{
						this.CleanButtons();
						GameApp.gApp.mClickedHardMode = false;
						GameApp.gApp.mBambooTransition.mTransitionDelegate = new BambooTransition.BambooTransitionDelegate(GameApp.gApp.HideAdventureModeMapScreen);
					}
					GameApp.gApp.ToggleBambooTransition();
					return;
				}
				if (id == this.mSelectZoneBackBtn.mId)
				{
					if (!this.mFromCheckpoint)
					{
						this.mZoneBtn.mDisabled = (this.mContinueBtn.mDisabled = false);
						this.mZoneBtn.mVisible = (this.mContinueBtn.mVisible = true);
						this.mDisplayingZones = false;
						return;
					}
					this.CleanButtons();
					this.mRemove = true;
					return;
				}
				else if (id == this.mZoneBtn.mId)
				{
					this.mDisplayingZones = true;
					this.mZoneBtn.mDisabled = (this.mContinueBtn.mDisabled = true);
					this.mZoneBtn.mVisible = (this.mContinueBtn.mVisible = false);
				}
			}
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0003FF28 File Offset: 0x0003E128
		public void ButtonPress(int id)
		{
			if (GameApp.gApp.mBambooTransition != null && GameApp.gApp.mBambooTransition.IsInProgress())
			{
				return;
			}
			int soundByID = Res.GetSoundByID(ResID.SOUND_BUTTON1);
			int soundByID2 = Res.GetSoundByID(ResID.SOUND_BUTTON2);
			int soundByID3 = Res.GetSoundByID(ResID.SOUND_BUTTON3);
			if (this.mContinueBtn.mId == id)
			{
				GameApp.gApp.PlaySample(soundByID3);
				return;
			}
			if (this.mBackBtn.mId == id || this.mZoneBtn.mId == id)
			{
				GameApp.gApp.PlaySample(soundByID2);
				return;
			}
			GameApp.gApp.PlaySample(soundByID);
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0003FFC4 File Offset: 0x0003E1C4
		public virtual void ButtonMouseEnter(int id)
		{
			this.mLastMouseX = (this.mLastMouseY = -1);
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0003FFE4 File Offset: 0x0003E1E4
		public void ProcessHardwareBackButton()
		{
			if (GameApp.gApp.mBambooTransition != null && GameApp.gApp.mBambooTransition.IsInProgress())
			{
				GameApp.gApp.OnHardwareBackButtonPressProcessed();
				return;
			}
			if (this.mIsTryAndBuyDialogShowing)
			{
				Dialog dialog = GameApp.gApp.GetDialog(1);
				if (dialog != null)
				{
					dialog.ButtonDepress(1001);
					GameApp.gApp.OnHardwareBackButtonPressProcessed();
					return;
				}
			}
			if (this.mDisplayingZones)
			{
				if (this.mSelectedZone != -1)
				{
					Dialog dialog2 = GameApp.gApp.GetDialog(1);
					if (dialog2 != null)
					{
						dialog2.ButtonDepress(1001);
						GameApp.gApp.OnHardwareBackButtonPressProcessed();
						return;
					}
				}
				this.mZoneBtn.mDisabled = (this.mContinueBtn.mDisabled = false);
				this.mZoneBtn.mVisible = (this.mContinueBtn.mVisible = true);
				this.mDisplayingZones = false;
				GameApp.gApp.OnHardwareBackButtonPressProcessed();
				return;
			}
			if (GameApp.gApp.GetBoard() != null)
			{
				this.CleanButtons();
				GameApp.gApp.mClickedHardMode = false;
				GameApp.gApp.mBambooTransition.mTransitionDelegate = new BambooTransition.BambooTransitionDelegate(GameApp.gApp.DoDeferredEndGame);
			}
			else
			{
				this.CleanButtons();
				GameApp.gApp.mClickedHardMode = false;
				GameApp.gApp.mBambooTransition.mTransitionDelegate = new BambooTransition.BambooTransitionDelegate(GameApp.gApp.HideAdventureModeMapScreen);
			}
			GameApp.gApp.ToggleBambooTransition();
			GameApp.gApp.OnHardwareBackButtonPressProcessed();
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00040144 File Offset: 0x0003E344
		private void DrawZoneSelectBackground(SexyGraphics g)
		{
			Image imageByID = Res.GetImageByID(ResID.IMAGE_UI_MAP_OPENBOOK_PAGES);
			Image imageByID2 = Res.GetImageByID(ResID.IMAGE_BOOKTEXT);
			int num = (GameApp.gApp.GetScreenRect().mWidth - imageByID.mWidth) / 2 + Common._DS(Common._M(0));
			int num2 = (GameApp.gApp.mHeight - imageByID.mHeight) / 2;
			g.DrawImage(imageByID, num, num2);
			int num3 = 4;
			g.DrawImage(imageByID2, num, num2, imageByID2.mWidth - num3, imageByID2.mHeight);
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x000401C4 File Offset: 0x0003E3C4
		private void GetZoneImage(int inZoneID, out Image outZoneImage, out Rect outZoneRect)
		{
			Image levelThumbnail = GameApp.gApp.GetLevelThumbnail(inZoneID * 10);
			Rect rect = this.mCards[inZoneID];
			rect.mWidth = (int)(Common._S(2f) * Common._S(0.55f) * (float)levelThumbnail.mWidth);
			rect.mHeight = (int)(Common._S(2f) * Common._S(0.55f) * (float)levelThumbnail.mHeight);
			outZoneImage = levelThumbnail;
			outZoneRect = rect;
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00040248 File Offset: 0x0003E448
		private void DrawZoneImage(SexyGraphics g, int inZoneID, Image inZoneImage, Rect inZoneRect)
		{
			g.PushState();
			g.SetColorizeImages(false);
			g.DrawImage(inZoneImage, inZoneRect.mX, inZoneRect.mY, inZoneRect.mWidth, inZoneRect.mHeight);
			if (this.mSelectedZone - 1 == inZoneID)
			{
				g.PushState();
				g.SetColorizeImages(true);
				g.SetColor(255, 255, 255, (int)((double)Common._M(100) * this.mAlpha));
				g.SetDrawMode(1);
				g.DrawImage(inZoneImage, inZoneRect.mX, inZoneRect.mY, inZoneRect.mWidth, inZoneRect.mHeight);
				g.PopState();
			}
			g.PopState();
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00040300 File Offset: 0x0003E500
		private void DrawZoneName(SexyGraphics g, int inZoneID, Rect inZoneRect)
		{
			string text = string.Format("{0} - {1}", inZoneID + 1, MapScreen.gZoneNames[inZoneID]);
			int num = g.GetFont().StringWidth(text);
			int num2 = inZoneRect.mX + (inZoneRect.mWidth - num) / 2;
			int num3 = inZoneRect.mY + inZoneRect.mHeight + Common._DS(50);
			g.SetColor(Color.Black);
			g.DrawString(text, num2, num3);
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00040374 File Offset: 0x0003E574
		private void DrawZoneLockedOverlay(SexyGraphics g, int inZoneID, Rect inZoneRect)
		{
			if (inZoneID <= 0 || this.mOverlays[inZoneID - 1].mUnlocked)
			{
				return;
			}
			g.SetColor(0, 0, 0, 191);
			g.FillRect(inZoneRect);
			string @string = TextManager.getInstance().getString(664);
			g.SetColor(Common._M(255), Common._M1(0), Common._M2(0), (int)((double)Common._M3(255) * this.mAlpha));
			int num = g.GetFont().StringWidth(@string);
			int mHeight = g.GetFont().mHeight;
			int num2 = inZoneRect.mX + (inZoneRect.mWidth - num) / 2 + Common._DS(15);
			int num3 = inZoneRect.mY + Common._DS(Common._M(119));
			float num4 = 0f;
			if ((int)Localization.GetCurrentLanguage() == 6)
			{
				num4 = 0.8f;
			}
			Graphics3D graphics3D = g.Get3D();
			if (graphics3D != null)
			{
				num2 += Common._DS(Common._M(20));
				SexyTransform2D sexyTransform2D;
				sexyTransform2D = new SexyTransform2D(false);
				sexyTransform2D.Translate((float)(-(float)num2 - num / 2 + GlobalMembers.gSexyApp.mScreenBounds.mX), (float)(-(float)num3 - mHeight / 2));
				sexyTransform2D.RotateDeg((float)Common._M(45));
				if (num4 != 0f)
				{
					sexyTransform2D.Scale(num4, num4);
				}
				sexyTransform2D.Translate((float)(num2 + num / 2 - GlobalMembers.gSexyApp.mScreenBounds.mX), (float)(num3 + mHeight / 2));
				graphics3D.PushTransform(sexyTransform2D);
				g.DrawString(@string, num2, num3);
				graphics3D.PopTransform();
				return;
			}
			g.DrawString(@string, num2, num3);
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00040510 File Offset: 0x0003E710
		private void DrawMapZoneName(SexyGraphics g)
		{
			g.SetColor(255, 255, 255, (int)(255.0 * this.mExtrasAlpha * this.mAlpha));
			if (this.mNewZoneTextSize > 0f)
			{
				this.mGlobalTranform.Reset();
				if (g.Is3D())
				{
					this.mGlobalTranform.Scale(this.mNewZoneTextSize, this.mNewZoneTextSize);
				}
				int num = (int)((this.mXOff + (float)GameApp.gApp.GetScreenRect().mWidth + (float)GameApp.gApp.GetScreenRect().mX) / 2f);
				if (this.mFromIntro)
				{
					g.DrawImageTransform(this.mNewZoneTextImg, this.mGlobalTranform, (float)num, (float)Common._DS(Common._M1(1000)));
				}
				else if (this.mCompletedZone)
				{
					g.DrawImageTransform(this.mNewZoneTextImg, this.mGlobalTranform, (float)num, (float)Common._DS(Common._M1(960)));
				}
				else
				{
					g.DrawImageTransform(this.mNewZoneTextImg, this.mGlobalTranform, (float)(Common._DS(1000) + GameApp.gApp.GetScreenRect().mX - GameApp.gApp.mWideScreenXOffset), (float)Common._DS(Common._M1(150)));
				}
			}
			if (this.mZoneEffect != null && this.mUpdateCount > Common._M(50) && !this.mFromIntro)
			{
				this.mZoneEffect.Draw(g);
			}
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00040690 File Offset: 0x0003E890
		private void OnZoneCardSelected()
		{
			if (!this.mDisplayingZones)
			{
				return;
			}
			for (int i = 0; i < 6; i++)
			{
				if (this.MouseOverCard(i))
				{
					this.mSelectedZone = i + 1;
					GameApp.gApp.PlaySample(Res.GetSoundByID(ResID.SOUND_BUTTON1));
					return;
				}
			}
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x000406DC File Offset: 0x0003E8DC
		public void ProcessTrialYesNo(int theId)
		{
			if (theId == 1000)
			{
				GameApp.gApp.ToMarketPlace();
				this.mIsTryAndBuyDialogShowing = false;
				return;
			}
			if (theId == 1001)
			{
				if (GameApp.gApp.GetBoard() != null)
				{
					this.CleanButtons();
					GameApp.gApp.mClickedHardMode = false;
					GameApp.gApp.mBambooTransition.mTransitionDelegate = new BambooTransition.BambooTransitionDelegate(GameApp.gApp.DoDeferredEndGame);
				}
				else
				{
					this.CleanButtons();
					GameApp.gApp.mClickedHardMode = false;
					GameApp.gApp.mBambooTransition.mTransitionDelegate = new BambooTransition.BambooTransitionDelegate(GameApp.gApp.HideAdventureModeMapScreen);
				}
				GameApp.gApp.ToggleBambooTransition();
				this.mIsTryAndBuyDialogShowing = false;
			}
		}

		// Token: 0x04000BAF RID: 2991
		internal const float POSTCARD_PCT = 0.55f;

		// Token: 0x04000BB0 RID: 2992
		internal static readonly string[] gZoneNames = new string[]
		{
			"Jungle of Mystery",
			"Quiet Village",
			"Lost City",
			"Mosquito Coast",
			"Underwater Grotto",
			"Volcano Temple"
		};

		// Token: 0x04000BB1 RID: 2993
		protected Transform mGlobalTranform = new Transform();

		// Token: 0x04000BB2 RID: 2994
		private Point[] mAreaCoords = new Point[]
		{
			new Point(0, 0),
			new Point(Common._DS(Common._M(1150)), Common._DS(Common._M1(120))),
			new Point(Common._DS(Common._M2(1080)), Common._DS(Common._M3(396))),
			new Point(Common._DS(Common._M4(660)), Common._DS(Common._M5(341))),
			new Point(Common._DS(Common._M(600)), Common._DS(Common._M1(305))),
			new Point(Common._DS(Common._M2(720)), Common._DS(Common._M3(40)))
		};

		// Token: 0x04000BB3 RID: 2995
		private Point[] mZoneCenters = new Point[]
		{
			new Point(Common._M(1458), Common._M1(653)),
			new Point(Common._M2(1120), Common._M3(305)),
			new Point(Common._M4(1077), Common._M5(585)),
			new Point(Common._M6(692), Common._M7(532)),
			new Point(Common._M(600), Common._M1(830)),
			new Point(Common._M2(728), Common._M3(290))
		};

		// Token: 0x04000BB4 RID: 2996
		public PIEffect mZoneEffect;

		// Token: 0x04000BB5 RID: 2997
		public MemoryImage mNewZoneTextImg;

		// Token: 0x04000BB6 RID: 2998
		public float mNewZoneTextSize;

		// Token: 0x04000BB7 RID: 2999
		public int mNewZoneTextBounceCount;

		// Token: 0x04000BB8 RID: 3000
		public MapButton mContinueBtn;

		// Token: 0x04000BB9 RID: 3001
		public MapGenericButton mZoneBtn;

		// Token: 0x04000BBA RID: 3002
		public ButtonWidget mBackBtn;

		// Token: 0x04000BBB RID: 3003
		public MapGenericButton mSelectZoneBackBtn;

		// Token: 0x04000BBC RID: 3004
		public MapOverlay[] mOverlays = Common.CreateObjectArray<MapOverlay>(5);

		// Token: 0x04000BBD RID: 3005
		public bool mDisplayingZones;

		// Token: 0x04000BBE RID: 3006
		public bool mFromCheckpoint;

		// Token: 0x04000BBF RID: 3007
		public bool mFromIntro;

		// Token: 0x04000BC0 RID: 3008
		public bool mContinueGoesToCheckpoint;

		// Token: 0x04000BC1 RID: 3009
		public bool mBeatGame;

		// Token: 0x04000BC2 RID: 3010
		public int mHighestDot;

		// Token: 0x04000BC3 RID: 3011
		public int mUpdateCount;

		// Token: 0x04000BC4 RID: 3012
		public int mSlideDir;

		// Token: 0x04000BC5 RID: 3013
		public float mXOff;

		// Token: 0x04000BC6 RID: 3014
		public float mUnlockScrollAmt;

		// Token: 0x04000BC7 RID: 3015
		public int mLastMouseX;

		// Token: 0x04000BC8 RID: 3016
		public int mLastMouseY;

		// Token: 0x04000BC9 RID: 3017
		public bool mHasPlayedZoneUnlockedSound;

		// Token: 0x04000BCA RID: 3018
		public CurvedVal mUnlockNameAlpha = new CurvedVal();

		// Token: 0x04000BCB RID: 3019
		public CurvedVal mUnlockNameHilite = new CurvedVal();

		// Token: 0x04000BCC RID: 3020
		public CurvedVal mUnlockOutlineAlpha = new CurvedVal();

		// Token: 0x04000BCD RID: 3021
		public CurvedVal mUnlockIconAlpha = new CurvedVal();

		// Token: 0x04000BCE RID: 3022
		public CurvedVal mClickToEnterAlpha = new CurvedVal();

		// Token: 0x04000BCF RID: 3023
		public CurvedVal mExtrasAlpha = new CurvedVal();

		// Token: 0x04000BD0 RID: 3024
		public CurvedVal mDotSubtract = new CurvedVal();

		// Token: 0x04000BD1 RID: 3025
		public bool mDisableInput;

		// Token: 0x04000BD2 RID: 3026
		public bool mFadingOut;

		// Token: 0x04000BD3 RID: 3027
		public float mDisplayZoneAlpha;

		// Token: 0x04000BD4 RID: 3028
		public bool mIncDisplayZoneAlpha;

		// Token: 0x04000BD5 RID: 3029
		public WidgetContainer mParent;

		// Token: 0x04000BD6 RID: 3030
		public CurvedVal mAlpha = new CurvedVal();

		// Token: 0x04000BD7 RID: 3031
		public Rect[] mCards = Common.CreateObjectArray<Rect>(6);

		// Token: 0x04000BD8 RID: 3032
		public bool mCompletedZone;

		// Token: 0x04000BD9 RID: 3033
		public bool mDirty;

		// Token: 0x04000BDA RID: 3034
		public bool mRemove;

		// Token: 0x04000BDB RID: 3035
		public bool mContinueFromCheckpoint;

		// Token: 0x04000BDC RID: 3036
		public int mSelectedZone;

		// Token: 0x04000BDD RID: 3037
		public int mDisplayedZone;

		// Token: 0x04000BDE RID: 3038
		public int mMapOffsetX = 53;

		// Token: 0x04000BDF RID: 3039
		public bool mIntroClosing;

		// Token: 0x04000BE0 RID: 3040
		public bool mClosing;

		// Token: 0x04000BE1 RID: 3041
		public bool mZoneOver;

		// Token: 0x04000BE2 RID: 3042
		public float mZoneOverPct;

		// Token: 0x04000BE3 RID: 3043
		public PIEffect[] zone_effects = new PIEffect[6];

		// Token: 0x04000BE4 RID: 3044
		public bool mIsTryAndBuyDialogShowing;

		// Token: 0x04000BE5 RID: 3045
		public bool mIsTrialEnd;
	}
}
