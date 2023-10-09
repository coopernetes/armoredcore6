#!/usr/bin/env bash
set -x
if [[ $# -eq 0 ]]; then
    echo "Usage: $0 [clean|schema]"
    exit 1
fi
if [[ $1 == "clean" ]]; then
    cat << EOF | sqlite3 ./data/ArmoredCore6.db
DELETE FROM parts_weapon WHERE part_id IS NOT NULL;
DELETE FROM parts_core_expansion WHERE part_id IS NOT NULL;
DELETE FROM parts_frame_arms WHERE part_id IS NOT NULL;
DELETE FROM parts_frame_core WHERE part_id IS NOT NULL;
DELETE FROM parts_frame_head WHERE part_id IS NOT NULL;
DELETE FROM parts_frame_legs WHERE part_id IS NOT NULL;
DELETE FROM parts_internal_boosters WHERE part_id IS NOT NULL;
DELETE FROM parts_internal_fcs WHERE part_id IS NOT NULL;
DELETE FROM parts_internal_generator WHERE part_id IS NOT NULL;
EOF
    exit 0
fi
if [[ $1 == "schema" ]]; then
    sqlite3 ./data/ArmoredCore6.db < schema.sql
    sqlite3 ./data/ArmoredCore6.db < test.sql
    dotnet clean WikidotScraper/WikidotScraper.fsproj
    exit 0
fi
