## How to compile into a .exe

Run the following command:

dotnet publish "Pathto\Admin Tools\core\core.csproj" -c Release -r win-x64 -o "Pathto\Admin Tools\core" --self-contained true /p:PublishSingleFile=true

Replace `Pathto` with the actual path to the Admin Tools directory.

### After Publishing:
- The **working .exe** will be in the `core` folder.
- You can safely delete the `bin` and `obj` folders.
- The `.exe` file is self-contained — everything needed is baked in.
- You can even move the `.exe` to your desktop and delete everything else if preferred.

Note: The `win-x64` folder will be generated as part of the process, but it's not needed unless you're debugging or doing deeper inspections.
