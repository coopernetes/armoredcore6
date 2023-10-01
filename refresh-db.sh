#!/usr/bin/env bash
set -x
rm -f ./data/ArmoredCore6.db
sqlite3 ./data/ArmoredCore6.db < schema.sql
sqlite3 ./data/ArmoredCore6.db < test.sql
(cd WikidotScraper && dotnet clean)

