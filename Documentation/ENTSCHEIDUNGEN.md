# Technische Entscheidungen

## Projektbasis

- Unity: 6000.3.24f1
- Template: Universal 3D (URP)
- Zielgerät: Meta Quest 3
- Versionsverwaltung: Git mit GitHub

Weitere Entscheidungen werden mit Datum, Begründung und möglichen Alternativen ergänzt.

## 2026-09-15 – Definition des Quest-Bootstraps

Der Quest-Bootstrap gilt erst dann als abgeschlossen, wenn eine eigene Android-Build auf der physischen Quest 3 installiert wurde, dort erfolgreich startet und reale sowie virtuelle Inhalte sichtbar kombiniert.

Eine reine Paketinstallation oder fehlerfreie Unity-Kompilierung reicht dafür nicht aus. Als minimaler End-to-End-Nachweis dient eine Passthrough-Szene mit der Meldung `HELLO QUESTTABLELAB`.

## 2026-09-15 – Kameragebundene Testmeldung

Die Meldung des ersten Smoke-Tests wird absichtlich an der XR-Kamera befestigt. Dadurch bleibt sie unabhängig von der Ausgangsposition des Benutzers sicher sichtbar und bewegt sich mit dem Headset.

Diese Lösung ist nur für den Bootstrap-Test vorgesehen. Der Würfel des folgenden Meilensteins wird über einen räumlichen Treffer beziehungsweise Anker in der realen Umgebung positioniert und soll deshalb beim Bewegen des Kopfes an seinem Platz bleiben.

## 2026-09-15 – Raumfestigkeit vor semantischer Platzierung getrennt prüfen

Die erste Würfelstufe verwendet bewusst eine feste Weltposition auf der Szenenwurzel. Dadurch wird zunächst isoliert nachgewiesen, dass ein virtuelles Objekt nicht an der XR-Kamera hängt und bei Kopfbewegungen stabil im Raum erscheint.

Die Beschriftung wird ebenfalls als World-Space-Canvas auf der Szenenwurzel platziert. Sie ist damit dem Würfel visuell zugeordnet, ohne dauerhaft im Sichtfeld zu stören.

Diese feste Position war nur ein Zwischenschritt. Danach wurde zunächst die manuelle Controller- und Fußbodenplatzierung als eigener Meilenstein umgesetzt. Erst der folgende Meilenstein verwendet eine semantisch als Tisch klassifizierte Fläche aus den Quest-Raumdaten.

## 2026-09-15 – Fast Enter Play Mode als Projektstandard

Für schnelle lokale Iterationen werden Domain Reload und Scene Reload beim Eintritt in den Play Mode deaktiviert. Play-Mode-Tests laden ihre benötigte Projektszene deshalb ausdrücklich und dürfen nicht von einem impliziten Szenenreset abhängen.

Dieser Geschwindigkeitsvorteil bringt Verantwortung mit sich: Statische Felder, abonnierte Events und veränderte Laufzeitobjekte werden bei zukünftigen Funktionen bewusst zurückgesetzt. Bei schwer nachvollziehbaren XR-Zuständen wird ein Test mit vollständigem Reload als Kontrolllauf verwendet.

## 2026-09-15 – QuestTableLab bleibt eine allgemeine Lern- und Template-Basis

QuestTableLab bildet den allgemeinen Quest-AR-/MR-Grundaufbau ab: Git-Arbeitsweise, OpenXR, Passthrough, Steuerung, Platzierung, semantische Raumlabels und konfigurierbare Objektzuordnung.

Der eigene 3D-gedruckte Prototyp mit prototypspezifischer Teileerkennung und Wartungs- oder Reparaturbegleitung wird später als separates Praktikumsprojekt angelegt. Nach Abschluss der allgemeinen Grundlage soll aus QuestTableLab ein bereinigtes Template entstehen, damit technische Basis und fachlicher Anwendungsfall nicht miteinander vermischt werden.

## 2026-09-15 – Renderqualität auf dem Zielgerät

Das mobile URP-Profil verwendet Render Scale 1,0 und 4x MSAA. Die vorherige Render Scale von 0,8 führte im Headset zu sichtbar unscharfen und jitternden Kanten. Die neue Einstellung wurde auf der Quest 3 als scharf bestätigt.

Die Oberfläche des Quest-Systemmenüs kann durch die Compositor-Darstellung weiterhin anders wirken als normale Unity-Szenengeometrie. Maßgeblich ist deshalb die praktische Lesbarkeit und Stabilität der eigenen Anwendung auf dem Zielgerät.

## 2026-09-15 – Eindeutiger rechter Controller-Strahl

Der Teststrahl besitzt einen eigenen Aim-Knoten unter dem XR Tracking Space und wird ausschließlich aus der Pose des rechten Touch-Controllers gespeist. Bei aktiver Handsteuerung wird er ausgeblendet. Eine getrennte Hand-Ray-Interaktion kann später bewusst ergänzt werden, statt denselben sichtbaren Strahl unklar für beide Eingabearten zu verwenden.

Der Strahl endet am nächsten gültigen Physik- oder Bodentreffer. Cyan kennzeichnet den normalen Zielzustand, Orange den gedrückten rechten Index-Trigger und Grün die gültige Bodenvorschau.

## 2026-09-15 – Bedienung für die manuelle Platzierung

Der rechte Index-Trigger übernimmt sowohl das Aufnehmen und Verschieben des Würfels als auch die bestätigte Platzierung am angezeigten Bodenpunkt. Die B-Taste setzt den Würfel auf seine beim Start gespeicherte Position und Rotation zurück.

