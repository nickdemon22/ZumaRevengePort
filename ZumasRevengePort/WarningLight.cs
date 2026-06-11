using System;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	// Token: 0x020000AE RID: 174
	public class WarningLight
	{
		// Token: 0x06000DE8 RID: 3560 RVA: 0x0008D384 File Offset: 0x0008B584
		public WarningLight(float x, float y)
		{
			this.mX = x;
			this.mY = y;
			this.mAlpha = 0f;
			this.mUpdateCount = 0;
			this.mAngle = 0f;
			this.mState = 0;
			this.mWaypoint = -1f;
			this.mPulseAlpha = 0f;
			this.mPulseRate = 0f;
			this.mPriority = 0;
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x0008D3F4 File Offset: 0x0008B5F4
		public bool Update()
		{
			this.mUpdateCount++;
			float num = Common._M(5f);
			if (this.mState == 1)
			{
				this.mAlpha = Math.Min(255f, this.mAlpha + num);
				if (this.mAlpha >= 255f)
				{
					this.mState = 0;
				}
			}
			else if (this.mState == -1)
			{
				this.mAlpha = Math.Max(0f, this.mAlpha - num);
				if (this.mAlpha <= 0f)
				{
					this.mState = 0;
				}
			}
			else if (this.mPulseRate != 0f)
			{
				this.mPulseAlpha += ((this.mPulseRate > 0f) ? (this.mPulseRate * 2f) : this.mPulseRate);
				if (this.mPulseRate < 0f && this.mPulseAlpha <= 0f)
				{
					this.mPulseRate = 0f;
					this.mPulseAlpha = 0f;
				}
				else if (this.mPulseAlpha >= 255f && this.mPulseRate > 0f)
				{
					this.mPulseRate *= -1f;
					this.mPulseAlpha = 255f;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x0008D540 File Offset: 0x0008B740
		public void Draw(SexyGraphics g)
		{
			if (this.mAlpha == 0f)
			{
				return;
			}
			g.PushState();
			if (this.mAlpha != 0f)
			{
				g.SetColorizeImages(true);
				g.SetColor(255, 255, 255, (int)this.mAlpha);
			}
			Image imageByID = Res.GetImageByID(ResID.IMAGE_SKULL_PATH);
			g.DrawImageRotated(imageByID, (int)(Common._S(this.mX) - (float)(imageByID.mWidth / 2)), (int)(Common._S(this.mY) - (float)(imageByID.mHeight / 2)), (double)(this.mAngle + 1.570795f));
			if (this.mPulseAlpha != 0f)
			{
				Image imageByID2 = Res.GetImageByID(ResID.IMAGE_SKULL_PATH_LIT);
				g.SetColorizeImages(true);
				g.SetColor(255, 255, 255, (int)this.mPulseAlpha);
				g.DrawImageRotated(imageByID2, (int)(Common._S(this.mX) - (float)(imageByID2.mWidth / 2)), (int)(Common._S(this.mY) - (float)(imageByID2.mHeight / 2)), (double)(this.mAngle + 1.570795f));
			}
			g.SetColorizeImages(false);
			g.PopState();
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x0008D664 File Offset: 0x0008B864
		public void SyncState(DataSync sync)
		{
			sync.SyncFloat(ref this.mAlpha);
			sync.SyncFloat(ref this.mPulseAlpha);
			sync.SyncFloat(ref this.mPulseRate);
			sync.SyncLong(ref this.mState);
			sync.SyncLong(ref this.mUpdateCount);
		}

		// Token: 0x0400165B RID: 5723
		public float mX;

		// Token: 0x0400165C RID: 5724
		public float mY;

		// Token: 0x0400165D RID: 5725
		public float mAlpha;

		// Token: 0x0400165E RID: 5726
		public float mAngle;

		// Token: 0x0400165F RID: 5727
		public float mPulseAlpha;

		// Token: 0x04001660 RID: 5728
		public float mPulseRate;

		// Token: 0x04001661 RID: 5729
		public float mWaypoint;

		// Token: 0x04001662 RID: 5730
		public int mState;

		// Token: 0x04001663 RID: 5731
		public int mUpdateCount;

		// Token: 0x04001664 RID: 5732
		public int mPriority;
	}
}
