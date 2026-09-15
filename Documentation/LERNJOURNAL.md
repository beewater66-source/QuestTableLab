# Lernjournal: QuestTableLab als AR-/MR-Grundlage

Stand: 2026-09-15 12:52 CEST
Projekt: QuestTableLab  
Zielgerät: Meta Quest 3

## Übergeordnetes Lernziel

QuestTableLab ist meine allgemeine Lern- und Testumgebung für Mixed Reality. Ich möchte damit den gesamten Weg von meinen ersten praktisch verwendeten Git-Grundlagen bis zu einer wiederverwendbaren Quest-AR-Basis verstehen und dokumentieren.

Der Lernweg umfasst:

1. Git und GitHub im Projektalltag verstehen.
2. Eine eigene Mixed-Reality-Anwendung auf der Quest 3 bauen, installieren und testen.
3. Virtuelle Inhalte raumfest anzeigen und manuell steuern beziehungsweise platzieren.
4. Semantische Raumlabels wie `TABLE` und `WALL_FACE` auslesen und nutzen.
5. Reale Kategorien konfigurierbar virtuellen Inhalten zuweisen.
6. Aus dem stabilen Grundprojekt später ein bereinigtes Template ableiten.

Der nicht funktionale 3D-Druck-Prototyp mit eigener Teileerkennung und Wartungs- oder Reparaturbegleitung gehört zu einem **separaten späteren Praktikumsprojekt**. Er soll auf dem hier erarbeiteten Template aufbauen. QuestTableLab darf allgemeine Grundlagen für Objektzuordnung und spätere Erkennung vorbereiten, enthält aber keine prototypspezifische Wartungslogik.

## Rollen und Arbeitsweise

### Selbst ausgeführt – Christoph Dorn

- Unity 6000.3.24f1 und Android Build Support einschließlich SDK, NDK und OpenJDK installiert.
- Unity-/Meta-Developer-Konten, Developer Hub, Developer Mode und USB-Debugging eingerichtet.
- Quest 3 verbunden und als Entwicklungsgerät geprüft.
- Repository und Unity-Projekt angelegt sowie den versehentlich verschachtelten Projektordner korrigiert.
- Projekt im Unity Hub neu verknüpft und Build Profile auf Meta/Android umgestellt.
- Git-Branches, Commits, Publish/Push und Pull Requests praktisch durchgeführt.
- Build-Ergebnisse im Headset bewertet und Fehlerbilder präzise zurückgemeldet.
- Passthrough, Raumstabilität, Beschriftung, Bildschärfe, Controller-Strahl, Handwechsel, Würfelbewegung, Reset und Bodenplatzierung praktisch geprüft.
- Den abschließenden Meilenstein-3-Test mit „Green smoke“ als bestanden bestätigt.
- Pflichtfehler und Hinweise im Meta Project Setup Tool geprüft und behoben.
- Windows-Rendering auf D3D11 umgestellt, Meta XR Simulator installiert und die Scene-Berechtigungsanfrage aktiviert.
- Application SpaceWarp als Meta-Performanceempfehlung aktiviert.

### An Codex delegiert

- Lern- und Projektfahrplan strukturieren sowie Git- und Unity-Schritte erklären.
- Eigene Projektstruktur unter `Assets/App` anlegen.
- Android-, OpenXR-, Meta-XR- und Passthrough-Konfiguration prüfen.
- Szenenobjekte, Controllersteuerung und Platzierungslogik in der laufenden Unity-Umgebung umsetzen.
- Renderqualität untersuchen und korrigieren.
- Play-Mode-Tests entwickeln, ausführen und Fehlerursachen beheben.
- Android-Builds erzeugen, installieren und starten.
- Dokumentation, Entscheidungen und Testergebnisse fortlaufend pflegen.
- `HelloPanel` auf `OVROverlayCanvas` mit getrennten Render-Layern umstellen und automatisch prüfen.

### Gemeinsame Arbeitsregel

