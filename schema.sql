CREATE TABLE IF NOT EXISTS
parts_frame_head (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL,
    manufacturer TEXT NOT NULL,
    ap INT NOT NULL,
    anti_kinetic_defense INT NOT NULL,
    anti_energy_defense INT NOT NULL,
    anti_explosive_defense INT NOT NULL,
    average_defense DECIMAL(10,5) NOT NULL,
    attitude_stability INT NOT NULL,
    system_recovery INT NOT NULL,
    scan_distance INT NOT NULL,
    scan_effect_duration DECIMAL(10,5) NOT NULL,
    weight INT NOT NULL,
    en_load INT NOT NULL,
    description TEXT NOT NULL,
    image BLOB
);

CREATE TABLE IF NOT EXISTS
parts_frame_core (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL,
    manufacturer TEXT NOT NULL,
    ap INT NOT NULL,
    anti_kinetic_defense INT NOT NULL,
    anti_energy_defense INT NOT NULL,
    anti_explosive_defense INT NOT NULL,
    average_defense DECIMAL(10,5) NOT NULL,
    attitude_stability INT NOT NULL,
    booster_efficiency_adjustment INT NOT NULL,
    generator_output_adjustment INT NOT NULL,
    generator_supply_adjustment INT NOT NULL,
    weight INT NOT NULL,
    en_load INT NOT NULL,
    description TEXT NOT NULL,
    image BLOB
);

CREATE TABLE IF NOT EXISTS
parts_frame_arms (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL,
    manufacturer TEXT NOT NULL,
    ap INT NOT NULL,
    anti_kinetic_defense INT NOT NULL,
    anti_energy_defense INT NOT NULL,
    anti_explosive_defense INT NOT NULL,
    average_defense DECIMAL(10,5) NOT NULL,
    arms_load_limit INT NOT NULL,
    recoil_control INT NOT NULL,
    firearms_specialization INT NOT NULL,
    melee_specialization INT NOT NULL,
    weight INT NOT NULL,
    en_load INT NOT NULL,
    description TEXT NOT NULL,
    image BLOB
);

CREATE TABLE IF NOT EXISTS
parts_frame_legs (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL,
    manufacturer TEXT NOT NULL,
    ap INT NOT NULL,
    anti_kinetic_defense INT NOT NULL,
    anti_energy_defense INT NOT NULL,
    anti_explosive_defense INT NOT NULL,
    average_defense DECIMAL(10,5) NOT NULL,
    attitude_stability INT NOT NULL,
    load_limit INT NOT NULL,
    jump_distance INT NOT NULL,
    jump_height INT NOT NULL,
    weight INT NOT NULL,
    en_load INT NOT NULL,
    description TEXT NOT NULL,
    image BLOB
);

CREATE TABLE IF NOT EXISTS
parts_internal_boosters (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL,
    manufacturer TEXT NOT NULL,
    thrust INT NOT NULL,
    upward_thrust INT NOT NULL,
    upward_en_consumption INT NOT NULL,
    qb_thrust INT NOT NULL,
    qb_jet_duration DECIMAL(10,5) NOT NULL,
    qb_en_consumption INT NOT NULL,
    qb_reload_time DECIMAL(10,5) NOT NULL,
    qb_reload_ideal_weight INT NOT NULL,
    ab_thrust INT NOT NULL,
    ab_en_consumption INT NOT NULL,
    melee_attack_thrust INT NOT NULL,
    melee_atk_en_consumption INT NOT NULL,
    weight INT NOT NULL,
    en_load INT NOT NULL,
    description TEXT NOT NULL,
    image BLOB
);

CREATE TABLE IF NOT EXISTS
parts_internal_fcs (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL,
    manufacturer TEXT NOT NULL,
    close_assist INT NOT NULL,
    medium_assist INT NOT NULL,
    long_assist INT NOT NULL,
    avg_assist INT NOT NULL,
    missile_correction INT NOT NULL,
    multi_lock_correction INT NOT NULL,
    weight INT NOT NULL,
    en_load INT NOT NULL,
    description TEXT NOT NULL,
    image BLOB
);

CREATE TABLE IF NOT EXISTS
parts_internal_generator (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL,
    manufacturer TEXT NOT NULL,
    en_capacity INT NOT NULL,
    en_recharge INT NOT NULL,
    supply_recovery INT NOT NULL,
    post_recovery_en_supply INT NOT NULL,
    energy_firearm_spec INT NOT NULL,
    weight INT NOT NULL,
    en_load INT NOT NULL,
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
    attack_power INT,
    attack_power_multiplier INT,
    impact INT,
    impact_multiplier INT,
    accumulative_impact INT,
    accumulative_impact_multiplier INT,
    blast_radius INT,
    atk_heat_build_up INT,
    direct_hit_adjustment INT,
    recoil INT,
    effective_range INT,
    range_limt INT,
    rapid_fire DECIMAL(10,5),
    total_rounds INT,
    reload DECIMAL(10,5),
    cooling INT,
    ammunition_cost INT,
    consecutive_hits INT,
    weight INT NOT NULL,
    en_load INT NOT NULL,
    description TEXT NOT NULL,
    image BLOB
);