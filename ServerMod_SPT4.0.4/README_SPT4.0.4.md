# BackdoorBandit - SPT 4.0.4 Server Mod

This is the SPT 4.0.4 compatible server-side component of the BackdoorBandit mod.

## What This Does

The server mod adds three custom items to SPT 4.0.4:
1. **12/70 Door-Breaching Round** - Special shotgun ammunition for breaching doors
2. **12/70 Door-Breaching 5-Round Box** - Ammo pack containing 5 breach rounds
3. **C4 Explosive** - Explosive charge for breaching reinforced doors

## Features

- Adds custom items to the game database
- Adds breach rounds to all 12-gauge weapons
- Adds trader assortments to Mechanic (LL1):
  - Door Breacher Round for 10,000 Roubles
  - Door Breacher Round for 1x Electric Wire (barter)
- Adds hideout crafting recipe for C4 Explosive at Workbench Level 2

## Installation

1. Build the project:
   ```bash
   dotnet build DoorBreacher.csproj -c Release
   ```

2. Copy the entire `bin/Release/DoorBreacher/` folder to your SPT installation:
   ```
   SPT_Install/user/mods/DoorBreacher-2.0.0/
   ```

3. Start your SPT server

## Project Structure

```
ServerMod_SPT4.0.4/
├── DoorBreaćherMod.cs          # Main mod entry point with ModMetadata
├── DoorBreacher.csproj          # Project file
├── database/
│   └── templates/
│       ├── items.json           # Custom item definitions
│       └── craftingItem.json    # Hideout crafting recipes
├── bundles/                      # Unity asset bundles
│   ├── C4Explosive.bundle
│   ├── DoorBreacher.bundle
│   └── DoorBreacherBox.bundle
└── bundles.json                  # Bundle manifest
```

## Changes from SPT 3.x

### TypeScript → C#
- Migrated from Node.js/TypeScript to .NET 9/C#
- Converted from CommonJS modules to .NET assemblies (DLL)
- Replaced tsyringe DI with native SPT DI container

### API Changes
- `package.json` → `ModMetadata` record class
- `container.resolve()` → Constructor injection with `[Injectable]`
- `this.db = container.resolve<DatabaseServer>().getTables()` → `_databaseService.GetTables()`
- JSON5 configs → Standard JSON (no comments, no trailing commas)

### Build System
- npm/webpack → dotnet build
- Output: `bin/Release/DoorBreacher/` (ready to copy to SPT mods folder)

## Requirements

- .NET 9 SDK
- SPT 4.0.4 server
- Compatible with the client-side BepInEx plugin (BackdoorBandit)

## Notes

- The mod uses `dynamic` types for database manipulation to handle SPT's complex JSON structures
- All custom items use bundles for 3D models and textures
- The C4 crafting recipe requires: 2x TNT, 1x Bundle of Wires, 1x Grenade Fuze, 1x Military Circuit Board, and Pliers Elite

## Troubleshooting

If the mod doesn't load:
1. Check SPT server logs for errors
2. Verify the ModGuid is unique: `com.dvize.doorbreacher`
3. Ensure all bundle files are present in the `bundles/` folder
4. Verify `items.json` and `craftingItem.json` are valid JSON (no comments)

## Credits

- **Author**: dvize
- **Contributors**: Props, Tron, MakerMacher
- **Migration to SPT 4.0.4**: Assisted by Claude
