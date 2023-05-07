#!/usr/bin/env bash

cp -n RestRecipeApp.Api/appsettings.Example.json RestRecipeApp.Api/appsettings.json 2>/dev/null
sed -i 's|<APPLICATION_URL>|'"$APPLICATION_URL"'|g' RestRecipeApp.Api/appsettings.json
sed -i -e 's/<DB_HOST>/'"$DB_HOST"'/g' RestRecipeApp.Api/appsettings.json
sed -i -e 's/<DB_PORT>/'"$DB_PORT"'/g' RestRecipeApp.Api/appsettings.json
sed -i -e 's/<DB_DATABASENAME>/'"$DB_DATABASENAME"'/g' RestRecipeApp.Api/appsettings.json
sed -i -e 's/<DB_USERNAME>/'"$DB_USERNAME"'/g' RestRecipeApp.Api/appsettings.json
sed -i -e 's/<DB_PASSWORD>/'"$DB_PASSWORD"'/g' RestRecipeApp.Api/appsettings.json
  