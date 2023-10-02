# ac6-parts-db
SQLite database for Armored Core 6 parts &amp; stats

## Usage
The database is stored in `data/ArmoredCore6.db`. It can be queried using any SQLite client, or the command line tool `sqlite3`. To recreate the database, run `./refresh-db.sh`. This also regenerates the [SqlHydra types](./WikidotScraper/DatabaseTypes.fs).

## TODO

- [x] Create the database and tables
- [ ] Scrape the wikidot pages for the data and store in memory
    - [ ] All frame parts (head, core, arms, legs)
    - [ ] All internal parts (FCS, generator, booster)
    - [ ] All weapons
- [ ] Write the data to the database
- [ ] Expand columns to include all stats
- [ ] Create a simple web interface to query the database
- [ ] Create a build randomizer that will pick a valid build using the stats in the database

## Credits
Information stored in the database is sourced from the [Armored Core 6 Wikidot](http://armoredcore6.wikidot.com/wiki:armored-core-vi:fires-of-rubicon-parts). All credit to the original authors & editors on those pages for providing the data used here.
