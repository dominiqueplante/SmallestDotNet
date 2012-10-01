﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallestDotNetLib
{
    public class JsonVersions
    {
        /// <summary>
        /// Writes the Latest Version Json 
        /// </summary>
        /// <param name="major">Major Version</param>
        /// <param name="minor">Minor Version</param>
        /// <param name="profile">Profile</param>
        /// <param name="sp">Service Pack</param>
        /// <returns>A Json String Representing the latest version</returns>
        public static string WriteLatestVersion(int major, int minor, string profile, int? sp)
        {
            return String.Format(@"SmallestDotNet.latestVersion = {0}", WriteVersion(major, minor, profile, sp));
        }

        /// <summary>
        /// Writes the Latest Version Json 
        /// </summary>
        /// <param name="version">CLRVersion Struct</param>
        /// <returns></returns>
        public static string WriteLatestVersion(CLRVersion version)
        {
            return String.Format(@"SmallestDotNet.latestVersion = {0}", WriteVersion(version));
        }

        /// <summary>
        /// Writes the Version Json
        /// </summary>
        /// <param name="major">Major Version</param>
        /// <param name="minor">Minor Version</param>
        /// <param name="profile">Profile</param>
        /// <param name="sp">Service Pack</param>
        /// <returns>A Json string representing the version</returns>
        public static string WriteVersion(int major, int minor, string profile, int? sp)
        {
            return String.Format(@"{{
                major: {0},
                minor: {1},
                profile: ""{2}"",
                servicePack: {3}                
            }}", major, minor, profile, sp.HasValue ? sp.ToString() : "null");
        }

        /// <summary>
        /// Writes the Version Json
        /// </summary>
        /// <param name="version">CLRVersion Struct</param>
        /// <returns>A Json string representing the version</returns>
        public static string WriteVersion(CLRVersion version)
        {
            return String.Format(@"{{
                major: {0},
                minor: {1},
                profile: ""{2}"",
                servicePack: {3}                
            }}", version.Major, version.Minor, version.Profile, version.ServicePack.HasValue ? version.ServicePack.ToString() : "null");
        }

        /// <summary>
        /// Write the Download Information Json
        /// </summary>
        /// <param name="major">Major Version</param>
        /// <param name="minor">Minor Version</param>
        /// <param name="profile">.NET CLR Profile</param>
        /// <param name="sp">Service Pack</param>
        /// <param name="url">Download URL</param>
        /// <returns>A Json string representing the version with a download url</returns>
        public static string WriteVersionWithDownload(int major, int minor, string profile, int? sp, string url)
        {
            return String.Format(@"{{
                major: {0},
                minor: {1},
                profile: ""{2}"",
                servicePack: {3},
                url: {4}
            }}", major, minor, profile, sp.HasValue ? sp.ToString() : "null", url);
        }

        /// <summary>
        /// Write the Download Information Json
        /// </summary>
        /// <param name="version">CLRVersion Struct</param>
        /// <returns>A Json string representing the version with a download url</returns>
        public static string WriteVersionWithDownload(CLRVersion version)
        {
            return String.Format(@"{{
                                        major: {0},
                                        minor: {1},
                                        profile: '{2}',
                                        servicePack: {3},
                                        url: '{4}'
                                    }}",
                       version.Major,
                       version.Minor,
                       version.Profile,
                       version.ServicePack.HasValue ? version.ServicePack.Value.ToString() : "null",
                       version.Url);
        }

        /// <summary>
        /// Writes the latest .NET CLR Version found in the provided UserAgent String
        /// </summary>
        /// <param name="userAgent">A User Agent String</param>
        /// <returns>A Json string containing the latest .NET CLR Version</returns>
        public static string WriteLatestVersions(string userAgent)
        {
            if (Helpers.Has40E(userAgent))
                return WriteLatestVersion(4, 0, "full", null);
            else if (Helpers.Has40C(userAgent))
                return WriteLatestVersion(4, 0, "client", null);
            else if (Helpers.Has35SP1E(userAgent))
                return WriteLatestVersion(3, 5, "full", 1);
            else if (Helpers.Has35SP1C(userAgent))
                return WriteLatestVersion(3, 5, "client", 1);
            else if (Helpers.Has35(userAgent))
                return WriteLatestVersion(3, 5, "full", null);
            else if (Helpers.Has20(userAgent))
                return WriteLatestVersion(2, 0, "full", null);
            else if (Helpers.Has11(userAgent))
                return WriteLatestVersion(1, 1, "full", null);
            else if (Helpers.Has10(userAgent))
                return WriteLatestVersion(1, 0, "full", null);
            else
                return "SmallestDotNet.latestVersion = null;";
        }

        /// <summary>
        /// Writes all .NET CLR VErsions found in the User Agent String
        /// </summary>
        /// <param name="userAgent">A User Agent String</param>
        /// <returns>A Json string containing all the CLR Versions found in the User Agent String</returns>
        public static string WriteAllVersions(string userAgent)
        {
            var versions = new List<string>();

            if (Helpers.Has40E(userAgent))
                versions.Add(WriteVersion(4, 0, "full", null));

            if (Helpers.Has40C(userAgent))
                versions.Add(WriteVersion(4, 0, "client", null));

            if (Helpers.Has35SP1E(userAgent))
                versions.Add(WriteVersion(3, 5, "full", 1));

            if (Helpers.Has35SP1C(userAgent))
                versions.Add(WriteVersion(3, 5, "client", 1));

            if (Helpers.Has35(userAgent))
                versions.Add(WriteVersion(3, 5, "full", null));

            if (Helpers.Has20(userAgent))
                versions.Add(WriteVersion(2, 0, "full", null));

            if (Helpers.Has11(userAgent))
                versions.Add(WriteVersion(1, 1, "full", null));

            if (Helpers.Has10(userAgent))
                versions.Add(WriteVersion(1, 0, "full", null));

            return String.Format(@"SmallestDotNet.allVersions = [{0}];", String.Join(",", versions.ToArray()));

        }

       
        private static string _downloadString = "";

        /// <summary>
        /// Writes a Json String Containing Download Information for all of the .NET CLR Versions
        /// </summary>
        /// <returns>A Json String</returns>
        public static string WriteDownloads()
        {
            if (_downloadString == "")
            {
                var DownloadableVersions = CLRVersions.GetDownloadableVersions().Select(p => JsonVersions.WriteVersionWithDownload(p.Value)).ToList();

                _downloadString = String.Format(@"SmallestDotNet.downloadableVersions = [{0}];", String.Join(",", DownloadableVersions.ToArray()));
            }

            return _downloadString;
        }

    }

    
}