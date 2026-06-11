using System;
using System.Collections.Generic;
using SexyFramework;

namespace ZumasRevenge
{
	// Token: 0x020000AC RID: 172
	public class EffectManager : IDisposable
	{
		// Token: 0x06000DE1 RID: 3553 RVA: 0x0008D0CA File Offset: 0x0008B2CA
		public EffectManager()
		{
			this.Reset();
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x0008D0E4 File Offset: 0x0008B2E4
		public virtual void Dispose()
		{
			for (int i = 0; i < this.mEffects.Count; i++)
			{
				if (this.mEffects[i] != null)
				{
					this.mEffects[i].Dispose();
					this.mEffects[i] = null;
				}
			}
			this.mEffects.Clear();
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x0008D140 File Offset: 0x0008B340
		public void Reset()
		{
			if (GameApp.gApp != null && GameApp.gApp.mShutdown)
			{
				return;
			}
			for (int i = 0; i < this.mEffects.Count; i++)
			{
				if (this.mEffects[i] != null)
				{
					this.mEffects[i].Dispose();
					this.mEffects[i] = null;
				}
			}
			this.mEffects.Clear();
			this.mEffects.Add(new WaterEffect1());
			this.mEffects.Add(new WillOWisp());
			this.mEffects.Add(new BallWake());
			this.mEffects.Add(new Fog());
			this.mEffects.Add(new WaterShader1());
			this.mEffects.Add(new LavaShader());
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x0008D210 File Offset: 0x0008B410
		public Effect GetEffect(string fx_name, string level_id, Level copy_effects_from)
		{
			int i = 0;
			while (i < this.mEffects.Count)
			{
				Effect effect = this.mEffects[i];
				if (Common.StrEquals(effect.GetName(), fx_name, true))
				{
					Effect effect2 = null;
					if (copy_effects_from != null)
					{
						for (int j = 0; j < copy_effects_from.mEffects.Count; j++)
						{
							if (Common.StrEquals(copy_effects_from.mEffects[j].GetName(), fx_name, true))
							{
								effect2 = copy_effects_from.mEffects[j];
								break;
							}
						}
					}
					if (effect2 != null)
					{
						return effect2;
					}
					effect.Reset(level_id);
					return effect;
				}
				else
				{
					i++;
				}
			}
			return null;
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x0008D2A2 File Offset: 0x0008B4A2
		private Effect GetEffect(string fx_name, string level_id)
		{
			return this.GetEffect(fx_name, level_id);
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x0008D2AC File Offset: 0x0008B4AC
		private void CopyFrom(EffectManager m)
		{
			this.Reset();
			for (int i = 0; i < m.mEffects.Count; i++)
			{
				this.mEffects[i].CopyFrom(m.mEffects[i]);
			}
		}

		// Token: 0x04001648 RID: 5704
		protected List<Effect> mEffects = new List<Effect>();
	}
}
