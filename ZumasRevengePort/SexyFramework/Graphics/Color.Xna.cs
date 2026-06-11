using Microsoft.Xna.Framework;

namespace SexyFramework.Graphics
{
	public partial struct Color
	{
		public uint PackedValue => (uint)ToInt();

		public XnaColor ToXnaColor() => new XnaColor(mRed, mGreen, mBlue, mAlpha);

		public static implicit operator XnaColor(Color c) => c.ToXnaColor();

		public static implicit operator Color(XnaColor c) => new Color(c.R, c.G, c.B, c.A);
	}
}
