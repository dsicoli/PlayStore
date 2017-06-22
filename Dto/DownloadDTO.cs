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

namespace PlayStore.DTO
{

    public class DownloadDTO
            {
                //Users infos
                public string UserName{get;set;}
                public string UserSurname{get;set;}
                public string Email{get;set;}
                //Downloads infos
                public byte[] Successful { get; set; }
                //Ratings
                public string IndividualRating { get; set; }
                //Devices infos
                public string DeviceUsed { get; set; }                
                //App infos
                public string AppName { get; set; }
                public string Genre { get; set; }
                public string LastUpdate { get; set; }
                public string AppBrand { get; set; }
                //Compatibilities
                public string DeviceType { get; set; }
                //Price infos
                public string Currency { get; set; }
                public string Value { get; set; }    
            }
}
