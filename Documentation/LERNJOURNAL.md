# Lernjournal: QuestTableLab als AR-/MR-Grundlage

Stand: 2026-09-15 16:34 CEST
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

MRUK stellt Raumdaten bereit. Die Anwendung erkennt `TABLE`, unterscheidet geometrische Treffer von semantischer Bedeutung und platziert den Würfel relativ zur Tischfläche. Das Schild wird automatisch an einer geeigneten `WALL_FACE` platziert. Beide Objekte lassen sich innerhalb ihrer semantischen Fläche bewegen und beim Ziehen direkt einem anderen passenden Anker zuweisen. Die vollständige Interaktion wurde auf der Quest 3 bestätigt. **Abgeschlossen.**

### Meilenstein 5 – Konfigurierbare Zuordnung

Ein zentrales `SemanticLabelProfile` verbindet MRUK-Kategorien mit Anzeigenamen und Farben. Optionale Felder für Icons und Content-Prefabs bereiten spätere Erweiterungen vor. Diagnoseansicht und zugeordneter Würfel lesen dieselbe `TABLE`-Konfiguration. Die Funktion wurde mit 16 Play-Mode-Tests, Android-Build und Gerätetest bestätigt. **Abgeschlossen.**

### Meilenstein 6 – Wiederverwendbares Template

Der stabile Projektstand wurde als **Quest MR Foundation** aufbereitet. Ein unabhängiges Projekt wurde direkt aus dem Template und gleichzeitig mit lokalem Git- sowie privatem GitHub-Repository erzeugt. 16 Play-Mode-Tests und ein Android-Build bestanden. **Technisch abgeschlossen; Pull Request, Merge und Release-Tag stehen noch aus.**

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
| 2026-09-15 ca. 14:10–14:20 | Codex, von Christoph delegiert | Ersten Ankerwechsel umgesetzt und im Editor geprüft | Zunächst wechselten A und rechter Stick zyklisch durch gültige `TABLE`- beziehungsweise `WALL_FACE`-Anker. 13 von 13 Play-Mode-Tests, Android-Build und Installation bestanden. Der anschließende Gerätetest zeigte, dass blindes Durchschalten räumlich unverständlich ist. |
| 2026-09-15 ca. 14:22–14:39 | Gemeinsam | Ankerwechsel als räumliches Drag-and-Drop neu gefasst | Christoph präzisierte die gewünschte Interaktion: Das gegriffene Objekt soll beim Zeigen auf eine andere reale Fläche deren semantischen Anker übernehmen. Codex ersetzte die Tastenbelegung durch direkte MRUK-Raum-Raycasts auf Tischoberseiten und Wandflächen; 13 von 13 Play-Mode-Tests, Android-Build und Installation bestanden. |
| 2026-09-15 ca. 14:40 | Christoph | Direkten semantischen Flächenwechsel bestätigt | Würfel und Schild übernahmen beim Ziehen auf eine andere passende reale Fläche automatisch den tatsächlich anvisierten `TABLE`- beziehungsweise `WALL_FACE`-Anker. Smoke-Test bestanden. |
| 2026-09-15 ca. 14:45–15:00 | Christoph | Meilenstein 4 per Pull Request #4 abgeschlossen | Semantische Platzierung in `main` übernommen; beim anschließenden Branchwechsel wurde ein veraltetes lokales `main` erkannt, aktualisiert und der neue Branch kontrolliert mit dem Merge-Stand verbunden. |
| 2026-09-15 15:00–15:21 | Gemeinsam | Semantische Diagnoseansicht umgesetzt und auf der Quest geprüft | Codex implementierte farbige Grenzen und raumorientierte Labeltexte für MRUK-Anker sowie den Toggle über den rechten Stick-Klick. 15 von 15 Play-Mode-Tests und Android-Build `build_41377d06778d` bestanden; Christoph bestätigte Darstellung und Umschaltung im Headset. |
| 2026-09-15 15:21–15:47 | Gemeinsam | Konfigurierbares semantisches Labelprofil umgesetzt und geprüft | Codex implementierte das ScriptableObject mit Anzeigename, Farbe sowie optionalem Icon und Content-Prefab und band Diagnoseansicht und Würfel daran. 16 von 16 Play-Mode-Tests sowie Android-Build `build_f3babb65535c` mit 0 Fehlern bestanden; Christoph bestätigte die vollständige Funktion auf der Quest 3. |
| 2026-09-15 ca. 15:48–16:05 | Codex, von Christoph delegiert | Template erstmals erzeugt und technisch untersucht | Der direkte Export enthielt versehentlich Unity-Caches und war rund 202 MB groß. Ein sauberer Export aus ausschließlich versionierbaren Projektbestandteilen reduzierte das Archiv auf rund 3,16 MB. Platzhalterdateien sichern leere Strukturordner in Git und im Template. |
| 2026-09-15 ca. 16:05–16:18 | Codex, von Christoph delegiert | Template-Identität neutralisiert und ersten unabhängigen Versuch ausgewertet | Der erste Test zeigte, dass Unity persönliche Cloud-Zuordnung und die explizite Android Package-ID sonst übernimmt. Diese Angaben werden nun nur im temporären Exportstand neutralisiert; das Ausgangsprojekt behält seine korrekte Identität. |
| 2026-09-15 ca. 16:18–16:34 | Codex, von Christoph delegiert | GitHub-Erstellung und Template-Abnahme unabhängig nachgewiesen | Unity erstellte `QuestMRFoundationSmokeTest`, lokales Git und das private GitHub-Repository gemeinsam. Initial Commit `76a2969` wurde gepusht. 16 von 16 Play-Mode-Tests sowie Android-Build `build_2e4e76fbb33a` mit 0 Fehlern bestanden. |