Diese kleine Belegung hält den Lernprototyp einfach, erlaubt aber bereits wiederholbare Interaktionstests ohne einen Neustart der Anwendung.

## 2026-09-15 – Floor-Level-Platzierung vor MRUK-Semantik

Die manuelle Bodenplatzierung aus Meilenstein 3 verwendet den auf der Quest kalibrierten Floor-Level-Ursprung und eine horizontale geometrische Ebene bei Y = 0. Sie benötigt noch keine Scene Permission und keine semantischen Raumdaten.

Diese Trennung ist beabsichtigt: Controller-Raycast, Vorschau, Platzierungsrechnung und Reset sind unabhängig von MRUK nachgewiesen. In Meilenstein 4 wird diese funktionierende Platzierungsbasis um `TABLE` und anschließend `WALL_FACE` ergänzt. Der Versatz des automatisch an der Wand platzierten Schilds soll konfigurierbar bleiben.

## 2026-09-15 – Eigene Runtime Assembly

Die Anwendungsskripte unter `Assets/App/Scripts` werden in einer eigenen Runtime Assembly zusammengefasst, die ausdrücklich auf `Oculus.VR` verweist. Die Play-Mode-Test-Assembly referenziert diese Runtime Assembly.

Dadurch können Tests die App-Komponenten direkt verwenden, ohne von der impliziten `Assembly-CSharp` abhängig zu sein. Gleichzeitig bleibt die Grenze zwischen Laufzeitcode und Testcode nachvollziehbar.

## 2026-09-15 – Meta Project Setup vollständig, aber Empfehlungen einzeln bewerten

Pflichtfehler des Meta Project Setup Tools werden behoben. Empfehlungen werden dagegen nicht pauschal über `Apply All` übernommen, sondern nach ihrem Nutzen für QuestTableLab bewertet.

Für Windows Standalone wird D3D11 verwendet. Das unterstützt den Meta XR Simulator und den Meta XR Operator, verändert aber nicht die Android-Grafik-API der Quest-Build. Simulator, Operator und OpenXR API Layer bleiben als ergänzende Entwicklungswerkzeuge eingerichtet; der physische Quest-Test bleibt der verbindliche Nachweis.

Scene Support und die automatische Laufzeit-Berechtigungsanfrage sind aktiviert, weil Meilenstein 4 unmittelbar MRUK-Raumdaten benötigt. Application SpaceWarp bleibt als Meta-Performanceempfehlung aktiviert und muss zusammen mit dem restlichen Rendering auf dem Zielgerät geprüft werden.

## 2026-09-15 – World-Space-Schild über OVROverlayCanvas

Das raumfeste `HelloPanel` verwendet `OVROverlayCanvas`, damit Text durch den Quest-Compositor klarer dargestellt werden kann. Für das statische Schild gelten Metas Textvorgaben: Depth-Tested-Komposition, Opaque-with-Clip, manuelles Redraw, deaktivierte dynamische Overlay-Auflösung und automatisch erzeugte Mipmaps.

Der authored Canvas und seine Inhalte liegen auf dem eigenen versteckten Layer `Overlay UI`. Dieser Layer wird aus den Culling Masks der XR-Kameras entfernt; normale Szenengeometrie auf `Default` bleibt sichtbar. Ein separater temporärer Layer `OVROverlayCanvas Rendering` ist für die interne Overlay-Ausgabe reserviert und in den URP-Renderer-Masken enthalten.

## 2026-09-15 – Scene API über MRUK bewusst und diagnostizierbar laden

Die Quest Scene API ist die Quelle des im Space Setup gespeicherten Raummodells und seiner semantischen Labels. MRUK 205.0.0 wird als Unity-Abstraktion verwendet, um diese Daten zu laden und als Raum- beziehungsweise Objektanker abzufragen. Das ist von einer späteren visuellen Erkennung individueller Prototypbauteile zu unterscheiden.

Der Anwendungscode stößt das Laden bewusst selbst an, statt MRUK ohne Rückmeldung beim Start laden zu lassen. Dadurch kann das Schild zwischen fehlender Raumfreigabe, fehlendem Space Setup, fehlendem `TABLE`-Label und allgemeinen Ladefehlern unterscheiden. Für komplexere Räume wird zunächst das High-Fidelity-Modell V2 mit Rückfall auf V1 angefordert.

Als erste Regel wird das nächstgelegene geeignete `TABLE`-Volumen gewählt. MRUK definiert die Position eines Volumenankers als Mittelpunkt seiner Oberseite; die halbe Höhe des Würfel-Colliders plus ein kleiner Sicherheitsabstand ergibt daher die Zielposition. Die bestehende manuelle Platzierung bleibt aktiv, und ihr Reset-Ziel wird nach erfolgreicher semantischer Platzierung aktualisiert.

## 2026-09-15 – Auswahl und Positionierung des WALL_FACE-Schilds

Die Tischwahl bleibt positionsbezogen: Das horizontal nächstgelegene geeignete `TABLE`-Volumen gewinnt. Für ein Statusschild ist dagegen Sichtbarkeit wichtiger. Deshalb bevorzugt die Anwendung eine ebene `WALL_FACE` in Blickrichtung, deren Vorderseite zum Benutzer zeigt; als Rückfall wird die nächstgelegene zum Benutzer gerichtete Wand verwendet.

Die Schildposition wird vom Mittelpunkt der erkannten Wandfläche aus über einen horizontalen und vertikalen Versatz bestimmt. Ein eigener Abstand hält das Schild geringfügig vor der realen Wand. Alle Werte sind im Inspector konfigurierbar, wobei die resultierende Position einschließlich Schildgröße innerhalb der Wandbegrenzung gehalten wird.
