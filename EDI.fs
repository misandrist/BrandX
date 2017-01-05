module BrandX.EDI

open BrandX.B2
open BrandX.B2A
open BrandX.GS
open BrandX.IEA
open BrandX.ISA
open BrandX.N1
open BrandX.NTE
open BrandX.ST
open BrandX.N1
open BrandX.N3
open BrandX.N4
open BrandX.S5
open BrandX.L11
open BrandX.IEA
open FParsec

type EDI =
    | EDI of ISA * GS * ST * B2 * B2A * NTE * N1 * N3 * N4 * S5 * L11 list

let pEDI : Parser<EDI,_> = parse {
    let! a = pISARec
    let! b = pGS
    let! c = pST
    let! d = pB2
    let! e = pB2A
    let! f = pNTE
    let! g = pN1
    let! h = pN3
    let! i = pN4
    let! j = pS5
    let! k = many pL11
    return (EDI(a, b, c, d, e, f, g, h, i, j, k))
    }
