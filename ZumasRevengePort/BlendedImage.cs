using System;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	// Token: 0x020000BE RID: 190
	public class BlendedImage : IDisposable
	{
		// Token: 0x06000E14 RID: 3604 RVA: 0x0008EB99 File Offset: 0x0008CD99
		public void Dispose()
		{
			this.DeleteImages();
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x0008EBA4 File Offset: 0x0008CDA4
		public void DeleteImages()
		{
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					this.mImages[i, j].Dispose();
					this.mImages[i, j] = null;
				}
			}
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x0008EBEC File Offset: 0x0008CDEC
		public BlendedImage(MemoryImage theImage, Rect theSrcRect, bool rotated)
		{
			int num = theSrcRect.mWidth + 3;
			int num2 = theSrcRect.mHeight + 3;
			MemoryImage memoryImage = new MemoryImage();
			memoryImage.Create(num, num2);
			SexyGraphics graphics = new SexyGraphics(memoryImage);
			graphics.DrawImage(theImage, 1, 1, theSrcRect);
			graphics.ClearRenderContext();
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					MemoryImage memoryImage2 = new MemoryImage();
					memoryImage2.Create(num, num2);
					SexyGraphics graphics2 = new SexyGraphics(memoryImage2);
					if (!rotated)
					{
						graphics2.DrawImageF(memoryImage, (float)i / 4f * 0.9f + 0.1f, (float)j / 4f * 0.9f + 0.1f);
					}
					else
					{
						graphics2.DrawImageRotatedF(memoryImage, (float)i / 4f * 0.9f + 0.1f, (float)j / 4f * 0.9f + 0.1f, -1.5707000494003296);
					}
					this.mImages[i, j] = memoryImage2;
					graphics2.ClearRenderContext();
				}
			}
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x0008ED14 File Offset: 0x0008CF14
		public void Draw(SexyGraphics g, float x, float y)
		{
			int num = (int)(((double)x - Math.Floor((double)x)) * 4.0);
			int num2 = (int)(((double)y - Math.Floor((double)y)) * 4.0);
			g.DrawImage(this.mImages[num, num2], (int)x, (int)y);
		}

		// Token: 0x040016CE RID: 5838
		protected const int NUM_BLENDS = 4;

		// Token: 0x040016CF RID: 5839
		protected Image[,] mImages = new Image[4, 4];
	}
}
