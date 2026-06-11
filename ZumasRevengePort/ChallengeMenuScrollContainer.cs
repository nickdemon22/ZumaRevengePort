using System;
using System.Collections.Generic;
using SexyFramework.Graphics;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	// Token: 0x020000FD RID: 253
	public class ChallengeMenuScrollContainer : Widget
	{
		// Token: 0x06000F23 RID: 3875 RVA: 0x0009CA9C File Offset: 0x0009AC9C
		public ChallengeMenuScrollContainer(ChallengeMenu aChallengeMenu)
		{
			this.mChallengeMenu = aChallengeMenu;
			this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES);
			this.IMAGE_UI_CHALLENGESCREEN_DIVIDER = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_DIVIDER);
			this.mTableOfContents = new TableOfContents(this.mChallengeMenu);
			this.mTableOfContents.Init();
			this.mTableOfContents.Resize(0, 0, this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES.GetWidth(), this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES.GetHeight());
			this.AddWidget(this.mTableOfContents);
			int num = 4095;
			int num2 = this.mTableOfContents.mWidth;
			int num3 = 0;
			for (int i = 0; i < GlobalChallenge.NUM_CHALLENGE_ZONES; i++)
			{
				ZoneFrame zoneFrame = new ZoneFrame(this.mChallengeMenu, num3++, num);
				this.mZoneFrames.Add(zoneFrame);
				num += num;
				this.mZoneFrames[i].Move(num2, 0);
				num2 += this.mZoneFrames[i].mWidth;
				this.AddWidget(this.mZoneFrames[i]);
			}
			this.Resize(0, 0, this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES.GetWidth() * (this.mZoneFrames.Count + 1), this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES.GetHeight());
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x0009CBDC File Offset: 0x0009ADDC
		public override void Dispose()
		{
			for (int i = 0; i < this.mZoneFrames.Count; i++)
			{
				this.RemoveWidget(this.mZoneFrames[i]);
				if (this.mZoneFrames[i] != null)
				{
					this.mZoneFrames[i].Dispose();
				}
				this.mZoneFrames[i] = null;
			}
			this.mZoneFrames.Clear();
			this.RemoveWidget(this.mTableOfContents);
			if (this.mTableOfContents != null)
			{
				this.mTableOfContents.Dispose();
			}
			this.mTableOfContents = null;
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x0009CC70 File Offset: 0x0009AE70
		public void RehupChallengeButtons()
		{
			if (this.mZoneFrames.Count != 0)
			{
				for (int i = 0; i < this.mZoneFrames.Count; i++)
				{
					if (this.mZoneFrames[i] != null)
					{
						this.mZoneFrames[i].RehupChallengeButtons();
					}
				}
			}
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x0009CCC0 File Offset: 0x0009AEC0
		public override void Draw(SexyGraphics g)
		{
			int num = this.NumPages() - 1;
			int num2 = this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES.GetWidth() + GameApp.gApp.GetScreenRect().mX / 2;
			int num3 = (this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES.GetHeight() - this.IMAGE_UI_CHALLENGESCREEN_DIVIDER.GetHeight()) / 2;
			for (int i = 0; i < num; i++)
			{
				g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_DIVIDER, num2 - this.IMAGE_UI_CHALLENGESCREEN_DIVIDER.GetWidth() / 2, num3);
				num2 += this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES.GetWidth();
			}
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x0009CD44 File Offset: 0x0009AF44
		public int NumPages()
		{
			return this.mZoneFrames.Count + 1;
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x0009CD53 File Offset: 0x0009AF53
		public void AwardMedal(int theZone, bool isAce)
		{
			this.mTableOfContents.AwardMedal(theZone, isAce);
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x0009CD64 File Offset: 0x0009AF64
		public void PreloadButtonImage(int theZone)
		{
			ZoneFrame zoneFrame = this.mZoneFrames[theZone];
			if (zoneFrame != null)
			{
				zoneFrame.PreLoadButtonsImage();
			}
		}

		// Token: 0x040018A5 RID: 6309
		private ChallengeMenu mChallengeMenu;

		// Token: 0x040018A6 RID: 6310
		private List<ZoneFrame> mZoneFrames = new List<ZoneFrame>();

		// Token: 0x040018A7 RID: 6311
		private Image IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES;

		// Token: 0x040018A8 RID: 6312
		private Image IMAGE_UI_CHALLENGESCREEN_DIVIDER;

		// Token: 0x040018A9 RID: 6313
		private TableOfContents mTableOfContents;
	}
}
