# QuestTableLab

QuestTableLab ist meine Lern- und Testumgebung für Mixed Reality auf der Meta Quest 3. In diesem Projekt erarbeite ich mir einen nachvollziehbaren Grundaufbau für weitere AR-/MR-Projekte: Git und GitHub, Unity und OpenXR, Passthrough, räumliche Interaktion, semantische Raumlabels und später eine allgemein nutzbare Grundlage für Objektzuordnungen.

Das Projekt ist ausdrücklich **noch nicht** der spätere 3D-gedruckte Wartungsprototyp. Wenn diese Grundlage stabil ist, möchte ich daraus ein bereinigtes Template ableiten. Das Praktikumsprojekt mit eigenem physischen Prototyp, Teileerkennung sowie Wartungs- und Reparaturbegleitung entsteht anschließend als separates Projekt auf dieser Basis.

## Meine Ausgangslage

Ich habe bereits eigene Unity-Prototypen umgesetzt. GitHub hatte ich zuvor jedoch hauptsächlich zum Herunterladen fremder Projekte verwendet, nicht als Versionsverwaltung für meine tägliche Arbeit.

Mit QuestTableLab verbinde ich deshalb zwei Lernziele:

- Git und GitHub praktisch und strukturiert einsetzen.
- Eine wiederverwendbare Mixed-Reality-Grundlage für die Meta Quest 3 entwickeln und auf dem realen Gerät prüfen.

Repository, Branches, kleine Commits, Pull Requests, Dokumentation und überprüfbare Zwischenstände gehören damit genauso zum Projekt wie die XR-Anwendung selbst.

## Projektziel

In der ersten Ausbaustufe kann ich durch Passthrough meine reale Umgebung sehen, virtuelle Inhalte raumfest darstellen und einen Würfel mit dem rechten Controller bewegen, auf dem kalibrierten Fußboden platzieren und zurücksetzen.

Darauf aufbauend möchte ich:

1. semantische Labels der Quest-Raumdaten auslesen,
2. einen Würfel automatisch einer Fläche mit dem Label `TABLE` zuweisen,
3. ein Schild automatisch an einer `WALL_FACE` platzieren und seinen Abstand beziehungsweise Versatz konfigurierbar machen,
4. manuelle Steuerung und automatische semantische Platzierung miteinander verbinden,
5. eine konfigurierbare Zuordnung zwischen erkannten Kategorien und virtuellen Objekten schaffen,
6. die stabile, allgemeine Grundlage als Template für spätere AR-/MR-Projekte aufbereiten.

Die semantische Raumerkennung der Quest und die Erkennung individueller, selbst gebauter Bauteile sind unterschiedliche Aufgaben. Eine prototypspezifische Teileerkennung bleibt daher Bestandteil des späteren Praktikumsprojekts; QuestTableLab bereitet dafür eine erweiterbare technische Basis vor.

## Meilensteine

| Meilenstein | Inhalt | Status |
|---|---|---|
| 0 | Voraussetzungen: Unity, Android-Werkzeuge, Developer-Konto und Quest-Verbindung | Abgeschlossen |
| 1 | Repository, Unity-Projekt, Git-Arbeitsweise und Dokumentation | Abgeschlossen |
| 2 | Erste Quest-App: Build, Installation, Passthrough und sichtbarer virtueller Inhalt | Abgeschlossen |
| 3 | Manuelle Platzierung: Controller-Strahl, Verschieben, Fußbodenplatzierung und Reset | Abgeschlossen |
| 4 | Semantische Raumerkennung: `TABLE` und anschließend `WALL_FACE` über MRUK | In Arbeit |
| 5 | Konfigurierbare Zuordnung semantischer Kategorien und Abschluss der Template-Grundlage | Geplant |

## Aktueller Stand

Stand: **15. September 2026, 12:52 CEST**

Meilenstein 3 ist auf der Meta Quest 3 praktisch bestanden:

