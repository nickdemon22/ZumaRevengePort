using System;
using System.Collections.Generic;
using System.Linq;
using JeffLib;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	// Token: 0x0200000D RID: 13
	public abstract class Boss : IDisposable
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000305 RID: 773 RVA: 0x0002AD7E File Offset: 0x00028F7E
		// (set) Token: 0x06000306 RID: 774 RVA: 0x0002AD8B File Offset: 0x00028F8B
		public int mWallDownTime
		{
			get
			{
				return this.mDWallDownTime.value;
			}
			set
			{
				this.mDWallDownTime.value = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000307 RID: 775 RVA: 0x0002AD99 File Offset: 0x00028F99
		// (set) Token: 0x06000308 RID: 776 RVA: 0x0002ADA6 File Offset: 0x00028FA6
		public float mHPDecPerHit
		{
			get
			{
				return this.mDHPDecPerHit.value;
			}
			set
			{
				this.mDHPDecPerHit.value = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000309 RID: 777 RVA: 0x0002ADB4 File Offset: 0x00028FB4
		// (set) Token: 0x0600030A RID: 778 RVA: 0x0002ADC1 File Offset: 0x00028FC1
		public float mHPDecPerProxBomb
		{
			get
			{
				return this.mDHPDecPerProxBomb.value;
			}
			set
			{
				this.mDHPDecPerProxBomb.value = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0002ADCF File Offset: 0x00028FCF
		// (set) Token: 0x0600030C RID: 780 RVA: 0x0002ADDC File Offset: 0x00028FDC
		public int mTikiHealthRespawnAmt
		{
			get
			{
				return this.mDTikiHealthRespawnAmt.value;
			}
			set
			{
				this.mDTikiHealthRespawnAmt.value = value;
			}
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0002ADEC File Offset: 0x00028FEC
		private void InitParamPointers()
		{
			Dictionary<string, ParamData<float>>.Enumerator enumerator = this.mFParamPointerMap.GetEnumerator();
			while (enumerator.MoveNext())
			{
				DDS gDDS = GameApp.gDDS;
				KeyValuePair<string, ParamData<float>> keyValuePair = enumerator.Current;
				if (gDDS.HasBossParam(keyValuePair.Key))
				{
					ParamData<float> paramData = new ParamData<float>();
					ParamData<float> paramData2 = paramData;
					DDS gDDS2 = GameApp.gDDS;
					KeyValuePair<string, ParamData<float>> keyValuePair2 = enumerator.Current;
					paramData2.value = gDDS2.GetBossParam(keyValuePair2.Key);
					Dictionary<string, ParamData<float>> dictionary = this.mFParamPointerMap;
					KeyValuePair<string, ParamData<float>> keyValuePair3 = enumerator.Current;
					dictionary[keyValuePair3.Key] = paramData;
				}
			}
			Dictionary<string, ParamData<int>>.Enumerator enumerator2 = this.mIParamPointerMap.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				DDS gDDS3 = GameApp.gDDS;
				KeyValuePair<string, ParamData<int>> keyValuePair4 = enumerator2.Current;
				if (gDDS3.HasBossParam(keyValuePair4.Key))
				{
					ParamData<int> paramData3 = new ParamData<int>();
					ParamData<int> paramData4 = paramData3;
					DDS gDDS4 = GameApp.gDDS;
					KeyValuePair<string, ParamData<int>> keyValuePair5 = enumerator2.Current;
					paramData4.value = (int)gDDS4.GetBossParam(keyValuePair5.Key);
					Dictionary<string, ParamData<int>> dictionary2 = this.mIParamPointerMap;
					KeyValuePair<string, ParamData<int>> keyValuePair6 = enumerator2.Current;
					dictionary2[keyValuePair6.Key] = paramData3;
				}
			}
			for (int i = 0; i < Common.size<BerserkTier>(this.mBerserkTiers); i++)
			{
				BerserkTier berserkTier = this.mBerserkTiers[i];
				for (int j = 0; j < Common.size<BerserkModifier>(berserkTier.mParams); j++)
				{
					BerserkModifier berserkModifier = berserkTier.mParams[j];
					string text = berserkModifier.mParamName.ToLower();
					if (this.mFParamPointerMap.ContainsKey(text))
					{
						berserkModifier.AddPointerFloat(this.mFParamPointerMap[text]);
					}
					if (this.mIParamPointerMap.ContainsKey(text))
					{
						berserkModifier.AddPointerInt(this.mIParamPointerMap[text]);
					}
					if (this.mBParamPointerMap.ContainsKey(text))
					{
						berserkModifier.AddPointerBool(this.mBParamPointerMap[text]);
					}
				}
			}
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0002AFBC File Offset: 0x000291BC
		protected virtual void DecHearts(int amount)
		{
			Image imageByID = Res.GetImageByID(ResID.IMAGE_BOSS_HEARTS);
			for (int i = 0; i < Boss.NUM_HEARTS; i++)
			{
				if (this.mHeartCels[i] < imageByID.mNumCols - 1)
				{
					int num = this.mHeartCels[i];
					this.mHeartCels[i] += amount;
					if (this.mHeartCels[i] <= imageByID.mNumCols - 1)
					{
						break;
					}
					this.mHeartCels[i] = imageByID.mNumCols - 1;
					amount -= this.mHeartCels[i] - num;
				}
			}
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0002B048 File Offset: 0x00029248
		protected virtual void ResetWallAndTikis(int wall_index)
		{
			if (this.mHP <= 0f)
			{
				return;
			}
			if (Enumerable.Count<BossWall>(this.mWalls) == Enumerable.Count<Tiki>(this.mTikis))
			{
				this.mTikis[wall_index].mWasHit = false;
				this.mTikis[wall_index].mAlphaFadeDir = 1;
				this.mWalls[wall_index].mAlphaFadeDir = 1;
			}
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0002B0B4 File Offset: 0x000292B4
		protected virtual bool DoHit(Bullet b, bool from_prox_bomb)
		{
			float mPrevHP = this.mHP;
			float num = from_prox_bomb ? this.mHPDecPerProxBomb : this.mHPDecPerHit;
			int amount = from_prox_bomb ? this.mHeartPieceDecAmtProxBomb : this.mHeartPieceDecAmt;
			if (num <= 0f)
			{
				return false;
			}
			this.mHP -= num;
			if (this.mTikiHealthRespawnAmt > 0 && this.CanDecTikiHealthSpawnAmt())
			{
				this.mCurrTikiBossHealthRemoved += (int)num;
				if (this.mCurrTikiBossHealthRemoved >= this.mTikiHealthRespawnAmt)
				{
					this.mCurrTikiBossHealthRemoved = 0;
					for (int i = 0; i < Enumerable.Count<BossWall>(this.mWalls); i++)
					{
						this.ResetWallAndTikis(i);
					}
				}
			}
			if (this.mHP <= 0f)
			{
				this.mHP = 0f;
				this.mDeathTimer = 0;
				this.PlaySound(0);
				this.mApp.GetBoard().BossDied();
			}
			else
			{
				this.PlaySound(3);
			}
			this.mDoExplosion = true;
			if (this.mAllowCompacting)
			{
				this.mNeedsCompacting = true;
			}
			this.DecHearts(amount);
			if (this.mHP > 0f)
			{
				this.CheckIfShouldGoBerserk(mPrevHP);
			}
			else
			{
				this.mTauntQueue.Clear();
			}
			return true;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0002B1D4 File Offset: 0x000293D4
		protected virtual bool CompactCurves()
		{
			for (int i = 0; i < this.mLevel.mNumCurves; i++)
			{
				if (!this.mLevel.mCurveMgr[i].CanCompact())
				{
					return false;
				}
			}
			for (int j = 0; j < this.mLevel.mNumCurves; j++)
			{
				this.mLevel.mCurveMgr[j].CompactCurve(false);
			}
			return true;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0002B238 File Offset: 0x00029438
		protected virtual void DrawHearts(SexyGraphics g)
		{
			if (this.mHP <= 0f || this.mDoDeathExplosions || this.mLevel.mBoard.DoingBossIntro())
			{
				return;
			}
			g.PushState();
			if (this.mAlphaOverride <= 254f)
			{
				g.SetColor(255, 255, 255, (int)this.mAlphaOverride);
				g.SetColorizeImages(true);
			}
			Image imageByID = Res.GetImageByID(ResID.IMAGE_BOSS_HEARTS);
			for (int i = 0; i < Boss.NUM_HEARTS; i++)
			{
				g.DrawImageCel(imageByID, (int)(Common._S(this.mX + (float)this.mHeartXOff) + (float)(i * imageByID.GetCelWidth())), (int)Common._S(this.mY + (float)this.mHeartYOff), this.mHeartCels[i]);
			}
			g.PopState();
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0002B308 File Offset: 0x00029508
		protected virtual void DrawMisc(SexyGraphics g)
		{
			if (this.mHP > 0f && !this.mDoDeathExplosions && !this.mLevel.mBoard.DoingBossIntro())
			{
				for (int i = 0; i < Common.size<Tiki>(this.mTikis); i++)
				{
					this.mTikis[i].Draw(g);
				}
				this.DrawWalls(g);
			}
			Font fontByID = Res.GetFontByID(ResID.FONT_SHAGLOUNGE28_STROKE);
			if (Boss.gBerserkTextAlpha > 0f)
			{
				g.SetFont(fontByID);
				g.SetColor(Common._M(255), Common._M1(0), Common._M2(0), (int)Boss.gBerserkTextAlpha);
				string @string = TextManager.getInstance().getString(150);
				int num = g.GetFont().StringWidth(@string);
				g.DrawString(@string, (this.mApp.mWidth - num) / 2, (int)Boss.gBerserkTextY);
			}
			if (Boss.gImpatientTextAlpha > 0f)
			{
				g.SetFont(fontByID);
				g.SetColor(Common._M(0), Common._M1(0), Common._M2(0), (int)Boss.gImpatientTextAlpha);
				string string2 = TextManager.getInstance().getString(151);
				int num2 = g.GetFont().StringWidth(string2);
				g.DrawString(string2, (this.mApp.mWidth - num2) / 2, (int)Boss.gImpatientTextY);
			}
			if (this.mHP <= 0f && this.mBandagedImg != null)
			{
				g.SetColorizeImages(true);
				g.SetColor(255, 255, 255, 255 - (int)this.mAlphaOverride);
				g.DrawImage(this.mBandagedImg, (int)(Common._S(this.mX) - (float)(this.mBandagedImg.mWidth / 2) + (float)this.mShakeXOff + (float)Common._S(this.mBandagedXOff)), (int)(Common._S(this.mY) - (float)(this.mBandagedImg.mHeight / 2) + (float)this.mShakeYOff + (float)Common._S(this.mBandagedYOff)));
				g.SetColorizeImages(false);
			}
			if (this.mShouldDoDeathExplosions)
			{
				for (int j = 0; j < Common.size<PIEffect>(this.mDeathExplosions); j++)
				{
					PIEffect pieffect = this.mDeathExplosions[j];
					pieffect.mDrawTransform.LoadIdentity();
					float num3 = GameApp.DownScaleNum(1f);
					pieffect.mDrawTransform.Scale(num3, num3);
					pieffect.mDrawTransform.Translate(Common._S(this.mX), Common._S(this.mY));
					pieffect.Draw(g);
				}
			}
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0002B589 File Offset: 0x00029789
		protected virtual bool BulletIntersectsBoss(Bullet b)
		{
			return MathUtils.CirclesIntersect(b.GetX(), b.GetY(), this.mX, this.mY + (float)this.mBossRadiusYOff, (float)(this.mBossRadius + b.GetRadius()));
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0002B5BE File Offset: 0x000297BE
		protected void AddParamPointer(string p, ParamData<float> v)
		{
			this.mFParamPointerMap[p.ToLower()] = v;
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0002B5D2 File Offset: 0x000297D2
		protected void AddParamPointer(string p, ParamData<int> v)
		{
			this.mIParamPointerMap[p.ToLower()] = v;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0002B5E6 File Offset: 0x000297E6
		protected void AddParamPointer(string p, ParamData<bool> v)
		{
			this.mBParamPointerMap[p.ToLower()] = v;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0002B5FC File Offset: 0x000297FC
		protected void CheckIfShouldGoBerserk(float mPrevHP)
		{
			foreach (BerserkTier berserkTier in this.mBerserkTiers)
			{
				if (mPrevHP >= (float)berserkTier.mHealthLimit && this.mHP < (float)berserkTier.mHealthLimit)
				{
					for (int i = 0; i < Enumerable.Count<BerserkModifier>(berserkTier.mParams); i++)
					{
						berserkTier.mParams[i].ModifyVariable();
					}
					this.BerserkActivated(berserkTier.mHealthLimit);
					this.ReInit();
					break;
				}
			}
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0002B69C File Offset: 0x0002989C
		protected virtual void ReInit()
		{
			this.mHeartPieceDecAmt = (int)((float)(Boss.NUM_HEARTS * 4) / (this.mMaxHP / this.mHPDecPerHit));
			this.mHeartPieceDecAmtProxBomb = (int)((float)(Boss.NUM_HEARTS * 4) / (this.mMaxHP / this.mHPDecPerProxBomb));
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0002B6D8 File Offset: 0x000298D8
		protected virtual void BerserkActivated(int health_limit)
		{
			Boss.gBerserkTextAlpha = 255f;
			Boss.gBerserkTextY = (float)(this.mApp.mHeight / 2);
			this.mIsBerserk = true;
			this.PlaySound(1);
			foreach (HulaEntry hulaEntry in this.mHulaEntryVec)
			{
				if (hulaEntry.mBerserkAmt == health_limit)
				{
					this.mCurrentHulaEntry = hulaEntry;
					break;
				}
			}
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0002B764 File Offset: 0x00029964
		protected virtual void BallEaten(Bullet b)
		{
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0002B766 File Offset: 0x00029966
		protected virtual bool CanSpawnHulaDancers()
		{
			return true;
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0002B769 File Offset: 0x00029969
		protected virtual void DrawWalls(SexyGraphics g)
		{
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0002B76B File Offset: 0x0002996B
		protected virtual Rect GetWallRect(BossWall w)
		{
			return new Rect(w.mX, w.mY, w.mWidth, w.mHeight);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0002B78C File Offset: 0x0002998C
		protected virtual bool CollidesWithWall(Bullet b)
		{
			float num = (float)b.GetRadius() * 0.75f;
			Rect rect;
			rect = new Rect((int)(b.GetX() - num), (int)(b.GetY() - num), (int)(num * 2f), (int)(num * 2f));
			foreach (BossWall bossWall in this.mWalls)
			{
				if (bossWall.mAlphaFadeDir >= 0 && this.GetWallRect(bossWall).Intersects(rect))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0002B834 File Offset: 0x00029A34
		protected virtual bool CanDecTikiHealthSpawnAmt()
		{
			return true;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0002B837 File Offset: 0x00029A37
		protected virtual bool CanTaunt()
		{
			return true;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0002B83A File Offset: 0x00029A3A
		protected virtual void TikiHit(int idx)
		{
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0002B83C File Offset: 0x00029A3C
		public Boss() : this(null)
		{
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0002B848 File Offset: 0x00029A48
		public Boss(Level l)
		{
			this.mX = 0f;
			this.mY = 0f;
			this.mMaxHP = 0f;
			this.mHP = 0f;
			this.mWidth = 101;
			this.mHeight = 78;
			this.mUpdateCount = 0;
			this.mHPDecPerHit = 0f;
			this.mHPDecPerProxBomb = 0f;
			this.mLevel = l;
			this.mShakeXAmt = 0;
			this.mShakeYAmt = 0;
			this.mShouldDoDeathExplosions = true;
			this.mShakeXOff = 0;
			this.mShakeYOff = 0;
			this.mAllowLevelDDS = false;
			this.mDoExplosion = false;
			this.mNeedsCompacting = false;
			this.mAllowCompacting = false;
			this.mHeartXOff = 0;
			this.mHeartYOff = 150;
			this.mResetWallTimerOnTikiHit = false;
			this.mResetWallsOnBossHit = false;
			this.mWallDownTime = 0;
			this.mCurWallDownTime = 0;
			this.mStunTime = 0;
			this.mCurrTikiBossHealthRemoved = 0;
			this.mTikiHealthRespawnAmt = 0;
			this.mNum = 0;
			this.mIsBerserk = false;
			this.mApp = GameApp.gApp;
			this.mEatsBalls = false;
			this.mImpatientTimer = -1;
			this.mBombFreqMax = 0;
			this.mBombFreqMin = 0;
			this.mBombDuration = 0;
			this.mProxBombRadius = 80;
			this.mDrawRadius = false;
			this.mBossRadius = 70;
			this.mNeedsIntroSound = false;
			this.mBombInRange = false;
			this.mRadiusColorChangeMode = 1;
			this.mDoDeathExplosions = false;
			this.mDeathTimer = 0;
			this.mWordBubbleTimer = 300;
			this.mSepiaImage = null;
			this.mDeathTX = 0f;
			this.mDeathTY = 0f;
			this.mDeathVX = 0f;
			this.mDeathVY = 0f;
			this.mExplosionRate = 4;
			this.mBossRadiusYOff = 0;
			this.mHulaAmnesty = 0;
			this.mBandagedImg = null;
			this.mAlphaOverride = 255f;
			this.mBandagedXOff = 0;
			this.mBandagedYOff = 0;
			this.mDrawDeathBGTikis = true;
			this.mTauntTextYOff = 0;
			Boss.gBerserkTextAlpha = 0f;
			Boss.gBerserkTextY = 0f;
			Boss.gImpatientTextAlpha = 0f;
			Boss.gImpatientTextY = 0f;
			this.mResPrefix = "IMAGE_";
			this.mHitEffect = null;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0002BB84 File Offset: 0x00029D84
		public virtual void Dispose()
		{
			this.mSepiaImage = null;
			for (int i = 0; i < Common.size<HulaDancer>(this.mHulaDancers); i++)
			{
				this.mHulaDancers[i] = null;
			}
			for (int j = 0; j < this.mDeathExplosions.Count; j++)
			{
				if (this.mDeathExplosions[j] != null)
				{
					this.mDeathExplosions[j].Dispose();
				}
			}
			this.mDeathExplosions.Clear();
			if (this.mHitEffect != null)
			{
				this.mHitEffect.Dispose();
				this.mHitEffect = null;
			}
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0002BC18 File Offset: 0x00029E18
		public void AddTiki(int x, int y, int id, int rail_w, int rail_h, int travel_time)
		{
			Tiki tiki = new Tiki();
			this.mTikis.Add(tiki);
			tiki.mId = id;
			tiki.mX = (float)x;
			tiki.mY = (float)y;
			tiki.mRailStartX = x;
			tiki.mRailStartY = y;
			tiki.mRailEndX = x + rail_w;
			tiki.mRailEndY = y + rail_h;
			tiki.mTravelTime = travel_time;
			if (travel_time != 0)
			{
				tiki.mVX = (float)(tiki.mRailEndX - tiki.mRailStartX) / (float)travel_time;
			}
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0002BC94 File Offset: 0x00029E94
		public void AddTiki(int x, int y, int id)
		{
			this.AddTiki(x, y, id, 0, 0, 0);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0002BCA4 File Offset: 0x00029EA4
		public void AddWall(int x, int y, int w, int h, int id)
		{
			BossWall bossWall = new BossWall();
			bossWall.mX = x;
			bossWall.mY = y;
			bossWall.mWidth = w;
			bossWall.mHeight = h;
			bossWall.mId = id;
			bossWall.mAlphaFadeDir = 1;
			bossWall.mAlpha = 0;
			this.mWalls.Add(bossWall);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0002BCF6 File Offset: 0x00029EF6
		public List<BossWall> getWalls()
		{
			return this.mWalls;
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0002BD00 File Offset: 0x00029F00
		public void ForceNextTauntText()
		{
			this.mTauntQueue.Clear();
			Boss.FNTT_last_idx = (Boss.FNTT_last_idx + 1) % Enumerable.Count<TauntText>(this.mTauntText);
			if (Boss.FNTT_last_idx > Enumerable.Count<TauntText>(this.mTauntText))
			{
				Boss.FNTT_last_idx = 0;
			}
			this.mTauntQueue.Add(this.mTauntText[Boss.FNTT_last_idx]);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0002BD64 File Offset: 0x00029F64
		public virtual void Init(Level l)
		{
			this.mMaxHP = (this.mHP = 100f);
			if (l != null)
			{
				this.mLevel = l;
				for (int i = 0; i < Common.size<Tiki>(this.mTikis); i++)
				{
					this.mTikis[i].Init(this);
				}
			}
			if (this.mResGroup.Length > 0 && !this.mApp.mResourceManager.IsGroupLoaded(this.mResGroup) && !this.mApp.mResourceManager.LoadResources(this.mResGroup))
			{
				this.mApp.ShowResourceError(true);
				this.mApp.Shutdown();
				return;
			}
			if (!this.mApp.mResourceManager.IsGroupLoaded("Bosses") && !this.mApp.mResourceManager.LoadResources("Bosses"))
			{
				this.mApp.ShowResourceError(true);
				this.mApp.Shutdown();
				return;
			}
			if (this.mNum == 6 && !this.mApp.mResourceManager.IsGroupLoaded("Boss6Common") && !this.mApp.mResourceManager.LoadResources("Boss6Common"))
			{
				this.mApp.ShowResourceError(true);
				this.mApp.Shutdown();
				return;
			}
			this.mHitEffect = Res.GetPIEffectByID(ResID.PIEFFECT_NONRESIZE_DEATH_EXPLOSION).Duplicate();
			Common.SetFXNumScale(this.mHitEffect, this.mApp.Is3DAccelerated() ? 1f : Common._M(0.25f));
			Image imageByID = Res.GetImageByID(ResID.IMAGE_BOSS_HEARTS);
			this.ReInit();
			for (int j = 0; j < Boss.NUM_HEARTS; j++)
			{
				this.mHeartCels[j] = imageByID.mNumCols - 1;
			}
			this.InitParamPointers();
			if (Common.size<BossWall>(this.mWalls) == Common.size<Tiki>(this.mTikis))
			{
				for (int k = 0; k < Common.size<BossWall>(this.mWalls); k++)
				{
					BossWall bossWall = this.mWalls[k];
					bossWall.mAlphaFadeDir = 1;
					bossWall.mAlpha = 0;
				}
			}
			if (Common.size<Tiki>(this.mTikis) == 2)
			{
				this.mTikis[0].SetIsLeft(this.mTikis[0].mX < this.mTikis[1].mX);
				this.mTikis[1].SetIsLeft(this.mTikis[1].mX < this.mTikis[0].mX);
			}
			this.mSounds[6] = -1;
			this.mSounds[7] = -1;
			this.mSounds[8] = -1;
			this.mSounds[9] = -1;
			if (this.mNum < 6)
			{
				this.mSounds[0] = this.mApp.mResourceManager.LoadSound("SOUND_BOSS" + this.mNum + "_DIE");
				this.mSounds[1] = this.mApp.mResourceManager.LoadSound("SOUND_BOSS" + this.mNum + "_ENRAGE");
				this.mSounds[2] = this.mApp.mResourceManager.LoadSound("SOUND_BOSS" + this.mNum + "_FIRE");
				this.mSounds[3] = this.mApp.mResourceManager.LoadSound("SOUND_BOSS" + this.mNum + "_HIT");
				this.mSounds[4] = this.mApp.mResourceManager.LoadSound("SOUND_BOSS" + this.mNum + "_PLAYER_HIT");
				this.mSounds[5] = this.mApp.mResourceManager.LoadSound("SOUND_BOSS" + this.mNum + "_INTRO");
				if (this.mNum == 4)
				{
					this.mSounds[6] = this.mApp.mResourceManager.LoadSound("SOUND_BOSS4_EAT_BALL");
					this.mSounds[8] = this.mApp.mResourceManager.LoadSound("SOUND_BOSS4_TELEPORT");
				}
				else if (this.mNum == 1)
				{
					this.mSounds[7] = this.mApp.mResourceManager.LoadSound("SOUND_BOSS1_ROAR");
				}
				else if (this.mNum == 5)
				{
					this.mSounds[9] = this.mApp.mResourceManager.LoadSound("SOUND_BOSS5_SHIELD_HIT");
				}
			}
			else
			{
				this.mSounds[0] = this.mApp.mResourceManager.LoadSound("SOUND_BOSS_DIE" + (1 + Common.Rand() % 3));
				this.mSounds[1] = -1;
				this.mSounds[2] = this.mApp.mResourceManager.LoadSound("SOUND_BOSS_FIRE");
				this.mSounds[3] = this.mApp.mResourceManager.LoadSound("SOUND_BOSS_HIT" + (1 + Common.Rand() % 4));
				this.mSounds[4] = this.mApp.mResourceManager.LoadSound("SOUND_BULLET_HIT");
				this.mSounds[5] = this.mApp.mResourceManager.LoadSound("SOUND_BOSS_INTRO" + Common.Rand() % 3);
			}
			for (int m = 0; m < Common.size<HulaEntry>(this.mHulaEntryVec); m++)
			{
				if (this.mHulaEntryVec[m].mBerserkAmt >= 100)
				{
					this.mCurrentHulaEntry = this.mHulaEntryVec[m];
					break;
				}
			}
			int num = -1;
			for (int n = 0; n < Common.size<TauntText>(this.mTauntText); n++)
			{
				if (this.mTauntText[n].mMinDeaths <= this.mApp.mUserProfile.GetAdvModeVars().mNumDeathsCurLevel && this.mTauntText[n].mMinDeaths > num)
				{
					num = this.mTauntText[n].mMinDeaths;
				}
			}
			for (int num2 = 0; num2 < Common.size<TauntText>(this.mTauntText); num2++)
			{
				TauntText tauntText = this.mTauntText[num2];
				if (tauntText.mCondition == 0 && tauntText.mMinDeaths == num)
				{
					this.mTauntQueue.Add(tauntText);
				}
			}
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0002C3AC File Offset: 0x0002A5AC
		public virtual void Update(float f)
		{
			if (this.mHP <= 0f || this.mLevel.mBoard.GetGameState() == GameState.GameState_Losing)
			{
				float num = (this.mHP <= 0f) ? Common._M(1f) : Common._M1(3f);
				this.mAlphaOverride -= num;
				if (this.mAlphaOverride < 0f)
				{
					this.mAlphaOverride = 0f;
				}
			}
			else if (this.mLevel.DoingInitialPathHilite() && this.mLevel.mBoard.GetGameState() != GameState.GameState_BossIntro && this.mUpdateCount % Common._M(8) == 0 && this.mCleanHeart)
			{
				for (int i = 0; i < Boss.NUM_HEARTS; i++)
				{
					if (this.mHeartCels[i] != 0)
					{
						this.mHeartCels[i]--;
						break;
					}
				}
			}
			if (this.mAlphaOverride < 255f && this.mLevel.mBoard.GetGameState() != GameState.GameState_Losing && this.mLevel.mBoard.GetGameState() != GameState.GameState_BossDead)
			{
				this.mAlphaOverride += Common._M(3f);
				if (this.mAlphaOverride > 255f)
				{
					this.mAlphaOverride = 255f;
				}
			}
			this.mUpdateCount++;
			if (this.mDoExplosion)
			{
				this.mHitEffect.Update();
				if (!this.mHitEffect.IsActive())
				{
					this.mHitEffect.ResetAnim();
					this.mDoExplosion = false;
				}
			}
			if (this.mCurWallDownTime > 0 && --this.mCurWallDownTime == 0)
			{
				for (int j = 0; j < Common.size<BossWall>(this.mWalls); j++)
				{
					this.ResetWallAndTikis(j);
				}
			}
			if (this.mWordBubbleTimer > 0 && !this.mLevel.mBoard.DoingBossIntro())
			{
				this.mWordBubbleTimer--;
			}
			if (this.mDoDeathExplosions && this.mShouldDoDeathExplosions && this.mHP <= 0f && this.mUpdateCount % Common._M(25) == 0)
			{
				PIEffect pieffect = Res.GetPIEffectByID(ResID.PIEFFECT_NONRESIZE_DEATH_EXPLOSION).Duplicate();
				this.mDeathExplosions.Add(pieffect);
				Common.SetFXNumScale(pieffect, this.mApp.Is3DAccelerated() ? 1f : Common._M(0.25f));
				SexyTransform2D sexyTransform2D;
				sexyTransform2D = new SexyTransform2D(false);
				sexyTransform2D.Translate((float)Common._S(-this.mWidth / 3 + Common.Rand() % (int)((double)this.mWidth / 1.5)), (float)Common._S(-this.mHeight / 3 + Common.Rand() % (int)((double)this.mHeight / 1.5)));
				pieffect.mEmitterTransform.CopyFrom(sexyTransform2D);
			}
			for (int k = 0; k < Common.size<PIEffect>(this.mDeathExplosions); k++)
			{
				PIEffect pieffect2 = this.mDeathExplosions[k];
				pieffect2.Update();
				if (!pieffect2.IsActive())
				{
					pieffect2.Dispose();
					this.mDeathExplosions.RemoveAt(k);
					k--;
				}
			}
			for (int l = 0; l < Common.size<TauntText>(this.mTauntQueue); l++)
			{
				TauntText tauntText = this.mTauntQueue[l];
				tauntText.mUpdateCount++;
				if (tauntText.mUpdateCount < tauntText.mDelay)
				{
					break;
				}
				this.mTauntQueue.RemoveAt(l);
				l--;
			}
			if (Common.size<TauntText>(this.mTauntQueue) == 0 && this.mApp.GetLevelMgr().mBossTauntChance > 0 && this.CanTaunt() && Common._geq(this.mAlphaOverride, 255f))
			{
				List<int> list = new List<int>();
				for (int m = 0; m < Common.size<TauntText>(this.mTauntText); m++)
				{
					TauntText tauntText2 = this.mTauntText[m];
					if (this.mUpdateCount > tauntText2.mMinTime && Common.Rand() % this.mApp.GetLevelMgr().mBossTauntChance == 0 && (tauntText2.mCondition != 1 || (Common._eq(this.mHP, this.mMaxHP) && tauntText2.mCondition != 0)) && (tauntText2.mMinDeaths < 0 || tauntText2.mMinDeaths == this.mApp.mUserProfile.GetAdvModeVars().mNumDeathsCurLevel))
					{
						list.Add(m);
					}
				}
				if (Common.size<int>(list) > 0)
				{
					this.mTauntQueue.Add(this.mTauntText[list[Common.Rand() % Common.size<int>(list)]]);
				}
			}
			if (this.mDoExplosion || this.mDoDeathExplosions)
			{
				this.mShakeXOff = Common.IntRange(0, this.mShakeXAmt);
				this.mShakeYOff = Common.IntRange(0, this.mShakeYAmt);
			}
			if (Boss.gBerserkTextAlpha > 0f)
			{
				Boss.gBerserkTextAlpha -= Common._M(1f);
				Boss.gBerserkTextY -= Common._M(1f);
			}
			if (Boss.gImpatientTextAlpha > 0f)
			{
				Boss.gImpatientTextAlpha -= Common._M(1f);
				Boss.gImpatientTextY -= Common._M(1f);
			}
			if (this.mLevel.mBoard.DoingBossIntro())
			{
				return;
			}
			if (this.mHP <= 0f)
			{
				if ((!this.mLevel.mFinalLevel || !this.mLevel.mBoard.mAdventureWinScreen) && Boss.last_idx >= 4)
				{
					Boss.last_idx = 0;
				}
				if (!this.mDoDeathExplosions)
				{
					for (int n = 0; n < Common.size<BossText>(this.mDeathText); n++)
					{
						BossText bossText = this.mDeathText[n];
						if (bossText.mAlpha < 255f)
						{
							bool flag = n == Common.size<BossText>(this.mDeathText) - 1 && bossText.mAlpha < 255f;
							bossText.mAlpha = Math.Min(255f, bossText.mAlpha + 3f);
							if (flag && bossText.mAlpha >= 255f)
							{
								this.mApp.SetCursor((ECURSOR)1);
							}
						}
						if (bossText.mAlpha < (float)Common._M(200))
						{
							break;
						}
					}
				}
				this.mX += this.mDeathVX;
				this.mY += this.mDeathVY;
				if ((this.mDeathVX > 0f && this.mX >= this.mDeathTX) || (this.mDeathVX < 0f && this.mX <= this.mDeathTX))
				{
					this.mX = this.mDeathTX;
					this.mDeathVX = 0f;
				}
				if ((this.mDeathVY > 0f && this.mY >= this.mDeathTY) || (this.mDeathVY < 0f && this.mY <= this.mDeathTY))
				{
					this.mY = this.mDeathTY;
					this.mDeathVY = 0f;
				}
				return;
			}
			bool flag2 = this.mLevel.AllCurvesAtRolloutPoint();
			if (this.mNeedsIntroSound && flag2 && !this.mApp.GetBoard().DoingIntros())
			{
				this.mNeedsIntroSound = false;
				this.PlaySound(5);
			}
			if (this.IsStunned())
			{
				this.mStunTime--;
			}
			if (this.mNeedsCompacting && !this.IsStunned() && this.CompactCurves())
			{
				this.mNeedsCompacting = false;
			}
			if (this.mHulaAmnesty > 0)
			{
				this.mHulaAmnesty--;
			}
			else if (this.mCurrentHulaEntry.mSpawnRate > 0 && this.mUpdateCount % this.mCurrentHulaEntry.mSpawnRate == 0 && Common._geq(this.mAlphaOverride, 255f) && this.CanSpawnHulaDancers())
			{
				HulaDancer hulaDancer = new HulaDancer();
				this.mHulaDancers.Add(hulaDancer);
				bool has_proj = Common.Rand() % 100 < this.mCurrentHulaEntry.mProjChance;
				hulaDancer.Setup(has_proj, (float)this.mCurrentHulaEntry.mSpawnY, this.mCurrentHulaEntry.mProjVY);
			}
			for (int num2 = 0; num2 < Common.size<HulaDancer>(this.mHulaDancers); num2++)
			{
				HulaDancer hulaDancer2 = this.mHulaDancers[num2];
				if (!Common._eq(this.mAlphaOverride, 255f))
				{
					hulaDancer2.mFadeOut = true;
				}
				hulaDancer2.Update(this.mCurrentHulaEntry.mVX);
				if (hulaDancer2.CanRemove())
				{
					this.mHulaDancers[num2].Dispose();
					this.mHulaDancers.RemoveAt(num2);
					num2--;
				}
				else if (hulaDancer2.ProjectileCollided(this.mLevel.mFrog.GetRect()))
				{
					if (!this.mLevel.mFrog.IsFuckedUp())
					{
						this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_NEW_SLOW));
						switch (this.mCurrentHulaEntry.mAttackType)
						{
						case 1:
							this.mLevel.mFrog.Stun(this.mCurrentHulaEntry.mAttackTime);
							break;
						case 2:
							this.mLevel.mFrog.Poison(this.mCurrentHulaEntry.mAttackTime);
							break;
						case 3:
							this.mLevel.mBoard.SetHallucinateTimer(this.mCurrentHulaEntry.mAttackTime);
							break;
						case 4:
							this.mLevel.mFrog.SetSlowTimer(this.mCurrentHulaEntry.mAttackTime);
							break;
						}
					}
					hulaDancer2.DestroyBullet();
				}
				else if (!hulaDancer2.HasFired() && this.CanSpawnHulaDancers() && hulaDancer2.GetX() > (float)(this.mLevel.mFrog.GetCenterX() + this.mCurrentHulaEntry.mProjRange))
				{
					hulaDancer2.Fire();
				}
			}
			if (this.mImpatientTimer > 0 && flag2 && --this.mImpatientTimer == 0)
			{
				Boss.gImpatientTextAlpha = 255f;
				Boss.gImpatientTextY = (float)(this.mApp.mHeight / 2);
			}
			if (this.mDrawRadius && flag2)
			{
				this.mBombInRange = false;
				int num3 = this.mProxBombRadius + 56 + Common.GetDefaultBallRadius();
				int num4 = num3 * num3;
				int num5 = 0;
				while (num5 < this.mLevel.mNumCurves && !this.mBombInRange)
				{
					for (int num6 = 0; num6 < this.mLevel.mCurveMgr[num5].mBallList.Count; num6++)
					{
						Ball ball = this.mLevel.mCurveMgr[num5].mBallList[num6];
						if (ball.GetPowerOrDestType(false) == PowerType.PowerType_ProximityBomb)
						{
							if ((this.mRadiusColorChangeMode != 2 || ball.GetY() > this.mY - (float)(this.mHeight / 2) + (float)Common._M(0)) && Common.Distance(ball.GetX(), ball.GetY(), this.mX, this.mY, false) <= (float)num4)
							{
								ball.mDoBossPulse = true;
								this.mBombInRange = true;
							}
							else
							{
								ball.mDoBossPulse = false;
							}
						}
					}
					num5++;
				}
			}
			if (this.IsImpatient())
			{
				for (int num7 = 0; num7 < this.mLevel.mNumCurves; num7++)
				{
					this.mLevel.mCurveMgr[num7].mSpeedScale += 0.000100000005f;
				}
			}
			for (int num8 = 0; num8 < Common.size<Tiki>(this.mTikis); num8++)
			{
				this.mTikis[num8].Update();
			}
			for (int num9 = 0; num9 < Common.size<BossWall>(this.mWalls); num9++)
			{
				BossWall bossWall = this.mWalls[num9];
				int mAlpha = bossWall.mAlpha;
				bossWall.mAlpha += bossWall.mAlphaFadeDir * Common._M(8);
				if (bossWall.mAlpha < 0)
				{
					bossWall.mAlpha = 0;
				}
				else if (bossWall.mAlpha > 255)
				{
					bossWall.mAlpha = 255;
				}
				if (bossWall.mAlphaFadeDir == 1 && bossWall.mAlpha >= 255 && mAlpha < bossWall.mAlpha)
				{
					bossWall.mAlphaFadeDir = 0;
					this.ResetWallAndTikis(num9);
				}
			}
			if (this.mBombInRange)
			{
				Boss.gWackColorFade += Boss.gWackColorFadeDir;
				if (Boss.gWackColorFade >= 255 && Boss.gWackColorFadeDir > 0)
				{
					Boss.gWackColorFade = 255;
					Boss.gWackColorFadeDir *= -1;
					return;
				}
				if (Boss.gWackColorFade <= 0 && Boss.gWackColorFadeDir < 0)
				{
					Boss.gWackColorFade = 0;
					Boss.gWackColorFadeDir *= -1;
				}
			}
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0002D06B File Offset: 0x0002B26B
		public virtual void Update()
		{
			this.Update(1f);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0002D078 File Offset: 0x0002B278
		public virtual void DrawDeathBGTikis(SexyGraphics g)
		{
			if (this.mHP <= 0f && this.mDrawDeathBGTikis)
			{
				int num = (int)((255f - this.mAlphaOverride) / (float)Common._M(11));
				if (num > 255)
				{
					num = 255;
				}
				g.PushState();
				g.SetColorizeImages(true);
				g.SetColor(255, 255, 255, num);
				for (int i = 0; i < 13; i++)
				{
					ResID id = ResID.IMAGE_BOSSES_DEATH_BG_TIKIS_1 + i;
					Image imageByID = Res.GetImageByID(id);
					int num2 = Common._DS(Res.GetOffsetXByID(id) - 160);
					int num3 = Common._DS(Res.GetOffsetYByID(id));
					g.DrawImage(imageByID, num2, num3);
					if (i != 7 && i != 5)
					{
						g.DrawImageMirror(imageByID, num2 + imageByID.GetWidth(), num3);
					}
				}
				g.PopState();
			}
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0002D150 File Offset: 0x0002B350
		public virtual void Draw(SexyGraphics g)
		{
			if (this.mHP > 0f && !this.mDoDeathExplosions && !this.mLevel.mBoard.DoingBossIntro())
			{
				for (int i = 0; i < Common.size<HulaDancer>(this.mHulaDancers); i++)
				{
					this.mHulaDancers[i].Draw(g);
				}
			}
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0002D1AC File Offset: 0x0002B3AC
		public void DrawDeathText(SexyGraphics g, int alpha_override)
		{
			bool flag = false;
			for (int i = 0; i < Common.size<BossText>(this.mDeathText); i++)
			{
				BossText bossText = this.mDeathText[i];
				if (bossText.mAlpha <= 0f)
				{
					break;
				}
				if (i == Common.size<BossText>(this.mDeathText) - 1 && bossText.mAlpha >= (float)Common._M(200))
				{
					flag = true;
				}
				Font fontByID = Res.GetFontByID(ResID.FONT_BOSS_TAUNT);
				if ((int)Localization.GetCurrentLanguage() == 5)
				{
					fontByID.mAscent = 25;
				}
				g.SetFont(fontByID);
				int num = Common._S(Common._M(200)) + i * Common._S(Common._M1(30));
				g.SetColor(Common._M(255), Common._M1(255), Common._M2(255), (int)((alpha_override == -1) ? bossText.mAlpha : ((float)alpha_override)));
				g.WriteWordWrapped(new Rect(0, num + Localization.GetCurrentFontOffsetY() * i, this.mApp.mWidth, this.mApp.mHeight), bossText.mText, -1, 0);
			}
			if (flag)
			{
				if (alpha_override != -1)
				{
					g.SetColorizeImages(true);
					g.SetColor(255, 255, 255, alpha_override);
				}
				Image imageByID = Res.GetImageByID(ResID.IMAGE_FROG_RIBBIT);
				g.DrawImage(imageByID, (this.mApp.mWidth - imageByID.mWidth) / 2, Common._S(Common._M(330)));
				g.SetColorizeImages(false);
				Font fontByID2 = Res.GetFontByID(ResID.FONT_SHAGLOUNGE28_STROKE);
				g.SetFont(fontByID2);
				g.SetColor(Common._M(255), Common._M1(255), Common._M2(255));
				g.WriteString(TextManager.getInstance().getString(433), 0, Common._DS(Common._M(1170)), this.mApp.mWidth, 0);
			}
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0002D38B File Offset: 0x0002B58B
		public void DrawDeathText(SexyGraphics g)
		{
			this.DrawDeathText(g, -1);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0002D395 File Offset: 0x0002B595
		public virtual void DrawTopLevel(SexyGraphics g)
		{
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0002D397 File Offset: 0x0002B597
		public virtual void DrawBottomLevel(SexyGraphics g)
		{
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0002D39C File Offset: 0x0002B59C
		public virtual void DrawBelowBalls(SexyGraphics g)
		{
			if (this.mDrawRadius && this.mHP > 0f && !this.mDoDeathExplosions && !this.mLevel.mBoard.DoingBossIntro())
			{
				Color color;
				color = new Color(0, 0, 255, Common._M(125));
				if (this.mRadiusColorChangeMode != 0 && this.mBombInRange)
				{
					color = new Color(255, 0, 0, Common._M(200));
				}
				g.SetColor(color);
				CommonGraphics.DrawCircle(g, Common._S(this.mX), Common._S(this.mY), (float)this.mProxBombRadius, Common._S(Common._M(30)));
			}
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0002D454 File Offset: 0x0002B654
		public virtual void DrawWordBubble(SexyGraphics g)
		{
			if (Common.size<TauntText>(this.mTauntQueue) == 0)
			{
				return;
			}
			TauntText tauntText = this.mTauntQueue[0];
			int wordBubbleAlpha = this.GetWordBubbleAlpha(tauntText);
			if (wordBubbleAlpha < 0)
			{
				return;
			}
			Font fontByID = Res.GetFontByID(ResID.FONT_MAIN22);
			Image image;
			Rect rect;
			Rect rect2;
			this.SetWordBubbleLayout(tauntText.mText, fontByID, out image, out rect, out rect2);
			g.SetFont(fontByID);
			g.SetColor(255, 255, 255, wordBubbleAlpha);
			if ((wordBubbleAlpha != 255 && Common.size<TauntText>(this.mTauntQueue) == 1) || this.mAlphaOverride <= 254f)
			{
				g.SetColorizeImages(true);
			}
			g.DrawImageBox(rect, image);
			g.SetColor(0, 0, 0, wordBubbleAlpha);
			g.WriteWordWrapped(rect2, tauntText.mText, -1, 0);
			g.SetColorizeImages(false);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0002D518 File Offset: 0x0002B718
		public int GetWordBubbleAlpha(TauntText inTauntText)
		{
			int num = inTauntText.mDelay - inTauntText.mUpdateCount;
			int num2 = 255;
			if (num <= 20)
			{
				num2 -= 26 * (20 - num);
			}
			if (this.mAlphaOverride <= 254f)
			{
				num2 = (int)Math.Min((float)num2, this.mAlphaOverride);
			}
			return num2;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0002D568 File Offset: 0x0002B768
		public void SetWordBubbleLayout(string inText, Font inFont, out Image outBubbleBkg, out Rect outBubble, out Rect outInset)
		{
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_INGAME_BOSSUI);
			Image imageByID2 = Res.GetImageByID(ResID.IMAGE_BOSS_WORD_BUBBLE);
			Image imageByID3 = Res.GetImageByID(ResID.IMAGE_BOSS_WORD_BUBBLE_MIRROR);
			int num = (int)((float)this.mApp.GetScreenRect().mWidth - (float)imageByID.GetWidth() * 1.5f);
			int num2 = (int)((float)(num - Common._S(this.mWidth)) * 0.4f);
			int num3 = Common._S(5);
			int num4 = num2 - num3 * 2;
			int num5 = Common._GetWordWrappedHeight(inText, inFont, num4);
			int num6 = num5 + num3 * 2;
			Image image = imageByID2;
			int num7 = (int)((float)image.GetWidth() * 0.23f);
			int num8 = (int)((float)image.GetHeight() * 0.22f);
			Rect rect = default(Rect);
			rect.mX = (int)Common._S(this.mX + (float)this.mWidth * 0.5f);
			rect.mY = (int)(Common._S(this.mY - (float)this.mHeight * 0.5f) + (float)this.mTauntTextYOff);
			rect.mWidth = num2 + num7 * 2;
			rect.mHeight = num6 + num8 * 2;
			int num9 = this.mApp.GetScreenRect().mX + num;
			if (rect.mX + rect.mWidth >= num9)
			{
				int num10 = rect.mWidth + Common._S(this.mWidth);
				if (rect.mX - num10 >= 0)
				{
					image = imageByID3;
					rect.mX -= num10;
				}
			}
			outBubbleBkg = image;
			outBubble = rect;
			outInset = new Rect(rect.mX + num7 + num3, rect.mY + num8 + num3, num4, num5);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0002D719 File Offset: 0x0002B919
		public virtual void FrogInitialized(Gun g)
		{
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0002D71B File Offset: 0x0002B91B
		public virtual void MouseDownDuringNoFire(int x, int y)
		{
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0002D71D File Offset: 0x0002B91D
		public virtual bool AllowFrogToFire()
		{
			return this.mLevel.HasReachedCruisingSpeed();
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0002D72A File Offset: 0x0002B92A
		public virtual int GetFrogReloadType()
		{
			return -1;
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0002D72D File Offset: 0x0002B92D
		public virtual void MoveToDeathPosition(float x, float y)
		{
			this.mDeathTX = x;
			this.mDeathTY = y;
			this.mDeathVX = (x - this.mX) / 200f;
			this.mDeathVY = (y - this.mY) / 200f;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0002D768 File Offset: 0x0002B968
		public void ShowAllDeathText()
		{
			this.mApp.SetCursor((ECURSOR)1);
			for (int i = 0; i < Enumerable.Count<BossText>(this.mDeathText); i++)
			{
				this.mDeathText[i].mAlpha = 255f;
			}
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0002D7B0 File Offset: 0x0002B9B0
		public void AddHulaEntry(float vx, float projvy, int spawn, int spawny, int proj_chance, int berserk_amt, int proj_range, int atype, int atime, int amnesty)
		{
			HulaEntry hulaEntry = new HulaEntry();
			hulaEntry.mBerserkAmt = berserk_amt;
			hulaEntry.mAmnesty = amnesty;
			hulaEntry.mProjVY = projvy;
			hulaEntry.mSpawnRate = spawn;
			hulaEntry.mVX = vx;
			hulaEntry.mSpawnY = spawny;
			hulaEntry.mProjChance = proj_chance;
			hulaEntry.mAttackTime = atime;
			hulaEntry.mAttackType = atype;
			hulaEntry.mProjRange = proj_range;
			this.mHulaEntryVec.Add(hulaEntry);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0002D81C File Offset: 0x0002BA1C
		public List<HulaEntry> getHulaEntryList()
		{
			return this.mHulaEntryVec;
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0002D824 File Offset: 0x0002BA24
		public void PlaySound(int soundid)
		{
			if (this.mApp.GetBoard().DoingIntros())
			{
				return;
			}
			if (this.mSounds[soundid] != -1)
			{
				this.mApp.PlaySample(this.mSounds[soundid]);
			}
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0002D857 File Offset: 0x0002BA57
		public virtual void ProximityBombActivated(float x, float y, int radius)
		{
			this.ForceActivation(true);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0002D860 File Offset: 0x0002BA60
		public void AddBerserkValue(int health_limit, string param_name, string value, ref string minval, ref string maxval, bool _override)
		{
			BerserkModifier berserkModifier = new BerserkModifier(param_name, value, minval, maxval, _override);
			bool flag = param_name.Length == 0;
			for (int i = 0; i < Enumerable.Count<BerserkTier>(this.mBerserkTiers); i++)
			{
				if (this.mBerserkTiers[i].mHealthLimit == health_limit)
				{
					if (!flag)
					{
						this.mBerserkTiers[i].mParams.Add(berserkModifier);
					}
					return;
				}
			}
			BerserkTier berserkTier = new BerserkTier(health_limit);
			if (!flag)
			{
				berserkTier.mParams.Add(berserkModifier);
			}
			for (int j = 0; j < Enumerable.Count<BerserkTier>(this.mBerserkTiers); j++)
			{
				if (health_limit > this.mBerserkTiers[j].mHealthLimit)
				{
					this.mBerserkTiers.Insert(j, berserkTier);
					return;
				}
			}
			this.mBerserkTiers.Add(berserkTier);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0002D92C File Offset: 0x0002BB2C
		public List<BerserkTier> getBerserkTiers()
		{
			return this.mBerserkTiers;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0002D934 File Offset: 0x0002BB34
		public void AddBerserkValue(int health_limit, string param_name, string value)
		{
			string text = "";
			this.AddBerserkValue(health_limit, param_name, value, ref text, ref text, false);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0002D958 File Offset: 0x0002BB58
		public virtual void SyncState(DataSync sync)
		{
			sync.SyncBoolean(ref this.mEatsBalls);
			sync.SyncFloat(ref this.mX);
			sync.SyncFloat(ref this.mY);
			sync.SyncFloat(ref this.mMaxHP);
			sync.SyncFloat(ref this.mHP);
			sync.SyncLong(ref this.mHulaAmnesty);
			sync.SyncFloat(ref this.mDHPDecPerHit.value);
			sync.SyncFloat(ref this.mDHPDecPerProxBomb.value);
			sync.SyncBoolean(ref this.mNeedsIntroSound);
			sync.SyncBoolean(ref this.mIsBerserk);
			sync.SyncLong(ref this.mWidth);
			sync.SyncLong(ref this.mHeight);
			sync.SyncLong(ref this.mUpdateCount);
			sync.SyncBoolean(ref this.mBombInRange);
			sync.SyncBoolean(ref this.mDoExplosion);
			if (sync.isWrite())
			{
				Common.SerializePIEffect(this.mHitEffect, sync);
			}
			else
			{
				Common.DeserializePIEffect(this.mHitEffect, sync);
				Common.SetFXNumScale(this.mHitEffect, GameApp.gApp.Is3DAccelerated() ? 1f : 0.25f);
			}
			sync.SyncBoolean(ref this.mNeedsCompacting);
			sync.SyncLong(ref this.mStunTime);
			sync.SyncLong(ref this.mCurrTikiBossHealthRemoved);
			sync.SyncLong(ref this.mDTikiHealthRespawnAmt.value);
			sync.SyncFloat(ref this.mDeathVX);
			sync.SyncFloat(ref this.mDeathVY);
			sync.SyncFloat(ref this.mDeathTX);
			sync.SyncFloat(ref this.mDeathTY);
			sync.SyncLong(ref this.mWordBubbleTimer);
			sync.SyncLong(ref this.mDeathTimer);
			sync.SyncBoolean(ref this.mDoDeathExplosions);
			sync.SyncLong(ref this.mDWallDownTime.value);
			sync.SyncLong(ref this.mCurWallDownTime);
			sync.SyncLong(ref this.mImpatientTimer);
			sync.SyncLong(ref this.mCurrentHulaEntry.mBerserkAmt);
			sync.SyncFloat(ref this.mCurrentHulaEntry.mVX);
			sync.SyncFloat(ref this.mCurrentHulaEntry.mProjVY);
			sync.SyncLong(ref this.mCurrentHulaEntry.mSpawnRate);
			sync.SyncLong(ref this.mCurrentHulaEntry.mSpawnY);
			sync.SyncLong(ref this.mCurrentHulaEntry.mProjChance);
			sync.SyncLong(ref this.mCurrentHulaEntry.mAttackType);
			sync.SyncLong(ref this.mCurrentHulaEntry.mAttackTime);
			sync.SyncLong(ref this.mCurrentHulaEntry.mProjRange);
			sync.SyncLong(ref this.mCurrentHulaEntry.mAmnesty);
			this.SyncTauntTexts(sync, true);
			Buffer buffer = sync.GetBuffer();
			if (sync.isWrite())
			{
				buffer.WriteLong((long)this.mHulaDancers.Count);
				for (int i = 0; i < this.mHulaDancers.Count; i++)
				{
					this.mHulaDancers[i].SyncState(sync);
				}
				buffer.WriteLong((long)this.mDeathText.Count);
				for (int j = 0; j < this.mDeathText.Count; j++)
				{
					buffer.WriteFloat(this.mDeathText[j].mAlpha);
				}
				buffer.WriteLong((long)this.mDeathExplosions.Count);
				for (int k = 0; k < this.mDeathExplosions.Count; k++)
				{
					Common.SerializePIEffect(this.mDeathExplosions[k], sync);
				}
			}
			else
			{
				int num = (int)buffer.ReadLong();
				for (int l = 0; l < num; l++)
				{
					HulaDancer hulaDancer = new HulaDancer();
					hulaDancer.SyncState(sync);
					this.mHulaDancers.Add(hulaDancer);
				}
				int num2 = (int)buffer.ReadLong();
				for (int m = 0; m < num2; m++)
				{
					this.mDeathText[m].mAlpha = buffer.ReadFloat();
				}
				num2 = (int)buffer.ReadLong();
				for (int n = 0; n < num2; n++)
				{
					PIEffect pieffect = Res.GetPIEffectByID(ResID.PIEFFECT_NONRESIZE_DEATH_EXPLOSION).Duplicate();
					this.mDeathExplosions.Add(pieffect);
					Common.DeserializePIEffect(pieffect, sync);
					Common.SetFXNumScale(pieffect, GameApp.gApp.Is3DAccelerated() ? 1f : Common._M(0.25f));
				}
			}
			for (int num3 = 0; num3 < this.mWalls.Count; num3++)
			{
				sync.SyncLong(ref this.mWalls[num3].mAlpha);
				sync.SyncLong(ref this.mWalls[num3].mAlphaFadeDir);
			}
			for (int num4 = 0; num4 < this.mTikis.Count; num4++)
			{
				sync.SyncLong(ref this.mTikis[num4].mAlphaFadeDir);
				sync.SyncFloat(ref this.mTikis[num4].mX);
				sync.SyncFloat(ref this.mTikis[num4].mY);
				sync.SyncBoolean(ref this.mTikis[num4].mWasHit);
				sync.SyncLong(ref this.mTikis[num4].mAlpha);
			}
			for (int num5 = 0; num5 < Boss.NUM_HEARTS; num5++)
			{
				sync.SyncLong(ref this.mHeartCels[num5]);
			}
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0002DE68 File Offset: 0x0002C068
		private void SyncTauntTexts(DataSync sync, bool clear)
		{
			if (sync.isRead())
			{
				if (clear)
				{
					this.mTauntQueue.Clear();
				}
				long num = sync.GetBuffer().ReadLong();
				int num2 = 0;
				while ((long)num2 < num)
				{
					TauntText tauntText = new TauntText();
					tauntText.SyncState(sync);
					this.mTauntQueue.Add(tauntText);
					num2++;
				}
				return;
			}
			sync.GetBuffer().WriteLong((long)this.mTauntQueue.Count);
			foreach (TauntText tauntText2 in this.mTauntQueue)
			{
				tauntText2.SyncState(sync);
			}
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0002DF1C File Offset: 0x0002C11C
		public virtual bool Collides(Bullet b)
		{
			float num = (float)b.GetRadius() * Common._M(0.75f);
			Rect r;
			r = new Rect((int)(b.GetX() - num), (int)(b.GetY() - num), (int)(num * 2f), (int)(num * 2f));
			bool flag = false;
			if (this.AllowFrogToFire())
			{
				flag = this.BulletIntersectsBoss(b);
				if (flag && !this.mEatsBalls)
				{
					flag = this.DoHit(b, false);
				}
				else if (flag && this.mEatsBalls)
				{
					this.BallEaten(b);
					this.PlaySound(6);
					return true;
				}
			}
			if (this.CollidesWithWall(b))
			{
				return true;
			}
			for (int i = 0; i < Enumerable.Count<HulaDancer>(this.mHulaDancers); i++)
			{
				if (this.mHulaDancers[i].Collided(r))
				{
					this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_NEW_HULAGIRLHIT));
					this.mHulaAmnesty = this.mCurrentHulaEntry.mAmnesty;
					this.mHulaDancers[i].Disable();
					return true;
				}
			}
			bool flag2 = false;
			if (this.AllowFrogToFire())
			{
				for (int j = 0; j < Enumerable.Count<Tiki>(this.mTikis); j++)
				{
					if (!this.mTikis[j].mWasHit && this.mTikis[j].mAlphaFadeDir >= 0)
					{
						bool flag3 = false;
						if (this.mTikis[j].Collides(b, ref flag3))
						{
							this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_TIKI_HIT));
							if (Enumerable.Count<Tiki>(this.mTikis) == Enumerable.Count<BossWall>(this.mWalls))
							{
								BossWall bossWall = this.mWalls[j];
								bossWall.mAlphaFadeDir = -1;
								this.TikiHit(j);
								int num2 = 0;
								for (int k = 0; k < Enumerable.Count<Tiki>(this.mTikis); k++)
								{
									if (this.mTikis[k].mWasHit)
									{
										num2++;
									}
								}
								if (num2 == Enumerable.Count<Tiki>(this.mTikis))
								{
									this.mCurWallDownTime = this.mWallDownTime;
								}
							}
							return true;
						}
					}
				}
			}
			if (flag && this.mResetWallsOnBossHit)
			{
				for (int l = 0; l < Enumerable.Count<BossWall>(this.mWalls); l++)
				{
					this.mWalls[l].mAlphaFadeDir = 1;
				}
				for (int m = 0; m < Enumerable.Count<Tiki>(this.mTikis); m++)
				{
					this.mTikis[m].mAlphaFadeDir = 1;
					this.mTikis[m].mWasHit = false;
				}
			}
			return flag || flag2;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0002E1B2 File Offset: 0x0002C3B2
		public virtual void ForceActivation(bool from_prox_bomb)
		{
			this.DoHit(null, from_prox_bomb);
		}

		// Token: 0x06000349 RID: 841
		public abstract Boss Instantiate();

		// Token: 0x0600034A RID: 842 RVA: 0x0002E1C0 File Offset: 0x0002C3C0
		public virtual void PostInstantiationHook(Boss source_boss)
		{
			this.mFParamPointerMap.Clear();
			this.mIParamPointerMap.Clear();
			this.mBParamPointerMap.Clear();
			this.AddParamPointer("WallDownTime", this.mDWallDownTime);
			this.AddParamPointer("HPDecPerHit", this.mDHPDecPerHit);
			this.AddParamPointer("HPDecPerProxBomb", this.mDHPDecPerProxBomb);
			this.AddParamPointer("TikiHealthRespawn", this.mDTikiHealthRespawnAmt);
			this.mTikis.Clear();
			foreach (Tiki tiki in source_boss.mTikis)
			{
				this.AddTiki((int)tiki.mX, (int)tiki.mY, tiki.mId, tiki.mRailEndX - tiki.mRailStartX, tiki.mRailEndY - tiki.mRailStartY, tiki.mTravelTime);
			}
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0002E2B8 File Offset: 0x0002C4B8
		public virtual bool CanAdvanceBalls()
		{
			return true;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0002E2BB File Offset: 0x0002C4BB
		public virtual void PlayerStartedFiring()
		{
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0002E2BD File Offset: 0x0002C4BD
		public virtual void SetXY(float x, float y)
		{
			this.mX = x;
			this.mY = y;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0002E2CD File Offset: 0x0002C4CD
		public virtual void SetX(float x)
		{
			this.mX = x;
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0002E2D6 File Offset: 0x0002C4D6
		public virtual void SetY(float y)
		{
			this.mY = y;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0002E2DF File Offset: 0x0002C4DF
		public virtual void SetHPDecPerHit(float hp)
		{
			this.mHPDecPerHit = hp;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0002E2E8 File Offset: 0x0002C4E8
		public virtual void SetHPDecPerHitProxBomb(float hp)
		{
			this.mHPDecPerProxBomb = hp;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0002E2F1 File Offset: 0x0002C4F1
		public virtual void Stun(int stime)
		{
			this.mStunTime = stime;
			this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_BOSS_STUNNED));
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0002E310 File Offset: 0x0002C510
		public virtual void SetHP(float hp)
		{
			float num = this.mHP;
			this.mHP = hp;
			int num2 = (int)((num - this.mHP) / this.mHPDecPerHit);
			for (int i = 0; i < num2; i++)
			{
				this.DecHearts(this.mHeartPieceDecAmt);
			}
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0002E354 File Offset: 0x0002C554
		public bool IsStunned()
		{
			return this.mStunTime > 0;
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0002E35F File Offset: 0x0002C55F
		public float GetHP()
		{
			return this.mHP;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0002E367 File Offset: 0x0002C567
		public virtual int GetX()
		{
			return (int)this.mX;
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0002E370 File Offset: 0x0002C570
		public virtual int GetY()
		{
			return (int)this.mY;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0002E379 File Offset: 0x0002C579
		public virtual int GetTopLeftX()
		{
			return (int)this.mX - this.mWidth / 2;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0002E38B File Offset: 0x0002C58B
		public virtual int GetTopLeftY()
		{
			return (int)this.mY - this.mHeight / 2;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0002E39D File Offset: 0x0002C59D
		public int GetWidth()
		{
			return this.mWidth;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0002E3A5 File Offset: 0x0002C5A5
		public int GetHeight()
		{
			return this.mHeight;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0002E3AD File Offset: 0x0002C5AD
		public bool IsImpatient()
		{
			return this.mImpatientTimer == 0;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0002E3B8 File Offset: 0x0002C5B8
		public bool IsHitByExplosion(float x, float y, int radius)
		{
			return MathUtils.CirclesIntersect(x, y, this.mX, this.mY, (float)(this.mProxBombRadius + 56 + Common.GetDefaultBallRadius()));
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0002E3DD File Offset: 0x0002C5DD
		public virtual void InitParam()
		{
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0002E3E0 File Offset: 0x0002C5E0
		public void CopyFrom(Boss rhs)
		{
			this.mX = rhs.mX;
			this.mY = rhs.mY;
			this.mMaxHP = rhs.mMaxHP;
			this.mHP = rhs.mHP;
			this.mWidth = rhs.mWidth;
			this.mHeight = rhs.mHeight;
			this.mUpdateCount = rhs.mUpdateCount;
			this.mHPDecPerHit = rhs.mHPDecPerHit;
			this.mHPDecPerProxBomb = rhs.mHPDecPerProxBomb;
			this.mShakeXAmt = rhs.mShakeXAmt;
			this.mShakeYAmt = rhs.mShakeYAmt;
			this.mShouldDoDeathExplosions = rhs.mShouldDoDeathExplosions;
			this.mShakeXOff = rhs.mShakeXOff;
			this.mShakeYOff = rhs.mShakeYOff;
			this.mAllowLevelDDS = rhs.mAllowLevelDDS;
			this.mDoExplosion = rhs.mDoExplosion;
			this.mNeedsCompacting = rhs.mNeedsCompacting;
			this.mAllowCompacting = rhs.mAllowCompacting;
			this.mHeartXOff = rhs.mHeartXOff;
			this.mHeartYOff = rhs.mHeartYOff;
			this.mResetWallTimerOnTikiHit = rhs.mResetWallTimerOnTikiHit;
			this.mResetWallsOnBossHit = rhs.mResetWallsOnBossHit;
			this.mWallDownTime = rhs.mWallDownTime;
			this.mCurWallDownTime = rhs.mCurWallDownTime;
			this.mStunTime = rhs.mStunTime;
			this.mCurrTikiBossHealthRemoved = rhs.mCurrTikiBossHealthRemoved;
			this.mTikiHealthRespawnAmt = rhs.mTikiHealthRespawnAmt;
			this.mNum = rhs.mNum;
			this.mIsBerserk = rhs.mIsBerserk;
			this.mApp = GameApp.gApp;
			this.mEatsBalls = rhs.mEatsBalls;
			this.mImpatientTimer = rhs.mImpatientTimer;
			this.mBombFreqMax = rhs.mBombFreqMax;
			this.mBombFreqMin = rhs.mBombFreqMin;
			this.mBombDuration = rhs.mBombDuration;
			this.mProxBombRadius = rhs.mProxBombRadius;
			this.mDrawRadius = rhs.mDrawRadius;
			this.mBossRadius = rhs.mBossRadius;
			this.mNeedsIntroSound = rhs.mNeedsIntroSound;
			this.mBombInRange = rhs.mBombInRange;
			this.mRadiusColorChangeMode = rhs.mRadiusColorChangeMode;
			this.mDoDeathExplosions = rhs.mDoDeathExplosions;
			this.mDeathTimer = rhs.mDeathTimer;
			this.mWordBubbleTimer = rhs.mWordBubbleTimer;
			this.mSepiaImage = rhs.mSepiaImage;
			this.mDeathTX = rhs.mDeathTX;
			this.mDeathTY = rhs.mDeathTY;
			this.mDeathVX = rhs.mDeathVX;
			this.mDeathVY = rhs.mDeathVY;
			this.mExplosionRate = rhs.mExplosionRate;
			this.mBossRadiusYOff = rhs.mBossRadiusYOff;
			this.mHulaAmnesty = rhs.mHulaAmnesty;
			this.mBandagedImg = rhs.mBandagedImg;
			this.mAlphaOverride = rhs.mAlphaOverride;
			this.mBandagedXOff = rhs.mBandagedXOff;
			this.mBandagedYOff = rhs.mBandagedYOff;
			this.mDrawDeathBGTikis = rhs.mDrawDeathBGTikis;
			this.mTauntTextYOff = rhs.mTauntTextYOff;
			this.mResPrefix = rhs.mResPrefix;
			this.mHitEffect = rhs.mHitEffect;
			this.mDeathText.Clear();
			this.mDeathText.AddRange(rhs.mDeathText.ToArray());
			this.mTauntText.Clear();
			this.mTauntText.AddRange(rhs.mTauntText.ToArray());
			this.mTikis.Clear();
			for (int i = 0; i < rhs.mTikis.Count; i++)
			{
				this.mTikis.Add(new Tiki(rhs.mTikis[i]));
			}
			this.mHulaDancers.Clear();
			for (int j = 0; j < rhs.mHulaDancers.Count; j++)
			{
				this.mHulaDancers.Add(new HulaDancer(rhs.mHulaDancers[j]));
			}
			this.mHulaEntryVec.Clear();
			for (int k = 0; k < rhs.mHulaEntryVec.Count; k++)
			{
				this.mHulaEntryVec.Add(new HulaEntry(rhs.mHulaEntryVec[k]));
			}
			if (rhs.mCurrentHulaEntry != null)
			{
				this.mCurrentHulaEntry = new HulaEntry(rhs.mCurrentHulaEntry);
			}
			Dictionary<string, ParamData<float>>.Enumerator enumerator = rhs.mFParamPointerMap.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Dictionary<string, ParamData<float>> dictionary = this.mFParamPointerMap;
				KeyValuePair<string, ParamData<float>> keyValuePair = enumerator.Current;
				if (dictionary[keyValuePair.Key] != null)
				{
					Dictionary<string, ParamData<float>> dictionary2 = this.mFParamPointerMap;
					KeyValuePair<string, ParamData<float>> keyValuePair2 = enumerator.Current;
					ParamData<float> paramData = dictionary2[keyValuePair2.Key];
					KeyValuePair<string, ParamData<float>> keyValuePair3 = enumerator.Current;
					paramData.value = keyValuePair3.Value.value;
				}
			}
			Dictionary<string, ParamData<int>>.Enumerator enumerator2 = rhs.mIParamPointerMap.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				Dictionary<string, ParamData<int>> dictionary3 = this.mIParamPointerMap;
				KeyValuePair<string, ParamData<int>> keyValuePair4 = enumerator2.Current;
				if (dictionary3[keyValuePair4.Key] != null)
				{
					Dictionary<string, ParamData<int>> dictionary4 = this.mIParamPointerMap;
					KeyValuePair<string, ParamData<int>> keyValuePair5 = enumerator2.Current;
					ParamData<int> paramData2 = dictionary4[keyValuePair5.Key];
					KeyValuePair<string, ParamData<int>> keyValuePair6 = enumerator2.Current;
					paramData2.value = keyValuePair6.Value.value;
				}
			}
			Dictionary<string, ParamData<bool>>.Enumerator enumerator3 = rhs.mBParamPointerMap.GetEnumerator();
			while (enumerator3.MoveNext())
			{
				Dictionary<string, ParamData<bool>> dictionary5 = this.mBParamPointerMap;
				KeyValuePair<string, ParamData<bool>> keyValuePair7 = enumerator3.Current;
				if (dictionary5[keyValuePair7.Key] != null)
				{
					Dictionary<string, ParamData<bool>> dictionary6 = this.mBParamPointerMap;
					KeyValuePair<string, ParamData<bool>> keyValuePair8 = enumerator3.Current;
					ParamData<bool> paramData3 = dictionary6[keyValuePair8.Key];
					KeyValuePair<string, ParamData<bool>> keyValuePair9 = enumerator3.Current;
					paramData3.value = keyValuePair9.Value.value;
				}
			}
			this.mBerserkTiers.Clear();
			for (int l = 0; l < rhs.mBerserkTiers.Count; l++)
			{
				this.mBerserkTiers.Add(new BerserkTier(rhs.mBerserkTiers[l]));
			}
			this.mWalls.Clear();
			for (int m = 0; m < rhs.mWalls.Count; m++)
			{
				this.mWalls.Add(new BossWall(rhs.mWalls[m]));
			}
			this.mDeathExplosions.Clear();
			for (int n = 0; n < rhs.mDeathExplosions.Count; n++)
			{
				this.mDeathExplosions.Add(this.mDeathExplosions[n]);
			}
			this.mTauntQueue.Clear();
			for (int num = 0; num < rhs.mTauntQueue.Count; num++)
			{
				this.mTauntQueue.Add(new TauntText(this.mTauntQueue[num]));
			}
			for (int num2 = 0; num2 < rhs.mSounds.Length; num2++)
			{
				this.mSounds[num2] = rhs.mSounds[num2];
			}
			for (int num3 = 0; num3 < rhs.mHeartCels.Length; num3++)
			{
				this.mHeartCels[num3] = rhs.mHeartCels[num3];
			}
		}

		// Token: 0x04000A1F RID: 2591
		public static float gBerserkTextAlpha;

		// Token: 0x04000A20 RID: 2592
		public static float gBerserkTextY;

		// Token: 0x04000A21 RID: 2593
		public static float gImpatientTextAlpha;

		// Token: 0x04000A22 RID: 2594
		public static float gImpatientTextY;

		// Token: 0x04000A23 RID: 2595
		protected static int gWackColorFade = 0;

		// Token: 0x04000A24 RID: 2596
		protected static int gWackColorFadeDir = 2;

		// Token: 0x04000A25 RID: 2597
		protected static int NUM_HEARTS = 5;

		// Token: 0x04000A26 RID: 2598
		protected static int FNTT_last_idx = 0;

		// Token: 0x04000A27 RID: 2599
		protected static int last_idx = 0;

		// Token: 0x04000A28 RID: 2600
		protected Dictionary<string, ParamData<float>> mFParamPointerMap = new Dictionary<string, ParamData<float>>();

		// Token: 0x04000A29 RID: 2601
		protected Dictionary<string, ParamData<int>> mIParamPointerMap = new Dictionary<string, ParamData<int>>();

		// Token: 0x04000A2A RID: 2602
		protected Dictionary<string, ParamData<bool>> mBParamPointerMap = new Dictionary<string, ParamData<bool>>();

		// Token: 0x04000A2B RID: 2603
		protected ParamData<int> mDWallDownTime = new ParamData<int>();

		// Token: 0x04000A2C RID: 2604
		protected ParamData<float> mDHPDecPerHit = new ParamData<float>();

		// Token: 0x04000A2D RID: 2605
		protected ParamData<float> mDHPDecPerProxBomb = new ParamData<float>();

		// Token: 0x04000A2E RID: 2606
		protected ParamData<int> mDTikiHealthRespawnAmt = new ParamData<int>();

		// Token: 0x04000A2F RID: 2607
		public bool mShouldDoDeathExplosions;

		// Token: 0x04000A30 RID: 2608
		public bool mDoDeathExplosions;

		// Token: 0x04000A31 RID: 2609
		public bool mNeedsIntroSound;

		// Token: 0x04000A32 RID: 2610
		public bool mEatsBalls;

		// Token: 0x04000A33 RID: 2611
		public GameApp mApp;

		// Token: 0x04000A34 RID: 2612
		public Level mLevel;

		// Token: 0x04000A35 RID: 2613
		public bool mAllowCompacting;

		// Token: 0x04000A36 RID: 2614
		public int mShakeXAmt;

		// Token: 0x04000A37 RID: 2615
		public int mShakeYAmt;

		// Token: 0x04000A38 RID: 2616
		public int mHeartXOff;

		// Token: 0x04000A39 RID: 2617
		public int mHeartYOff;

		// Token: 0x04000A3A RID: 2618
		public bool mResetWallsOnBossHit;

		// Token: 0x04000A3B RID: 2619
		public bool mResetWallTimerOnTikiHit;

		// Token: 0x04000A3C RID: 2620
		public bool mAllowLevelDDS;

		// Token: 0x04000A3D RID: 2621
		public bool mDrawRadius;

		// Token: 0x04000A3E RID: 2622
		public int mRadiusColorChangeMode;

		// Token: 0x04000A3F RID: 2623
		public int mCurWallDownTime;

		// Token: 0x04000A40 RID: 2624
		public int mCurrTikiBossHealthRemoved;

		// Token: 0x04000A41 RID: 2625
		public int mImpatientTimer;

		// Token: 0x04000A42 RID: 2626
		public int mNum;

		// Token: 0x04000A43 RID: 2627
		public string mName = "";

		// Token: 0x04000A44 RID: 2628
		public string mResPrefix = "";

		// Token: 0x04000A45 RID: 2629
		public int mBombFreqMin;

		// Token: 0x04000A46 RID: 2630
		public int mBombFreqMax;

		// Token: 0x04000A47 RID: 2631
		public int mBombDuration;

		// Token: 0x04000A48 RID: 2632
		public int mProxBombRadius;

		// Token: 0x04000A49 RID: 2633
		public int mBossRadius;

		// Token: 0x04000A4A RID: 2634
		public int mBossRadiusYOff;

		// Token: 0x04000A4B RID: 2635
		public int mVolcanoOffscreenDelay;

		// Token: 0x04000A4C RID: 2636
		public List<BossText> mDeathText = new List<BossText>();

		// Token: 0x04000A4D RID: 2637
		public string mWordBubbleText = "";

		// Token: 0x04000A4E RID: 2638
		public string mSepiaImagePath = "";

		// Token: 0x04000A4F RID: 2639
		public string mResGroup = "";

		// Token: 0x04000A50 RID: 2640
		public List<TauntText> mTauntText = new List<TauntText>();

		// Token: 0x04000A51 RID: 2641
		public DeviceImage mSepiaImage;

		// Token: 0x04000A52 RID: 2642
		public PIEffect mHitEffect;

		// Token: 0x04000A53 RID: 2643
		public float mAlphaOverride;

		// Token: 0x04000A54 RID: 2644
		public List<Tiki> mTikis = new List<Tiki>();

		// Token: 0x04000A55 RID: 2645
		protected int mTauntTextYOff;

		// Token: 0x04000A56 RID: 2646
		protected Image mBandagedImg;

		// Token: 0x04000A57 RID: 2647
		protected int mBandagedXOff;

		// Token: 0x04000A58 RID: 2648
		protected int mBandagedYOff;

		// Token: 0x04000A59 RID: 2649
		protected List<HulaDancer> mHulaDancers = new List<HulaDancer>();

		// Token: 0x04000A5A RID: 2650
		protected List<HulaEntry> mHulaEntryVec = new List<HulaEntry>();

		// Token: 0x04000A5B RID: 2651
		protected HulaEntry mCurrentHulaEntry = new HulaEntry();

		// Token: 0x04000A5C RID: 2652
		protected List<BerserkTier> mBerserkTiers = new List<BerserkTier>();

		// Token: 0x04000A5D RID: 2653
		protected List<BossWall> mWalls = new List<BossWall>();

		// Token: 0x04000A5E RID: 2654
		protected List<PIEffect> mDeathExplosions = new List<PIEffect>();

		// Token: 0x04000A5F RID: 2655
		protected List<TauntText> mTauntQueue = new List<TauntText>();

		// Token: 0x04000A60 RID: 2656
		protected int[] mSounds = new int[10];

		// Token: 0x04000A61 RID: 2657
		protected int mExplosionRate;

		// Token: 0x04000A62 RID: 2658
		protected bool mDrawDeathBGTikis;

		// Token: 0x04000A63 RID: 2659
		protected float mX;

		// Token: 0x04000A64 RID: 2660
		protected float mY;

		// Token: 0x04000A65 RID: 2661
		protected float mMaxHP;

		// Token: 0x04000A66 RID: 2662
		protected float mHP;

		// Token: 0x04000A67 RID: 2663
		protected float mDeathTX;

		// Token: 0x04000A68 RID: 2664
		protected float mDeathTY;

		// Token: 0x04000A69 RID: 2665
		protected float mDeathVX;

		// Token: 0x04000A6A RID: 2666
		protected float mDeathVY;

		// Token: 0x04000A6B RID: 2667
		protected int mHulaAmnesty;

		// Token: 0x04000A6C RID: 2668
		protected int mWidth;

		// Token: 0x04000A6D RID: 2669
		protected int mHeight;

		// Token: 0x04000A6E RID: 2670
		protected int mShakeXOff;

		// Token: 0x04000A6F RID: 2671
		protected int mShakeYOff;

		// Token: 0x04000A70 RID: 2672
		protected int mUpdateCount;

		// Token: 0x04000A71 RID: 2673
		protected int mHeartPieceDecAmt;

		// Token: 0x04000A72 RID: 2674
		protected int mHeartPieceDecAmtProxBomb;

		// Token: 0x04000A73 RID: 2675
		protected int[] mHeartCels = new int[Boss.NUM_HEARTS];

		// Token: 0x04000A74 RID: 2676
		protected int mStunTime;

		// Token: 0x04000A75 RID: 2677
		protected int mDeathTimer;

		// Token: 0x04000A76 RID: 2678
		protected bool mDoExplosion;

		// Token: 0x04000A77 RID: 2679
		protected bool mNeedsCompacting;

		// Token: 0x04000A78 RID: 2680
		protected bool mIsBerserk;

		// Token: 0x04000A79 RID: 2681
		protected bool mBombInRange;

		// Token: 0x04000A7A RID: 2682
		protected int mWordBubbleTimer;

		// Token: 0x04000A7B RID: 2683
		protected bool mCleanHeart = true;

		// Token: 0x020000B1 RID: 177
		public enum Sound
		{
			// Token: 0x04001672 RID: 5746
			Sound_Die,
			// Token: 0x04001673 RID: 5747
			Sound_Enrage,
			// Token: 0x04001674 RID: 5748
			Sound_Fire,
			// Token: 0x04001675 RID: 5749
			Sound_BossHit,
			// Token: 0x04001676 RID: 5750
			Sound_PlayerHit,
			// Token: 0x04001677 RID: 5751
			Sound_Intro,
			// Token: 0x04001678 RID: 5752
			Sound_EatBalls,
			// Token: 0x04001679 RID: 5753
			Sound_Roar,
			// Token: 0x0400167A RID: 5754
			Sound_Teleport,
			// Token: 0x0400167B RID: 5755
			Sound_ShieldHit,
			// Token: 0x0400167C RID: 5756
			Max_Sounds
		}

		// Token: 0x020000B2 RID: 178
		public enum ColorChange
		{
			// Token: 0x0400167E RID: 5758
			ColorChange_Never,
			// Token: 0x0400167F RID: 5759
			ColorChange_BombInRange,
			// Token: 0x04001680 RID: 5760
			ColorChange_NotBehind
		}
	}
}
