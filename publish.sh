#!/bin/bash

root=./src/Infusio/bin/Debug
rm -rf $root
dotnet clean
dotnet pack
for file in "${root}/infusio.*.nupkg"; do
	dotnet nuget push $file -k $NUGET_API_KEY -s https://www.nuget.org
done