- Unity **6000.3.24f1** mit Universal Render Pipeline
- Android/ARM64, OpenXR und Meta-XR-Pakete
- Passthrough und raumfeste virtuelle Inhalte
- einzelner Strahl am rechten Controller
- visuelles Trigger-Feedback; Ausblenden bei aktiver Handsteuerung
- Verkürzung des Strahls an Würfel und kalibriertem Fußboden
- Würfel per Trigger aufnehmen, verschieben und loslassen
- grüne Platzierungsvorschau auf dem Fußboden
- Würfel per Trigger mit seiner Unterkante auf den Fußboden setzen
- Reset von Position und Rotation über die B-Taste
- Render Scale 1,0 und 4x MSAA für eine auf dem Gerät bestätigte scharfe Darstellung
- Meta XR Simulator sowie Meta XR Operator mit OpenXR API Layer für ergänzende Entwicklungs- und Agententests
- aktivierte Scene-Unterstützung mit automatischer Berechtigungsanfrage beim Start
- aktiviertes Application SpaceWarp als optionale Meta-Performancefunktion
- `OVROverlayCanvas` für das raumfeste Schild mit getrennten Render-Layern
- zehn bestandene Play-Mode-Tests
- erfolgreiche Android-Build und Installation der ersten MRUK-/Scene-API-Fassung
- automatische Suche nach dem nächstgelegenen geeigneten `TABLE`-Volumen
- verständliche Statusmeldungen für fehlende Berechtigung, fehlendes Space Setup und fehlendes Tisch-Label
- automatische Platzierung des Würfels auf der Tischoberkante; B setzt anschließend auf diese Position zurück
- automatische Auswahl einer zum Benutzer gerichteten `WALL_FACE` für das Schild; horizontaler und vertikaler Versatz sowie Wandabstand sind konfigurierbar
- Controller-Replatzierung von Würfel und Schild mit Begrenzung auf die jeweils erkannte Tisch- beziehungsweise Wandfläche

Meilenstein 3 wurde über Pull Request #3 in `main` übernommen. Die aktuelle Arbeit liegt auf `feature/semantic-table-placement`. Die semantische Tischplatzierung wurde auf zwei realen, im Space Setup erfassten Tischen erfolgreich bestätigt. Nach Korrektur der Canvas-Ausrichtung wurde auch die automatische Wandplatzierung des Schilds auf der Quest erfolgreich geprüft.

## Technische Basis

| Bereich | Verwendung |
|---|---|
| Engine | Unity 6000.3.24f1 |
| Template | Universal 3D (URP) |
| Zielgerät | Meta Quest 3 |
| Zielplattform | Android, ARM64 |
| XR-Laufzeit | OpenXR mit Meta-Quest-Unterstützung |
| Raumverständnis | Meta MR Utility Kit 205.0.0 und Quest Scene API |
| Versionsverwaltung | Git und GitHub |

Der Meta XR Simulator und Meta XR Operator sind als zusätzliche Entwicklungswerkzeuge eingerichtet. Der verbindliche Nachweis für räumliche Interaktion, Darstellung und Tracking bleibt trotzdem der praktische Test auf der physischen Quest 3.

## Projektstruktur

Meine eigenen Inhalte liegen gebündelt unter `Assets/App`:

```text
Assets/App/
├── Art/
├── Prefabs/
├── Scenes/
├── Scripts/
└── UI/
```

Von Unity oder XR-Paketen erzeugte Inhalte bleiben davon getrennt. Temporäre lokale Verzeichnisse wie `Library`, `Temp` und `Logs` werden nicht mit Git versioniert.

## Arbeitsweise

Jeder Meilenstein durchläuft möglichst denselben Weg:

1. ein klar begrenztes Lernziel festlegen,
2. auf einem passenden Feature-Branch umsetzen,
3. schnelle Play-Mode-Tests ausführen,
4. eine Development-Build auf der Quest praktisch prüfen,
5. Ergebnis, Fehler und Entscheidungen dokumentieren,
6. einen verständlich benannten Commit erstellen,
7. den Stand per Pull Request prüfen und in `main` übernehmen.

Ich führe die Geräteprüfung, die praktische Bewertung und die Git-Aktionen selbst aus. Technische Implementierung, wiederholbare Prüfungen, Builds und Fehleranalyse delegiere ich teilweise an Codex. Im Lernjournal ist festgehalten, wer welchen Anteil ausgeführt hat.

## Dokumentation

- [Lernjournal und Verlauf](Documentation/LERNJOURNAL.md)
- [Technische Entscheidungen](Documentation/ENTSCHEIDUNGEN.md)
- [Testprotokoll](Documentation/TESTPROTOKOLL.md)

## Nächster Schritt

Die flächengebundene Replatzierung wurde auf der Quest 3 praktisch bestätigt. Als Nächstes folgt eine verständliche Controllerbedienung, mit der Würfel und Schild zwischen mehreren erkannten `TABLE`- beziehungsweise `WALL_FACE`-Ankern wechseln können.
