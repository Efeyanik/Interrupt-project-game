using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI; 

public class ShipController : MonoBehaviour
{
    [Header("Hýz Ayarlarý")]
    public float normalSpeed = 8f; // Normal hýz
    public float overheatedSpeed = 2f; // Isýnýnca düþeceði hýz
    private float currentSpeed; // O anki aktif hýz

    [Header("Isýnma (Donaným) Ayarlarý")]
    public Slider heatBar; // Sahnedeki Slider
    public float heatRate = 20f; // Hareket ederken ýsýnma hýzý
    public float coolRate = 5f;  // Dururken soðuma hýzý

    [Header("Baðlantýlar")]
    public InterruptManager interruptManager; // Kesme ekranýný yönetecek script
    public GameObject PressC;
    public EndingManeger endingManeger;
    

    // Deðiþkenler
    private GameControls controls;
    private float moveInput;

    void Awake()
    {
        // Oyun baþlarken hýzý normale eþitle
        currentSpeed = normalSpeed;
    }

    void OnEnable()
    {
        if (controls == null)
        {
            controls = new GameControls();
            controls.Gameplay.Move.performed += ctx => moveInput = ctx.ReadValue<float>();
            controls.Gameplay.Move.canceled += ctx => moveInput = 0;
        }
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        if (controls != null)
        {
            controls.Gameplay.Disable();
        }
    }

    void Update()
    {
        // 1. ISINMA MANTIÐI 
        HandleHeating();

        // 2. HAREKET (EXECUTE)
        // moveSpeed yerine artýk 'currentSpeed' kullanýyoruz (Isýnýnca yavaþlamasý için)
        transform.Translate(Vector3.up * moveInput * currentSpeed * Time.deltaTime, Space.World);

        // 3. SINIRLAMA
        float clampedY = Mathf.Clamp(transform.position.y, -4.5f, 4.5f);
        transform.position = new Vector3(transform.position.x, clampedY, 0);

        // 4. KESME ÝSTEÐÝ (I/O INTERRUPT)
        // Bar %90 doluysa VE 'C' tuþuna basýlýrsa
        if (heatBar.value >= heatBar.maxValue * 0.9f)
        {
            // Klavyedeki C tuþunu dinle
            if (Keyboard.current.cKey.wasPressedThisFrame)
            {
                // InterruptManager scriptindeki fonksiyonu çaðýr
                if (interruptManager != null)
                {
                    interruptManager.StartInterrupt();
                }
            }
        }
    }

    // Isýnma ve Hýz Kontrolü Fonksiyonu
    void HandleHeating()
    {
        if (heatBar == null) return; // Bar baðlý deðilse hata verme

        // Hareket varsa (Input 0 deðilse) Isýn
        if (moveInput != 0)
        {
            heatBar.value += heatRate * Time.deltaTime;
        }
        else
        {
            // Hareket yoksa Soðu
            heatBar.value -= coolRate * Time.deltaTime;
        }

        // Bar %90'ý geçtiyse YAVAÞLA (Throttling)
        if (heatBar.value >= heatBar.maxValue * 0.9f)
        {
            currentSpeed = overheatedSpeed; // Hýzý düþür
            GetComponent<SpriteRenderer>().color = Color.red; // Gemiyi kýzart
            PressC.SetActive(true); //Sadece görüntüyü aktif eder
            

        }
        else
        {
            // Kesme ekraný açýk deðilse normal hýza dön
            currentSpeed = normalSpeed;
            GetComponent<SpriteRenderer>().color = Color.white;
            PressC.SetActive(false);
        }
    }

    // Kesme bittikten sonra çaðrýlacak Reset fonksiyonu
    public void RestoreStatus()
    {
        currentSpeed = normalSpeed;
        GetComponent<SpriteRenderer>().color = Color.white;
    }






    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Rock")
        {

            endingManeger.endingScreen();
           
        }
    }






}