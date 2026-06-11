using System;
using System.Collections.Generic;
using System.Text;
using JeffLib;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	// Token: 0x0200014D RID: 333
	public class TikiTemplePages : Widget
	{
		// Token: 0x06001028 RID: 4136 RVA: 0x000A6060 File Offset: 0x000A4260
		public TikiTemplePages(TikiTemple theTikiTemple)
		{
			this.mTikiTemple = theTikiTemple;
			this.mNumPages = 0;
			this.mHeaderFont = Res.GetFontByID(ResID.FONT_SHAGEXOTICA38_BLACK_GLOW);
			this.mStatsFont = Res.GetFontByID(ResID.FONT_SHAGLOUNGE38_STROKE);
			this.mText = new List<TempleText>();
			this.AddPage(TikiTemplePages.PageInfo.TikiTemple_PageAdventure);
			this.AddPage(TikiTemplePages.PageInfo.TikiTemple_PageChallenge);
			this.AddPage(TikiTemplePages.PageInfo.TikiTemple_PageStats);
			this.AddPage(TikiTemplePages.PageInfo.TikiTemple_PageMoreStats);
			for (int i = 0; i < this.mText.Count; i++)
			{
				this.mText[i].mAlpha = 255f;
			}
			this.Resize(0, 0, (this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES.GetWidth() + 30) * this.mNumPages, this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES.GetHeight());
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x000A6136 File Offset: 0x000A4336
		public int NumPages()
		{
			return this.mNumPages;
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x000A6140 File Offset: 0x000A4340
		private void AddPage(TikiTemplePages.PageInfo thePage)
		{
			int num = (this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES.GetWidth() + 30) * this.mNumPages + (int)this.mTikiTemple.GetTitleXOffset() + 55;
			switch (thePage)
			{
			case TikiTemplePages.PageInfo.TikiTemple_PageStats:
				this.SetupStatsText(ref num);
				break;
			case TikiTemplePages.PageInfo.TikiTemple_PageMoreStats:
				this.SetupMoreStatsText(ref num);
				break;
			case TikiTemplePages.PageInfo.TikiTemple_PageChallenge:
				this.SetupChallengeText(ref num);
				break;
			case TikiTemplePages.PageInfo.TikiTemple_PageAdventure:
				this.SetupAdventureText(false, ref num);
				break;
			}
			this.mNumPages++;
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x000A61C4 File Offset: 0x000A43C4
		public override void Draw(SexyGraphics g)
		{
			for (int i = 0; i < this.mText.Count; i++)
			{
				TempleText templeText = this.mText[i];
				if (templeText.mValueStr.Length == 0)
				{
					g.SetFont(this.mHeaderFont);
					g.SetColor(255, 249, 161);
					g.DrawString(templeText.mHeaderStr, templeText.mX + (int)this.mTikiTemple.mXOff, templeText.mY + g.GetFont().GetAscent());
				}
				else
				{
					g.SetFont(this.mStatsFont);
					g.SetColor(166, 158, 255);
					g.WriteString(templeText.mHeaderStr, templeText.mX + (int)this.mTikiTemple.mXOff, templeText.mY + this.mStatsFont.GetAscent(), 0, 1);
					g.SetColor(89, 187, 149);
					g.WriteString(templeText.mValueStr, templeText.mX + Common._DS(Common._M(20)) + (int)this.mTikiTemple.mXOff, templeText.mY + this.mStatsFont.GetAscent(), this.mWidth, -1);
				}
			}
			int num = this.NumPages() - 1;
			int num2 = this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES.GetWidth() + 30;
			int num3 = (this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES.GetHeight() - this.IMAGE_UI_CHALLENGESCREEN_DIVIDER.GetHeight()) / 2;
			for (int j = 0; j < num; j++)
			{
				g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_DIVIDER, num2 - this.IMAGE_UI_CHALLENGESCREEN_DIVIDER.GetWidth(), num3);
				num2 += this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES.GetWidth() + 30;
			}
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x000A637C File Offset: 0x000A457C
		public override void Update()
		{
			float num = Common._M(10f);
			for (int i = 0; i < Common.size<TempleText>(this.mText); i++)
			{
				TempleText templeText = this.mText[i];
				if (templeText.mFadeIn && templeText.mAlpha < 255f)
				{
					this.MarkDirty();
					templeText.mAlpha += num;
					if (templeText.mAlpha > 255f)
					{
						templeText.mAlpha = 255f;
					}
				}
				else if (!templeText.mFadeIn && templeText.mAlpha > 0f)
				{
					this.MarkDirty();
					templeText.mAlpha -= num;
					if (templeText.mAlpha <= 0f)
					{
						this.mText.RemoveAt(i);
						i--;
					}
				}
			}
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x000A6448 File Offset: 0x000A4648
		private void SetupIronFrogText(ref int theStartX)
		{
			int mX = Common._DS(Common._M(832)) - this.mX + (GameApp.gApp.GetScreenWidth() - GameApp.gApp.mScreenBounds.mWidth);
			int num = Common._DS(Common._M(6)) + this.mStatsFont.mHeight;
			IronFrogTempleStats mIronFrogStats = GameApp.gApp.mUserProfile.mIronFrogStats;
			this.mText.Add(new TempleText());
			TempleText templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(791);
			templeText.mX = (GameApp.gApp.GetScreenWidth() - this.mHeaderFont.StringWidth(templeText.mHeaderStr)) / 2;
			templeText.mY = Common._DS(Common._M(64));
			int num2 = Common._DS(Common._M(416));
			this.mText.Add(new TempleText());
			templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(792);
			templeText.mValueStr = string.Format("{0:D}", mIronFrogStats.mNumAttempts);
			templeText.mX = mX;
			templeText.mY = num2;
			num2 += num;
			this.mText.Add(new TempleText());
			templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(793);
			templeText.mValueStr = string.Format("{0:D}", mIronFrogStats.mNumVictories);
			templeText.mX = mX;
			templeText.mY = num2;
			num2 += num;
			this.mText.Add(new TempleText());
			templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(794);
			templeText.mValueStr = ((mIronFrogStats.mBestTime == 0) ? "None" : Common.UpdateToTimeStr(mIronFrogStats.mBestTime, false));
			templeText.mX = mX;
			templeText.mY = num2;
			num2 += num;
			this.mText.Add(new TempleText());
			templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(795);
			templeText.mValueStr = Common.CommaSeperate(mIronFrogStats.mBestScore);
			templeText.mX = mX;
			templeText.mY = num2;
			num2 += num;
			this.mText.Add(new TempleText());
			templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(796);
			templeText.mValueStr = string.Format("{0:D}", mIronFrogStats.mHighestLevel);
			templeText.mX = mX;
			templeText.mY = num2;
			num2 += num;
			this.mText.Add(new TempleText());
			templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(797);
			int num3 = 0;
			int num4 = -1;
			for (int i = 0; i < 10; i++)
			{
				if (mIronFrogStats.mLevelDeaths[i] > num3)
				{
					num3 = mIronFrogStats.mLevelDeaths[i];
					num4 = i;
				}
			}
			if (num4 == -1)
			{
				templeText.mValueStr = TextManager.getInstance().getString(771);
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder(TextManager.getInstance().getString(798));
				stringBuilder.Replace("$1", (num4 + 1).ToString());
				stringBuilder.Replace("$2", num3.ToString());
				templeText.mValueStr = stringBuilder.ToString();
			}
			templeText.mX = mX;
			templeText.mY = num2;
			num2 += num;
			this.mText.Add(new TempleText());
			templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(799);
			templeText.mValueStr = Common.UpdateToTimeStr(mIronFrogStats.mTotalTimePlayed, true);
			templeText.mX = mX;
			templeText.mY = num2;
			num2 += num;
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x000A6848 File Offset: 0x000A4A48
		private void SetupStatsText(ref int theStartX)
		{
			int mX = theStartX + Common._DS(Common._M(732));
			int num = Common._DS(Common._M(6)) + this.mStatsFont.mHeight;
			ChallengeTempleStats mChallengeStats = GameApp.gApp.mUserProfile.mChallengeStats;
			this.mText.Add(new TempleText());
			TempleText templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(800);
			templeText.mX = theStartX - Common._DS(60) + (this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES.GetWidth() + 30 - this.mHeaderFont.StringWidth(templeText.mHeaderStr)) / 2;
			templeText.mY = Common._DS(Common._M(64));
			int num2 = Common._DS(Common._M(140));
			this.mText.Add(new TempleText());
			templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(801);
			templeText.mValueStr = string.Format("{0:D}", GameApp.gApp.mUserProfile.mLargestChainShot);
			templeText.mX = mX;
			templeText.mY = num2;
			num2 += num;
			this.mText.Add(new TempleText());
			templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(802);
			templeText.mValueStr = string.Format("{0:D}", GameApp.gApp.mUserProfile.mLargestCombo);
			templeText.mX = mX;
			templeText.mY = num2;
			num2 += num;
			this.mText.Add(new TempleText());
			templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(803);
			templeText.mValueStr = string.Format("{0:D}", GameApp.gApp.mUserProfile.mHighestGapShotScore);
			templeText.mX = mX;
			templeText.mY = num2;
			num2 += num;
			this.mText.Add(new TempleText());
			templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(804);
			templeText.mValueStr = string.Format("{0:D}", GameApp.gApp.mUserProfile.mNumGapShots);
			templeText.mX = mX;
			templeText.mY = num2;
			num2 += num;
			this.mText.Add(new TempleText());
			templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(805);
			templeText.mValueStr = string.Format("{0:D}", GameApp.gApp.mUserProfile.mNumDoubleGapShots);
			templeText.mX = mX;
			templeText.mY = num2;
			num2 += num;
			this.mText.Add(new TempleText());
			templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(806);
			templeText.mValueStr = string.Format("{0:D}", GameApp.gApp.mUserProfile.mNumTripleGapShots);
			templeText.mX = mX;
			templeText.mY = num2;
			num2 += num;
			this.mText.Add(new TempleText());
			templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(807);
			templeText.mValueStr = string.Format("{0:D}", GameApp.gApp.mUserProfile.mNumFruits);
			templeText.mX = mX;
			templeText.mY = num2;
			num2 += num;
			this.mText.Add(new TempleText());
			templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(808);
			templeText.mValueStr = string.Format("{0:D}", GameApp.gApp.mUserProfile.mNumTimesActivatedPowerup[0]);
			templeText.mX = mX;
			templeText.mY = num2;
			num2 += num;
			this.mText.Add(new TempleText());
			templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(809);
			templeText.mValueStr = string.Format("{0:D}", GameApp.gApp.mUserProfile.mNumTimesActivatedPowerup[9]);
			templeText.mX = mX;
			templeText.mY = num2;
			num2 += num;
			this.mText.Add(new TempleText());
			templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(810);
			templeText.mValueStr = string.Format("{0:D}", GameApp.gApp.mUserProfile.mNumTimesActivatedPowerup[8]);
			templeText.mX = mX;
			templeText.mY = num2;
			num2 += num;
			this.mText.Add(new TempleText());
			templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(811);
			templeText.mValueStr = string.Format("{0:D}", GameApp.gApp.mUserProfile.mNumTimesActivatedPowerup[7]);
			templeText.mX = mX;
			templeText.mY = num2;
			num2 += num;
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x000A6D8C File Offset: 0x000A4F8C
		private void SetupMoreStatsText(ref int theStartX)
		{
			int mX = theStartX + Common._DS(Common._M(732));
			int num = Common._DS(Common._M(6)) + this.mStatsFont.mHeight;
			ChallengeTempleStats mChallengeStats = GameApp.gApp.mUserProfile.mChallengeStats;
			this.mText.Add(new TempleText());
			TempleText templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(812);
			templeText.mX = theStartX - Common._DS(60) + (this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES.GetWidth() + 30 - this.mHeaderFont.StringWidth(templeText.mHeaderStr)) / 2;
			templeText.mY = Common._DS(Common._M(64));
			int num2 = Common._DS(Common._M(140));
			this.mText.Add(new TempleText());
			templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(813);
			templeText.mValueStr = string.Format("{0:D}", GameApp.gApp.mUserProfile.mBallsSwapped);
			templeText.mX = mX;
			templeText.mY = num2;
			num2 += num;
			this.mText.Add(new TempleText());
			templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(814);
			templeText.mValueStr = string.Format("{0:D}", GameApp.gApp.mUserProfile.mBallsFired);
			templeText.mX = mX;
			templeText.mY = num2;
			num2 += num;
			int num3 = 0;
			int t = 0;
			for (int i = 0; i < 14; i++)
			{
				if (!Common.IsDeprecatedPowerUp((PowerType)i) && GameApp.gApp.mUserProfile.mNumTimesActivatedPowerup[i] > num3)
				{
					num3 = GameApp.gApp.mUserProfile.mNumTimesActivatedPowerup[i];
					t = i;
				}
			}
			this.mText.Add(new TempleText());
			templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(815);
			templeText.mValueStr = ((num3 <= 0) ? TextManager.getInstance().getString(771) : string.Format("{0} ({1:D}x)", Common.PowerupToStr((PowerType)t, false), num3));
			templeText.mX = mX;
			templeText.mY = num2;
			num2 += num;
			this.mText.Add(new TempleText());
			templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(816);
			templeText.mValueStr = Common.CommaSeperate(GameApp.gApp.mUserProfile.mPointsFromCombos);
			templeText.mX = mX;
			templeText.mY = num2;
			num2 += num;
			this.mText.Add(new TempleText());
			templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(817);
			templeText.mValueStr = Common.CommaSeperate(GameApp.gApp.mUserProfile.mPointsFromChainShots);
			templeText.mX = mX;
			templeText.mY = num2;
			num2 += num;
			this.mText.Add(new TempleText());
			templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(818);
			templeText.mValueStr = Common.CommaSeperate(GameApp.gApp.mUserProfile.mPointsFromGapShots);
			templeText.mX = mX;
			templeText.mY = num2;
			num2 += num;
			int num4 = GameApp.gApp.mUserProfile.mPointsFromCannon + GameApp.gApp.mUserProfile.mPointsFromColorNuke + GameApp.gApp.mUserProfile.mPointsFromLaser + GameApp.gApp.mUserProfile.mPointsFromProxBomb;
			this.mText.Add(new TempleText());
			templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(819);
			templeText.mValueStr = Common.CommaSeperate(num4);
			templeText.mX = mX;
			templeText.mY = num2;
			num2 += num;
			int num5 = GameApp.gApp.mUserProfile.mAdventureStats.mTotalTimePlayed + GameApp.gApp.mUserProfile.mHeroicStats.mTotalTimePlayed + GameApp.gApp.mUserProfile.mIronFrogStats.mTotalTimePlayed + GameApp.gApp.mUserProfile.mChallengeStats.mTotalTime;
			this.mText.Add(new TempleText());
			templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(820);
			templeText.mValueStr = Common.UpdateToTimeStr(num5, true);
			templeText.mX = mX;
			templeText.mY = num2;
			num2 += num;
			this.mText.Add(new TempleText());
			templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(821);
			templeText.mValueStr = string.Format("{0:D}", GameApp.gApp.mUserProfile.mFruitBombed);
			templeText.mX = mX;
			templeText.mY = num2;
			num2 += num;
			this.mText.Add(new TempleText());
			templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(822);
			templeText.mValueStr = string.Format("{0:D}", GameApp.gApp.mUserProfile.mBallsTossed);
			templeText.mX = mX;
			templeText.mY = num2;
			num2 += num;
			this.mText.Add(new TempleText());
			templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(823);
			templeText.mValueStr = string.Format("{0:D}", GameApp.gApp.mUserProfile.mDeathsAfterZuma);
			templeText.mX = mX;
			templeText.mY = num2;
			num2 += num;
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x000A7374 File Offset: 0x000A5574
		private void SetupChallengeText(ref int theStartX)
		{
			int mX = theStartX + Common._DS(Common._M(732));
			int num = Common._DS(Common._M(6)) + this.mStatsFont.mHeight;
			ChallengeTempleStats mChallengeStats = GameApp.gApp.mUserProfile.mChallengeStats;
			this.mText.Add(new TempleText());
			TempleText templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(782);
			templeText.mX = theStartX - Common._DS(60) + (this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES.GetWidth() + 30 - this.mHeaderFont.StringWidth(templeText.mHeaderStr)) / 2;
			templeText.mY = Common._DS(Common._M(64));
			int num2 = Common._DS(Common._M(140));
			this.mText.Add(new TempleText());
			templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(783);
			templeText.mValueStr = Common.CommaSeperate(mChallengeStats.mHighestScore);
			templeText.mX = mX;
			templeText.mY = num2;
			num2 += num;
			int num3 = 0;
			int num4 = 0;
			for (int i = 0; i < 7; i++)
			{
				for (int j = 0; j < 10; j++)
				{
					if (GameApp.gApp.mUserProfile.mChallengeUnlockState[i, j] == 4)
					{
						num3++;
					}
					else if (GameApp.gApp.mUserProfile.mChallengeUnlockState[i, j] == 5)
					{
						num3++;
						num4++;
					}
				}
			}
			this.mText.Add(new TempleText());
			templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(784);
			templeText.mValueStr = string.Format("{0:D} / 60", num3);
			templeText.mX = mX;
			templeText.mY = num2;
			num2 += num;
			this.mText.Add(new TempleText());
			templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(785);
			templeText.mValueStr = string.Format("{0:D} / 60", num4);
			templeText.mX = mX;
			templeText.mY = num2;
			num2 += num;
			int num5 = 0;
			int num6 = 0;
			for (int k = 1; k < 7; k++)
			{
				if (GameApp.gApp.mUserProfile.ChallengeCupComplete(k) == 2)
				{
					num5++;
					num6++;
				}
				else if (GameApp.gApp.mUserProfile.ChallengeCupComplete(k) == 1)
				{
					num5++;
				}
			}
			this.mText.Add(new TempleText());
			templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(786);
			templeText.mValueStr = string.Format("{0:D} / 6", num5);
			templeText.mX = mX;
			templeText.mY = num2;
			num2 += num;
			this.mText.Add(new TempleText());
			templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(787);
			templeText.mValueStr = string.Format("{0:D} / 6", num6);
			templeText.mX = mX;
			templeText.mY = num2;
			num2 += num;
			this.mText.Add(new TempleText());
			templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(788);
			templeText.mValueStr = string.Format("x{0:D}", mChallengeStats.mHighestMult);
			templeText.mX = mX;
			templeText.mY = num2;
			num2 += num;
			this.mText.Add(new TempleText());
			templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(789);
			int num7 = 0;
			int num8 = 0;
			for (int l = 0; l < 70; l++)
			{
				if (mChallengeStats.mNumTimesPlayedCurve[l] > num7)
				{
					num7 = mChallengeStats.mNumTimesPlayedCurve[l];
					num8 = l + 1;
				}
			}
			StringBuilder stringBuilder = new StringBuilder(TextManager.getInstance().getString(790));
			stringBuilder.Replace("$1", num8.ToString());
			stringBuilder.Replace("$2", num7.ToString());
			templeText.mValueStr = stringBuilder.ToString();
			templeText.mX = mX;
			templeText.mY = num2;
			num2 += num;
			this.mText.Add(new TempleText());
			templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(780);
			templeText.mValueStr = Common.UpdateToTimeStr(mChallengeStats.mTotalTime, true);
			templeText.mX = mX;
			templeText.mY = num2;
			num2 += num;
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x000A7850 File Offset: 0x000A5A50
		private void SetupAdventureText(bool hard_mode, ref int theStartX)
		{
			int mX = theStartX + Common._DS(Common._M(732));
			int num = Common._DS(Common._M(6)) + this.mStatsFont.mHeight;
			AdvModeTempleStats advModeTempleStats = hard_mode ? GameApp.gApp.mUserProfile.mHeroicStats : GameApp.gApp.mUserProfile.mAdventureStats;
			this.mText.Add(new TempleText());
			TempleText templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = (hard_mode ? TextManager.getInstance().getString(766) : TextManager.getInstance().getString(767));
			templeText.mX = theStartX - Common._DS(60) + (this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES.GetWidth() + 30 - this.mHeaderFont.StringWidth(templeText.mHeaderStr)) / 2;
			templeText.mY = Common._DS(Common._M(64));
			int num2 = Common._DS(Common._M(140));
			this.mText.Add(new TempleText());
			templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(768);
			templeText.mValueStr = string.Format("{0:D}", advModeTempleStats.mHighestLevel);
			templeText.mX = mX;
			templeText.mY = num2;
			num2 += num;
			this.mText.Add(new TempleText());
			templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(769);
			templeText.mValueStr = ((advModeTempleStats.mBestTime <= 0 || advModeTempleStats.mBestTime == int.MaxValue) ? TextManager.getInstance().getString(771) : Common.UpdateToTimeStr(advModeTempleStats.mBestTime, true));
			templeText.mX = mX;
			templeText.mY = num2;
			num2 += num;
			this.mText.Add(new TempleText());
			templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(770);
			templeText.mValueStr = ((advModeTempleStats.mBestScore <= 0) ? TextManager.getInstance().getString(771) : Common.CommaSeperate(advModeTempleStats.mBestScore));
			templeText.mX = mX;
			templeText.mY = num2;
			num2 += num;
			this.mText.Add(new TempleText());
			templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(772);
			templeText.mValueStr = string.Format("{0:D}", advModeTempleStats.mNumLevelsAced);
			templeText.mX = mX;
			templeText.mY = num2;
			num2 += num;
			this.mText.Add(new TempleText());
			templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(773);
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			for (int i = 0; i < 60; i++)
			{
				num3 += advModeTempleStats.mLevelDeaths[i];
				if (advModeTempleStats.mLevelDeaths[i] > num5)
				{
					num5 = advModeTempleStats.mLevelDeaths[i];
					num4 = i + 1;
				}
			}
			templeText.mValueStr = string.Format("{0:D}", num3);
			templeText.mX = mX;
			templeText.mY = num2;
			num2 += num;
			this.mText.Add(new TempleText());
			templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(774);
			templeText.mValueStr = string.Format("{0:D}", advModeTempleStats.mNumPerfectLevels);
			templeText.mX = mX;
			templeText.mY = num2;
			num2 += num;
			this.mText.Add(new TempleText());
			templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(775);
			templeText.mValueStr = string.Format("{0:D}", advModeTempleStats.mNumClearCurves);
			templeText.mX = mX;
			templeText.mY = num2;
			num2 += num;
			this.mText.Add(new TempleText());
			templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(776);
			int num6 = 0;
			int num7 = 0;
			for (int j = 0; j < 6; j++)
			{
				if (advModeTempleStats.mBossDeaths[j] > num7)
				{
					num6 = j + 1;
					num7 = advModeTempleStats.mBossDeaths[j];
				}
			}
			if (num6 == 0)
			{
				templeText.mValueStr = TextManager.getInstance().getString(771);
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder(TextManager.getInstance().getString(778));
				stringBuilder.Replace("$1", num6.ToString());
				stringBuilder.Replace("$2", num7.ToString());
				templeText.mValueStr = stringBuilder.ToString();
			}
			templeText.mX = mX;
			templeText.mY = num2;
			num2 += num;
			this.mText.Add(new TempleText());
			templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(777);
			if (num4 == 0)
			{
				templeText.mValueStr = TextManager.getInstance().getString(771);
			}
			else
			{
				StringBuilder stringBuilder2 = new StringBuilder(TextManager.getInstance().getString(779));
				stringBuilder2.Replace("$1", num4.ToString());
				stringBuilder2.Replace("$2", num5.ToString());
				templeText.mValueStr = stringBuilder2.ToString();
			}
			templeText.mX = mX;
			templeText.mY = num2;
			num2 += num;
			this.mText.Add(new TempleText());
			templeText = Common.back<TempleText>(this.mText);
			templeText.mHeaderStr = TextManager.getInstance().getString(780);
			templeText.mValueStr = Common.UpdateToTimeStr(advModeTempleStats.mTotalTimePlayed, true);
			templeText.mX = mX;
			templeText.mY = num2;
			num2 += num;
		}

		// Token: 0x04001ABF RID: 6847
		private List<TempleText> mText;

		// Token: 0x04001AC0 RID: 6848
		private Font mHeaderFont;

		// Token: 0x04001AC1 RID: 6849
		private Font mStatsFont;

		// Token: 0x04001AC2 RID: 6850
		private TikiTemple mTikiTemple;

		// Token: 0x04001AC3 RID: 6851
		private int mNumPages;

		// Token: 0x04001AC4 RID: 6852
		private Image IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES);

		// Token: 0x04001AC5 RID: 6853
		private Image IMAGE_UI_CHALLENGESCREEN_DIVIDER = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_DIVIDER);

		// Token: 0x02000159 RID: 345
		private enum PageInfo
		{
			// Token: 0x04001AF4 RID: 6900
			TikiTemple_PageStats,
			// Token: 0x04001AF5 RID: 6901
			TikiTemple_PageMoreStats,
			// Token: 0x04001AF6 RID: 6902
			TikiTemple_PageChallenge,
			// Token: 0x04001AF7 RID: 6903
			TikiTemple_PageAdventure
		}
	}
}
