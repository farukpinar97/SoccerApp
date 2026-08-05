# ⚽ GoalZone — Premier Lig Skor & Fikstür Sistemi

GoalZone, İngiltere Premier Ligi'ne ait maç sonuçları, fikstür, puan durumu ve maç detaylarını sunan; **ASP.NET Core Web API** ve **MVC** olmak üzere iki katmanlı olarak  **Murat Yücedağ** eğitmenliğinde geliştirilmiş bir web uygulamasıdır.

---

## 📋 Proje Hakkında

Proje, 20 takımlı bir lig yapısını uçtan uca modelleyen bir API ve bu API'yi tüketen bir arayüzden oluşur.

Veri katmanında maçlar, takımlar, oyuncular, maç olayları ve istatistikler tutulur. Arayüz tarafında kullanıcı haftalık sonuçları görüntüleyebilir, sonraki haftanın fikstürünü inceleyebilir, puan durumunu takip edebilir ve herhangi bir maçın detay sayfasında gol/kart/değişiklik zaman çizelgesine ve maç istatistiklerine ulaşabilir.

Projenin ayırt edici yanı, **puan durumunun ayrı bir tabloda tutulmamasıdır**. Sıralama, puan, averaj ve son 5 maç form bilgisi; tamamlanmış maç sonuçlarından her istekte dinamik olarak hesaplanır. Yeni bir maç sonucu girildiğinde tablo kendiliğinden güncellenir.

---

## 🎯 Öne Çıkan Özellikler

### 🌐 Kullanıcı Tarafı (MVC)

- **Haftalık Sonuçlar** — Hafta seçimi (ileri/geri navigasyon + dropdown ile doğrudan atlama), maç durumuna göre filtreleme sekmeleri (Tümü / Canlı / Tamamlanan / Yaklaşan), öne çıkan maç kartı
- **Fikstür** — Bir sonraki haftanın maçları, güne göre gruplanmış listeleme, ilk maça canlı geri sayım
- **Takım Formu** — Her maç kartında iki takımın son 5 maç formu ayrı ayrı (G/B/M), butona tıklayınca AJAX ile açılan detaylı son 5 maç listesi
- **Puan Durumu** — Maç sonuçlarından hesaplanan dinamik tablo; oynanan, galibiyet, beraberlik, mağlubiyet, atılan/yenilen gol, averaj, puan ve form; Şampiyonlar Ligi / Avrupa Ligi / küme düşme bölge renkleri
- **Maç Detayı** — Gol, sarı kart, kırmızı kart ve oyuncu değişikliklerinin dakika bazlı zaman çizelgesi; oransal bar grafiklerle maç istatistikleri; maç bilgileri tablosu
- **Responsive Tasarım** — Mobil, tablet ve masaüstü uyumlu koyu tema

### 🔧 Admin Panel

- **Maç Ekleme** — Hafta, takımlar, stadyum, hakem, tarih/saat, durum ve skor bilgileri
- **Maç Olayı Ekleme** — Takım seçimine göre AJAX ile yüklenen oyuncu listesi; olay türü "Oyuncu Değişikliği" seçildiğinde otomatik açılan "oyuna giren" alanı
- **Maç İstatistiği Ekleme** — 10 metrik için ev/deplasman değerleri; topa sahip olma oranının otomatik tamamlanması (55 girilince diğeri 45 olur)
- 
---

## 🛠️ Kullanılan Teknolojiler

| Teknoloji | Açıklama |
|-----------|----------|
| **ASP.NET Core 8** | Web API ve MVC |
| **Entity Framework Core 8** | ORM, Code First yaklaşımı |
| **MSSQL Server** | İlişkisel veritabanı |
| **Swagger / Swashbuckle** | API dokümantasyonu ve testi |
| **IHttpClientFactory** | API tüketimi |
| **System.Text.Json** | Serileştirme |
| **Bootstrap 5.3** | Responsive tasarım |
| **Bootstrap Icons** | İkon seti |
| **Vanilla JavaScript** | Filtreleme, geri sayım, AJAX işlemleri |

---

## 📷 Ekran Görüntüleri

<img width="1920" height="1950" alt="01_Homepage" src="https://github.com/user-attachments/assets/99ad22b1-523c-48ff-a04b-6523a1af6eb7" />

<img width="1920" height="2505" alt="02_Fixture" src="https://github.com/user-attachments/assets/c9c9d8bb-ded2-4761-9859-77ff2b52e5fa" />

<img width="1920" height="1856" alt="03_Standing" src="https://github.com/user-attachments/assets/3c111648-adc5-4c4b-92b0-a385edb91a96" />

<img width="1920" height="2132" alt="04_MatchDetail" src="https://github.com/user-attachments/assets/2e3062e0-6f55-4157-ac4b-67f221fe08b3" />

<img width="1920" height="945" alt="05_Admin" src="https://github.com/user-attachments/assets/d7e5bf4f-c949-4480-912b-e7aae455ae97" />

<img width="1920" height="1357" alt="06_MatchAdd" src="https://github.com/user-attachments/assets/ca47a8dc-1335-436d-a0c1-3b3268ba4d01" />

<img width="1920" height="1021" alt="07_EventAdd" src="https://github.com/user-attachments/assets/edf3949e-63fa-4172-ae9b-12e90a489d82" />

<img width="1920" height="1449" alt="08_StatisticAdd" src="https://github.com/user-attachments/assets/cc788dda-038b-445e-ad7e-efa3f5e3c837" />


