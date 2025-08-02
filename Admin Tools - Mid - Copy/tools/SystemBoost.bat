@echo off

echo Switching to High Performance mode...
powercfg /setactive SCHEME_MIN

echo Setting Processor Performance Boost Mode to Aggressive...
powercfg -setacvalueindex SCHEME_MIN SUB_PROCESSOR PERFBOOSTMODE 2
powercfg -setdcvalueindex SCHEME_MIN SUB_PROCESSOR PERFBOOSTMODE 2

echo Applying updated power settings...
powercfg /S SCHEME_MIN

echo Disabling Windows Defender scheduled tasks...

:: Use full, correct task names
schtasks /Change /TN "\Microsoft\Windows\Windows Defender\Windows Defender Scheduled Scan" /Disable
schtasks /Change /TN "\Microsoft\Windows\Windows Defender\Windows Defender Verification" /Disable
schtasks /Change /TN "\Microsoft\Windows\Windows Defender\Windows Defender Cache Maintenance" /Disable
schtasks /Change /TN "\Microsoft\Windows\Windows Defender\Windows Defender Cleanup" /Disable

echo All Defender tasks disabled and system fully optimized.