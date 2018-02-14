# Inside Pulp Fiction
![Dancing GIF](https://github.com/juniorxsound/Pulp-Fiction-AR/blob/master/Resources/1.gif)

An experiment using [Volume](https://volume.gl) to reconstruct Pulp Fiction's dance scene in Augmented Reality. The experiment is built using the Unity3D game engine and Apple's ARKit framework.
1. [Installation](#installation)
1. [Usage](#usage)
1. [Video](https://www.youtube.com/watch?v=iwJt4DM6mJA)
1. [License](#license)

## Installation
- Clone the repository to your computer

- Download the pulp fiction assets [from this link](http://cdn.volume.gl/pulp_fiction_videos.zip) and unzip the contents into ```Assets/Videos``` in the project folder

- Open the project in Unity and open the Unity scene from ```Scenes/Pulp Fiction``` 

- Reconnect the videos you downloaded to the ```VideoPlayer``` components under the ```DepthPlayer``` game object
![Reconnect](https://github.com/juniorxsound/Pulp-Fiction-AR/blob/master/Resources/reconnect.png)

> Tested on macOS 10.13.3 with Unity 2017 3.0f2 Personal

## Usage
To be able to build an ARKit application you need a:
- Mac computer with XCode

- An Apple Developer account

[This is step-by-step guide](https://mobile-ar.reality.news/how-to/arkit-101-using-unity-arkit-plugin-create-apps-for-iphone-ipad-0178022/) of setting up the aforementioned

Once you are setup, go to Build Settings under ```File->Build Settings``` and make sure your platform is iOS, it is set to Latest Version of XCode and you are building in release mode

![Settings image](https://github.com/juniorxsound/Pulp-Fiction-AR/blob/master/Resources/settings.png)

## License

