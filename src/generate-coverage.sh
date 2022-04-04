#!/bin/bash

rm -rf Checkin.Tests/TestResults/*
dotnet test --collect:"XPlat Code Coverage" /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura 
reportgenerator -reports:"./Checkin.Tests/TestResults/**/*/coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html  