Es wird jeweils ein überschaubarer Schritt umgesetzt und auf dem Gerät geprüft. Ein technischer Build-Erfolg zählt nicht allein als Abschluss: Für räumliche und visuelle Funktionen ist Christophs praktischer Test in der Quest maßgeblich. Erst nach bestandenem Smoke-Test werden Dokumentation und Git-Stand abgeschlossen.

## Meilensteine und Erfolgskriterien

### Meilenstein 0 – Voraussetzungen

Unity, Android-Werkzeuge, Developer-Zugang und Geräteverbindung sind einsatzbereit. **Abgeschlossen.**

### Meilenstein 1 – Repository und Projekt

Repository, Unity-Projektstruktur, `.gitignore`, erste Commits, Branch- und Pull-Request-Arbeitsweise sind vorhanden. **Abgeschlossen.**

### Meilenstein 2 – Erste Quest-Anwendung

Eine eigene Development-Build läuft auf der Quest 3 und kombiniert Passthrough mit sichtbaren virtuellen Inhalten. Würfel und Beschriftung sind raumfest. **Abgeschlossen.**

### Meilenstein 3 – Manuelle Platzierung

Der rechte Controller besitzt einen eindeutigen Zielstrahl. Der Würfel lässt sich aufnehmen, bewegen, auf dem kalibrierten Fußboden ablegen und zurücksetzen. Editor-Tests und Gerätetest bestehen. **Abgeschlossen.**

### Meilenstein 4 – Semantische Raumlabels

MRUK stellt Raumdaten bereit. Die Anwendung erkennt `TABLE`, unterscheidet geometrische Treffer von semantischer Bedeutung und platziert den Würfel relativ zur Tischfläche. Der Gerätetest war auf beiden erfassten Tischen erfolgreich. Das Schild wird automatisch an einer geeigneten `WALL_FACE` platziert; sein Versatz und Wandabstand bleiben konfigurierbar. Nach Korrektur der Canvas-Ausrichtung bestand auch dieser Gerätetest. **In Arbeit; flächengebundene Replatzierung folgt.**

### Meilenstein 5 – Konfigurierbare Zuordnung und Template-Basis

Semantische Kategorien und virtuelle Inhalte werden über eine verständliche Konfiguration verbunden. Grenzen und Fehlerfälle sind dokumentiert. Anschließend kann aus dem bereinigten Stand ein wiederverwendbares Template für neue Quest-AR-Projekte entstehen. **Geplant.**

## Zeitlicher Verlauf

Alle Zeiten sind lokale Zeit in Deutschland (CEST). Git-bestätigte Zeitpunkte stammen aus der Commit-Historie; Gesprächs- und Testzeitpunkte wurden beim Dokumentieren ergänzt.

