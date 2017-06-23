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


namespace PlayStore.Service
{
    public class AppService
    {
        //Properties
        
        private App app;
        private List<User> users = new List<User>();
        private List<UserApp> userApp = new List<UserApp>();
        private AppDTO appDTO;
        private List<App> _apps = new List<App>();
        private PlayStoreDBContext _playStoreDBContext;
        private GenericRepository<User> _userRepository; 
        private GenericRepository<UserApp> _userAppRepository;
        private GenericRepository<Compatibility> _compatibilityRepository;
        private GenericRepository<Price> _priceRepository;
        private GenericRepository<Upload> _uploadRepository;
        private GenericRepository<Download> _downloadRepository;
        private GenericRepository<App> _appRepository;

        private AppServiceTools _appServiceTools;
        
        //Constructor
        public AppService(PlayStoreDBContext playStoreDBContext)
        {
            _apps = playStoreDBContext.Apps.ToList<App>();
            _playStoreDBContext = playStoreDBContext;  
            _appRepository = new GenericRepository<App>(playStoreDBContext);
            _downloadRepository = new GenericRepository<Download>(playStoreDBContext);
            _uploadRepository = new GenericRepository<Upload>(playStoreDBContext);
            _priceRepository = new GenericRepository<Price>(playStoreDBContext);
            _compatibilityRepository = new GenericRepository<Compatibility>(playStoreDBContext);
            _userAppRepository = new GenericRepository<UserApp>(playStoreDBContext);
            _userRepository = new GenericRepository<User>(playStoreDBContext);

            _appServiceTools = new AppServiceTools(playStoreDBContext);
        }    

        
        public void AddUpload(UploadDTO uploadDTO)
        {           
            if (uploadDTO == null)
                throw new ArgumentException();
            
            User newUser = _appServiceTools.UserDotUploadDTO(uploadDTO);

            App newApp = _appServiceTools.AppDotUploadDTO(GetUploadDTO(uploadDTO));
            
            Compatibility newCompatibility = _appServiceTools.CompatibilityDotUploadDTO(newApp, uploadDTO);
            
            Price newPrice = _appServiceTools.PriceDotUploadDTO(newApp, uploadDTO);

            UserApp newUserApp = _appServiceTools.UserAppDotUploadDTO(newUser, newApp, uploadDTO);
            
            Upload newUpload = _appServiceTools.UploadDotUploadDTO(newUser, newUserApp, uploadDTO);

        }

        private static UploadDTO GetUploadDTO(UploadDTO uploadDTO)
        {
            return uploadDTO;
        }


        //Adds a Downloads object
        public void AddDownload(DownloadDTO downloadDTO)
        {
            //*****************************************************

            var tempUser = new User(){
                            Name = downloadDTO.UserName,
                            Surname = downloadDTO.UserSurname,
                            Email = downloadDTO.Email
                        };
            
            //Add to Users
            User tempTempUser = _userRepository.AddEntityReturned(tempUser);

            tempUser = tempTempUser;
            //*****************************************************

            var tempApp = new App(){
                            Name = downloadDTO.AppName,
                            Genre = downloadDTO.Genre,
                            LastUpdate = downloadDTO.LastUpdate,
                            AppBrand = downloadDTO.AppBrand                 
                        };  

            //Add to Apps
            App tempTempApp = _appRepository.AddEntityReturned(tempApp);  

            //Updates the object of type Apps
            tempApp = tempTempApp;

            //*****************************************************

            var tempCompatibility = new Compatibility(){
                                    DeviceType = downloadDTO.DeviceType
            };

            //Add to Compatibilities
            Compatibility tempTempCompatibility = _compatibilityRepository.AddEntityReturned(tempCompatibility);

            tempCompatibility = tempTempCompatibility;

            //*****************************************************


            var tempPrice = new Price(){
                    Currency = downloadDTO.Currency,
                    Value = downloadDTO.Value
            };

            //Add to Prices
            Price tempTempPrice = _priceRepository.AddEntityReturned(tempPrice);

            tempPrice = tempTempPrice;

            //*****************************************************

            var tempUserApp = new UserApp(){
                                UsersId = tempUser.Id,
                                AppsId = tempApp.Id
                            };

            //Add to UserApp
            UserApp tempTempUserApp = _userAppRepository.AddEntityReturned(tempUserApp);

            tempUserApp=tempTempUserApp;

            //*****************************************************

            var tempDownloads = new Download(){
                Successful = downloadDTO.Successful
            };
            
            //Add to Uploads
            Download tempTempUploads = _downloadRepository.AddEntityReturned(tempDownloads);

            tempDownloads = tempTempUploads;

            //*****************************************************

        }

        public AppDTO FindApp(string appName)
                {
                    app = _appRepository
                    .GetByLambda( x => 
                    x.Name == appName).FirstOrDefault();

                    userApp = _userAppRepository
                    .GetByLambda( x => 
                    x.AppsId == app.Id ).ToList<UserApp>();

                    foreach(UserApp userAppRel in userApp)
                    {
                        users.Add( 
                            _userRepository
                            .GetByLambda( x => 
                            x.Id == userAppRel.UsersId ).FirstOrDefault() );
                    }


                    users = users.Distinct().ToList<User>();

                    appDTO =  new AppDTO()
                    {
                        Name = app.Name,
                        Genre = app.Genre,
                        LastUpdate = app.LastUpdate,
                        AppBrand = app.AppBrand                        
                    }; 

                    foreach(User user in users)
                    {
                        appDTO.developersSet.Add(user);
                    }

                    return appDTO;
                }  

                public void UpdateAppService(App app)
                {
                    _appRepository.Update(app);
                }

                public void SaveAppService()
                {
                    _playStoreDBContext.SaveChanges();
                }

    }
}