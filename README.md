# mcrcon
mcrcon is an RCON (Remote Connection) tool for interfacing with [Minecraft's](http://minecraft.net) RCON capabilities built into it's server software. mcrcon utilizes the [SourceRcon C# Library]() built to follow [Valve's RCON TCP Protocol](). This tool is made to make using RCON simple, and with a GUI interface to make doing so quick and easy. And console free! Unless of course, you want to use a console. Then by all means, you have this option as well!

## SourceRcon
SourceRcon is a C# library made to interface with servers that implement the Valve RCON protocol. It is a stand-alone library, and is used in mcrcon as the RCON back-end. 

## Licensing
mcrcon is licensed under the GNU GPL v2 license as laid out in [LICENSE.md](https://github.com/pazuzu156/mcrcon/blob/master/LICENSE.md).

SourceRcon is licensed under the BSD 2-Clause license as laid out in the same license file as above.

## Building
mcrcon is built using Visual Studio 2013 utilizing .NET 4.5 framework. SourceRcon uses an older VS version and an older version of the .NET framework. However, opening mcrcon.sln in VS2013 will automatically update the SourceRcon library to the latest solution format supported by VS2013. 

To build, open Visual Studio 2013, and open mcrcon.sln. Press F6, and the project will build along with SourceRcon.

If you wish, you can also use Visual Studio 2015. You'll just have to go through the same process with mcrcon as you do with SourceRcon.
