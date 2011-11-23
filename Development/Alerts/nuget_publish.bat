@ECHO OFF

REM *** PACK PROJECT ***
for /f %%a IN ('dir /b *.csproj') do call nuget pack %%a -Build -Symbols -Properties Configuration=Release

REM *** PUSH TO SERVER ***
for /f %%a IN ('dir /b *.nupkg') do call nuget push %%a

REM *** DELETING LOCAL COPIES ***
del *.nupkg

PAUSE