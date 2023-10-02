# Armored Core 6
SQLite database for Armored Core 6 parts &amp; stats. I've always wanted to write a build creator or randomizer as a side project, so I'm starting with the database. This is a work in progress.

### Why not just use a spreadsheet?
I'm a lazy developer and would rather write a scraper than manually copy-paste data. Also, I wanted to learn [F#](https://fsharp.org/) and this seemed like a good project to do that with.

GitHub Copilot is also writing most of this README for me, so that's pretty cool.

### Credits
Information stored in the database is sourced from the [Armored Core 6 Wikidot](http://armoredcore6.wikidot.com/wiki:armored-core-vi:fires-of-rubicon-parts) as well as the [AC-3000 WRECKER arms page from Fandom](https://armoredcore.fandom.com/wiki/AC-3000_WRECKER). All credit to the original authors & editors on those pages for providing the data used here.


## Usage
Running the scraper (requires [.NET 7+](https://dotnet.microsoft.com/download/dotnet/7.0)):
```bash
$ dotnet run --project WikidotScraper/WikidotScraper.fsproj
```

The database is stored in `data/ArmoredCore6.db` and is inserted to from the scraper. It can be queried using any SQLite client, or the command line tool `sqlite3`. 

To re-initialize the database, run `./manage-db.sh clean` or delete the file then run `./manage-db.sh schema`. This creates the tables & regenerates the [SqlHydra types](./WikidotScraper/DatabaseTypes.fs).


## TODO

- [x] Create the database and tables
- [x] Scrape the wikidot pages for the data and store in memory
    - [x] All frame parts (head, core, arms, legs)
    - [x] All internal parts (FCS, generator, booster)
    - [x] All weapons
    - [ ] Core expansions
- [x] Write the data to the database
- [ ] Expand columns to include all stats
- [ ] Create a simple web interface to query the database
- [ ] Create a build randomizer that will pick a valid build using the stats in the database
