LiveSplit.HMA v1.0.0
=====================

LiveSplit.HMA is a [LiveSplit](http://livesplit.org/) component for Hitman: Absolution.

Features
--------
  * Keeps track of Game Time to get rid of loading times.
  * Auto start/reset/stop the timer.
  * Automatically pauses, if the game crashes.
  * Splits after each game section / at result screens (configurable). 

Requirements
------------

  * Hitman: Absolution on Steam (Cracked and previous versions are not supported)
  * LiveSplit 1.4+
  * .NET Framework 4  

Install
-------
Starting with LiveSplit 1.4, you can download and install LiveSplit.HMA automatically from within the Splits Editor with just one click. Just type in "Hitman: Absolution" and click Activate. This downloads LiveSplit.HMA to the Components folder.

If the plugin is not working with this process, download the plugin from the [releases page](https://github.com/SuiMachine/LiveSplit.HitmanAbsolution/releases) and place the LiveSplit.HMA.dll in your Components directory of LiveSplit.

Configure
---------
Open your Splits Editor and active the autosplitter. If this is not working, leave it deactivated and manually add it in the Layout Editor. You can configure the settings in whichever editor it has been enabled in.

After configuring everything you'll most likely want to turn on game time as primary timing, so that your splits will run off game time. You can do this by right-clicking LiveSplit and going to Compare Against -> Game Time.


Credits
-------
  * [SuicideMachine](http://twitch.tv/suicidemachine)
  * [DrTChops](http://twitch.tv/drtchops) for helping me with it.
  * Plugin is based off [LiveSplit.Dishonored](https://github.com/fatalis/LiveSplit.Dishonored) by [Fatalis](http://twitch.tv/fatalis_).
