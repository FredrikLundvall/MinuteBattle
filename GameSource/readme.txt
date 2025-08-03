
The game loop has a current scene that is managed by the Stage class. It is a collection of scenes that can be switched between. 
The current scene is the one that is currently being updated and rendered. It can be fetched using the following code: Stage.GetCurrentScene(_game);
In the game loop, the current scene is updated and rendered each frame. The Stage class handles the logic for switching scenes and managing the scene stack based om the CardGame object 
that holds the actual game state.


