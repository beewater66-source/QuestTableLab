# Testprotokoll

Für jeden Test werden Datum, Git-Commit, Unity-Version, Zielgerät, Testart, erwartetes Ergebnis und beobachtetes Ergebnis festgehalten.

## Testeinträge

### Test 001 – Quest-Bootstrap und Passthrough-Smoke-Test

| Feld | Eintrag |
|---|---|
| Start | 2026-09-15 08:24 CEST |
| Sichtprüfung bestätigt | 2026-09-15 08:48 CEST |
| Git-Branch | `feature/quest-bootstrap` |
| Ausgangscommit | `df16974` (`docs: expand project README`) |
| Unity-Version | 6000.3.24f1 |
| Zielgerät | Meta Quest 3 |
| Betriebssystem | Meta Horizon OS 2.7 |
| Testart | Development-Build, Installation, technischer Laufzeittest und Sichtprüfung im Headset |
| Ausgeführt von | Technische Umsetzung und Auslieferung: Codex/Worker; Sichtprüfung: Christoph Dorn |

**Erwartetes Ergebnis**

- Die Android-Build wird ohne Fehler erzeugt.
- Die APK lässt sich auf der verbundenen Quest 3 installieren und starten.
- OpenXR erkennt das Headset und initialisiert Positions- und Rotationstracking.
- Die reale Umgebung ist per Passthrough sichtbar.
- Das Schild `HELLO QUESTTABLELAB – Passthrough-Smoke-Test` ist lesbar.

**Beobachtetes Ergebnis**

- Development-Build erfolgreich mit 0 Fehlern und 7 Warnungen erzeugt.
- Build-Dauer beim ersten vollständigen IL2CPP-/ARM64-Build: ungefähr 13 Minuten.
- APK-Größe: 64.941.377 Byte, rund 65 MB.
- APK erfolgreich als `com.christophdorn.questtablelab` installiert.
- Anwendung wurde gestartet, blieb aktiv und befand sich im Vordergrund.
- OpenXR meldete Meta Quest 3 sowie verfügbares Positions- und Rotationstracking.
- Passthrough-Schnittstelle wurde vom Quest-Laufzeitsystem aktiviert.
- Christoph bestätigte im Headset die reale Umgebung und das sichtbare Schild.
- Das Schild bewegt sich mit dem Headset, weil es für diesen Smoke-Test bewusst an der XR-Kamera befestigt ist.

**Ergebnis: BESTANDEN**

Der vollständige Weg von der Unity-Szene über Android-Build und Installation bis zur sichtbaren Mixed-Reality-Ausgabe auf der Quest 3 ist nachgewiesen. Raumfeste Platzierung war nicht Gegenstand dieses Tests.

### Test 002 – Raumfester Würfel und World-Space-Beschriftung

