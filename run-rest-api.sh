#!/usr/bin/env bash
sh ./wait-for-it.sh $DATABASE_HOST 5432 "dotnet RestRecipeApp.Api.dll"
