@echo off
echo [CPU]
powershell "Get-CimInstance Win32_Processor | Format-Table Name, MaxClockSpeed, NumberOfCores"
echo.
echo [GPU]
powershell "Get-CimInstance Win32_VideoController | Format-Table Name, DriverVersion"
echo.
echo [RAM]
powershell "Get-CimInstance Win32_PhysicalMemory | Select-Object Manufacturer, Capacity, Speed | Format-Table"
echo [Installed RAM Total]
powershell "(Get-CimInstance Win32_ComputerSystem).TotalPhysicalMemory / 1GB"
echo.
echo [Motherboard]
powershell "Get-CimInstance Win32_BaseBoard | Format-Table Manufacturer, Product, Version"
echo.
echo [Storage]
powershell "Get-CimInstance Win32_DiskDrive | Format-Table Model, Size, InterfaceType"
echo.
echo [Monitor(s)]
powershell "Get-CimInstance Win32_DesktopMonitor | Select Name, ScreenWidth, ScreenHeight | Format-Table"
echo.
echo [Operating System]
powershell "Get-CimInstance Win32_OperatingSystem | Format-Table Caption, Version, BuildNumber"
echo.
echo [Network]
powershell "Get-CimInstance Win32_NetworkAdapter | Where-Object { $_.NetConnectionStatus -eq 2 } | Format-Table Name, MACAddress"
echo.
echo [Battery]
powershell "Get-CimInstance Win32_Battery | Format-Table Name, EstimatedChargeRemaining, BatteryStatus"
echo.
echo [BIOS]
powershell "Get-CimInstance Win32_BIOS | Format-Table Manufacturer, Version, ReleaseDate"
echo.