# Checkin Memory
This is a simple heartbeat snapshot server primarily using memory storage.

The purpose of this project is to view at a high level devices which are "alive" and their health. As the server will be RESTful and allow devices to submit their heartbeat through HTTP requests (for example curl).

## What this is not
A fully featured network monitoring tool. I intended to create something fairly lightweight and easy to deploy. This is a key advantage of memory storage, it is schema less so we do not need to provision the data store.

# Getting started
The code folder is located in `src`. If you intend to develop and run, it is suggested you use that as your working directory.

## Build
This project is built in dotnet core, so you will need to install .NET 6 SDK and Runtime. Instructions can be found here: https://dotnet.microsoft.com/en-us/download

You can then build the project with `dotnet build`. 

## Test
Running `dotnet test` will run all unit tests (located in Checkin.Tests).

If you wish to run code coverage, you will need to install reportgenerator: https://github.com/danielpalme/ReportGenerator. You can then run the `./generate-coverage.sh` shell script. 

This will only work in Linux systems for now, however if you inspect the coverage shell script you can see the dotnet and reportgenerator commands which should also work on Windows/macOS systems.
