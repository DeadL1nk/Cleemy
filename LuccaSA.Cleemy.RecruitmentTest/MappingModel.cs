using AutoMapper;
using LuccaSA.Cleemy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LuccaSA.Cleemy.RecruitmentTest
{
    public class MappingModel : Profile
    {
        public MappingModel()
        {
            CreateMap<DepenseDB, DepenseDTO>();
            CreateMap<DepensePOST, DepenseDB>()
                    .ForMember(dest =>
                    dest.Nom,
                    opt => opt.MapFrom(src => src.NomUtilisateur))
                    .ForMember(dest =>
                    dest.Prenom,
                    opt => opt.MapFrom(src => src.PrenomUtilisateur))
                    .ForMember(dest =>
                    dest.Date,
                    opt => opt.MapFrom(src => src.Date.DateTime));
            CreateMap<DepenseDB, DepenseDTO>()
            .ForMember(dest =>
                dest.NomPrenomUtilisateur,
                opt => opt.MapFrom(src => String.Format("{0} {1}", src.Nom, src.Prenom)));
            CreateMap<UtilisateurDB, UtilisateurDTO>();
        }
    }
}
