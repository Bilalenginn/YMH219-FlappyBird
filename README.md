# YMH219-FlappyBird

Windows Forms üzerinde C# ile geliştirilmiş, Flappy Bird benzeri basit bir masaüstü oyunu. Nesne Tabanlı Programlama (YMH219) dersi ek uygulama ödevi kapsamında hazırlanmıştır.

Öğrenci: Bilal Engin


## Kullanılan Teknolojiler

- C# / .NET 8
- Windows Forms (WinForms)
- Visual Studio 2022
- Harici NuGet paketi kullanılmamıştır.

## Kurulum ve Çalıştırma

1. Depoyu klonlayın:
   ```
   git clone https://github.com/Bilalenginn/YMH219-FlappyBird.git
   ```
2. `YMH219-FlappyBird.sln` dosyasını Visual Studio 2022 ile açın.
3. Gerekirse **.NET 8 SDK** ve "Masaüstü geliştirme (.NET)" iş yükünün yüklü olduğundan emin olun.
4. **F5** ile projeyi derleyip çalıştırın.

## Nasıl Oynanır?

| Tuş / Buton | İşlev |
|---|---|
| `Space` | Oyunu başlatır, kuşu yukarı zıplatır |
| `R` veya "Yeniden Başlat" butonu | Oyunu sıfırlar |

Kuş yer çekimiyle sürekli aşağı iner. Boruların arasındaki boşluktan geçtikçe skor artar. Borulara, zemine veya tavana çarpınca oyun biter.

## Temel Özellikler

- Klavye ile kuşu zıplatma ve yer çekimi simülasyonu
- Sağdan sola hareket eden, ekran dışına çıkınca yeniden konumlandırılan borular (rastgele boşluk yüksekliği)
- Çarpışma kontrolü ve "Game Over" ekranı
- Boru geçildikçe artan skor
- Her 5 skorda seviye artışı ve buna bağlı hız artışı
- Oturum içi en yüksek skor takibi
- Yeniden başlatma (klavye ve buton)

## Proje Yapısı

| Dosya | Açıklama |
|---|---|
| `Bird.cs` | Kuşun konumu, hızı, yer çekimi ve zıplama mantığı |
| `Pipe.cs` | Borunun hareketi, yeniden konumlanması ve çarpışma alanları |
| `Game.cs` | Oyun durumu: skor, seviye, hız, çarpışma kontrolü, sıfırlama |
| `Form1.cs` | Arayüz, Timer ile oyun döngüsü, klavye olayları ve çizim |

Oyun mantığı (`Game`, `Bird`, `Pipe`) arayüzden bağımsız sınıflarda tutulmuş, `Form1` yalnızca olayları yönlendirip çizim yapmaktadır.

## Ekran Görüntüleri

         [Oyun ekranı]
<img width="236" height="321" alt="bird1" src="https://github.com/user-attachments/assets/31bfd7d6-979e-4255-a117-40cf099fc58a" />


          [Game Over ekranı]
<img width="237" height="319" alt="bird2" src="https://github.com/user-attachments/assets/b01aadee-e14b-4b2b-85ea-f7952af75038" />


## Bilinen Sınırlamalar / Geliştirme Notları

- En yüksek skor yalnızca uygulama açıkken tutulur, dosyaya kaydedilmez.
- Ses efekti ve zorluk seviyesi seçimi yoktur.
- Kuş ve borular basit geometrik şekillerle çizilmiştir, görsel (sprite) kullanılmamıştır.
