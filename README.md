# OnlineRoulette
API de Ruleta de Apuestas Online en .NET Core

# Endpoints
1. POST - /api/Roulette     
Endpoint de creación de nuevas ruletas que devuelva el id de la nueva ruleta creada

2. PUT - /api/Roulette/[rouletteId]/open 
Endpoint de apertura de ruleta (el input es un id de ruleta) que permita las
posteriores peticiones de apuestas, este debe devolver simplemente un estado que
confirme que la operación fue exitosa o denegada.

3. POST - /api/Roulette/[rouletteId]/bet 
Endpoint de apuesta a un número (los números válidos para apostar son del 0 al 36)
o color (negro o rojo) de la ruleta una cantidad determinada de dinero (máximo
10.000 dólares) a una ruleta abierta.

4. PUT - /api/Roulette/[rouletteId]/close
Endpoint de cierre apuestas dado un id de ruleta, este endpoint debe devolver el
resultado de las apuestas hechas desde su apertura hasta el cierre.

5. GET - /api/Roulette 
Endpoint de listado de ruletas creadas con sus estados (abierta o cerrada)
