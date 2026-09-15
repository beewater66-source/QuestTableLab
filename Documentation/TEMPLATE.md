# Quest MR Foundation – Template-Anleitung

Stand: 15. September 2026, 16:34 CEST  
Template-ID: `com.christophdorn.template.quest-mr-foundation`  
Version: `0.1.0`  
Unity: `6000.3.24f1`

## Zweck

Das Template ist der bereinigte, wiederverwendbare Stand von QuestTableLab. Es liefert eine funktionierende Mixed-Reality-Grundlage für die Meta Quest 3, ohne die Git-Historie, Unity-Caches, persönliche Unity-Cloud-Zuordnungen oder die Package-ID des Ausgangsprojekts zu übernehmen.

Enthalten sind unter anderem:

- Universal Render Pipeline, Android/ARM64 und IL2CPP,
- OpenXR und Meta-XR-/MRUK-Pakete,
- Passthrough und Scene-API-Berechtigung,
- Controller-Strahl, Greifen, Platzieren und Reset,
- semantische `TABLE`- und `WALL_FACE`-Platzierung,
- schaltbare Labeldiagnose und `SemanticLabelProfile`,
- die Beispielszene und 16 Play-Mode-Tests.

Nicht enthalten sind `.git`, `Library`, `Temp`, `Logs`, die Repository-Historie und die Unity-Cloud-Identität von QuestTableLab. Die internen C#-Namespaces und Assembly-Namen heißen weiterhin `QuestTableLab`; das ist technisch unproblematisch und kann bei Bedarf später separat umbenannt werden.

## Neues Projekt korrekt mit GitHub anlegen

Wichtig: Das gleichnamige Repository und den Projektordner **nicht vorher erstellen oder klonen**. Unity erhält als `Location` nur den Elternordner, beispielsweise `E:\GitHub`, und erzeugt darunter selbst den Ordner mit dem Projektnamen.

1. In Unity das Template **Quest MR Foundation** auswählen.
2. Einen neuen, eindeutigen Projektnamen eingeben.
3. Als Speicherort den Elternordner auswählen, zum Beispiel `E:\GitHub`.
4. GitHub bereits im Erstellungsdialog als Source-Control-Provider aktivieren und das verbundene Konto beziehungsweise einen passend beschränkten Access Token verwenden.
5. Unity Projektordner, lokales Git-Repository, privates GitHub-Repository, `main`, Initial Commit und ersten Push gemeinsam erstellen lassen.
6. Erst danach in GitHub Desktop **Add existing repository** wählen und den von Unity erzeugten Projektordner hinzufügen.

GitHub Desktop verbindet in diesem Ablauf kein nachträglich erzeugtes Unity-Projekt. Es dient anschließend als übersichtliche Oberfläche für Branches, Commits, Push/Pull und Pull Requests.

### Entsprechender CLI-Ablauf

```powershell
unity projects create MyQuestProject `
  --path E:\GitHub `
  --editor-version 6000.3.24f1 `
  --template com.christophdorn.template.quest-mr-foundation `
  --vcs github `
  --git-namespace beewater66-source `
  --git-repo MyQuestProject `
  --git-visibility private `
  --git-default-branch main `
  --no-cloud
```

Der Befehl verwendet den Windows Git Credential Manager. Alternativ kann ein Token sicher über `--git-token-stdin` übergeben werden; ein Token gehört niemals in ein Skript oder einen Commit. `--no-cloud` betrifft nur Unity Cloud und hat nichts mit der GitHub-Verknüpfung zu tun.

## Pflichtschritte nach der Erstellung

1. In den Player Settings den eigenen **Company Name** eintragen.
2. Die Android Package-ID `com.DefaultCompany.questmrfoundation` durch eine weltweit eindeutige ID ersetzen, beispielsweise `com.firma.projektname`. Der Platzhalter darf nicht veröffentlicht werden.
3. Android/Meta als aktives Build Profile prüfen.
4. Meta Project Setup kontrollieren und die Quest verbinden.
5. Die 16 Play-Mode-Tests ausführen.
6. Einen Android-Build erzeugen und die räumlichen Funktionen anschließend auf dem Headset testen.

Unitys benutzerdefinierte Template-Erstellung leitet die Android Package-ID hier nicht automatisch aus dem neuen Projektnamen ab. Deshalb verwendet das Template bewusst eine sichtbare, gültige Platzhalter-ID und dokumentiert deren Austausch als Pflichtschritt.

## Template neu exportieren

Das Skript `Tools/Build-QuestMRTemplate.ps1` kopiert ausschließlich `Assets`, `Packages` und `ProjectSettings` in ein temporäres Staging-Projekt, neutralisiert dort persönliche Projektangaben, prüft den Stand strikt und installiert anschließend das Template neu.

```powershell
.\Tools\Build-QuestMRTemplate.ps1
```

Der aktuelle Template-Archivpfad lautet:

```text
C:\Users\c_bra\Unity user templates\comchristophdorntemplatequest-mr-foundation\comchristophdorntemplatequest-mr-foundation.tgz
```

## Unabhängiger Nachweis

Das Projekt `E:\GitHub\QuestMRFoundationSmokeTest` wurde am 15. September 2026 direkt aus dem Template erstellt. Unity erzeugte dabei gleichzeitig das private GitHub-Repository, den lokalen Ordner, `main`, den Initial Commit `76a2969` und den ersten Push.

- strikte Projektprüfung: 0 Fehler, 0 Warnungen,
- Git: keine versionierten `Library`- oder `Temp`-Dateien,
- Play Mode: 16 von 16 Tests bestanden,
- Android-Build `build_2e4e76fbb33a`: erfolgreich, 0 Fehler, 4 bekannte Hinweise,
- APK: rund 65 MB,
- Build-Dauer: rund 539 Sekunden.

Dieser Template-Nachweis endet bewusst beim unabhängigen Android-Build. Die vollständige räumliche Funktion des Ausgangsstands wurde zuvor in QuestTableLab auf der physischen Quest 3 bestätigt; die APK des separaten Smoke-Test-Projekts wurde nicht zusätzlich auf dem Gerät installiert.
