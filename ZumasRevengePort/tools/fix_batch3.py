"""Batch fix pass 3."""
import re
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]

REPLACEMENTS = [
    (r"\(ulong\)-16777216", r"unchecked((ulong)-16777216)"),
    (r"\(ulong\)\(\(ulong\)-16777216\)", r"unchecked((ulong)-16777216)"),
    (r"TryParse\(([^)]+),\s*NumberStyles\.[^,]+,\s*CultureInfo\.InvariantCulture,\s*ref ", r"TryParse(\1, NumberStyles.Float, CultureInfo.InvariantCulture, out "),
    (r"int\.TryParse\(([^)]+),\s*ref ", r"int.TryParse(\1, out "),
    (r"double\.TryParse\(([^)]+),\s*ref ", r"double.TryParse(\1, out "),
    (r"float\.TryParse\(([^)]+),\s*ref ", r"float.TryParse(\1, out "),
    (r"DrawPrimitive\(0U,\s*(\d+),", r"DrawPrimitive(0U, (Graphics3D.EPrimitiveType)\1,"),
    (r"SetCursor\((\d+)\)", r"SetCursor((ECURSOR)\1)"),
    (r"return 1;\s*\n\t\t\}\s*\n\t\t// Token: 0x06000AAE", r"return (ImagePredrawResult)1;\n\t\t}\n\t\t// Token: 0x06000AAE"),  # Gun only - fragile
    (r"ButtonState\.Pressed\s*==\s*1", r"ButtonState.Pressed == ButtonState.Pressed"),
    (r"\.State\s*==\s*1\b", r".State == TouchLocationState.Pressed"),
    (r"SoundState\.Stopped\s*==\s*0", r"(int)SoundState.Stopped == 0"),
    (r"MediaState\.Playing\s*==\s*1", r"(int)MediaState.Playing == 1"),
    (r"MediaState\.Stopped\s*!=\s*0", r"(int)MediaState.Stopped != 0"),
    (r"MediaState\.Playing\s*!=\s*0", r"(int)MediaState.Playing != 0"),
    (r"\.mElementType\s*==\s*(\d+)", r"(int).mElementType == \1"),
    (r"Buffer\.SetByte\(this\.mMuMuMode,\s*0,\s*0\)", r"this.mMuMuMode[0] = (char)0"),
    (r"switch \(this\.mCurrentLanguage\)", r"switch ((int)this.mCurrentLanguage)"),
    (r"Widget value = keyValuePair\.Value", r"SexyFramework.Widget.Widget value = keyValuePair.Value"),
    (r"PrimitiveType primitiveType = 0", r"PrimitiveType primitiveType = PrimitiveType.LineList"),
    (r"primitiveType = 0;", r"primitiveType = PrimitiveType.LineList;"),
    (r"primitiveType = 1;", r"primitiveType = PrimitiveType.TriangleList;"),
    (r"primitiveType = 3;", r"primitiveType = PrimitiveType.TriangleStrip;"),
    (r"\.Seek\(([^,]+),\s*(\d+)\)", r".Seek(\1, (System.IO.SeekOrigin)\2)"),
]

# BaseXNARenderDevice enum return casts
XNA_RETURN_FIXES = [
    ("return 0;", "return (Microsoft.Xna.Framework.Graphics.SurfaceFormat)0;"),
    ("return 1;", "return (Microsoft.Xna.Framework.Graphics.SurfaceFormat)1;"),
    ("return 3;", "return (Microsoft.Xna.Framework.Graphics.SurfaceFormat)3;"),
]

COMPARE_RETURNS = {f"return {i};": f"return (Microsoft.Xna.Framework.Graphics.CompareFunction){i};" for i in range(8)}
BLEND_RETURNS = {f"return {i};": f"return (Microsoft.Xna.Framework.Graphics.Blend){i};" for i in [0, 1, 2, 3, 4, 5, 6, 7, 12]}


def fix_base_xna(text: str) -> str:
    # Only patch inside specific methods by line-based heuristics
    lines = text.split("\n")
    in_get_xna_format = False
    in_compare = False
    in_blend = False
    out = []
    for line in lines:
        if "public SurfaceFormat GetXnaFormat" in line:
            in_get_xna_format = True
            in_compare = in_blend = False
        elif "public CompareFunction GetXNACompareFunc" in line:
            in_compare = True
            in_get_xna_format = in_blend = False
        elif "public Blend GetXNABlendMode" in line:
            in_blend = True
            in_get_xna_format = in_compare = False
        elif line.strip().startswith("public ") and "GetXna" not in line and "GetXNA" not in line:
            if in_get_xna_format or in_compare or in_blend:
                if line.strip() == "}":
                    in_get_xna_format = in_compare = in_blend = False

        if in_get_xna_format and line.strip().startswith("return "):
            m = re.match(r"\s*return (\d+);", line)
            if m:
                line = line.replace(f"return {m.group(1)};", f"return (Microsoft.Xna.Framework.Graphics.SurfaceFormat){m.group(1)};")
        elif in_compare and line.strip().startswith("return "):
            m = re.match(r"\s*return (\d+);", line)
            if m:
                line = line.replace(f"return {m.group(1)};", f"return (Microsoft.Xna.Framework.Graphics.CompareFunction){m.group(1)};")
        elif in_blend and line.strip().startswith("return "):
            m = re.match(r"\s*return (\d+);", line)
            if m:
                line = line.replace(f"return {m.group(1)};", f"return (Microsoft.Xna.Framework.Graphics.Blend){m.group(1)};")
        out.append(line)
    return "\n".join(out)


def fix_gun_return(text: str, path: Path) -> str:
    if path.name != "Gun.cs":
        return text
    return re.sub(
        r"(ImagePredrawResult PopAnimImagePredraw[^\{]+\{)\s*return 1;",
        r"\1\n\t\t\treturn (ImagePredrawResult)1;",
        text,
        count=1,
    )


def main():
    for p in ROOT.rglob("*.cs"):
        if "obj" in p.parts or "tools" in p.parts:
            continue
        t = p.read_text(encoding="utf-8")
        o = t
        for pat, repl in REPLACEMENTS:
            t = re.sub(pat, repl, t)
        if p.name == "BaseXNARenderDevice.cs":
            t = fix_base_xna(t)
        t = fix_gun_return(t, p)
        if t != o:
            p.write_text(t, encoding="utf-8")
            print(p.relative_to(ROOT))


if __name__ == "__main__":
    main()
