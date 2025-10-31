# Light Seeker — Roadmap

> Kısa açıklama: Light Seeker, oyuncunun bir ışık huzmesini yönlendirip yansıtıcı ve kırıcı yüzeylerle hedeflere ulaştırdığı, fizik tabanlı bir 2D bulmaca-keşif oyunudur. Bu roadmap üç öncelik seviyesinde düzenlenmiştir: Çekirdek Mekanikler, Görsel & Teknik Temeller ve Gelişmiş Sistemler & Sunum.

---

## İçindekiler

1. Çekirdek Mekanikler
2. Görsel & Teknik Temeller
3. Gelişmiş Sistemler ve Sunum
4. Proje klasör yapısı & başlangıç dosyaları
5. Kilit kilometre taşları (milestones) ve kabul kriterleri

---

# 1️⃣ Çekirdek Mekanikler

Amaç: Oyunun oynanışını ve temel ışık fiziğini çalışır hâle getirmek — oynanabilir, test edilebilir ve genişletilebilir bir prototip.

* **Işık huzmesi temel sistemi**

  * 2D raycast tabanlı ışık huzmesi (Physics2D.Raycast kullanarak) implementasyonu.
  * Huzme başlatma noktası (Emitter) ve yön kontrolü (fare/joystick) mekanikleri.
  * Huzmenin görsel temsili: çizgi (LineRenderer benzeri 2D çözüm) + küçük parlama/flare particle.
  * Deliverable: `Scene/Prototype_Core.unity` — tek bir test sahnesi.

* **Yansıtıcı yüzeyler (Mirror)**

  * Yansıma mantığı: gelen ışık vektörünü normal üzerinden yansıtma (Vector2.Reflect).
  * Mirror prefab: ayarlanabilir normal, tag/layer, enerji kaybı (isteğe bağlı float).
  * Deliverable: `Prefabs/Mirror.prefab`, örnek test yerleşimi.

* **Kırıcı yüzeyler (Refractor / Lens / Prism)**

  * Basit Snell yasası benzeri kırılma yaklaşımı (indeks parametresi ile approximate kırılma)
  * Prizma için açısal ayrıştırma (basit olarak RGB kanallarını ayrı raylarla ayırma — estetik amaçlı)
  * Deliverable: `Prefabs/Lens.prefab`, `Prefabs/Prism.prefab`.

* **Hedef sistem (Goal / Receiver)**

  * Hedefler: belirli renk/enerji gereksinimi, tamamlanma tetikleyicisi.
  * Birden fazla hedef ve doğrulama (puzzle solved state).
  * Deliverable: `Prefabs/Receiver.prefab` + hedef kontrol scripti.

* **Enerji & attenuasyon**

  * Ray başına enerji değeri; yansıma/kırılma süreçlerinde enerji azalması.
  * Minimum enerji altındaki ışıkları kesme/sonlandırma.

* **Temel Level flow**

  * Başlat, reset, success/fail durumları.
  * Deliverable: `Scripts/GameFlowManager.cs`.

* **Testler ve debug araçları**

  * Görsel debug ray çizimleri, GUI'de anlık ray sayacı, frame-by-frame single-step modu.

---

# 2️⃣ Görsel & Teknik Temeller

Amaç: Mekanikleri estetik, verimli ve üretime yakın hale getirmek; sahne organizasyonu ve araç zinciri kurmak.

* **Aydınlatma & shading**

  * 2D ışık hissi için shader grafikleri (Shader Graph veya custom sprite shader): glow, additive blending, soft edges.
  * Glossy/metallic görünüm için normal map kullanımına hazır sprite materyalleri.
  * Deliverable: `Shaders/LightBeam.shader`, `Materials/Beam.mat`.

* **Particle & VFX**

  * Beam çıkış/çarpma/odak noktası particle şemaları (burst, spark, dust).
  * Post-process hissi için screen-space bloom (Unity 2D + URP önerisi).

* **Audio**

  * Etkileşim sesleri: yansıma ping, kırılma tın, hedef tamamlanma chime.
  * Dinamik SFX parametreleri (ör. enerjiye göre pitch/volume değişimi).

* **UI & UX temel bileşenleri**

  * Minimal HUD: level name, attempts, reset butonu, hedef göstergesi.
  * Menü: MainMenu, LevelSelect, Pause, LevelComplete ekranları.

* **Sahne (Scene) organizasyonu**

  * Önerilen sahneler: `Scenes/Prototype_Core`, `Scenes/Level_01`, `Scenes/MainMenu`, `Scenes/DevTools`.
  * Layer & sorting: Background, World, Interactables, BeamEffects, UI.

