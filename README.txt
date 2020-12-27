
,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,
...,,,..........,,,...(@@@@@@@@@*,,,,...,,...........,,....,,,,,,,,,,,,,...,,,..
....................&@@@@@@@@@@@@@@,.........................,,,,,,,,,..........
...................#@@@@@@@@@@@@@@@@@.........................,,,,,,,...........
.................../@@@@@@@@@**@@@@@@@,......................,,,,,,,,,..........
.,..................,,@#*@   @@.   @@@@/..............,&@@@@@@@@@#,,,,,..,......
,,,,,.............,,,,#*&    &&      &@@#,(.......*@@@@@@@@@@@@@@@@@*,,,,,,,....
,,,,(/,,.......,,,,,,,#*(              @@&,..,@@@@@****@@@@@@@@@@@@@@(,,,,,,,,,,
,,,,,(@*,,,,,,,,,,,,,,#*&              .%#@&@@@.   @@@*      @@@@@@@@@,,,,,,,,,,
.,,.../@@*,,,,,,,,,.....,/@@@@*....,@@(***/%@@@               #@@@@@@/.,,,,....,
......./@@@,,,,@@#......,*@*...................#&../@@*      .@*(#%*............
........*@@@@@@@@@@**@@@@@#......................(.....&&   @**#................
.........*@@@@@@@@@@@@@@@@(.............................&@....,,................
....,,.,,,*@@&*#@@@@@@@@@@@/............................@@,,................,..,
..,,,,,,,,,,,,,,,@@#,,,,@@@@@@@(.......................@@@@@%.............,,,,,(
,,,,,,,,,,,,,,,,,,,,,,,,,@@(,..#@,..............*#..#@@@@@@@@@@@@@&..,,,,*&@@(,,
,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,#@@(........#@&/@@@@@@@@@@@@@@@@@@@/@@@@@%,,,,,
...,,,..........,,,....,,,,,,,,,,,,,,,#&@@&#,...../@%,,..(@@@@@@@@@@@@@@,..,,,..
.........................,,,,,,,,,.........................&&,,,,*@@@*..........
..........................,,,,,,,............................,,,,,,,,...........

    HEY STINKY!                
*** DOWNLOAD UNITY VERSION 2019.4.17f1 BEFORE DOWNLOADING THIS REPOSITORY!!! ***


--------------------------------------------------------------------------------


Welcome to the EGaDS 2020 Micro-jam! Hopefully, this size of this document doesn't alarm you; please understand that a lot of standards need to be met in order to combine the works of a multitude of game devs into a single unity project

This doc is mostly aimed at the programmers, but there are some things that the audio people need to see as well. For sounds, check the constraints list. For music, check the bottom.

I wanna give a huge shout-out to junior officers Gluethulu (Alex), 1f1nity (Matt), and Noah! (Noah), as well as senior officers Terry, Karina and David for the work they've contributed to this project over the past month. This jam wouldn't be happening without them! :D

If you have any questions, send a message in #ask-the-officers or pm me directly (Grant/Frogbee).

Happy Jamming!

-----------------------------------------
   Requirements (what you need to do):
-----------------------------------------
> EVERY SCRIPT you add must have the same unique namespace:
	///
	namespace MyTeam
	{
		public class MyClass : Monobehavior
		{
		// Your code here //

		}
	}
    ///
	- If you don't do this, there may be naming conflicts with other team's projects
	- Give the folder you submit the name: [team number]-[team name]. This is so that file names don't collide.
	    - Examples: 1-TheHoneycombs
	                6-Team 6
	                
                    <*****************************************>
> DELETE all starting example files besides the main scene. DON'T reuse ANY of them
    - The purpose of these files is to show the you how to incorporate winning and losing into you're game, as well as music and sound effects. 
    - If you reuse these objects, there will be GUID collisions with other projects that also reuse them, which causes bad things to happen.
    - List of Items to DELETE after you are done looking at them:
        - ExampleGameScript.cs
        - ExampleMusic.wav
        - win.wav
        - Minigame (Minigame scriptable object)
        - Example Game Object (In the Hierarchy window)
    - In addition, DON'T add components to the "Minigame Manager" object in the inspector. It should only have the MinigameManager script attached.    
    - Once you delete the Minigame scriptable object, you'll need to make another one.  
        - Right click in the Project window.
        - Click "Create" and then "Minigame"
        - Drag your new minigame into the "Minigame" slot on the Minigame Manager in the inspector.
        - Change the name of the object to the name of your game at some point.
-----------------------------------------	
  Constraints List (what you can't do):
-----------------------------------------
 > Don't change ANYTHING in project settings. Don't add any new tags, collision layers, or sorting layers either.
    - There are a few added tags along with collision and rendering layers for you to work with. Hopefully these will be enough with how simple the games are.
    - You can check the collision matrix in the physics2D section of project settings, but again, don't change them.
	
                    <*****************************************>
 > Keep sounds to the Minigame scriptable object
    - This is so your sounds will be faded out on transition rather than cut off or continue playing
    - Play them by calling MinigameManager.Instance.PlaySound([sound name])
    - If you want a sound to loop, I recommend doing it using a coroutine that calls PlaySound at set intervals:
        ///
        private bool soundShouldPlay // set this elsewhere in code
        private IEnumerator PlayLoopingSound (string soundName) {
            while (soundShouldPlay) {
                MinigameManager.Instance.PlaySound(soundName);
                yield return new WaitForSeconds([looptime]);
            }
        }
        ///
        
                    <*****************************************>
 > Stick to WASD/arrow keys and space for your games controls. 
    - You can technically still call Input.GetKeyDown, but this may confuse the player. Keeping the controls simple makes the snappy-ness of these games possible
    
                    <*****************************************>
 > Don't use commands like "Timescale.time" or "SceneManager.Load"
    - There's probably a whole slew of commands you could do that would break the main game. If you have questions about using any commands, ask in #ask-the-officers
-----------------------------------------
        Tips for Minigame-making:
-----------------------------------------
 > The name of your scene is made into text that flashes before your game starts. The default name, you may notice, is "Scene".
    - Use this text to give some direction to the player. It should describe what the player needs to do in 1-3 words.
    - An exclamation mark is automatically added at the end of your scene name.
    - Changing the name of your scene shouldn't cause any loading issues, since the minigame scenes are loaded by numerical ID. Please don't edit the build settings.
    - This is not the title of your game. Make the name of your Minigame scriptable object the title.
    
                    <*****************************************>
 > On the Minigame Manager object in the inspector, theres a bool called "debug game only". 
    - Set this to true when you want to test the game alone.
    - You will need to set it to false on occasion to make sure that your minigame is incorporated well into the main game. For example, you will need to check if winning and losing work as you intended.
    
                    <*****************************************>
 > You may notice that there is a Yellow border attached to the Camera
    - This border is meant to contain your game, but dont be afraid to break out of it! 
    - The final border will be animated and be on Canvas sort order 1, so if you want to have some UI pop outside the border, set the canvas to level 2 or higher (but not above 99).
    
                    <*****************************************>
 > Music Making Guidelines: 
    - 140 BPM (to be in-time with the clock and main music)
    - Short games are 2 measures (~3.4 seconds)
    - Long games are 4 measures (~6.8 seconds)
    - I recommend that you leave about a half-beat of rest at the end so that the transition between songs is smooth.
    - By no means do you need to follow these guidelines exactly. The lengths are the only hard-limits.
-----------------------------------------
        SUBMISSION CHECKLIST:
-----------------------------------------
 - Did you delete the objects listed above?
 - Did you rename the folder that has all of your assets from "MyTeam"?
 - Did you put all of your scripts in the same namespace?
 - Did you remove all of your debug.log and print statements? (so that we can debug easier)
 - Did you ONLY zip up YOUR scene and assets in your submission file?
 
 
 
 
 
 
 
 
 
 
                                                                                 
                                  %@@@@@@&  @@@@@@@*                            
                      ..    @@@@%&&((((((#@@@/(((((@@                           
                   @@%/((@&@(((((@((((((((%@((((((((@@&((%@@                    
                  &@(((((((@(((((%(((((((((#(((((((/@((((((&&&@@&@@@(           
            &@%((&@@(((((((#((((((%((((((((((((((((/%((((((#@(((((((@&          
           %@(((((((&(((((((((((((((((((((((((((((((((((((/&((((((((&&          
      .&@@@&@#((((((((&(((((((((((((((((((((((((((((((((((((((((((((@&&&#       
     @@%(((((&(((((((((((((((((((((((((((((((((((((((((((((((((((((%(((((@@     
    .@&((((((((%(((((((((((((((((((((((((((((((((((((((((((((((((((((((((#@.    
     &@((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((#@@,     
       &@#(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((@@/  
    @@#(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((@@  
   @@/((((((((((((((((((((((((((((((%((((((((((((((((((((((((((((((((((((((%@/  
   %@%(((((((((((((((((((((((((&/((((&((((#((((((&/(%(((((((((((((((((&&&&@@@(  
    (@@&(/((((((((((((((((((((/@/((((%(((((%(((((&&((((((((((((((((((((((((((@@.
 *@@/((((((((((((((((((((((((((%%(((@@((((@@#(((&@/(((((((((((((((((((((((((((@&
 @@#((((((((((((((((((((((#/((((%@@&../&@&../@@@&@(((((@(((((((((((((((((((((@@ 
 .@@#((((((((((((((((((((((@((((&@               @@@@@#((((((((((((((((((#(%@&  
     /#@@((((((((((((((((((((&@&*@@@@@@/   ,@@@@@@* @@(((((((((((((((((((((((&@*
     #@((((((((((((((((((((((#@%  %@@&. %&@, %@@@/ % /@@/((((((((((((((((((((&@%
     %@&((((&@(((((((#((((&%@@##.%%#&#&(((((#%@#(&*/..@((((((((((((((@#((((%@@% 
           %&((((((#@%(((((@( %......&(((((((##.....%(@@@&((((((&(((((&@ ..     
           @@((((%@  @#((((@*@,.....&(((((((((%*.....@  %#((((((@@#(((@@,       
            (&&%.     %@@@&,#@@%.(%(((((((((((((%&.%@%.  @%((((@& .&@@&,   - Oh yeah, dude. That's all she wrote. 
                        &@&($$#&.@(((((((((((((((&*@#($(%@%*%%/                 Heh heh heh...
                     &@#($$$$$(@..,&@@&(((((#@@%,..@($$$$$$(@@                  
                   &@($$$$$$$$(@..@@%,&@&.#@%.(@@*.@($$$$$$$$(&@                
                 (@%($$$$$$$$$(@,.@%,..........,@*,@($$$$$$$$$$(@%              
                /@@@@@@#($$$$$(&(.@%............@*,@($$$$$$$(&@@@@#             
                      ,@%@&($$(%&/@#..,&,../&..,@&(&($$$(&&(@%                  
                     &@($$(#@(@@@@@@*..........@@@@@@&(&($$$(%@                 
                    @&($$(#@(&@@@@@*...../......@@@@@@#(@($$$$(@,               
                  #@(((((((((@@#((((&@@@@@@@@@%(((((%@@#($$$$$$(@&              
                  