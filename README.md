# Unity_VersionFileBuilder

Write a Version.txt when the project succeeded in build.

## Import to Your Project

Paste this code into PackageManager > Add package from git URL.

```
https://github.com/XJINE/Unity_VersionFileBuilder.git?path=/Packages/VersionFileBuilder
```

### Dependencies

You have to import following assets to use this asset.

- https://github.com/XJINE/Unity_TextFileReadWriter

## How to Use

1. Set the version number in ``ProjectSettings > Player > Version``.
2. Write Version.txt when the project succeeded in build.
    - Alert and stop building when the existing version is newer than current.