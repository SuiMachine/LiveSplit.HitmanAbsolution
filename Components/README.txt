LiveSplit.HMA v0.9.5
=====================

LiveSplit.HMA is a [LiveSplit](http://livesplit.org/) component for Hitman: Absolution.

Features
--------
  * Keeps track of Game Time to get rid of loading times.
  * Auto start/stop/reset the timer.
  * Automatically pauses, if the game crashes.
  * Splits after each game section / at result screens (configurable).
  
Currently the component isn't fully tested. If, I consider it to be ready, I'll probably ask to add it to Livesplit.
  

Install
-------
Download the plugin from the [releases page](https://github.com/SuiMachine/LiveSplit.HitmanAbsolution/releases) and place the LiveSplit.DXHR.dll in your Components directory of LiveSplit.

Configure
---------
Open your Layout editor and add Other -> HMA. You can then configure it, using Layout editor.

After configuring everything you'll most likely want to turn on game time as primary timing, so that your splits will run off game time. You can do this by right-clicking LiveSplit and going to Compare Against -> Game Time.

#### Auto Split
The default settings are to automatically reset, start, and end the splits (the first and last splits). You can enable individual splits here.

#### Alternate Timing Method
If you wish to show Real Time on your layout, download AlternateTimingMethod from the [LiveSplit Components page](http://livesplit.org/components/) or its own [Github page](https://github.com/Dalet/LiveSplit.AlternateTimingMethod/releases).

Change Log
----------
https://github.com/SuiMachine/LiveSplit.HitmanAbsolution/releases

Credits
-------
  * [SuicideMachine](http://twitch.tv/suicidemachine)
  * [DrTChops](http://twitch.tv/drtchops) for helping me with it.
  * Plugin is based off [LiveSplit.Dishonored](https://github.com/fatalis/LiveSplit.Dishonored) by [Fatalis](http://twitch.tv/fatalis_).
