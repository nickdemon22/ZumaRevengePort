using System;
using SexyFramework.AELib;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	// Token: 0x020000A1 RID: 161
	public class Tiki : IDisposable
	{
		// Token: 0x06000DB5 RID: 3509 RVA: 0x0008BD01 File Offset: 0x00089F01
		public Tiki()
		{
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x0008BD23 File Offset: 0x00089F23
		public Tiki(Tiki rhs) : this()
		{
			this.CopyFrom(rhs);
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x0008BD34 File Offset: 0x00089F34
		public void CopyFrom(Tiki rhs)
		{
			this.mUpdateCount = rhs.mUpdateCount;
			this.mCollRect = new Rect(rhs.mCollRect);
			this.mDoExplosion = rhs.mDoExplosion;
			this.mBoss = rhs.mBoss;
			this.mRailStartX = rhs.mRailStartX;
			this.mRailStartY = rhs.mRailStartY;
			this.mRailEndX = rhs.mRailEndX;
			this.mRailEndY = rhs.mRailEndY;
			this.mTravelTime = rhs.mTravelTime;
			this.mId = rhs.mId;
			this.mAlphaFadeDir = rhs.mAlphaFadeDir;
			this.mAlpha = rhs.mAlpha;
			this.mX = rhs.mX;
			this.mY = rhs.mY;
			this.mWasHit = rhs.mWasHit;
			this.mIsLeftTiki = rhs.mIsLeftTiki;
			this.mVX = rhs.mVX;
			this.mComp = rhs.mComp;
			this.mExplosion = rhs.mExplosion;
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x0008BE2A File Offset: 0x0008A02A
		public virtual void Dispose()
		{
			if (this.mExplosion != null)
			{
				this.mExplosion.Dispose();
				this.mExplosion = null;
			}
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x0008BE46 File Offset: 0x0008A046
		public void Init(Boss b)
		{
			this.mBoss = b;
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x0008BE50 File Offset: 0x0008A050
		public void Update()
		{
			if (this.mDoExplosion)
			{
				this.mExplosion.mDrawTransform.LoadIdentity();
				float num = GameApp.DownScaleNum(1f);
				this.mExplosion.mDrawTransform.Scale(num, num);
				this.mExplosion.mDrawTransform.Translate(Common._S(this.mX) + (float)Common._DS(Common._M(80)), Common._S(this.mY) + (float)Common._DS(Common._M1(150)));
				this.mExplosion.Update();
				if (this.mExplosion.mFrameNum > (float)this.mExplosion.mLastFrameNum)
				{
					this.mDoExplosion = false;
				}
			}
			this.mComp.Update();
			this.mAlpha += this.mAlphaFadeDir * Common._M(12);
			if (this.mAlpha < 0)
			{
				this.mAlpha = 0;
			}
			else if (this.mAlpha > 255)
			{
				this.mAlpha = 255;
			}
			if (!this.mDoExplosion && ((this.mVX > 0f && this.mX + (float)this.mCollRect.mX > (float)this.mRailEndX) || (this.mVX < 0f && this.mX + (float)this.mCollRect.mX < (float)this.mRailStartX)))
			{
				this.mX = (float)((this.mVX > 0f) ? (this.mRailEndX - this.mCollRect.mX) : (this.mRailStartX - this.mCollRect.mX));
				this.mVX *= -1f;
			}
			this.mX += this.mVX;
			this.mUpdateCount++;
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x0008C020 File Offset: 0x0008A220
		public void Draw(SexyGraphics g)
		{
			if (this.mAlpha > 0)
			{
				CumulativeTransform cumulativeTransform = new CumulativeTransform();
				cumulativeTransform.mOpacity = (float)this.mAlpha / 255f;
				if (this.mBoss != null && this.mBoss.mAlphaOverride <= 254f)
				{
					cumulativeTransform.mOpacity = this.mBoss.mAlphaOverride / 255f;
				}
				cumulativeTransform.mTrans.Translate(Common._S(this.mX - (float)this.mCollRect.mX), Common._S(this.mY - (float)this.mCollRect.mY));
				this.mComp.Draw(g, cumulativeTransform, -1, Common._DS(1f));
			}
			if (g.Is3D() && this.mDoExplosion)
			{
				if (this.mBoss != null && this.mBoss.mAlphaOverride <= 254f)
				{
					this.mExplosion.mColor.mAlpha = (int)(this.mBoss.mAlphaOverride / 255f);
				}
				this.mExplosion.Draw(g);
			}
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x0008C130 File Offset: 0x0008A330
		public void SetIsLeft(bool l)
		{
			this.mIsLeftTiki = l;
			this.mCollRect = new Rect(Common._M(74), Common._M1(70), Common._M2(75), Common._M4(104));
			this.mExplosion = null;
			this.mExplosion = (this.mIsLeftTiki ? GameApp.gApp.mResourceManager.GetPIEffect("PIEFFECT_NONRESIZE_CIRCLEEXPLOSIONTIKI").Duplicate() : GameApp.gApp.mResourceManager.GetPIEffect("PIEFFECT_NONRESIZE_TRIANGLEEXPLOSIONTIKI").Duplicate());
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x0008C1B4 File Offset: 0x0008A3B4
		public bool Collides(Bullet b, ref bool should_destroy)
		{
			Rect rect;
			rect = new Rect(this.mCollRect);
			rect.mX = (int)this.mX;
			rect.mY = (int)this.mY;
			Rect rect2;
			rect2 = new Rect((int)b.GetX() - b.GetRadius(), (int)b.GetY() - b.GetRadius(), b.GetRadius() * 2, b.GetRadius() * 2);
			should_destroy = false;
			if (this.mWasHit || this.mAlphaFadeDir < 0 || !rect.Intersects(rect2))
			{
				return false;
			}
			should_destroy = true;
			this.mWasHit = true;
			this.mAlphaFadeDir = -1;
			this.mDoExplosion = true;
			this.mExplosion.ResetAnim();
			return true;
		}

		// Token: 0x04001604 RID: 5636
		protected int mUpdateCount;

		// Token: 0x04001605 RID: 5637
		protected Rect mCollRect = default(Rect);

		// Token: 0x04001606 RID: 5638
		protected bool mDoExplosion;

		// Token: 0x04001607 RID: 5639
		protected Boss mBoss;

		// Token: 0x04001608 RID: 5640
		public int mRailStartX;

		// Token: 0x04001609 RID: 5641
		public int mRailStartY;

		// Token: 0x0400160A RID: 5642
		public int mRailEndX;

		// Token: 0x0400160B RID: 5643
		public int mRailEndY;

		// Token: 0x0400160C RID: 5644
		public int mTravelTime;

		// Token: 0x0400160D RID: 5645
		public int mId = -1;

		// Token: 0x0400160E RID: 5646
		public int mAlphaFadeDir = 1;

		// Token: 0x0400160F RID: 5647
		public int mAlpha;

		// Token: 0x04001610 RID: 5648
		public float mX;

		// Token: 0x04001611 RID: 5649
		public float mY;

		// Token: 0x04001612 RID: 5650
		public bool mWasHit;

		// Token: 0x04001613 RID: 5651
		public bool mIsLeftTiki;

		// Token: 0x04001614 RID: 5652
		public float mVX;

		// Token: 0x04001615 RID: 5653
		public Composition mComp;

		// Token: 0x04001616 RID: 5654
		public PIEffect mExplosion;
	}
}