* **Performans & mobil/PC hedefleme**

  * Raycast budget (max ray depth / reflection count) ayarları.
  * Pooling sistemi (particle/impact prefabs) ve iş parçacığına uygun olmayan işlemlerden kaçınma.

* **Prefab, ScriptableObject altyapısı**

  * Mirror/Lens/Receiver parametreleri için ScriptableObject tabanlı tanımlar (tuning amaçlı).
  * Deliverable: `ScriptableObjects/SurfaceDefinitions.asset`.

---

# 3️⃣ Gelişmiş Sistemler ve Sunum

Amaç: Oyunu bir portföy ürünü hâline getirip, yayımlamaya/sergilemeye hazır hale getirmek.

* **Seviye tasarımı & progression**

  * Seviye kategorileri: Öğretici, Mekanik Kombinasyonu, Zaman/Dinamik Engeller, Estetik keşif odaları.
  * Parselasyon: küçük puzzle blokları -> birleşik seviye akışı.
  * Level pack ve zorluk eğrisi taslağı.

* **Gelişmiş ışık fizik özellikleri**

  * Dalga etkisi / soft scattering (optik efekt/ambience için approximate çözüm).
  * Renk karıştırma kuralları (kanal bazlı enerji transferi).
  * Lens distortion, chromatic aberration (shader ile estetik kırılma).

* **Editor araçları & level editörü**

  * Level tasarımcı için custom editorpencereleri: snap grid, align tools, bake preview (ray preview within editor).
  * Level import/export (JSON) ve playtest düğmesi.

* **Polish & accessibility**

  * Kontrast/ses kontrolleri, renk körlüğü modu (kanal etiketleme + simgeler), kontrolleri yeniden atama.

* **Analytics & telemetry**

  * Level completion times, average reflections used, player drop-off noktaları (temel telemetry to CSV/remote).

* **Portföy ve sunum materyalleri**

  * Kısa oynanış videosu (30–60s) — sahne seçimi, kamera zoom & pan planı.
  * GIF'ler / Web demo (WebGL) için optimize edilmiş 1–2 seviye.
  * ArtDirection.md ve DevNotes.md dosyalarının tamamlanması.
  * Release build checklist (PC & WebGL) ve küçük oynanış trailer planı.

---

# Proje klasör yapısı (öneri)

```
Assets/
  Scenes/
  Scripts/
    Core/            # Beam, Reflection, Refract, GameFlow
    Gameplay/        # Level logic, goals
    Systems/         # Pooling, AudioManager, UIManager
    Editor/          # Custom editor araçları
  Prefabs/
  Materials/
  Shaders/
  Particles/
  Audio/
  Art/
  ScriptableObjects/
  Docs/
    Roadmap.md
    DevNotes.md
    ArtDirection.md
    LevelDesign.md
```

---

# Başlangıç dosyaları (öneri)

* `Docs/Roadmap.md` (bu dosya)
* `Docs/DevNotes.md` (geliştirme kısa notları, bug tracker başlığı)
* `Scenes/Prototype_Core.unity` (ilk prototip)
* `Scripts/Core/BeamController.cs` (ilk derlenebilir script)

---

# Kilit kilometre taşları (milestones) & kabul kriterleri

1. **Proto-Playable**

   * İçerik: Beam emit, mirror yansıması, tek hedef. Playable flow tamam.
   * Kabul: Bir test sahnesinde oyuncu hedefi %100 enerji / doğru renk ile tetikleyebiliyor.

2. **Visual Polish + VFX**

   * İçerik: Beam shader, particles, basic audio, UI.
   * Kabul: Sahnedeki beam görseli tutarlı ve performans hedefleri karşılıyor (ör. 60 FPS desktop, 30 FPS mobil hedefi için ölçüm yapılmalı).

3. **Level Pack & Portfolio Build**

   * İçerik: 8–12 iyi tasarlanmış seviye, oynanış videosu, ArtDirection.md.
   * Kabul: Seviye oynanışları net, ders veriyor (tutor), portföy sunumunda 60s trailer hazır.

---

# Notlar & Sonraki adımlar

* İstersen `Scripts/Core/BeamController.cs` için eksiksiz, açıklamalı bir başlangıç scripti yazabilirim. (Bu Roadmap'e bağlı ilk adım olarak önerilir.)
* Ayrıca isterseniz Roadmap'i sprint bazlı (2 haftalık) görevlere bölebilirim.

---

*Hazırlandı: Light Seeker — Roadmap (İlk versiyon)*
