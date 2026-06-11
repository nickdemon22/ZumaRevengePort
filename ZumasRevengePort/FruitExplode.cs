using System;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	// Token: 0x02000074 RID: 116
	public class FruitExplode
	{
		// Token: 0x06000B93 RID: 2963 RVA: 0x0006CDA5 File Offset: 0x0006AFA5
		public FruitExplode(Board board)
		{
			this.mBoard = board;
			this.mAnim = null;
			this.Reset();
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x0006CDCC File Offset: 0x0006AFCC
		public virtual void Dispose()
		{
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x0006CDD0 File Offset: 0x0006AFD0
		public void Reset()
		{
			this.mDone = false;
			if (this.mBoard.mLevel == null || this.mBoard.mCurTreasure == null)
			{
				return;
			}
			switch (this.mBoard.mLevel.mZone)
			{
			case 1:
				this.mAnim = Res.GetPopAnimByID(ResID.POPANIM_NONRESIZE_PINEAPPLEMUSH);
				break;
			case 2:
				this.mAnim = Res.GetPopAnimByID(ResID.POPANIM_NONRESIZE_BANANAMUSH);
				break;
			case 3:
				this.mAnim = Res.GetPopAnimByID(ResID.POPANIM_NONRESIZE_COCOAMUSH);
				break;
			case 4:
				this.mAnim = Res.GetPopAnimByID(ResID.POPANIM_NONRESIZE_MANGOMUSH);
				break;
			case 5:
				this.mAnim = Res.GetPopAnimByID(ResID.POPANIM_NONRESIZE_COCONUTMUSH);
				break;
			case 6:
			case 7:
				this.mAnim = Res.GetPopAnimByID(ResID.POPANIM_NONRESIZE_ACORNMUSH);
				break;
			default:
				this.mAnim = null;
				break;
			}
			this.mAnim.Play("Main");
			int num = (int)Common._S((float)this.mBoard.mCurTreasure.x + ModVal.M(-130f));
			int num2 = (int)Common._S((float)this.mBoard.mCurTreasure.y + ModVal.M(-120f));
			this.mGlobalTranform.Reset();
			this.mGlobalTranform.Translate((float)num, (float)num2);
			this.mAnim.SetTransform(this.mGlobalTranform.GetMatrix());
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x0006CF30 File Offset: 0x0006B130
		public void Update()
		{
			if (this.mDone || this.mAnim == null || this.mBoard.mCurTreasure == null)
			{
				return;
			}
			int num = (int)Common._S((float)this.mBoard.mCurTreasure.x + ModVal.M(-130f));
			int num2 = (int)Common._S((float)this.mBoard.mCurTreasure.y + ModVal.M(-120f));
			this.mGlobalTranform.Reset();
			this.mGlobalTranform.Translate((float)num, (float)num2);
			this.mAnim.SetTransform(this.mGlobalTranform.GetMatrix());
			this.mAnim.Update();
			if (!this.mAnim.IsActive() || this.mAnim.mMainSpriteInst.mFrameNum >= (float)(this.mAnim.mMainSpriteInst.mDef.mFrames.Count - 1))
			{
				this.mDone = true;
			}
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x0006D01F File Offset: 0x0006B21F
		public void Draw(SexyGraphics g)
		{
			if (this.mAnim != null)
			{
				this.mAnim.Draw(g);
			}
		}

		// Token: 0x04001398 RID: 5016
		protected PopAnim mAnim;

		// Token: 0x04001399 RID: 5017
		protected Board mBoard;

		// Token: 0x0400139A RID: 5018
		protected Transform mGlobalTranform = new Transform();

		// Token: 0x0400139B RID: 5019
		public bool mDone;
	}
}
