#!/usr/bin/env python3
"""Update the VersionInfo.cs file with the provided semantic version."""

from __future__ import annotations

import argparse
import re
import sys
from pathlib import Path


SEMVER_PATTERN = re.compile(r"^\d+\.\d+\.\d+$")


def parse_args() -> argparse.Namespace:
    parser = argparse.ArgumentParser(description="Update VersionInfo.cs with the given version.")
    parser.add_argument("version", help="Version in the format X.Y.Z")
    return parser.parse_args()


def validate_version(raw_version: str) -> str:
    version = raw_version.strip()
    if not SEMVER_PATTERN.match(version):
        raise ValueError("Version must match X.Y.Z (semantic version).")
    return version


def update_app_version(version: str) -> None:
    version_file = Path.cwd() / "AspNetUltimateBase.Presentation" / "VersionInfo.cs"

    if not version_file.exists():
        raise FileNotFoundError(f"Version file not found at {version_file}")

    template = (
        "namespace AspNetUltimateBase.Presentation;\n\n"
        "public static class VersionInfo\n"
        "{\n"
        f'    public const string AppVersion = "{version}";\n'
        "}\n"
    )

    try:
        current_content = version_file.read_text(encoding="utf-8").replace("\r\n", "\n").strip()
    except OSError as exc:
        raise OSError(f"Unable to read {version_file}: {exc}") from exc

    if current_content == template.strip():
        print(f"VersionInfo.cs already set to {version}.")
        return

    try:
        version_file.write_text(f"{template}\n", encoding="utf-8")
    except OSError as exc:
        raise OSError(f"Unable to update {version_file}: {exc}") from exc

    print(f"Updated VersionInfo.cs to {version}.")


def main() -> int:
    try:
        args = parse_args()
        version = validate_version(args.version)
        update_app_version(version)
    except Exception as exc:  # noqa: BLE001 - surface friendly error
        print(exc, file=sys.stderr)
        return 1
    return 0


if __name__ == "__main__":
    sys.exit(main())
