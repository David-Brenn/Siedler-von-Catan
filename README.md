Siedler von Catan, das Kartenspiel

Dieses Projekt dient zum Lernen von Unity.

Gamemanager
  Singelton, zum Managen vom Gamestate. Soll die Logik des Spiels beinhalten

Player
  Beinhalter alle Karten die ein Spieler hat. Außerdem werden hier die möglich Aktionen gehandelt durch z. B. dem Grabber

  CardPos: Beinhaltet empty Gameobjects für alle möglichen Kartenpositionen.
  Cards: Beinhaltet alle Gameobjects die auf dem Feld liegen von einem Spieler
  
DeckManager
  Erstellt und managed die verschiedenen Karten decks. 

Deck 
  Hat eine Liste von Karten und stellt verschiedene methoden zum interagieren mit dem Deck. Hat eine referenz zum HandCardObject um ein neues Karten object zu erstellen wenn eine Karte gezogen wird.

Karten
  Es gibt 3 verschiedene stadies die eine Karte haben kann. 1. Scriptable object (Nur die daten), 2. UI Element (Als handkarte), 3. Als 3d objekt auf dem Brett

  1. Scriptable objects speichern die kosten von Karten als auch verbundene interaktionen und prefabs.
  2. Karten werden als HandCardObject UI objekt dargestellt um interaktionen mit dem Spieler zu ermöglichen
  3. Auf dem Brett werden die Prefabs die im scripable object gespeichert sind initialisiert.


  
