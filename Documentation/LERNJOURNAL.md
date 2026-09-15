# Lernjournal: Von Git-Grundlagen zum AR-Wartungsprototyp

Stand: 2026-09-15 08:48 CEST  
Projekt: QuestTableLab  
Zielgerät: Meta Quest 3

## Übergeordnetes Lernziel

Dieses Projekt dient nicht nur dazu, eine einzelne Unity-Anwendung zu bauen. Es soll einen nachvollziehbaren Lernweg abbilden:

1. Git und GitHub im praktischen Projektalltag verstehen.
2. Eine erste Mixed-Reality-Anwendung auf der Meta Quest 3 ausführen.
3. Einen virtuellen Würfel auf einer real erkannten Tischfläche platzieren.
4. Semantische Raumlabels der Quest auslesen und sichtbar machen.
5. Virtuelle Inhalte erkannten realen Flächen oder Objekten zuweisen.
6. Das Gelernte auf einen einfachen, nicht funktionalen 3D-Druck-Prototyp übertragen.
7. Bauteile des Prototyps erkennen und eine geführte Wartungs- oder Reparaturabfolge darstellen.

Der Lernerfolg wird deshalb nicht nur an funktionierendem Code gemessen. Ebenso wichtig sind verständliche Git-Schritte, dokumentierte Entscheidungen, reproduzierbare Tests und die Fähigkeit, das Vorgehen auf ein zweites Objekt zu übertragen. Zusätzlich wird festgehalten, welche Arbeit selbst ausgeführt und welche Arbeit an Codex als technischen Worker delegiert wurde.

## Rollen und Arbeitsweise

### Selbst ausgeführt – Christoph Dorn

- Unity Editor 6000.3.24f1 installiert.
- Android Build Support einschließlich SDK, NDK und OpenJDK installiert.
- Unity-ID beziehungsweise Organisation eingerichtet.
- Meta-Developer-Account eingerichtet und Meta Quest Developer Hub 6.5 installiert.
- Developer Mode und USB-Debugging auf der Quest 3 aktiviert.
- Quest 3 per USB verbunden und die Verbindung im Developer Hub hergestellt.
- GitHub-Repository `QuestTableLab` erstellt und lokal unter `E:\GitHub` bereitgestellt.
- Unity-Projekt über die grafische Oberfläche angelegt.
- Den versehentlich verschachtelten Projektordner erkannt.
- Den vollständigen Projektinhalt nach Abschluss des Kopiervorgangs in die richtige Repository-Wurzel verschoben.
- Projekt anschließend im Unity Hub neu verknüpft.
- Build Profile auf Meta/Android umgestellt und notwendige XR-Einrichtung in Unity angestoßen.
- Commits in GitHub Desktop erstellt.
- Feature-Branch erstellt und mit `Publish branch` auf GitHub veröffentlicht.
- Fehlermeldungen und unerwartete Zustände mit Screenshots gemeldet und vor dem Fortfahren geprüft.
- Die installierte Anwendung im Headset praktisch geprüft und Passthrough sowie die sichtbare Meldung bestätigt.

### An Codex/Worker delegiert

- Einen strukturierten Lern- und Projektfahrplan entwickeln.
- Unity-Version, Template und geeigneten Meta-XR-/OpenXR-Technikweg bewerten.
- Git-Begriffe und die jeweils nächste Aktion erklären.
- Die Projektordnerstruktur unter `Assets/App` anlegen.
- Die konkrete XR-Paket- und Projekteinstellung prüfen und fertigstellen.
- Meta XR Core SDK, Interaction SDK, MR Utility Kit, Unity OpenXR und Meta OpenXR installieren beziehungsweise verifizieren.
- Android-, OpenXR-, Passthrough-, Plane-, Raycast-, Anchor- und Meshing-Einstellungen prüfen.
- Android-Paketname, Produktname und Herstellerangabe setzen.
- Generiertes Android-Manifest und Meta-Projekteinstellungen anwenden beziehungsweise prüfen.
- Unity-Kompilierung, Konsolenzustand, Android-Zielplattform, SDK-Stufen, ARM64 und Geräteverbindung technisch kontrollieren.
- Fehlgeschlagene Background Tasks untersuchen und als optionalen Meta-XR-Simulator einordnen.
- Lernjournal, Gesamtfahrplan und Verlinkung im README erstellen und fortlaufend pflegen.
- Eine minimale Passthrough-Szene mit XR-Kamera und schwebender Testmeldung erstellen.
- Die erste Android-Build erzeugen, auf der Quest installieren, starten und technisch über OpenXR- und Android-Laufzeitdaten prüfen.

