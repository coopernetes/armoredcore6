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

CREATE TABLE IF NOT EXISTS
parts_internal_booster (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT,
    manufacturer TEXT,
    thrust INTEGER,
    upward_thrust INTEGER,
    upward_en_consumption INTEGER,
    qb_thrust INTEGER,
    qb_jet_duration REAL,
    qb_en_consumption INTEGER,
    qb_reload_time REAL,
    qb_reload_ideal_weight INTEGER,
    ab_thrust INTEGER,
    ab_en_consumption INTEGER,
    melee_atk_thrust INTEGER,
    melee_atk_en_consumption INTEGER,
    weight INTEGER,
    en_load INTEGER,
    description TEXT,
    image BLOB
);

CREATE TABLE IF NOT EXISTS
parts_internal_fcs (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT,
    manufacturer TEXT,
    close_assist INTEGER,
    medium_assist INTEGER,
    long_assist INTEGER,
    avg_assist INTEGER,
    missile_correction INTEGER,
    multi_lock_correction INTEGER,
    weight INTEGER,
    en_load INTEGER,
    description TEXT,
    image BLOB
);

CREATE TABLE IF NOT EXISTS
parts_internal_generator (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT,
    manufacturer TEXT,
    en_capacity INTEGER,
    en_recharge INTEGER,
    supply_recovery INTEGER,
    post_recovery_en_supply INTEGER,
    energy_firearm_spec INTEGER,
    weight INTEGER,
    en_load INTEGER,
    description TEXT,
    image BLOB
);