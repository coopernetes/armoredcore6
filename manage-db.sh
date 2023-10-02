#!/usr/bin/env bash
set -x
if [[ $# -eq 0 ]]; then
    echo "Usage: $0 [clean|schema]"
    exit 1
fi
if [[ $1 == "clean" ]]; then
    rm -f ./data/ArmoredCore6.db
    exit 0
fi
if [[ $1 == "schema" ]]; then
    rm -f ./data/ArmoredCore6.db
    sqlite3 ./data/ArmoredCore6.db < schema.sql
    sqlite3 ./data/ArmoredCore6.db < test.sql
    (cd WikidotScraper && dotnet clean)
    exit 0
fi
