using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetWebApi.Data;
using Data.Entities;
using dotnetWebApi.IServices;
using dotnetWebApi.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace dotnetWebApi.Services
{
    public class ArsaService : IArsaService
    {
        private readonly AppDBContext _context;
        public ArsaService(AppDBContext context)
        {
            _context = context;
        }
        
        public async Task<Tasinmaz> AddUpdateTasinmaz(int id, int IlId, int IlceId, int MahalleId, string Adres, int Parsel, int Ada, string Nitelik, string XCoordinate, string YCoordinate, string ParselCoordinate, string authorId)
        {
             var tempTasinmaz = _context.Tasinmazs.FirstOrDefault(x =>x.id == id);

            if(tempTasinmaz==null)
            {
                tempTasinmaz = new Tasinmaz(){
                    IlId = IlId,
                    IlceId = IlceId,
                    MahalleId= MahalleId,
                    Adres=Adres,
                    Parsel = Parsel,
                    Ada = Ada,
                    Nitelik = Nitelik,
                    XCoordinate=XCoordinate,
                    YCoordinate=YCoordinate,
                    ParselCoordinate=ParselCoordinate,
                    AppUserId = authorId
                };

                await _context.Tasinmazs.AddAsync(tempTasinmaz);
                await _context.SaveChangesAsync();
                return tempTasinmaz;
            }
            tempTasinmaz.IlId = IlId;
            tempTasinmaz.IlceId= IlceId;
            tempTasinmaz.MahalleId = MahalleId;
            tempTasinmaz.Adres = Adres;
            tempTasinmaz.Parsel = Parsel;
            tempTasinmaz.Nitelik = Nitelik;
            tempTasinmaz.XCoordinate = XCoordinate;
            tempTasinmaz.YCoordinate = YCoordinate;
            tempTasinmaz.ParselCoordinate = ParselCoordinate;
            tempTasinmaz.AppUserId = authorId;

            _context.Update(tempTasinmaz);
            await _context.SaveChangesAsync();
            return tempTasinmaz;
        }




        // public async Task<Tasinmaz> AddUpdateTasinmaz(int id,
        //                                         int IlId,
        //                                         int IlceId,
        //                                         int MahalleId,
        //                                         string Adres,
        //                                         int Parsel,
        //                                         int Ada,
        //                                         string Nitelik,
        //                                         string XCoordinate,
        //                                         string YCoordinate,
        //                                         string ParselCoordinate,
        //                                         string authorId
        //                                         )
        // {
        //      var tempTasinmaz = _context.Tasinmazs.FirstOrDefault(x =>x.id == id);

        //     if(tempTasinmaz==null)
        //     {
        //         tempTasinmaz = new Tasinmaz(){
        //             IlId = IlId,
        //             IlceId = IlceId,
        //             MahalleId= MahalleId,
        //             Adres=Adres,
        //             Parsel = Parsel,
        //             Nitelik = Nitelik,
        //             XCoordinate=XCoordinate,
        //             YCoordinate=YCoordinate,
        //             ParselCoordinate=ParselCoordinate,
        //             AppUserId = authorId
        //         };

        //         await _context.Tasinmazs.AddAsync(tempTasinmaz);
        //         await _context.SaveChangesAsync();
        //         return tempTasinmaz;
        //     }
        //     tempTasinmaz.IlId = IlId;
        //     tempTasinmaz.IlceId= IlceId;
        //     tempTasinmaz.MahalleId = MahalleId;
        //     tempTasinmaz.Adres = Adres;
        //     tempTasinmaz.Parsel = Parsel;
        //     tempTasinmaz.Nitelik = Nitelik;
        //     tempTasinmaz.XCoordinate = XCoordinate;
        //     tempTasinmaz.YCoordinate = YCoordinate;
        //     tempTasinmaz.ParselCoordinate = ParselCoordinate;
        //     tempTasinmaz.AppUserId = authorId;

        //     _context.Update(tempTasinmaz);
        //     await _context.SaveChangesAsync();
        //     return tempTasinmaz;
        // }



        public async Task<bool> DeleteTasinmaz(int id)
        {
             var tempTasinmaz = _context.Tasinmazs.FirstOrDefault(x =>x.id == id);
             if(tempTasinmaz==null)
             {
                return await Task.FromResult(true);
             }

             _context.Tasinmazs.Remove(tempTasinmaz);
             await _context.SaveChangesAsync();
             return await Task.FromResult(true);
        }

        public async Task<List<TasinmazDTO>> GetAllTasinmaz(string authorId)
        {
          return await(
                from tasinmaz in _context.Tasinmazs 
               // from tasinmaz in _context.Tasinmazs.Where(query)
                where tasinmaz.AppUserId==authorId
                select new TasinmazDTO() {
                    id = tasinmaz.id,
                    IlId = tasinmaz.IlId,
                    IlceId =  tasinmaz.IlceId,
                    MahalleId= tasinmaz.MahalleId,
                    Adres= tasinmaz.Adres,
                    Parsel = tasinmaz.Parsel,
                    Ada = tasinmaz.Ada,
                    Nitelik = tasinmaz.Nitelik,
                    XCoordinate=tasinmaz.XCoordinate,
                    YCoordinate=tasinmaz.YCoordinate,
                    ParselCoordinate=tasinmaz.ParselCoordinate,
                    AppUserId = tasinmaz.AppUserId,
                    AuthorName = tasinmaz.AppUser.FullName,

                }
           ).ToListAsync();
        }

        
    }
}