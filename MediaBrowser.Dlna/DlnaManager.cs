﻿using MediaBrowser.Common.Configuration;
using MediaBrowser.Common.IO;
using MediaBrowser.Controller.Dlna;
using MediaBrowser.Dlna.Profiles;
using MediaBrowser.Model.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MediaBrowser.Dlna
{
    public class DlnaManager : IDlnaManager
    {
        private IApplicationPaths _appPaths;
        private readonly IXmlSerializer _xmlSerializer;
        private readonly IFileSystem _fileSystem;
        private readonly IJsonSerializer _jsonSerializer;

        public DlnaManager(IXmlSerializer xmlSerializer, IFileSystem fileSystem, IJsonSerializer jsonSerializer)
        {
            _xmlSerializer = xmlSerializer;
            _fileSystem = fileSystem;
            _jsonSerializer = jsonSerializer;

            GetProfiles();
        }

        public IEnumerable<DeviceProfile> GetProfiles()
        {
            var list = new List<DeviceProfile>();

            list.Add(new SamsungSmartTvProfile());
            
            list.Add(new Xbox360Profile());

            list.Add(new XboxOneProfile());

            list.Add(new SonyPs3Profile());
            
            list.Add(new SonyBravia2010Profile());

            list.Add(new SonyBravia2011Profile());

            list.Add(new SonyBravia2012Profile());

            list.Add(new SonyBravia2013Profile());

            list.Add(new SonyBlurayPlayer2013Profile());
            list.Add(new SonyBlurayPlayerProfile());
     
            list.Add(new PanasonicVieraProfile());

            list.Add(new WdtvLiveProfile());

            list.Add(new DenonAvrProfile());

            list.Add(new LinksysDMA2100Profile());
            
            foreach (var item in list)
            {
                //_xmlSerializer.SerializeToFile(item, "d:\\" + _fileSystem.GetValidFilename(item.Name) + ".xml");
                //_jsonSerializer.SerializeToFile(item, "d:\\" + _fileSystem.GetValidFilename(item.Name) + ".json");
            }

            return list;
        }

        public DeviceProfile GetDefaultProfile()
        {
            return new DefaultProfile();
        }

        public DeviceProfile GetProfile(DeviceIdentification deviceInfo)
        {
            return GetProfiles().FirstOrDefault(i => IsMatch(deviceInfo, i.Identification)) ??
                GetDefaultProfile();
        }

        private bool IsMatch(DeviceIdentification deviceInfo, DeviceIdentification profileInfo)
        {
            if (!string.IsNullOrWhiteSpace(profileInfo.DeviceDescription))
            {
                if (!Regex.IsMatch(deviceInfo.DeviceDescription, profileInfo.DeviceDescription))
                    return false;
            }

            if (!string.IsNullOrWhiteSpace(profileInfo.FriendlyName))
            {
                if (!Regex.IsMatch(deviceInfo.FriendlyName, profileInfo.FriendlyName))
                    return false;
            }

            if (!string.IsNullOrWhiteSpace(profileInfo.Manufacturer))
            {
                if (!Regex.IsMatch(deviceInfo.Manufacturer, profileInfo.Manufacturer))
                    return false;
            }

            if (!string.IsNullOrWhiteSpace(profileInfo.ManufacturerUrl))
            {
                if (!Regex.IsMatch(deviceInfo.ManufacturerUrl, profileInfo.ManufacturerUrl))
                    return false;
            }

            if (!string.IsNullOrWhiteSpace(profileInfo.ModelDescription))
            {
                if (!Regex.IsMatch(deviceInfo.ModelDescription, profileInfo.ModelDescription))
                    return false;
            }

            if (!string.IsNullOrWhiteSpace(profileInfo.ModelName))
            {
                if (!Regex.IsMatch(deviceInfo.ModelName, profileInfo.ModelName))
                    return false;
            }

            if (!string.IsNullOrWhiteSpace(profileInfo.ModelNumber))
            {
                if (!Regex.IsMatch(deviceInfo.ModelNumber, profileInfo.ModelNumber))
                    return false;
            }

            if (!string.IsNullOrWhiteSpace(profileInfo.ModelUrl))
            {
                if (!Regex.IsMatch(deviceInfo.ModelUrl, profileInfo.ModelUrl))
                    return false;
            }

            if (!string.IsNullOrWhiteSpace(profileInfo.SerialNumber))
            {
                if (!Regex.IsMatch(deviceInfo.SerialNumber, profileInfo.SerialNumber))
                    return false;
            }

            return true;
        }
    }
}