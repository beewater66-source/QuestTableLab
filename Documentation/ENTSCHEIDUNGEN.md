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

Diese feste Position ist nur ein Zwischenschritt. Im folgenden Schritt soll die Anwendung eine semantisch als Tisch klassifizierte Fläche aus den Quest-Raumdaten bestimmen und Würfel sowie Beschriftung relativ zu dieser Fläche platzieren.
