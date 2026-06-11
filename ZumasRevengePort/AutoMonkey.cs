using System;
using System.Collections.Generic;
using System.Linq;
using SexyFramework;
using SexyFramework.Drivers;

namespace ZumasRevenge
{
	// Token: 0x02000017 RID: 23
	public class AutoMonkey
	{
		// Token: 0x060003AB RID: 939 RVA: 0x00030258 File Offset: 0x0002E458
		public AutoMonkey(GameApp app)
		{
			this.mApp = app;
			this.mStateList.Add(MonkeyState.IntroScreen);
			this.mAllModesMode = MonkeyMode.PlayThroughGame;
			this.mAutoMonkeyMode = MonkeyMode.PlayThroughGame;
			this.mLastButtonPress = 0;
			this.mStateCount = 0;
			this.mRandomButtonPress = 0;
			this.mMoveDir = 2;
			this.mAllowedButtons.Add((GamepadButton)16);
			this.mAllowedButtons.Add((GamepadButton)17);
			this.mAllowedButtons.Add((GamepadButton)18);
			this.mAllowedButtons.Add((GamepadButton)19);
			this.mAllowedButtons.Add((GamepadButton)0);
			this.mAllowedButtons.Add((GamepadButton)1);
			this.mAllowedButtons.Add((GamepadButton)2);
			this.mAllowedButtons.Add((GamepadButton)3);
			this.mAllowedButtons.Add((GamepadButton)4);
			this.mAllowedButtons.Add((GamepadButton)5);
			this.mAllowedButtons.Add((GamepadButton)6);
			this.mAllowedButtons.Add((GamepadButton)7);
			this.mAllowedButtons.Add((GamepadButton)8);
			this.mAllowedButtons.Add((GamepadButton)9);
			this.mAllowedButtons.Add((GamepadButton)10);
			this.mAllowedButtons.Add((GamepadButton)11);
			this.mAllowedButtons.Add((GamepadButton)12);
			this.mAllowedButtons.Add((GamepadButton)13);
			this.mAllowedButtons.Add((GamepadButton)14);
			this.mAllowedButtons.Add((GamepadButton)15);
			this.mAllowedButtons.Add((GamepadButton)16);
			this.mAllowedButtons.Add((GamepadButton)17);
			this.mAllowedButtons.Add((GamepadButton)19);
			this.mAllowedButtons.Add((GamepadButton)18);
			this.mDirectionButtons.Add((GamepadButton)0);
			this.mDirectionButtons.Add((GamepadButton)1);
			this.mDirectionButtons.Add((GamepadButton)3);
			this.mDirectionButtons.Add((GamepadButton)2);
			this.mAutoMonkeyDelay = 0.3f;
			this.mEnableAutoMonkey = false;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0003043C File Offset: 0x0002E63C
		~AutoMonkey()
		{
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00030464 File Offset: 0x0002E664
		public void Update()
		{
			this.mLastButtonPress++;
			this.mStateCount++;
			this.mRandomButtonPress++;
			switch (Enumerable.Last<MonkeyState>(this.mStateList))
			{
			case MonkeyState.IntroScreen:
				this.UpdateIntroScreen();
				return;
			case MonkeyState.MainMenu:
				this.UpdateMainMenu();
				return;
			case MonkeyState.ModalOkDialog:
				this.UpdateModalDialog();
				return;
			case MonkeyState.ModalYesNoDialog:
				this.UpdateYesNoDialog();
				return;
			case MonkeyState.PauseDialog:
				break;
			case MonkeyState.Playing:
				this.UpdatePlaying();
				break;
			default:
				return;
			}
		}

		// Token: 0x060003AE RID: 942 RVA: 0x000304E8 File Offset: 0x0002E6E8
		public void SetState(MonkeyState state)
		{
			this.mStateList.Add(state);
			this.mStateCount = 0;
			this.mLastButtonPress = 0;
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00030504 File Offset: 0x0002E704
		public void RemoveLastInstanceOfState(MonkeyState state)
		{
			bool flag = false;
			int num = this.mStateList.Count - 1;
			while (num >= 0 && !flag)
			{
				if (this.mStateList[num] == state)
				{
					this.mStateList.RemoveAt(num);
					flag = true;
				}
				num--;
			}
			if (!flag)
			{
				Console.WriteLine("Unable to find state '{0}' to remove from AutoMonkey!!", this.GetStateString(state));
			}
			this.mStateCount = 0;
			this.mLastButtonPress = 0;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0003056D File Offset: 0x0002E76D
		public MonkeyMode GetMode()
		{
			return this.mAutoMonkeyMode;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00030575 File Offset: 0x0002E775
		public bool IsEnabled()
		{
			return this.GetMode() != MonkeyMode.Disabled;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00030583 File Offset: 0x0002E783
		protected void UpdateIntroScreen()
		{
			if (this.mAutoMonkeyDelay <= (float)this.mLastButtonPress / 100f)
			{
				this.PressButtonDown((GamepadButton)6, true);
			}
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x000305A2 File Offset: 0x0002E7A2
		protected void UpdateMainMenu()
		{
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x000305A4 File Offset: 0x0002E7A4
		protected void UpdateModalDialog()
		{
			if (3f <= (float)this.mStateCount / 100f)
			{
				this.PressButton((GamepadButton)6, true);
			}
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x000305C4 File Offset: 0x0002E7C4
		protected void UpdateYesNoDialog()
		{
			if (3f <= (float)this.mStateCount / 100f)
			{
				if (Common.Rand() % 2 == 0)
				{
					this.PressButton((GamepadButton)3, true);
				}
				else
				{
					this.PressButton((GamepadButton)2, true);
				}
				this.PressButton((GamepadButton)6, true);
			}
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0003060C File Offset: 0x0002E80C
		protected void UpdatePlaying()
		{
			if (this.mApp.mBoard == null)
			{
				return;
			}
			if (this.mApp.mMapScreen != null)
			{
				this.PressButton((GamepadButton)6, true);
			}
			bool flag = !this.mApp.mBoard.mDoingFirstTimeIntro && !this.mApp.mBoard.mDoingIronFrogWin && Enumerable.Count<ZumaTip>(this.mApp.mBoard.mZumaTips) == 0 && this.mApp.mBoard.mLevelTransition == null && !this.mApp.mBoard.mShowMapScreen;
			if (this.mAutoMonkeyDelay <= (float)this.mLastButtonPress / 100f)
			{
				if (this.mApp.mBoard.mDoingFirstTimeIntro || this.mApp.mBoard.mDoingIronFrogWin || this.mApp.mBoard.mLevelTransition != null || this.mApp.mBoard.mShowMapScreen)
				{
					this.mApp.mBoard.MouseDown(GameApp.gApp.GetScreenRect().mWidth / 2, GameApp.gApp.GetScreenRect().mHeight / 2, 1);
					this.mApp.mBoard.MouseUp(GameApp.gApp.GetScreenRect().mWidth / 2, GameApp.gApp.GetScreenRect().mHeight / 2, 1);
					this.PressButton((GamepadButton)6, true);
				}
				else if (this.mApp.mBoard.mZumaTips.Count != 0)
				{
					if (this.mApp.mBoard.mZumaTips[0].mId == ZumaProfile.FIRST_SHOT_HINT)
					{
						this.mApp.mBoard.mFrog.SetDestAngle(4.64f);
						this.mApp.mBoard.MouseUp((int)Common._S(this.mApp.mBoard.mFrog.mCurX - 150f), (int)Common._S(this.mApp.mBoard.mFrog.mCurY), 1);
					}
					else if (this.mApp.mBoard.mZumaTips[0].mId == ZumaProfile.ZUMA_BAR_HINT || this.mApp.mBoard.mZumaTips[0].mId == ZumaProfile.SKULL_PIT_HINT)
					{
						this.mApp.mBoard.MouseDown(GameApp.gApp.GetScreenRect().mWidth / 2, GameApp.gApp.GetScreenRect().mHeight / 2, 1);
						this.mApp.mBoard.MouseUp(GameApp.gApp.GetScreenRect().mWidth / 2, GameApp.gApp.GetScreenRect().mHeight / 2, 1);
						this.PressButton((GamepadButton)6, true);
					}
					else if (this.mApp.mBoard.mZumaTips[0].mId == ZumaProfile.LILLY_PAD_HINT)
					{
						if (this.mApp.mBoard.mLevel != null)
						{
							int gunPointFromPos = this.mApp.mBoard.mLevel.GetGunPointFromPos((int)this.mApp.mBoard.mFrog.mCurX, (int)this.mApp.mBoard.mFrog.mCurY);
							int num;
							do
							{
								num = Common.Rand() % this.mApp.mBoard.mLevel.mNumFrogPoints;
							}
							while (num == gunPointFromPos);
							if (num >= 0 && num != this.mApp.mBoard.mLevel.mCurFrogPoint)
							{
								this.mApp.mBoard.mLevel.mCurFrogPoint = num;
								this.mApp.mBoard.mFrog.SetDestPos(this.mApp.mBoard.mLevel.mFrogX[num], this.mApp.mBoard.mLevel.mFrogY[num], this.mApp.mBoard.mLevel.mMoveSpeed, true);
								this.mApp.mBoard.mLevel.ChangedPad(num);
								this.mApp.mUserProfile.MarkHintAsSeen(ZumaProfile.LILLY_PAD_HINT);
								this.mApp.mBoard.mZumaTips.RemoveAt(0);
								if (Enumerable.Count<ZumaTip>(this.mApp.mBoard.mZumaTips) == 0)
								{
									this.mApp.mBoard.mPreventBallAdvancement = false;
								}
							}
						}
					}
					else if (this.mApp.mBoard.mZumaTips[0].mId == ZumaProfile.FRUIT_HINT)
					{
						this.mApp.mBoard.mFrog.SetDestAngle(4.2f);
						this.PressButton((GamepadButton)6, true);
						this.mApp.mBoard.MouseUp((int)Common._S(this.mApp.mBoard.mFrog.mCurX - 150f), (int)Common._S(this.mApp.mBoard.mFrog.mCurY - 100f), 1);
					}
					else if (this.mApp.mBoard.mZumaTips[0].mId == ZumaProfile.SWAP_BALL_HINT)
					{
						this.mApp.mBoard.MouseDown((int)Common._S(this.mApp.mBoard.mFrog.mCurX), (int)Common._S(this.mApp.mBoard.mFrog.mCurY), 1);
						this.mApp.mBoard.MouseUp((int)Common._S(this.mApp.mBoard.mFrog.mCurX), (int)Common._S(this.mApp.mBoard.mFrog.mCurY), 1);
					}
				}
				else if (this.mApp.mCredits != null)
				{
					if (this.mApp.mCredits.mInitialDelay >= Common._M(300))
					{
						this.mApp.ReturnFromCredits();
					}
				}
				else if (this.mApp.mBoard.mLevel != null && this.mApp.mBoard.mLevel.mFinalLevel && this.mApp.mBoard.mAdventureWinScreen && this.mApp.mBoard.mAdventureWinAlpha > 0f)
				{
					if (this.mApp.mBoard.mAdvWinBtn != null)
					{
						this.mApp.mBoard.ButtonDepress(this.mApp.mBoard.mAdvWinBtn.mId);
					}
				}
				else
				{
					bool flag2 = true;
					for (int i = 0; i < this.mApp.mBoard.mLevel.mNumCurves; i++)
					{
						if (Enumerable.Count<Bullet>(this.mApp.mBoard.mLevel.mCurveMgr[i].mBulletList) != 0)
						{
							flag2 = false;
							break;
						}
					}
					flag2 = (flag2 && Enumerable.Count<Bullet>(this.mApp.mBoard.mBulletList) == 0);
					if (flag2)
					{
						this.mApp.mBoard.mFrog.UpdateAutoMonkeyShotCorrection();
						if (this.mApp.mBoard.mFrog.mShotCorrectionTarget.x != 0f && this.mApp.mBoard.mFrog.mShotCorrectionTarget.y != 0f)
						{
							this.mApp.mBoard.mFrog.SetDestAngle(this.mApp.mBoard.mFrog.mShotCorrectionRad + 1.570795f);
							this.mApp.mBoard.MouseUp((int)(Common._S(this.mApp.mBoard.mFrog.mCurX) + this.mApp.mBoard.mFrog.mShotCorrectionTarget.x), (int)(Common._S(this.mApp.mBoard.mFrog.mCurY) + this.mApp.mBoard.mFrog.mShotCorrectionTarget.y), 1);
						}
						else if (this.mApp.mBoard.mLevel.mNumFrogPoints > 1)
						{
							this.PressButton((GamepadButton)9, true);
						}
						else
						{
							this.PressButton((GamepadButton)7, true);
							this.mApp.mBoard.SwapFrogBalls();
						}
					}
				}
			}
			if (flag && this.mApp.mBoard != null && this.mApp.mBoard.mLevel.mMoveType == 1 && this.mApp.mBoard.mLevel.mBoss != null)
			{
				int num2 = this.mApp.mBoard.mLevel.mFrogX[0];
				int num3 = num2 + this.mApp.mBoard.mLevel.mBarWidth;
				int curX = this.mApp.mBoard.mFrog.GetCurX();
				if (curX <= num2)
				{
					this.mMoveDir = 2;
					this.mApp.mBoard.mFrog.SetDestPos(num2 + this.mMoveDir, this.mApp.mBoard.mFrog.GetCurY(), this.mApp.mBoard.mLevel.mMoveSpeed, true);
				}
				else if (curX >= num3)
				{
					this.mMoveDir = -2;
					this.mApp.mBoard.mFrog.SetDestPos(num3 + this.mMoveDir, this.mApp.mBoard.mFrog.GetCurY(), this.mApp.mBoard.mLevel.mMoveSpeed, true);
				}
				this.mApp.mBoard.mFrog.SetDestPos(curX + this.mMoveDir, this.mApp.mBoard.mFrog.GetCurY(), this.mApp.mBoard.mLevel.mMoveSpeed, true);
			}
			if (flag && this.mApp.mBoard != null && this.mApp.mBoard.mCheckpointEffect != null)
			{
				this.mApp.mBoard.mCheckpointEffect.ButtonDepress(0);
			}
			if (!flag && this.mApp.mBoard != null && this.mApp.mBoard.mStatsContinueBtn != null)
			{
				this.mApp.mBoard.ButtonDepress(2);
			}
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00031043 File Offset: 0x0002F243
		protected void PressButtonDown(GamepadButton button, bool bResetTimer)
		{
			this.mApp.GamepadButtonDown(button, 0, 0U);
			if (bResetTimer)
			{
				this.mLastButtonPress = 0;
			}
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0003105D File Offset: 0x0002F25D
		protected void PressButtonUp(GamepadButton button, bool bResetTimer)
		{
			this.mApp.GamepadButtonUp(button, 0, 0U);
			if (bResetTimer)
			{
				this.mLastButtonPress = 0;
			}
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00031077 File Offset: 0x0002F277
		protected void PressButton(GamepadButton button, bool bResetTimer)
		{
			this.PressButtonDown(button, bResetTimer);
			this.PressButtonUp(button, bResetTimer);
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0003108C File Offset: 0x0002F28C
		public string GetStateString(MonkeyState state)
		{
			switch (state)
			{
			case MonkeyState.IntroScreen:
				return "IntroScreen";
			case MonkeyState.MainMenu:
				return "MainMenu";
			case MonkeyState.ModalOkDialog:
				return "ModalOkDialog";
			case MonkeyState.ModalYesNoDialog:
				return "ModalYesNoDialog";
			case MonkeyState.PauseDialog:
				return "PauseDialog";
			case MonkeyState.Playing:
				return "Playing";
			case MonkeyState.None:
				return "None";
			default:
				return "";
			}
		}

		// Token: 0x060003BB RID: 955 RVA: 0x000310F0 File Offset: 0x0002F2F0
		public string GetButtonString(GamepadButton button)
		{
			switch (button)
			{
			case (GamepadButton)0:
				return "GAMEPAD_BUTTON_UP";
			case (GamepadButton)1:
				return "GAMEPAD_BUTTON_DOWN";
			case (GamepadButton)2:
				return "GAMEPAD_BUTTON_LEFT";
			case (GamepadButton)3:
				return "GAMEPAD_BUTTON_RIGHT";
			case (GamepadButton)4:
				return "GAMEPAD_BUTTON_BACK";
			case (GamepadButton)5:
				return "GAMEPAD_BUTTON_START";
			case (GamepadButton)6:
				return "GAMEPAD_BUTTON_A";
			case (GamepadButton)7:
				return "GAMEPAD_BUTTON_B";
			case (GamepadButton)8:
				return "GAMEPAD_BUTTON_X";
			case (GamepadButton)9:
				return "GAMEPAD_BUTTON_Y";
			case (GamepadButton)10:
				return "GAMEPAD_BUTTON_LB";
			case (GamepadButton)11:
				return "GAMEPAD_BUTTON_RB";
			case (GamepadButton)12:
				return "GAMEPAD_BUTTON_LTRIGGER";
			case (GamepadButton)13:
				return "GAMEPAD_BUTTON_RTRIGGER";
			case (GamepadButton)14:
				return "GAMEPAD_BUTTON_LSTICK";
			case (GamepadButton)15:
				return "GAMEPAD_BUTTON_RSTICK";
			case (GamepadButton)16:
				return "GAMEPAD_BUTTON_DPAD_UP";
			case (GamepadButton)17:
				return "GAMEPAD_BUTTON_DPAD_DOWN";
			case (GamepadButton)18:
				return "GAMEPAD_BUTTON_DPAD_LEFT";
			case (GamepadButton)19:
				return "GAMEPAD_BUTTON_DPAD_RIGHT";
			default:
				return "NONE";
			}
		}

		// Token: 0x04000AB7 RID: 2743
		public MonkeyMode mAutoMonkeyMode;

		// Token: 0x04000AB8 RID: 2744
		public float mAutoMonkeyDelay;

		// Token: 0x04000AB9 RID: 2745
		public bool mEnableAutoMonkey;

		// Token: 0x04000ABA RID: 2746
		protected GameApp mApp;

		// Token: 0x04000ABB RID: 2747
		protected List<MonkeyState> mStateList = new List<MonkeyState>();

		// Token: 0x04000ABC RID: 2748
		protected int mStateCount;

		// Token: 0x04000ABD RID: 2749
		protected List<GamepadButton> mAllowedButtons = new List<GamepadButton>();

		// Token: 0x04000ABE RID: 2750
		protected List<GamepadButton> mDirectionButtons = new List<GamepadButton>();

		// Token: 0x04000ABF RID: 2751
		protected int mLastButtonPress;

		// Token: 0x04000AC0 RID: 2752
		protected int mMoveDir;

		// Token: 0x04000AC1 RID: 2753
		protected int mRandomButtonPress;

		// Token: 0x04000AC2 RID: 2754
		protected MonkeyMode mAllModesMode;
	}
}
