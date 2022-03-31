# Linux Server

## **This is a work in progress, and will be useless once S&box adds a linux server build, so this is pointless to some degree. DO NOT ASK FOR HELP FROM FACEPUNCH!**

## Notes

**Read this whole document, then follow the steps/install the server.**
- Not everything works perfectly
- Lag is an issue sometimes
- CPU usage will be abnormally high, even when idle (due to wine/proton, **DON'T ASK FOR A FIX**, from what I know this is just a side effect)
- The file `sbox-server.json` is aimed at people using the pterodactyl server panel
- This is aimed at people who know linux
- Proton doesn't support stdin/terminal inputs (so no commands after starting the server without some sort of rcon)

## Install

Install the following
- `winbind`
- `steamcmd`
- `xvfb-run` / `Xvfb`
- `wine` (64-bit, use the winehq website)
- `python3`

SteamCmd
- Using steamcmd install proton 6.3 (no login) using appid `1580130` to a new folder (I'll call mine `./proton6`)
- Using steamcmd install proton experimental (no login required) using appid `1493710` to a new folder (I'll call mine `./protonexp`)
- Using steamcmd install sbox-server (no login required) using appid `1892930` and `+@sSteamCmdForcePlatformType windows` to force installing the windows version

Launch
- Set the envvar `STEAM_COMPAT_DATA_PATH` and `STEAM_COMPAT_CLIENT_INSTALL_PATH` to two different empty folders.
- Launch the `sbox-server.exe` with all your setup args using `./proton6/proton run`. The server will give an error and crash (an issue with proton 6, but this step is required).
- Launch `sbox-server.exe` with the same `STEAM_COMPAT_DATA_PATH`, `STEAM_COMPAT_CLIENT_INSTALL_PATH` and server args as before, but use `./protonexp/proton run`
- Server should start just like it would on Windows

Have any issues? Let me know, **DO NOT bug facepunch about it**.

## Configure

- https://wiki.facepunch.com/sbox/Dedicated_Server
- Server Name `+hostname servername`
- Server Port `+hostport port`
- Server IP (might not work) `+hostip ip`