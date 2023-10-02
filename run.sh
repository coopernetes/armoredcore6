#!/usr/bin/env bash
set -x
./manage-db.sh clean
./manage-db.sh schema
dotnet run --project WikidotScraper/WikidotScraper.fsproj
sqlite3 data/ArmoredCore6.db < missing-data.sql
