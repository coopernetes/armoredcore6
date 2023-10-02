// This code was generated by `SqlHydra.Sqlite` -- v2.1.1.0.
namespace WikidotScraper.DatabaseTypes


[<AutoOpen>]
module ColumnReaders =
    type Column(reader: System.Data.IDataReader, getOrdinal: string -> int, column) =
            member __.Name = column
            member __.IsNull() = getOrdinal column |> reader.IsDBNull
            override __.ToString() = __.Name

    type RequiredColumn<'T, 'Reader when 'Reader :> System.Data.IDataReader>(reader: 'Reader, getOrdinal, getter: int -> 'T, column) =
            inherit Column(reader, getOrdinal, column)
            member __.Read(?alias) = alias |> Option.defaultValue __.Name |> getOrdinal |> getter

    type OptionalColumn<'T, 'Reader when 'Reader :> System.Data.IDataReader>(reader: 'Reader, getOrdinal, getter: int -> 'T, column) =
            inherit Column(reader, getOrdinal, column)
            member __.Read(?alias) = 
                match alias |> Option.defaultValue __.Name |> getOrdinal with
                | o when reader.IsDBNull o -> None
                | o -> Some (getter o)

[<AutoOpen>]
module private DataReaderExtensions =
    type System.Data.IDataReader with
        member reader.GetDateOnly(ordinal: int) = 
            reader.GetDateTime(ordinal) |> System.DateOnly.FromDateTime
    
    type System.Data.Common.DbDataReader with
        member reader.GetTimeOnly(ordinal: int) = 
            reader.GetFieldValue(ordinal) |> System.TimeOnly.FromTimeSpan
        

