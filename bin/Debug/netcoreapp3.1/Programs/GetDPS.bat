@echo off
for /F "tokens=2 delims=," %%P in ('
    tasklist /SVC /FI "Services eq DPS" /FO CSV /NH
') do echo %%~P