### Gemeinsam entschieden oder überprüft

- Das erste Ziel bleibt bewusst klein: Passthrough, Tischfläche und ein platzierbarer Würfel.
- Danach folgen semantische Labels und erst anschließend der eigene 3D-Druck-Prototyp.
- `main` bleibt zunächst stabil; der Quest-Aufbau erfolgt auf `feature/quest-bootstrap`.
- Der optionale Meta-XR-Simulator wird vorerst nicht weiterverfolgt, weil direkt auf der physischen Quest 3 getestet werden kann.
- Application SpaceWarp und Meta-Platform-Dienste sind für den ersten Prototyp nicht erforderlich.
- Die Arbeit wird künftig in einzelnen, überprüfbaren Schritten fortgesetzt.
- Für den Bootstrap ist eine an der Kamera befestigte Meldung sinnvoll, weil sie beim ersten Start sicher sichtbar ist. Raumfeste Inhalte werden erst im nächsten Meilenstein umgesetzt.

## Lernphasen und Erfolgskriterien

### Phase 0 – Arbeitsweise und Versionsverwaltung

Ziel: Änderungen kontrolliert durchführen und bei Bedarf nachvollziehen können.

Erfolgskriterien:

- Repository lokal und auf GitHub vorhanden.
- Unterschied zwischen Repository, Commit, Branch, Publish und Push verstanden.
- Änderungen werden in kleinen, sinnvoll benannten Commits gespeichert.
- `main` bleibt stabil; experimentelle Arbeit findet zunächst in einem Branch statt.
- Generierte lokale Unity-Ordner wie `Library`, `Temp` und `Logs` werden nicht versioniert.

### Phase 1 – Technischer Quest-Grundaufbau

Ziel: Eine leere Anwendung zuverlässig auf der Quest 3 starten können.

Erfolgskriterien:

- Quest 3 wird vom Entwicklungsrechner erkannt.
- Android-, OpenXR- und Meta-XR-Konfiguration kompiliert fehlerfrei.
- Eine Entwicklungs-Build lässt sich installieren und starten.
- Passthrough zeigt die reale Umgebung.

### Phase 2 – Würfel auf dem Tisch

Ziel: Den vollständigen kleinen AR-/MR-Arbeitsablauf verstehen.

Erfolgskriterien:

- Eine reale Tischfläche wird erkannt.
- Ein Platzierungshinweis reagiert auf einen gültigen Treffer.
- Ein Würfel kann bewusst auf dem Tisch platziert werden.
- Position und Ausrichtung bleiben nachvollziehbar stabil.
- Der Versuch wird auf dem Gerät getestet und im Testprotokoll festgehalten.

### Phase 3 – Semantische Labels und Objektzuweisung

Ziel: Verstehen, welche Bedeutung die Quest ihrer erfassten Umgebung zuordnet.

Erfolgskriterien:

- Verfügbare Raumflächen und ihre semantischen Labels werden ausgelesen.
- Labels wie Tisch, Wand, Boden oder Decke werden sichtbar dargestellt oder protokolliert.
- Die Anwendung unterscheidet zwischen geometrischem Treffer und semantischer Bedeutung.
- Virtuelle Inhalte werden nur passenden Flächen zugewiesen.
- Grenzen und Fehlklassifikationen werden dokumentiert.

