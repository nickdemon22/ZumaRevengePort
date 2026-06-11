import re
from pathlib import Path

sf_common = Path(__file__).resolve().parents[1] / "SexyFramework" / "Common.cs"
zuma_common = Path(__file__).resolve().parents[1] / "Common.cs"

text = sf_common.read_text(encoding="utf-8")
pat = re.compile(
    r"public static\s+([\w<>\[\],\s\?]+?)\s+(\w+)(?:<[^>]+>)?\s*\(([^)]*)\)\s*\{",
    re.MULTILINE,
)
forwards: list[str] = []
for m in pat.finditer(text):
    ret, name, args = m.group(1).strip(), m.group(2), m.group(3).strip()
    if name in ("gBrightBallColors", "size", "back", "front", "Reserve", "Resize", "CreateObjectArray"):
        continue
    if name in ("StrEquals",):
        continue
    if name == "DividePoly":
        forwards.append(
            "\t\tpublic static bool DividePoly(Vector2[] v, int n, Vector2[,] theTris, int theMaxTris, ref int theNumTris) => SexyFramework.Common.DividePoly(v, n, theTris, theMaxTris, ref theNumTris);"
        )
        continue
    sig_args = args.replace("this ", "")
    if "this " in args:
        plist = []
        for a in [x.strip() for x in args.split(",") if x.strip()]:
            if a.startswith("this "):
                plist.append(a.split()[-1])
            else:
                plist.append(a.rsplit(" ", 1)[-1])
        call = ", ".join(plist)
    else:
        argnames = []
        for a in [x.strip() for x in args.split(",") if x.strip()]:
            if a.startswith("ref ") or a.startswith("out "):
                argnames.append(a.split()[-1])
            else:
                argnames.append(a.rsplit(" ", 1)[-1])
        call = ", ".join(argnames)
    forwards.append(
        f"\t\tpublic static {ret} {name}({sig_args}) => SexyFramework.Common.{name}({call});"
    )

jeff_src = Path(__file__).resolve().parents[1] / "SexyFramework" / "JeffLib" / "Common.cs"
jeff_text = jeff_src.read_text(encoding="utf-8")
jeff_forwards = []
for m in pat.finditer(jeff_text):
    ret, name, args = m.group(1).strip(), m.group(2), m.group(3).strip()
    if name in ("RIGHT_BUTTON", "LEFT_BUTTON", "MTRAND_MAX"):
        continue
    if name in ("RotatePoint",):
        continue
    sig_args = args.replace("this ", "")
    if "this " in args:
        plist = []
        for a in [x.strip() for x in args.split(",") if x.strip()]:
            if a.startswith("this "):
                plist.append(a.split()[-1])
            else:
                plist.append(a.rsplit(" ", 1)[-1])
        call = ", ".join(plist)
    else:
        argnames = []
        for a in [x.strip() for x in args.split(",") if x.strip()]:
            if a.startswith("ref ") or a.startswith("out "):
                argnames.append(a.split()[-1])
            else:
                argnames.append(a.rsplit(" ", 1)[-1])
        call = ", ".join(argnames)
    jeff_forwards.append(
        f"\t\tpublic static {ret} {name}({sig_args}) => JeffLib.JCommon.{name}({call});"
    )

ext_forwards = [
    "\t\tpublic static int size<T>(List<T> list) => SexyFramework.Common.size(list);",
    "\t\tpublic static T back<T>(List<T> list) => SexyFramework.Common.back(list);",
    "\t\tpublic static T front<T>(List<T> list) => SexyFramework.Common.front(list);",
    "\t\tpublic static void Reserve<T>(List<T> list, int newSize) => SexyFramework.Common.Reserve(list, newSize);",
    "\t\tpublic static void Resize<T>(List<T> list, int newSize) => SexyFramework.Common.Resize(list, newSize);",
    "\t\tpublic static T[] CreateObjectArray<T>(int size) => SexyFramework.Common.CreateObjectArray<T>(size);",
]

block = (
    "\n\n\t\t// Auto-generated forwards (MonoGame port)\n"
    + "\n".join(ext_forwards)
    + "\n"
    + "\n".join(forwards)
    + "\n"
    + "\n".join(jeff_forwards)
    + "\n"
)

content = zuma_common.read_text(encoding="utf-8")
marker = "\n\n\t\t// Auto-generated forwards (MonoGame port)"
if marker in content:
    start = content.index(marker)
    end = content.rindex("\n\t}")
    content = content[:start] + block + content[end:]
else:
    content = content.replace("\n\t}\n}", block + "\n\t}\n}")

zuma_common.write_text(content, encoding="utf-8")
print(f"Wrote {len(forwards)} SexyFramework + {len(jeff_forwards)} JeffLib forwards")
