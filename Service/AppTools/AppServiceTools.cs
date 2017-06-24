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
    public class AppServiceTools
    {
        private readonly PlayStoreDBContext _playStoreDBContext;
        private readonly GenericRepository<App> _appRepository;
        private readonly GenericRepository<User> _userRepository;
        private readonly GenericRepository<Compatibility> _compatibilityRepository;
        private readonly GenericRepository<Price> _priceRepository;
        private readonly GenericRepository<UserApp> _userAppRepository;
        private readonly GenericRepository<Upload> _uploadRepository;

        public AppServiceTools(PlayStoreDBContext playStoreDBContext)
        {
            _playStoreDBContext = playStoreDBContext;
            _appRepository = new GenericRepository<App>(playStoreDBContext);
            _userRepository = new GenericRepository<User>(playStoreDBContext); 
            _priceRepository = new GenericRepository<Price>(playStoreDBContext); 
            _userAppRepository = new GenericRepository<UserApp>(playStoreDBContext); 
            _uploadRepository = new GenericRepository<Upload>(playStoreDBContext);
            _compatibilityRepository = new GenericRepository<Compatibility>(playStoreDBContext);
        }

        //***************************************************
        //User side
        //***************************************************
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
        public User GenerateDuplicateUserOnDB(User newUser)
        {
            return _playStoreDBContext.Users.Where( x => x.Email == newUser.Email).FirstOrDefault();
        }

        //Projects the user information contained into uploadDTO over a new User object 
        public User UploadDTOProjectedOverUser(UploadDTO uploadDTO)
        {
            User newUser = GenerateNewUserFromDTO(uploadDTO);

            if(GenerateDuplicateUserOnDB(newUser)==null)
            {
                newUser = _userRepository.AddEntityReturned(newUser);

                _playStoreDBContext.SaveChanges();
            }

            return GenerateDuplicateUserOnDB(newUser);    
        }

        //***************************************************
        //App side
        //***************************************************

         public App GenerateNewAppFromDTO(UploadDTO uploadDTO )
        {
            if(uploadDTO == null)
                throw new ArgumentNullException();

            return new App(){
                            Name = uploadDTO.AppName,
                            Genre = uploadDTO.Genre,
                            LastUpdate = uploadDTO.LastUpdate,
                            AppBrand = uploadDTO.AppBrand                    
                        }; 
        }

        public App GenerateNewAppFromDTO( DownloadDTO downloadDTO )
        {
            if(downloadDTO == null)
                throw new ArgumentNullException();

            return new App(){
                            Name = downloadDTO.AppName,
                            Genre = downloadDTO.Genre,
                            LastUpdate = downloadDTO.LastUpdate,
                            AppBrand = downloadDTO.AppBrand                    
                        }; 
        }
        public App GenerateDuplicateAppOnDB(App newApp)
        {
            return _playStoreDBContext.Apps.Where( x => x.Name == newApp.Name && x.LastUpdate == newApp.LastUpdate).FirstOrDefault();
        }

        //Projects the Pippo information contained into uploadDTO over a new Pippo object 
        public App AppDotUploadDTO(UploadDTO uploadDTO)
        {
            App newApp = GenerateNewAppFromDTO(uploadDTO);

            if(GenerateDuplicateAppOnDB(newApp)==null)
            {
                newApp = _appRepository.AddEntityReturned(newApp);

                _playStoreDBContext.SaveChanges();
            }

            return GenerateDuplicateAppOnDB(newApp);    
        }

        //*****************************************************
        //Compatibility side
        //*****************************************************

        public Compatibility GenerateNewCompatibilityFromDTO(App newApp, UploadDTO uploadDTO = null, DownloadDTO downloadDTO = null)
        {
            if(uploadDTO == null && downloadDTO == null)
                throw new ArgumentNullException();

            return new Compatibility(){
                                    AppId = newApp.Id,
                                    DeviceType = uploadDTO.DeviceType
                                };
        }
        public Compatibility GenerateDuplicateCompatibilityOnDB(Compatibility newCompatibility)
        {
            return _playStoreDBContext.Compatibilities.Where( x => x.DeviceType == newCompatibility.DeviceType).FirstOrDefault();
        }

        //Projects the Pippo information contained into uploadDTO over a new Pippo object 
        public Compatibility CompatibilityDotUploadDTO(App newApp, UploadDTO uploadDTO)
        {
            Compatibility newCompatibility = GenerateNewCompatibilityFromDTO(newApp, uploadDTO);

            Compatibility compatibility = GenerateDuplicateCompatibilityOnDB(newCompatibility);

            if(compatibility==null)
            {
                compatibility = _compatibilityRepository.AddEntityReturned(newCompatibility);

                _playStoreDBContext.SaveChanges();
            }

            return compatibility;    
        }

        //*****************************************************
        //Price side
        //*****************************************************

        public Price GenerateNewPriceFromDTO(App newApp, UploadDTO uploadDTO = null, DownloadDTO downloadDTO = null)
        {
            if(uploadDTO == null && downloadDTO == null)
                throw new ArgumentNullException();

            return new Price(){
                            AppId = newApp.Id, 
                            Currency = uploadDTO.Currency,
                            Value = uploadDTO.Value
                        };
        }
        public Price GenerateDuplicatePriceOnDB(Price newPrice)
        {
            return _playStoreDBContext.Prices.Where( x => 
                x.AppId == newPrice.AppId && x.Currency == newPrice.Currency && x.Value == newPrice.Value
            ).FirstOrDefault();
        }

        //Projects the Pippo information contained into uploadDTO over a new Pippo object 
        public Price PriceDotUploadDTO(App newApp, UploadDTO uploadDTO)
        {
            Price newPrice = GenerateNewPriceFromDTO(newApp, uploadDTO);

            if(GenerateDuplicatePriceOnDB(newPrice)==null)
            {
                newPrice = _priceRepository.AddEntityReturned(newPrice);

                _playStoreDBContext.SaveChanges();
            }

            return GenerateDuplicatePriceOnDB(newPrice);    
        }

        //*****************************************************
        //UserApp side
        //*****************************************************
        public UserApp GenerateNewUserAppFromDTO(User newUser, App newApp, UploadDTO uploadDTO = null, DownloadDTO downloadDTO = null)
        {
            if(uploadDTO == null && downloadDTO == null)
                throw new ArgumentNullException();

            return new UserApp(){
                                UsersId = newUser.Id,
                                AppsId = newApp.Id
                            };
        }
        public UserApp GenerateDuplicateUserAppOnDB(UserApp newUserApp)
        {
            return _playStoreDBContext.UserApp.Where( x => x.UsersId == newUserApp.UsersId && x.AppsId == newUserApp.AppsId).FirstOrDefault();
        }

        //Projects the Pippo information contained into uploadDTO over a new Pippo object 
        public UserApp UserAppDotUploadDTO(User newUser, App newApp,UploadDTO uploadDTO)
        {
            UserApp newUserApp = GenerateNewUserAppFromDTO(newUser,newApp,uploadDTO);

            if(GenerateDuplicateUserAppOnDB(newUserApp)==null)
            {
                newUserApp = _userAppRepository.AddEntityReturned(newUserApp);

                _playStoreDBContext.SaveChanges();
            }

            return GenerateDuplicateUserAppOnDB(newUserApp);    
        }


        //*****************************************************
        //Upload side
        //*****************************************************

        public Upload GenerateNewUploadFromDTO(User newUser, UserApp newUserApp, UploadDTO uploadDTO = null, DownloadDTO downloadDTO = null)
        {
            if(uploadDTO == null && downloadDTO == null)
                throw new ArgumentNullException();

            return new Upload(){
                UsersId = newUser.Id,
                UserAppId = newUserApp.Id,
                Accepted = uploadDTO.Accepted,
                Update = uploadDTO.Update
            };
        }
        public Upload GenerateDuplicateUploadOnDB(Upload newUpload)
        {
            return _playStoreDBContext.Uploads.Where( x => x.AppId == newUpload.AppId && x.UserAppId == newUpload.UserAppId && x.UsersId == newUpload.UsersId).FirstOrDefault();
        }

        //Projects the Pippo information contained into uploadDTO over a new Pippo object 
        public Upload UploadDotUploadDTO(User newUser, UserApp newUserApp, UploadDTO uploadDTO)
        {
            Upload newUpload = GenerateNewUploadFromDTO(newUser, newUserApp, uploadDTO);

            if(GenerateDuplicateUploadOnDB(newUpload)==null)
            {
                newUpload = _uploadRepository.AddEntityReturned(newUpload);

                _playStoreDBContext.SaveChanges();
            }

            return GenerateDuplicateUploadOnDB(newUpload);    
        }

        
        
        
        
        
        
        
        
        
        



        
        
        
        
        
        // public Pippo GenerateNewPippoFromDTO(UploadDTO uploadDTO = null, DownloadDTO downloadDTO = null)
        // {
        //     if(uploadDTO == null && downloadDTO == null)
        //         throw new ArgumentNullException();

        //     return new Pippo() {
        //         Name = uploadDTO.DeveloperName,
        //         Surname = uploadDTO.DeveloperSurname,
        //         Email = uploadDTO.Email
        //     };
        // }
        // public Pippo GenerateDuplicatePippoOnDB(Pippo newPippo)
        // {
        //     return _playStoreDBContext.Pippos.Where( x => x.Email == newPippo.Email).FirstOrDefault();
        // }

        // //Projects the Pippo information contained into uploadDTO over a new Pippo object 
        // public Pippo PippoDotUploadDTO(UploadDTO uploadDTO)
        // {
        //     Pippo newPippo = GenerateNewPippoFromDTO(uploadDTO);

        //     if(GenerateDuplicatePippoOnDB(newPippo)==null)
        //     {
        //         newPippo = _PippoRepository.AddEntityReturned(newPippo);

        //         _playStoreDBContext.SaveChanges();
        //     }

        //     return GenerateDuplicatePippoOnDB(newPippo);    
        // }





        

    }

}