### Phase 4 – Eigener 3D-Druck-Prototyp

Ziel: Den Tischversuch auf ein kontrolliertes reales Demonstrationsobjekt übertragen.

Erfolgskriterien:

- Ein einfacher, nicht funktionaler Prototyp mit klar unterscheidbaren Bauteilen ist konstruiert und gedruckt.
- Bauteile besitzen stabile Namen, IDs und definierte Wartungsschritte.
- Die Anwendung kann ein Bauteil auswählen oder einer erkannten Position zuordnen.
- Hinweise markieren das richtige Teil und erklären den nächsten Arbeitsschritt.
- Eine kurze Wartungs- oder Reparatursequenz kann vollständig durchlaufen werden.

Hinweis: Die semantischen Raumlabels der Quest erkennen zunächst Kategorien der Umgebung. Eine zuverlässige Erkennung eigener Bauteile ist eine zusätzliche technische Aufgabe und wird nicht automatisch allein durch Scene Understanding gelöst. Dafür werden später gezielt Marker, räumliche Anker, Modellabgleich oder ein eigenes Erkennungsverfahren bewertet.

## Zeitlicher Verlauf

Alle Zeiten sind lokale Zeit in Deutschland (CEST). Git-bestätigte Zeitpunkte stammen aus der Commit-Historie; Gesprächs- und Lernzeitpunkte werden beim Dokumentieren ergänzt.

| Zeitpunkt | Ausgeführt von | Ereignis | Ergebnis / Lernfortschritt | Nachweis |
|---|---|---|---|---|
| 2026-09-14, vor Projektbeginn | Christoph | Entwicklungsumgebung und Quest vorbereitet | Unity mit Android-Werkzeugen, Developer Hub, Developer Mode und USB-Debugging einsatzbereit. | Manuell geprüfter Ausgangsstand |
| 2026-09-14 09:19 | Christoph | Repository initialisiert | Erster Ausgangspunkt auf `main` vorhanden. | Commit `2cf6743` |
| 2026-09-14 11:05 | Christoph | Unity-Projekt erstellt | Unity 6.3 URP als technische Projektbasis angelegt. | Commit `30898c4` |
| 2026-09-14, vormittags | Christoph, mit Anleitung durch Codex | Verschachtelten Projektordner korrigiert | Projektinhalt aus `QuestTableLab/QuestTableLab` an die richtige Repository-Wurzel verschoben und im Hub neu verknüpft. | Arbeitsablauf |
| 2026-09-14 11:41 | Codex, von Christoph delegiert | Projektstruktur angelegt | Eigene Bereiche für Szenen, Skripte, Prefabs, Art und UI geschaffen; anschließend von Christoph committed. | Commit `0e289dc` |
| 2026-09-14, vormittags | Christoph, mit Anleitung durch Codex | Ersten praktischen Branch erstellt | `feature/quest-bootstrap` trennt den Quest-Aufbau vom stabilen Stand auf `main`. | Git-Branch |
| 2026-09-14, mittags | Codex, von Christoph delegiert | Quest-/OpenXR-Einrichtung fertiggestellt und geprüft | Pakete, Manifest, Projekteinstellungen und Geräteverbindung technisch validiert. | Unity-/Android-Prüfung |
| 2026-09-14 12:59 | Christoph | Quest-/OpenXR-Grundkonfiguration committed | Android-, OpenXR- und Meta-XR-Basis auf dem Feature-Branch gespeichert. | Commit `9bb4aa1` |
| 2026-09-14, ca. 13:00 | Christoph | Branch veröffentlicht | Lokaler Feature-Branch wurde mit `Publish branch` auf GitHub verfügbar gemacht. | `origin/feature/quest-bootstrap` |
| 2026-09-14 ab 13:03 | Codex, von Christoph delegiert | Lernjournal begonnen und erweitert | Technische Arbeit, Lernziel und Arbeitsaufteilung werden gemeinsam nachvollziehbar. | Diese Datei |
| 2026-09-15 08:24–08:38 | Codex, von Christoph delegiert | Erste Passthrough-Anwendung gebaut und ausgeliefert | XR-Kamera, Passthrough und Testmeldung eingerichtet; Android-Build mit 0 Fehlern erzeugt, installiert und gestartet. | Test 001 |
| 2026-09-15 08:48 | Christoph | Ersten Mixed-Reality-Gerätetest bestätigt | Reale Umgebung und `HELLO QUESTTABLELAB` im Headset sichtbar; Meldung folgt erwartungsgemäß der Kamera. | Test 001 bestanden |
| 2026-09-15, vormittags | Christoph | Pull Request #1 gemergt und Bootstrap-Branch auf GitHub gelöscht | Der geprüfte Quest-Bootstrap wurde in `main` übernommen; die abgeschlossene Arbeitslinie wurde aufgeräumt. | Merge-Commit `d93dd39` |
| 2026-09-15 09:13–09:27 | Codex, von Christoph delegiert | Raumfesten Testwürfel und raumfeste Beschriftung umgesetzt, gebaut und ausgeliefert | 20-cm-Würfel sowie World-Space-Canvas außerhalb der XR-Kamera angelegt; drei inkrementelle Android-Builds erstellt und installiert. | Test 002 |
| 2026-09-15 09:28 | Christoph | Raumfeste Darstellung im Headset bestätigt | Würfel bleibt auch bei schnellen Kopfbewegungen stabil; die korrigierte Beschriftung steht gut lesbar über ihm. | Test 002 bestanden |
| 2026-09-15, vormittags | Christoph | Beschriftung im Unity Editor manuell feinjustiert | Schriftgröße, Panelgröße und räumliche Position wurden anhand des Geräteeindrucks verbessert. | Szenenänderung auf `feature/table-cube-placement` |
| 2026-09-15 10:10–10:16 | Codex, von Christoph delegiert | Erste automatisierte Play-Mode-Tests eingerichtet | Tests prüfen Raumwurzel, Pose des Würfels, World-Space-Canvas und visuelle Nähe der Beschriftung; 2 von 2 Tests bestanden. | Test 003 |