module main =
    [<CLIMutable>]
    type parts_frame_arms =
        { id: int64
          name: string
          manufacturer: string
          ap: int
          anti_kinetic_defense: int
          anti_energy_defense: int
          anti_explosive_defense: int
          average_defense: decimal
          arms_load_limit: int
          recoil_control: int
          firearms_specialization: int
          melee_specialization: int
          weight: int
          en_load: int
          description: string
          image: Option<byte []> }

    let parts_frame_arms = SqlHydra.Query.Table.table<parts_frame_arms>

    [<CLIMutable>]
    type parts_frame_core =
        { id: int64
          name: string
          manufacturer: string
          ap: int
          anti_kinetic_defense: int
          anti_energy_defense: int
          anti_explosive_defense: int
          average_defense: decimal
          attitude_stability: int
          booster_efficiency_adjustment: int
          generator_output_adjustment: int
          generator_supply_adjustment: int
          weight: int
          en_load: int
          description: string
          image: Option<byte []> }

    let parts_frame_core = SqlHydra.Query.Table.table<parts_frame_core>

    [<CLIMutable>]
    type parts_frame_head =
        { id: int64
          name: string
          manufacturer: string
          ap: int
          anti_kinetic_defense: int
          anti_energy_defense: int
          anti_explosive_defense: int
          average_defense: decimal
          attitude_stability: int
          system_recovery: int
          scan_distance: int
          scan_effect_duration: decimal
          weight: int
          en_load: int
          description: string
          image: Option<byte []> }

    let parts_frame_head = SqlHydra.Query.Table.table<parts_frame_head>

    [<CLIMutable>]
    type parts_frame_legs =
        { id: int64
          name: string
          manufacturer: string
          ap: int
          anti_kinetic_defense: int
          anti_energy_defense: int
          anti_explosive_defense: int
          average_defense: decimal
          attitude_stability: int
          load_limit: int
          jump_distance: int
          jump_height: int
          weight: int
          en_load: int
          description: string
          image: Option<byte []> }

    let parts_frame_legs = SqlHydra.Query.Table.table<parts_frame_legs>

    [<CLIMutable>]
    type parts_internal_boosters =
        { id: int64
          name: string
          manufacturer: string
          thrust: int
          upward_thrust: int
          upward_en_consumption: int
          qb_thrust: int
          qb_jet_duration: decimal
          qb_en_consumption: int
          qb_reload_time: decimal
          qb_reload_ideal_weight: int
          ab_thrust: int
          ab_en_consumption: int
          melee_attack_thrust: int
          melee_atk_en_consumption: int
          weight: int
          en_load: int
          description: string
          image: Option<byte []> }

    let parts_internal_boosters = SqlHydra.Query.Table.table<parts_internal_boosters>

    [<CLIMutable>]
    type parts_internal_fcs =
        { id: int64
          name: string
          manufacturer: string
          close_assist: int
          medium_assist: int
          long_assist: int
          avg_assist: int
          missile_correction: int
          multi_lock_correction: int
          weight: int
          en_load: int
          description: string
          image: Option<byte []> }

    let parts_internal_fcs = SqlHydra.Query.Table.table<parts_internal_fcs>

    [<CLIMutable>]
    type parts_internal_generator =
        { id: int64
          name: string
          manufacturer: string
          en_capacity: int
          en_recharge: int
          supply_recovery: int
          post_recovery_en_supply: int
          energy_firearm_spec: int
          weight: int
          en_load: int
          description: string
          image: Option<byte []> }

    let parts_internal_generator = SqlHydra.Query.Table.table<parts_internal_generator>

    [<CLIMutable>]
    type parts_weapon =
        { id: int64
          slot: Option<string>
          name: string
          part_type: Option<string>
          manufacturer: string
          attack_power: Option<int>
          attack_power_multiplier: Option<int>
          impact: Option<int>
          impact_multiplier: Option<int>
          accumulative_impact: Option<int>
          accumulative_impact_multiplier: Option<int>
          blast_radius: Option<int>
          atk_heat_build_up: Option<int>
          direct_hit_adjustment: Option<int>
          recoil: Option<int>
          effective_range: Option<int>
          range_limt: Option<int>
          rapid_fire: Option<decimal>
          total_rounds: Option<int>
          reload: Option<decimal>
          cooling: Option<int>
          ammunition_cost: Option<int>
          consecutive_hits: Option<int>
          weight: int
          en_load: int
          description: string
          image: Option<byte []> }

    let parts_weapon = SqlHydra.Query.Table.table<parts_weapon>

    [<CLIMutable>]
    type test =
        { id: int64
          text_field: string
          number: Option<int64>
          number2: Option<double>
          data: Option<byte []> }

    let test = SqlHydra.Query.Table.table<test>

    module Readers =
        type parts_frame_armsReader(reader: System.Data.Common.DbDataReader, getOrdinal) =
            member __.id = RequiredColumn(reader, getOrdinal, reader.GetInt64, "id")
            member __.name = RequiredColumn(reader, getOrdinal, reader.GetString, "name")
            member __.manufacturer = RequiredColumn(reader, getOrdinal, reader.GetString, "manufacturer")
            member __.ap = RequiredColumn(reader, getOrdinal, reader.GetInt32, "ap")
            member __.anti_kinetic_defense = RequiredColumn(reader, getOrdinal, reader.GetInt32, "anti_kinetic_defense")
            member __.anti_energy_defense = RequiredColumn(reader, getOrdinal, reader.GetInt32, "anti_energy_defense")
            member __.anti_explosive_defense = RequiredColumn(reader, getOrdinal, reader.GetInt32, "anti_explosive_defense")
            member __.average_defense = RequiredColumn(reader, getOrdinal, reader.GetDecimal, "average_defense")
            member __.arms_load_limit = RequiredColumn(reader, getOrdinal, reader.GetInt32, "arms_load_limit")
            member __.recoil_control = RequiredColumn(reader, getOrdinal, reader.GetInt32, "recoil_control")
            member __.firearms_specialization = RequiredColumn(reader, getOrdinal, reader.GetInt32, "firearms_specialization")
            member __.melee_specialization = RequiredColumn(reader, getOrdinal, reader.GetInt32, "melee_specialization")
            member __.weight = RequiredColumn(reader, getOrdinal, reader.GetInt32, "weight")
            member __.en_load = RequiredColumn(reader, getOrdinal, reader.GetInt32, "en_load")
            member __.description = RequiredColumn(reader, getOrdinal, reader.GetString, "description")
            member __.image = OptionalColumn(reader, getOrdinal, reader.GetFieldValue, "image")

            member __.Read() =
                { parts_frame_arms.id = __.id.Read()
                  name = __.name.Read()
                  manufacturer = __.manufacturer.Read()
                  ap = __.ap.Read()
                  anti_kinetic_defense = __.anti_kinetic_defense.Read()
                  anti_energy_defense = __.anti_energy_defense.Read()
                  anti_explosive_defense = __.anti_explosive_defense.Read()
                  average_defense = __.average_defense.Read()
                  arms_load_limit = __.arms_load_limit.Read()
                  recoil_control = __.recoil_control.Read()
                  firearms_specialization = __.firearms_specialization.Read()
                  melee_specialization = __.melee_specialization.Read()
                  weight = __.weight.Read()
                  en_load = __.en_load.Read()
                  description = __.description.Read()
                  image = __.image.Read() }

            member __.ReadIfNotNull() =
                if __.id.IsNull() then None else Some(__.Read())

        type parts_frame_coreReader(reader: System.Data.Common.DbDataReader, getOrdinal) =
            member __.id = RequiredColumn(reader, getOrdinal, reader.GetInt64, "id")
            member __.name = RequiredColumn(reader, getOrdinal, reader.GetString, "name")
            member __.manufacturer = RequiredColumn(reader, getOrdinal, reader.GetString, "manufacturer")
            member __.ap = RequiredColumn(reader, getOrdinal, reader.GetInt32, "ap")
            member __.anti_kinetic_defense = RequiredColumn(reader, getOrdinal, reader.GetInt32, "anti_kinetic_defense")
            member __.anti_energy_defense = RequiredColumn(reader, getOrdinal, reader.GetInt32, "anti_energy_defense")
            member __.anti_explosive_defense = RequiredColumn(reader, getOrdinal, reader.GetInt32, "anti_explosive_defense")
            member __.average_defense = RequiredColumn(reader, getOrdinal, reader.GetDecimal, "average_defense")
            member __.attitude_stability = RequiredColumn(reader, getOrdinal, reader.GetInt32, "attitude_stability")
            member __.booster_efficiency_adjustment = RequiredColumn(reader, getOrdinal, reader.GetInt32, "booster_efficiency_adjustment")
            member __.generator_output_adjustment = RequiredColumn(reader, getOrdinal, reader.GetInt32, "generator_output_adjustment")
            member __.generator_supply_adjustment = RequiredColumn(reader, getOrdinal, reader.GetInt32, "generator_supply_adjustment")
            member __.weight = RequiredColumn(reader, getOrdinal, reader.GetInt32, "weight")
            member __.en_load = RequiredColumn(reader, getOrdinal, reader.GetInt32, "en_load")
            member __.description = RequiredColumn(reader, getOrdinal, reader.GetString, "description")
            member __.image = OptionalColumn(reader, getOrdinal, reader.GetFieldValue, "image")

            member __.Read() =
                { parts_frame_core.id = __.id.Read()
                  name = __.name.Read()
                  manufacturer = __.manufacturer.Read()
                  ap = __.ap.Read()
                  anti_kinetic_defense = __.anti_kinetic_defense.Read()
                  anti_energy_defense = __.anti_energy_defense.Read()
                  anti_explosive_defense = __.anti_explosive_defense.Read()
                  average_defense = __.average_defense.Read()
                  attitude_stability = __.attitude_stability.Read()
                  booster_efficiency_adjustment = __.booster_efficiency_adjustment.Read()
                  generator_output_adjustment = __.generator_output_adjustment.Read()
                  generator_supply_adjustment = __.generator_supply_adjustment.Read()
                  weight = __.weight.Read()
                  en_load = __.en_load.Read()
                  description = __.description.Read()
                  image = __.image.Read() }

            member __.ReadIfNotNull() =
                if __.id.IsNull() then None else Some(__.Read())

        type parts_frame_headReader(reader: System.Data.Common.DbDataReader, getOrdinal) =
            member __.id = RequiredColumn(reader, getOrdinal, reader.GetInt64, "id")
            member __.name = RequiredColumn(reader, getOrdinal, reader.GetString, "name")
            member __.manufacturer = RequiredColumn(reader, getOrdinal, reader.GetString, "manufacturer")
            member __.ap = RequiredColumn(reader, getOrdinal, reader.GetInt32, "ap")
            member __.anti_kinetic_defense = RequiredColumn(reader, getOrdinal, reader.GetInt32, "anti_kinetic_defense")
            member __.anti_energy_defense = RequiredColumn(reader, getOrdinal, reader.GetInt32, "anti_energy_defense")
            member __.anti_explosive_defense = RequiredColumn(reader, getOrdinal, reader.GetInt32, "anti_explosive_defense")
            member __.average_defense = RequiredColumn(reader, getOrdinal, reader.GetDecimal, "average_defense")
            member __.attitude_stability = RequiredColumn(reader, getOrdinal, reader.GetInt32, "attitude_stability")
            member __.system_recovery = RequiredColumn(reader, getOrdinal, reader.GetInt32, "system_recovery")
            member __.scan_distance = RequiredColumn(reader, getOrdinal, reader.GetInt32, "scan_distance")
            member __.scan_effect_duration = RequiredColumn(reader, getOrdinal, reader.GetDecimal, "scan_effect_duration")
            member __.weight = RequiredColumn(reader, getOrdinal, reader.GetInt32, "weight")
            member __.en_load = RequiredColumn(reader, getOrdinal, reader.GetInt32, "en_load")
            member __.description = RequiredColumn(reader, getOrdinal, reader.GetString, "description")
            member __.image = OptionalColumn(reader, getOrdinal, reader.GetFieldValue, "image")

            member __.Read() =
                { parts_frame_head.id = __.id.Read()
                  name = __.name.Read()
                  manufacturer = __.manufacturer.Read()
                  ap = __.ap.Read()
                  anti_kinetic_defense = __.anti_kinetic_defense.Read()
                  anti_energy_defense = __.anti_energy_defense.Read()
                  anti_explosive_defense = __.anti_explosive_defense.Read()
                  average_defense = __.average_defense.Read()
                  attitude_stability = __.attitude_stability.Read()
                  system_recovery = __.system_recovery.Read()
                  scan_distance = __.scan_distance.Read()
                  scan_effect_duration = __.scan_effect_duration.Read()
                  weight = __.weight.Read()
                  en_load = __.en_load.Read()
                  description = __.description.Read()
                  image = __.image.Read() }

            member __.ReadIfNotNull() =
                if __.id.IsNull() then None else Some(__.Read())

        type parts_frame_legsReader(reader: System.Data.Common.DbDataReader, getOrdinal) =
            member __.id = RequiredColumn(reader, getOrdinal, reader.GetInt64, "id")
            member __.name = RequiredColumn(reader, getOrdinal, reader.GetString, "name")
            member __.manufacturer = RequiredColumn(reader, getOrdinal, reader.GetString, "manufacturer")
            member __.ap = RequiredColumn(reader, getOrdinal, reader.GetInt32, "ap")
            member __.anti_kinetic_defense = RequiredColumn(reader, getOrdinal, reader.GetInt32, "anti_kinetic_defense")
            member __.anti_energy_defense = RequiredColumn(reader, getOrdinal, reader.GetInt32, "anti_energy_defense")
            member __.anti_explosive_defense = RequiredColumn(reader, getOrdinal, reader.GetInt32, "anti_explosive_defense")
            member __.average_defense = RequiredColumn(reader, getOrdinal, reader.GetDecimal, "average_defense")
            member __.attitude_stability = RequiredColumn(reader, getOrdinal, reader.GetInt32, "attitude_stability")
            member __.load_limit = RequiredColumn(reader, getOrdinal, reader.GetInt32, "load_limit")
            member __.jump_distance = RequiredColumn(reader, getOrdinal, reader.GetInt32, "jump_distance")
            member __.jump_height = RequiredColumn(reader, getOrdinal, reader.GetInt32, "jump_height")
            member __.weight = RequiredColumn(reader, getOrdinal, reader.GetInt32, "weight")
            member __.en_load = RequiredColumn(reader, getOrdinal, reader.GetInt32, "en_load")
            member __.description = RequiredColumn(reader, getOrdinal, reader.GetString, "description")
            member __.image = OptionalColumn(reader, getOrdinal, reader.GetFieldValue, "image")

            member __.Read() =
                { parts_frame_legs.id = __.id.Read()
                  name = __.name.Read()
                  manufacturer = __.manufacturer.Read()
                  ap = __.ap.Read()
                  anti_kinetic_defense = __.anti_kinetic_defense.Read()
                  anti_energy_defense = __.anti_energy_defense.Read()
                  anti_explosive_defense = __.anti_explosive_defense.Read()
                  average_defense = __.average_defense.Read()
                  attitude_stability = __.attitude_stability.Read()
                  load_limit = __.load_limit.Read()
                  jump_distance = __.jump_distance.Read()
                  jump_height = __.jump_height.Read()
                  weight = __.weight.Read()
                  en_load = __.en_load.Read()
                  description = __.description.Read()
                  image = __.image.Read() }

            member __.ReadIfNotNull() =
                if __.id.IsNull() then None else Some(__.Read())

        type parts_internal_boostersReader(reader: System.Data.Common.DbDataReader, getOrdinal) =
            member __.id = RequiredColumn(reader, getOrdinal, reader.GetInt64, "id")
            member __.name = RequiredColumn(reader, getOrdinal, reader.GetString, "name")
            member __.manufacturer = RequiredColumn(reader, getOrdinal, reader.GetString, "manufacturer")
            member __.thrust = RequiredColumn(reader, getOrdinal, reader.GetInt32, "thrust")
            member __.upward_thrust = RequiredColumn(reader, getOrdinal, reader.GetInt32, "upward_thrust")
            member __.upward_en_consumption = RequiredColumn(reader, getOrdinal, reader.GetInt32, "upward_en_consumption")
            member __.qb_thrust = RequiredColumn(reader, getOrdinal, reader.GetInt32, "qb_thrust")
            member __.qb_jet_duration = RequiredColumn(reader, getOrdinal, reader.GetDecimal, "qb_jet_duration")
            member __.qb_en_consumption = RequiredColumn(reader, getOrdinal, reader.GetInt32, "qb_en_consumption")
            member __.qb_reload_time = RequiredColumn(reader, getOrdinal, reader.GetDecimal, "qb_reload_time")
            member __.qb_reload_ideal_weight = RequiredColumn(reader, getOrdinal, reader.GetInt32, "qb_reload_ideal_weight")
            member __.ab_thrust = RequiredColumn(reader, getOrdinal, reader.GetInt32, "ab_thrust")
            member __.ab_en_consumption = RequiredColumn(reader, getOrdinal, reader.GetInt32, "ab_en_consumption")
            member __.melee_attack_thrust = RequiredColumn(reader, getOrdinal, reader.GetInt32, "melee_attack_thrust")
            member __.melee_atk_en_consumption = RequiredColumn(reader, getOrdinal, reader.GetInt32, "melee_atk_en_consumption")
            member __.weight = RequiredColumn(reader, getOrdinal, reader.GetInt32, "weight")
            member __.en_load = RequiredColumn(reader, getOrdinal, reader.GetInt32, "en_load")
            member __.description = RequiredColumn(reader, getOrdinal, reader.GetString, "description")
            member __.image = OptionalColumn(reader, getOrdinal, reader.GetFieldValue, "image")

            member __.Read() =
                { parts_internal_boosters.id = __.id.Read()
                  name = __.name.Read()
                  manufacturer = __.manufacturer.Read()
                  thrust = __.thrust.Read()
                  upward_thrust = __.upward_thrust.Read()
                  upward_en_consumption = __.upward_en_consumption.Read()
                  qb_thrust = __.qb_thrust.Read()
                  qb_jet_duration = __.qb_jet_duration.Read()
                  qb_en_consumption = __.qb_en_consumption.Read()
                  qb_reload_time = __.qb_reload_time.Read()
                  qb_reload_ideal_weight = __.qb_reload_ideal_weight.Read()
                  ab_thrust = __.ab_thrust.Read()
                  ab_en_consumption = __.ab_en_consumption.Read()
                  melee_attack_thrust = __.melee_attack_thrust.Read()
                  melee_atk_en_consumption = __.melee_atk_en_consumption.Read()
                  weight = __.weight.Read()
                  en_load = __.en_load.Read()
                  description = __.description.Read()
                  image = __.image.Read() }

            member __.ReadIfNotNull() =
                if __.id.IsNull() then None else Some(__.Read())

        type parts_internal_fcsReader(reader: System.Data.Common.DbDataReader, getOrdinal) =
            member __.id = RequiredColumn(reader, getOrdinal, reader.GetInt64, "id")
            member __.name = RequiredColumn(reader, getOrdinal, reader.GetString, "name")
            member __.manufacturer = RequiredColumn(reader, getOrdinal, reader.GetString, "manufacturer")
            member __.close_assist = RequiredColumn(reader, getOrdinal, reader.GetInt32, "close_assist")
            member __.medium_assist = RequiredColumn(reader, getOrdinal, reader.GetInt32, "medium_assist")
            member __.long_assist = RequiredColumn(reader, getOrdinal, reader.GetInt32, "long_assist")
            member __.avg_assist = RequiredColumn(reader, getOrdinal, reader.GetInt32, "avg_assist")
            member __.missile_correction = RequiredColumn(reader, getOrdinal, reader.GetInt32, "missile_correction")
            member __.multi_lock_correction = RequiredColumn(reader, getOrdinal, reader.GetInt32, "multi_lock_correction")
            member __.weight = RequiredColumn(reader, getOrdinal, reader.GetInt32, "weight")
            member __.en_load = RequiredColumn(reader, getOrdinal, reader.GetInt32, "en_load")
            member __.description = RequiredColumn(reader, getOrdinal, reader.GetString, "description")
            member __.image = OptionalColumn(reader, getOrdinal, reader.GetFieldValue, "image")

            member __.Read() =
                { parts_internal_fcs.id = __.id.Read()
                  name = __.name.Read()
                  manufacturer = __.manufacturer.Read()
                  close_assist = __.close_assist.Read()
                  medium_assist = __.medium_assist.Read()
                  long_assist = __.long_assist.Read()
                  avg_assist = __.avg_assist.Read()
                  missile_correction = __.missile_correction.Read()
                  multi_lock_correction = __.multi_lock_correction.Read()
                  weight = __.weight.Read()
                  en_load = __.en_load.Read()
                  description = __.description.Read()
                  image = __.image.Read() }

            member __.ReadIfNotNull() =
                if __.id.IsNull() then None else Some(__.Read())

        type parts_internal_generatorReader(reader: System.Data.Common.DbDataReader, getOrdinal) =
            member __.id = RequiredColumn(reader, getOrdinal, reader.GetInt64, "id")
            member __.name = RequiredColumn(reader, getOrdinal, reader.GetString, "name")
            member __.manufacturer = RequiredColumn(reader, getOrdinal, reader.GetString, "manufacturer")
            member __.en_capacity = RequiredColumn(reader, getOrdinal, reader.GetInt32, "en_capacity")
            member __.en_recharge = RequiredColumn(reader, getOrdinal, reader.GetInt32, "en_recharge")
            member __.supply_recovery = RequiredColumn(reader, getOrdinal, reader.GetInt32, "supply_recovery")
            member __.post_recovery_en_supply = RequiredColumn(reader, getOrdinal, reader.GetInt32, "post_recovery_en_supply")
            member __.energy_firearm_spec = RequiredColumn(reader, getOrdinal, reader.GetInt32, "energy_firearm_spec")
            member __.weight = RequiredColumn(reader, getOrdinal, reader.GetInt32, "weight")
            member __.en_load = RequiredColumn(reader, getOrdinal, reader.GetInt32, "en_load")
            member __.description = RequiredColumn(reader, getOrdinal, reader.GetString, "description")
            member __.image = OptionalColumn(reader, getOrdinal, reader.GetFieldValue, "image")

            member __.Read() =
                { parts_internal_generator.id = __.id.Read()
                  name = __.name.Read()
                  manufacturer = __.manufacturer.Read()
                  en_capacity = __.en_capacity.Read()
                  en_recharge = __.en_recharge.Read()
                  supply_recovery = __.supply_recovery.Read()
                  post_recovery_en_supply = __.post_recovery_en_supply.Read()
                  energy_firearm_spec = __.energy_firearm_spec.Read()
                  weight = __.weight.Read()
                  en_load = __.en_load.Read()
                  description = __.description.Read()
                  image = __.image.Read() }

            member __.ReadIfNotNull() =
                if __.id.IsNull() then None else Some(__.Read())

        type parts_weaponReader(reader: System.Data.Common.DbDataReader, getOrdinal) =
            member __.id = RequiredColumn(reader, getOrdinal, reader.GetInt64, "id")
            member __.slot = OptionalColumn(reader, getOrdinal, reader.GetString, "slot")
            member __.name = RequiredColumn(reader, getOrdinal, reader.GetString, "name")
            member __.part_type = OptionalColumn(reader, getOrdinal, reader.GetString, "part_type")
            member __.manufacturer = RequiredColumn(reader, getOrdinal, reader.GetString, "manufacturer")
            member __.attack_power = OptionalColumn(reader, getOrdinal, reader.GetInt32, "attack_power")
            member __.attack_power_multiplier = OptionalColumn(reader, getOrdinal, reader.GetInt32, "attack_power_multiplier")
            member __.impact = OptionalColumn(reader, getOrdinal, reader.GetInt32, "impact")
            member __.impact_multiplier = OptionalColumn(reader, getOrdinal, reader.GetInt32, "impact_multiplier")
            member __.accumulative_impact = OptionalColumn(reader, getOrdinal, reader.GetInt32, "accumulative_impact")
            member __.accumulative_impact_multiplier = OptionalColumn(reader, getOrdinal, reader.GetInt32, "accumulative_impact_multiplier")
            member __.blast_radius = OptionalColumn(reader, getOrdinal, reader.GetInt32, "blast_radius")
            member __.atk_heat_build_up = OptionalColumn(reader, getOrdinal, reader.GetInt32, "atk_heat_build_up")
            member __.direct_hit_adjustment = OptionalColumn(reader, getOrdinal, reader.GetInt32, "direct_hit_adjustment")
            member __.recoil = OptionalColumn(reader, getOrdinal, reader.GetInt32, "recoil")
            member __.effective_range = OptionalColumn(reader, getOrdinal, reader.GetInt32, "effective_range")
            member __.range_limt = OptionalColumn(reader, getOrdinal, reader.GetInt32, "range_limt")
            member __.rapid_fire = OptionalColumn(reader, getOrdinal, reader.GetDecimal, "rapid_fire")
            member __.total_rounds = OptionalColumn(reader, getOrdinal, reader.GetInt32, "total_rounds")
            member __.reload = OptionalColumn(reader, getOrdinal, reader.GetDecimal, "reload")
            member __.cooling = OptionalColumn(reader, getOrdinal, reader.GetInt32, "cooling")
            member __.ammunition_cost = OptionalColumn(reader, getOrdinal, reader.GetInt32, "ammunition_cost")
            member __.consecutive_hits = OptionalColumn(reader, getOrdinal, reader.GetInt32, "consecutive_hits")
            member __.weight = RequiredColumn(reader, getOrdinal, reader.GetInt32, "weight")
            member __.en_load = RequiredColumn(reader, getOrdinal, reader.GetInt32, "en_load")
            member __.description = RequiredColumn(reader, getOrdinal, reader.GetString, "description")
            member __.image = OptionalColumn(reader, getOrdinal, reader.GetFieldValue, "image")

            member __.Read() =
                { parts_weapon.id = __.id.Read()
                  slot = __.slot.Read()
                  name = __.name.Read()
                  part_type = __.part_type.Read()
                  manufacturer = __.manufacturer.Read()
                  attack_power = __.attack_power.Read()
                  attack_power_multiplier = __.attack_power_multiplier.Read()
                  impact = __.impact.Read()
                  impact_multiplier = __.impact_multiplier.Read()
                  accumulative_impact = __.accumulative_impact.Read()
                  accumulative_impact_multiplier = __.accumulative_impact_multiplier.Read()
                  blast_radius = __.blast_radius.Read()
                  atk_heat_build_up = __.atk_heat_build_up.Read()
                  direct_hit_adjustment = __.direct_hit_adjustment.Read()
                  recoil = __.recoil.Read()
                  effective_range = __.effective_range.Read()
                  range_limt = __.range_limt.Read()
                  rapid_fire = __.rapid_fire.Read()
                  total_rounds = __.total_rounds.Read()
                  reload = __.reload.Read()
                  cooling = __.cooling.Read()
                  ammunition_cost = __.ammunition_cost.Read()
                  consecutive_hits = __.consecutive_hits.Read()
                  weight = __.weight.Read()
                  en_load = __.en_load.Read()
                  description = __.description.Read()
                  image = __.image.Read() }

            member __.ReadIfNotNull() =
                if __.id.IsNull() then None else Some(__.Read())

        type testReader(reader: System.Data.Common.DbDataReader, getOrdinal) =
            member __.id = RequiredColumn(reader, getOrdinal, reader.GetInt64, "id")
            member __.text_field = RequiredColumn(reader, getOrdinal, reader.GetString, "text_field")
            member __.number = OptionalColumn(reader, getOrdinal, reader.GetInt64, "number")
            member __.number2 = OptionalColumn(reader, getOrdinal, reader.GetDouble, "number2")
            member __.data = OptionalColumn(reader, getOrdinal, reader.GetFieldValue, "data")

            member __.Read() =
                { test.id = __.id.Read()
                  text_field = __.text_field.Read()
                  number = __.number.Read()
                  number2 = __.number2.Read()
                  data = __.data.Read() }

            member __.ReadIfNotNull() =
                if __.id.IsNull() then None else Some(__.Read())

