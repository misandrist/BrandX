module BrandX.IEA

open System
open System.IO
open FParsec
open BrandX.Structures

type InclFunGrps =
    | InclFunGrps of string

let fgrps : Parser<InclFunGrps> = manyMinMaxSatisfy 1 5 (isNoneOf "~") |>> InclFunGrps .>> fsep


type IntchgCtrlNo =
    | IntchgCtrlNo of uint16

let ctrlNo : Parser<IntchgCtrlNo> = (manyMinMaxSatisfy 9 9 isDigit |>> (fun ctl -> UInt16.Parse(ctl) |> IntchgCtrlNo)) .>> fsep

type IEA = IEA of InclFunGrps * IntchgCtrlNo

let iEA =
    skipString "IEA" >>. fsep >>. tuple2 fgrps ctrlNo |>> IEA .>> rsep
