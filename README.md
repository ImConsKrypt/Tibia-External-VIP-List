# Tibia External VIP List
A simplistic external VIP list for the MMORPG "Tibia"

**REQUIREMENTS**
- Microsoft Visual Studio 2022
- Newtonsoft JSon (NuGet Package)

**About**

Add player names and the worlds that they reside on within the `./Data/Config.json` file you wish to track, this application will check TibiaData's RESTFul API every 10 seconds (By default, configurable in `Config.json`) for matching names in either the Friend or Enemy categories within the local `Config.json` file and will show you Friends in GREEN, Guild Members in CYAN and Enemies in RED along with their vocation and level.

**KNOWN "ISSUES"**

Due to the nature of the RESTFul API, there is a slight delay to when people appear to show online/offline, unfortunately this is nothing that I can help as thats just how it is to reduce load on the services provided.
