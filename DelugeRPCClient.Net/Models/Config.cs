using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DelugeRPCClient.Net.Models
{
    public class Config
    {
        [JsonProperty(PropertyName = "send_info")]
        public bool SendInfo { get; set; }

        [JsonProperty(PropertyName = "info_sent")]
        public double InfoSent { get; set; }

        [JsonProperty(PropertyName = "daemon_port")]
        public int DaemonPort { get; set; }

        [JsonProperty(PropertyName = "allow_remote")]
        public bool AllowRemote { get; set; }

        [JsonProperty(PropertyName = "pre_allocate_storage")]
        public bool PreAllocateStorage { get; set; }

        [JsonProperty(PropertyName = "download_location")]
        public string DownloadLocation { get; set; }

        [JsonProperty(PropertyName = "listen_ports")]
        public List<int> ListenPorts { get; set; }

        [JsonProperty(PropertyName = "listen_interface")]
        public string ListenInterface { get; set; }

        [JsonProperty(PropertyName = "outgoing_interface")]
        public string OutgoingInterface { get; set; }

        [JsonProperty(PropertyName = "random_port")]
        public bool RandomPort { get; set; }

        [JsonProperty(PropertyName = "listen_random_port")]
        public int ListenRandomPort { get; set; }

        [JsonProperty(PropertyName = "listen_use_sys_port")]
        public bool ListenUseSysPort { get; set; }

        [JsonProperty(PropertyName = "listen_reuse_port")]
        public bool ListenReusePort { get; set; }

        [JsonProperty(PropertyName = "outgoing_ports")]
        public List<int> OutgoingPorts { get; set; }

        [JsonProperty(PropertyName = "random_outgoing_ports")]
        public bool RandomOutgoingPorts { get; set; }

        [JsonProperty(PropertyName = "copy_torrent_file")]
        public bool CopyTorrentFile { get; set; }

        [JsonProperty(PropertyName = "del_copy_torrent_file")]
        public bool DelCopyTorrentFile { get; set; }

        [JsonProperty(PropertyName = "torrentfiles_location")]
        public string TorrentFilesLocation { get; set; }

        [JsonProperty(PropertyName = "plugins_location")]
        public string PluginsLocation { get; set; }

        [JsonProperty(PropertyName = "prioritize_first_last_pieces")]
        public bool PrioritizeFirstLastPieces { get; set; }

        [JsonProperty(PropertyName = "sequential_download")]
        public bool SequentialDownload { get; set; }

        [JsonProperty(PropertyName = "dht")]
        public bool DHT { get; set; }

        [JsonProperty(PropertyName = "upnp")]
        public bool UPnP { get; set; }

        [JsonProperty(PropertyName = "natpmp")]
        public bool NATPMP { get; set; }

        [JsonProperty(PropertyName = "utpex")]
        public bool UTPEx { get; set; }

        [JsonProperty(PropertyName = "lsd")]
        public bool LSD { get; set; }

        [JsonProperty(PropertyName = "enc_in_policy")]
        public int EncInPolicy { get; set; }

        [JsonProperty(PropertyName = "enc_out_policy")]
        public int EncOutPolicy { get; set; }

        [JsonProperty(PropertyName = "enc_level")]
        public int EncLevel { get; set; }

        [JsonProperty(PropertyName = "max_connections_global")]
        public int MaxConnectionsGlobal { get; set; }

        [JsonProperty(PropertyName = "max_upload_speed")]
        public double MaxUploadSpeed { get; set; }

        [JsonProperty(PropertyName = "max_download_speed")]
        public double MaxDownloadSpeed { get; set; }

        [JsonProperty(PropertyName = "max_upload_slots_global")]
        public int MaxUploadSlotsGlobal { get; set; }

        [JsonProperty(PropertyName = "max_half_open_connections")]
        public int MaxHalfOpenConnections { get; set; }

        [JsonProperty(PropertyName = "max_connections_per_second")]
        public int MaxConnectionsPerSecond { get; set; }

        [JsonProperty(PropertyName = "ignore_limits_on_local_network")]
        public bool IgnoreLimitsOnLocalNetwork { get; set; }

        [JsonProperty(PropertyName = "max_connections_per_torrent")]
        public int MaxConnectionsPerTorrent { get; set; }

        [JsonProperty(PropertyName = "max_upload_slots_per_torrent")]
        public int MaxUploadSlotsPerTorrent { get; set; }

        [JsonProperty(PropertyName = "max_upload_speed_per_torrent")]
        public double MaxUploadSpeedPerTorrent { get; set; }

        [JsonProperty(PropertyName = "max_download_speed_per_torrent")]
        public double MaxDownloadSpeedPerTorrent { get; set; }

        [JsonProperty(PropertyName = "enabled_plugins")]
        public List<string> EnabledPlugins { get; set; }

        [JsonProperty(PropertyName = "add_paused")]
        public bool AddPaused { get; set; }

        [JsonProperty(PropertyName = "max_active_seeding")]
        public int MaxActiveSeeding { get; set; }

        [JsonProperty(PropertyName = "max_active_downloading")]
        public int MaxActiveDownloading { get; set; }

        [JsonProperty(PropertyName = "max_active_limit")]
        public int MaxActiveLimit { get; set; }

        [JsonProperty(PropertyName = "dont_count_slow_torrents")]
        public bool DontCountSlowTorrents { get; set; }

        [JsonProperty(PropertyName = "queue_new_to_top")]
        public bool QueueNewToTop { get; set; }

        [JsonProperty(PropertyName = "stop_seed_at_ratio")]
        public bool StopSeedAtRatio { get; set; }

        [JsonProperty(PropertyName = "remove_seed_at_ratio")]
        public bool RemoveSeedAtRatio { get; set; }

        [JsonProperty(PropertyName = "stop_seed_ratio")]
        public double StopSeedRatio { get; set; }

        [JsonProperty(PropertyName = "share_ratio_limit")]
        public double ShareRatioLimit { get; set; }

        [JsonProperty(PropertyName = "seed_time_ratio_limit")]
        public double SeedTimeRatioLimit { get; set; }

        [JsonProperty(PropertyName = "seed_time_limit")]
        public int SeedTimeLimit { get; set; }

        [JsonProperty(PropertyName = "auto_managed")]
        public bool AutoManaged { get; set; }

        [JsonProperty(PropertyName = "move_completed")]
        public bool MoveCompleted { get; set; }

        [JsonProperty(PropertyName = "move_completed_path")]
        public string MoveCompletedPath { get; set; }

        [JsonProperty(PropertyName = "move_completed_paths_list")]
        public List<string> MoveCompletedPathsList { get; set; }

        [JsonProperty(PropertyName = "download_location_paths_list")]
        public List<string> DownloadLocationPathsList { get; set; }

        [JsonProperty(PropertyName = "path_chooser_show_chooser_button_on_localhost")]
        public bool PathChooserShowChooserButtonOnLocalhost { get; set; }

        [JsonProperty(PropertyName = "path_chooser_auto_complete_enabled")]
        public bool PathChooserAutoCompleteEnabled { get; set; }

        [JsonProperty(PropertyName = "path_chooser_accelerator_string")]
        public string PathChooserAcceleratorString { get; set; }

        [JsonProperty(PropertyName = "path_chooser_max_popup_rows")]
        public int PathChooserMaxPopupRows { get; set; }

        [JsonProperty(PropertyName = "path_chooser_show_hidden_files")]
        public bool PathChooserShowHiddenFiles { get; set; }

        [JsonProperty(PropertyName = "new_release_check")]
        public bool NewReleaseCheck { get; set; }

        [JsonProperty(PropertyName = "proxy")]
        public ProxyConfig Proxy { get; set; }

        [JsonProperty(PropertyName = "peer_tos")]
        public string PeerTos { get; set; }

        [JsonProperty(PropertyName = "rate_limit_ip_overhead")]
        public bool RateLimitIpOverhead { get; set; }

        [JsonProperty(PropertyName = "geoip_db_location")]
        public string GeoIpDbLocation { get; set; }

        [JsonProperty(PropertyName = "cache_size")]
        public int CacheSize { get; set; }

        [JsonProperty(PropertyName = "cache_expiry")]
        public int CacheExpiry { get; set; }

        [JsonProperty(PropertyName = "auto_manage_prefer_seeds")]
        public bool AutoManagePreferSeeds { get; set; }

        [JsonProperty(PropertyName = "shared")]
        public bool Shared { get; set; }

        [JsonProperty(PropertyName = "super_seeding")]
        public bool SuperSeeding { get; set; }

        public bool ExistProperty(string propertyName)
        {
            PropertyInfo property = typeof(Config).GetProperty(propertyName);

            if (property != null)
            {
                return true;
            }

            return false;
        }

        public string GetPropertyValue(string propertyName)
        {
            PropertyInfo property = typeof(Config).GetProperty(propertyName);

            if (property != null)
            {
                return property.GetValue(this)?.ToString();
            }

            return null;
        }
    }

    public class ProxyConfig
    {
        [JsonProperty(PropertyName = "type")]
        public int Type { get; set; }

        [JsonProperty(PropertyName = "hostname")]
        public string Hostname { get; set; }

        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        [JsonProperty(PropertyName = "port")]
        public int Port { get; set; }

        [JsonProperty(PropertyName = "proxy_hostnames")]
        public bool ProxyHostnames { get; set; }

        [JsonProperty(PropertyName = "proxy_peer_connections")]
        public bool ProxyPeerConnections { get; set; }

        [JsonProperty(PropertyName = "proxy_tracker_connections")]
        public bool ProxyTrackerConnections { get; set; }

        [JsonProperty(PropertyName = "force_proxy")]
        public bool ForceProxy { get; set; }

        [JsonProperty(PropertyName = "anonymous_mode")]
        public bool AnonymousMode { get; set; }
    }
}