| Zeitpunkt | Ausgeführt von | Ereignis | Ergebnis / Lernfortschritt |
|---|---|---|---|
| 2026-09-14, vor Projektbeginn | Christoph | Entwicklungsumgebung und Quest vorbereitet | Unity, Android-Werkzeuge, Developer Hub, Developer Mode und USB-Debugging einsatzbereit. |
| 2026-09-14 09:19 | Christoph | Repository initialisiert | Erster Ausgangspunkt auf `main`, Commit `2cf6743`. |
| 2026-09-14 11:05 | Christoph | Unity-Projekt erstellt | Unity-6.3-URP-Projekt, Commit `30898c4`. |
| 2026-09-14, vormittags | Christoph mit Anleitung durch Codex | Verschachtelten Projektordner korrigiert | Projektinhalt in die Repository-Wurzel verschoben und im Hub neu verknüpft. |
| 2026-09-14 11:41 | Codex, von Christoph delegiert | Projektstruktur angelegt | Eigene Bereiche für Szenen, Skripte, Prefabs, Art und UI; anschließend Commit `0e289dc`. |
| 2026-09-14 bis 2026-09-15 08:48 | Gemeinsam | Quest-Bootstrap umgesetzt und getestet | Pakete, OpenXR, Passthrough, Build, Installation und sichtbare Testmeldung nachgewiesen. |
| 2026-09-15, vormittags | Christoph | Pull Request #1 gemergt | Bootstrap in `main`, Merge-Commit `d93dd39`; Branch gelöscht. |
| 2026-09-15 09:13–09:28 | Gemeinsam | Raumfesten Würfel und Beschriftung umgesetzt | Würfel bleibt bei schnellen Kopfbewegungen stabil; Schild nach manueller Feinjustierung gut lesbar. |
| 2026-09-15 10:10–10:16 | Codex, von Christoph delegiert | Erste Play-Mode-Tests eingerichtet | Szenenstruktur und räumliche Zuordnung mit 2 von 2 Tests bestätigt. |
| 2026-09-15, vormittags | Christoph | Pull Request #2 gemergt | Raumfester Würfel, Beschriftung und Testbasis in `main`; Merge-Commit `b223204`. |
| 2026-09-15 10:30–10:54 | Gemeinsam | Darstellungs- und Controllerprobleme untersucht | Render Scale von 0,8 auf 1,0 erhöht; Strahl an einen eigenen rechten Controller-Aim gebunden; Darstellung auf der Quest bestätigt. |
| 2026-09-15 ca. 11:00–11:35 | Gemeinsam | Würfelsteuerung und Reset umgesetzt | Würfel per Trigger bewegbar, Strahl am Treffer verkürzt, B-Taste setzt den Würfel zurück. |
| 2026-09-15 11:35–11:54 | Gemeinsam | Manuelle Fußbodenplatzierung umgesetzt und getestet | Grüne Vorschau auf Floor-Level, Platzierung mit Würfelunterkante auf dem Boden, 4 von 4 Play-Mode-Tests, erfolgreiche Quest-Build und bestandener Smoke-Test. |
| 2026-09-15 12:00–12:24 | Gemeinsam | Meta Project Setup vervollständigt und UI auf Overlay-Rendering umgestellt | D3D11 für Standalone, Simulator, Operator/API Layer, Scene Permission und SpaceWarp eingerichtet; `HelloPanel` verwendet einen getrennten Overlay-Layer; 5 von 5 Play-Mode-Tests, Android-Build und Sichtprüfung bestanden. |
| 2026-09-15 12:24–12:52 | Codex, von Christoph delegiert | Meilenstein 4 begonnen: MRUK und semantische Tischsuche integriert | Neuer Branch von PR-#3-Merge; Scene API lädt explizit vom Gerät, wählt das nächste geeignete `TABLE`-Volumen, platziert den Würfel auf dessen Oberkante und aktualisiert den Reset. 8 von 8 Play-Mode-Tests, Android-Build und Installation bestanden; räumliche Sichtprüfung steht aus. |
| 2026-09-15 ca. 13:20 | Christoph | Semantische Tischplatzierung auf der Quest praktisch geprüft | Der Würfel wurde erfolgreich auf beiden im Space Setup erfassten Tischen platziert. Die Auswahl erfolgt abhängig von der Benutzerposition über den horizontal nächstgelegenen geeigneten Tisch, nicht über das Sichtfeld. |
| 2026-09-15 ca. 13:25–13:40 | Codex, von Christoph delegiert | Automatische Schildplatzierung an `WALL_FACE` vorbereitet | Eine sichtbare und zum Benutzer gerichtete Wand wird gewählt. Horizontaler und vertikaler Versatz sowie Abstand vor der Wand sind einstellbar und werden auf die erkannte Wandfläche begrenzt. 10 von 10 Play-Mode-Tests, Android-Build und Installation bestanden; räumlicher Sichttest steht aus. |
| 2026-09-15 ca. 13:42 | Gemeinsam | Ersten WALL_FACE-Sichttest ausgewertet | Der Würfel erschien korrekt, das Schild blieb unsichtbar. Das Quest-Log bestätigte Wandanker und Zielposition; die Canvas-Vorderseite war gegenüber der MRUK-Wandnormalen verkehrt ausgerichtet. Codex korrigierte die Rotation für den nächsten Build. |
| 2026-09-15 ca. 13:47 | Christoph | Korrigierte WALL_FACE-Platzierung praktisch bestätigt | Das Schild erschien sichtbar und raumfest auf der erkannten Wand. Der abschließende Smoke-Test war grün. |
| 2026-09-15 ca. 13:55–14:05 | Codex, von Christoph delegiert | Flächengebundene Replatzierung umgesetzt | Würfel und Schild lassen sich per Trigger greifen. Controllerstrahlen werden auf die semantische Tisch- beziehungsweise Wandebene projiziert und die Objektgrenzen innerhalb des jeweiligen Ankers gehalten. B setzt beide zurück. 11 von 11 Play-Mode-Tests und Android-Build bestanden; Quest-Sichttest folgt. |
| 2026-09-15 ca. 14:08 | Christoph | Flächengebundene Interaktion auf der Quest bestätigt | Würfel und Schild ließen sich innerhalb ihrer Tisch- beziehungsweise Wandflächen verschieben; Begrenzung und gemeinsamer Reset funktionierten. Smoke-Test bestanden. |

