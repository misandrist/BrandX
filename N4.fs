﻿module BrandX.N4

open System
open System.Collections.Generic
open System.IO
open FParsec
open BrandX.Structures

type City =
    | City of string

let city : Parser<City option> =
    (opt
        (manyMinMaxSatisfy 2 30 (isNoneOf "*~") |>> City)) .>> fsep

type State =
    | State of string

let state : Parser<State option> =
    (opt
        (anyString 2 |>> State)) .>> fsep

type Zipcode =
    | Zipcode of string

let zip : Parser<Zipcode option> =
    (opt
        (manyMinMaxSatisfy 3 15 (isNoneOf"*~.,':;' '") |>> Zipcode)) .>> fsep

type Country =
    | Country of string

let country : Parser<Country option> =
    (opt
        (manyMinMaxSatisfy 2 3 isAsciiLetter |>> Country)) //.>> fsep



type Location =
    { city : City option
      state : State option
      zip : Zipcode option
      country : Country option}

let pLoc =
    pipe4 pCity pState pZip pCountry (fun m s z c ->
        {city = m
         state = s
         zip = z
         country = c})

type N4 =
    | N4 of AddressInfo * City option * State option * Zipcode option * Country option //City * State * Zipcode * Country

let n4 =
    addInf
    >>= fun a ->
        city
        >>= fun b ->
            state
            >>= fun c ->
                zip
                >>= fun d ->
                    country
                    >>= fun e ->
                        preturn (N4(a,b,c,d,e))

let n4record = skipString "N4" .>> fsep >>. n4 .>> rsep
