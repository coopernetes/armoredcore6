CREATE TABLE IF NOT EXISTS
parts_frame_head (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT,
    manufacturer TEXT,
    ap INTEGER,
    def_kinetic INTEGER,
    def_energy INTEGER,
    def_explosive INTEGER,
    def_avg INTEGER,
    attitude_stability INTEGER,
    system_recovery INTEGER,
    scan_distance INTEGER,
    scan_effect_duration REAL,
    weight INTEGER,
    en_load INTEGER,
    description TEXT,
    image BLOB
);

CREATE TABLE IF NOT EXISTS
parts_frame_core (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT,
    manufacturer TEXT,
    ap INTEGER,
    def_kinetic INTEGER,
    def_energy INTEGER,
    def_explosive INTEGER,
    def_avg INTEGER,
    attitude_stability INTEGER,
    booster_efficiency_adj INTEGER,
    generator_output_adj INTEGER,
    generator_supply_adj INTEGER,
    weight INTEGER,
    en_load INTEGER,
    description TEXT,
    image BLOB
);

CREATE TABLE IF NOT EXISTS
parts_frame_arms (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT,
    manufacturer TEXT,
    ap INTEGER,
    def_kinetic INTEGER,
    def_energy INTEGER,
    def_explosive INTEGER,
    def_avg INTEGER,
    arms_load_limit INTEGER,
    recoil_control INTEGER,
    firearms_spec INTEGER,
    melee_spec INTEGER,
    weight INTEGER,
    en_load INTEGER,
    description TEXT,
    image BLOB
);

CREATE TABLE IF NOT EXISTS
parts_frame_legs (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT,
    manufacturer TEXT,
    ap INTEGER,
    def_kinetic INTEGER,
    def_energy INTEGER,
    def_explosive INTEGER,
    def_avg INTEGER,
    attitude_stability INTEGER,
    load_limit INTEGER,
    jump_distance INTEGER,
    jump_height INTEGER,
    weight INTEGER,
    en_load INTEGER,
    description TEXT,
    image BLOB
);
