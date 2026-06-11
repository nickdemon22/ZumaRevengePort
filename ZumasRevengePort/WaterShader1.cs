using System;
using System.Collections.Generic;
using SexyFramework;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	// Token: 0x0200012B RID: 299
	public class WaterShader1 : Effect
	{
		// Token: 0x06000FD4 RID: 4052 RVA: 0x000A244C File Offset: 0x000A064C
		protected override void Init()
		{
			for (int i = 0; i < this.mImages.Count; i++)
			{
				if (this.mImages[i].mFileName.Length > 0)
				{
					if (this.mImages[i].mImage != null)
					{
						this.mImages[i].mImage.Dispose();
						this.mImages[i].mImage = null;
					}
				}
				else
				{
					GameApp.gApp.mResourceManager.DeleteResources(this.mImages[i].mResId);
				}
				this.mImages[i].mImage = null;
			}
			for (int j = 0; j < this.mImages.Count; j++)
			{
				WaterShaderImage waterShaderImage = this.mImages[j];
				if (waterShaderImage.mImage == null)
				{
					if (waterShaderImage.mFileName.Length > 0)
					{
						waterShaderImage.mImage = GameApp.gApp.GetImage(GameApp.gApp.GetResImagesDir() + waterShaderImage.mFileName, true, true, false);
					}
					else
					{
						SharedImageRef sharedImageRef = GameApp.gApp.mResourceManager.LoadImage(waterShaderImage.mResId);
						if (sharedImageRef != null)
						{
							waterShaderImage.mImage = (DeviceImage)sharedImageRef.GetImage();
						}
					}
				}
			}
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x000A258D File Offset: 0x000A078D
		public override void Update()
		{
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x000A2590 File Offset: 0x000A0790
		public override void DrawUnderBackground(SexyGraphics g)
		{
			bool flag = GameApp.gApp.ShadersSupported();
			if (!GameApp.gApp.mLoadingThreadStarted || !GameApp.gApp.mLoadingThreadCompleted)
			{
			}
			flag = false;
			for (int i = 0; i < this.mImages.Count; i++)
			{
				if (!flag || this.mImages[i].mBypass)
				{
					int num = this.mImages[i].mScale ? Common._S(this.mImages[i].mX) : Common._DS(this.mImages[i].mX - 160);
					int num2 = this.mImages[i].mScale ? Common._S(this.mImages[i].mY) : Common._DS(this.mImages[i].mY);
					g.DrawImage(this.mImages[i].mImage, num, num2);
				}
			}
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x000A26A0 File Offset: 0x000A08A0
		public override void SetParams(string key, string value)
		{
			if (key.IndexOf("image") != 0 && key.IndexOf("resid") != 0)
			{
				return;
			}
			int i;
			for (i = 5; i < key.Length; i++)
			{
				try
				{
					float.Parse(string.Concat(key[i]));
				}
				catch (Exception)
				{
					break;
				}
			}
			string text = key.Substring(5, i - 5);
			int num = Common.StrToInt(text);
			WaterShaderImage waterShaderImage = null;
			for (int j = 0; j < this.mImages.Count; j++)
			{
				if (this.mImages[j].mId == num)
				{
					waterShaderImage = this.mImages[j];
					break;
				}
			}
			if (waterShaderImage == null)
			{
				waterShaderImage = new WaterShaderImage();
				this.mImages.Add(waterShaderImage);
				waterShaderImage.mId = num;
			}
			char c = key[key.Length - 1];
			if (text.Length + 5 == key.Length)
			{
				if (key.IndexOf("image") == 0)
				{
					waterShaderImage.mFileName = value;
					return;
				}
				waterShaderImage.mResId = "IMAGE_LEVELS_" + value;
				return;
			}
			else
			{
				if (c == 'x' || c == 'X')
				{
					waterShaderImage.mX = Common.StrToInt(value);
					return;
				}
				if (c == 'y' || c == 'Y')
				{
					waterShaderImage.mY = Common.StrToInt(value);
					return;
				}
				if (Common.StrEquals(key.Substring(text.Length + 5, key.Length), "scale"))
				{
					waterShaderImage.mScale = bool.Parse(value);
					return;
				}
				if (Common.StrEquals(key.Substring(text.Length + 5, key.Length), "bypass"))
				{
					waterShaderImage.mBypass = bool.Parse(value);
				}
				return;
			}
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x000A2848 File Offset: 0x000A0A48
		public override void NukeParams()
		{
			for (int i = 0; i < this.mImages.Count; i++)
			{
				if (this.mImages[i].mFileName.Length > 0)
				{
					if (this.mImages[i].mImage != null)
					{
						this.mImages[i].mImage.Dispose();
						this.mImages[i].mImage = null;
					}
				}
				else
				{
					GameApp.gApp.mResourceManager.DeleteResources(this.mImages[i].mResId);
				}
			}
			this.mImages.Clear();
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x000A28EF File Offset: 0x000A0AEF
		public override string GetName()
		{
			return "WaterShader1";
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x000A28F6 File Offset: 0x000A0AF6
		public override void CopyFrom(Effect e)
		{
		}

		// Token: 0x040019E7 RID: 6631
		protected List<WaterShaderImage> mImages = new List<WaterShaderImage>();
	}
}
