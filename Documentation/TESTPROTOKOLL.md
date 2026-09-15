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
