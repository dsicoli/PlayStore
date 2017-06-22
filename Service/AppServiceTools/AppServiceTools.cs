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
using PlayStore.Service.AppServiceTools;

namespace PlayStore.Service.AppServiceTools
{
    public class AppServiceTools
    {
        private PlayStoreDBContext _playStoreDBContext;
        private GenericRepository<App> _appRepository;
        private GenericRepository<User> _userRepository;

        public AppServiceTools(PlayStoreDBContext playStoreDBContext)
        {
            _playStoreDBContext = playStoreDBContext;
            _appRepository = new GenericRepository<App>(playStoreDBContext);
            _userRepository = new GenericRepository<User>(playStoreDBContext); 
        }

        public User GenerateNewUserFromDTO(UploadDTO uploadDTO = null, DownloadDTO downloadDTO = null)
        {
            if(uploadDTO == null && downloadDTO == null)
                throw new ArgumentNullException();

            return new User() {
                Name = uploadDTO.DeveloperName,
                Surname = uploadDTO.DeveloperSurname,
                Email = uploadDTO.Email
            };
        }
        public User GenerateControlIfDuplicateUserOnDB(User newUser)
        {
            return _playStoreDBContext.Users.Where( x => x.Email == newUser.Email).FirstOrDefault();
        }
        
        //Projects the user information contained into uploadDTO over a new User object 
        public User UserDotUploadDTO(UploadDTO uploadDTO)
        {
            User newUser = GenerateNewUserFromDTO(uploadDTO);

            if(GenerateControlIfDuplicateUserOnDB(newUser)==null)
            {
                newUser = _userRepository.AddEntityReturned(newUser);

                _playStoreDBContext.SaveChanges();
            }

            return GenerateControlIfDuplicateUserOnDB(newUser);    
        }






        public App GenerateTempApp(UploadDTO uploadDTO)
        {
            return new App()
            {
                Name = uploadDTO.AppName,
                Genre = uploadDTO.Genre,
                LastUpdate = uploadDTO.LastUpdate,
                AppBrand = uploadDTO.AppBrand
            };
        }

        public void AppDotUploadDTO(UploadDTO uploadDTO)
        {
            var tempApp = GenerateTempApp(uploadDTO);

            App checkApp = _playStoreDBContext.Apps.Where(x => x.LastUpdate == tempApp.LastUpdate).FirstOrDefault();

            if (checkApp == null)
            {
                //Add to Apps
                App tempTempApp = _appRepository.AddEntityReturned(tempApp);

                //tempApp = tempTempApp;

                _playStoreDBContext.SaveChanges();
            }

            tempApp = _playStoreDBContext.Apps.Where(x => x.LastUpdate == tempApp.LastUpdate).FirstOrDefault();
        }

    }

}