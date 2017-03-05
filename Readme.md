This provides a cross platform way to launch processes on mac/linux/or windows.

Simply call  `Opener.Open()` with whatever commands you need


common examples


```cs

Opener.Net.Opener.Open("http://google.com"); // launch a browser window

Opener.Net.Opener.Open("npm install", processStartInfo: new ProcessStartInfo(){ WorkingDirectory = "c:/projects/npmrest"}); //npm install in a specific directory

```