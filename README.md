# Interrupt project game (Unity)


A visual demonstration of **Interrupts**, **Traps**, and **Throttling** concepts in Computer Architecture using a gamified simulation.


---

## 🇬🇧 English: Project Overview

This project simulates the relationship between the CPU, OS, and I/O devices through a spaceship game mechanic. It was developed to visualize how a computer handles interruptions and errors.

### ⚙️ Mechanics vs. Theory

| Game Mechanic | Computer Architecture Concept | Explanation |
| :--- | :--- | :--- |
| **Ship Movement** | **Fetch-Execute Cycle** | Represents the main process (Main Thread) executing instructions continuously. |
| **Heat Bar** | **CPU Throttling** | When the CPU overheats, it lowers its frequency (speed) to protect hardware. |
| **"C" Key Mini-Game** | **I/O Interrupt (IRQ)** | A hardware interrupt request. The CPU pauses the main task, saves context, executes the ISR (Interrupt Service Routine), and then resumes. |
| **Rock Collision** | **Trap / Fatal Exception** | An unrecoverable error (like division by zero). The OS terminates the process immediately (Game Over). |
| **Restart Button** | **System Reboot** | Clearing memory and restarting the Operating System. |

### 🕹️ Controls
* **Arrow Keys:** Move the ship.
* **"C" Key:** Triggers the Interrupt Service Routine (when heat bar is critical).
* **On-Screen Arrows:** Complete the sequence to finish the ISR and cool down the CPU.

---

## 🇹🇷 Türkçe: Proje Özeti

Bu proje, bir uzay gemisi oyunu üzerinden CPU, İşletim Sistemi ve Giriş/Çıkış birimleri arasındaki ilişkiyi simüle eder. Bilgisayarın kesmeleri ve hataları nasıl ele aldığını görselleştirmek için geliştirilmiştir.

### ⚙️ Mekanikler ve Teori Eşleşmesi

| Oyun Mekaniği | Bilgisayar Mimarisi Karşılığı | Açıklama |
| :--- | :--- | :--- |
| **Gemi Hareketi** | **Fetch-Execute Döngüsü** | İşlemcinin ana programı sürekli olarak yürütmesini temsil eder. |
| **Isınma Barı** | **CPU Throttling (Hız Kesme)** | İşlemci ısındığında donanımı korumak için frekansını (hızını) düşürür. |
| **"C" Tuşu (Mini Oyun)** | **I/O Interrupt (Kesme)** | Bir donanım kesme isteğidir. CPU ana işi durdurur, durumu kaydeder, ISR'yi (Kesme Servis Rutini) çalıştırır ve sonra devam eder. |
| **Kayaya Çarpma** | **Trap / Fatal Exception** | Kurtarılamaz bir hata (örn: sıfıra bölme). İşletim sistemi süreci anında sonlandırır (Oyun Biter). |
| **Yeniden Başlat** | **Sistem Resetleme** | Belleğin temizlenmesi ve İşletim Sisteminin yeniden başlatılması. |

### 🕹️ Kontroller
* **Ok Tuşları:** Gemiyi hareket ettirir.
* **"C" Tuşu:** Kesme Servis Rutinini (ISR) tetikler (Isınma barı dolduğunda).
* **Ekrandaki Oklar:** ISR işlemini tamamlamak ve işlemciyi soğutmak için doğru sırayla girilmelidir.

---
**Developed by:** [Efe Yanık]