## Zentrale Learnings

### Git und GitHub

- Ein Repository ist der versionierte Projektbereich; ein Commit ist ein benannter Zwischenstand darin.
- Ein lokaler Commit ist kein vollständiges Rechner-Backup. Erst Push beziehungsweise Publish legt den versionierten Stand zusätzlich auf GitHub ab.
- Ein Branch ist eine getrennte Entwicklungslinie. Ein Pull Request vergleicht diese Linie mit `main`, ermöglicht eine Prüfung und führt die Änderungen anschließend kontrolliert in den Stamm zusammen.
- Nach einem Merge kann der abgeschlossene Feature-Branch gelöscht werden; der Inhalt bleibt über `main` und die Historie erhalten.

### Unity- und Quest-Workflow

- Ein erfolgreicher Editor-Test ersetzt keinen Gerätetest. Raumgefühl, Lesbarkeit, Tracking und Bildqualität müssen im Headset bewertet werden.
- Ein Objekt auf der Szenenwurzel ist unabhängig von der XR-Kamera und kann dadurch raumfest erscheinen.
- Ein eigener Runtime-Assembly-Bereich macht App-Code aus Play-Mode-Test-Assemblies sauber referenzierbar.
- Fast Enter Play Mode bleibt mit deaktiviertem Domain- und Scene-Reload aktiv. Tests müssen ihre Szene ausdrücklich laden und eigener Laufzeitzustand muss bewusst zurückgesetzt werden.

### Rendering und Controller

- Die Render Scale von 0,8 verursachte auf der Quest sichtbar unscharfe beziehungsweise jitternde Kanten. Render Scale 1,0 mit 4x MSAA wurde auf dem Gerät als scharf bestätigt.
- Ein Controller-Strahl darf nicht an einer allgemeinen Hand-Hierarchie hängen, wenn er ausschließlich den rechten Touch-Controller repräsentieren soll. Ein eigener Aim-Knoten unter dem Tracking Space trennt Pose und Darstellungszustand sauber.
- Ein zur Laufzeit gesuchter URP-Unlit-Shader war in der Android-Build nicht enthalten und erzeugte eine magentafarbene Fehlerdarstellung. Ein bereits sicher referenzierter URP/Lit-Shader vermeidet dieses Shader-Stripping-Problem.
- Der Controller-Strahl wird am nächsten gültigen Treffer verkürzt. Das vermittelt besser, welches Objekt oder welcher Punkt tatsächlich ausgewählt wird.
- Ein `OVROverlayCanvas` benötigt einen eigenen versteckten Szenen-Layer. Den allgemeinen `Default`-Layer aus der Kamera-Maske zu entfernen wäre falsch, weil dadurch auch normale Szenenobjekte verschwinden könnten.
- Für das statische Schild verwendet das Overlay Depth-Tested-Komposition, Opaque-with-Clip, manuelles Redraw und automatisch erzeugte Mipmaps. Dadurch wird das Schild nicht unnötig in jedem Frame neu gerendert.

