# DelugeRPCClient.Net
.Net DelugeWeb RPC Client is a refactorisation of the great work done by [SilverCard](https://github.com/SilverCard) on [DelugeWebClient](https://github.com/SilverCard/DelugeWebClient)

---
This library allows you to connect to the delugeweb RPC API. Some methods have been implemented, others will come according to needs or requests.
The library targets multiple frameworks : 
- .Net Core 3.1
- .Net Core 5
- .Net Core 6
- .NET Standard 2.0
- .NET 4.8

---
## Installation
Simply download and restore nuget packages https://www.nuget.org/packages/DelugeRPCClient.Net/ or install it from package manager
```
PM> Install-Package DelugeRPCClient.Net -Version x.x.x
```

---
## Create a DelugeClient Object
```C#
DelugeClient client = new DelugeClient(url: Constants.DelugeUrl, password: Constants.DelugePassword);
```

## Authentification

#### Login

```C#
bool loginResult = await client.Login();
```

#### Logout

```C#
bool logoutResult = await client.Logout();
```

## Torrents

#### List and Get torrents

List torrents
```C#
DelugeClient client = new DelugeClient(url: Constants.DelugeUrl, password: Constants.DelugePassword);
bool loginResult = await client.Login();
List<Torrent> torrents = await client.ListTorrents();
bool logoutResult = await client.Logout();
```

Get torrent by hash
```C#
DelugeClient client = new DelugeClient(url: Constants.DelugeUrl, password: Constants.DelugePassword);
bool loginResult = await client.Login();
Torrent torrent = await client.GetTorrent(torrentHash);
bool logoutResult = await client.Logout();
```

#### Add Torrent

Add a torrent by magnet uri
```C#
bool loginResult = await client.Login();
Torrent torrent = await client.AddTorrentByMagnet(torrentMagnetUri);
bool logoutResult = await client.Logout();
```

Add a torrent by .torrent file
```C#
bool loginResult = await client.Login();
Torrent torrent = await client.AddTorrentByFile(torrentFilename);
bool logoutResult = await client.Logout();
```

#### Remove Torrent
```C#
bool loginResult = await client.Login();
bool removeTorrentResult = await client.RemoveTorrent(torrentHash);
bool logoutResult = await client.Logout();
```

#### Pause and Resume Torrent
Pause Torrent
```C#
bool loginResult = await client.Login();
bool pauseResult = await client.PauseTorrent(torrentHash);
bool logoutResult = await client.Logout();
```
Resume Torrent
```C#
bool loginResult = await client.Login();
bool resumeResult = await client.ResumeTorrent(torrentHash);
bool logoutResult = await client.Logout();
```

## Labels

#### List existing labels
```C#
bool loginResult = await client.Login();
List<string> labels = await client.ListLabels();
bool logoutResult = await client.Logout();
```

#### Check if a label exists
```C#
bool loginResult = await client.Login();
bool exists = await client.LabelExists(label);
bool logoutResult = await client.Logout();
```

#### Add a new label
```C#
bool loginResult = await client.Login();
bool addLabelResult = await client.AddLabel(label);
bool logoutResult = await client.Logout();
```

#### Remove an existing label
```C#
bool loginResult = await client.Login();
bool removeLabelResult = await client.RemoveLabel(label);
bool logoutResult = await client.Logout();
```

#### Assing a label to a torrent (if label dont exists it will be created)
```C#
bool loginResult = await client.Login();
bool assignResult = await client.SetTorrentLabel(torrentHash, label);
bool logoutResult = await client.Logout();
```
