#!/bin/bash

cd neol
dotnet publish -c $1 -p:SelfContained=true
cd ..