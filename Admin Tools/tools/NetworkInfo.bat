@echo off
echo [Open Ports & Connections]
netstat -ano
echo.
echo [IP & Adapter Info]
ipconfig /all
echo.
echo [Ping Test - Google DNS]
ping 8.8.8.8 -n 4
echo.
echo [Traceroute - Google DNS]
tracert 8.8.8.8
echo.
echo [DNS Lookup - Microsoft Domain]
nslookup www.microsoft.com
echo.
echo [Wi-Fi SSID (if applicable)]
netsh wlan show interfaces | findstr /R "^....SSID"
echo.
echo [Adapter List - Connected Only]
powershell "Get-CimInstance Win32_NetworkAdapter | Where-Object { $_.NetConnectionStatus -eq 2 } | Format-Table Name, MACAddress"