-- Missing data from wikidot
-- Partial data: http://armoredcore6.wikidot.com/weapon:hml-g2
INSERT INTO parts_weapon (slot, name, part_type, manufacturer, attack_power, attack_power_multiplier, impact, impact_multiplier, accumulative_impact, accumulative_impact_multiplier, direct_hit_adjustment, effective_range, total_rounds, reload, ammunition_cost, weight, en_load, description)
VALUES (
    'Right Arm',
    'HML-G2/P19 MLT-04',
    'Active Homing Missile Launcher',
    'Furlong Dynamics',
    216,
    4,
    175,
    4,
    123,
    4,
    155,
    2500,
    180,
    3.0,
    150,
    3250,
    165,
    'Handheld multi-missile launcher developed by Furlong Dynamics. A masterpiece of Furlong’s second-gen lineup, this weapon is capable of multi-locking up to four targets.'
);

INSERT INTO parts_weapon (slot, name, part_type, manufacturer, attack_power, impact,  accumulative_impact, blast_radius, direct_hit_adjustment, effective_range, total_rounds, reload, ammunition_cost, weight, en_load, description)
VALUES (
    'Left Back',
    '45-091 JVLN BETA',
    'Detonating Missile Launcher',
    'ALLMIND',
    791,
    717,
    563,
    20,
    165,
    360,
    32,
    3.6,
    450,
    4250,
    425,
    'Special missile launcher developed by ALLMIND. Creates a chain delayed explosions along the missile''s trajectory, allowing for sustained suppressive fire even against targets that manage to evade the initial missile.'
);
