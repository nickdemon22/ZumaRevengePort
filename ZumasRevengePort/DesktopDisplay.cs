using System;
using Microsoft.Xna.Framework.Graphics;

namespace ZumasRevenge
{
	internal static class DesktopDisplay
	{
		public const int GameWidth = 1066;
		public const int GameHeight = 640;

		public static readonly int[][] ResolutionPresets = new int[][]
		{
			new int[] { 0, 0 },
			new int[] { 1066, 640 },
			new int[] { 1280, 720 },
			new int[] { 1600, 900 },
			new int[] { 1920, 1080 }
		};

		public static readonly string[] ResolutionLabels = new string[]
		{
			"Авто",
			"1066 x 640",
			"1280 x 720",
			"1600 x 900",
			"1920 x 1080"
		};

		public static void GetMonitorSize(out int width, out int height)
		{
			DisplayMode mode = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
			width = mode.Width;
			height = mode.Height;
		}

		public static void FitAspect(int gameW, int gameH, int maxW, int maxH, out int outW, out int outH)
		{
			float gameAspect = (float)gameW / gameH;
			float maxAspect = (float)maxW / maxH;
			if (maxAspect > gameAspect)
			{
				outH = maxH;
				outW = Math.Max(1, (int)(maxH * gameAspect));
			}
			else
			{
				outW = maxW;
				outH = Math.Max(1, (int)(maxW / gameAspect));
			}
		}

		public static void ResolvePreset(int presetIndex, bool fullscreen, out int width, out int height)
		{
			if (presetIndex < 0 || presetIndex >= ResolutionPresets.Length)
			{
				presetIndex = 0;
			}
			int[] preset = ResolutionPresets[presetIndex];
			if (preset[0] <= 0 || preset[1] <= 0)
			{
				GetMonitorSize(out width, out height);
				if (fullscreen)
				{
					FitAspect(GameWidth, GameHeight, width, height, out width, out height);
				}
				return;
			}
			width = preset[0];
			height = preset[1];
		}
	}
}
