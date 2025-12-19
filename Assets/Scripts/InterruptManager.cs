using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.InputSystem; 
using System.Collections.Generic; 
using TMPro;
using System.Collections;


public class InterruptManager : MonoBehaviour
{

    [Header("UI Bağlantıları")]
    public GameObject interruptPanel; 
    public TextMeshProUGUI arrowDisplay; 
    public Slider heatBar;

    [Header("Gemi Bağlantısı")]
    public ShipController shipController; // Gemiye "Hızlan/Dur" demek için

    private List<Key> sequence = new List<Key>(); // Basılması gereken tuşlar
    private int currentIndex = 0; // Kaçıncı tuştayız?
    private bool isHandling = false; // Şu an kesme ekranı açık mı?
    private bool inputLocked = false;



    public void StartInterrupt()
    {
        isHandling = true;
        interruptPanel.SetActive(true); // Paneli aç
        inputLocked = false;
        GenerateSequence(); // Rastgele oklar üret
        UpdateDisplay(); 
        Time.timeScale = 0; // Oyunu arka planda dondur 
    }

    void GenerateSequence()
    {
        sequence.Clear();
        currentIndex = 0;
        arrowDisplay.text = ""; // Ekranı temizle

        for (int i = 0; i < 4; i++)
        {
            int rnd = Random.Range(0, 4); // 0-3 arası sayı
            switch (rnd)
            {
                case 0:
                    sequence.Add(Key.UpArrow);
                    arrowDisplay.text += "( ^ )";
                    break;
                case 1:
                    sequence.Add(Key.DownArrow);
                    arrowDisplay.text += "( v )";
                    break;
                case 2:
                    sequence.Add(Key.LeftArrow);
                    arrowDisplay.text += "( < )";
                    break;
                case 3:
                    sequence.Add(Key.RightArrow);
                    arrowDisplay.text += "( > )";
                    break;
            }
        }
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isHandling && !inputLocked)
        {
            CheckInput();
        }
    }



    void CheckInput()
    {
        if (Keyboard.current.upArrowKey.wasPressedThisFrame) ValidateKey(Key.UpArrow);
        else if (Keyboard.current.downArrowKey.wasPressedThisFrame) ValidateKey(Key.DownArrow);
        else if (Keyboard.current.leftArrowKey.wasPressedThisFrame) ValidateKey(Key.LeftArrow);
        else if (Keyboard.current.rightArrowKey.wasPressedThisFrame) ValidateKey(Key.RightArrow);
    }



    void ValidateKey(Key pressedKey)
    {
        // Doğru tuşa mı bastı?
        if (sequence[currentIndex] == pressedKey)
        {
            
            // Doğruysa bir sonrakine geç
            currentIndex++;
            UpdateDisplay(); // Ekranı güncelle (Yeşil yap)


            // Hepsi bitti mi?
            if (currentIndex >= sequence.Count)
            {
                CompleteInterrupt();
            }
        }
        else
        {
            // Yanlış tuşa basarsa başa sar! (Cezalandırma)
            Debug.Log("Yanlış Tuş! Başa dönüldü.");
            currentIndex = 0;
            StartCoroutine(ShowErrorRoutine());

        }
    }

    IEnumerator ShowErrorRoutine()
    {
        inputLocked = true; // Oyuncu hata sırasında başka tuşa basamasın
        UpdateDisplay(true); // Hatalı çiz (Kırmızı)

        yield return new WaitForSecondsRealtime(0.5f); // Yarım saniye bekle (TimeScale 0 olduğu için Realtime kullanıyoruz)

        currentIndex = 0; // Başa sar
        inputLocked = false; // Kilidi aç
        UpdateDisplay(); // Tekrar beyaz çiz
    }

    void UpdateDisplay(bool isError = false)
    {
        arrowDisplay.text = ""; // Metni temizle

        for (int i = 0; i < sequence.Count; i++)
        {
            string symbol = GetArrowSymbol(sequence[i]);

            if (i < currentIndex)
            {
                // Daha önce doğru basılanlar: YEŞİL
                arrowDisplay.text += $"<color=green>{symbol}</color> ";
            }
            else if (i == currentIndex)
            {
                // Şu an basılması gereken
                if (isError)
                {
                    // Hata varsa: KIRMIZI
                    arrowDisplay.text += $"<color=red>{symbol}</color> ";
                }
                else
                {
                    // Sıra bundaysa (ve hata yoksa): BEYAZ (veya Sarı yapabilirsin belirgin olsun diye)
                    arrowDisplay.text += $"<color=white>{symbol}</color> ";
                }
            }
            else
            {
                // Henüz sıra gelmeyenler: GRİ (Daha sönük)
                arrowDisplay.text += $"<color=#888888>{symbol}</color> ";
            }
        }
    }

    string GetArrowSymbol(Key k)
    {
        switch (k)
        {
            case Key.UpArrow: return "( ^ )";
            case Key.DownArrow: return "( v )";
            case Key.LeftArrow: return "( < )";
            case Key.RightArrow: return "( > )";
            default: return "( ? )";
        }
    }



    void CompleteInterrupt()
    {
        isHandling = false;
        interruptPanel.SetActive(false); // Paneli kapat

        // Barı sıfırla
        heatBar.value = 0;

        // Gemiyi normal hızına döndür 
        shipController.RestoreStatus();

        Time.timeScale = 1; // Oyunu devam ettir
    }





}

