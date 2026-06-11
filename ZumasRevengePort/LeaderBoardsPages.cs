using System;
using System.Collections.Generic;
using System.IO;
// using Microsoft.Xna.Framework.GamerServices;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	// Token: 0x02000145 RID: 325
	public class LeaderBoardsPages : Widget
	{
		// Token: 0x06001002 RID: 4098 RVA: 0x000A3990 File Offset: 0x000A1B90
		public LeaderBoardsPages(LeaderBoards theLeaderBoards)
		{
			this.mLeaderBoards = theLeaderBoards;
			this.mNumPages = 0;
			this.mHeaderFont = Res.GetFontByID(ResID.FONT_SHAGLOUNGE45_GAUNTLET);
			this.mStatsFont = Res.GetFontByID(ResID.FONT_SHAGLOUNGE38_STROKE);
			this.mText = new List<LeaderBoardText>();
			for (int i = 0; i < this.mText.Count; i++)
			{
				this.mText[i].mAlpha = 255f;
			}
			this.Resize(0, 0, this.IMAGE_UI_LEADERBOARDS_SHADOW.GetWidth(), (this.IMAGE_UI_LEADERBOARDS_SHADOW.GetHeight() + 30) * this.mNumPages);
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x000A3A71 File Offset: 0x000A1C71
		public int NumPages()
		{
			return this.mNumPages;
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x000A3A7C File Offset: 0x000A1C7C
		public void AddPage(int page, bool isUpdate, object reader)
		{
			int num = (this.IMAGE_UI_LEADERBOARDS_SHADOW.GetHeight() - 40) * (this.mNumPages + 1);
			this.SetupLeaderboardsTextXLive(ref num, page, isUpdate, reader);
			if (!isUpdate)
			{
				this.mNumPages++;
			}
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x000A3AC0 File Offset: 0x000A1CC0
		public void AddPage(int page, bool isUpdate)
		{
			int num = (this.IMAGE_UI_LEADERBOARDS_SHADOW.GetHeight() - 40) * (this.mNumPages + 1);
			this.SetupLeaderboardsText(ref num, page, isUpdate);
			if (!isUpdate)
			{
				this.mNumPages++;
			}
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x000A3B00 File Offset: 0x000A1D00
		public override void Draw(SexyGraphics g)
		{
			Graphics3D graphics3D = g.Get3D();
			graphics3D.SetMasking((Graphics3D.EMaskMode)4);
			int num = 0;
			for (int i = 0; i < this.mText.Count; i++)
			{
				LeaderBoardText leaderBoardText = this.mText[i];
				int num2 = 0;
				int num3 = 0;
				if (leaderBoardText.mIcon == null && leaderBoardText.mShowIcon)
				{
					leaderBoardText.mIcon = Res.GetImageByID(ResID.IMAGE_UI_AVATAR);
				}
				if (leaderBoardText.mIcon != null)
				{
					num2 = leaderBoardText.mIcon.GetWidth() + 20;
					num3 = 35;
					g.DrawImage(leaderBoardText.mIcon, leaderBoardText.mX, leaderBoardText.mY);
				}
				if (leaderBoardText.mValueStr.Length == 0)
				{
					num++;
					g.SetFont(this.mHeaderFont);
					g.SetColor(255, 249, 161);
					if (num == 2)
					{
						if (leaderBoardText.mIcon == null)
						{
							num2 = 84;
						}
						int num4 = 30;
						Rect rect;
						rect = new Rect(num2 + leaderBoardText.mX + (int)this.mLeaderBoards.mXOff, leaderBoardText.mY + g.GetFont().GetAscent() - num4, 250, this.mStatsFont.GetHeight() * 2);
						g.WriteWordWrapped(rect, leaderBoardText.mHeaderStr, 25, -1);
					}
					else if (num == 3)
					{
						int num5 = 170;
						g.DrawString(leaderBoardText.mHeaderStr, num2 + leaderBoardText.mX + (int)this.mLeaderBoards.mXOff + num5 - g.GetFont().StringWidth(leaderBoardText.mHeaderStr), num3 + leaderBoardText.mY + g.GetFont().GetAscent());
					}
					else
					{
						g.DrawString(leaderBoardText.mHeaderStr, num2 + leaderBoardText.mX + (int)this.mLeaderBoards.mXOff, num3 + leaderBoardText.mY + g.GetFont().GetAscent());
					}
					if ((i + 1) % 3 == 0)
					{
						num = 0;
					}
				}
				else
				{
					g.SetFont(this.mStatsFont);
					g.SetColor(166, 158, 255);
					g.WriteString(leaderBoardText.mHeaderStr, num2 + leaderBoardText.mX + (int)this.mLeaderBoards.mXOff, num3 + leaderBoardText.mY + this.mStatsFont.GetAscent(), 0, 1);
					g.SetColor(89, 187, 149);
					g.WriteString(leaderBoardText.mValueStr, num2 + leaderBoardText.mX + Common._DS(Common._M(20)) + (int)this.mLeaderBoards.mXOff, num3 + leaderBoardText.mY + this.mStatsFont.GetAscent(), this.mWidth, -1);
				}
			}
			int num6 = this.NumPages() - 1;
			int num7 = this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES.GetWidth() + 30;
			int num8 = (this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES.GetHeight() - this.IMAGE_UI_CHALLENGESCREEN_DIVIDER.GetHeight()) / 2;
			for (int j = 0; j < num6; j++)
			{
				g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_DIVIDER, num7 - this.IMAGE_UI_CHALLENGESCREEN_DIVIDER.GetWidth(), num8);
				num7 += this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES.GetWidth() + 30;
			}
			graphics3D.SetMasking((Graphics3D.EMaskMode)0);
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x000A3E1C File Offset: 0x000A201C
		public override void Update()
		{
			float num = Common._M(10f);
			for (int i = 0; i < Common.size<LeaderBoardText>(this.mText); i++)
			{
				LeaderBoardText leaderBoardText = this.mText[i];
				if (leaderBoardText.mFadeIn && leaderBoardText.mAlpha < 255f)
				{
					this.MarkDirty();
					leaderBoardText.mAlpha += num;
					if (leaderBoardText.mAlpha > 255f)
					{
						leaderBoardText.mAlpha = 255f;
					}
				}
				else if (!leaderBoardText.mFadeIn && leaderBoardText.mAlpha > 0f)
				{
					this.MarkDirty();
					leaderBoardText.mAlpha -= num;
					if (leaderBoardText.mAlpha <= 0f)
					{
						this.mText.RemoveAt(i);
						i--;
					}
				}
			}
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x000A3EE8 File Offset: 0x000A20E8
		private void SetupLeaderboardsTextXLive(ref int theStartY, int page, bool update, object reader)
		{
		}

		public void UpdatePage(int page)
		{
			for (int i = 0; i < this.mNumLines; i++)
			{
				LeaderBoardText leaderBoardText = this.mText[i * 3];
				leaderBoardText.mHeaderStr = " " + (i + 1 + page * this.mNumLines);
				leaderBoardText.mValueStr = "";
				leaderBoardText = this.mText[i * 3 + 1];
				if (leaderBoardText.mIcon != null)
				{
					leaderBoardText.mIcon.Dispose();
				}
				leaderBoardText.mIcon = Res.GetImageByID(ResID.IMAGE_UI_ACHIEVEMENTS_FROGSTATUE);
				leaderBoardText.mHeaderStr = "KKKKKKKKKK ";
				leaderBoardText.mValueStr = "";
				leaderBoardText = this.mText[i * 3 + 2];
				leaderBoardText.mHeaderStr = "1000000";
				leaderBoardText.mValueStr = "";
			}
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x000A42F0 File Offset: 0x000A24F0
		private void SetupLeaderboardsText(ref int theStartY, int page, bool update)
		{
			int num = Common._DS(Common._M(80));
			int num2 = 40 + this.mStatsFont.mHeight;
			if (!update)
			{
				int num3 = Common._DS(Common._M(0));
				num3 += 45;
				for (int i = 0; i < this.mNumLines; i++)
				{
					this.mText.Add(new LeaderBoardText());
					LeaderBoardText leaderBoardText = Common.back<LeaderBoardText>(this.mText);
					leaderBoardText.mHeaderStr = " " + (i + 1 + page * this.mNumLines);
					leaderBoardText.mValueStr = "";
					leaderBoardText.mX = num;
					leaderBoardText.mY = num3 + theStartY;
					this.mText.Add(new LeaderBoardText());
					leaderBoardText = Common.back<LeaderBoardText>(this.mText);
					leaderBoardText.mIcon = Res.GetImageByID(ResID.IMAGE_UI_AVATAR);
					leaderBoardText.mHeaderStr = "KKKKKKKKKKkkkkkkkkkkkkkkk ";
					leaderBoardText.mValueStr = "";
					leaderBoardText.mX = num + 45;
					leaderBoardText.mY = num3 + theStartY;
					this.mText.Add(new LeaderBoardText());
					leaderBoardText = Common.back<LeaderBoardText>(this.mText);
					leaderBoardText.mHeaderStr = Common.CommaSeperate(1000000);
					leaderBoardText.mValueStr = "";
					leaderBoardText.mX = num + 380;
					leaderBoardText.mY = num3 + theStartY;
					num3 += num2;
				}
				return;
			}
			for (int j = 0; j < this.mNumLines; j++)
			{
				LeaderBoardText leaderBoardText = this.mText[j * 3];
				leaderBoardText.mHeaderStr = " " + (j + 1 + page * this.mNumLines);
				leaderBoardText.mValueStr = "";
				leaderBoardText = this.mText[j * 3 + 1];
				if (leaderBoardText.mIcon != null)
				{
					leaderBoardText.mIcon.Dispose();
				}
				leaderBoardText.mIcon = Res.GetImageByID(ResID.IMAGE_UI_ACHIEVEMENTS_FROGSTATUE);
				leaderBoardText.mHeaderStr = "KKKKKKKKKK ";
				leaderBoardText.mValueStr = "";
				leaderBoardText = this.mText[j * 3 + 2];
				leaderBoardText.mHeaderStr = Common.CommaSeperate(1000000);
				leaderBoardText.mValueStr = "";
			}
		}

		// Token: 0x04001A86 RID: 6790
		private List<LeaderBoardText> mText;

		// Token: 0x04001A87 RID: 6791
		private Font mHeaderFont;

		// Token: 0x04001A88 RID: 6792
		private Font mStatsFont;

		// Token: 0x04001A89 RID: 6793
		private LeaderBoards mLeaderBoards;

		// Token: 0x04001A8A RID: 6794
		public int mNumPages;

		// Token: 0x04001A8B RID: 6795
		public int mNumLines = 4;

		// Token: 0x04001A8C RID: 6796
		private Image IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES);

		// Token: 0x04001A8D RID: 6797
		private Image IMAGE_UI_CHALLENGESCREEN_DIVIDER = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_DIVIDER);

		// Token: 0x04001A8E RID: 6798
		private Image IMAGE_UI_AVATAR = Res.GetImageByID(ResID.IMAGE_UI_AVATAR);

		// Token: 0x04001A8F RID: 6799
		public Image IMAGE_UI_LEADERBOARDS_SHADOW = Res.GetImageByID(ResID.IMAGE_UI_LEADERBOARDS_SHADOW);

		// Token: 0x02000147 RID: 327
		private enum TextField
		{
			// Token: 0x04001A99 RID: 6809
			txtNumber,
			// Token: 0x04001A9A RID: 6810
			txtGameTag,
			// Token: 0x04001A9B RID: 6811
			txtGameScore,
			// Token: 0x04001A9C RID: 6812
			txtTotal
		}
	}
}
