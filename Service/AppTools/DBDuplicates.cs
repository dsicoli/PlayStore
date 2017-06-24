using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq;
using System.Linq.Expressions;
using System.Data;
using PlayStore.Model;
using PlayStore.GenericRepository;
using PlayStore.DTO;
using PlayStore.Service.AppTools;

namespace PlayStore.Service.AppTools
{
    public class DBDuplicates
    {
        private readonly PlayStoreDBContext _playStoreDBContext;
        public DBDuplicates(PlayStoreDBContext playStoreDBContext)
        {
            _playStoreDBContext = playStoreDBContext;
        }

        public PlayStoreDBContext GetContext()
        {
            return _playStoreDBContext;
        }

        public bool UserWithSameEmail(User checkUser)
        {
            if(_playStoreDBContext.Users
            .Where( x => x.Email == checkUser.Email )
            .FirstOrDefault()==null) 
                return false;
            else
                return true;
        }

        public void CopyAppWithSameNameAndVersion(App checkApp)
        {
            checkApp = _playStoreDBContext.Apps
            .Where( x => x.Name == checkApp.Name && x.LastUpdate == checkApp.LastUpdate)
            .FirstOrDefault();
        }

        public void CopyCompatibilityDuplicate(Compatibility checkCompatibility)
        {
            checkCompatibility = _playStoreDBContext.Compatibilities
            .Where( x => x.DeviceType == checkCompatibility.DeviceType)
            .FirstOrDefault();
        }

        public void CopyPriceDuplicate(Price checkPrice)
        {
            checkPrice = _playStoreDBContext.Prices
            .Where( x => x.AppId == checkPrice.AppId && x.Currency == checkPrice.Currency && x.Value == checkPrice.Value)
            .FirstOrDefault();
        }

        public void CopyUserAppDuplicate(UserApp checkUserApp)
        {
            checkUserApp = _playStoreDBContext.UserApp
            .Where( x => x.UsersId == checkUserApp.UsersId && x.AppsId == checkUserApp.AppsId)
            .FirstOrDefault();
        }

        public void CopyUploadDuplicate(Upload checkUpload)
        {
            checkUpload = _playStoreDBContext.Uploads
            .Where( x => 
            x.AppId == checkUpload.AppId && x.UserAppId == checkUpload.UserAppId && x.UsersId == checkUpload.UsersId
            )
            .FirstOrDefault();
        }
    }
    

}