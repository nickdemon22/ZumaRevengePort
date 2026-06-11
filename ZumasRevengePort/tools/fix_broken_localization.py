"""Undo double-applied Localization.GetCurrentLanguage() regex fixes."""
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]

REPLACEMENTS = [
    (
        "(int)Localization.(int)(int)Localization.GetCurrentLanguage()",
        "(int)Localization.GetCurrentLanguage()",
    ),
    (
        "(int)Localization.(int)Localization.GetCurrentLanguage()",
        "(int)Localization.GetCurrentLanguage()",
    ),
]


def main():
    n = 0
    for p in ROOT.rglob("*.cs"):
        if "obj" in p.parts or "tools" in p.parts:
            continue
        t = p.read_text(encoding="utf-8")
        o = t
        for old, new in REPLACEMENTS:
            t = t.replace(old, new)
        if t != o:
            p.write_text(t, encoding="utf-8")
            n += 1
            print(p.relative_to(ROOT))
    print(f"Fixed {n} files")


if __name__ == "__main__":
    main()
