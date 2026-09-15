# QuestTableLab

QuestTableLab ist mein Lern- und Prototypingprojekt für Mixed Reality auf der Meta Quest 3. Ich möchte damit nicht nur eine funktionierende Anwendung bauen, sondern den gesamten Weg bewusst nachvollziehbar machen: von meinen ersten aktiv genutzten Git- und GitHub-Grundlagen über räumliche Erkennung bis zu einer einfachen AR-gestützten Wartungs- und Reparaturbegleitung.

## Meine Ausgangslage

Ich habe bereits eigene Prototypen mit Unity umgesetzt. GitHub hatte ich zuvor jedoch hauptsächlich zum Herunterladen fremder Projekte verwendet, nicht als Versionsverwaltung für meine eigene tägliche Arbeit.

Mit diesem Projekt verbinde ich deshalb zwei Lernziele:

- Git und GitHub praktisch und strukturiert einsetzen.
- Eine Mixed-Reality-Anwendung für die Meta Quest 3 von Grund auf entwickeln und auf dem realen Gerät testen.

Das bewusste Arbeiten mit Repository, Branches, kleinen Commits, Dokumentation und überprüfbaren Zwischenständen ist damit ebenso Teil des Projekts wie die eigentliche XR-Anwendung.

## Projektziel

Mein erstes kleines Ziel ist eine Quest-Anwendung, in der ich durch Passthrough meine reale Umgebung sehe, eine Tischfläche erkenne und einen virtuellen Würfel darauf platzieren kann.

Darauf aufbauend möchte ich:

1. semantische Labels der erfassten Umgebung auslesen und sichtbar machen,
2. virtuelle Inhalte passenden realen Flächen oder Objekten zuweisen,
3. einen eigenen einfachen, nicht funktionalen Prototyp konstruieren und in 3D drucken,
4. dessen Bauteile innerhalb einer MR-Anwendung unterscheiden,
5. eine geführte Wartungs- oder Reparaturabfolge am Prototyp darstellen.

Die Quest-eigene semantische Raumerkennung und die Erkennung individueller Bauteile sind dabei nicht dasselbe. Für den späteren Prototyp werde ich deshalb untersuchen, ob räumliche Anker, Marker, Modellabgleich oder ein eigenes Erkennungsverfahren geeignet sind.

## Lern- und Entwicklungsphasen

| Phase | Inhalt | Geplanter Nachweis |
|---|---|---|
| 0 | Git, GitHub und Projektstruktur | Kleine Commits, Feature-Branch und nachvollziehbare Dokumentation |
| 1 | Quest-Grundaufbau | Anwendung lässt sich auf der Quest 3 installieren und starten |
| 2 | Würfel auf dem Tisch | Reale Tischfläche wird erkannt und ein Würfel darauf platziert |
| 3 | Semantische Labels | Erkannte Raumkategorien werden ausgelesen und visualisiert |
| 4 | Eigener 3D-Druck-Prototyp | Reale Bauteile werden virtuellen Informationen zugeordnet |
| 5 | Wartungsbegleitung | Eine kurze Wartungs- oder Reparatursequenz kann vollständig durchlaufen werden |

## Aktueller Stand

Stand: **15. September 2026**

Der technische Quest-Grundaufbau ist vorbereitet:

- Unity **6000.3.24f1 (Unity 6.3 LTS)** mit Universal Render Pipeline
- Android Build Support mit SDK, NDK und OpenJDK
- Meta Quest 3 im Developer Mode mit aktiviertem USB-Debugging
- Android als Build-Ziel, ARM64, mindestens Android API 32
- Unity OpenXR und Meta OpenXR
- Meta XR Core SDK, Interaction SDK und MR Utility Kit
- vorbereitete Funktionen für Passthrough, Ebenen, Raycasts, Anker und Meshing
- eigener Android-Paketname `com.christophdorn.questtablelab`

Die Konfiguration kompiliert fehlerfrei und die Quest 3 wird vom Entwicklungsrechner erkannt. Noch nicht praktisch nachgewiesen sind die Installation einer eigenen Build auf dem Gerät, sichtbares Passthrough und die Platzierung des Würfels. Das ist der nächste Meilenstein.

Die aktuelle Entwicklungsarbeit liegt auf dem Branch `feature/quest-bootstrap`. `main` bleibt vorerst der stabile Ausgangspunkt.

## Technische Basis

| Bereich | Verwendung |
|---|---|
| Engine | Unity 6000.3.24f1 |
| Rendering | Universal Render Pipeline (URP) |
| Zielgerät | Meta Quest 3 |
| Zielplattform | Android, ARM64 |
| XR-Laufzeit | OpenXR mit Meta-Quest-Unterstützung |
| Versionsverwaltung | Git und GitHub |

Der optionale Meta XR Simulator gehört derzeit nicht zu meinem erforderlichen Testweg. Ich teste auf der physisch angeschlossenen Quest 3.

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

Von Unity oder den XR-Paketen erzeugte Inhalte bleiben davon getrennt. Temporäre lokale Unity-Verzeichnisse wie `Library`, `Temp` und `Logs` werden nicht mit Git versioniert.

## Arbeitsweise

Ich entwickle das Projekt in kleinen, überprüfbaren Schritten. Jeder Meilenstein soll möglichst diese Abfolge durchlaufen:

1. ein klar begrenztes Lernziel festlegen,
2. die Änderung auf einem passenden Branch umsetzen,
3. Kompilierung und Verhalten prüfen,
4. Ergebnis und Probleme dokumentieren,
5. einen verständlich benannten Commit erstellen.

Ich führe die praktischen Einrichtungsschritte und Git-Aktionen selbst aus. Technische Recherche, wiederholbare Prüfungen und klar abgegrenzte Implementierungsarbeiten delegiere ich teilweise an Codex als Worker. Im Lernjournal halte ich fest, wer welchen Anteil ausgeführt hat. So bleibt sichtbar, was ich selbst gelernt und gemacht habe und wobei ich Werkzeugunterstützung eingesetzt habe.

## Dokumentation

- [Lernjournal und Gesamtfahrplan](Documentation/LERNJOURNAL.md) – Lernziele, Zeitverlauf, eigene Arbeit, delegierte Aufgaben und Learnings
- [Technische Entscheidungen](Documentation/ENTSCHEIDUNGEN.md) – gewählte Technik und Begründungen
- [Testprotokoll](Documentation/TESTPROTOKOLL.md) – praktische Tests mit Erwartung und beobachtetem Ergebnis

## Nächster Meilenstein

Als Nächstes erstelle ich eine minimale Mixed-Reality-Szene, baue sie für Android und starte sie auf meiner Quest 3. Der erste Gerätetest soll ausschließlich beweisen, dass der Weg von Unity über die Installation bis zum sichtbaren Passthrough funktioniert. Erst danach ergänze ich Tischflächenerkennung und Würfelplatzierung.
