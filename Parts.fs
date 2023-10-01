type PartType =
    | Frame
    | Internal
    | Weapon

type Part = { Name: string ; Manufacturer: string ; PartType: PartType }

type Frame = { AP: int }
type FrameType =
    | Head
    | Core
    | Arms
    | Legs
type Head = { ScanDistance: int }
type Core = { Name : string }
type Arms = { Name : string }

type Legs = 
    | Biped
    | ReverseJoint
    | Tetrapod
    | Tank
type Biped = { Name : string }
type ReverseJoint = { Name : string }
type Tetrapod = { Name : string }
type Tank = { Name : string }

type Internal =
    | Booster
    | FCS
    | Generator
type Booster = { Name : string }
type FCS = { Name : string }
type Generator = { Name : string }

type Weapon =
    | Arm
    | Back
type Side =
    | LeftOnly = 0
    | Both = 1
type Arm = { Name: string; Side: Side }
type Back = { Name: string }