| Feld | Eintrag |
|---|---|
| Start | 2026-09-15 09:13 CEST |
| Sichtprüfung bestätigt | 2026-09-15 09:28 CEST |
| Git-Branch | `feature/table-cube-placement` |
| Ausgangscommit | `d93dd39` (Merge von Pull Request #1) |
| Unity-Version | 6000.3.24f1 |
| Zielgerät | Meta Quest 3 |
| Betriebssystem | Meta Horizon OS 2.7 |
| Testart | Inkrementeller Development-Build, Installation, technischer Laufzeittest und Sichtprüfung im Headset |
| Ausgeführt von | Technische Umsetzung und Auslieferung: Codex; Sichtprüfung und räumliche Bewertung: Christoph Dorn |

**Erwartetes Ergebnis**

- Ein 20 cm großer Würfel erscheint vor dem Startpunkt.
- Der Würfel bleibt bei langsamen und schnellen Kopfbewegungen an derselben Stelle im Raum.
- Die Testmeldung ist nicht mehr an die Kamera gekoppelt und steht lesbar über dem Würfel.

**Beobachtetes Ergebnis**

- Der Würfel wurde als Szenenobjekt bei `(0, 0.9, 1.5)` angelegt.
- Christoph bestätigte, dass der Würfel auch bei schnellen Kopfbewegungen stabil im Raum bleibt.
- Der erste Beschriftungsversuch lag fast einen Meter unter dem Würfel und war zu klein.
- Ursache war die weiterhin auf Y = 0 stehende `Anchored Position` des Root-Canvas.
- Nach der Korrektur liegt das Panelzentrum bei `(0, 1.2, 1.5)` und die Schildbreite beträgt ungefähr 57 cm.
- Christoph bestätigte Position, Raumstabilität und Lesbarkeit der korrigierten Fassung.
- Alle drei Android-Builds wurden mit 0 Fehlern und den bereits bekannten 7 Hinweisen abgeschlossen.

**Ergebnis: BESTANDEN**

Die raumfeste Darstellung eines einfachen virtuellen Objekts und einer zugeordneten Beschriftung ist auf der Quest 3 praktisch nachgewiesen. Eine semantische Tischfläche oder ein persistenter Raumanker wird in diesem Test noch nicht verwendet.

### Test 003 – Automatisierte Play-Mode-Prüfung

| Feld | Eintrag |
|---|---|
| Ausgeführt | 2026-09-15 10:10–10:16 CEST |
| Git-Branch | `feature/table-cube-placement` |
| Ausgangscommit | `102e607` (`feat: add room-fixed cube and label`) |
| Unity-Version | 6000.3.24f1 |
| Testumgebung | Unity Editor, Play Mode |
| Fast Enter Play Mode | Domain Reload und Scene Reload deaktiviert |
| Ausgeführt von | Testentwurf, Implementierung und Ausführung: Codex; Fast-Reload-Entscheidung und manuelle Schildanpassung: Christoph Dorn |

**Geprüft**

- `RoomFixedTestCube` existiert auf der Szenenwurzel und besitzt die erwartete Pose und Skalierung.
- `HelloPanel` existiert auf der Szenenwurzel, verwendet einen World-Space-Canvas und bleibt oberhalb sowie in visueller Nähe des Würfels.

**Beobachteter Lernverlauf**

- Ein erster Compilerfehler im Vektorvergleich wurde korrigiert.
- Ein erster Lauf wurde durch einen Speichern-Dialog blockiert und abgebrochen.
- Ein Lauf ohne explizites Laden von `SampleScene` schlug mit 0 von 2 Tests fehl, weil der Test Runner eine eigene leere Testszene verwendet.
- Nach Wiederherstellung des expliziten Szenenladens bestand zunächst 1 von 2 Tests.
- Die verbleibende Abweichung stammte aus Christophs bewusster manueller Schildpositionierung. Der Test wurde auf die fachliche Anforderung „visuell nahe am Würfel“ ausgerichtet.
- Abschließend bestanden 2 von 2 Tests in 1,35 Sekunden.

**Ergebnis: BESTANDEN**

Milestone 2 besitzt nun neben dem praktischen Quest-Nachweis eine schnelle automatisierte Editor-Prüfung.

### Test 004 – Controllersteuerung und manuelle Fußbodenplatzierung

| Feld | Eintrag |
|---|---|
| Ausgeführt | 2026-09-15 10:30–11:54 CEST |
| Git-Branch | `feature/controller-input` |
| Ausgangscommit | `b223204` (Merge von Pull Request #2) |
| Unity-Version | 6000.3.24f1 |
| Zielgerät | Meta Quest 3 |
| Betriebssystem | Meta Horizon OS 2.7 |
| Testarten | Play Mode sowie Development-Build, Installation und Sicht-/Interaktionstest im Headset |
| Ausgeführt von | Umsetzung, Tests und Build: Codex; praktische Bedienung und visuelle Bewertung: Christoph Dorn |

**Erwartetes Ergebnis**

- Ein einzelner Strahl folgt der Pose des rechten Touch-Controllers.
- Der Strahl zeigt Triggerbetätigung farblich an und verschwindet beim Wechsel zur Handsteuerung.
- Der Strahl endet am nächsten Würfel- oder Fußbodentreffer.
- Der Würfel kann mit dem Trigger aufgenommen, bewegt und wieder raumfest abgelegt werden.
- Auf dem kalibrierten Fußboden erscheint eine grüne Platzierungsvorschau.
- Ein Triggerdruck auf den Fußboden platziert die Unterkante des Würfels auf Bodenniveau.
- Die B-Taste setzt Position und Rotation des Würfels zurück.
- Die Darstellung bleibt auf dem Gerät klar und ausreichend ruhig.

**Beobachteter Lernverlauf**

- Der erste Strahl erschien doppelt beziehungsweise magentafarben, war nicht korrekt am Controller verankert und blieb bei Handsteuerung aktiv.
- Ursachen waren die Anbindung an eine zu allgemeine Hand-/Controller-Hierarchie und ein in der Android-Build nicht enthaltener dynamisch gesuchter URP-Unlit-Shader.
- Nach dem Wechsel auf einen eigenen rechten Aim-Knoten und einen sicher enthaltenen URP/Lit-Shader bestätigte Christoph korrekte Pose, Farbe, Einzelanzeige und Ausblenden bei Handsteuerung.
- Die Render Scale wurde von 0,8 auf 1,0 erhöht; 4x MSAA blieb aktiv. Christoph bestätigte anschließend eine scharfe Darstellung ohne das zuvor störende Kantenflimmern.
- Würfelbewegung, verkürzter Zielstrahl und Reset wurden nacheinander auf der Quest bestätigt.
- Ein automatisierter Bodentest erkannte zunächst veraltete Collider-Grenzen direkt nach der Transform-Änderung. Nach expliziter Physik-Synchronisation lag die Würfelunterkante korrekt bei Y = 0.
- Abschließend bestanden 4 von 4 Play-Mode-Tests in 2,37 Sekunden.
- Die letzte Development-Build wurde mit 0 Fehlern und 7 bereits bekannten Warnungen erzeugt, installiert und gestartet.
- Christoph bestätigte die grüne Bodenvorschau und Platzierung mit „Green smoke“ als bestandenen Smoke-Test.

**Ergebnis: BESTANDEN**

Meilenstein 3 ist damit praktisch abgeschlossen. Die Bodenplatzierung verwendet den kalibrierten Floor-Level-Ursprung; semantische MRUK-Flächen sind Gegenstand von Meilenstein 4.

### Test 005 – Project-Setup-Vervollständigung und Overlay-Canvas

| Feld | Eintrag |
|---|---|
| Ausgeführt | 2026-09-15 12:00–12:24 CEST |
| Git-Branch | `feature/controller-input` |
| Ausgangscommit | `b223204` (Merge von Pull Request #2) |
| Unity-Version | 6000.3.24f1 |
| Zielgerät | Meta Quest 3 |
| Testarten | Meta Project Setup, Play Mode, Android-Development-Build, Installation und Start |
| Ausgeführt von | Project-Setup-Auswahl: Christoph Dorn; Overlay-Konfiguration, Tests, Build und Installation: Codex |

**Geprüft und umgesetzt**

- Rote Pflichtfehler des Meta Project Setup Tools sind beseitigt.
- D3D11 ist für Windows Standalone eingerichtet; die Android-Grafikkonfiguration der Quest bleibt davon getrennt.
- Meta XR Simulator sowie Meta XR Operator und dessen OpenXR API Layer sind eingerichtet.
- Scene Support ist im Manifest vorhanden und `OVRManager` fordert die Laufzeitberechtigung beim Start an.
- Application SpaceWarp ist als optionale Meta-Performancefunktion aktiviert.
- `HelloPanel` verwendet `OVROverlayCanvas` mit Depth-Tested-Komposition, Opaque-with-Clip, manuellem Redraw und Mipmaps.
- `HelloPanel` und sein Inhalt liegen auf dem versteckten Layer `Overlay UI`; die XR-Kameras rendern diesen Layer nicht zusätzlich direkt.
- Der temporäre Overlay-Render-Layer ist in den URP-Renderer-Masken enthalten.

**Automatisiertes Ergebnis**

- Ein erster neuer Overlay-Test war zu streng und bewertete eine von `OVROverlayCanvas` zur Laufzeit erzeugte Hilfskamera fälschlich als authored UI-Inhalt.
- Nach Eingrenzung auf `HelloPanel` und `Message` bestanden 5 von 5 Play-Mode-Tests in 2,85 Sekunden.
- Android-Build `build_586a062b76dd` bestand in 97,6 Sekunden mit 0 Fehlern und 7 bekannten Hinweisen.
- APK-Größe: 65.170.237 Byte, rund 65,2 MB.
- Die APK wurde auf der verbundenen Quest 3 installiert und `com.christophdorn.questtablelab` erfolgreich gestartet.
- Christoph bestätigte anschließend im Headset die korrekte Darstellung und die weiterhin funktionierende Interaktion mit „Green“.

**Ergebnis: BESTANDEN**

Der vollständige Stand aus Project Setup, Overlay-Darstellung und bisheriger Controllerinteraktion ist damit im Editor und auf der Quest 3 nachgewiesen.

### Test 006 – MRUK-Integration und semantische TABLE-Platzierung

| Feld | Eintrag |
|---|---|
| Vorbereitet | 2026-09-15 12:24–12:52 CEST |
| Git-Branch | `feature/semantic-table-placement` |
| Ausgangscommit | `599d4db` (Merge von Pull Request #3) |
| Unity-Version | 6000.3.24f1 |
| MRUK-Version | 205.0.0 |
| Zielgerät | Meta Quest 3 |
| Testarten | Play Mode, Android-Build, Installation und räumlicher Headset-Test |
| Ausgeführt von | Implementierung, Tests, Build und Installation: Codex; räumliche Sichtprüfung: Christoph Dorn |

**Geprüft und umgesetzt**

- MRUK lädt Scene-API-Daten explizit vom Gerät und kann dadurch konkrete Fehlerzustände melden.
- Die Auswahl berücksichtigt nur Anker mit dem semantischen Label `TABLE` und gültigem Volumen.
- Bei mehreren Tischen wird horizontal der dem Benutzer nächstgelegene gewählt.
- Der Würfel wird mit seiner Unterkante auf die von MRUK definierte Tischoberkante gesetzt.
- Nach erfolgreicher Platzierung führt die B-Taste zur semantischen Tischposition zurück.
- Fehlende Raumfreigabe, fehlendes Space Setup und fehlendes `TABLE`-Label erhalten verständliche Meldungen.

**Automatisiertes Ergebnis**

- 8 von 8 Play-Mode-Tests bestanden, einschließlich simulierter Auswahl aus nahen, fernen, falsch gelabelten und geometrisch ungeeigneten Ankern.
- Android-Build `build_985d85fb85af` bestand in 140,8 Sekunden mit 0 Fehlern und 7 bekannten Hinweisen.
- APK-Größe: 65.180.177 Byte.
- APK erfolgreich auf der verbundenen Quest 3 installiert und bis zur OpenXR-Initialisierung für Meta Quest 3 gestartet.
- Das Manifest enthält `USE_SCENE` und `USE_ANCHOR_API`.

**Räumliches Ergebnis**

Christoph bestätigte die Platzierung auf beiden im Space Setup erfassten Tischen. Je nach Benutzerposition wurde jeweils der horizontal nächstgelegene geeignete Tisch gewählt. Die Auswahl ist damit semantisch und positionsbezogen; sie richtet sich nicht nach dem aktuellen Sichtfeld.

**Ergebnis: BESTANDEN**

### Test 007 – Automatische WALL_FACE-Platzierung des Schilds

| Feld | Eintrag |
|---|---|
| Datum | 2026-09-15 |
| Branch | `feature/semantic-table-placement` |
| Unity-Version | 6000.3.24f1 |
| MRUK-Version | 205.0.0 |
| Zielgerät | Meta Quest 3 |
| Testarten | Play Mode, Android-Build und Installation; räumlicher Sichttest folgt |
| Ausgeführt von | Implementierung und Play-Mode-Tests: Codex; räumliche Sichtprüfung: Christoph Dorn |

**Geprüft und umgesetzt**

- Es werden nur gültige `WALL_FACE`-Anker mit ebener Begrenzung berücksichtigt.
- Bevorzugt wird eine Wand in Blickrichtung, deren Vorderseite zum Benutzer zeigt; andernfalls dient die nächstgelegene zum Benutzer gerichtete Wand als Rückfall.
- Horizontaler und vertikaler Versatz sowie der Abstand vor der Wand sind im Inspector einstellbar.
- Die Zielposition wird so begrenzt, dass das Schild einschließlich seiner Größe innerhalb der erkannten Wandfläche bleibt.
- Die bestehende `OVROverlayCanvas`-Darstellung wird weiterverwendet.

**Automatisiertes Ergebnis**

- 10 von 10 Play-Mode-Tests bestanden.
- Keine Kompilierfehler und keine neuen Console-Fehler.
- Android-Build `build_1c002570b394` in 157 Sekunden mit 0 Fehlern und 7 bekannten Warnungen erfolgreich erstellt.
- APK erfolgreich auf der verbundenen Quest 3 installiert.

**Zwischenergebnis: ERSTER SICHTTEST NICHT BESTANDEN**

**Erster räumlicher Sichttest und Korrektur**

Der Gerätetest bestätigte über das Quest-Log einen gültigen `WALL_FACE`-Anker und eine berechnete Schildposition. Das Schild selbst war jedoch nicht sichtbar. Ursache war die unterschiedliche Vorwärtsdefinition: Die MRUK-Wandnormale zeigt in den Raum, während die sichtbare Vorderseite des World-Space-Canvas auf der entgegengesetzten lokalen Seite liegt. Die Schildrotation wurde deshalb um 180 Grad korrigiert; ein erneuter Sichttest ist erforderlich.

Die korrigierte Fassung bestand erneut 10 von 10 Play-Mode-Tests. Android-Build `build_66693d341d43` wurde in 111 Sekunden mit 0 Fehlern und 7 bekannten Warnungen erstellt und erfolgreich auf der Quest 3 installiert.

**Abschließendes räumliches Ergebnis**

Christoph bestätigte, dass das korrigierte Schild sichtbar und raumfest auf der erkannten Wand erscheint. Würfel, semantische Tischplatzierung und semantische Wandplatzierung funktionierten gemeinsam im selben Quest-Lauf.

**Ergebnis: BESTANDEN**

### Test 008 – Flächengebundene Replatzierung

| Feld | Eintrag |
|---|---|
| Datum | 2026-09-15 |
| Branch | `feature/semantic-table-placement` |
| Zielgerät | Meta Quest 3 |
| Testarten | Play Mode, Android-Build, Installation und räumlicher Interaktionstest |
| Ausgeführt von | Implementierung und technische Tests: Codex; räumliche Interaktion: Christoph Dorn |

**Geprüft und umgesetzt**

- Das Schild besitzt einen unsichtbaren Collider in Canvas-Größe und ist dadurch mit dem Controllerstrahl greifbar.
- Während des Greifens wird der Würfel auf die Oberseite seines gewählten `TABLE`-Ankers projiziert.
- Das Schild wird während des Greifens auf die Ebene seines gewählten `WALL_FACE`-Ankers projiziert.
- Beide Zielpositionen berücksichtigen die Objektgröße und werden innerhalb der semantischen Flächengrenzen gehalten.
- Ohne geladene semantische Tischdaten bleibt die bisherige freie Würfelbewegung als Rückfall erhalten.
- B setzt Würfel und Schild auf ihre semantischen Ausgangspositionen zurück.

**Technisches Ergebnis**

- 11 von 11 Play-Mode-Tests bestanden.
- Android-Build `build_79be74699a90` in 248 Sekunden mit 0 Fehlern und 7 bekannten Warnungen erstellt.
- APK erfolgreich auf der verbundenen Quest 3 installiert.

**Räumliches Ergebnis**

Christoph bestätigte auf der Quest 3, dass sich Würfel und Schild per Controller auf ihrer erkannten Tisch- beziehungsweise Wandfläche verschieben lassen. Die Flächenbegrenzung und das gemeinsame Zurücksetzen mit B funktionierten im Gerätetest.

**Ergebnis: BESTANDEN**
