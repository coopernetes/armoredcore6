CREATE TABLE IF NOT EXISTS
parts_frame_head (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL,
    manufacturer TEXT NOT NULL,
    ap INTEGER NOT NULL,
    def_kinetic INTEGER,
    def_energy INTEGER,
    def_explosive INTEGER,
    def_avg INTEGER,
    attitude_stability INTEGER,
    system_recovery INTEGER,
    scan_distance INTEGER,
    scan_effect_duration REAL,
    weight INTEGER NOT NULL,
    en_load INTEGER NOT NULL,
    description TEXT NOT NULL,
    image BLOB
);

CREATE TABLE IF NOT EXISTS
parts_frame_core (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL,
    manufacturer TEXT NOT NULL,
    ap INTEGER NOT NULL,
    def_kinetic INTEGER,
    def_energy INTEGER,
    def_explosive INTEGER,
    def_avg INTEGER,
    attitude_stability INTEGER,
    booster_efficiency_adj INTEGER,
    generator_output_adj INTEGER,
    generator_supply_adj INTEGER,
    weight INTEGER NOT NULL,
    en_load INTEGER NOT NULL,
    description TEXT NOT NULL,
    image BLOB
);

CREATE TABLE IF NOT EXISTS
parts_frame_arms (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL,
    manufacturer TEXT NOT NULL,
    ap INTEGER NOT NULL,
    def_kinetic INTEGER,
    def_energy INTEGER,
    def_explosive INTEGER,
    def_avg INTEGER,
    arms_load_limit INTEGER,
    recoil_control INTEGER,
    firearms_spec INTEGER,
    melee_spec INTEGER,
    weight INTEGER NOT NULL,
    en_load INTEGER NOT NULL,
    description TEXT NOT NULL,
    image BLOB
);

CREATE TABLE IF NOT EXISTS
parts_frame_legs (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL,
    manufacturer TEXT NOT NULL,
    ap INTEGER NOT NULL,
    def_kinetic INTEGER,
    def_energy INTEGER,
    def_explosive INTEGER,
    def_avg INTEGER,
    attitude_stability INTEGER,
    load_limit INTEGER,
    jump_distance INTEGER,
    jump_height INTEGER,
    weight INTEGER NOT NULL,
    en_load INTEGER NOT NULL,
    description TEXT NOT NULL,
    image BLOB
);

CREATE TABLE IF NOT EXISTS
parts_internal_boosters (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL,
    manufacturer TEXT NOT NULL,
    thrust INTEGER,
    upward_thrust INTEGER,
    upward_en_consumption INTEGER,
    qb_thrust INTEGER,
    qb_jet_duration REAL,
    qb_en_consumption INTEGER,
    qb_reload_time REAL,
    qb_reload_ideal_weight INTEGER NOT NULL,
    ab_thrust INTEGER,
    ab_en_consumption INTEGER,
    melee_atk_thrust INTEGER,
    melee_atk_en_consumption INTEGER,
    weight INTEGER NOT NULL,
    en_load INTEGER NOT NULL,
    description TEXT NOT NULL,
    image BLOB
);

CREATE TABLE IF NOT EXISTS
parts_internal_fcs (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL,
    manufacturer TEXT NOT NULL,
    close_assist INTEGER,
    medium_assist INTEGER,
    long_assist INTEGER,
    avg_assist INTEGER,
    missile_correction INTEGER,
    multi_lock_correction INTEGER,
    weight INTEGER NOT NULL,
    en_load INTEGER NOT NULL,
    description TEXT NOT NULL,
    image BLOB
);

CREATE TABLE IF NOT EXISTS
parts_internal_generator (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL,
    manufacturer TEXT NOT NULL,
    en_capacity INTEGER,
    en_recharge INTEGER,
    supply_recovery INTEGER,
    post_recovery_en_supply INTEGER,
    energy_firearm_spec INTEGER,
    weight INTEGER NOT NULL,
    en_load INTEGER NOT NULL,
    description TEXT NOT NULL,
    image BLOB
);

CREATE TABLE IF NOT EXISTS
parts_weapon (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    slot TEXT,
    name TEXT NOT NULL,
    part_type TEXT,
    manufacturer TEXT NOT NULL,
    attack_power INTEGER,
    attack_power_multiplier INTEGER,
    impact INTEGER,
    impact_multiplier INTEGER,
    accum_impact INTEGER,
    accum_impact_multiplier INTEGER,
    blast_radius INTEGER,
    atk_heat_buildup INTEGER,
    direct_hit_adj INTEGER,
    recoil INTEGER,
    effective_range INTEGER,
    range_limt INTEGER,
    rapid_fire REAL,
    total_rounds INTEGER,
    reload REAL,
    cooling INTEGER,
    ammo_cost INTEGER,
    consecutive_hits INTEGER,
    weight INTEGER NOT NULL,
    en_load INTEGER NOT NULL,
    description TEXT NOT NULL,
    image BLOB
);