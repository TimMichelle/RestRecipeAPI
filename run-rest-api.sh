#!/usr/bin/env bash
sh ./wait-for-it.sh database 5432 "dotnet RestRecipeApp.Api.dll"