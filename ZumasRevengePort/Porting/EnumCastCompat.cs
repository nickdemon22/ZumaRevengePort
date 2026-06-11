using SexyFramework.Drivers;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace ZumasRevenge.Porting
{
	/// <summary>Decompiled code often compares enums to int literals; use these helpers where a cast is required.</summary>
	public static class EnumCastCompat
	{
		public static bool Eq(KeyCode key, int value) => (int)key == value;
		public static bool Eq(Localization.LanguageType lang, int value) => (int)lang == value;
		public static ScrollWidget.ScrollMode ScrollMode(int value) => (ScrollWidget.ScrollMode)value;
		public static Graphics3D.EMaskMode MaskMode(int value) => (Graphics3D.EMaskMode)value;
		public static GamepadButton GamepadButton(int value) => (GamepadButton)value;
	}
}