## Bisherige Learnings

### Git und GitHub

- Ein Repository ist der gesamte versionierte Projektbereich; ein Commit ist ein benannter Zwischenstand darin.
- Ein Commit auf `main` ist kein vollständiges Vollbackup des Rechners. Er sichert nur versionierte Dateien. Erst durch Push oder Publish liegt der Stand zusätzlich auf GitHub.
- Ein Branch ist eine getrennte Entwicklungslinie. Dadurch kann der stabile Stand auf `main` erhalten bleiben, während ein neuer Aufbau ausprobiert wird.
- `Publish branch` erscheint, wenn ein Branch bisher nur lokal existiert. Nach der Veröffentlichung werden spätere Änderungen mit `Push origin` hochgeladen.
- Gute Commit-Namen beschreiben die Absicht einer zusammengehörigen Änderung, beispielsweise `chore: configure Meta Quest OpenXR`.

### Projektanlage

- Beim verwendeten Unity-/GitHub-Workflow darf Projekt- und Repository-Erstellung nicht unkontrolliert doppelt erfolgen. Sonst kann Unity einen zweiten gleichnamigen Ordner innerhalb des bereits vorhandenen Ordners erzeugen.
- Vor dem Erstellen muss geprüft werden, ob der angezeigte Zielpfad die Repository-Wurzel oder deren übergeordneten Ordner meint.
- Nach Dateioperationen muss erst deren Abschluss abgewartet werden, bevor aus einem vermeintlich unvollständigen Zustand weitere Schlüsse gezogen werden.
- Viele geänderte Dateien nach der XR-Einrichtung sind normal. Entscheidend ist, dass temporäre Unity-Verzeichnisse durch `.gitignore` ausgeschlossen bleiben.

