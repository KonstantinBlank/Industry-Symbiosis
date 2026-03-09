Matching


Views
User sieht alle seine Inputs in einer Liste
- Es werden alle Streams mit dem Attribut is_Input = true und noch nicht vollständig gemachted des eigenen Unternehmens aufgerufen und aufgelistet
Beim Klicken auf einen Input werden ihm alle möglichen Matches (Outputs anderer Unternehmen) angezeigt. 
- Es werden für jedes Unternehmen (auch das eigene) alle Outputs durchsucht und gefiltert. Filterkriterien sind das Material bzw. Energie des gewählten Input-Streams, frei verfügbare Menge und ohne Start- und Enddatum vorhanden.

User sieht alle seine Outputs in einer Liste
- Es werden alle Streams mit dem Attribut is_Input = false und noch nicht vollständig gemachted des eigenen Unternehmens aufgerufen und aufgelistet
Beim Klick auf einen Output werden ihm alle möglichen Matches (Inputs anderer Unternehmen) angezeigt. 
- Es werden für jedes Unternehmen (auch das eigene) alle Inputs durchsucht und gefiltert. Filterkriterien sind das Material bzw. Energie des gewählten Input-Streams, frei verfügbare Menge, ohne Start- und Enddatum  vorhanden.

User sieht alle angefragten Matches -> Outgoing
- Aus der Match-Tabelle werden alle Matches, die von dem Unternehmen kommen und den Proposed-Status haben, abgefragt

User sieht alle angefragten Matchen -> Incoming
- Aus der Match-Tabelle werden alle Matches, die an das Unternehmen gerichtet sind und den Proposed-Status haben, abgefragt

User sieht alle aktiven Matches
- Aus der Match-Tabelle werden alle Matches, die von dem Unternehmen kommen oder an das Unternehmen gerichtet sind und den Aktiv-Status haben, abgefragt

Process
Wenn der Nutzer auf “Match anfragen” in der View Inputs oder Outputs klickt
- Er wählt einen Input- bzw. Outputstream von sich aus
- Es werden ihm alle möglichen Matches (Outputs angezeigt)
- Er wählt einen Output- bzw. Inputstream (der anderen Unternehmen) aus
- Außerdem gibt er folgende Daten an
    - Startdatum
    - Enddatum
    - Menge (maximal die Menge des eigenen Inputs bzw. Outputs) 
    - Stoff / Energie (automatisch, ergibt sich aus den Streams) 
    - Intervall (pro Woche, pro Monat)
    - Preisvorschlag
    - Kommentar
- Abschließend bestätigt er mit einem Button-Klick
    - Es wird geprüft wie viel der Menge insgesamt bereits in anderen Matches gehandelt wird. Wenn ein Stream bereits teilweise gehandelt wird, wird der Nutzer beim Antrag stellen darüber informiert. 
    - Es wird ein Match-Eintrag in der Match-Tabelle erzeugt
- Es erhalten alle Nutzer des anderen Unternehmen eine Email mit der Matchanfrage und den Details sowie Kontaktdaten der anfragenden Person
    - Es wird eine Mail versendet.
- Es wird ein Eintrag in den Incoming Matches der Nutzer des anderen Unternehmen erzeugt
- Es wird ein neues Eintrag in den Outgoing Matches erzeugt
- Das potenzielle Match wird aus der Liste der möglichen Matches für beide Unternehmen entfernt


Wenn der Nutzer eine Match-Anfrage erhält
- Kann er dieses Bestätigen
    - Der Output-Stream wird als gematched markiert
    - Der Input-Stream wird als gematched markiert
    - Der Input und  der Output-Stream wird von den Listen möglicher Matches angepasst bzw. entfernt
        - Der Match-Eintrag erhält den Status aktiv. Außerdem drop table.
    - Die Liste der angefragten Machtes (Incoming und Outgoing) wird auf Machbarkeit überprüft / neuberechnet 
        - Wenn ein angefragtes Match nicht mehr möglich ist, werden beide Unternehmen darüber informiert und das Match wird aus der Datenbank entfernt.
        - Es werden alle Matches, die proposed sind und einen der beiden Streams enthalten auf die neue, verfügbare Menge überprüft. Ist der Bedarf größer als das neue Angebot bzw. ist das Angebot kleiner als der gefragte Bedarf wird das Match gelöscht. Beide Unternehmen werden mit allen Details informiert. 
- Kann er dieses Ablehnen
    - Das Match wird aus der Liste der Incoming Matches entfernt
    - Das Match wird aus der Liste der Outgoing Matches der Nutzer des anderen Unternehmens entfernt
    - Der anfragende Nutzer erhält eine Mail mit der Absage
        - Es wird eine Email gesendet.
    - In der Datenbank wird das Match als abgesagt markiert.


Data 
- Wenn ein Stream bearbeitet wird, werden alle assoziierten Matches beendet. 
