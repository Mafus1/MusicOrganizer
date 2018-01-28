# morg - Music ORGanizer

Simple application to edit tags of music files.

## Building
Built using Visual Studio 2017.
Let NuGet restore packages bevor building the solution.

## Dependency
This app depends on [taglib-sharp](https://github.com/mono/taglib-sharp) for all the tag editing.

## Usage
You can either build it yourself, or download the pre build zip from the [release page](https://github.com/Mafus1/MusicOrganizer/releases).

When you download the zip:
1. Unpack the content to a folder on your pc.
2. Add the path to the folder to your [PATH environment variable](https://www.howtogeek.com/118594/how-to-edit-your-system-path-for-easy-command-line-access/).
3. Open a music album folder like this: `morg "Downloads\Rage Against The Machine\1996 - Evil Empire"`.
4. Type help to see all the commands available.
