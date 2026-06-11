"""Second-pass fixes for remaining compile errors."""
import re
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]

GLOBAL_REPLACEMENTS = [
    (r"string\.Compare\(([^,]+),\s*([^,]+),\s*1\)", r"string.Compare(\1, \2, true)"),
    (
        r"SexyFramework\.Common\.RIGHT_BUTTON",
        r"JCommon.RIGHT_BUTTON",
    ),
    (
        r"SexyFramework\.Common\.DOUBLE_RIGHT_BUTTON",
        r"JCommon.DOUBLE_RIGHT_BUTTON",
    ),
    (
        r"SexyFramework\.Common\.StringToInt\(theString, theIntVal\)",
        r"SexyFramework.Common.StringToInt(theString, ref theIntVal)",
    ),
    (
        r"SexyFramework\.Common\.StringToDouble\(aTempString, theDouble\)",
        r"SexyFramework.Common.StringToDouble(aTempString, ref theDouble)",
    ),
    (r"PressButtonDown\((\d+),", r"PressButtonDown((GamepadButton)\1,"),
    (r"PressButton\((\d+),", r"PressButton((GamepadButton)\1,"),
    (r"PressButtonUp\((\d+),", r"PressButtonUp((GamepadButton)\1,"),
]

AUTO_MONKEY_SWITCH = re.compile(
    r"(public string GetButtonString\(GamepadButton button\)\s*\{[^}]*?switch \(button\)\s*\{)(.*?)(\n\t\t\})",
    re.DOTALL,
)


def fix_auto_monkey_switch(text: str) -> str:
    m = AUTO_MONKEY_SWITCH.search(text)
    if not m:
        return text
    body = m.group(2)
    body = re.sub(r"\n\t\t\tcase (\d+):", r"\n\t\t\tcase (GamepadButton)\1:", body)
    return text[: m.start(2)] + body + text[m.end(2) :]


def fix_base_xna(path: Path, text: str) -> str:
    if path.name != "BaseXNARenderDevice.cs":
        return text
    text = text.replace(
        "this.mDevice.GraphicsDevice.Clear(4, Color.White, 0f, 0);",
        "this.mDevice.GraphicsDevice.Clear((Microsoft.Xna.Framework.Graphics.ClearOptions)4, XnaColor.White, 0f, 0);",
    )
    text = text.replace(
        "this.mDevice.GraphicsDevice.Clear(new Color(inColor.mRed, inColor.mGreen, inColor.mBlue, inColor.mAlpha));",
        "this.mDevice.GraphicsDevice.Clear(new XnaColor(inColor.mRed, inColor.mGreen, inColor.mBlue, inColor.mAlpha));",
    )
    text = text.replace(
        "depthStencilState.StencilPass = passFunc;",
        "depthStencilState.StencilPass = (Microsoft.Xna.Framework.Graphics.StencilOperation)passFunc;",
    )
    text = text.replace(
        "depthStencilState.StencilFail = failFunc;",
        "depthStencilState.StencilFail = (Microsoft.Xna.Framework.Graphics.StencilOperation)failFunc;",
    )
    text = text.replace(
        "color = new Color(vector);",
        "color = new XnaColor(vector);",
    )
    text = text.replace(
        "this.mDevice.SupportedOrientations = 3;",
        "this.mDevice.SupportedOrientations = (Microsoft.Xna.Framework.DisplayOrientation)3;",
    )
    blend_props = [
        "AlphaDestinationBlend",
        "ColorDestinationBlend",
        "AlphaSourceBlend",
        "ColorSourceBlend",
    ]
    for prop in blend_props:
        text = re.sub(
            rf"(\.{prop} = )(\d+);",
            r"\1(Microsoft.Xna.Framework.Graphics.Blend)\2;",
            text,
        )
    text = re.sub(
        r"rasterizerState\.CullMode = 0;",
        "rasterizerState.CullMode = (Microsoft.Xna.Framework.Graphics.CullMode)0;",
        text,
    )
    text = re.sub(
        r"rasterizerState\.FillMode = fillMode;",
        "rasterizerState.FillMode = (Microsoft.Xna.Framework.Graphics.FillMode)fillMode;",
        text,
    )
    text = re.sub(
        r"rasterizerState\.CullMode = cullMode;",
        "rasterizerState.CullMode = (Microsoft.Xna.Framework.Graphics.CullMode)cullMode;",
        text,
    )
    # XNA API assignments: Property = intLiteral (single line)
    xna_enum_props = [
        ("DepthBufferFunction", "CompareFunction"),
        ("StencilFunction", "CompareFunction"),
        ("SurfaceFormat", "SurfaceFormat"),
        ("DepthFormat", "DepthFormat"),
        ("PresentationInterval", "PresentInterval"),
        ("MultiSampleCount", "int"),  # skip
    ]
    for prop, enum_name in xna_enum_props:
        if enum_name == "int":
            continue
        text = re.sub(
            rf"(\.{prop} = )(\d+);",
            rf"\1(Microsoft.Xna.Framework.Graphics.{enum_name})\2;",
            text,
        )
    return text


def fix_unchecked_ulong(text: str) -> str:
    # negative int assigned to ulong without unchecked
    return re.sub(
        r"(=\s*)(-?\d{8,})(\s*;)",
        lambda m: f"{m.group(1)}unchecked((ulong){m.group(2)}){m.group(3)}"
        if int(m.group(2)) < 0
        else m.group(0),
        text,
    )


def fix_file(path: Path) -> bool:
    t = path.read_text(encoding="utf-8")
    o = t
    for pat, repl in GLOBAL_REPLACEMENTS:
        t = re.sub(pat, repl, t)
    if path.name == "AutoMonkey.cs":
        t = fix_auto_monkey_switch(t)
    t = fix_base_xna(path, t)
    if path.name == "PopAnim.cs":
        t = fix_unchecked_ulong(t)
    if t != o:
        path.write_text(t, encoding="utf-8")
        return True
    return False


def main():
    changed = []
    for p in ROOT.rglob("*.cs"):
        if "obj" in p.parts or "tools" in p.parts:
            continue
        if fix_file(p):
            changed.append(p.relative_to(ROOT))
    print(f"Updated {len(changed)} files")


if __name__ == "__main__":
    main()
