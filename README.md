# Jumper game met ML Agent
## Door Nicolas Vanruysseveldt en Salwa Azzouz
## Inleiding
In dit project experimenteren we met de ML-Agent van Unity om een kubus (de agent) over een balk te laten springen en zoveel mogelijk bonuspunten te verzamelen. Het doel is om dit eerst handmatig te bereiken en vervolgens te automatiseren met behulp van ML-Agent.

## Omgeving
Om dit project uit te voeren, hebben we gebruik gemaakt van Unity en de ML-Agent package. In de omgeving bevinden zich: een blauwe kubus (agent), rode uitgestrekte kubussen (obstacle), een gele kubus (bonuspunt), een grotere onzichtbare kubus (wall) en een plane object (ground).
De obstakels bewegen met een willekeurige snelheid naar de overkant. Wanneer ze de onzichtbare muur raken, "spawnen" ze terug naar het begin. Als dit niet lukt, zal het obstakel opnieuw "gespawned" worden.

![Screenshot 2023-04-20 181641](https://user-images.githubusercontent.com/38139170/233451877-43a81f8b-24c3-42c4-9ba8-9f0e4ba8bb80.png)

De agent heeft een Ray Perception Sensor 3D. Deze heet een spacesize van 2 en 2 stacked vectors. De rays hebben een 0.75 Sphere radius. De rays zijn over 16.6 graden gespreid en hebben een lengte van 15.

![Screenshot 2023-04-20 191726](https://user-images.githubusercontent.com/38139170/233453117-92e62f6a-bb8f-4d8f-969c-993986d667a7.png)

De agent heeft volgende parameters: 

![Screenshot 2023-04-20 192128](https://user-images.githubusercontent.com/38139170/233453179-a4e7a8f7-3f12-42e2-9217-4017a1f45617.png)

Laat de AI voor een uur of langer trainen voor het beste resultaat.

## Resultaat
Hieronder vindt u een voorbeeld van het resultaat:

![Screenshot 2023-04-20 182850](https://user-images.githubusercontent.com/38139170/233453402-bd44939d-def4-4fb1-84c9-10377c58427b.png)

Hoe meer beloningen de agent verdient hoe langer de episode duurt.
