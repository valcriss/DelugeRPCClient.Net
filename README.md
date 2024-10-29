# DelugeRPCClient.Net [![.NET](https://github.com/valcriss/DelugeRPCClient.Net/actions/workflows/dotnet.yml/badge.svg?branch=main)](https://github.com/valcriss/DelugeRPCClient.Net/actions/workflows/dotnet.yml)
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
List<Torrent> torrents = await client.ListTorrents();
```

List torrents extended
```C#
List<TorrentExtended> torrents = await client.ListTorrentsExtended();
```

Get torrent by hash
```C#
Torrent torrent = await client.GetTorrent(torrentHash);
```

Get torrent extended by hash
```C#
TorrentExtended torrent = await client.GetTorrentExtended(torrentHash);
```

#### Add Torrent

Add a torrent by magnet uri
```C#
Torrent torrent = await client.AddTorrentByMagnet(torrentMagnetUri);
```

Add a torrent by .torrent file
```C#
Torrent torrent = await client.AddTorrentByFile(torrentFilename);
```

Add a torrent by .torrent url
```C#
Torrent torrent = await client.AddTorrentByUrl(torrentUrl);
```

#### Remove Torrent
```C#
bool removeTorrentResult = await client.RemoveTorrent(torrentHash);
```

#### Pause and Resume Torrent
Pause Torrent
```C#
bool pauseResult = await client.PauseTorrent(torrentHash);
```
Resume Torrent
```C#
bool resumeResult = await client.ResumeTorrent(torrentHash);
```

#### Recheck Torrents
```C#
bool recheckTorrentResult = await client.RecheckTorrents(List<string> torrentsHash);
```

## Configs

#### List configs
```C#
List<Config> config = await client.ListConfigs();
```

## Labels

#### List existing labels
```C#
List<string> labels = await client.ListLabels();
```

#### Check if a label exists
```C#
bool exists = await client.LabelExists(label);
```

#### Add a new label
```C#
bool addLabelResult = await client.AddLabel(label);
```

#### Remove an existing label
```C#
bool removeLabelResult = await client.RemoveLabel(label);
```

#### Assing a label to a torrent (if label dont exists it will be created)
```C#
bool assignResult = await client.SetTorrentLabel(torrentHash, label);
```
