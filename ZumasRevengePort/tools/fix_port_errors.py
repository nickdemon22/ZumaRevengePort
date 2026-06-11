"""Bulk fixes for ZumasRevenge MonoGame port compile errors."""
import re
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]

GAMEPAD_ADD = [
    (r"(mAllowedButtons|mDirectionButtons)\.Add\((\d+)\)", r"\1.Add((GamepadButton)\2)"),
]

ENUM_CASTS = [
    (r"\.SetScrollMode\((\d+)\)", r".SetScrollMode((ScrollWidget.ScrollMode)\1)"),
    (r"\.SetMasking\((\d+)\)", r".SetMasking((Graphics3D.EMaskMode)\1)"),
    (r"OpenFile\(([^,]+),\s*(\d+)\)", r"OpenFile(\1, (System.IO.FileMode)\2)"),
    (r"\.Seek\(([^,]+),\s*(\d+)\)", r".Seek(\1, (System.IO.SeekOrigin)\2)"),
    (r"GamepadButtonDown\((\d+)\s*,", r"GamepadButtonDown((GamepadButton)\1,"),
    (r"GamepadAxisMove\(([^,]+),\s*(\d+)\s*,", r"GamepadAxisMove(\1, (GamepadAxis)\2,"),
    (r"SetCursor\((\d+)\)", r"SetCursor((ECURSOR)\1)"),
    (r"KeyDown\((\d+)\)", r"KeyDown((KeyCode)\1)"),
    (r"KeyUp\((\d+)\)", r"KeyUp((KeyCode)\1)"),
    (r"mKeyDown\[(\d+)\]", r"mKeyDown[(int)(KeyCode)\1]"),
    (r"(?<!\(int\))GetLanguage\(\)\s*==\s*(\d+)", r"(int)Localization.GetCurrentLanguage() == \1"),
    (r"(?<!\(int\))GetCurrentLanguage\(\)\s*==\s*(\d+)", r"(int)Localization.GetCurrentLanguage() == \1"),
    (r"(?<!\(int\))GetCurrentLanguage\(\)\s*!=\s*(\d+)", r"(int)Localization.GetCurrentLanguage() != \1"),
    (r"(?<!\(int\))Localization\.GetCurrentLanguage\(\)\s*==\s*(\d+)", r"(int)Localization.GetCurrentLanguage() == \1"),
    (r"(?<!\(int\))Localization\.GetCurrentLanguage\(\)\s*!=\s*(\d+)", r"(int)Localization.GetCurrentLanguage() != \1"),
    (r"theKey\s*==\s*(\d+)", r"(int)theKey == \1"),
    (r"theKeyCode\s*==\s*(\d+)", r"(int)theKeyCode == \1"),
    (r"key\s*==\s*(\d+)", r"(int)key == \1"),
    (r"theCode\s*==\s*(\d+)", r"(int)theCode == \1"),
]

TRY_PARSE = [
    (r",\s*167\s*,", r", NumberStyles.Float, "),
    (r",\s*511\s*,", r", NumberStyles.Any, "),
    (r"double\.Parse\(([^,]+),\s*167\s*,", r"double.Parse(\1, NumberStyles.Float, "),
    (r"int\.Parse\(([^,]+),\s*167\s*,", r"int.Parse(\1, NumberStyles.Integer, "),
    (r"float\.Parse\(([^,]+),\s*167\s*,", r"float.Parse(\1, NumberStyles.Float, "),
    (r", CultureInfo\.InvariantCulture, ref ", r", CultureInfo.InvariantCulture, out "),
    (r"NumberStyles\.Float, CultureInfo\.InvariantCulture, ref ", r"NumberStyles.Float, CultureInfo.InvariantCulture, out "),
]

# Fix Common.RotatePoint ref forwarding
COMMON_ROTATE_FIX = (
    "public static void RotatePoint(float pAngle, ref float x, ref float y, float cx, float cy) => SexyFramework.Common.RotatePoint(pAngle, ref x, ref y, cx, cy);"
)

def fix_file(path: Path) -> bool:
    t = path.read_text(encoding="utf-8")
    o = t

    for pat, repl in GAMEPAD_ADD + ENUM_CASTS + TRY_PARSE:
        t = re.sub(pat, repl, t)

    if path.name == "Common.cs":
        t = t.replace(
            "SexyFramework.Common.RotatePoint(pAngle, x, y, cx, cy)",
            "SexyFramework.Common.RotatePoint(pAngle, ref x, ref y, cx, cy)",
        )
        t = t.replace(
            "SexyFramework.Common.DistFromPointToLine(line_p1, line_p2, p, t)",
            "SexyFramework.Common.DistFromPointToLine(line_p1, line_p2, p, ref t)",
        )

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
    for c in changed[:30]:
        print(f"  {c}")
    if len(changed) > 30:
        print(f"  ... and {len(changed) - 30} more")


if __name__ == "__main__":
    main()
