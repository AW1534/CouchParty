using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CouchPlay : MonoBehaviour
{

    private static string[] encdec_ = { 
        "aA", "aB", "aC", "aD", "aE", "aF", "aG", "aH",
        "aI", "aJ", "aK", "aL", "aM", "aN", "aO", "aP",
        "aQ", "aR", "aS", "aT", "aU", "aV", "aW", "aX",
        "aY", "aZ",

        "Ba", "BA", "Bb", "BB", "Bc", "BC", "Bd", "BD", "Be", "BE", "Bf", "BF", "Bg", "BG", "Bh", "BH",
        "Bi", "BI", "Bj", "BJ", "Bk", "BK", "Bl", "BL", "Bm", "BM", "Bn", "BN", "Bo", "BO", "Bp", "BP",
        "Bq", "BQ", "Br", "BR", "Bs", "BS", "Bt", "BT", "Bu", "BU", "Bv", "BV", "Bw", "BW", "Bx", "BX",
        "By", "BY", "Bz", "BZ",

        "ca", "cA", "cb", "cB", "cc", "cC", "cd", "cD", "ce", "cE", "cf", "cF", "cg", "cG", "ch", "cH",
        "ci", "cI", "cj", "cJ", "ck", "cK", "cl", "cL", "cm", "cM", "cn", "cN", "co", "cO", "cp", "cP",
        "cq", "cQ", "cr", "cR", "cs", "cS", "ct", "cT", "cu", "cU", "cv", "cV", "cw", "cW", "cx", "cX",
        "cy", "cY", "cz", "cZ",

        "Da", "DA", "Db", "DB", "Dc", "DC", "Dd", "DD", "De", "DE", "Df", "DF", "Dg", "DG", "Dh", "DH",
        "Di", "DI", "Dj", "DJ", "Dk", "DK", "Dl", "DL", "Dm", "DM", "Dn", "DN", "Do", "DO", "Dp", "DP",
        "Dq", "DQ", "Dr", "DR", "Ds", "DS", "Dt", "DT", "Du", "DU", "Dv", "DV", "Dw", "DW", "Dx", "DX",
        "Dy", "DY", "Dz", "DZ",

        "ea", "eA", "eb", "eB", "ec", "eC", "ed", "eD", "ee", "eE", "ef", "eF", "eg", "eG", "eh", "eH",
        "ei", "eI", "ej", "eJ", "ek", "eK", "el", "eL", "em", "eM", "en", "eN", "eo", "eO", "ep", "eP",
        "eq", "eQ", "er", "eR", "es", "eS", "et", "eT", "eu", "eU", "ev", "eV", "ew", "eW", "ex", "eX",
        "ey", "eY", "ez", "eZ",

        "fA", "fB", "fC", "fD", "fE", "fF", "fG", "fH",
        "fI", "fJ", "fK", "fL", "fM", "fN", "fO", "fP",
        "fQ", "fR", "fS", "fT", "fU", "fV", "fW", "fX",
        "fY", "fZ",
    };

    private void Awake()
    {
        //Debug.Log($"Encoded: {Encode("192.168.2.10")}");
        //Debug.Log($"Decoded: {Decode("efDtaCaK")}");
    }

    public static string Encode(string value)
    {
        string ip = value;
        string[] parts = ip.Split('.'); //[ "192", "168", "2", "10" ];

        string encoded = $"{EncodeP3(parts[0])}{EncodeP3(parts[1])}{EncodeP3(parts[2])}{EncodeP3(parts[3])}";
        return encoded;
    }

    public string Decode(string value)
    {
        string decoded = "";

        List<string> dec_ = new List<string>();

        string[] parts = { value.Substring(0, 2), value.Substring(2, 2), value.Substring(4, 2), value.Substring(6, 2) };

        foreach (string str in encdec_)
        {
            dec_.Add(str);
        }

        int index = 0;
        foreach (string str in parts)
        {
            decoded += $"{dec_.IndexOf(str)}";
            index++;
            if (index <= 3) { decoded += ".";  }
        }

        return decoded;
    }

    private static string EncodeP3(string value)
    {
        int integ = int.Parse(value);
        string _ = "";

        _ = encdec_[integ];

        return _;
    }
}