### Platzierung

- Meilenstein 3 verwendet bewusst den kalibrierten Floor-Level-Ursprung und eine geometrische Bodenebene. Das ist noch keine semantische MRUK-Erkennung.
- Eine grüne Vorschau zeigt vor dem Auslösen, wo der Würfel platziert wird. Beim Platzieren wird die halbe Würfelhöhe berücksichtigt, damit seine Unterkante statt seines Mittelpunkts auf dem Boden liegt.
- Nach einer Transform-Änderung können Collider-Grenzen im selben Testschritt noch veraltet sein. `Physics.SyncTransforms()` stellt sicher, dass der Test die aktuelle Position bewertet.
- Eine Reset-Funktion gehört früh in eine interaktive Testumgebung. Sie beschleunigt wiederholbare Gerätetests und verhindert, dass ein ungünstig platziertes Objekt den Versuch blockiert.
- Die Quest Scene API liefert ein zuvor im Space Setup gespeichertes Raummodell. `TABLE` ist damit eine semantische Klassifizierung eines Raumankers und keine bei jedem Start neu ausgeführte allgemeine Bilderkennung.
- MRUK ist die Unity-Hilfsschicht für das Laden, Abfragen und räumliche Ausrichten dieser Scene-API-Anker. Bei Volumenankern definiert MRUK die Transform-Position als Mittelpunkt der Oberseite.
- Die automatische Platzierung ersetzt die manuelle Steuerung nicht. Nach erfolgreicher Tischsuche bleibt der Würfel bewegbar; die B-Taste kehrt nun zur semantisch ermittelten Tischposition zurück.
- Die Tischwahl verwendet bewusst die horizontale Nähe und nicht das Sichtfeld. Für Status-UI ist dagegen die Blickrichtung sinnvoll: Das Schild bevorzugt eine sichtbare `WALL_FACE`, die zum Benutzer zeigt.
- Eine gültige Wandposition garantiert noch keine sichtbare UI: MRUKs Wandnormale und die sichtbare Seite eines Unity-Canvas verwenden entgegengesetzte Vorwärtsrichtungen. Das Quest-Log half, Erkennungs- und Ausrichtungsfehler voneinander zu trennen.
- Für flächengebundene Interaktion reicht eine semantische Startposition nicht. Jeder neue Controller-Zielpunkt wird in den lokalen Koordinatenraum des erkannten Ankers umgerechnet und dort einschließlich der Objektgröße begrenzt.

## Aktueller Stand

Erreicht:

- Meilensteine 0 bis 3 vollständig umgesetzt, per Pull Request #3 gemergt und praktisch nachgewiesen.
- Milestone-4-Fassung mit MRUK sowie bestätigter `TABLE`- und `WALL_FACE`-Platzierung implementiert.
- Zehn Play-Mode-Tests bestanden.
- Android-Build `build_985d85fb85af` mit 0 Fehlern erstellt und auf der Quest installiert.
- Controller-Strahl, Bewegung, Bodenplatzierung und Reset funktionieren.
- README, Lernjournal, Entscheidungen und Testprotokoll auf den allgemeinen Template-/Lernzweck ausgerichtet.

Noch offen:

- Würfel innerhalb seines erkannten Tisches kontrolliert replatzierbar machen.
- Schild innerhalb seiner erkannten Wand kontrolliert replatzierbar machen.
- Zwischen mehreren erkannten Tischen und Wänden wechseln.

## Nächster einzelner Lernschritt

Eine verständliche Controllerbedienung für den Wechsel des aktiven `TABLE`- beziehungsweise `WALL_FACE`-Ankers festlegen und umsetzen.

## Vorlage für neue Einträge

```text
Zeitpunkt:
Lernziel:
Ausgeführt von: Christoph / Codex / gemeinsam
Ausgeführter Schritt:
Erwartung:
Beobachtung:
Ergebnis:
Learning / Entscheidung:
Git-Branch und Commit:
Nächster einzelner Schritt:
```
