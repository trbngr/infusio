#!/bin/bash
root=./src/Infusio/bin/Debug/
rm -rf $root
dotnet pack
for pkg in $root*.nupkg; do
	dotnet nuget push ${pkg} -k ${NUGET_API_KEY} -s https://www.nuget.org
done