type HydraReader(reader: System.Data.Common.DbDataReader) =
    let mutable accFieldCount = 0
    let buildGetOrdinal fieldCount =
        let dictionary = 
            [0..reader.FieldCount-1] 
            |> List.map (fun i -> reader.GetName(i), i)
            |> List.sortBy snd
            |> List.skip accFieldCount
            |> List.take fieldCount
            |> dict
        accFieldCount <- accFieldCount + fieldCount
        fun col -> dictionary.Item col
        
    let lazymainparts_frame_arms = lazy (main.Readers.parts_frame_armsReader (reader, buildGetOrdinal 16))
    let lazymainparts_frame_core = lazy (main.Readers.parts_frame_coreReader (reader, buildGetOrdinal 16))
    let lazymainparts_frame_head = lazy (main.Readers.parts_frame_headReader (reader, buildGetOrdinal 16))
    let lazymainparts_frame_legs = lazy (main.Readers.parts_frame_legsReader (reader, buildGetOrdinal 16))
    let lazymainparts_internal_boosters = lazy (main.Readers.parts_internal_boostersReader (reader, buildGetOrdinal 19))
    let lazymainparts_internal_fcs = lazy (main.Readers.parts_internal_fcsReader (reader, buildGetOrdinal 13))
    let lazymainparts_internal_generator = lazy (main.Readers.parts_internal_generatorReader (reader, buildGetOrdinal 12))
    let lazymainparts_weapon = lazy (main.Readers.parts_weaponReader (reader, buildGetOrdinal 27))
    let lazymaintest = lazy (main.Readers.testReader (reader, buildGetOrdinal 5))
    member __.``main.parts_frame_arms`` = lazymainparts_frame_arms.Value
    member __.``main.parts_frame_core`` = lazymainparts_frame_core.Value
    member __.``main.parts_frame_head`` = lazymainparts_frame_head.Value
    member __.``main.parts_frame_legs`` = lazymainparts_frame_legs.Value
    member __.``main.parts_internal_boosters`` = lazymainparts_internal_boosters.Value
    member __.``main.parts_internal_fcs`` = lazymainparts_internal_fcs.Value
    member __.``main.parts_internal_generator`` = lazymainparts_internal_generator.Value
    member __.``main.parts_weapon`` = lazymainparts_weapon.Value
    member __.``main.test`` = lazymaintest.Value
    member private __.AccFieldCount with get () = accFieldCount and set (value) = accFieldCount <- value

    member private __.GetReaderByName(entity: string, isOption: bool) =
        match entity, isOption with
        | "main.parts_frame_arms", false -> __.``main.parts_frame_arms``.Read >> box
        | "main.parts_frame_arms", true -> __.``main.parts_frame_arms``.ReadIfNotNull >> box
        | "main.parts_frame_core", false -> __.``main.parts_frame_core``.Read >> box
        | "main.parts_frame_core", true -> __.``main.parts_frame_core``.ReadIfNotNull >> box
        | "main.parts_frame_head", false -> __.``main.parts_frame_head``.Read >> box
        | "main.parts_frame_head", true -> __.``main.parts_frame_head``.ReadIfNotNull >> box
        | "main.parts_frame_legs", false -> __.``main.parts_frame_legs``.Read >> box
        | "main.parts_frame_legs", true -> __.``main.parts_frame_legs``.ReadIfNotNull >> box
        | "main.parts_internal_boosters", false -> __.``main.parts_internal_boosters``.Read >> box
        | "main.parts_internal_boosters", true -> __.``main.parts_internal_boosters``.ReadIfNotNull >> box
        | "main.parts_internal_fcs", false -> __.``main.parts_internal_fcs``.Read >> box
        | "main.parts_internal_fcs", true -> __.``main.parts_internal_fcs``.ReadIfNotNull >> box
        | "main.parts_internal_generator", false -> __.``main.parts_internal_generator``.Read >> box
        | "main.parts_internal_generator", true -> __.``main.parts_internal_generator``.ReadIfNotNull >> box
        | "main.parts_weapon", false -> __.``main.parts_weapon``.Read >> box
        | "main.parts_weapon", true -> __.``main.parts_weapon``.ReadIfNotNull >> box
        | "main.test", false -> __.``main.test``.Read >> box
        | "main.test", true -> __.``main.test``.ReadIfNotNull >> box
        | _ -> failwith $"Could not read type '{entity}' because no generated reader exists."

    static member private GetPrimitiveReader(t: System.Type, reader: System.Data.Common.DbDataReader, isOpt: bool) =
        let wrap get (ord: int) = 
            if isOpt 
            then (if reader.IsDBNull ord then None else get ord |> Some) |> box 
            else get ord |> box 
        

        if t = typedefof<int16> then Some(wrap reader.GetInt16)
        else if t = typedefof<int> then Some(wrap reader.GetInt32)
        else if t = typedefof<double> then Some(wrap reader.GetDouble)
        else if t = typedefof<System.Single> then Some(wrap reader.GetDouble)
        else if t = typedefof<decimal> then Some(wrap reader.GetDecimal)
        else if t = typedefof<bool> then Some(wrap reader.GetBoolean)
        else if t = typedefof<byte> then Some(wrap reader.GetByte)
        else if t = typedefof<int64> then Some(wrap reader.GetInt64)
        else if t = typedefof<byte []> then Some(wrap reader.GetFieldValue<byte []>)
        else if t = typedefof<string> then Some(wrap reader.GetString)
        else if t = typedefof<System.DateTime> then Some(wrap reader.GetDateTime)
        else if t = typedefof<System.DateOnly> then Some(wrap reader.GetDateOnly)
        else if t = typedefof<System.TimeOnly> then Some(wrap reader.GetTimeOnly)
        else if t = typedefof<System.Guid> then Some(wrap reader.GetGuid)
        else None

    static member Read(reader: System.Data.Common.DbDataReader) = 
        let hydra = HydraReader(reader)
                    
        let getOrdinalAndIncrement() = 
            let ordinal = hydra.AccFieldCount
            hydra.AccFieldCount <- hydra.AccFieldCount + 1
            ordinal
            
        let buildEntityReadFn (t: System.Type) = 
            let t, isOpt = 
                if t.IsGenericType && t.GetGenericTypeDefinition() = typedefof<Option<_>> 
                then t.GenericTypeArguments.[0], true
                else t, false
            
            match HydraReader.GetPrimitiveReader(t, reader, isOpt) with
            | Some primitiveReader -> 
                let ord = getOrdinalAndIncrement()
                fun () -> primitiveReader ord
            | None ->
                let nameParts = t.FullName.Split([| '.'; '+' |])
                let schemaAndType = nameParts |> Array.skip (nameParts.Length - 2) |> fun parts -> System.String.Join(".", parts)
                hydra.GetReaderByName(schemaAndType, isOpt)
            
        // Return a fn that will hydrate 'T (which may be a tuple)
        // This fn will be called once per each record returned by the data reader.
        let t = typeof<'T>
        if FSharp.Reflection.FSharpType.IsTuple(t) then
            let readEntityFns = FSharp.Reflection.FSharpType.GetTupleElements(t) |> Array.map buildEntityReadFn
            fun () ->
                let entities = readEntityFns |> Array.map (fun read -> read())
                Microsoft.FSharp.Reflection.FSharpValue.MakeTuple(entities, t) :?> 'T
        else
            let readEntityFn = t |> buildEntityReadFn
            fun () -> 
                readEntityFn() :?> 'T
        
