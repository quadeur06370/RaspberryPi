#!/usr/bin/env python
# coding: utf-8
from sense_hat import SenseHat
import socket
from time import sleep
sense = SenseHat()

S = (0,255,0) #Vert
F = (255,0,0) # Rouge

succes = [
S, S, S, S, S, S, S, S,
S, S, S, S, S, S, S, S,
S, S, S, S, S, S, S, S,
S, S, S, S, S, S, S, S,
S, S, S, S, S, S, S, S,
S, S, S, S, S, S, S, S,
S, S, S, S, S, S, S, S,
S, S, S, S, S, S, S, S
]

fail = [
F, F, F, F, F, F, F, F,
F, F, F, F, F, F, F, F,
F, F, F, F, F, F, F, F,
F, F, F, F, F, F, F, F,
F, F, F, F, F, F, F, F,
F, F, F, F, F, F, F, F,
F, F, F, F, F, F, F, F,
F, F, F, F, F, F, F, F
]

g = (0, 255, 0)
b = (0, 0,255)

O = (0,0,0)
X = (255,255,255)

smileyGauche = [
  O,X,X,X,X,X,X,O,
  X,O,O,O,O,O,O,X,
  X,O,X,O,X,O,O,X,
  X,O,O,O,O,O,O,X,
  X,X,O,O,O,X,O,X,
  X,O,X,X,X,O,O,X,
  X,O,O,O,O,O,O,X,
  O,X,X,X,X,X,X,O
  ]
  
smileyDroit = [
  O,X,X,X,X,X,X,O,
  X,O,O,O,O,O,O,X,
  X,O,O,X,O,X,O,X,
  X,O,O,O,O,O,O,X,
  X,O,X,O,O,O,X,X,
  X,O,O,X,X,X,O,X,
  X,O,O,O,O,O,O,X,
  O,X,X,X,X,X,X,O
  ]

TristeGauche = [
  O,X,X,X,X,X,X,O,
  X,O,O,O,O,O,O,X,
  X,O,X,O,X,O,O,X,
  X,O,O,O,O,O,O,X,
  X,O,O,O,O,O,O,X,
  X,O,X,X,X,O,O,X,
  X,X,O,O,O,X,O,X,
  O,X,X,X,X,X,X,O
  ]
  
TristeDroit = [
  O,X,X,X,X,X,X,O,
  X,O,O,O,O,O,O,X,
  X,O,O,X,O,X,O,X,
  X,O,O,O,O,O,O,X,
  X,O,O,O,O,O,O,X,
  X,O,O,X,X,X,O,X,
  X,O,X,O,O,O,X,X,
  O,X,X,X,X,X,X,O
  ]


socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
socket.bind(('', 15555))
sense = SenseHat()

def left():
  sense.set_pixels(smileyGauche)

def right():
  sense.set_pixels(smileyDroit)
  
def gauchetriste():
  sense.set_pixels(TristeGauche)

def droitetriste():
  sense.set_pixels(TristeDroit)
        

while True:
        socket.listen(5)
        client, address = socket.accept()
        print "{} connected".format( address )

        
        while True :
            recu = client.recv(255)
            reponse = recu.split(' ')
            print recu
            if reponse[0] == "completed": 
                sense.set_pixels(succes)
                sleep(3)
                sense.clear(0,0,0)
                sense.set_pixels(smileyGauche)
                sense.stick.direction_left = left
                sense.stick.direction_right = right
                sleep(5)
                sense.clear(0,0,0)
                sense.show_message(reponse[1] , text_colour=(0,255,0))
            if reponse[0] == "failed":
                sense.set_pixels(fail)
                sleep(3)
                sense.clear(0,0,0)
                sense.set_pixels(TristeGauche)
                sense.stick.direction_left = gauchetriste
                sense.stick.direction_right = droitetriste
                sleep(5)
                sense.clear(0,0,0)
                sense.show_message(reponse[1] , text_colour=(255,0,0))
            
       


                        
        

client.close()
stock.close()
