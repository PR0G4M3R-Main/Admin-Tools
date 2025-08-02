@echo off
echo.
echo [Battery]
powercfg /batteryreport /output "%TEMP%\battery-report.html"
echo Battery report saved to: %TEMP%\battery-report.html
echo.
echo [Memory Modules]
powershell "Get-CimInstance Win32_PhysicalMemory | Format-Table BankLabel, Capacity, Speed, Manufacturer"
echo.
echo [Active Processes]
powershell "Get-Process | Sort-Object CPU -Descending | Select-Object Name, CPU, ID | Format-Table -AutoSize"
echo.
echo [Graphics Pipeline]
powershell "Get-CimInstance Win32_VideoController | Format-Table Name, DriverVersion, VideoModeDescription, CurrentRefreshRate"
echo.
echo [Startup Programs]
powershell "Get-CimInstance Win32_StartupCommand | Format-Table Name, Command, Location"
echo.