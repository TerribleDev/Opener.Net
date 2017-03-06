This provides a cross platform way to launch processes on mac/linux/or windows. Inspired by [opener](https://github.com/domenic/opener)

Simply call  `Opener.Open()` with whatever commands you need


common examples


```cs

Opener.Net.Opener.Open("http://google.com"); // launch a browser window

Opener.Net.Opener.Open("npm install", processStartInfo: new ProcessStartInfo(){ WorkingDirectory = "c:/projects/npmrest"}); //npm install in a specific directory

```

## Why?


Because Windows has start, Macs have open, and *nix has xdg-open. At least according to [some guy on StackOverflow](http://stackoverflow.com/q/1480971/3191). And I like things that work on all three. Like dotnet core. And Opener.Net.

