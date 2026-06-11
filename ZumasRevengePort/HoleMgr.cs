using System;
using System.Linq;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	// Token: 0x02000081 RID: 129
	public class HoleMgr
	{
		// Token: 0x06000C90 RID: 3216 RVA: 0x0007F77E File Offset: 0x0007D97E
		public HoleMgr()
		{
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x0007F794 File Offset: 0x0007D994
		public HoleMgr(HoleMgr rhs)
		{
			if (rhs == null)
			{
				return;
			}
			for (int i = 0; i < 4; i++)
			{
				this.mHoles[i] = new HoleInfo(rhs.mHoles[i]);
			}
			this.mNumHoles = rhs.mNumHoles;
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x0007F7E4 File Offset: 0x0007D9E4
		protected void SetupHole(ref int x, ref int y, ref float rot)
		{
			x -= 48;
			y -= 48;
			while (rot < 0f)
			{
				rot += 6.28318f;
			}
			while (rot > 6.28318f)
			{
				rot -= 6.28318f;
			}
			if ((double)Math.Abs(rot) < 0.2)
			{
				rot = 0f;
				return;
			}
			if ((double)Math.Abs(rot - 1.570795f) < 0.2)
			{
				rot = 1.570795f;
				return;
			}
			if ((double)Math.Abs(rot - 3.14159f) < 0.2)
			{
				rot = 3.14159f;
				return;
			}
			if ((double)Math.Abs(rot - 4.712385f) < 0.2)
			{
				rot = 4.712385f;
				return;
			}
			if ((double)Math.Abs(rot - 6.28318f) < 0.2)
			{
				rot = 0f;
			}
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x0007F8C8 File Offset: 0x0007DAC8
		public int PlaceHole(int curve_num, int x, int y, float rot, bool visible)
		{
			this.SetupHole(ref x, ref y, ref rot);
			HoleInfo holeInfo = new HoleInfo();
			holeInfo.mX = x;
			holeInfo.mY = y;
			holeInfo.mFrame = 0;
			holeInfo.mRotation = rot;
			holeInfo.mPercentOpen = 0f;
			holeInfo.mVisible = visible;
			holeInfo.mCurve = null;
			holeInfo.mCurveNum = curve_num;
			this.mHoles[this.mNumHoles++] = holeInfo;
			return this.mNumHoles - 1;
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x0007F946 File Offset: 0x0007DB46
		public int PlaceHole(int curve_num, int x, int y, float rot)
		{
			return this.PlaceHole(curve_num, x, y, rot, true);
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x0007F954 File Offset: 0x0007DB54
		public void UpdateHoleInfo(int hole_index, int x, int y, float rot, bool visible)
		{
			HoleInfo holeInfo = this.mHoles[hole_index];
			this.SetupHole(ref x, ref y, ref rot);
			holeInfo.mX = x;
			holeInfo.mY = y;
			holeInfo.mRotation = rot;
			holeInfo.mVisible = visible;
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x0007F994 File Offset: 0x0007DB94
		public void UpdateHoleInfo(int hole_index, int x, int y, float rot)
		{
			this.UpdateHoleInfo(hole_index, x, y, rot, true);
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x0007F9A4 File Offset: 0x0007DBA4
		public void Update()
		{
			for (int i = 0; i < this.mNumHoles; i++)
			{
				HoleInfo holeInfo = this.mHoles[i];
				for (int j = 0; j < holeInfo.mShared.Count; j++)
				{
					HoleInfo holeInfo2 = this.mHoles[holeInfo.mShared[j]];
					if (holeInfo.GetPctOpen() > holeInfo2.GetPctOpen())
					{
						holeInfo2.SetPctOpen(holeInfo.GetPctOpen());
					}
				}
				holeInfo.Update();
			}
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x0007FA18 File Offset: 0x0007DC18
		public void SetPctOpen(int curve_num, float pct_open)
		{
			this.mHoles[curve_num].SetPctOpen(pct_open);
			HoleInfo holeInfo = this.mHoles[curve_num];
			if (!holeInfo.mVisible)
			{
				for (int i = 0; i < Enumerable.Count<int>(holeInfo.mShared); i++)
				{
					HoleInfo holeInfo2 = this.mHoles[holeInfo.mShared[i]];
					if (holeInfo.GetPctOpen() > holeInfo2.GetPctOpen())
					{
						holeInfo2.SetPctOpen(holeInfo.GetPctOpen());
					}
				}
			}
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x0007FA88 File Offset: 0x0007DC88
		public void Draw(SexyGraphics g)
		{
			float hilite_override = 0f;
			for (int i = 0; i < this.mNumHoles; i++)
			{
				if (!this.mHoles[i].mVisible && this.mHoles[i].mCurve != null && this.mHoles[i].mCurve.mInitialPathHilite)
				{
					hilite_override = this.mHoles[i].mCurve.mSkullHilite;
				}
			}
			for (int j = 0; j < this.mNumHoles; j++)
			{
				if (this.mHoles[j].mVisible)
				{
					this.mHoles[j].Draw(g, hilite_override);
				}
			}
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x0007FB20 File Offset: 0x0007DD20
		public void DrawRings(SexyGraphics g)
		{
			for (int i = 0; i < this.mNumHoles; i++)
			{
				if (this.mHoles[i].mVisible)
				{
					this.mHoles[i].DrawRings(g);
				}
			}
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x0007FB5B File Offset: 0x0007DD5B
		public int GetNumHoles()
		{
			return this.mNumHoles;
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x0007FB63 File Offset: 0x0007DD63
		public HoleInfo GetHole(int idx)
		{
			if (idx < 0 || idx >= this.mNumHoles)
			{
				return null;
			}
			return this.mHoles[idx];
		}

		// Token: 0x04001483 RID: 5251
		protected HoleInfo[] mHoles = new HoleInfo[4];

		// Token: 0x04001484 RID: 5252
		protected int mNumHoles;
	}
}