### Lern- und Arbeitsmethode

- Beim erstmaligen Einrichten wird jeweils nur ein Schritt ausgeführt und überprüft, bevor der nächste folgt.
- Fehlermeldungen werden nach ihrer tatsächlichen Auswirkung bewertet. Der fehlgeschlagene optionale Meta-XR-Simulator blockiert Tests auf der physisch angeschlossenen Quest 3 nicht.
- Eine erfolgreiche Paketinstallation ist noch kein erfolgreicher Prototyp. Der nächste relevante Beweis ist eine installierte und gestartete Build auf dem Zielgerät.
- Entscheidungen und Irrwege werden dokumentiert, weil sie Teil des Lernergebnisses sind und spätere Projekte beschleunigen.
- Der erste IL2CPP-/ARM64-Build kann wegen der einmaligen nativen Übersetzung aller XR-Abhängigkeiten deutlich länger dauern als spätere Builds.
- Ein kameragebundenes UI bleibt stets im Sichtfeld und bewegt sich mit dem Kopf. Für einen raumfesten Würfel wird stattdessen eine Position im erfassten Raum beziehungsweise ein Anker benötigt.
- Ein Objekt auf der Szenenwurzel bewegt sich nicht mit der XR-Kamera. Damit lässt sich Raumfestigkeit zunächst unabhängig von semantischer Tischerkennung prüfen.
- Bei einem World-Space-Canvas auf der Szenenwurzel muss die `Anchored Position` des `RectTransform` korrekt gesetzt werden. Nur die allgemeine Transform-Position zu ändern kann dazu führen, dass Unity X/Y wieder aus den Canvas-Ankern berechnet.
- Der praktische Sichttest ist unverzichtbar: Technisch plausible Koordinaten reichen nicht aus, um Größe, Lesbarkeit und wahrgenommene Position im Headset zu beurteilen.
- Play-Mode-Tests im Editor ersetzen keinen Quest-Gerätetest, sichern aber Objektstruktur und Laufzeiteigenschaften in wenigen Sekunden ab.
- Bei vollständig deaktiviertem Domain- und Scene-Reload startet der Test Runner trotzdem in einer eigenen Testszene. Benötigte Projektszenen müssen daher im Test ausdrücklich geladen werden.
- Ein modaler Speichern-Dialog kann einen automatisierten Testlauf blockieren. Vor Play-Mode-Tests muss die offene Projektszene gespeichert sein.

## Aktueller Stand

Erreicht:

- GitHub-Repository und lokaler Clone vorhanden.
- Erste Commits auf `main` vorhanden.
- Feature-Branch `feature/quest-bootstrap` erstellt und veröffentlicht.
- Unity-Projekt strukturiert.
- Quest 3 als Entwicklungsgerät verbunden.
- Quest-/OpenXR-Grundkonfiguration fehlerfrei kompiliert.
- Erste Development-Build mit 0 Fehlern erzeugt und auf der Quest 3 installiert.
- Eigene Anwendung auf der Quest 3 gestartet.
- Passthrough und virtuelle Testmeldung im Headset sichtbar.
- Quest-Bootstrap praktisch nachgewiesen.
- Erster virtueller Würfel bleibt bei Kopfbewegungen stabil im Raum.
- Raumfeste Beschriftung steht lesbar über dem Würfel.
- Zwei automatisierte Play-Mode-Tests bestehen im Unity Editor.
- Fast Enter Play Mode ist ohne Domain- und Scene-Reload aktiviert.

Noch nicht praktisch nachgewiesen:

- Tisch erkannt und Würfel platziert.
- Semantische Labels ausgelesen.

## Nächster einzelner Lernschritt

Den bestandenen Teststand auf `feature/table-cube-placement` committen. Danach wird die feste Testposition durch eine Platzierung auf einer semantisch erkannten Tischfläche ersetzt.

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
