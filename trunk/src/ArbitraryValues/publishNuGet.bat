:: This script assumes you've already set your NuGet API key:
:: http://nuget.org/account displays it and the command to set it

@echo off
%windir%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe ArbitraryValues.csproj /t:rebuild /property:Configuration=Release
C:\NuGet\NuGet pack ArbitraryValues.csproj -Prop Configuration=Release
C:\NuGet\NuGet push *.nupkg
del *.nupkg
pause