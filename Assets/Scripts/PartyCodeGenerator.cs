using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
public class PartyCodeGenerator : MonoBehaviour
{
    [Header("QR Code")]
    public int size = 150;
    private string url = $"https://api.qrserver.com/v1/create-qr-code/?size=";
    private string partyCode = "";
    public Renderer qrRenderer;
    public int stringLength = 10;
    bool CodeGenerated = false;
    public float qrDelay = 0.5f;

    [Header("QR code color")]
    public Color backgroundColor = new Color(1f, 0.847f, 0.592f);
    public Color foregroundColor = Color.cyan; //new Color(230 / 255, 182 / 255, 103 / 255);
    public bool changeBackgroundColor = true;
    public bool changeForegroundColor = true;

    [Header("Party code")]
    public Text CodeDisplay;

    private int timer = 0;
    private bool shouldTime = true;

    void Awake()
    {
        url += $"{size}x{size}&data=";

        qrRenderer.material.color = backgroundColor;
        //GeneratePartyCode(true);
        partyCode = CouchPlay.Encode(GetLocalIPAddress());
        Debug.Log("Party code: " + partyCode);
        Invoke("LoadQrCode", qrDelay);
    }

    void FixedUpdate()
    {
        //Debug.Log($"timer: {timer}");
        if (shouldTime && timer < 500)
        {
            timer++;
            if (timer > 120)
            {
                shouldTime = false;
                GetComponent<Animator>().SetBool("popup", true);
            }
        }
    }

    public void GeneratePartyCode(bool random)
    {
        if (random)
        {
            int _stringLength = stringLength - 1;
            string[] characters = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            for (int i = 0; i <= _stringLength; i++)
            {
                partyCode = partyCode + characters[Random.Range(0, characters.Length)];
            }
        }
        else
        {
            partyCode = GetLocalIPAddress() ;
        }
        partyCode = "sus";
        CodeDisplay.text = partyCode;
    }

    public string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new System.Exception("No network adapters with an IPv4 address in the system!");
    }

    // this section will be run independently
    [System.Obsolete]
    private IEnumerator QrCodeLoadCoroutine()
    {
        //Debug.Log("Loading ....");
        WWW wwwLoader = new WWW(url + partyCode);   // create WWW object pointing to the url
        yield return wwwLoader;         // start loading whatever in that url ( delay happens here )

        //Debug.Log("Loaded");
        qrRenderer.material.color = Color.white;              // set white

        Texture2D tex = wwwLoader.texture;

        for (int x = 0; x < tex.width; x++)
        {
            for (int y = 0; y < tex.height; y++)
            {
                if (tex.GetPixel(x, y) == Color.white && changeBackgroundColor)
                {
                    tex.SetPixel(x, y, backgroundColor);
                    tex.Apply();
                }
                else if (tex.GetPixel(x, y) == Color.black && changeForegroundColor)
                {
                    tex.SetPixel(x, y, foregroundColor);
                    tex.Apply();
                }
            }
        }

        tex.filterMode = FilterMode.Point;
        tex.wrapMode = (TextureWrapMode) WrapMode.Clamp;
        qrRenderer.material.mainTexture = tex; // set loaded image
        qrRenderer.material.shader = Shader.Find("Unlit/Texture"); // @Mexury --> forces the QR code to be light.
    }

    [System.Obsolete]
    public void LoadQrCode()
    {
        StartCoroutine(QrCodeLoadCoroutine());
    }
}