using System;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	// Token: 0x02000008 RID: 8
	public abstract class Effect : IDisposable
	{
		// Token: 0x06000134 RID: 308 RVA: 0x0000863A File Offset: 0x0000683A
		protected virtual void Init()
		{
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000863C File Offset: 0x0000683C
		public Effect()
		{
			this.mUpdateCount = 0;
			this.mIs3D = GameApp.gApp.mGraphicsDriver.Is3D();
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00008660 File Offset: 0x00006860
		public virtual void Dispose()
		{
		}

		// Token: 0x06000137 RID: 311
		public abstract void Update();

		// Token: 0x06000138 RID: 312
		public abstract string GetName();

		// Token: 0x06000139 RID: 313 RVA: 0x00008662 File Offset: 0x00006862
		public virtual void DrawUnderBalls(SexyGraphics g)
		{
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00008664 File Offset: 0x00006864
		public virtual void DrawAboveBalls(SexyGraphics g)
		{
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00008666 File Offset: 0x00006866
		public virtual void DrawUnderBackground(SexyGraphics g)
		{
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00008668 File Offset: 0x00006868
		public virtual void LevelStarted(bool from_load)
		{
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000866A File Offset: 0x0000686A
		public virtual void DrawFullScene(SexyGraphics g)
		{
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000866C File Offset: 0x0000686C
		public virtual void DrawFullSceneNoFrog(SexyGraphics g)
		{
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000866E File Offset: 0x0000686E
		public virtual void DrawPriority(SexyGraphics g, int priority)
		{
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00008670 File Offset: 0x00006870
		public virtual bool DrawTunnel(SexyGraphics g, Image img, int x, int y)
		{
			return true;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00008674 File Offset: 0x00006874
		public virtual void Reset(string level_id)
		{
			if (level_id.Length == 0)
			{
				return;
			}
			char c = level_id[0];
			if (c >= 'a' && c <= 'z')
			{
				c -= ' ';
				this.mLevelId = c + level_id.Substring(1);
			}
			else
			{
				this.mLevelId = level_id;
			}
			if (this.mResGroup.Length > 0 && GameApp.gApp.mResourceManager.IsGroupLoaded(this.mResGroup))
			{
				this.Init();
			}
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000086F0 File Offset: 0x000068F0
		public virtual void LoadResources()
		{
			if (this.mResGroup.Length == 0 || GameApp.gApp.mResourceManager.IsGroupLoaded(this.mResGroup))
			{
				return;
			}
			GameApp.gApp.mResourceManager.LoadResources(this.mResGroup);
			this.Init();
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000873E File Offset: 0x0000693E
		public virtual void DeleteResources()
		{
			if (this.mResGroup.Length == 0 || !GameApp.gApp.mResourceManager.IsGroupLoaded(this.mResGroup))
			{
				return;
			}
			GameApp.gApp.mResourceManager.DeleteResources(this.mResGroup);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000877A File Offset: 0x0000697A
		public virtual void BulletFired(Bullet b)
		{
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000877C File Offset: 0x0000697C
		public virtual bool DrawSkullPit(SexyGraphics g, HoleMgr hole)
		{
			return false;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000877F File Offset: 0x0000697F
		public virtual void UserDied()
		{
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00008781 File Offset: 0x00006981
		public virtual void NukeParams()
		{
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00008783 File Offset: 0x00006983
		public virtual void SetParams(string key, string value)
		{
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00008785 File Offset: 0x00006985
		public virtual void BulletHit(Bullet b)
		{
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00008787 File Offset: 0x00006987
		public virtual void CopyFrom(Effect e)
		{
			this.Reset(this.mLevelId);
			this.mUpdateCount = e.mUpdateCount;
			this.mIs3D = e.mIs3D;
			this.mResGroup = e.mResGroup;
			this.mLevelId = e.mLevelId;
		}

		// Token: 0x04000809 RID: 2057
		protected int mUpdateCount;

		// Token: 0x0400080A RID: 2058
		protected bool mIs3D;

		// Token: 0x0400080B RID: 2059
		protected string mResGroup;

		// Token: 0x0400080C RID: 2060
		protected string mLevelId;
	}
}
