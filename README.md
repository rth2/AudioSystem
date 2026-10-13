# AudioSystem
  I have this issue when working with unity, either for clients or on my own projects, where handling audio gets kind of annoying. Having to add serialized fields to objects or prefabs. Making sure everything has an audio source and they have separate ones for overlapping sounds. So I decided to set up a custom audio manager for my personal projects that streamlines my audio setup and maintenance.
  This is pretty basic currently, but as I work with it on projects it will be expanded for different things when applicable.

Main things this does and addresses:

1 - Centralizes audio management with Sound Definitions, enabling project wide updates or modifications without having to go through scene objects or prefabs. No need to mess with a bunch of serialized fields.

2 - Configurable Sound Libraries that can be persistent or dynamically loaded. This allows for fine control over the audio scope of the scene.

3 - Sound Keys are generated automatically at the push of an editor button on the Audio Manager. This links each sound definition to a generated enum making calling sounds type safe. This can act as both a sound lookup system while programming and also as a notification if a sound definition no longer exists.

4 - Audio pooling for audio sources allows for control over audio overhead. You can set a pool size, have the pool be expandable (so if you go over the size it will just expand), or have it not expandable so it won't go over a sound limit that you expect.

5 - Sound definitions have individual fields like volume, pitch, looping, etc... that allow for customization of the audio clip after import. So if you are handed an audio file that is super loud you can lower the sound definitions volume and have that controlled across the project.

Below is the readme if you want to check that out.

[README.md](https://github.com/user-attachments/files/33395621/README.md)
