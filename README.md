# Tibia-Player-Status
A simplistic external VIP list for the MMORPG "Tibia"

**REQUIREMENTS**
- Microsoft Visual Studio 2022
- Newtonsoft JSon (NuGet Package)

**About**

Add names within the `./Data/Names.json` file you wish to track, this application will check TibiaData's RESTFul API every 5 seconds for matching names in either the Friend or Enemy categories within the local `Names.json` file and will show you Friends in GREEN and Enemies in RED along with their vocation and level.

**KNOWN "ISSUES"**

Due to the nature of the RESTFul API, there is a slight delay to when people appear to show online/offline, unfortunately this is nothing that I can help as thats just how it is to reduce load on the services provided.
