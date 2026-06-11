using System;
using System.Collections.Generic;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	// Token: 0x02000103 RID: 259
	public class TableOfContents : Widget, ButtonListener
	{
		// Token: 0x06000F4B RID: 3915 RVA: 0x0009E738 File Offset: 0x0009C938
		public TableOfContents(ChallengeMenu aChallengeMenu)
		{
			this.mChallengeMenu = aChallengeMenu;
			for (int i = 0; i < GlobalChallenge.NUM_CHALLENGE_ZONES; i++)
			{
				this.mChallengeZoneBtns[i] = null;
			}
			Image imageByID = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES);
			this.Resize(0, 0, imageByID.GetWidth(), imageByID.GetHeight());
			this.mIsAwardingMedal = false;
			this.mMedalSize = 1f;
			this.mMedalAlpha = 255f;
			this.mAwardedMedal = -1;
			this.mIsAwardAce = false;
			this.mTimer = 0;
			this.mSmokeParticles = new List<LTSmokeParticle>();
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x0009E7D8 File Offset: 0x0009C9D8
		public override void Dispose()
		{
			base.Dispose();
			this.RemoveAllWidgets(false, true);
			for (int i = 0; i < GlobalChallenge.NUM_CHALLENGE_ZONES; i++)
			{
				if (this.mChallengeZoneBtns[i] != null)
				{
					this.mChallengeZoneBtns[i].Dispose();
				}
				this.mChallengeZoneBtns[i] = null;
			}
			for (int j = 0; j < this.mSmokeParticles.Count; j++)
			{
				this.mSmokeParticles[j] = null;
			}
			this.mSmokeParticles.Clear();
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x0009E854 File Offset: 0x0009CA54
		public void AwardMedal(int theZone, bool isAced)
		{
			this.mIsAwardingMedal = true;
			this.mMedalSize = 15f;
			this.mMedalAlpha = 0f;
			this.mAwardedMedal = theZone;
			this.mIsAwardAce = isAced;
			this.mTimer = 0;
			for (int i = 0; i < GlobalChallenge.NUM_CHALLENGE_ZONES; i++)
			{
				IndexMedal indexMedal = this.mChallengeZoneBtns[i];
				if (indexMedal != null)
				{
					indexMedal.SetVisible(false);
					indexMedal.SetDisabled(true);
				}
			}
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x0009E8C0 File Offset: 0x0009CAC0
		public void Init()
		{
			int[,] array = new int[6, 2];
			array[0, 0] = Common._DS(300);
			array[0, 1] = Common._DS(250);
			array[1, 0] = Common._DS(600);
			array[1, 1] = Common._DS(250);
			array[2, 0] = Common._DS(900);
			array[2, 1] = Common._DS(250);
			array[3, 0] = Common._DS(300);
			array[3, 1] = Common._DS(600);
			array[4, 0] = Common._DS(600);
			array[4, 1] = Common._DS(600);
			array[5, 0] = Common._DS(900);
			array[5, 1] = Common._DS(600);
			int[,] array2 = array;
			for (int i = 0; i < GlobalChallenge.NUM_CHALLENGE_ZONES; i++)
			{
				this.mChallengeZoneBtns[i] = new IndexMedal(this.mChallengeMenu.HasAcedZone(i), 10101 + i, this);
				Image imageByID;
				if (this.mChallengeMenu.HasAcedZone(i))
				{
					imageByID = Res.GetImageByID(ResID.IMAGE_UI_MAIN_MENU_LEAVES_CUPICON_ZONE_1 + (i + 1) * 3);
				}
				else if (this.mChallengeMenu.HasBeatZone(i))
				{
					imageByID = Res.GetImageByID(ResID.IMAGE_UI_MAIN_MENU_LEAVES_CUPICON_ZONE_1_STONE + (i + 1) * 3);
				}
				else
				{
					imageByID = Res.GetImageByID(ResID.IMAGE_UI_MAIN_MENU_LEAVES_CUPICON_ZONE_1_WOOD + (i + 1) * 3);
				}
				this.mChallengeZoneBtns[i].mButtonImage = imageByID;
				this.mChallengeZoneBtns[i].Resize(array2[i, 0] + GameApp.gApp.GetScreenRect().mX / 2, array2[i, 1], imageByID.GetWidth(), imageByID.GetHeight());
				this.mChallengeZoneBtns[i].SetVisible(true);
				this.mChallengeZoneBtns[i].SetDisabled(false);
				this.mChallengeZoneBtns[i].Init();
				this.AddWidget(this.mChallengeZoneBtns[i]);
			}
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x0009EABC File Offset: 0x0009CCBC
		public override void Update()
		{
			if (!this.mIsAwardingMedal)
			{
				return;
			}
			if (GameApp.gApp.mBambooTransition.IsInProgress())
			{
				return;
			}
			for (int i = 0; i < this.mSmokeParticles.Count; i++)
			{
				if (BambooTransition.UpdateSmokeParticle(this.mSmokeParticles[i]))
				{
					this.mSmokeParticles.RemoveAt(i);
					i--;
				}
			}
			this.MarkDirty();
			this.mTimer++;
			int num = Common._M(75) - this.mTimer;
			float num2 = 255f / (float)num;
			this.mMedalAlpha += num2;
			if (this.mMedalAlpha > 255f)
			{
				this.mMedalAlpha = 255f;
			}
			num2 = Common._M(15f) / (float)num;
			this.mMedalSize -= num2;
			if (this.mMedalSize <= 1f)
			{
				if (this.mIsAwardAce)
				{
					GameApp.gApp.PlaySample(Res.GetSoundByID(ResID.SOUND_MINI_CROWN_IMPACT));
				}
				else
				{
					GameApp.gApp.PlaySample(Res.GetSoundByID(ResID.SOUND_ACE_MINI_CROWN_IMPACT));
				}
				GlobalChallenge.gScreenShakeTimer = Common._M(15);
				this.mMedalSize = 1f;
				this.mMedalAlpha = 255f;
				if (GameApp.gApp.mUserProfile.mDoChallengeAceCupComplete)
				{
					GameApp.gApp.mUserProfile.mDoChallengeAceCupComplete = false;
				}
				else if (GameApp.gApp.mUserProfile.mDoChallengeCupComplete)
				{
					GameApp.gApp.mUserProfile.mDoChallengeCupComplete = false;
				}
				Image imageByID;
				if (this.mIsAwardAce)
				{
					imageByID = Res.GetImageByID(ResID.IMAGE_UI_MAIN_MENU_LEAVES_CUPICON_ZONE_1 + (this.mAwardedMedal + 1) * 3);
					this.mChallengeZoneBtns[this.mAwardedMedal].SetAced();
				}
				else
				{
					imageByID = Res.GetImageByID(ResID.IMAGE_UI_MAIN_MENU_LEAVES_CUPICON_ZONE_1_STONE + (this.mAwardedMedal + 1) * 3);
				}
				this.mChallengeZoneBtns[this.mAwardedMedal].mButtonImage = imageByID;
				this.mIsAwardingMedal = false;
				this.mIsAwardAce = false;
				this.mMedalSize = 1f;
				this.mMedalAlpha = 255f;
				for (int j = 0; j < 40; j++)
				{
					float x = (float)this.mChallengeZoneBtns[this.mAwardedMedal].mX + (float)this.mChallengeZoneBtns[this.mAwardedMedal].mWidth / 2f;
					float y = (float)this.mChallengeZoneBtns[this.mAwardedMedal].mY + (float)this.mChallengeZoneBtns[this.mAwardedMedal].mHeight / 2f;
					this.mSmokeParticles.Add(BambooTransition.SpawnSmokeParticle(x, y, false, false));
				}
				for (int k = 0; k < GlobalChallenge.NUM_CHALLENGE_ZONES; k++)
				{
					ButtonWidget buttonWidget = this.mChallengeZoneBtns[k];
					if (buttonWidget != null)
					{
						buttonWidget.SetVisible(true);
						buttonWidget.SetDisabled(false);
					}
				}
			}
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x0009ED6C File Offset: 0x0009CF6C
		public override void Draw(SexyGraphics g)
		{
			string @string = TextManager.getInstance().getString(426);
			g.SetFont(Res.GetFontByID(ResID.FONT_SHAGEXOTICA68_STROKE));
			g.SetColor(Color.White);
			float num = (float)g.GetFont().StringWidth(@string);
			g.DrawString(@string, (int)((float)(GameApp.gApp.GetScreenRect().mX + this.mWidth) - num) / 2, Common._DS(150));
			float[,] array = new float[6, 2];
			array[0, 0] = (float)Common._DS(5);
			array[0, 1] = (float)Common._DS(-5);
			array[1, 0] = (float)Common._DS(11);
			array[1, 1] = (float)Common._DS(2);
			array[2, 0] = (float)Common._DS(-8);
			array[2, 1] = (float)Common._DS(5);
			array[3, 0] = (float)Common._DS(-7);
			array[3, 1] = (float)Common._DS(-1);
			array[4, 0] = (float)Common._DS(0);
			array[4, 1] = (float)Common._DS(0);
			array[5, 0] = (float)Common._DS(0);
			array[5, 1] = (float)Common._DS(0);
			float[,] array2 = array;
			for (int i = 0; i < GlobalChallenge.NUM_CHALLENGE_ZONES; i++)
			{
				if (this.mChallengeZoneBtns[i] != null)
				{
					Image imageByID = Res.GetImageByID(ResID.IMAGE_UI_MAIN_MENU_LEAVES_LEAVES1 + i);
					float num2 = array2[i, 0] + (float)this.mChallengeZoneBtns[i].mX - (float)((imageByID.GetWidth() - this.mChallengeZoneBtns[i].mButtonImage.GetWidth()) / 2);
					float num3 = array2[i, 1] + (float)this.mChallengeZoneBtns[i].mY - (float)((imageByID.GetHeight() - this.mChallengeZoneBtns[i].mButtonImage.GetHeight()) / 2);
					g.DrawImage(imageByID, (int)num2, (int)num3);
					if (this.mIsAwardingMedal)
					{
						g.DrawImage(this.mChallengeZoneBtns[i].mButtonImage, this.mChallengeZoneBtns[i].mX, this.mChallengeZoneBtns[i].mY);
					}
					for (int j = 0; j < this.mSmokeParticles.Count; j++)
					{
						BambooTransition.DrawSmokeParticle(g, this.mSmokeParticles[j]);
					}
				}
			}
			if (this.mIsAwardingMedal)
			{
				g.PushState();
				g.ClearClipRect();
				Image imageByID2;
				if (this.mIsAwardAce)
				{
					imageByID2 = Res.GetImageByID(ResID.IMAGE_UI_MAIN_MENU_LEAVES_CUPICON_ZONE_1 + (this.mAwardedMedal + 1) * 3);
				}
				else
				{
					imageByID2 = Res.GetImageByID(ResID.IMAGE_UI_MAIN_MENU_LEAVES_CUPICON_ZONE_1_STONE + (this.mAwardedMedal + 1) * 3);
				}
				g.SetColor(new Color(255, 255, 255, (int)this.mMedalAlpha));
				g.SetColorizeImages(true);
				SexyTransform2D sexyTransform2D;
				sexyTransform2D = new SexyTransform2D(false);
				sexyTransform2D.Scale(this.mMedalSize, this.mMedalSize);
				sexyTransform2D.Translate((float)this.mChallengeZoneBtns[this.mAwardedMedal].mX + ((float)this.mChallengeZoneBtns[this.mAwardedMedal].mButtonImage.mWidth - (float)imageByID2.mWidth * this.mMedalSize) / 2f, (float)this.mChallengeZoneBtns[this.mAwardedMedal].mY + ((float)this.mChallengeZoneBtns[this.mAwardedMedal].mButtonImage.mHeight - (float)imageByID2.mHeight * this.mMedalSize) / 2f);
				g.DrawImageMatrix(imageByID2, sexyTransform2D, (float)imageByID2.mWidth * this.mMedalSize / 2f, (float)imageByID2.mHeight * this.mMedalSize / 2f);
				g.PopState();
			}
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x0009F11C File Offset: 0x0009D31C
		public virtual void ButtonDepress(int id)
		{
			if (GameApp.gApp.mBambooTransition != null && GameApp.gApp.mBambooTransition.IsInProgress())
			{
				return;
			}
			GameApp.gApp.PlaySample(Res.GetSoundByID(ResID.SOUND_BUTTON2));
			switch (id)
			{
			case 10101:
				this.mChallengeMenu.mChallengeScrollWidget.SetPageHorizontal(1, true);
				return;
			case 10102:
				this.mChallengeMenu.mChallengeScrollWidget.SetPageHorizontal(2, true);
				return;
			case 10103:
				this.mChallengeMenu.mChallengeScrollWidget.SetPageHorizontal(3, true);
				return;
			case 10104:
				this.mChallengeMenu.mChallengeScrollWidget.SetPageHorizontal(4, true);
				return;
			case 10105:
				this.mChallengeMenu.mChallengeScrollWidget.SetPageHorizontal(5, true);
				return;
			case 10106:
				this.mChallengeMenu.mChallengeScrollWidget.SetPageHorizontal(6, true);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x0009F1F3 File Offset: 0x0009D3F3
		public virtual void ButtonDownTick(int x)
		{
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x0009F1F5 File Offset: 0x0009D3F5
		public virtual void ButtonMouseEnter(int x)
		{
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x0009F1F7 File Offset: 0x0009D3F7
		public virtual void ButtonMouseLeave(int x)
		{
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x0009F1F9 File Offset: 0x0009D3F9
		public virtual void ButtonMouseMove(int x, int y, int z)
		{
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x0009F1FB File Offset: 0x0009D3FB
		public virtual void ButtonPress(int id)
		{
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x0009F1FD File Offset: 0x0009D3FD
		public virtual void ButtonPress(int id, int count)
		{
		}

		// Token: 0x040018CB RID: 6347
		private bool mIsAwardingMedal;

		// Token: 0x040018CC RID: 6348
		private bool mIsAwardAce;

		// Token: 0x040018CD RID: 6349
		private float mMedalSize;

		// Token: 0x040018CE RID: 6350
		private float mMedalAlpha;

		// Token: 0x040018CF RID: 6351
		private int mAwardedMedal;

		// Token: 0x040018D0 RID: 6352
		private int mTimer;

		// Token: 0x040018D1 RID: 6353
		private IndexMedal[] mChallengeZoneBtns = new IndexMedal[GlobalChallenge.NUM_CHALLENGE_ZONES];

		// Token: 0x040018D2 RID: 6354
		private ChallengeMenu mChallengeMenu;

		// Token: 0x040018D3 RID: 6355
		private List<LTSmokeParticle> mSmokeParticles;

		// Token: 0x02000104 RID: 260
		private enum ChallengeZonePages
		{
			// Token: 0x040018D5 RID: 6357
			ContentId_MettleOfTheMonkey = 10101,
			// Token: 0x040018D6 RID: 6358
			ContentId_RoosterRumble,
			// Token: 0x040018D7 RID: 6359
			ContentId_JackalJam,
			// Token: 0x040018D8 RID: 6360
			ContentId_MarshMadness,
			// Token: 0x040018D9 RID: 6361
			ContentId_UnderseaUndertaking,
			// Token: 0x040018DA RID: 6362
			ContentId_SerpentScuffle,
			// Token: 0x040018DB RID: 6363
			NUM_CHALLENGE_ZONE_PAGES
		}
	}
}
