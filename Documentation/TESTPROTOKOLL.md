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