## Zentrale Learnings

### Git und GitHub

- Ein Repository ist der versionierte Projektbereich; ein Commit ist ein benannter Zwischenstand darin.
- Ein lokaler Commit ist kein vollständiges Rechner-Backup. Erst Push beziehungsweise Publish legt den versionierten Stand zusätzlich auf GitHub ab.
- Ein Branch ist eine getrennte Entwicklungslinie. Ein Pull Request vergleicht diese Linie mit `main`, ermöglicht eine Prüfung und führt die Änderungen anschließend kontrolliert in den Stamm zusammen.
- Nach einem Merge kann der abgeschlossene Feature-Branch gelöscht werden; der Inhalt bleibt über `main` und die Historie erhalten.
- Für ein neues Unity-Projekt darf nicht gleichzeitig ein gleichnamiger leerer Clone vorbereitet werden, wenn Unity selbst über den GitHub-Provider das Repository erstellen soll. Der praktisch bestätigte Ablauf lautet: Repository und Projekt gemeinsam in Unity erzeugen, danach den vorhandenen lokalen Ordner in GitHub Desktop als bestehendes Repository hinzufügen.
- GitHub und Unity Cloud sind getrennte Dienste. Ein Projekt kann direkt mit GitHub angelegt werden, während Unity Cloud bewusst deaktiviert bleibt.
- Leere Ordner werden von Git nicht versioniert. Kleine `.gitkeep`-Dateien erhalten die vorbereitete `Art`-, `Prefabs`-, `UI`- und `StreamingAssets`-Struktur im Template.
- Ein Template muss in einem vollständig neuen Projekt geprüft werden. Erst dabei wurden übernommene Cloud- und Package-Identitäten sowie der zu große Cache-Export zuverlässig sichtbar.
- Das Custom Template setzt die Android Package-ID nicht automatisch passend zum neuen Projektnamen. Deshalb enthält es einen gut sichtbaren gültigen Platzhalter, der nach jeder Projekterstellung verpflichtend ersetzt wird.

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
- Zyklisches Umschalten ist bei räumlich verteilten Flächen zwar technisch einfach, aber ohne sichtbare Zuordnung unverständlich. Ein direkter Scene-API-Raycast verbindet die Controllerhandlung stattdessen mit der tatsächlich anvisierten realen Fläche.
- Eine semantische Diagnoseebene sollte von der eigentlichen Objektlogik getrennt bleiben. Dadurch können erkannte Kategorien und Grenzen sichtbar gemacht werden, ohne Platzierung oder Interaktion zu verändern.
- Ein ScriptableObject eignet sich als gemeinsame semantische Quelle für Diagnoseansicht und virtuelle Inhalte. Anzeigenamen, Farben und spätere Icons oder Prefabs lassen sich dadurch erweitern, ohne die Erkennungslogik umzubauen.

## Aktueller Stand

Erreicht:

- Meilensteine 0 bis 5 vollständig umgesetzt, praktisch nachgewiesen und in `main` übernommen.
- Meilenstein 6 technisch abgeschlossen: Template `Quest MR Foundation` 0.1.0 erstellt und in einem unabhängigen GitHub-Projekt geprüft.
- Schaltbare Visualisierung aller erkannten semantischen Raumanker und zentrale, erweiterbare Labelkonfiguration umgesetzt.
- 16 Play-Mode-Tests bestanden.
- Android-Build `build_f3babb65535c` mit 0 Fehlern erstellt, auf der Quest installiert und praktisch bestätigt.
- Unabhängiger Template-Build `build_2e4e76fbb33a` mit 0 Fehlern erstellt; die separate Test-APK wurde nicht erneut auf der Quest installiert.
- Controller-Strahl, Bewegung, Bodenplatzierung und Reset funktionieren.
- README, Lernjournal, Entscheidungen und Testprotokoll auf den allgemeinen Template-/Lernzweck ausgerichtet.

Noch offen:

- Template-Branch committen, per Pull Request in `main` übernehmen und den Abschlussstand mit `v0.1.0` markieren.

## Nächster einzelner Lernschritt

Den vorbereiteten Template-Stand committen und den Pull Request für Meilenstein 6 erstellen.

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
