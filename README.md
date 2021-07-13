# Lights Out

Lights Out is a puzzle game consisting of an n x n grid of lights. At the beginning of the game, some of the lights are switched on. 
When a light is pressed, this light and the four adjacent lights are toggled, i.e., they are switched on if they were off, and switched off otherwise. 
The purpose of the game is to switch all the lights off.

This repo is an implementation of the lights out game using event sourcing. Each action is saved in an event store and has snapshot functionality (saving the current state to prevent replaying all events from 0).

# Preview 

![](https://user-images.githubusercontent.com/20759400/125062469-b3c69780-e0ae-11eb-8ffb-925537411ae6.gif)
